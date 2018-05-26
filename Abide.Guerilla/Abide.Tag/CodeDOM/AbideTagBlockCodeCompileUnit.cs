using Abide.Tag.Definition;
using System.CodeDom;
using System.Collections.Generic;
using System.Reflection;

namespace Abide.Tag.CodeDOM
{
    /// <summary>
    /// Represents a tag group for a CodeDOM graph.
    /// </summary>
    public sealed class AbideTagBlockCodeCompileUnit : CodeCompileUnit
    {
        private const string c_TagNamespace = "Abide.Tag";
        private const string c_GeneratedNamespace = "Abide.Tag.Generated";
        private const string c_IoNamespace = "System.IO";

        private readonly CodeTypeDeclaration tagGroupCodeTypeDeclaration;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbideTagBlockCodeCompileUnit"/> class.
        /// </summary>
        /// <param name="tagBlock">The tag block definition.</param>
        public AbideTagBlockCodeCompileUnit(AbideTagBlock tagBlock)
        {
            //Prepare
            string blockTypeName = AbideCodeDomGlobals.GetMemberName(tagBlock);
            string baseTypeName = nameof(Block);
            
            //Create namespace
            CodeNamespace generatedCodeNamespace = new CodeNamespace(c_GeneratedNamespace);

            //Add imports
            generatedCodeNamespace.Imports.Add(new CodeNamespaceImport(c_TagNamespace));

            //Create type
            tagGroupCodeTypeDeclaration = new CodeTypeDeclaration(blockTypeName)
            {
                TypeAttributes = TypeAttributes.Public |TypeAttributes.Sealed,
                IsClass = true
            };
            tagGroupCodeTypeDeclaration.BaseTypes.Add(baseTypeName);
            tagGroupCodeTypeDeclaration.Comments.Add(new CodeCommentStatement("<summary>", true));
            tagGroupCodeTypeDeclaration.Comments.Add(new CodeCommentStatement($"Represents the generated {tagBlock.Name} tag block.", true));
            tagGroupCodeTypeDeclaration.Comments.Add(new CodeCommentStatement("</summary>", true));

            //Name property
            CodeMemberProperty nameMemberProperty = new CodeMemberProperty()
            {
                Attributes = MemberAttributes.Public | MemberAttributes.Override,
                Name = nameof(Block.Name),
                Type = new CodeTypeReference(typeof(string))
            };
            nameMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(tagBlock.Name)));
            nameMemberProperty.Comments.Add(new CodeCommentStatement("<summary>", true));
            nameMemberProperty.Comments.Add(new CodeCommentStatement($"Gets and returns the name of the {tagBlock.Name} tag block.", true));
            nameMemberProperty.Comments.Add(new CodeCommentStatement("</summary>", true));
            tagGroupCodeTypeDeclaration.Members.Add(nameMemberProperty);

