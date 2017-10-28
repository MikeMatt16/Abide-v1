using Abide.Guerilla.Managed;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;

namespace Abide.Guerilla.CodeDom
{
    /// <summary>
    /// Represents a type declaration for a class or structure that contains field members.
    /// </summary>
    internal class CodeFieldsContainerTypeDeclaration : CodeTypeDeclaration
    {
        private readonly GuerillaInstance guerilla;
        private readonly List<string> membersList = new List<string>();
        private readonly Dictionary<TagBlockDefinition, string> blockNameDictionary = new Dictionary<TagBlockDefinition, string>();
        private readonly Dictionary<TagFieldDefinition, string> optionNameDictionary = new Dictionary<TagFieldDefinition, string>();
        private readonly Dictionary<TagFieldDefinition, string> arrayNameDictionary = new Dictionary<TagFieldDefinition, string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeFieldsContainerTypeDeclaration"/> class using the supplied type name and fields.
        /// </summary>
        /// <param name="guerilla">Me want banana.</param>
        /// <param name="name">The type name.</param>
        /// <param name="tagFields">The fields within the type.</param>
        public CodeFieldsContainerTypeDeclaration(GuerillaInstance guerilla, string name, IEnumerable<TagFieldDefinition> tagFields) : base(name)
        {
            //Add
            membersList.Add(name);
            membersList.Add("Size");
            membersList.Add("Initialize");
            membersList.Add("Write");
            membersList.Add("Read");

            //Prepare
            this.guerilla = guerilla;
            List<TagFieldDefinition> fields = new List<TagFieldDefinition>(tagFields);

            //Create blocks
            foreach (TagFieldDefinition fieldDefinition in fields.Where(f => f.Type == FieldType.FieldBlock))
            {
                //Get tag block
                TagBlockDefinition blockDefinition = guerilla.FindTagBlock(fieldDefinition.DefinitionAddress);
                if (blockNameDictionary.ContainsKey(blockDefinition)) continue;

                //Create struct
                string structName = blockDefinition.Name.ToPascalCasedMember(membersList);
                membersList.Add(structName);
                blockNameDictionary.Add(blockDefinition, structName);

                //Create type
                CodeFieldsContainerTypeDeclaration blockCodeTypeDefinition = new CodeFieldsContainerTypeDeclaration(guerilla, structName, blockDefinition.GetLatestFieldDefinitions());
                blockCodeTypeDefinition.TypeAttributes = System.Reflection.TypeAttributes.Public | System.Reflection.TypeAttributes.Sealed;
                blockCodeTypeDefinition.BaseTypes.Add(new CodeTypeReference(typeof(Tags.IReadable)));
                blockCodeTypeDefinition.BaseTypes.Add(new CodeTypeReference(typeof(Tags.IWritable)));
                blockCodeTypeDefinition.IsClass = true;

                //Create field set attribute
                CodeAttributeDeclaration fieldSetCodeAttributeDeclaration = new CodeAttributeDeclaration(new CodeTypeReference(typeof(Tags.FieldSetAttribute)));
                fieldSetCodeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(blockDefinition.GetLatestFieldSet().Size)));
                fieldSetCodeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(blockDefinition.GetLatestFieldSet().Alignment)));

                //Add field set attribute
                blockCodeTypeDefinition.CustomAttributes.Add(fieldSetCodeAttributeDeclaration);

                //Create Size property
                CodeMemberProperty sizeCodeMemberProperty = new CodeMemberProperty
                {
                    Attributes = MemberAttributes.Public | MemberAttributes.Final,
                    Name = "Size",
                    Type = new CodeTypeReference(typeof(int))
                };
                sizeCodeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(blockDefinition.GetLatestFieldSet().Size)));

                //Create Initialize method
                CodeMemberMethod initializeCodeMethod = new CodeMemberMethod
                {
                    Attributes = MemberAttributes.Public | MemberAttributes.Final,
                    Name = "Initialize",
                    ReturnType = new CodeTypeReference(typeof(void))
                };
                GenerateInitializeMethod(initializeCodeMethod, fields);

                //Create Read method
                CodeMemberMethod readCodeMemberMethod = new CodeMemberMethod
                {
                    Attributes = MemberAttributes.Public | MemberAttributes.Final,
                    Name = "Read",
                    ReturnType = new CodeTypeReference(typeof(void))
                };
                readCodeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(System.IO.BinaryReader)), "reader"));
                GenerateReadMethod(readCodeMemberMethod, fields);

                //Create Write method
                CodeMemberMethod writeCodeMemberMethod = new CodeMemberMethod
                {
                    Attributes = MemberAttributes.Public | MemberAttributes.Final,
                    Name = "Write",
                    ReturnType = new CodeTypeReference(typeof(void))
                };
                writeCodeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(System.IO.BinaryWriter)), "writer"));
                GenerateWriteMethod(writeCodeMemberMethod, fields);

                //Add implemented members
                blockCodeTypeDefinition.Members.Add(sizeCodeMemberProperty);
                blockCodeTypeDefinition.Members.Add(initializeCodeMethod);
                blockCodeTypeDefinition.Members.Add(readCodeMemberMethod);
                blockCodeTypeDefinition.Members.Add(writeCodeMemberMethod);

                //Add
                Members.Add(blockCodeTypeDefinition);
            }

            //Create structs
            foreach (TagFieldStructDefinition fieldDefinition in fields.Where(f => f.Type == FieldType.FieldStruct))
            {
                //Get struct
                TagBlockDefinition structDefinition = guerilla.FindTagBlock(fieldDefinition.BlockDefinitionAddresss);
                if (blockNameDictionary.ContainsKey(structDefinition)) continue;

                //Create struct
                string structName = structDefinition.Name.ToPascalCasedMember(membersList);
                membersList.Add(structName);
                blockNameDictionary.Add(structDefinition, structName);

                //Create type
                CodeFieldsContainerTypeDeclaration structCodeTypeDeclaration = new CodeFieldsContainerTypeDeclaration(guerilla, structName, structDefinition.GetLatestFieldDefinitions());
                structCodeTypeDeclaration.TypeAttributes = System.Reflection.TypeAttributes.Public | System.Reflection.TypeAttributes.Sealed;
                structCodeTypeDeclaration.BaseTypes.Add(new CodeTypeReference(typeof(Tags.IReadable)));
                structCodeTypeDeclaration.BaseTypes.Add(new CodeTypeReference(typeof(Tags.IWritable)));
                structCodeTypeDeclaration.IsClass = true;

                //Create field set attribute
                CodeAttributeDeclaration fieldSetCodeAttributeDeclaration = new CodeAttributeDeclaration(new CodeTypeReference(typeof(Tags.FieldSetAttribute)));
                fieldSetCodeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(structDefinition.GetLatestFieldSet().Size)));
                fieldSetCodeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(structDefinition.GetLatestFieldSet().Alignment)));

                //Add field set attribute
                structCodeTypeDeclaration.CustomAttributes.Add(fieldSetCodeAttributeDeclaration);

                //Create Size property
                CodeMemberProperty sizeCodeMemberProperty = new CodeMemberProperty
                {
                    Attributes = MemberAttributes.Public | MemberAttributes.Final,
                    Name = "Size",
                    Type = new CodeTypeReference(typeof(int))
                };
                sizeCodeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(structDefinition.GetLatestFieldSet().Size)));

                //Create Initialize method
                CodeMemberMethod initializeCodeMethod = new CodeMemberMethod
                {
                    Attributes = MemberAttributes.Public | MemberAttributes.Final,
                    Name = "Initialize",
                    ReturnType = new CodeTypeReference(typeof(void))
                };

                //Create Read method
                CodeMemberMethod readCodeMemberMethod = new CodeMemberMethod
                {
                    Attributes = MemberAttributes.Public | MemberAttributes.Final,
                    Name = "Read",
                    ReturnType = new CodeTypeReference(typeof(void))
                };
                readCodeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(System.IO.BinaryReader)), "reader"));

                //Create Write method
                CodeMemberMethod writeCodeMemberMethod = new CodeMemberMethod
                {
                    Attributes = MemberAttributes.Public | MemberAttributes.Final,
                    Name = "Write",
                    ReturnType = new CodeTypeReference(typeof(void))
                };
                writeCodeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(System.IO.BinaryWriter)), "writer"));

                //Add implemented members
                structCodeTypeDeclaration.Members.Add(sizeCodeMemberProperty);
                structCodeTypeDeclaration.Members.Add(initializeCodeMethod);
                structCodeTypeDeclaration.Members.Add(readCodeMemberMethod);
                structCodeTypeDeclaration.Members.Add(writeCodeMemberMethod);

                //Add
                Members.Add(structCodeTypeDeclaration);
            }

            //Create elements for each array
            foreach (TagFieldDefinitionArray arrayDefinition in fields.Where(f => f.Type == FieldType.FieldArrayStart))
            {
                //Get element name
                string elementName = (arrayDefinition.Name.FilterName() + " element").ToPascalCasedMember(membersList);

                //Add
                membersList.Add(elementName);
                arrayNameDictionary.Add(arrayDefinition, elementName);

                //Create a code type
                CodeFieldsContainerTypeDeclaration elementCodeTypeDeclaration = new CodeFieldsContainerTypeDeclaration(guerilla, elementName, arrayDefinition.GetFields());
                elementCodeTypeDeclaration.TypeAttributes = System.Reflection.TypeAttributes.Public | System.Reflection.TypeAttributes.Sealed;
                elementCodeTypeDeclaration.BaseTypes.Add(new CodeTypeReference(typeof(Tags.IReadable)));
                elementCodeTypeDeclaration.BaseTypes.Add(new CodeTypeReference(typeof(Tags.IWritable)));
                elementCodeTypeDeclaration.IsClass = true;

                //Create Size property
                CodeMemberProperty sizeCodeMemberProperty = new CodeMemberProperty
                {
                    Attributes = MemberAttributes.Public | MemberAttributes.Final,
                    Name = "Size",
                    Type = new CodeTypeReference(typeof(int))
                };
                sizeCodeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(arrayDefinition.Size)));

                //Create Initialize method
                CodeMemberMethod initializeCodeMethod = new CodeMemberMethod
                {
                    Attributes = MemberAttributes.Public | MemberAttributes.Final,
                    Name = "Initialize",
                    ReturnType = new CodeTypeReference(typeof(void))
                };

                //Create Read method
                CodeMemberMethod readCodeMemberMethod = new CodeMemberMethod
                {
                    Attributes = MemberAttributes.Public | MemberAttributes.Final,
                    Name = "Read",
                    ReturnType = new CodeTypeReference(typeof(void))
                };
                readCodeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(System.IO.BinaryReader)), "reader"));

                //Create Write method
                CodeMemberMethod writeCodeMemberMethod = new CodeMemberMethod
                {
                    Attributes = MemberAttributes.Public | MemberAttributes.Final,
                    Name = "Write",
                    ReturnType = new CodeTypeReference(typeof(void))
                };
                writeCodeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(System.IO.BinaryWriter)), "writer"));
                
                //Add implemented members
                elementCodeTypeDeclaration.Members.Add(sizeCodeMemberProperty);
                elementCodeTypeDeclaration.Members.Add(initializeCodeMethod);
                elementCodeTypeDeclaration.Members.Add(readCodeMemberMethod);
                elementCodeTypeDeclaration.Members.Add(writeCodeMemberMethod);

                //Add element
                Members.Add(elementCodeTypeDeclaration);
            }

            //Create Enumerations
            foreach (TagFieldEnumDefinition enumDefinition in fields.Where(f => f.Type == FieldType.FieldEnum || f.Type == FieldType.FieldCharEnum ||
            f.Type == FieldType.FieldLongEnum || f.Type == FieldType.FieldByteFlags || f.Type == FieldType.FieldWordFlags || f.Type == FieldType.FieldLongFlags))
            {
                //Get enum name
                string optionsName = (enumDefinition.Name.FilterName() + " options").ToPascalCasedMember(membersList);

                //Add
                membersList.Add(optionsName);
                optionNameDictionary.Add(enumDefinition, optionsName);

                //Create a code type
                CodeTypeDeclaration optionsCodeTypeDeclaration = new CodeTypeDeclaration(optionsName);
                optionsCodeTypeDeclaration.IsEnum = true;

                //Prepare options
                List<string> optionsNameList = new List<string>(); int index = 0, value = 0;
                foreach (string option in enumDefinition.Options)
                {
                    //Get Value
                    if (enumDefinition.Type == FieldType.FieldByteFlags || enumDefinition.Type == FieldType.FieldWordFlags || enumDefinition.Type == FieldType.FieldLongFlags) value = 1 << index++;
                    else value = index++;

                    //Get option name
                    string optionName = option.ToPascalCasedMember(optionsNameList);

                    //Add
                    optionsNameList.Add(optionName);

                    //Create member
                    CodeMemberField optionMemberField = new CodeMemberField(optionsName, optionName);
                    optionMemberField.InitExpression = new CodePrimitiveExpression(value);
                    optionsCodeTypeDeclaration.Members.Add(optionMemberField);
                }

                //Add enumeration
                Members.Add(optionsCodeTypeDeclaration);
            }

            //Add field members
            AddFields(fields);
        }

        private void GenerateInitializeMethod(CodeMemberMethod method, List<TagFieldDefinition> fields)
        {

        }

        private void GenerateReadMethod(CodeMemberMethod method, List<TagFieldDefinition> fields)
        {

        }

        private void GenerateWriteMethod(CodeMemberMethod method, List<TagFieldDefinition> fields)
        {

        }
        
        private void AddFields(List<TagFieldDefinition> fields)
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
            TagFieldDefinitionArray arrayDefinition = null;
            TagBlockDefinition blockDefinition = null;

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
                string fieldName = field.Name.ToPascalCasedMember(membersList);

                //Check
                if (string.IsNullOrWhiteSpace(fieldName)) throw new NotImplementedException();

                //Add
                membersList.Add(fieldName);
                fieldType = GetFieldType(field, fields.IndexOf(field));
                
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
                        arrayAttribute.Arguments.Add(new CodeAttributeArgument(new CodeTypeReferenceExpression($"typeof({arrayNameDictionary[arrayDefinition]})")));
                        memberField.CustomAttributes.Add(arrayAttribute);
                    }

                    //Create Block attribute?
                    if (field.Type == FieldType.FieldBlock)
                    {
                        CodeAttributeDeclaration blockAttribute = new CodeAttributeDeclaration(new CodeTypeReference(typeof(Tags.BlockAttribute)));
                        blockAttribute.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(blockDefinition.DisplayName)));
                        blockAttribute.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(blockDefinition.MaximumElementCount)));
                        blockAttribute.Arguments.Add(new CodeAttributeArgument(new CodeTypeReferenceExpression($"typeof({blockNameDictionary[blockDefinition]})")));
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
                    if (enumDefinition != null)
                    {
                        CodeAttributeDeclaration optionsAttribute = new CodeAttributeDeclaration(new CodeTypeReference(typeof(Tags.OptionsAttribute)));
                        optionsAttribute.Arguments.Add(new CodeAttributeArgument(new CodeTypeReferenceExpression($"typeof({optionNameDictionary[field]})")));
                        optionsAttribute.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(field.Type == FieldType.FieldByteFlags || field.Type == FieldType.FieldWordFlags || field.Type == FieldType.FieldLongFlags)));
                        memberField.CustomAttributes.Add(optionsAttribute);
                    }

                    //Add
                    Members.Add(memberField);
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
                case FieldType.FieldStruct: return blockNameDictionary[guerilla.FindTagBlock(((TagFieldStructDefinition)fieldDefinition).BlockDefinitionAddresss)];
                case FieldType.FieldData:
                case FieldType.FieldVertexBuffer: return null;
                case FieldType.FieldArrayStart: return $"{arrayNameDictionary[(TagFieldDefinitionArray)fieldDefinition]}[]";
                case FieldType.FieldPad:
                case FieldType.FieldSkip: return typeof(byte[]).Name;
                case FieldType.FieldArrayEnd:
                case FieldType.FieldUselessPad:
                case FieldType.FieldTerminator:
                case FieldType.FieldCustom:
                default: return null;
            }
        }
    }
}
