using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct ScenarioSceneryDatumStructV4Block
	{
		public enum PathfindingPolicy1Options
		{
			TagDefault_0 = 0,
			PathfindingDYNAMIC_1 = 1,
			PathfindingCUTOUT_2 = 2,
			PathfindingSTATIC_3 = 3,
			PathfindingNONE_4 = 4,
		}
		public enum LightmappingPolicy2Options
		{
			TagDefault_0 = 0,
			Dynamic_1 = 1,
			PerVertex_2 = 2,
		}
		public enum ValidMultiplayerGames5Options
		{
			CaptureTheFlag_0 = 1,
			Slayer_1 = 2,
			Oddball_2 = 4,
			KingOfTheHill_3 = 8,
			Juggernaut_4 = 16,
			Territories_5 = 32,
			Assault_6 = 64,
		}
		[Field("Pathfinding Policy", typeof(PathfindingPolicy1Options))]
		public short PathfindingPolicy1;
		[Field("Lightmapping Policy", typeof(LightmappingPolicy2Options))]
		public short LightmappingPolicy2;
		[Field("Pathfinding References*", null)]
		[Block("Pathfinding Object Index List Block", 16, typeof(PathfindingObjectIndexListBlock))]
		public TagBlock PathfindingReferences3;
		[Field("", null)]
		public fixed byte _4[2];
		[Field("Valid Multiplayer Games", typeof(ValidMultiplayerGames5Options))]
		public short ValidMultiplayerGames5;
	}
}
#pragma warning restore CS1591
