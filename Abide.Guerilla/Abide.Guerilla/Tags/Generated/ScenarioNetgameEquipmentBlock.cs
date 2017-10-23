using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(152, 4)]
	public unsafe struct ScenarioNetgameEquipmentBlock
	{
		public enum Flags0Options
		{
			Levitate_0 = 1,
			DestroyExistingOnNewSpawn_1 = 2,
		}
		public enum GameType11Options
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
		public enum GameType22Options
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
		public enum GameType33Options
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
		public enum GameType44Options
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
		public enum RespawnTimerStarts8Options
		{
			OnPickUp_0 = 0,
			OnBodyDepletion_1 = 1,
		}
		public enum Classification9Options
		{
			Weapon_0 = 0,
			PrimaryLightLand_1 = 1,
			SecondaryLightLand_2 = 2,
			PrimaryHeavyLand_3 = 3,
			PrimaryFlying_4 = 4,
			SecondaryHeavyLand_5 = 5,
			PrimaryTurret_6 = 6,
			SecondaryTurret_7 = 7,
			Grenade_8 = 8,
			Powerup_9 = 9,
		}
		[Field("Flags", typeof(Flags0Options))]
		public int Flags0;
		[Field("Game Type 1", typeof(GameType11Options))]
		public short GameType11;
		[Field("Game Type 2", typeof(GameType22Options))]
		public short GameType22;
		[Field("Game Type 3", typeof(GameType33Options))]
		public short GameType33;
		[Field("Game Type 4", typeof(GameType44Options))]
		public short GameType44;
		[Field("", null)]
		public fixed byte _5[2];
		[Field("Spawn Time (in seconds, 0 = default)", null)]
		public short SpawnTimeInSeconds0Default6;
		[Field("Respawn on Empty Time:seconds", null)]
		public short RespawnOnEmptyTime7;
		[Field("Respawn Timer Starts", typeof(RespawnTimerStarts8Options))]
		public short RespawnTimerStarts8;
		[Field("Classification", typeof(Classification9Options))]
		public byte Classification9;
		[Field("", null)]
		public fixed byte _10[3];
		[Field("", null)]
		public fixed byte _11[40];
		public Vector3 Position12;
		[Field("Orientation", typeof(ScenarioNetgameEquipmentOrientationStructBlock))]
		[Block("Scenario Netgame Equipment Orientation Struct", 1, typeof(ScenarioNetgameEquipmentOrientationStructBlock))]
		public ScenarioNetgameEquipmentOrientationStructBlock Orientation13;
		[Field("Item/Vehicle Collection", null)]
		public TagReference ItemVehicleCollection14;
		[Field("", null)]
		public fixed byte _15[48];
	}
}
#pragma warning restore CS1591
