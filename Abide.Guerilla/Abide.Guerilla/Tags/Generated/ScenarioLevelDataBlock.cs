using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(40, 4)]
	public unsafe struct ScenarioLevelDataBlock
	{
		[Field("Level Description", null)]
		public TagReference LevelDescription0;
		[Field("Campaign Level Data", null)]
		[Block("Global Ui Campaign Level Block", 20, typeof(GlobalUiCampaignLevelBlock))]
		public TagBlock CampaignLevelData1;
		[Field("Multiplayer", null)]
		[Block("Global Ui Multiplayer Level Block", 50, typeof(GlobalUiMultiplayerLevelBlock))]
		public TagBlock Multiplayer2;
	}
}
#pragma warning restore CS1591
