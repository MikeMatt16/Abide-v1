using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(36, 4)]
	public unsafe struct UiLevelsDefinitionBlock
	{
		[Field("Campaigns", null)]
		[Block("Ui Campaign Block", 4, typeof(UiCampaignBlock))]
		public TagBlock Campaigns0;
		[Field("Campaign Levels", null)]
		[Block("Global Ui Campaign Level Block", 20, typeof(GlobalUiCampaignLevelBlock))]
		public TagBlock CampaignLevels1;
		[Field("Multiplayer Levels", null)]
		[Block("Global Ui Multiplayer Level Block", 50, typeof(GlobalUiMultiplayerLevelBlock))]
		public TagBlock MultiplayerLevels2;
	}
}
#pragma warning restore CS1591
