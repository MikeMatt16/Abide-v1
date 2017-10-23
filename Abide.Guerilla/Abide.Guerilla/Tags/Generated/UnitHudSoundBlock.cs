using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(56, 4)]
	public unsafe struct UnitHudSoundBlock
	{
		public enum LatchedTo1Options
		{
			ShieldRecharging_0 = 1,
			ShieldDamaged_1 = 2,
			ShieldLow_2 = 4,
			ShieldEmpty_3 = 8,
			HealthLow_4 = 16,
			HealthEmpty_5 = 32,
			HealthMinorDamage_6 = 64,
			HealthMajorDamage_7 = 128,
		}
		[Field("sound^", null)]
		public TagReference Sound0;
		[Field("latched to", typeof(LatchedTo1Options))]
		public int LatchedTo1;
		[Field("scale", null)]
		public float Scale2;
		[Field("", null)]
		public fixed byte _3[32];
	}
}
#pragma warning restore CS1591
