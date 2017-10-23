using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(128, 4)]
	public unsafe struct PoweredMassPointBlock
	{
		public enum Flags1Options
		{
			GroundFriction_0 = 1,
			WaterFriction_1 = 2,
			AirFriction_2 = 4,
			WaterLift_3 = 8,
			AirLift_4 = 16,
			Thrust_5 = 32,
			Antigrav_6 = 64,
			GetsDamageFromRegion_7 = 128,
		}
		[Field("name^", null)]
		public String Name0;
		[Field("flags", typeof(Flags1Options))]
		public int Flags1;
		[Field("antigrav strength", null)]
		public float AntigravStrength2;
		[Field("antigrav offset", null)]
		public float AntigravOffset3;
		[Field("antigrav height", null)]
		public float AntigravHeight4;
		[Field("antigrav damp fraction", null)]
		public float AntigravDampFraction5;
		[Field("antigrav normal k1", null)]
		public float AntigravNormalK16;
		[Field("antigrav normal k0", null)]
		public float AntigravNormalK07;
		[Field("damage source region name", null)]
		public StringId DamageSourceRegionName8;
		[Field("", null)]
		public fixed byte _9[64];
	}
}
#pragma warning restore CS1591
