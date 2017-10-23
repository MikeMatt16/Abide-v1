using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("equipment", "eqip", "item", typeof(EquipmentBlock))]
	[FieldSet(24, 4)]
	public unsafe struct EquipmentBlock
	{
		public enum PowerupType0Options
		{
			None_0 = 0,
			DoubleSpeed_1 = 1,
			OverShield_2 = 2,
			ActiveCamouflage_3 = 3,
			FullSpectrumVision_4 = 4,
			Health_5 = 5,
			Grenade_6 = 6,
		}
		public enum GrenadeType1Options
		{
			HumanFragmentation_0 = 0,
			CovenantPlasma_1 = 1,
		}
		[Field("powerup type", typeof(PowerupType0Options))]
		public short PowerupType0;
		[Field("grenade type", typeof(GrenadeType1Options))]
		public short GrenadeType1;
		[Field("powerup time:seconds", null)]
		public float PowerupTime2;
		[Field("pickup sound", null)]
		public TagReference PickupSound3;
		[Field("", null)]
		public fixed byte _4[144];
	}
}
#pragma warning restore CS1591
