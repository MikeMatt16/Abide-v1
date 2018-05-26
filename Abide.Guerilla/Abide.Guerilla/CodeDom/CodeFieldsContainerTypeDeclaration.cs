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
        private readonly Dictionary<TagFieldDefinition, string> fieldNames = new Dictionary<TagFieldDefinition, string>();
        private readonly Dictionary<TagBlockDefinition, string> blockNameDictionary = new Dictionary<TagBlockDefinition, string>();
        private readonly Dictionary<TagFieldDefinition, string> optionNameDictionary = new Dictionary<TagFieldDefinition, string>();
        private readonly Dictionary<TagFieldDefinition, string> arrayNameDictionary = new Dictionary<TagFieldDefinition, string>();
        private readonly Dictionary<TagFieldDefinition, string> blockListNameDictionary = new Dictionary<TagFieldDefinition, string>();

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

            //Create datas
            foreach (TagFieldDataDefinition dataDefinition in fields.Where(f => f.Type == FieldType.FieldData))
            {
                //Create private list member
                string blockListFieldName = (dataDefinition.Name.FilterName() + " list").ToCamelCasedMember(membersList);
                blockListNameDictionary.Add(dataDefinition, blockListFieldName);
                membersList.Add(blockListFieldName);

                //Create public list member
                string blockListPropertyName = (dataDefinition.Name.FilterName() + " list").ToPascalCasedMember(membersList);
                membersList.Add(blockListPropertyName);

                //Create private list field
                CodeMemberField listField = new CodeMemberField(new CodeTypeReference(typeof(Tags.DataList).Name), blockListFieldName)
                {
                    Attributes = MemberAttributes.Private,
                    InitExpression = new CodeObjectCreateExpression(new CodeTypeReference(typeof(Tags.DataList).Name), new CodePrimitiveExpression(dataDefinition.MaximumSize))
                };
                Members.Add(listField);

                //Create public list property
                CodeMemberProperty listProperty = new CodeMemberProperty()
                {
                    Name = blockListPropertyName,
                    Attributes = MemberAttributes.Public | MemberAttributes.Final,
                    HasGet = true,
                    Type = new CodeTypeReference(typeof(Tags.DataList).Name),
                };
                listProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), blockListFieldName)));
                Members.Add(listProperty);
            }

            //Create blocks
            foreach (TagFieldDefinition fieldDefinition in fields.Where(f => f.Type == FieldType.FieldBlock))
            {
                //Get tag block
                TagBlockDefinition blockDefinition = guerilla.FindTagBlock(fieldDefinition.DefinitionAddress);
                if (blockNameDictionary.ContainsKey(blockDefinition)) continue;

                //Create block
                string blockName = blockDefinition.Name.ToPascalCasedMember(membersList);
                membersList.Add(blockName);
                blockNameDictionary.Add(blockDefinition, blockName);

                //Create private list member
                string blockListFieldName = (fieldDefinition.Name.FilterName() + " list").ToCamelCasedMember(membersList);
                blockListNameDictionary.Add(fieldDefinition, blockListFieldName);
                membersList.Add(blockListFieldName);

                //Create public list member
                string blockListPropertyName = (fieldDefinition.Name.FilterName() + " list").ToPascalCasedMember(membersList);
                membersList.Add(blockListPropertyName);

                //Create type
                CodeFieldsContainerTypeDeclaration blockCodeTypeDefinition = new CodeFieldsContainerTypeDeclaration(guerilla, blockName, blockDefinition.GetLatestFieldDefinitions())
                {
                    IsClass = true,
                    TypeAttributes = System.Reflection.TypeAttributes.Public | System.Reflection.TypeAttributes.Sealed
                };
                blockCodeTypeDefinition.BaseTypes.Add(new CodeTypeReference(typeof(Tags.AbideTagBlock).Name));

                //Create field set attribute
                CodeAttributeDeclaration fieldSetCodeAttributeDeclaration = new CodeAttributeDeclaration(new CodeAttributeTypeReference<Tags.FieldSetAttribute>());
                fieldSetCodeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(blockDefinition.GetLatestFieldSet().Size)));
                fieldSetCodeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(blockDefinition.GetLatestFieldSet().Alignment)));

                //Add field set attribute
                blockCodeTypeDefinition.CustomAttributes.Add(fieldSetCodeAttributeDeclaration);

                //Create Size property
                CodeMemberProperty sizeCodeMemberProperty = new CodeMemberProperty
                {
                    Attributes = MemberAttributes.Public | MemberAttributes.Override,
                    Name = "Size",
                    Type = new CodeTypeReference(typeof(int))
                };
                sizeCodeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(blockDefinition.GetLatestFieldSet().Size)));
                blockCodeTypeDefinition.Members.Add(sizeCodeMemberProperty);

                //Create private list field
                CodeMemberField listField = new CodeMemberField(new CodeTypeReference(typeof(Tags.TagBlockList<>).Name, new CodeTypeReference(blockName)), blockListFieldName)
                {
                    Attributes = MemberAttributes.Private,
                    InitExpression = new CodeObjectCreateExpression(new CodeTypeReference(typeof(Tags.TagBlockList<>).Name, new CodeTypeReference(blockName)), new CodePrimitiveExpression(blockDefinition.MaximumElementCount))
                };
                Members.Add(listField);

                //Create public list property
                CodeMemberProperty listProperty = new CodeMemberProperty()
                {
                    Name = blockListPropertyName,
                    Attributes = MemberAttributes.Public | MemberAttributes.Final,
                    HasGet = true,
                    Type = new CodeTypeReference(typeof(Tags.TagBlockList<>).Name, new CodeTypeReference(blockName)),
                };
                listProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), blockListFieldName)));
                Members.Add(listProperty);

                //Add type
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
                CodeFieldsContainerTypeDeclaration structCodeTypeDeclaration = new CodeFieldsContainerTypeDeclaration(guerilla, structName, structDefinition.GetLatestFieldDefinitions())
                {
                    TypeAttributes = System.Reflection.TypeAttributes.Public | System.Reflection.TypeAttributes.Sealed,
                    IsClass = true
                };
                structCodeTypeDeclaration.BaseTypes.Add(new CodeTypeReference(typeof(Tags.AbideTagBlock).Name));

                //Create field set attribute
                CodeAttributeDeclaration fieldSetCodeAttributeDeclaration = new CodeAttributeDeclaration(new CodeAttributeTypeReference<Tags.FieldSetAttribute>());
                fieldSetCodeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(structDefinition.GetLatestFieldSet().Size)));
                fieldSetCodeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(structDefinition.GetLatestFieldSet().Alignment)));

                //Add field set attribute
                structCodeTypeDeclaration.CustomAttributes.Add(fieldSetCodeAttributeDeclaration);

                //Create Size property
                CodeMemberProperty sizeCodeMemberProperty = new CodeMemberProperty
                {
                    Attributes = MemberAttributes.Public | MemberAttributes.Override,
                    Name = "Size",
                    Type = new CodeTypeReference(typeof(int))
                };
                sizeCodeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(structDefinition.GetLatestFieldSet().Size)));
                structCodeTypeDeclaration.Members.Add(sizeCodeMemberProperty);

                //Add
                Members.Add(structCodeTypeDeclaration);
            }

            //Create elements for each array
            foreach (TagFieldDefinitionArray arrayDefinition in fields.Where(f => f.Type == FieldType.FieldArrayStart))
            {
                //Create element
                string elementName = (arrayDefinition.Name.FilterName() + " element").ToPascalCasedMember(membersList);
                membersList.Add(elementName);
                arrayNameDictionary.Add(arrayDefinition, elementName);

                //Create type
                CodeFieldsContainerTypeDeclaration elementCodeTypeDeclaration = new CodeFieldsContainerTypeDeclaration(guerilla, elementName, arrayDefinition.GetFields())
                {
                    TypeAttributes = System.Reflection.TypeAttributes.Public | System.Reflection.TypeAttributes.Sealed,
                    IsClass = true
                };
                elementCodeTypeDeclaration.BaseTypes.Add(new CodeTypeReference(typeof(Tags.AbideTagBlock).Name));

                //Create Size property
                CodeMemberProperty sizeCodeMemberProperty = new CodeMemberProperty
                {
                    Attributes = MemberAttributes.Public | MemberAttributes.Override,
                    Name = "Size",
                    Type = new CodeTypeReference(typeof(int))
                };
                sizeCodeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(arrayDefinition.Size)));
                elementCodeTypeDeclaration.Members.Add(sizeCodeMemberProperty);

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
                CodeTypeDeclaration optionsCodeTypeDeclaration = new CodeTypeDeclaration(optionsName)
                {
                    IsEnum = true
                };
                optionsCodeTypeDeclaration.BaseTypes.Add(new CodeTypeReference(GetEnumBaseType(enumDefinition)));

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
                    CodeMemberField optionMemberField = new CodeMemberField(optionsName, optionName)
                    {
                        InitExpression = new CodePrimitiveExpression(value)
                    };
                    optionsCodeTypeDeclaration.Members.Add(optionMemberField);
                }

                //Add enumeration
                Members.Add(optionsCodeTypeDeclaration);
            }

            //Add field members
            AddFields(fields);

            //Create Initialize method
            CodeMemberMethod initializeCodeMethod = new CodeMemberMethod
            {
                Attributes = MemberAttributes.Public | MemberAttributes.Override,
                Name = "Initialize",
                ReturnType = new CodeTypeReference(typeof(void))
            };
            GenerateInitializeMethod(initializeCodeMethod, fields.ToList());

            //Create Read method
            CodeMemberMethod readCodeMemberMethod = new CodeMemberMethod
            {
                Attributes = MemberAttributes.Public | MemberAttributes.Override,
                Name = "Read",
                ReturnType = new CodeTypeReference(typeof(void))
            };
            readCodeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(System.IO.BinaryReader).Name), "reader"));
            GenerateReadMethod(readCodeMemberMethod, fields.ToList());

            //Create Write method
            CodeMemberMethod writeCodeMemberMethod = new CodeMemberMethod
            {
                Attributes = MemberAttributes.Public | MemberAttributes.Override,
                Name = "Write",
                ReturnType = new CodeTypeReference(typeof(void))
            };
            writeCodeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(System.IO.BinaryWriter).Name), "writer"));
            GenerateWriteMethod(writeCodeMemberMethod, fields.ToList());

            //Add Methods
            Members.Add(initializeCodeMethod);
            Members.Add(readCodeMemberMethod);
            Members.Add(writeCodeMemberMethod);
        }

        private void GenerateInitializeMethod(CodeMemberMethod method, List<TagFieldDefinition> fields)
        {
            //Prepare
            CodeExpression rightExpression = null;
            CodeAssignStatement codeAssignStatement = null;
            TagFieldEnumDefinition enumDefinition = null;
            TagFieldBlockIndexCustomSearchDefinition blockIndexCustomSearchDefinition = null;
            TagFieldExplanationDefinition explanationDefinition = null;
            TagFieldStructDefinition tagStructDefinition = null;
            TagFieldDataDefinition dataDefinition = null;
            TagFieldDefinitionArray arrayDefinition = null;
            TagBlockDefinition blockDefinition = null;
            string fieldName = null;

            //Clear lists
            foreach (string listName in blockListNameDictionary.Values)
                method.Statements.Add(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), listName), "Clear")));

            //Loop
            foreach (TagFieldDefinition field in fields)
                if (fieldNames.ContainsKey(field))
                {
                    //Get extended definitions
                    blockDefinition = guerilla.FindTagBlock(field.DefinitionAddress);
                    blockIndexCustomSearchDefinition = field as TagFieldBlockIndexCustomSearchDefinition;
                    explanationDefinition = field as TagFieldExplanationDefinition;
                    tagStructDefinition = field as TagFieldStructDefinition;
                    dataDefinition = field as TagFieldDataDefinition;
                    enumDefinition = field as TagFieldEnumDefinition;
                    arrayDefinition = field as TagFieldDefinitionArray;

                    //Get Field name
                    fieldName = fieldNames[field];

                    //Create assignment
                    codeAssignStatement = new CodeAssignStatement()
                    {
                        Left = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName),
                    };

                    //Handle
                    switch (field.Type)
                    {
                        case FieldType.FieldString: rightExpression = new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(typeof(Types.String32).Name), "Empty"); break;
                        case FieldType.FieldLongString: rightExpression = new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(typeof(Types.String256).Name), "Empty"); break;

                        case FieldType.FieldStringId:
                        case FieldType.FieldOldStringId: rightExpression = new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(typeof(HaloLibrary.StringId).Name), "Zero"); break;

                        case FieldType.FieldLongBlockFlags:
                        case FieldType.FieldWordBlockFlags:
                        case FieldType.FieldByteBlockFlags:
                        case FieldType.FieldReal:
                        case FieldType.FieldRealFraction:
                        case FieldType.FieldCharInteger:
                        case FieldType.FieldShortInteger:
                        case FieldType.FieldLongInteger:
                        case FieldType.FieldAngle:
                        case FieldType.FieldCharBlockIndex1:
                        case FieldType.FieldCharBlockIndex2:
                        case FieldType.FieldShortBlockIndex1:
                        case FieldType.FieldShortBlockIndex2:
                        case FieldType.FieldLongBlockIndex1:
                        case FieldType.FieldLongBlockIndex2: rightExpression = new CodePrimitiveExpression(0); break;

                        case FieldType.FieldTag: rightExpression = new CodePrimitiveExpression("null"); break;
                        case FieldType.FieldTagReference: rightExpression = new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(typeof(Types.TagReference).Name), "Null"); break;
                        case FieldType.FieldBlock:
                        case FieldType.FieldData: rightExpression = new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(typeof(HaloLibrary.TagBlock).Name), "Zero"); break;

                        case FieldType.FieldArrayStart: rightExpression = new CodeArrayCreateExpression(new CodeTypeReference(arrayNameDictionary[field]), field.DefinitionAddress); break;
                        case FieldType.FieldStruct: rightExpression = new CodeObjectCreateExpression(blockNameDictionary[guerilla.FindTagBlock(tagStructDefinition.BlockDefinitionAddresss)]); break;

                        case FieldType.FieldCharEnum:
                        case FieldType.FieldEnum:
                        case FieldType.FieldLongEnum:
                        case FieldType.FieldLongFlags:
                        case FieldType.FieldWordFlags:
                        case FieldType.FieldByteFlags: rightExpression = new CodeCastExpression(optionNameDictionary[field], new CodePrimitiveExpression(0)); break;

                        case FieldType.FieldRectangle2D:
                        case FieldType.FieldEulerAngles2D:
                        case FieldType.FieldRealVector2D:
                        case FieldType.FieldRealPoint2D:
                        case FieldType.FieldPoint2D: rightExpression = new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(typeof(Types.Vector2).Name), "Zero"); break;

                        case FieldType.FieldRealPlane2D:
                        case FieldType.FieldEulerAngles3D:
                        case FieldType.FieldRealPoint3D:
                        case FieldType.FieldRealVector3D: rightExpression = new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(typeof(Types.Vector3).Name), "Zero"); break;

                        case FieldType.FieldQuaternion: rightExpression = new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(typeof(Types.Quaternion).Name), "Zero"); break;

                        case FieldType.FieldRealPlane3D: rightExpression = new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(typeof(Types.Vector4).Name), "Zero"); break;

                        case FieldType.FieldRgbColor: rightExpression = new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(typeof(Types.ColorRgb).Name), "Zero"); break;
                        case FieldType.FieldArgbColor: rightExpression = new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(typeof(Types.ColorArgb).Name), "Zero"); break;
                        case FieldType.FieldRealRgbColor: rightExpression = new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(typeof(Types.ColorRgbF).Name), "Zero"); break;
                        case FieldType.FieldRealArgbColor: rightExpression = new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(typeof(Types.ColorArgbF).Name), "Zero"); break;
                        case FieldType.FieldRealHsvColor: rightExpression = new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(typeof(Types.ColorHsvF).Name), "Zero"); break;
                        case FieldType.FieldRealAhsvColor: rightExpression = new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(typeof(Types.ColorAhsvF).Name), "Zero"); break;

                        case FieldType.FieldShortBounds:
                        case FieldType.FieldAngleBounds:
                        case FieldType.FieldRealBounds:
                        case FieldType.FieldRealFractionBounds: rightExpression = new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(typeof(Types.FloatRange).Name), "Zero"); break;

                        case FieldType.FieldVertexBuffer: break;

                        case FieldType.FieldPad:
                        case FieldType.FieldSkip: rightExpression = new CodeArrayCreateExpression(new CodeTypeReference(typeof(byte)), new CodePrimitiveExpression(field.DefinitionAddress)); break;
                    }

                    //Check
                    if (rightExpression == null) continue;
                    codeAssignStatement.Right = rightExpression;

                    //Add
                    method.Statements.Add(codeAssignStatement);
                }
        }

        private void GenerateReadMethod(CodeMemberMethod method, List<TagFieldDefinition> fields)
        {
            //Prepare
            CodeExpression rightExpression = null;
            CodeAssignStatement codeAssignStatement = null;
            TagFieldEnumDefinition enumDefinition = null;
            TagFieldBlockIndexCustomSearchDefinition blockIndexCustomSearchDefinition = null;
            TagFieldExplanationDefinition explanationDefinition = null;
            TagFieldStructDefinition tagStructDefinition = null;
            TagFieldDataDefinition dataDefinition = null;
            TagFieldDefinitionArray arrayDefinition = null;
            TagBlockDefinition blockDefinition = null;
            string fieldName = null, blockListName = null, blockName = null;

            //Loop
            foreach (TagFieldDefinition field in fields)
                if (fieldNames.ContainsKey(field))
                {
                    //Get extended definitions
                    blockDefinition = guerilla.FindTagBlock(field.DefinitionAddress);
                    blockIndexCustomSearchDefinition = field as TagFieldBlockIndexCustomSearchDefinition;
                    explanationDefinition = field as TagFieldExplanationDefinition;
                    tagStructDefinition = field as TagFieldStructDefinition;
                    dataDefinition = field as TagFieldDataDefinition;
                    enumDefinition = field as TagFieldEnumDefinition;
                    arrayDefinition = field as TagFieldDefinitionArray;

                    //Get Struct block
                    if (tagStructDefinition != null) blockDefinition = guerilla.FindTagBlock(tagStructDefinition.BlockDefinitionAddresss);

                    //Get block list name
                    if (blockListNameDictionary.ContainsKey(field)) blockListName = blockListNameDictionary[field];

                    //Get block name
                    if (blockDefinition != null && blockNameDictionary.ContainsKey(blockDefinition)) blockName = blockNameDictionary[blockDefinition];

                    //Get Field name
                    fieldName = fieldNames[field];

                    //Create assignment
                    codeAssignStatement = new CodeAssignStatement()
                    {
                        Left = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName),
                    };

                    //Handle
                    switch (field.Type)
                    {
                        case FieldType.FieldString: rightExpression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeArgumentReferenceExpression("reader"), "Read", new CodeTypeReference(typeof(Types.String32).Name))); break;
                        case FieldType.FieldLongString: rightExpression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeArgumentReferenceExpression("reader"), "Read", new CodeTypeReference(typeof(Types.String256).Name))); break;

                        case FieldType.FieldStringId:
                        case FieldType.FieldOldStringId: rightExpression = new CodeMethodInvokeExpression(new CodeArgumentReferenceExpression("reader"), "ReadInt32"); break;

                        case FieldType.FieldCharInteger:
                        case FieldType.FieldCharBlockIndex1:
                        case FieldType.FieldCharBlockIndex2:
                        case FieldType.FieldByteBlockFlags: rightExpression = new CodeMethodInvokeExpression(new CodeArgumentReferenceExpression("reader"), "ReadByte"); break;

                        case FieldType.FieldShortInteger:
                        case FieldType.FieldShortBlockIndex1:
                        case FieldType.FieldShortBlockIndex2:
                        case FieldType.FieldWordBlockFlags: rightExpression = new CodeMethodInvokeExpression(new CodeArgumentReferenceExpression("reader"), "ReadInt16"); break;

                        case FieldType.FieldLongInteger:
                        case FieldType.FieldLongBlockFlags:
                        case FieldType.FieldLongBlockIndex1:
                        case FieldType.FieldLongBlockIndex2: rightExpression = new CodeMethodInvokeExpression(new CodeArgumentReferenceExpression("reader"), "ReadInt32"); break;

                        case FieldType.FieldReal:
                        case FieldType.FieldRealFraction:
                        case FieldType.FieldAngle: rightExpression = new CodeMethodInvokeExpression(new CodeArgumentReferenceExpression("reader"), "ReadSingle"); break;

                        case FieldType.FieldTag: rightExpression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeArgumentReferenceExpression("reader"), "Read", new CodeTypeReference(typeof(HaloLibrary.Tag).Name))); break;
                        case FieldType.FieldTagReference: rightExpression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeArgumentReferenceExpression("reader"), "Read", new CodeTypeReference(typeof(Types.TagReference).Name))); break;

                        case FieldType.FieldBlock:
                        case FieldType.FieldData: rightExpression = new CodeMethodInvokeExpression(new CodeArgumentReferenceExpression("reader"), "ReadInt64"); break;

                        //case FieldType.FieldArrayStart: rightExpression = new CodeArrayCreateExpression(new CodeTypeReference(arrayNameDictionary[field]), field.DefinitionAddress); break;
                        case FieldType.FieldStruct: rightExpression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeArgumentReferenceExpression("reader"), "ReadDataStructure", new CodeTypeReference(blockName))); break;

                        case FieldType.FieldCharEnum:
                        case FieldType.FieldByteFlags: rightExpression = new CodeCastExpression(optionNameDictionary[field], new CodeMethodInvokeExpression(new CodeArgumentReferenceExpression("reader"), "ReadByte")); break;

                        case FieldType.FieldEnum:
                        case FieldType.FieldWordFlags: rightExpression = new CodeCastExpression(optionNameDictionary[field], new CodeMethodInvokeExpression(new CodeArgumentReferenceExpression("reader"), "ReadInt16")); break;

                        case FieldType.FieldLongEnum:
                        case FieldType.FieldLongFlags: rightExpression = new CodeCastExpression(optionNameDictionary[field], new CodeMethodInvokeExpression(new CodeArgumentReferenceExpression("reader"), "ReadInt32")); break;

                        case FieldType.FieldRectangle2D:
                        case FieldType.FieldEulerAngles2D:
                        case FieldType.FieldRealVector2D:
                        case FieldType.FieldRealPoint2D:
                        case FieldType.FieldPoint2D: rightExpression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeArgumentReferenceExpression("reader"), "Read", new CodeTypeReference(typeof(Types.Vector2).Name))); break;

                        case FieldType.FieldRealPlane2D:
                        case FieldType.FieldEulerAngles3D:
                        case FieldType.FieldRealPoint3D:
                        case FieldType.FieldRealVector3D: rightExpression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeArgumentReferenceExpression("reader"), "Read", new CodeTypeReference(typeof(Types.Vector3).Name))); break;

                        case FieldType.FieldQuaternion: rightExpression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeArgumentReferenceExpression("reader"), "Read", new CodeTypeReference(typeof(Types.Quaternion).Name))); break;

                        case FieldType.FieldRealPlane3D: rightExpression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeArgumentReferenceExpression("reader"), "Read", new CodeTypeReference(typeof(Types.Vector4).Name))); break;

                        case FieldType.FieldRgbColor: rightExpression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeArgumentReferenceExpression("reader"), "Read", new CodeTypeReference(typeof(Types.ColorRgb).Name))); break;
                        case FieldType.FieldArgbColor: rightExpression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeArgumentReferenceExpression("reader"), "Read", new CodeTypeReference(typeof(Types.ColorArgb).Name))); break;
                        case FieldType.FieldRealRgbColor: rightExpression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeArgumentReferenceExpression("reader"), "Read", new CodeTypeReference(typeof(Types.ColorRgbF).Name))); break;
                        case FieldType.FieldRealArgbColor: rightExpression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeArgumentReferenceExpression("reader"), "Read", new CodeTypeReference(typeof(Types.ColorArgbF).Name))); break;
                        case FieldType.FieldRealHsvColor: rightExpression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeArgumentReferenceExpression("reader"), "Read", new CodeTypeReference(typeof(Types.ColorHsvF).Name))); break;
                        case FieldType.FieldRealAhsvColor: rightExpression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeArgumentReferenceExpression("reader"), "Read", new CodeTypeReference(typeof(Types.ColorAhsvF).Name))); break;

                        case FieldType.FieldShortBounds:
                        case FieldType.FieldAngleBounds:
                        case FieldType.FieldRealBounds:
                        case FieldType.FieldRealFractionBounds: rightExpression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeArgumentReferenceExpression("reader"), "Read", new CodeTypeReference(typeof(Types.FloatRange).Name))); break;

                        case FieldType.FieldVertexBuffer: break;

                        case FieldType.FieldPad:
                        case FieldType.FieldSkip: rightExpression = new CodeMethodInvokeExpression(new CodeArgumentReferenceExpression("reader"), "ReadBytes", new CodePrimitiveExpression(field.DefinitionAddress)); break;
                        default: continue;
                    }
                    
                    //Check
                    if (rightExpression == null) continue;
                    codeAssignStatement.Right = rightExpression;
                    
                    //Add
                    method.Statements.Add(codeAssignStatement);

                    //Add tag block read
                    if (field.Type == FieldType.FieldBlock)
                    {
                        CodeMethodInvokeExpression listReadCodeMethodInvokeExpression =
                            new CodeMethodInvokeExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), blockListName), "Read",
                            new CodeArgumentReferenceExpression("reader"), new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName));
                        method.Statements.Add(listReadCodeMethodInvokeExpression);
                    }

                    //Add data read
                    if(field.Type == FieldType.FieldData)
                    {
                        CodeMethodInvokeExpression listReadCodeMethodInvokeExpression =
                            new CodeMethodInvokeExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), blockListNameDictionary[field]), "Read",
                            new CodeArgumentReferenceExpression("reader"), new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName));
                        method.Statements.Add(listReadCodeMethodInvokeExpression);
                    }
                }
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
                fieldType = GetFieldType(field, fields.IndexOf(field));

                //Check
                if (string.IsNullOrWhiteSpace(fieldName)) throw new NotImplementedException();
                if (string.IsNullOrEmpty(fieldType)) continue;

                //Add
                membersList.Add(fieldName);
                fieldNames.Add(field, fieldName);

                //Create member
                memberField = new CodeMemberField(fieldType, fieldName)
                {
                    Attributes = MemberAttributes.Public | MemberAttributes.Final
                };

                //Create Field attribute
                fieldAttribute = new CodeAttributeDeclaration(new CodeAttributeTypeReference<Tags.FieldAttribute>());
                fieldAttribute.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(field.Name)));
                fieldAttribute.Arguments.Add(new CodeAttributeArgument(new CodeTypeReferenceExpression($"typeof({fieldType})")));
                memberField.CustomAttributes.Add(fieldAttribute);

                //Create Array attribute?
                if (field.Type == FieldType.FieldArrayStart)
                {
                    CodeAttributeDeclaration arrayAttribute = new CodeAttributeDeclaration(new CodeAttributeTypeReference<Tags.ArrayAttribute>());
                    arrayAttribute.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(arrayDefinition.ElementCount)));
                    arrayAttribute.Arguments.Add(new CodeAttributeArgument(new CodeTypeReferenceExpression($"typeof({arrayNameDictionary[arrayDefinition]})")));
                    memberField.CustomAttributes.Add(arrayAttribute);
                }

                //Create Data attribute?
                if (field.Type == FieldType.FieldData)
                {
                    CodeAttributeDeclaration dataAttribute = new CodeAttributeDeclaration(new CodeAttributeTypeReference<Tags.DataAttribute>());
                    dataAttribute.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(dataDefinition.MaximumSize)));
                    memberField.CustomAttributes.Add(dataAttribute);
                }

                //Create Block attribute?
                if (field.Type == FieldType.FieldBlock)
                {
                    CodeAttributeDeclaration blockAttribute = new CodeAttributeDeclaration(new CodeAttributeTypeReference<Tags.BlockAttribute>());
                    blockAttribute.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(blockDefinition.DisplayName)));
                    blockAttribute.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(blockDefinition.MaximumElementCount)));
                    blockAttribute.Arguments.Add(new CodeAttributeArgument(new CodeTypeReferenceExpression($"typeof({blockNameDictionary[blockDefinition]})")));
                    memberField.CustomAttributes.Add(blockAttribute);
                }

                //Create Padding attribute?
                if (field.Type == FieldType.FieldPad || field.Type == FieldType.FieldSkip)
                {
                    CodeAttributeDeclaration paddingAttribute = new CodeAttributeDeclaration(new CodeAttributeTypeReference<Tags.PaddingAttribute>());
                    paddingAttribute.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(field.DefinitionAddress)));
                    memberField.CustomAttributes.Add(paddingAttribute);
                }

                //Create Options attribute?
                if (enumDefinition != null)
                {
                    CodeAttributeDeclaration optionsAttribute = new CodeAttributeDeclaration(new CodeAttributeTypeReference<Tags.OptionsAttribute>());
                    optionsAttribute.Arguments.Add(new CodeAttributeArgument(new CodeTypeReferenceExpression($"typeof({optionNameDictionary[field]})")));
                    optionsAttribute.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(field.Type == FieldType.FieldByteFlags || field.Type == FieldType.FieldWordFlags || field.Type == FieldType.FieldLongFlags)));
                    memberField.CustomAttributes.Add(optionsAttribute);
                    memberField.Type = new CodeTypeReference(optionNameDictionary[field]);
                }

                //Add
                Members.Add(memberField);
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
                case FieldType.FieldCharEnum:
                case FieldType.FieldEnum:
                case FieldType.FieldLongEnum:
                case FieldType.FieldLongFlags:
                case FieldType.FieldWordFlags:
                case FieldType.FieldByteFlags: return optionNameDictionary[fieldDefinition];
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
                case FieldType.FieldData: return typeof(HaloLibrary.TagBlock).Name;
                case FieldType.FieldVertexBuffer: return null;
                case FieldType.FieldArrayStart: return $"{arrayNameDictionary[fieldDefinition]}[]";
                case FieldType.FieldPad:
                case FieldType.FieldSkip: return typeof(byte[]).Name;
                case FieldType.FieldArrayEnd:
                case FieldType.FieldUselessPad:
                case FieldType.FieldTerminator:
                case FieldType.FieldCustom:
                default: return null;
            }
        }

        private string GetEnumBaseType(TagFieldDefinition fieldDefinition)
        {
            switch (fieldDefinition.Type)
            {
                case FieldType.FieldCharEnum:
                case FieldType.FieldByteFlags: return typeof(byte).Name;
                case FieldType.FieldWordFlags:
                case FieldType.FieldEnum: return typeof(short).Name;
                case FieldType.FieldLongEnum:
                case FieldType.FieldLongFlags: return typeof(int).Name;
                default: return null;
            }
        }
    }
}
