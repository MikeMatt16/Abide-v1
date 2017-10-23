using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(120, 4)]
	public unsafe struct SquadsBlock
	{
		public enum Flags2Options
		{
			Unused_0 = 1,
			NeverSearch_1 = 2,
			StartTimerImmediately_2 = 4,
			NoTimerDelayForever_3 = 8,
			MagicSightAfterTimer_4 = 16,
			AutomaticMigration_5 = 32,
			DEPRECATED_6 = 64,
			RespawnEnabled_7 = 128,
			Blind_8 = 256,
			Deaf_9 = 512,
			Braindead_10 = 1024,
			_3dFiringPositions_11 = 2048,
			InitiallyPlaced_12 = 4096,
			UnitsNotEnterableByPlayer_13 = 8192,
		}
		public enum Team3Options
		{
			Default_0 = 0,
			Player_1 = 1,
			Human_2 = 2,
			Covenant_3 = 3,
			Flood_4 = 4,
			Sentinel_5 = 5,
			Heretic_6 = 6,
			Prophet_7 = 7,
			Unused8_8 = 8,
			Unused9_9 = 9,
			Unused10_10 = 10,
			Unused11_11 = 11,
			Unused12_12 = 12,
			Unused13_13 = 13,
			Unused14_14 = 14,
			Unused15_15 = 15,
		}
		public enum MajorUpgrade8Options
		{
			Normal_0 = 0,
			Few_1 = 1,
			Many_2 = 2,
			None_3 = 3,
			All_4 = 4,
		}
		public enum GrenadeType18Options
		{
			NONE_0 = 0,
			HumanGrenade_1 = 1,
			CovenantPlasma_2 = 2,
		}
		[Field("name^", null)]
		public String Name1;
		[Field("flags", typeof(Flags2Options))]
		public int Flags2;
		[Field("team", typeof(Team3Options))]
		public short Team3;
		[Field("parent", null)]
		public short Parent4;
		[Field("squad delay time:seconds", null)]
		public float SquadDelayTime5;
		[Field("normal diff count#initial number of actors on normal difficulty", null)]
		public short NormalDiffCount6;
		[Field("insane diff count#initial number of actors on insane difficulty (hard difficulty is midway between normal and insane)", null)]
		public short InsaneDiffCount7;
		[Field("major upgrade", typeof(MajorUpgrade8Options))]
		public short MajorUpgrade8;
		[Field("", null)]
		public fixed byte _9[2];
		[Field("", null)]
		public fixed byte _10[12];
		[Field("vehicle type", null)]
		public short VehicleType12;
		[Field("character type", null)]
		public short CharacterType13;
		[Field("initial zone", null)]
		public short InitialZone14;
		[Field("", null)]
		public fixed byte _15[2];
		[Field("initial weapon", null)]
		public short InitialWeapon16;
		[Field("initial secondary weapon", null)]
		public short InitialSecondaryWeapon17;
		[Field("grenade type", typeof(GrenadeType18Options))]
		public short GrenadeType18;
		[Field("initial order", null)]
		public short InitialOrder19;
		[Field("vehicle variant", null)]
		public StringId VehicleVariant20;
		[Field("", null)]
		public fixed byte _21[8];
		[Field("starting locations", null)]
		[Block("Actor Starting Locations Block", 32, typeof(ActorStartingLocationsBlock))]
		public TagBlock StartingLocations22;
		[Field("Placement script", null)]
		public String PlacementScript23;
		[Field("", null)]
		public fixed byte _24[2];
		[Field("", null)]
		public fixed byte _25[2];
	}
}
#pragma warning restore CS1591
