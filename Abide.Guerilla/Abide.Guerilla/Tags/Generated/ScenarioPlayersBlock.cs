using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(52, 4)]
	public unsafe struct ScenarioPlayersBlock
	{
		public enum TeamDesignator3Options
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
		public enum GameType15Options
		{
			NONE_0 = 0,
			CaptureTheFlag_1 = 1,
			Slayer_2 = 2,
			Oddball_3 = 3,
			KingOfTheHill_4 = 4,
			Race_5 = 5,
			Headhunter_6 = 6,
			Juggernaut_7 = 7,
			Territories_8 = 8,
			Stub_9 = 9,
			Ignored3_10 = 10,
			Ignored4_11 = 11,
			AllGameTypes_12 = 12,
			AllExceptCTF_13 = 13,
			AllExceptCTFRace_14 = 14,
		}
		public enum GameType26Options
		{
			NONE_0 = 0,
			CaptureTheFlag_1 = 1,
			Slayer_2 = 2,
			Oddball_3 = 3,
			KingOfTheHill_4 = 4,
			Race_5 = 5,
			Headhunter_6 = 6,
			Juggernaut_7 = 7,
			Territories_8 = 8,
			Stub_9 = 9,
			Ignored3_10 = 10,
			Ignored4_11 = 11,
			AllGameTypes_12 = 12,
			AllExceptCTF_13 = 13,
			AllExceptCTFRace_14 = 14,
		}
		public enum GameType37Options
		{
			NONE_0 = 0,
			CaptureTheFlag_1 = 1,
			Slayer_2 = 2,
			Oddball_3 = 3,
			KingOfTheHill_4 = 4,
			Race_5 = 5,
			Headhunter_6 = 6,
			Juggernaut_7 = 7,
			Territories_8 = 8,
			Stub_9 = 9,
			Ignored3_10 = 10,
			Ignored4_11 = 11,
			AllGameTypes_12 = 12,
			AllExceptCTF_13 = 13,
			AllExceptCTFRace_14 = 14,
		}
		public enum GameType48Options
		{
			NONE_0 = 0,
			CaptureTheFlag_1 = 1,
			Slayer_2 = 2,
			Oddball_3 = 3,
			KingOfTheHill_4 = 4,
			Race_5 = 5,
			Headhunter_6 = 6,
			Juggernaut_7 = 7,
			Territories_8 = 8,
			Stub_9 = 9,
			Ignored3_10 = 10,
			Ignored4_11 = 11,
			AllGameTypes_12 = 12,
			AllExceptCTF_13 = 13,
			AllExceptCTFRace_14 = 14,
		}
		public enum SpawnType09Options
		{
			Both_0 = 0,
			InitialSpawnOnly_1 = 1,
			RespawnOnly_2 = 2,
		}
		public enum SpawnType110Options
		{
			Both_0 = 0,
			InitialSpawnOnly_1 = 1,
			RespawnOnly_2 = 2,
		}
		public enum SpawnType211Options
		{
			Both_0 = 0,
			InitialSpawnOnly_1 = 1,
			RespawnOnly_2 = 2,
		}
		public enum SpawnType312Options
		{
			Both_0 = 0,
			InitialSpawnOnly_1 = 1,
			RespawnOnly_2 = 2,
		}
		public enum CampaignPlayerType15Options
		{
			Masterchief_0 = 0,
			Dervish_1 = 1,
			ChiefMultiplayer_2 = 2,
			EliteMultiplayer_3 = 3,
		}
		public Vector3 Position1;
		[Field("Facing:Degrees", null)]
		public float Facing2;
		[Field("Team Designator", typeof(TeamDesignator3Options))]
		public short TeamDesignator3;
		[Field("BSP Index", null)]
		public short BSPIndex4;
		[Field("Game Type 1", typeof(GameType15Options))]
		public short GameType15;
		[Field("Game Type 2", typeof(GameType26Options))]
		public short GameType26;
		[Field("Game Type 3", typeof(GameType37Options))]
		public short GameType37;
		[Field("Game Type 4", typeof(GameType48Options))]
		public short GameType48;
		[Field("Spawn Type 0", typeof(SpawnType09Options))]
		public short SpawnType09;
		[Field("Spawn Type 1", typeof(SpawnType110Options))]
		public short SpawnType110;
		[Field("Spawn Type 2", typeof(SpawnType211Options))]
		public short SpawnType211;
		[Field("Spawn Type 3", typeof(SpawnType312Options))]
		public short SpawnType312;
		[Field("EMPTY STRING", null)]
		public StringId EMPTYSTRING13;
		[Field("EMPTY STRING", null)]
		public StringId EMPTYSTRING14;
		[Field("Campaign Player Type", typeof(CampaignPlayerType15Options))]
		public short CampaignPlayerType15;
		[Field("", null)]
		public fixed byte _16[6];
	}
}
#pragma warning restore CS1591
