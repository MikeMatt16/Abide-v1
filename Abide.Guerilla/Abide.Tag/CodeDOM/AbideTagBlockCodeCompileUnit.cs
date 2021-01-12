using Abide.HaloLibrary;
using Abide.Tag.Definition;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Abide.Tag.CodeDom
{
    /// <summary>
    /// Represents a tag group for a CodeDOM graph.
    /// </summary>
    public sealed class AbideTagBlockCodeCompileUnit : CodeCompileUnit
    {
        private readonly CodeTypeDeclaration tagGroupCodeTypeDeclaration;
        private List<string> fieldNames = new List<string>();
        private int currentFieldIndex = -1;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbideTagBlockCodeCompileUnit"/> class.
        /// </summary>
        /// <param name="tagBlock">The tag block definition.</param>
        /// <param name="namespaceString">The optional namespace string. This defaults to "Cache."</param>
        /// <param name="tagNamespace"></param>
        public AbideTagBlockCodeCompileUnit(AbideTagBlock tagBlock, string namespaceString = "Cache", string tagNamespace = "Abide.Tag", TypeAttributes typeAttributes = TypeAttributes.Public)
        {
            //Prepare
            string blockTypeName = AbideCodeDomGlobals.GetMemberName(tagBlock);
            string baseTypeName = nameof(Block);

            //Create namespace
            CodeNamespace generatedCodeNamespace = new CodeNamespace($"{namespaceString}.Generated");

            //Add imports
            generatedCodeNamespace.Imports.Add(new CodeNamespaceImport(AbideCodeDomGlobals.SystemNamespace));
            generatedCodeNamespace.Imports.Add(new CodeNamespaceImport(AbideCodeDomGlobals.HaloLibraryNamespace));
            generatedCodeNamespace.Imports.Add(new CodeNamespaceImport(tagNamespace));

            //Create type
            tagGroupCodeTypeDeclaration = new CodeTypeDeclaration(blockTypeName)
            {
                TypeAttributes = typeAttributes | TypeAttributes.Sealed,
                IsClass = true
            };
            tagGroupCodeTypeDeclaration.BaseTypes.Add(baseTypeName);
            tagGroupCodeTypeDeclaration.Comments.Add(new CodeCommentStatement("<summary>", true));
            tagGroupCodeTypeDeclaration.Comments.Add(new CodeCommentStatement($"Represents the generated {tagBlock.Name} tag block.", true));
            tagGroupCodeTypeDeclaration.Comments.Add(new CodeCommentStatement("</summary>", true));

            //BlockName property
            CodeMemberProperty nameMemberProperty = new CodeMemberProperty()
            {
                Attributes = MemberAttributes.Public | MemberAttributes.Override,
                Name = nameof(Block.BlockName),
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

            //Add fields to fields list
            CodeExpression fieldCreateExpression = null;
            foreach (AbideTagField field in processedFields)
            {
                //Check
                if (field.FieldType == FieldType.FieldArrayStart)
                {
                    //Create
                    foreach (CodeExpression fieldExpression in CreateArrayFieldExpression(field))
                    {
                        //Add statement
                        constructor.Statements.Add(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodePropertyReferenceExpression(
                            new CodeThisReferenceExpression(), nameof(Block.Fields)), nameof(List<Field>.Add)), fieldExpression));

                        //Create property
                        CreateFieldProperty(++currentFieldIndex, field);
                    }
                }
                else
                {
                    //Create
                    fieldCreateExpression = CreateFieldCreateExpression(field);

                    //Add?
                    if (fieldCreateExpression != null)
                    {
                        //Add statement
                        constructor.Statements.Add(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodePropertyReferenceExpression(
                            new CodeThisReferenceExpression(), nameof(Block.Fields)), nameof(List<Field>.Add)), fieldCreateExpression));

                        //Create property
                        CreateFieldProperty(++currentFieldIndex, field);
                    }
                }
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
                Explanation = originalField.Explanation,
                GroupTag = originalField.GroupTag,
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
            CodeExpression explanationExpression = new CodePrimitiveExpression(tagField.Explanation ?? string.Empty);
            CodeExpression groupTagExpression = new CodePrimitiveExpression(tagField.GroupTag);

            //Check
            switch (tagField.FieldType)
            {
                case FieldType.FieldString:
                    return new CodeObjectCreateExpression(new CodeTypeReference("StringField"), nameExpression);
                case FieldType.FieldLongString:
                    return new CodeObjectCreateExpression(new CodeTypeReference("LongStringField"), nameExpression);
                case FieldType.FieldStringId:
                    return new CodeObjectCreateExpression(new CodeTypeReference("StringIdField"), nameExpression);
                case FieldType.FieldOldStringId:
                    return new CodeObjectCreateExpression(new CodeTypeReference("OldStringIdField"), nameExpression);
                case FieldType.FieldCharInteger:
                    return new CodeObjectCreateExpression(new CodeTypeReference("CharIntegerField"), nameExpression);
                case FieldType.FieldShortInteger:
                    return new CodeObjectCreateExpression(new CodeTypeReference("ShortIntegerField"), nameExpression);
                case FieldType.FieldLongInteger:
                    return new CodeObjectCreateExpression(new CodeTypeReference("LongIntegerField"), nameExpression);
                case FieldType.FieldAngle:
                    return new CodeObjectCreateExpression(new CodeTypeReference("AngleField"), nameExpression);
                case FieldType.FieldTag:
                    return new CodeObjectCreateExpression(new CodeTypeReference("TagField"), nameExpression);
                case FieldType.FieldCharEnum:
                    optionExpressions = new CodeExpression[tagField.Options.Count + 1];
                    optionExpressions[0] = nameExpression;
                    for (int i = 0; i < tagField.Options.Count; i++) optionExpressions[i + 1] = new CodePrimitiveExpression(tagField.Options[i].ToString());
                    return new CodeObjectCreateExpression(new CodeTypeReference("CharEnumField"), optionExpressions);
                case FieldType.FieldEnum:
                    optionExpressions = new CodeExpression[tagField.Options.Count + 1];
                    optionExpressions[0] = nameExpression;
                    for (int i = 0; i < tagField.Options.Count; i++) optionExpressions[i + 1] = new CodePrimitiveExpression(tagField.Options[i].ToString());
                    return new CodeObjectCreateExpression(new CodeTypeReference("EnumField"), optionExpressions);
                case FieldType.FieldLongEnum:
                    optionExpressions = new CodeExpression[tagField.Options.Count + 1];
                    optionExpressions[0] = nameExpression;
                    for (int i = 0; i < tagField.Options.Count; i++) optionExpressions[i + 1] = new CodePrimitiveExpression(tagField.Options[i].ToString());
                    return new CodeObjectCreateExpression(new CodeTypeReference("LongEnumField"), optionExpressions);
                case FieldType.FieldLongFlags:
                    optionExpressions = new CodeExpression[tagField.Options.Count + 1];
                    optionExpressions[0] = nameExpression;
                    for (int i = 0; i < tagField.Options.Count; i++) optionExpressions[i + 1] = new CodePrimitiveExpression(tagField.Options[i].ToString());
                    return new CodeObjectCreateExpression(new CodeTypeReference("LongFlagsField"), optionExpressions);
                case FieldType.FieldWordFlags:
                    optionExpressions = new CodeExpression[tagField.Options.Count + 1];
                    optionExpressions[0] = nameExpression;
                    for (int i = 0; i < tagField.Options.Count; i++) optionExpressions[i + 1] = new CodePrimitiveExpression(tagField.Options[i].ToString());
                    return new CodeObjectCreateExpression(new CodeTypeReference("WordFlagsField"), optionExpressions);
                case FieldType.FieldByteFlags:
                    optionExpressions = new CodeExpression[tagField.Options.Count + 1];
                    optionExpressions[0] = nameExpression;
                    for (int i = 0; i < tagField.Options.Count; i++) optionExpressions[i + 1] = new CodePrimitiveExpression(tagField.Options[i].ToString());
                    return new CodeObjectCreateExpression(new CodeTypeReference("ByteFlagsField"), optionExpressions);
                case FieldType.FieldPoint2D:
                    return new CodeObjectCreateExpression(new CodeTypeReference("Point2dField"), nameExpression);
                case FieldType.FieldRectangle2D:
                    return new CodeObjectCreateExpression(new CodeTypeReference("Rectangle2dField"), nameExpression);
                case FieldType.FieldRgbColor:
                    return new CodeObjectCreateExpression(new CodeTypeReference("RgbColorField"), nameExpression);
                case FieldType.FieldArgbColor:
                    return new CodeObjectCreateExpression(new CodeTypeReference("ArgbColorField"), nameExpression);
                case FieldType.FieldReal:
                    return new CodeObjectCreateExpression(new CodeTypeReference("RealField"), nameExpression);
                case FieldType.FieldRealFraction:
                    return new CodeObjectCreateExpression(new CodeTypeReference("RealFractionField"), nameExpression);
                case FieldType.FieldRealPoint2D:
                    return new CodeObjectCreateExpression(new CodeTypeReference("RealPoint2dField"), nameExpression);
                case FieldType.FieldRealPoint3D:
                    return new CodeObjectCreateExpression(new CodeTypeReference("RealPoint3dField"), nameExpression);
                case FieldType.FieldRealVector2D:
                    return new CodeObjectCreateExpression(new CodeTypeReference("RealVector2dField"), nameExpression);
                case FieldType.FieldRealVector3D:
                    return new CodeObjectCreateExpression(new CodeTypeReference("RealVector3dField"), nameExpression);
                case FieldType.FieldQuaternion:
                    return new CodeObjectCreateExpression(new CodeTypeReference("QuaternionField"), nameExpression);
                case FieldType.FieldEulerAngles2D:
                    return new CodeObjectCreateExpression(new CodeTypeReference("EulerAngles2dField"), nameExpression);
                case FieldType.FieldEulerAngles3D:
                    return new CodeObjectCreateExpression(new CodeTypeReference("EulerAngles3dField"), nameExpression);
                case FieldType.FieldRealPlane2D:
                    return new CodeObjectCreateExpression(new CodeTypeReference("RealPlane2dField"), nameExpression);
                case FieldType.FieldRealPlane3D:
                    return new CodeObjectCreateExpression(new CodeTypeReference("RealPlane3dField"), nameExpression);
                case FieldType.FieldRealRgbColor:
                    return new CodeObjectCreateExpression(new CodeTypeReference("RealRgbColorField"), nameExpression);
                case FieldType.FieldRealArgbColor:
                    return new CodeObjectCreateExpression(new CodeTypeReference("RealArgbColorField"), nameExpression);
                case FieldType.FieldRealHsvColor:
                    return new CodeObjectCreateExpression(new CodeTypeReference("RealHsvColorField"), nameExpression);
                case FieldType.FieldRealAhsvColor:
                    return new CodeObjectCreateExpression(new CodeTypeReference("RealAhsvColorField"), nameExpression);
                case FieldType.FieldShortBounds:
                    return new CodeObjectCreateExpression(new CodeTypeReference("ShortBoundsField"), nameExpression);
                case FieldType.FieldAngleBounds:
                    return new CodeObjectCreateExpression(new CodeTypeReference("AngleBoundsField"), nameExpression);
                case FieldType.FieldRealBounds:
                    return new CodeObjectCreateExpression(new CodeTypeReference("RealBoundsField"), nameExpression);
                case FieldType.FieldRealFractionBounds:
                    return new CodeObjectCreateExpression(new CodeTypeReference("RealFractionBoundsField"), nameExpression);
                case FieldType.FieldTagReference:
                    return new CodeObjectCreateExpression(new CodeTypeReference("TagReferenceField"), nameExpression, groupTagExpression);
                case FieldType.FieldBlock:
                    return new CodeObjectCreateExpression(new CodeTypeReference($"BlockField<{AbideCodeDomGlobals.GetMemberName(tagField.ReferencedBlock)}>"), nameExpression, new CodePrimitiveExpression(tagField.ReferencedBlock.MaximumElementCount));
                case FieldType.FieldLongBlockFlags:
                    return new CodeObjectCreateExpression(new CodeTypeReference("LongFlagsField"), nameExpression);
                case FieldType.FieldWordBlockFlags:
                    return new CodeObjectCreateExpression(new CodeTypeReference("WordFlagsField"), nameExpression);
                case FieldType.FieldByteBlockFlags:
                    return new CodeObjectCreateExpression(new CodeTypeReference("ByteFlagsField"), nameExpression);
                case FieldType.FieldCharBlockIndex1:
                    return new CodeObjectCreateExpression(new CodeTypeReference("CharBlockIndexField"), nameExpression);
                case FieldType.FieldCharBlockIndex2:
                    return new CodeObjectCreateExpression(new CodeTypeReference("CharBlockIndexField"), nameExpression);
                case FieldType.FieldShortBlockIndex1:
                    return new CodeObjectCreateExpression(new CodeTypeReference("ShortBlockIndexField"), nameExpression);
                case FieldType.FieldShortBlockIndex2:
                    return new CodeObjectCreateExpression(new CodeTypeReference("ShortBlockIndexField"), nameExpression);
                case FieldType.FieldLongBlockIndex1:
                    return new CodeObjectCreateExpression(new CodeTypeReference("LongBlockIndexField"), nameExpression);
                case FieldType.FieldLongBlockIndex2:
                    return new CodeObjectCreateExpression(new CodeTypeReference("LongBlockIndexField"), nameExpression);
                case FieldType.FieldData:
                    return new CodeObjectCreateExpression(new CodeTypeReference("DataField"), nameExpression, new CodePrimitiveExpression(tagField.ElementSize), new CodePrimitiveExpression(tagField.Alignment));
                case FieldType.FieldVertexBuffer:
                    return new CodeObjectCreateExpression(new CodeTypeReference("VertexBufferField"), nameExpression);
                case FieldType.FieldPad:
                    return new CodeObjectCreateExpression(new CodeTypeReference("PadField"), nameExpression, new CodePrimitiveExpression(tagField.Length));
                case FieldType.FieldSkip:
                    return new CodeObjectCreateExpression(new CodeTypeReference("SkipField"), nameExpression, new CodePrimitiveExpression(tagField.Length));
                case FieldType.FieldExplanation:
                    return new CodeObjectCreateExpression(new CodeTypeReference("ExplanationField"), nameExpression, explanationExpression);
                case FieldType.FieldStruct:
                    return new CodeObjectCreateExpression(new CodeTypeReference($"StructField<{AbideCodeDomGlobals.GetMemberName(tagField.ReferencedBlock)}>"), nameExpression);
                case FieldType.FieldTagIndex:
                    return new CodeObjectCreateExpression(new CodeTypeReference("TagIndexField"), nameExpression);
            }

            //Return
            return null;
        }

        private CodeExpression[] CreateArrayFieldExpression(AbideTagField arrayField)
        {
            //Prepare
            List<CodeExpression> expressions = new List<CodeExpression>();
            CodeExpression fieldCreateExpression = null;

            //Loop
            foreach (AbideTagField field in arrayField.Fields)
            {
                //Check
                if (field.FieldType == FieldType.FieldArrayStart)
                {
                    //Create
                    expressions.AddRange(CreateArrayFieldExpression(field));
                }
                else
                {
                    //Create
                    fieldCreateExpression = CreateFieldCreateExpression(field);

                    //Add?
                    if (fieldCreateExpression != null)
                    {
                        //Add statement
                        expressions.Add(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodePropertyReferenceExpression(
                            new CodeThisReferenceExpression(), nameof(Block.Fields)), nameof(List<Field>.Add)), fieldCreateExpression));
                    }
                }
            }

            //Return
            return expressions.ToArray();
        }

        private void CreateFieldProperty(int index, AbideTagField field)
        {
            return;

            if (field == null) throw new ArgumentNullException(nameof(field));
            if (string.IsNullOrEmpty(field.Name))
                return;

            Type returnType = null;

            switch (field.FieldType)
            {
                case FieldType.FieldString:
                case FieldType.FieldLongString:
                case FieldType.FieldStringId:
                    returnType = typeof(string);
                    break;

                case FieldType.FieldCharInteger:
                    returnType = typeof(byte);
                    break;

                case FieldType.FieldShortInteger:
                    returnType = typeof(short);
                    break;

                case FieldType.FieldLongInteger:
                    returnType = typeof(int);
                    break;

                case FieldType.FieldAngle:
                    returnType = typeof(float);
                    break;

                case FieldType.FieldTag:
                    returnType = typeof(TagFourCc);
                    break;

                case FieldType.FieldCharEnum:
                    returnType = typeof(byte);
                    break;

                case FieldType.FieldEnum:
                    returnType = typeof(short);
                    break;

                case FieldType.FieldLongEnum:
                    returnType = typeof(int);
                    break;

                case FieldType.FieldLongFlags:
                    returnType = typeof(int);
                    break;

                case FieldType.FieldWordFlags:
                    returnType = typeof(short);
                    break;

                case FieldType.FieldByteFlags:
                    returnType = typeof(byte);
                    break;

                case FieldType.FieldPoint2D:
                    returnType = typeof(Point2);
                    break;

                case FieldType.FieldRectangle2D:
                    returnType = typeof(Rectangle2);
                    break;

                case FieldType.FieldRgbColor:
                    returnType = typeof(ColorRgb);
                    break;

                case FieldType.FieldArgbColor:
                    returnType = typeof(ColorArgb);
                    break;

                case FieldType.FieldReal:
                case FieldType.FieldRealFraction:
                    returnType = typeof(float);
                    break;

                case FieldType.FieldRealPoint2D:
                    returnType = typeof(Point2F);
                    break;

                case FieldType.FieldRealPoint3D:
                    returnType = typeof(Point3F);
                    break;

                case FieldType.FieldRealVector2D:
                    returnType = typeof(Vector2);
                    break;

                case FieldType.FieldRealVector3D:
                    returnType = typeof(Vector3);
                    break;

                case FieldType.FieldQuaternion:
                    returnType = typeof(Quaternion);
                    break;

                case FieldType.FieldEulerAngles2D:
                    returnType = typeof(Vector2);
                    break;

                case FieldType.FieldEulerAngles3D:
                    returnType = typeof(Vector3);
                    break;
            }

            //Format name
            string originalName = AbideCodeDomGlobals.GeneratePascalCasedName(field.Name);
            if (string.IsNullOrEmpty(originalName))
                return;

            int nameCount = 0;
            string formattedName = originalName;
            while (fieldNames.Contains(formattedName))
                formattedName = $"{originalName}{++nameCount}";

            //Add to list
            fieldNames.Add(formattedName);

            //Create field indexer
            CodeIndexerExpression fieldsIndexerExpression = new CodeIndexerExpression(
                new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), nameof(Block.Fields)));
            fieldsIndexerExpression.Indices.Add(new CodePrimitiveExpression(index));

            //Create field value property expression
            CodePropertyReferenceExpression valuePropRef = new CodePropertyReferenceExpression(
                fieldsIndexerExpression, "Value");

            CodeAssignStatement setStatement = new CodeAssignStatement(valuePropRef, new CodePropertySetValueReferenceExpression());
            CodeMethodReturnStatement returnStatement = null;
            CodeTypeReference returnTypeReference = null;
            if (returnType != null)
            {
                returnTypeReference = new CodeTypeReference(returnType.Name);
                returnStatement = new CodeMethodReturnStatement(new CodeCastExpression(
                    new CodeTypeReference(returnType.Name), valuePropRef));
            }
            else
            {
                switch (field.FieldType)
                {
                    case FieldType.FieldBlock:
                        returnTypeReference = new CodeTypeReference($"BlockField<{AbideCodeDomGlobals.GetMemberName(field.ReferencedBlock)}>");
                        returnStatement = new CodeMethodReturnStatement(new CodeCastExpression(
                            returnTypeReference, fieldsIndexerExpression));
                        setStatement = null;
                        break;
                }
            }

            //Check
            if (returnStatement == null | returnTypeReference == null)
                return;

            //Create field property
            CodeMemberProperty fieldMemberProperty = new CodeMemberProperty()
            {
                Attributes = MemberAttributes.Public | MemberAttributes.Final,
                Name = formattedName,
                Type = returnTypeReference
            };
            fieldMemberProperty.GetStatements.Add(returnStatement);
            if (setStatement != null) fieldMemberProperty.SetStatements.Add(setStatement);
            fieldMemberProperty.Comments.Add(new CodeCommentStatement("<summary>", true));
            fieldMemberProperty.Comments.Add(new CodeCommentStatement($"Gets or sets the value of the {field.Name} field.", true));
            fieldMemberProperty.Comments.Add(new CodeCommentStatement("</summary>", true));
            tagGroupCodeTypeDeclaration.Members.Add(fieldMemberProperty);
        }
    }
}
