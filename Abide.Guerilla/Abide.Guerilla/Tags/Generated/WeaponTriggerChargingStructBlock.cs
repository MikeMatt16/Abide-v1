using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(52, 4)]
	public unsafe struct WeaponTriggerChargingStructBlock
	{
		public enum OverchargedAction3Options
		{
			None_0 = 0,
			Explode_1 = 1,
			Discharge_2 = 2,
		}
		[Field("charging time:seconds#the amount of time it takes for this trigger to become fully charged", null)]
		public float ChargingTime1;
		[Field("charged time:seconds#the amount of time this trigger can be charged before becoming overcharged", null)]
		public float ChargedTime2;
		[Field("overcharged action", typeof(OverchargedAction3Options))]
		public short OverchargedAction3;
		[Field("", null)]
		public fixed byte _4[2];
		[Field("charged illumination:[0,1]#the amount of illumination given off when the weapon is fully charged", null)]
		public float ChargedIllumination5;
		[Field("spew time:seconds#length of time the weapon will spew (fire continuously) while discharging", null)]
		public float SpewTime6;
		[Field("charging effect#the charging effect is created once when the trigger begins to charge", null)]
		public TagReference ChargingEffect7;
		[Field("charging damage effect#the charging effect is created once when the trigger begins to charge", null)]
		public TagReference ChargingDamageEffect8;
	}
}
#pragma warning restore CS1591
