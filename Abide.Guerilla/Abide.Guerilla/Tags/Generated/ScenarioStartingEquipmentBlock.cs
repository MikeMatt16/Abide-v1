using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(204, 4)]
	public unsafe struct ScenarioStartingEquipmentBlock
	{
		public enum Flags0Options
		{
			NoGrenades_0 = 1,
			PlasmaGrenades_1 = 2,
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
		public fixed byte _5[48];
		[Field("Item Collection 1", null)]
		public TagReference ItemCollection16;
		[Field("Item Collection 2", null)]
		public TagReference ItemCollection27;
		[Field("Item Collection 3", null)]
		public TagReference ItemCollection38;
		[Field("Item Collection 4", null)]
		public TagReference ItemCollection49;
		[Field("Item Collection 5", null)]
		public TagReference ItemCollection510;
		[Field("Item Collection 6", null)]
		public TagReference ItemCollection611;
		[Field("", null)]
		public fixed byte _12[48];
	}
}
#pragma warning restore CS1591