            //DisplayName property
            CodeMemberProperty displayNameMemberProperty = new CodeMemberProperty()
            {
                Attributes = MemberAttributes.Public | MemberAttributes.Override,
                Name = nameof(Block.DisplayName),
                Type = new CodeTypeReference(typeof(string))
            };
            displayNameMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(tagBlock.DisplayName)));
            displayNameMemberProperty.Comments.Add(new CodeCommentStatement("<summary>", true));
            displayNameMemberProperty.Comments.Add(new CodeCommentStatement($"Gets and returns the display name of the {tagBlock.Name} tag block.", true));
            displayNameMemberProperty.Comments.Add(new CodeCommentStatement("</summary>", true));
            tagGroupCodeTypeDeclaration.Members.Add(displayNameMemberProperty);

            //MaximumElementCount property
            CodeMemberProperty groupTagMemberProperty = new CodeMemberProperty()
            {
                Attributes = MemberAttributes.Public | MemberAttributes.Override,
                Name = nameof(Block.MaximumElementCount),
                Type = new CodeTypeReference(typeof(int))
            };
            groupTagMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(tagBlock.MaximumElementCount)));
            groupTagMemberProperty.Comments.Add(new CodeCommentStatement("<summary>", true));
            groupTagMemberProperty.Comments.Add(new CodeCommentStatement($"Gets and returns the maximum number of elements allowed of the {tagBlock.Name} tag block.", true));
            groupTagMemberProperty.Comments.Add(new CodeCommentStatement("</summary>", true));
            tagGroupCodeTypeDeclaration.Members.Add(groupTagMemberProperty);

            //Alignment property
            CodeMemberProperty alignmentMemberProperty = new CodeMemberProperty()
            {
                Attributes = MemberAttributes.Public | MemberAttributes.Override,
                Name = nameof(Block.Alignment),
                Type = new CodeTypeReference(typeof(int))
            };
            alignmentMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(tagBlock.FieldSet.Alignment)));
            alignmentMemberProperty.Comments.Add(new CodeCommentStatement("<summary>", true));
            alignmentMemberProperty.Comments.Add(new CodeCommentStatement($"Gets and returns the alignment of the {tagBlock.Name} tag block.", true));
            alignmentMemberProperty.Comments.Add(new CodeCommentStatement("</summary>", true));
            tagGroupCodeTypeDeclaration.Members.Add(alignmentMemberProperty);

            //Constructor
            CodeConstructor constructor = new CodeConstructor()
            {
                Attributes = MemberAttributes.Public,
            };
            constructor.Comments.Add(new CodeCommentStatement("<summary>", true));
            constructor.Comments.Add(new CodeCommentStatement($"Initializes a new instance of the <see cref=\"{blockTypeName}\"/> class.", true));
            constructor.Comments.Add(new CodeCommentStatement("</summary>", true));
            tagGroupCodeTypeDeclaration.Members.Add(constructor);

            //Process fields
            List<AbideTagField> processedFields = ProcessTagFields(tagBlock.FieldSet);
            List<int> tagBlockIndices = new List<int>();
            List<string> tagBlockTypes = new List<string>();
            
            //Add fields to fields list
            CodeExpression fieldCreateExpression = null; int index = 0;
            foreach (AbideTagField field in processedFields)
            {
                //Create
                fieldCreateExpression = CreateFieldCreateExpression(field);

                //Add?
                if (fieldCreateExpression != null)
                {
                    //Add Tag Block?
                    if (field.FieldType == FieldType.FieldBlock)
                    {
                        tagBlockIndices.Add(index);
                        tagBlockTypes.Add($"BlockField<{AbideCodeDomGlobals.GetMemberName(field.ReferencedBlock)}>");
                    }
                    
                    //Add statement
                    constructor.Statements.Add(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodePropertyReferenceExpression(
                        new CodeThisReferenceExpression(), nameof(Block.Fields)), nameof(List<Field>.Add)), fieldCreateExpression));

                    //Increment
                    index++;
                }
            }

            //Check
            if(tagBlockIndices.Count > 0)
            {
                //Add IO Namespace
                generatedCodeNamespace.Imports.Add(new CodeNamespaceImport(c_IoNamespace));

                //Override Write method
                CodeMemberMethod writeMethod = new CodeMemberMethod()
                {
                    Attributes = MemberAttributes.Public | MemberAttributes.Override,
                    Name = nameof(Block.Write),
                    ReturnType = new CodeTypeReference()
                };
                writeMethod.Comments.Add(new CodeCommentStatement("<summary>", true));
                writeMethod.Comments.Add(new CodeCommentStatement($"Writes the {tagBlock.Name} tag block using the specified binary writer.", true));
                writeMethod.Comments.Add(new CodeCommentStatement("</summary>", true));
                writeMethod.Comments.Add(new CodeCommentStatement($"<param name=\"writer\">The <see cref=\"BinaryWriter\"/> used to write the {tagBlock.Name} tag block.</param>"));
                writeMethod.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(nameof(System.IO.BinaryWriter)), "writer"));
                writeMethod.Statements.Add(new CodeCommentStatement("Invoke base write procedure."));
                writeMethod.Statements.Add(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(
                    new CodeBaseReferenceExpression(), nameof(Block.Write)), new CodeVariableReferenceExpression("writer")));
                writeMethod.Statements.Add(new CodeCommentStatement("Post-write the tag blocks."));
                for (int i = 0; i < tagBlockIndices.Count; i++)
                    writeMethod.Statements.Add(new CodeMethodInvokeExpression(
                        new CodeMethodReferenceExpression(
                            new CodeCastExpression(
                                new CodeTypeReference(tagBlockTypes[i]), 
                                new CodeArrayIndexerExpression(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), nameof(Block.Fields)), 
                                new CodePrimitiveExpression(tagBlockIndices[i]))), "WriteChildren"), new CodeVariableReferenceExpression("writer")));
                tagGroupCodeTypeDeclaration.Members.Add(writeMethod);
            }

            //Add type to namespace
            generatedCodeNamespace.Types.Add(tagGroupCodeTypeDeclaration);

            //Add namespace
            Namespaces.Add(generatedCodeNamespace);
        }

        private List<AbideTagField> ProcessTagFields(IList<AbideTagField> fieldsList, int iterationCount = 1)
        {
            //Prepare
            int arrayIndex = 0;
            List<AbideTagField> processedFields = new List<AbideTagField>();

            //Loop
            for (int i = 0; i < iterationCount; i++)
                for (int j = 0; j < fieldsList.Count; j++)
                {
                    //Check
                    if (fieldsList[j].FieldType == FieldType.FieldArrayStart)
                    {
                        //Prepare
                        List<AbideTagField> arrayList = new List<AbideTagField>();

                        //Get start index
                        int startArrayIndex = arrayIndex++;

                        //Loop
                        for (int k = j + 1; k < fieldsList.Count; k++)
                        {
                            //Break iteration
                            if (arrayIndex == startArrayIndex) break;

                            //Check type
                            if (fieldsList[k].FieldType == FieldType.FieldArrayStart) arrayIndex++;
                            else if (fieldsList[k].FieldType == FieldType.FieldArrayEnd) arrayIndex--;

                            //Add
                            arrayList.Add(fieldsList[k]);
                        }

                        //Process array
                        processedFields.AddRange(ProcessTagFields(arrayList, fieldsList[j].Length));

                        //Increment
                        j += (arrayList.Count - 1);
                    }
                    else if (fieldsList[j].FieldType != FieldType.FieldArrayEnd || fieldsList[j].FieldType != FieldType.FieldTerminator)
                        processedFields.Add(CloneField(fieldsList[j].Name, fieldsList[j]));  //Add Field
                }

            //Return
            return processedFields;
        }

        private static AbideTagField CloneField(string name, AbideTagField originalField)
        {
            //Setup
            ObjectName fieldName = originalField.NameObject;
            fieldName.Name = name;

            //Initialize
            AbideTagField field = new AbideTagField
            {
                NameObject = fieldName,
                ReferencedBlock = originalField.ReferencedBlock,
                BlockName = originalField.BlockName,
                StructName = originalField.StructName,
                FieldType = originalField.FieldType,
                Alignment = originalField.Alignment,
                MaximumSize = originalField.MaximumSize,
                ElementSize = originalField.ElementSize,
                Length = originalField.Length,
            };
            field.Options.AddRange(originalField.Options);

            //Return
            return field;
        }

        private CodeExpression CreateFieldCreateExpression(AbideTagField tagField)
        {
            //Prepare
            CodeExpression[] optionExpressions = null;
            CodeExpression nameExpression = new CodePrimitiveExpression(tagField.NameObject.ToString());

            //Check
            switch (tagField.FieldType)
            {
                case FieldType.FieldString:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(StringField)), nameExpression);
                case FieldType.FieldLongString:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(LongStringField)), nameExpression);
                case FieldType.FieldStringId:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(StringIdField)), nameExpression);
                case FieldType.FieldOldStringId:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(OldStringIdField)), nameExpression);
                case FieldType.FieldCharInteger:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(CharIntegerField)), nameExpression);
                case FieldType.FieldShortInteger:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(ShortIntegerField)), nameExpression);
                case FieldType.FieldLongInteger:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(LongIntegerField)), nameExpression);
                case FieldType.FieldAngle:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(AngleField)), nameExpression);
                case FieldType.FieldTag:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(TagField)), nameExpression);
                case FieldType.FieldCharEnum:
                    optionExpressions = new CodeExpression[tagField.Options.Count + 1];
                    optionExpressions[0] = nameExpression;
                    for (int i = 0; i < tagField.Options.Count; i++) optionExpressions[i + 1] = new CodePrimitiveExpression(tagField.Options[i].ToString());
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(CharEnumField)), optionExpressions);
                case FieldType.FieldEnum:
                    optionExpressions = new CodeExpression[tagField.Options.Count + 1];
                    optionExpressions[0] = nameExpression;
                    for (int i = 0; i < tagField.Options.Count; i++) optionExpressions[i + 1] = new CodePrimitiveExpression(tagField.Options[i].ToString());
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(EnumField)), optionExpressions);
                case FieldType.FieldLongEnum:
                    optionExpressions = new CodeExpression[tagField.Options.Count + 1];
                    optionExpressions[0] = nameExpression;
                    for (int i = 0; i < tagField.Options.Count; i++) optionExpressions[i + 1] = new CodePrimitiveExpression(tagField.Options[i].ToString());
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(LongEnumField)), optionExpressions);
                case FieldType.FieldLongFlags:
                    optionExpressions = new CodeExpression[tagField.Options.Count + 1];
                    optionExpressions[0] = nameExpression;
                    for (int i = 0; i < tagField.Options.Count; i++) optionExpressions[i + 1] = new CodePrimitiveExpression(tagField.Options[i].ToString());
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(LongFlagsField)), optionExpressions);
                case FieldType.FieldWordFlags:
                    optionExpressions = new CodeExpression[tagField.Options.Count + 1];
                    optionExpressions[0] = nameExpression;
                    for (int i = 0; i < tagField.Options.Count; i++) optionExpressions[i + 1] = new CodePrimitiveExpression(tagField.Options[i].ToString());
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(WordFlagsField)), optionExpressions);
                case FieldType.FieldByteFlags:
                    optionExpressions = new CodeExpression[tagField.Options.Count + 1];
                    optionExpressions[0] = nameExpression;
                    for (int i = 0; i < tagField.Options.Count; i++) optionExpressions[i + 1] = new CodePrimitiveExpression(tagField.Options[i].ToString());
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(ByteFlagsField)), optionExpressions);
                case FieldType.FieldPoint2D:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(Point2dField)), nameExpression);
                case FieldType.FieldRectangle2D:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(Rectangle2dField)), nameExpression);
                case FieldType.FieldRgbColor:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(RgbColorField)), nameExpression);
                case FieldType.FieldArgbColor:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(ArgbColorField)), nameExpression);
                case FieldType.FieldReal:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(RealField)), nameExpression);
                case FieldType.FieldRealFraction:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(RealFractionField)), nameExpression);
                case FieldType.FieldRealPoint2D:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(RealPoint2dField)), nameExpression);
                case FieldType.FieldRealPoint3D:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(RealPoint3dField)), nameExpression);
                case FieldType.FieldRealVector2D:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(RealVector2dField)), nameExpression);
                case FieldType.FieldRealVector3D:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(RealVector3dField)), nameExpression);
                case FieldType.FieldQuaternion:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(QuaternionField)), nameExpression);
                case FieldType.FieldEulerAngles2D:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(EulerAngles2dField)), nameExpression);
                case FieldType.FieldEulerAngles3D:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(EulerAngles3dField)), nameExpression);
                case FieldType.FieldRealPlane2D:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(RealPlane2dField)), nameExpression);
                case FieldType.FieldRealPlane3D:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(RealPlane3dField)), nameExpression);
                case FieldType.FieldRealRgbColor:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(RealRgbColorField)), nameExpression);
                case FieldType.FieldRealArgbColor:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(RealArgbColorField)), nameExpression);
                case FieldType.FieldRealHsvColor:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(RealHsvColorField)), nameExpression);
                case FieldType.FieldRealAhsvColor:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(RealAhsvColorField)), nameExpression);
                case FieldType.FieldShortBounds:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(ShortBoundsField)), nameExpression);
                case FieldType.FieldAngleBounds:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(AngleBoundsField)), nameExpression);
                case FieldType.FieldRealBounds:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(RealBoundsField)), nameExpression);
                case FieldType.FieldRealFractionBounds:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(RealFractionBoundsField)), nameExpression);
                case FieldType.FieldTagReference:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(TagReferenceField)), nameExpression);
                case FieldType.FieldBlock:
                    return new CodeObjectCreateExpression(new CodeTypeReference($"BlockField<{AbideCodeDomGlobals.GetMemberName(tagField.ReferencedBlock)}>"), nameExpression, new CodePrimitiveExpression(tagField.ReferencedBlock.MaximumElementCount));
                case FieldType.FieldLongBlockFlags:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(LongFlagsField)), nameExpression);
                case FieldType.FieldWordBlockFlags:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(WordFlagsField)), nameExpression);
                case FieldType.FieldByteBlockFlags:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(ByteFlagsField)), nameExpression);
                case FieldType.FieldCharBlockIndex1:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(CharBlockIndexField)), nameExpression);
                case FieldType.FieldCharBlockIndex2:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(CharBlockIndexField)), nameExpression);
                case FieldType.FieldShortBlockIndex1:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(ShortBlockIndexField)), nameExpression);
                case FieldType.FieldShortBlockIndex2:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(ShortBlockIndexField)), nameExpression);
                case FieldType.FieldLongBlockIndex1:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(LongBlockIndexField)), nameExpression);
                case FieldType.FieldLongBlockIndex2:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(LongBlockIndexField)), nameExpression);
                case FieldType.FieldData:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(DataField)), nameExpression);
                case FieldType.FieldVertexBuffer:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(VertexBufferField)), nameExpression);
                case FieldType.FieldPad:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(PadField)), nameExpression, new CodePrimitiveExpression(tagField.Length));
                case FieldType.FieldSkip:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(SkipField)), nameExpression, new CodePrimitiveExpression(tagField.Length));
                case FieldType.FieldExplanation:
                    return new CodeObjectCreateExpression(new CodeTypeReference(nameof(ExplanationField)), nameExpression, new CodePrimitiveExpression(tagField.Explanation));
                case FieldType.FieldStruct:
                    return new CodeObjectCreateExpression(new CodeTypeReference($"StructField<{AbideCodeDomGlobals.GetMemberName(tagField.ReferencedBlock)}>"), nameExpression);
            }

            //Return
            return null;
        }
    }
}
