using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(68, 4)]
	public unsafe struct GlobalDamageSectionBlock
	{
		public enum Flags2Options
		{
			AbsorbsBodyDamage_0 = 1,
			TakesFullDmgWhenObjectDies_1 = 2,
			CannotDieWithRiders_2 = 4,
			TakesFullDmgWhenObjDstryd_3 = 8,
			RestoredOnRessurection_4 = 16,
			Unused_5 = 32,
			Unused_6 = 64,
			Heatshottable_7 = 128,
			IgnoresShields_8 = 256,
		}
		[Field("name^", null)]
		public StringId Name0;
		[Field("flags", typeof(Flags2Options))]
		public int Flags2;
		[Field("vitality percentage:[0.1]#percentage of total object vitality", null)]
		public float VitalityPercentage3;
		[Field("instant responses", null)]
		[Block("Instantaneous Damage Repsonse Block", 16, typeof(InstantaneousDamageRepsonseBlock))]
		public TagBlock InstantResponses4;
		[Field("", null)]
		[Block("G Null Block", 0, typeof(GNullBlock))]
		public TagBlock _5;
		[Field("", null)]
		[Block("G Null Block", 0, typeof(GNullBlock))]
		public TagBlock _6;
		[Field("stun time:seconds", null)]
		public float StunTime7;
		[Field("recharge time:seconds", null)]
		public float RechargeTime8;
		[Field("", null)]
		public fixed byte _9[4];
		[Field("resurrection restored region name", null)]
		public StringId ResurrectionRestoredRegionName10;
		[Field("", null)]
		public fixed byte _11[4];
	}
}
#pragma warning restore CS1591
