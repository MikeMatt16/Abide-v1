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
        /// <param name="namespaceString">The optional namespace string. This defaults to "Cache."</param>
        /// <param name="tagNamespace"></param>
        public AbideTagLookupCodeCompileUnit(string namespaceString = "Cache", string tagNamespace = "Abide.Tag")
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
                TypeAttributes = TypeAttributes.Public,
                IsClass = true
            };
            tagLookupCodeTypeDeclaration.Comments.Add(new CodeCommentStatement("<summary>", true));
            tagLookupCodeTypeDeclaration.Comments.Add(new CodeCommentStatement($"Represents the global tag lookup class.", true));
            tagLookupCodeTypeDeclaration.Comments.Add(new CodeCommentStatement("</summary>", true));

            //Create Method
            CodeMemberMethod createTagGroupMethod = new CodeMemberMethod()
            {
                Attributes = MemberAttributes.Public | MemberAttributes.Static,
                Name = "CreateTagGroup",
                ReturnType = new CodeTypeReference(nameof(Group)),
            };
            createTagGroupMethod.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(nameof(TagFourCc)), "groupTag"));
            createTagGroupMethod.Comments.Add(new CodeCommentStatement("<summary>", true));
            createTagGroupMethod.Comments.Add(new CodeCommentStatement($"Returns a <see cref=\"Group\"/> instance based on the supplied tag.", true));
            createTagGroupMethod.Comments.Add(new CodeCommentStatement("</summary>", true));
            createTagGroupMethod.Comments.Add(new CodeCommentStatement("<param name=\"groupTag\">The group tag.</param>", true));
            tagLookupCodeTypeDeclaration.Members.Add(createTagGroupMethod);

            //Generate if statements
            foreach (var tagGroup in AbideCodeDomGlobals.GetTagGroups())
            {
                createTagGroupMethod.Statements.Add(new CodeCommentStatement($"Check for {tagGroup.Name}..."));
                createTagGroupMethod.Statements.Add(new CodeConditionStatement(new CodeBinaryOperatorExpression(new CodeArgumentReferenceExpression("groupTag"), CodeBinaryOperatorType.ValueEquality,
                    new CodePrimitiveExpression(tagGroup.GroupTag.FourCc)), new CodeMethodReturnStatement(
                        new CodeObjectCreateExpression(new CodeTypeReference(AbideCodeDomGlobals.GetMemberName(tagGroup))))));
            }
            createTagGroupMethod.Statements.Add(new CodeCommentStatement($"Return null..."));
            createTagGroupMethod.Statements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression()));

            //Add type to namespace
            generatedCodeNamespace.Types.Add(tagLookupCodeTypeDeclaration);

            //Add namespace
            Namespaces.Add(generatedCodeNamespace);
        }
    }
}
