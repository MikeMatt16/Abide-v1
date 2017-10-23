using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(104, 4)]
	public unsafe struct PhantomTypesBlock
	{
		public enum Flags0Options
		{
			GeneratesEffects_0 = 1,
			UseAccAsForce_1 = 2,
			NegatesGravity_2 = 4,
			IgnoresPlayers_3 = 8,
			IgnoresNonplayers_4 = 16,
			IgnoresBipeds_5 = 32,
			IgnoresVehicles_6 = 64,
			IgnoresWeapons_7 = 128,
			IgnoresEquipment_8 = 256,
			IgnoresGarbage_9 = 512,
			IgnoresProjectiles_10 = 1024,
			IgnoresScenery_11 = 2048,
			IgnoresMachines_12 = 4096,
			IgnoresControls_13 = 8192,
			IgnoresLightFixtures_14 = 16384,
			IgnoresSoundScenery_15 = 32768,
			IgnoresCrates_16 = 65536,
			IgnoresCreatures_17 = 131072,
			__18 = 262144,
			__19 = 524288,
			__20 = 1048576,
			__21 = 2097152,
			__22 = 4194304,
			__23 = 8388608,
			LocalizesPhysics_24 = 16777216,
			DisableLinearDamping_25 = 33554432,
			DisableAngularDamping_26 = 67108864,
			IgnoresDeadBipeds_27 = 134217728,
		}
		public enum MinimumSize1Options
		{
			Default_0 = 0,
			Tiny_1 = 1,
			Small_2 = 2,
			Medium_3 = 3,
			Large_4 = 4,
			Huge_5 = 5,
			ExtraHuge_6 = 6,
		}
		public enum MaximumSize2Options
		{
			Default_0 = 0,
			Tiny_1 = 1,
			Small_2 = 2,
			Medium_3 = 3,
			Large_4 = 4,
			Huge_5 = 5,
			ExtraHuge_6 = 6,
		}
		[Field("flags", typeof(Flags0Options))]
		public int Flags0;
		[Field("minimum size", typeof(MinimumSize1Options))]
		public byte MinimumSize1;
		[Field("maximum size", typeof(MaximumSize2Options))]
		public byte MaximumSize2;
		[Field("", null)]
		public fixed byte _3[2];
		[Field("marker name#you don't need this if you're just generating effects.  If empty it defaults to the up of the object", null)]
		public StringId MarkerName4;
		[Field("alignment marker name#you don't need this if you're just generating effects.  If empty it defaults to \"marker name\"", null)]
		public StringId AlignmentMarkerName5;
		[Field("", null)]
		public fixed byte _7[8];
		[Field("hookes law e#0 if you don't want this to behave like spring.  1 is a good starting point if you do.", null)]
		public float HookesLawE8;
		[Field("linear dead radius#radius from linear motion origin in which acceleration is dead.", null)]
		public float LinearDeadRadius9;
		[Field("center acc", null)]
		public float CenterAcc10;
		[Field("center max vel", null)]
		public float CenterMaxVel11;
		[Field("axis acc", null)]
		public float AxisAcc12;
		[Field("axis max vel", null)]
		public float AxisMaxVel13;
		[Field("direction acc", null)]
		public float DirectionAcc14;
		[Field("direction max vel", null)]
		public float DirectionMaxVel15;
		[Field("", null)]
		public fixed byte _16[28];
		[Field("alignment hookes law e#0 if you don't want this to behave like spring.  1 is a good starting point if you do.", null)]
		public float AlignmentHookesLawE18;
		[Field("alignment acc", null)]
		public float AlignmentAcc19;
		[Field("alignment max vel", null)]
		public float AlignmentMaxVel20;
		[Field("", null)]
		public fixed byte _21[8];
	}
}
#pragma warning restore CS1591
