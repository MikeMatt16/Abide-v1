using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(32, 4)]
	public unsafe struct ScenarioNetpointsBlock
	{
		public enum Type3Options
		{
			CTFFlagSpawn_0 = 0,
			CTFFlagReturn_1 = 1,
			AssaultBombSpawn_2 = 2,
			AssaultBombReturn_3 = 3,
			OddballSpawn_4 = 4,
			Unused_5 = 5,
			RaceCheckpoint_6 = 6,
			TeleporterSrc_7 = 7,
			TeleporterDest_8 = 8,
			HeadhunterBin_9 = 9,
			TerritoriesFlag_10 = 10,
			KingHill0_11 = 11,
			KingHill1_12 = 12,
			KingHill2_13 = 13,
			KingHill3_14 = 14,
			KingHill4_15 = 15,
			KingHill5_16 = 16,
			KingHill6_17 = 17,
			KingHill7_18 = 18,
		}
		public enum TeamDesignator4Options
		{
			RedAlpha_0 = 0,
			BlueBravo_1 = 1,
			YellowCharlie_2 = 2,
			GreenDelta_3 = 3,
			PurpleEcho_4 = 4,
			OrangeFoxtrot_5 = 5,
			BrownGolf_6 = 6,
			PinkHotel_7 = 7,
			NEUTRAL_8 = 8,
		}
		public enum Flags6Options
		{
			MultipleFlagBomb_0 = 1,
			SingleFlagBomb_1 = 2,
			NeutralFlagBomb_2 = 4,
		}
		public Vector3 Position0;
		[Field("Facing:Degrees", null)]
		public float Facing1;
		[Field("Type", typeof(Type3Options))]
		public short Type3;
		[Field("Team Designator", typeof(TeamDesignator4Options))]
		public short TeamDesignator4;
		[Field("Identifier", null)]
		public short Identifier5;
		[Field("Flags", typeof(Flags6Options))]
		public short Flags6;
		[Field("EMPTY STRING", null)]
		public StringId EMPTYSTRING7;
		[Field("EMPTY STRING", null)]
		public StringId EMPTYSTRING8;
	}
}
#pragma warning restore CS1591
