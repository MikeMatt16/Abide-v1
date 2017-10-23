using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(80, 4)]
	public unsafe struct WeaponTriggers
	{
		public enum Flags0Options
		{
			AutofireSingleActionOnly_0 = 1,
		}
		public enum Input1Options
		{
			RightTrigger_0 = 0,
			LeftTrigger_1 = 1,
			MeleeAttack_2 = 2,
		}
		public enum Behavior2Options
		{
			Spew_0 = 0,
			Latch_1 = 1,
			LatchAutofire_2 = 2,
			Charge_3 = 3,
			LatchZoom_4 = 4,
			LatchRocketlauncher_5 = 5,
		}
		public enum Prediction5Options
		{
			None_0 = 0,
			Spew_1 = 1,
			Charge_2 = 2,
		}
		[Field("flags", typeof(Flags0Options))]
		public int Flags0;
		[Field("input", typeof(Input1Options))]
		public short Input1;
		[Field("behavior", typeof(Behavior2Options))]
		public short Behavior2;
		[Field("primary barrel", null)]
		public short PrimaryBarrel3;
		[Field("secondary barrel", null)]
		public short SecondaryBarrel4;
		[Field("prediction", typeof(Prediction5Options))]
		public short Prediction5;
		[Field("", null)]
		public fixed byte _6[2];
		[Field("", null)]
		public fixed byte _7[28];
		[Field("autofire", typeof(WeaponTriggerAutofireStructBlock))]
		[Block("Weapon Trigger Autofire Struct", 1, typeof(WeaponTriggerAutofireStructBlock))]
		public WeaponTriggerAutofireStructBlock Autofire8;
		[Field("charging", typeof(WeaponTriggerChargingStructBlock))]
		[Block("Weapon Trigger Charging Struct", 1, typeof(WeaponTriggerChargingStructBlock))]
		public WeaponTriggerChargingStructBlock Charging9;
	}
}
#pragma warning restore CS1591
