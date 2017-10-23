using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("user_interface_globals_definition", "wgtz", "����", typeof(UserInterfaceGlobalsDefinitionBlock))]
	[FieldSet(60, 4)]
	public unsafe struct UserInterfaceGlobalsDefinitionBlock
	{
		[Field("shared globals", null)]
		public TagReference SharedGlobals1;
		[Field("screen widgets", null)]
		[Block("User Interface Widget Reference Block", 256, typeof(UserInterfaceWidgetReferenceBlock))]
		public TagBlock ScreenWidgets3;
		[Field("mp variant settings ui", null)]
		public TagReference MpVariantSettingsUi5;
		[Field("game hopper descriptions", null)]
		public TagReference GameHopperDescriptions7;
	}
}
#pragma warning restore CS1591
