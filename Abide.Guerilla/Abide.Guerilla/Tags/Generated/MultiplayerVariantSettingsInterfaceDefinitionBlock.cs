using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("multiplayer_variant_settings_interface_definition", "goof", "����", typeof(MultiplayerVariantSettingsInterfaceDefinitionBlock))]
	[FieldSet(472, 4)]
	public unsafe struct MultiplayerVariantSettingsInterfaceDefinitionBlock
	{
		[Field("", null)]
		public TagReference _0;
		[Field("", null)]
		public TagReference _1;
		[Field("", null)]
		public TagReference _2;
		[Field("game engine settings", null)]
		[Block("Variant Setting Edit Reference Block", 40, typeof(VariantSettingEditReferenceBlock))]
		public TagBlock GameEngineSettings3;
		[Field("default variant strings", null)]
		public TagReference DefaultVariantStrings4;
		[Field("default variants", null)]
		[Block("G Default Variants Block", 100, typeof(GDefaultVariantsBlock))]
		public TagBlock DefaultVariants5;
		[Field("", typeof(CreateNewVariantStructBlock))]
		[Block("Create New Variant Struct", 1, typeof(CreateNewVariantStructBlock))]
		public CreateNewVariantStructBlock _7;
		[Field("", typeof(CreateNewVariantStructBlock))]
		[Block("Create New Variant Struct", 1, typeof(CreateNewVariantStructBlock))]
		public CreateNewVariantStructBlock _9;
		[Field("", typeof(CreateNewVariantStructBlock))]
		[Block("Create New Variant Struct", 1, typeof(CreateNewVariantStructBlock))]
		public CreateNewVariantStructBlock _11;
		[Field("", typeof(CreateNewVariantStructBlock))]
		[Block("Create New Variant Struct", 1, typeof(CreateNewVariantStructBlock))]
		public CreateNewVariantStructBlock _14;
		[Field("", typeof(CreateNewVariantStructBlock))]
		[Block("Create New Variant Struct", 1, typeof(CreateNewVariantStructBlock))]
		public CreateNewVariantStructBlock _16;
		[Field("", typeof(CreateNewVariantStructBlock))]
		[Block("Create New Variant Struct", 1, typeof(CreateNewVariantStructBlock))]
		public CreateNewVariantStructBlock _18;
		[Field("", typeof(CreateNewVariantStructBlock))]
		[Block("Create New Variant Struct", 1, typeof(CreateNewVariantStructBlock))]
		public CreateNewVariantStructBlock _21;
		[Field("", typeof(CreateNewVariantStructBlock))]
		[Block("Create New Variant Struct", 1, typeof(CreateNewVariantStructBlock))]
		public CreateNewVariantStructBlock _23;
		[Field("", typeof(CreateNewVariantStructBlock))]
		[Block("Create New Variant Struct", 1, typeof(CreateNewVariantStructBlock))]
		public CreateNewVariantStructBlock _25;
		[Field("", typeof(CreateNewVariantStructBlock))]
		[Block("Create New Variant Struct", 1, typeof(CreateNewVariantStructBlock))]
		public CreateNewVariantStructBlock _28;
	}
}
#pragma warning restore CS1591
