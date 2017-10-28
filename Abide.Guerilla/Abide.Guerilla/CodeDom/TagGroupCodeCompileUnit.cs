using Abide.Guerilla.Managed;
using System.CodeDom;

namespace Abide.Guerilla.CodeDom
{
    /// <summary>
    /// Represents a tag group for a CodeDOM graph.
    /// </summary>
    public sealed class TagGroupCodeCompileUnit : TagBlockCodeCompileUnit
    {
        /// <summary>
        /// Initializes a new <see cref="TagGroupCodeCompileUnit"/> using the supplied tag group definition and tag block definition.
        /// </summary>
        /// <param name="guerilla">Me want banana.</param>
        /// <param name="tagGroupDefinition">The tag group definition.</param>
        /// <param name="tagBlockDefinition">The tag block definition.</param>
        public TagGroupCodeCompileUnit(GuerillaInstance guerilla, TagGroupDefinition tagGroupDefinition, TagBlockDefinition tagBlockDefinition) : base(guerilla, tagBlockDefinition)
        {
            //Field set attribute
            CodeAttributeDeclaration tagGroupAttribute = new CodeAttributeDeclaration(new CodeTypeReference(typeof(Tags.TagGroupAttribute)));
            tagGroupAttribute.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(tagGroupDefinition.Name)));
            tagGroupAttribute.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(tagGroupDefinition.GroupTag.Dword)));
            tagGroupAttribute.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(tagGroupDefinition.ParentGroupTag.Dword)));
            tagGroupAttribute.Arguments.Add(new CodeAttributeArgument(new CodeTypeReferenceExpression($"typeof({BlockName})")));
            TagBlockCodeTypeDeclaration.CustomAttributes.Add(tagGroupAttribute);
        }
    }
}
