using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(72, 4)]
	public unsafe struct CharacterChargeBlock
	{
		public enum ChargeFlags0Options
		{
			OffhandMeleeAllowed_0 = 1,
			BerserkWheneverCharge_1 = 2,
		}
		[Field("Charge flags", typeof(ChargeFlags0Options))]
		public int ChargeFlags0;
		[Field("melee consider range", null)]
		public float MeleeConsiderRange1;
		[Field("melee chance#chance of initiating a melee within a 1 second period", null)]
		public float MeleeChance2;
		[Field("melee attack range", null)]
		public float MeleeAttackRange3;
		[Field("melee abort range", null)]
		public float MeleeAbortRange4;
		[Field("melee attack timeout:seconds#Give up after given amount of time spent charging", null)]
		public float MeleeAttackTimeout5;
		[Field("melee attack delay timer:seconds#don't attempt again before given time since last melee", null)]
		public float MeleeAttackDelayTimer6;
		[Field("melee leap range", null)]
		public FloatBounds MeleeLeapRange7;
		[Field("melee leap chance", null)]
		public float MeleeLeapChance8;
		[Field("ideal leap velocity", null)]
		public float IdealLeapVelocity9;
		[Field("max leap velocity", null)]
		public float MaxLeapVelocity10;
		[Field("melee leap ballistic", null)]
		public float MeleeLeapBallistic11;
		[Field("melee delay timer:seconds#time between melee leaps", null)]
		public float MeleeDelayTimer12;
		[Field("", null)]
		public fixed byte _13[12];
		[Field("berserk weapon#when I berserk, I pull out a ...", null)]
		public TagReference BerserkWeapon14;
	}
}
#pragma warning restore CS1591
