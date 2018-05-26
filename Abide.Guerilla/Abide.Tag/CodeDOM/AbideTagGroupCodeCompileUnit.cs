using Abide.Tag.Definition;
using System.CodeDom;
using System.Collections.Generic;
using System.Reflection;
using HaloTag = Abide.HaloLibrary.Tag;

namespace Abide.Tag.CodeDOM
{
    /// <summary>
    /// Represents a tag block for a CodeDOM graph.
    /// </summary>
    public sealed class AbideTagGroupCodeCompileUnit : CodeCompileUnit
    {
        private const string c_HaloTagsNamespace = "HaloTag = Abide.HaloLibrary.Tag";
        private const string c_TagNamespace = "Abide.Tag";
        private const string c_GeneratedNamespace = "Abide.Tag.Generated";

        private readonly CodeTypeDeclaration tagGroupCodeTypeDeclaration;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbideTagGroupCodeCompileUnit"/> class.
        /// </summary>
        /// <param name="tagGroup">The tag group definition.</param>
        public AbideTagGroupCodeCompileUnit(AbideTagGroup tagGroup)
        {
            //Prepare
            string blockTypeName = AbideCodeDomGlobals.GetMemberName(AbideCodeDomGlobals.GetTagBlock(tagGroup.BlockName));
            string groupTypeName = AbideCodeDomGlobals.GetMemberName(tagGroup);
            
            //Create namespace
            CodeNamespace generatedCodeNamespace = new CodeNamespace(c_GeneratedNamespace);

            //Add imports
            generatedCodeNamespace.Imports.Add(new CodeNamespaceImport(c_TagNamespace));
            generatedCodeNamespace.Imports.Add(new CodeNamespaceImport(c_HaloTagsNamespace));

            //Create type
            tagGroupCodeTypeDeclaration = new CodeTypeDeclaration(groupTypeName)
            {
                TypeAttributes = TypeAttributes.Public,
                IsClass = true
            };
            tagGroupCodeTypeDeclaration.BaseTypes.Add(nameof(Group));
            tagGroupCodeTypeDeclaration.Comments.Add(new CodeCommentStatement("<summary>", true));
            tagGroupCodeTypeDeclaration.Comments.Add(new CodeCommentStatement($"Represents the generated {tagGroup.Name} " +
                $"({tagGroup.GroupTag.FourCc.Replace("<", "&lt;").Replace(">", "&gt;")}) tag group.", true));
            tagGroupCodeTypeDeclaration.Comments.Add(new CodeCommentStatement("</summary>", true));
            
            //Name property
            CodeMemberProperty nameMemberProperty = new CodeMemberProperty()
            {
                Attributes = MemberAttributes.Public | MemberAttributes.Override,
                Name = nameof(Group.Name),
                Type = new CodeTypeReference(typeof(string))
            };
            nameMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(tagGroup.Name)));
            nameMemberProperty.Comments.Add(new CodeCommentStatement("<summary>", true));
            nameMemberProperty.Comments.Add(new CodeCommentStatement($"Gets and returns the name of the {tagGroup.Name} tag group.", true));
            nameMemberProperty.Comments.Add(new CodeCommentStatement("</summary>", true));
            tagGroupCodeTypeDeclaration.Members.Add(nameMemberProperty);

            //GroupTag property
            CodeMemberProperty groupTagMemberProperty = new CodeMemberProperty()
            {
                Attributes = MemberAttributes.Public | MemberAttributes.Override,
                Name = nameof(Group.GroupTag),
                Type = new CodeTypeReference(nameof(HaloTag))
            };
            groupTagMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(tagGroup.GroupTag.FourCc)));
            groupTagMemberProperty.Comments.Add(new CodeCommentStatement("<summary>", true));
            groupTagMemberProperty.Comments.Add(new CodeCommentStatement($"Gets and returns the group tag of the {tagGroup.Name} tag group.", true));
            groupTagMemberProperty.Comments.Add(new CodeCommentStatement("</summary>", true));
            tagGroupCodeTypeDeclaration.Members.Add(groupTagMemberProperty);
            
            //Constructor
            CodeConstructor constructor = new CodeConstructor()
            {
                Attributes = MemberAttributes.Public,
            };
            constructor.Comments.Add(new CodeCommentStatement("<summary>", true));
            constructor.Comments.Add(new CodeCommentStatement($"Initializes a new instance of the <see cref=\"{groupTypeName}\"/> class.", true));
            constructor.Comments.Add(new CodeCommentStatement("</summary>", true));
            tagGroupCodeTypeDeclaration.Members.Add(constructor);

            //Initialize Blocks
            HaloTag parent = tagGroup.ParentGroupTag;
            while (parent.Dword != 0)
            {
                //Get Parent
                var parentGroup = AbideCodeDomGlobals.GetTagGroup(parent);

                //Add Block
                constructor.Statements.Insert(0, new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodePropertyReferenceExpression(
                    new CodeThisReferenceExpression(), nameof(Group.TagBlocks)), nameof(List<ITagBlock>.Add)), new CodeObjectCreateExpression(
                        new CodeTypeReference(AbideCodeDomGlobals.GetMemberName(AbideCodeDomGlobals.GetTagBlock(parentGroup.BlockName)))))));
                constructor.Statements.Insert(0, new CodeCommentStatement($"Add parent {parentGroup.Name} tag block to list."));

                //Get parent's parent group
                parent = parentGroup.ParentGroupTag;
            }

            //Add Block
            constructor.Statements.Add(new CodeCommentStatement($"Add tag block to list."));
            constructor.Statements.Add(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(), nameof(Group.TagBlocks)), nameof(List<ITagBlock>.Add)), new CodeObjectCreateExpression(
                    new CodeTypeReference(AbideCodeDomGlobals.GetMemberName(AbideCodeDomGlobals.GetTagBlock(tagGroup.BlockName))))));

            
            //Add type to namespace
            generatedCodeNamespace.Types.Add(tagGroupCodeTypeDeclaration);

            //Add namespace
            Namespaces.Add(generatedCodeNamespace);
        }
    }
}
