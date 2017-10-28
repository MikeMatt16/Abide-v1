using Abide.Guerilla.Managed;
using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Abide.Guerilla.CodeDom
{
    /// <summary>
    /// Represents a tag block CodeDOM program graph.
    /// </summary>
    public sealed class TagBlockCodeGraph
    {
        private readonly string tagBlockName;
        private readonly GuerillaInstance guerilla;
        private readonly CodeCompileUnit unit;
        private readonly CodeTypeDeclaration typeDeclaration;
        private readonly string outputDirectory;
        private readonly List<string> memberNameList = new List<string>();
        private readonly Dictionary<TagFieldDefinition, string> optionsDictionary = new Dictionary<TagFieldDefinition, string>();
        private readonly Dictionary<TagFieldDefinitionArray, string> arrayDictionary = new Dictionary<TagFieldDefinitionArray, string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="TagBlockCodeGraph"/> using the supplied Guerilla instance, tag block definition, and output directory.
        /// </summary>
        /// <param name="guerilla">The guerilla instance.</param>
        /// <param name="tagBlockDefinition">The tag block definition.</param>
        /// <param name="outputDirectory">The output directory of the code graph.</param>
        public TagBlockCodeGraph(GuerillaInstance guerilla, TagBlockDefinition tagBlockDefinition, string outputDirectory)
        {
            //Setup
            tagBlockName = tagBlockDefinition.Name;
            unit = new CodeCompileUnit();
            this.outputDirectory = outputDirectory;
            this.guerilla = guerilla;
            
            //Create Namespace and add using statements
            CodeNamespace tags = new CodeNamespace("Abide.Guerilla.Tags");
            tags.Imports.Add(new CodeNamespaceImport("Abide.Guerilla.Types"));
            tags.Imports.Add(new CodeNamespaceImport("Abide.HaloLibrary"));
            tags.Imports.Add(new CodeNamespaceImport("System"));

            //Add
            memberNameList.Add(tagBlockDefinition.Name);

            //Create block structure
            typeDeclaration = new CodeTypeDeclaration(tagBlockDefinition.Name)
            {
                TypeAttributes = TypeAttributes.Public,
                IsClass = true
            };

            //Field set attribute
            CodeAttributeDeclaration fieldSetAttribute = new CodeAttributeDeclaration(new CodeTypeReference(typeof(Tags.FieldSetAttribute)));
            fieldSetAttribute.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(tagBlockDefinition.GetLatestFieldSet().Size)));
            fieldSetAttribute.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(tagBlockDefinition.GetLatestFieldSet().Alignment)));
            typeDeclaration.CustomAttributes.Add(fieldSetAttribute);

            //Check
            if (tagBlockDefinition.IsTagGroup)
            {
                //Get Tag group
                TagGroupDefinition tagGroupDefinition = guerilla.FindTagGroup(tagBlockDefinition.Address);

                //Field set attribute
                CodeAttributeDeclaration tagGroupAttribute = new CodeAttributeDeclaration(new CodeTypeReference(typeof(Tags.TagGroupAttribute)));
                tagGroupAttribute.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(tagGroupDefinition.Name)));
                tagGroupAttribute.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(tagGroupDefinition.GroupTag.FourCc)));
                tagGroupAttribute.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(tagGroupDefinition.ParentGroupTag.FourCc)));
                tagGroupAttribute.Arguments.Add(new CodeAttributeArgument(new CodeTypeReferenceExpression($"typeof({tagBlockDefinition.Name})")));
                typeDeclaration.CustomAttributes.Add(tagGroupAttribute);
            }

            //Create Array elements
            foreach (TagFieldDefinitionArray arrayField in tagBlockDefinition.GetLatestFieldDefinitions().Where(f => f.Type == FieldType.FieldArrayStart))
            {
                //Create unique element name
                string elementName = CreateUniqueName(NameFilter(arrayField.Name) + " element");

                //Add member
                memberNameList.Add(elementName);
                arrayDictionary.Add(arrayField, elementName);

                //Search
                RecursiveSearch(arrayField.GetFields());

                //Make element
                CodeTypeDeclaration element = new CodeTypeDeclaration(elementName);
                element.IsClass = true;

                //Add elements
                AddFields(element, arrayField.GetFields());

                //Add
                typeDeclaration.Members.Add(element);
            }
            
            //Create Option enumerations
            foreach (TagFieldEnumDefinition enumField in tagBlockDefinition.GetLatestFieldDefinitions().
                Where(f => f.Type == FieldType.FieldEnum || f.Type == FieldType.FieldCharEnum || f.Type == FieldType.FieldLongEnum ||
                f.Type == FieldType.FieldByteFlags || f.Type == FieldType.FieldWordFlags || f.Type == FieldType.FieldLongFlags))
            {
                //Create unique enum name
                string enumName = CreateUniqueName(enumField.Name + " options");

                //Add
                memberNameList.Add(enumName);
                optionsDictionary.Add(enumField, enumName);

                //Make enum
                CodeTypeDeclaration enumDeclaration = new CodeTypeDeclaration(enumName);
                enumDeclaration.IsEnum = true;

                //Populate options
                List<string> optionNameList = new List<string>(); int index = 0; int value = 0;
                foreach (string option in enumField.Options)
                {
                    //Create unique field name
                    string optionName = NameFilter(option); if (string.IsNullOrWhiteSpace(optionName)) optionName = "EMPTY_STRING";
                    string formattedOptionName = GetPropertyName(optionName); int optionCloneIndex = 1;
                    while (optionNameList.Contains(formattedOptionName))
                    {
                        formattedOptionName = $"{GetPropertyName(optionName)}{optionCloneIndex}";
                        optionCloneIndex++;
                    }

                    //Add
                    optionNameList.Add(formattedOptionName);

                    //Create member
                    CodeMemberField memberField = new CodeMemberField(enumName, formattedOptionName);
                    if (enumField.Type == FieldType.FieldByteFlags || enumField.Type == FieldType.FieldWordFlags || enumField.Type == FieldType.FieldLongFlags) value = 1 << index++;
                    else value = index++;
                    memberField.InitExpression = new CodePrimitiveExpression(value);
                    enumDeclaration.Members.Add(memberField);
                }

                //Add
                typeDeclaration.Members.Add(enumDeclaration);
            }

            //Add...
            tags.Types.Add(typeDeclaration);
            unit.Namespaces.Add(tags);

            //Generate Fields
            AddFields(typeDeclaration, tagBlockDefinition.GetLatestFieldDefinitions().ToList());
        }
        /// <summary>
        /// Creates a code graph file for this tag block.
        /// </summary>
        public void CreateCodeGraph()
        {
            //Prepare
            CodeGeneratorOptions generatorOptions = new CodeGeneratorOptions() { BracingStyle = "C", BlankLinesBetweenMembers = false, VerbatimOrder = false };
            CodeSnippetCompileUnit pragmaDisable = new CodeSnippetCompileUnit("#pragma warning disable CS1591");
            CodeSnippetCompileUnit pragmaRestore = new CodeSnippetCompileUnit("#pragma warning restore CS1591");
            CodeDomProvider provider = new CSharpCodeProvider();
            
            //Create File
            using (FileStream fs = new FileStream(Path.Combine(outputDirectory, $"{tagBlockName}.Generated.{provider.FileExtension}"), FileMode.Create))
            using (StreamWriter writer = new StreamWriter(fs))
            {
                provider.GenerateCodeFromCompileUnit(pragmaDisable, writer, generatorOptions);
                provider.GenerateCodeFromCompileUnit(unit, writer, generatorOptions);
                provider.GenerateCodeFromCompileUnit(pragmaRestore, writer, generatorOptions);
            }
        }

        private void RecursiveSearch(IEnumerable<TagFieldDefinition> fields)
        {
            //Create Array elements
            foreach (TagFieldDefinitionArray arrayField in fields.Where(f => f.Type == FieldType.FieldArrayStart))
            {
                //Create unique element name
                string elementName = CreateUniqueName(NameFilter(arrayField.Name) + " element");

                //Add member
                memberNameList.Add(elementName);
                arrayDictionary.Add(arrayField, elementName);

                //Search
                RecursiveSearch(arrayField.GetFields());

                //Make element
                CodeTypeDeclaration element = new CodeTypeDeclaration(elementName);
                element.IsClass = true;

                //Add elements
                AddFields(element, arrayField.GetFields());

                //Add
                typeDeclaration.Members.Add(element);
            }

            //Create Option enumerations
            foreach (TagFieldEnumDefinition enumField in fields.Where(f => f.Type == FieldType.FieldEnum || f.Type == FieldType.FieldCharEnum || f.Type == FieldType.FieldLongEnum ||
            f.Type == FieldType.FieldByteFlags || f.Type == FieldType.FieldWordFlags || f.Type == FieldType.FieldLongFlags))
            {
                //Create unique enum name
                string enumName = CreateUniqueName(enumField.Name + " options");

                //Add
                memberNameList.Add(enumName);
                optionsDictionary.Add(enumField, enumName);

                //Make enum
                CodeTypeDeclaration enumDeclaration = new CodeTypeDeclaration(enumName);
                enumDeclaration.IsEnum = true;

                //Populate options
                List<string> optionNameList = new List<string>(); int index = 0; int value = 0;
                foreach (string option in enumField.Options)
                {
                    //Create unique field name
                    string optionName = NameFilter(option); if (string.IsNullOrWhiteSpace(optionName)) optionName = "EMPTY_STRING";
                    string formattedOptionName = GetPropertyName(optionName); int optionCloneIndex = 1;
                    while (optionNameList.Contains(formattedOptionName))
                    {
                        formattedOptionName = $"{GetPropertyName(optionName)}{optionCloneIndex}";
                        optionCloneIndex++;
                    }

                    //Add
                    optionNameList.Add(formattedOptionName);

                    //Create member
                    CodeMemberField memberField = new CodeMemberField(enumName, formattedOptionName);
                    if (enumField.Type == FieldType.FieldByteFlags || enumField.Type == FieldType.FieldWordFlags || enumField.Type == FieldType.FieldLongFlags) value = 1 << index++;
                    else value = index++;
                    memberField.InitExpression = new CodePrimitiveExpression(value);
                    enumDeclaration.Members.Add(memberField);
                }

                //Add
                typeDeclaration.Members.Add(enumDeclaration);
            }
        }

        private void AddFields(CodeTypeDeclaration declaration, IList<TagFieldDefinition> fields)
        {
            //Prepare
            string fieldType = null;
            CodeMemberField memberField = null;
            CodeAttributeDeclaration fieldAttribute = null;
            TagFieldEnumDefinition enumDefinition = null;
            TagFieldBlockIndexCustomSearchDefinition blockIndexCustomSearchDefinition = null;
            TagFieldExplanationDefinition explanationDefinition = null;
            TagFieldStructDefinition tagStructDefinition = null;
            TagFieldDataDefinition dataDefinition = null;
            TagBlockDefinition blockDefinition = null;
            TagFieldDefinitionArray arrayDefinition = null;

            //Loop
            foreach (TagFieldDefinition field in fields)
            {
                //Get extended definitions
                blockDefinition = guerilla.FindTagBlock(field.DefinitionAddress);
                blockIndexCustomSearchDefinition = field as TagFieldBlockIndexCustomSearchDefinition;
                explanationDefinition = field as TagFieldExplanationDefinition;
                tagStructDefinition = field as TagFieldStructDefinition;
                dataDefinition = field as TagFieldDataDefinition;
                enumDefinition = field as TagFieldEnumDefinition;
                arrayDefinition = field as TagFieldDefinitionArray;

                //Create unique field name
                string fieldName = CreateUniqueName(field.Name);

                //Check
                if (string.IsNullOrWhiteSpace(fieldName)) throw new NotImplementedException();

                //Add
                memberNameList.Add(fieldName);
                fieldType = GetFieldType(field, fields.IndexOf(field));

                //Create Block
                if (field.Type == FieldType.FieldBlock)
                    new TagBlockCodeGraph(guerilla, blockDefinition, outputDirectory).CreateCodeGraph();

                //Create Struct
                if (field.Type == FieldType.FieldStruct)
                    new TagBlockCodeGraph(guerilla, guerilla.FindTagBlock(tagStructDefinition.BlockDefinitionAddresss), outputDirectory).CreateCodeGraph();

                //Check
                if (!string.IsNullOrEmpty(fieldType))
                {
                    //Create member
                    memberField = new CodeMemberField(fieldType, fieldName);
                    memberField.Attributes = MemberAttributes.Public;

                    //Create Field attribute
                    fieldAttribute = new CodeAttributeDeclaration(new CodeTypeReference(typeof(Tags.FieldAttribute)));
                    fieldAttribute.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(field.Name)));
                    fieldAttribute.Arguments.Add(new CodeAttributeArgument(new CodeTypeReferenceExpression($"typeof({fieldType})")));
                    memberField.CustomAttributes.Add(fieldAttribute);

                    //Create Array attribute?
                    if (field.Type == FieldType.FieldArrayStart)
                    {
                        CodeAttributeDeclaration arrayAttribute = new CodeAttributeDeclaration(new CodeTypeReference(typeof(Tags.ArrayAttribute)));
                        arrayAttribute.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(arrayDefinition.ElementCount)));
                        arrayAttribute.Arguments.Add(new CodeAttributeArgument(new CodeTypeReferenceExpression($"typeof({arrayDictionary[arrayDefinition]})")));
                        memberField.CustomAttributes.Add(arrayAttribute);
                    }

                    //Create Block attribute?
                    if (field.Type == FieldType.FieldBlock)
                    {
                        CodeAttributeDeclaration blockAttribute = new CodeAttributeDeclaration(new CodeTypeReference(typeof(Tags.BlockAttribute)));
                        blockAttribute.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(blockDefinition.DisplayName)));
                        blockAttribute.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(blockDefinition.MaximumElementCount)));
                        blockAttribute.Arguments.Add(new CodeAttributeArgument(new CodeTypeReferenceExpression($"typeof({blockDefinition.Name})")));
                        memberField.CustomAttributes.Add(blockAttribute);
                    }

                    //Create Padding attribute?
                    if (field.Type == FieldType.FieldPad || field.Type == FieldType.FieldSkip)
                    {
                        CodeAttributeDeclaration paddingAttribute = new CodeAttributeDeclaration(new CodeTypeReference(typeof(Tags.PaddingAttribute)));
                        paddingAttribute.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(field.DefinitionAddress)));
                        memberField.CustomAttributes.Add(paddingAttribute);
                    }

                    //Create Options attribute?
                    if(enumDefinition != null)
                    {
                        CodeAttributeDeclaration optionsAttribute = new CodeAttributeDeclaration(new CodeTypeReference(typeof(Tags.OptionsAttribute)));
                        optionsAttribute.Arguments.Add(new CodeAttributeArgument(new CodeTypeReferenceExpression($"typeof({optionsDictionary[field]})")));
                        optionsAttribute.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(field.Type == FieldType.FieldByteFlags || field.Type == FieldType.FieldWordFlags || field.Type == FieldType.FieldLongFlags)));
                        memberField.CustomAttributes.Add(optionsAttribute);
                    }

                    //Add
                    declaration.Members.Add(memberField);
                }
            }
        }

        private string GetFieldType(TagFieldDefinition fieldDefinition, int index)
        {
            switch (fieldDefinition.Type)
            {
                case FieldType.FieldString: return typeof(Types.String32).Name;
                case FieldType.FieldLongString: return typeof(Types.String256).Name;
                case FieldType.FieldStringId: return typeof(HaloLibrary.StringId).Name;
                case FieldType.FieldOldStringId: return typeof(HaloLibrary.StringId).Name;
                case FieldType.FieldCharInteger: return typeof(byte).Name;
                case FieldType.FieldShortInteger: return typeof(short).Name;
                case FieldType.FieldLongInteger: return typeof(int).Name;
                case FieldType.FieldAngle: return typeof(float).Name;
                case FieldType.FieldTag: return typeof(HaloLibrary.Tag).Name;
                case FieldType.FieldCharEnum: return typeof(byte).Name;
                case FieldType.FieldEnum: return typeof(short).Name;
                case FieldType.FieldLongEnum: return typeof(int).Name;
                case FieldType.FieldLongFlags: return typeof(int).Name;
                case FieldType.FieldWordFlags: return typeof(short).Name;
                case FieldType.FieldByteFlags: return typeof(byte).Name;
                case FieldType.FieldPoint2D: return typeof(Types.Vector2).Name;
                case FieldType.FieldRectangle2D: return typeof(Types.Vector2).Name;
                case FieldType.FieldRgbColor: return typeof(Types.ColorRgb).Name;
                case FieldType.FieldArgbColor: return typeof(Types.ColorArgb).Name;
                case FieldType.FieldReal: return typeof(float).Name;
                case FieldType.FieldRealFraction: return typeof(float).Name;
                case FieldType.FieldRealPoint2D: return typeof(Types.Vector2).Name;
                case FieldType.FieldRealPoint3D: return typeof(Types.Vector3).Name;
                case FieldType.FieldRealVector2D: return typeof(Types.Vector2).Name;
                case FieldType.FieldRealVector3D: return typeof(Types.Vector3).Name;
                case FieldType.FieldQuaternion: return typeof(Types.Quaternion).Name;
                case FieldType.FieldEulerAngles2D: return typeof(Types.Vector2).Name;
                case FieldType.FieldEulerAngles3D: return typeof(Types.Vector3).Name;
                case FieldType.FieldRealPlane2D: return typeof(Types.Vector3).Name;
                case FieldType.FieldRealPlane3D: return typeof(Types.Vector4).Name;
                case FieldType.FieldRealRgbColor: return typeof(Types.ColorRgbF).Name;
                case FieldType.FieldRealArgbColor: return typeof(Types.ColorArgbF).Name;
                case FieldType.FieldRealHsvColor: return typeof(Types.ColorHsvF).Name;
                case FieldType.FieldRealAhsvColor: return typeof(Types.ColorAhsvF).Name;
                case FieldType.FieldTagReference: return typeof(Types.TagReference).Name;
                case FieldType.FieldBlock: return typeof(HaloLibrary.TagBlock).Name;
                case FieldType.FieldLongBlockFlags: return typeof(int).Name;
                case FieldType.FieldWordBlockFlags: return typeof(short).Name;
                case FieldType.FieldByteBlockFlags: return typeof(byte).Name;
                case FieldType.FieldCharBlockIndex1:
                case FieldType.FieldCharBlockIndex2: return typeof(byte).Name;
                case FieldType.FieldShortBlockIndex1:
                case FieldType.FieldShortBlockIndex2: return typeof(short).Name;
                case FieldType.FieldLongBlockIndex1:
                case FieldType.FieldLongBlockIndex2: return typeof(int).Name;
                case FieldType.FieldStruct: return GetStructTypeName((TagFieldStructDefinition)fieldDefinition);
                case FieldType.FieldData:
                case FieldType.FieldVertexBuffer:return null;
                case FieldType.FieldArrayStart: return $"{arrayDictionary[(TagFieldDefinitionArray)fieldDefinition]}[]";
                case FieldType.FieldPad:
                case FieldType.FieldSkip: return typeof(byte[]).Name;
                case FieldType.FieldArrayEnd:
                case FieldType.FieldUselessPad:
                case FieldType.FieldTerminator:
                case FieldType.FieldCustom:
                default: return null;
            }
        }
        
        private int FindArrayEndIndex(IList<TagFieldDefinition> fields, int startIndex)
        {
            var endIndex = startIndex;
            var depth = 0;
            for (int i = startIndex + 1; i < fields.Count; i++)
            {
                if (fields[i].Type == FieldType.FieldArrayStart) depth++;
                if (fields[i].Type != FieldType.FieldArrayEnd) continue;
                if(depth == 0)
                {
                    endIndex = i + 1;
                    break;
                }
                depth--;
            }
            return endIndex;
        }

        private string GetStructTypeName(TagFieldStructDefinition fieldDefinition)
        {
            //Get Block
            TagBlockDefinition tagBlockDefinition = guerilla.FindTagBlock(fieldDefinition.BlockDefinitionAddresss);

            //Check
            if (tagBlockDefinition != null) return tagBlockDefinition.Name;
            else return string.Empty;
        }

        private string CreateUniqueName(string name)
        {
            //Create unique field name
            string uniqueName = NameFilter(name); if (string.IsNullOrWhiteSpace(uniqueName)) uniqueName = "EMPTY_STRING";
            string formattedUniqueName = GetPropertyName(uniqueName); int cloneIndex = 1;
            while (memberNameList.Contains(formattedUniqueName))
            {
                formattedUniqueName = $"{GetPropertyName(uniqueName)}{cloneIndex}";
                cloneIndex++;
            }

            //Return
            return formattedUniqueName;
        }

        private static string NameFilter(string rawName)
        {
            string name = rawName;
            if (name.Contains("^")) name = name.Substring(0, name.IndexOf('^'));
            if (name.Contains("#")) name = name.Substring(0, name.IndexOf('#'));
            if (name.Contains(":")) name = name.Substring(0, name.IndexOf(':'));
            if (name.Contains("*")) name = name.Substring(0, name.IndexOf('*'));
            name = name.Replace("&", " and ");          //&
            name = name.Replace(">", " greater than "); // >
            name = name.Replace("<", " less than ");    // <
            name = name.Replace("=", " equals ");       // =
            name = name.Replace("\"", string.Empty);    // "
            name = name.Replace("\'", string.Empty);    // '
            name = name.Replace(".", string.Empty);     // .
            name = name.Replace(",", string.Empty);     // ,            
            name = name.Replace("+", string.Empty);     // +
            name = name.Replace("*", string.Empty);     // *
            name = name.Replace("/", string.Empty);     // /
            name = name.Replace("\\", string.Empty);    // \
            name = name.Replace("|", string.Empty);     // |
            name = name.Replace("`", string.Empty);     // `
            name = name.Replace("~", string.Empty);     // ~
            name = name.Replace("(", string.Empty);     // (
            name = name.Replace(")", string.Empty);     // )
            name = name.Replace("[", string.Empty);     // [
            name = name.Replace("]", string.Empty);     // ]
            name = name.Replace("{", string.Empty);     // {
            name = name.Replace("}", string.Empty);     // }
            name = name.Replace("?", string.Empty);     // ?
            name = name.Replace("!", string.Empty);     // !
            return name;
        }

        private static string GetFieldName(string rawName)
        {
            StringBuilder builder = new StringBuilder(CamelCase(NameFilter(rawName)));
            if (char.IsNumber(builder[0])) builder.Insert(0, '_');
            return builder.ToString();
        }

        private static string GetPropertyName(string rawName)
        {
            StringBuilder builder = new StringBuilder(PascalCase(NameFilter(rawName)));
            if (char.IsNumber(builder[0])) builder.Insert(0, '_');
            return builder.ToString();
        }

        private static string GetStructureName(string rawName)
        {
            StringBuilder builder = new StringBuilder(PascalCase(NameFilter(rawName)));
            if (char.IsNumber(builder[0])) builder.Insert(0, '_');
            return builder.ToString();
        }

        private static string PascalCase(string name)
        {
            //Check
            if (string.IsNullOrEmpty(name))
                return string.Empty;

            //Prepare
            StringBuilder builder = new StringBuilder();
            string[] parts = name.Split(NameSplitCharArray, StringSplitOptions.RemoveEmptyEntries);

            foreach (string part in parts)
            {
                StringBuilder partBuilder = new StringBuilder(part.ToLower());
                partBuilder[0] = part.ToUpper()[0];
                builder.Append(partBuilder.ToString());
            }

            //Return
            return builder.ToString();
        }

        private static string CamelCase(string name)
        {
            //Check
            if (string.IsNullOrEmpty(name))
                return string.Empty;

            //Prepare
            StringBuilder builder = new StringBuilder();
            string[] parts = name.Split(NameSplitCharArray, StringSplitOptions.RemoveEmptyEntries);

            builder.Append(parts[0].ToLower());
            for (int i = 1; i < parts.Length; i++)
            {
                StringBuilder partBuilder = new StringBuilder(parts[i].ToLower());
                partBuilder[0] = parts[i].ToUpper()[0];
                builder.Append(partBuilder.ToString());
            }

            //Return
            return builder.ToString();
        }

        private static readonly char[] NameSplitCharArray = new char[] { ' ', '_', '-', ',', '.' };
    }
}
