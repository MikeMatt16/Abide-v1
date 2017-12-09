using Abide.Guerilla.Managed;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Abide.Guerilla.CodeDom
{
    /// <summary>
    /// Represents a tag block for a CodeDOM graph.
    /// </summary>
    public class TagBlockCodeCompileUnit : CodeCompileUnit
    {
        /// <summary>
        /// Gets and returns the block name.
        /// </summary>
        public string BlockName
        {
            get { return blockName; }
        }
        /// <summary>
        /// Gets and returns the underlying code type declaration.
        /// </summary>
        public CodeTypeDeclaration TagBlockCodeTypeDeclaration
        {
            get { return tagBlockCodeTypeDeclaration; }
        }

        private const string SystemNamespace = "System";
        private const string IONamespace = "System.IO";
        private const string TagsNamespace = "Abide.Guerilla.Tags";
        private const string TypesNamespace = "Abide.Guerilla.Types";
        private const string HaloLibraryNamespace = "Abide.HaloLibrary";

        private readonly string blockName;
        private readonly List<string> membersList = new List<string>();
        private readonly Dictionary<TagBlockDefinition, string> blockNameDictionary = new Dictionary<TagBlockDefinition, string>();
        private readonly Dictionary<TagFieldDefinition, string> optionNameDictionary = new Dictionary<TagFieldDefinition, string>();
        private readonly Dictionary<TagFieldDefinition, string> arrayNameDictionary = new Dictionary<TagFieldDefinition, string>();
        private readonly CodeTypeDeclaration tagBlockCodeTypeDeclaration;

        /// <summary>
        /// Initializes the tag block code compile unit using the supplied tag block.
        /// </summary>
        /// <param name="guerilla">Me want banana.</param>
        /// <param name="tagBlockDefinition">The tag block definition.</param>
        public TagBlockCodeCompileUnit(GuerillaInstance guerilla, TagBlockDefinition tagBlockDefinition)
        {
            //Create Namespace
            CodeNamespace tagsCodeNamespace = new CodeNamespace(TagsNamespace);

            //Add imports
            tagsCodeNamespace.Imports.Add(new CodeNamespaceImport(TypesNamespace));
            tagsCodeNamespace.Imports.Add(new CodeNamespaceImport(HaloLibraryNamespace));
            tagsCodeNamespace.Imports.Add(new CodeNamespaceImport(SystemNamespace));
            tagsCodeNamespace.Imports.Add(new CodeNamespaceImport(IONamespace));

            //Get valid name
            blockName = tagBlockDefinition.Name.ToPascalCasedMember(membersList);
            membersList.Add(blockName);
            blockNameDictionary.Add(tagBlockDefinition, blockName);

            //Create declaration
            tagBlockCodeTypeDeclaration = new CodeFieldsContainerTypeDeclaration(guerilla, blockName, tagBlockDefinition.GetLatestFieldDefinitions())
            {
                TypeAttributes = System.Reflection.TypeAttributes.Public | System.Reflection.TypeAttributes.Sealed,
                IsClass = true
            };
            tagBlockCodeTypeDeclaration.BaseTypes.Add(new CodeTypeReference(typeof(Tags.AbideTagBlock).Name));

            //Create Size property
            CodeMemberProperty sizeCodeMemberProperty = new CodeMemberProperty
            {
                Attributes = MemberAttributes.Public | MemberAttributes.Override,
                Name = "Size",
                Type = new CodeTypeReference(typeof(int))
            };
            sizeCodeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(tagBlockDefinition.GetLatestFieldSet().Size)));
            tagBlockCodeTypeDeclaration.Members.Add(sizeCodeMemberProperty);

            //Create field set attribute
            CodeAttributeDeclaration fieldSetCodeAttributeDeclaration = new CodeAttributeDeclaration(new CodeAttributeTypeReference<Tags.FieldSetAttribute>());
            fieldSetCodeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(tagBlockDefinition.GetLatestFieldSet().Size)));
            fieldSetCodeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(tagBlockDefinition.GetLatestFieldSet().Alignment)));
            
            //Add field set attribute
            tagBlockCodeTypeDeclaration.CustomAttributes.Add(fieldSetCodeAttributeDeclaration);
            
            //Add type to namespace
            tagsCodeNamespace.Types.Add(tagBlockCodeTypeDeclaration);

            //Add namespace
            Namespaces.Add(tagsCodeNamespace);
        }
    }
}
