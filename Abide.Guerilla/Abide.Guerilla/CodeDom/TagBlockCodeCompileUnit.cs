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

            //Get valid name
            blockName = tagBlockDefinition.Name.ToPascalCasedMember(membersList);
            membersList.Add(blockName);
            blockNameDictionary.Add(tagBlockDefinition, blockName);

            //Create declaration
            tagBlockCodeTypeDeclaration = new CodeFieldsContainerTypeDeclaration(guerilla, blockName, tagBlockDefinition.GetLatestFieldDefinitions());
            tagBlockCodeTypeDeclaration.BaseTypes.Add(new CodeTypeReference(typeof(Tags.IReadable)));
            tagBlockCodeTypeDeclaration.BaseTypes.Add(new CodeTypeReference(typeof(Tags.IWritable)));
            tagBlockCodeTypeDeclaration.TypeAttributes = System.Reflection.TypeAttributes.Public | System.Reflection.TypeAttributes.Sealed;
            tagBlockCodeTypeDeclaration.IsClass = true;

            //Create Size property
            CodeMemberProperty sizeCodeMemberProperty = new CodeMemberProperty
            {
                Attributes = MemberAttributes.Public | MemberAttributes.Final,
                Name = "Size",
                Type = new CodeTypeReference(typeof(int))
            };
            sizeCodeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(tagBlockDefinition.GetLatestFieldSet().Size)));

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
            tagBlockCodeTypeDeclaration.Members.Add(sizeCodeMemberProperty);
            tagBlockCodeTypeDeclaration.Members.Add(initializeCodeMethod);
            tagBlockCodeTypeDeclaration.Members.Add(readCodeMemberMethod);
            tagBlockCodeTypeDeclaration.Members.Add(writeCodeMemberMethod);

            //Create field set attribute
            CodeAttributeDeclaration fieldSetCodeAttributeDeclaration = new CodeAttributeDeclaration(new CodeTypeReference(typeof(Tags.FieldSetAttribute)));
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
