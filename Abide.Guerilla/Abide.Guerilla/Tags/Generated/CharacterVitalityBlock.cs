using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(112, 4)]
	public unsafe struct CharacterVitalityBlock
	{
		public enum VitalityFlags0Options
		{
			Unused_0 = 1,
		}
		[Field("vitality flags", typeof(VitalityFlags0Options))]
		public int VitalityFlags0;
		[Field("normal body vitality#maximum body vitality of our unit", null)]
		public float NormalBodyVitality1;
		[Field("normal shield vitality#maximum shield vitality of our unit", null)]
		public float NormalShieldVitality2;
		[Field("legendary body vitality#maximum body vitality of our unit (on legendary)", null)]
		public float LegendaryBodyVitality3;
		[Field("legendary shield vitality#maximum shield vitality of our unit (on legendary)", null)]
		public float LegendaryShieldVitality4;
		[Field("body recharge fraction#fraction of body health that can be regained after damage", null)]
		public float BodyRechargeFraction5;
		[Field("soft ping threshold (with shields)#damage necessary to trigger a soft ping when shields are up", null)]
		public float SoftPingThresholdWithShields6;
		[Field("soft ping threshold (no shields)#damage necessary to trigger a soft ping when shields are down", null)]
		public float SoftPingThresholdNoShields7;
		[Field("soft ping min interrupt time#minimum time before a soft ping can be interrupted", null)]
		public float SoftPingMinInterruptTime8;
		[Field("", null)]
		public fixed byte _9[4];
		[Field("hard ping threshold (with shields)#damage necessary to trigger a hard ping when shields are up", null)]
		public float HardPingThresholdWithShields10;
		[Field("hard ping threshold (no shields)#damage necessary to trigger a hard ping when shields are down", null)]
		public float HardPingThresholdNoShields11;
		[Field("hard ping min interrupt time#minimum time before a hard ping can be interrupted", null)]
		public float HardPingMinInterruptTime12;
		[Field("", null)]
		public fixed byte _13[4];
		[Field("current damage decay delay#current damage begins to fall after a time delay has passed since last the damage", null)]
		public float CurrentDamageDecayDelay14;
		[Field("current damage decay time#amount of time it would take for 100% current damage to decay to 0", null)]
		public float CurrentDamageDecayTime15;
		[Field("", null)]
		public fixed byte _16[8];
		[Field("recent damage decay delay#recent damage begins to fall after a time delay has passed since last the damage", null)]
		public float RecentDamageDecayDelay17;
		[Field("recent damage decay time#amount of time it would take for 100% recent damage to decay to 0", null)]
		public float RecentDamageDecayTime18;
		[Field("body recharge delay time#amount of time delay before a shield begins to recharge", null)]
		public float BodyRechargeDelayTime19;
		[Field("body recharge time#amount of time for shields to recharge completely", null)]
		public float BodyRechargeTime20;
		[Field("shield recharge delay time#amount of time delay before a shield begins to recharge", null)]
		public float ShieldRechargeDelayTime21;
		[Field("shield recharge time#amount of time for shields to recharge completely", null)]
		public float ShieldRechargeTime22;
		[Field("stun threshold#stun level that triggers the stunned state (currently, the 'stunned' behavior)", null)]
		public float StunThreshold23;
		[Field("stun time bounds:seconds", null)]
		public FloatBounds StunTimeBounds24;
		[Field("", null)]
		public fixed byte _25[16];
		[Field("extended shield damage threshold:%#Amount of shield damage sustained before it is considered 'extended'", null)]
		public float ExtendedShieldDamageThreshold26;
		[Field("extended body damage threshold:%#Amount of body damage sustained before it is considered 'extended'", null)]
		public float ExtendedBodyDamageThreshold27;
		[Field("", null)]
		public fixed byte _28[16];
		[Field("suicide radius#when I die and explode, I damage stuff within this distance of me.", null)]
		public float SuicideRadius29;
		[Field("", null)]
		public fixed byte _30[8];
	}
}
#pragma warning restore CS1591
