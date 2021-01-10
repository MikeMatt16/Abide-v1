using Abide.HaloLibrary;
using System.CodeDom;
using System.Reflection;

namespace Abide.Tag.CodeDom
{
    /// <summary>
    /// Represents a tag lookup for a CodeDOM graph.
    /// </summary>
    public class AbideTagLookupCodeCompileUnit : CodeCompileUnit
    {
        private readonly CodeTypeDeclaration tagLookupCodeTypeDeclaration;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbideTagLookupCodeCompileUnit"/> class.
        /// </summary>
        /// <param name="namespaceString">The optional namespace string. This defaults to "Abide.Tag.Cache."</param>
        /// <param name="tagNamespace"></param>
        public AbideTagLookupCodeCompileUnit(string namespaceString = "Abide.Tag.Cache", string tagNamespace = "Abide.Tag", TypeAttributes typeAttributes = TypeAttributes.Public)
        {
            //Create namespace
            CodeNamespace generatedCodeNamespace = new CodeNamespace($"{namespaceString}.Generated");

            //Add imports
            generatedCodeNamespace.Imports.Add(new CodeNamespaceImport(AbideCodeDomGlobals.SystemNamespace));
            generatedCodeNamespace.Imports.Add(new CodeNamespaceImport(AbideCodeDomGlobals.HaloLibraryNamespace));
            generatedCodeNamespace.Imports.Add(new CodeNamespaceImport(tagNamespace));

            //Create type
            tagLookupCodeTypeDeclaration = new CodeTypeDeclaration("TagLookup")
            {
                TypeAttributes = typeAttributes,
                IsClass = true
            };
            tagLookupCodeTypeDeclaration.Comments.Add(new CodeCommentStatement("<summary>", true));
            tagLookupCodeTypeDeclaration.Comments.Add(new CodeCommentStatement($"Represents the global tag lookup class.", true));
            tagLookupCodeTypeDeclaration.Comments.Add(new CodeCommentStatement("</summary>", true));

            //Create Method
            CodeMemberMethod createTagGroupMethod1 = new CodeMemberMethod()
            {
                Attributes = MemberAttributes.Public | MemberAttributes.Static,
                Name = "CreateTagGroup",
                ReturnType = new CodeTypeReference(nameof(Group)),
            };
            createTagGroupMethod1.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(nameof(TagFourCc)), "groupTag"));
            createTagGroupMethod1.Comments.Add(new CodeCommentStatement("<summary>", true));
            createTagGroupMethod1.Comments.Add(new CodeCommentStatement($"Returns a <see cref=\"Group\"/> instance based on the supplied tag.", true));
            createTagGroupMethod1.Comments.Add(new CodeCommentStatement("</summary>", true));
            createTagGroupMethod1.Comments.Add(new CodeCommentStatement("<param name=\"groupTag\">The group tag.</param>", true));
            tagLookupCodeTypeDeclaration.Members.Add(createTagGroupMethod1);

            //Create Method
            CodeMemberMethod createTagGroupMethod2 = new CodeMemberMethod()
            {
                Attributes = MemberAttributes.Public | MemberAttributes.Static,
                Name = "CreateTagGroup",
                ReturnType = new CodeTypeReference(nameof(Group)),
            };
            createTagGroupMethod2.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(string)), "tagGroupName"));
            createTagGroupMethod2.Comments.Add(new CodeCommentStatement("<summary>", true));
            createTagGroupMethod2.Comments.Add(new CodeCommentStatement($"Returns a <see cref=\"Group\"/> instance based on the supplied tag group name.", true));
            createTagGroupMethod2.Comments.Add(new CodeCommentStatement("</summary>", true));
            createTagGroupMethod2.Comments.Add(new CodeCommentStatement("<param name=\"tagGroupName\">The name of the tag group.</param>", true));
            tagLookupCodeTypeDeclaration.Members.Add(createTagGroupMethod2);

            //Generate if statements
            foreach (var tagGroup in AbideCodeDomGlobals.GetTagGroups())
            {
                createTagGroupMethod1.Statements.Add(new CodeCommentStatement($" {tagGroup.Name}"));
                createTagGroupMethod1.Statements.Add(new CodeConditionStatement(new CodeBinaryOperatorExpression(new CodeArgumentReferenceExpression("groupTag"), CodeBinaryOperatorType.ValueEquality,
                    new CodePrimitiveExpression(tagGroup.GroupTag.FourCc)), new CodeMethodReturnStatement(
                        new CodeObjectCreateExpression(new CodeTypeReference(AbideCodeDomGlobals.GetMemberName(tagGroup))))));
            }
            createTagGroupMethod1.Statements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression()));

            //Generate if statements
            foreach (var tagGroup in AbideCodeDomGlobals.GetTagGroups())
            {
                createTagGroupMethod2.Statements.Add(new CodeCommentStatement($" {tagGroup.Name}"));
                createTagGroupMethod2.Statements.Add(new CodeConditionStatement(new CodeBinaryOperatorExpression(new CodeArgumentReferenceExpression("tagGroupName"), CodeBinaryOperatorType.ValueEquality,
                    new CodePrimitiveExpression(tagGroup.Name)), new CodeMethodReturnStatement(
                        new CodeObjectCreateExpression(new CodeTypeReference(AbideCodeDomGlobals.GetMemberName(tagGroup))))));
            }
            createTagGroupMethod2.Statements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression()));

            //Add type to namespace
            generatedCodeNamespace.Types.Add(tagLookupCodeTypeDeclaration);

            //Add namespace
            Namespaces.Add(generatedCodeNamespace);
        }
    }
}
