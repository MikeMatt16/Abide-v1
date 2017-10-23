using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(224, 4)]
	public unsafe struct CharacterWeaponsBlock
	{
		public enum WeaponsFlags0Options
		{
			BurstingInhibitsMovement_0 = 1,
			MustCrouchToShoot_1 = 2,
			UseExtendedSafeToSaveRange_2 = 4,
		}
		public enum SpecialFireModeTheTypeOfSpecialWeaponFireThatWeCanUse27Options
		{
			None_0 = 0,
			Overcharge_1 = 1,
			SecondaryTrigger_2 = 2,
		}
		public enum SpecialFireSituationWhenWeWillDecideToUseOurSpecialWeaponFireMode28Options
		{
			Never_0 = 0,
			EnemyVisible_1 = 1,
			EnemyOutOfSight_2 = 2,
			Strafing_3 = 3,
		}
		[Field("weapons flags", typeof(WeaponsFlags0Options))]
		public int WeaponsFlags0;
		[Field("weapon^", null)]
		public TagReference Weapon1;
		[Field("", null)]
		public fixed byte _2[24];
		[Field("maximum firing range:world units#we can only fire our weapon at targets within this distance", null)]
		public float MaximumFiringRange4;
		[Field("minimum firing range#weapon will not be fired at target closer than given distance", null)]
		public float MinimumFiringRange5;
		[Field("normal combat range:world units", null)]
		public FloatBounds NormalCombatRange6;
		[Field("bombardment range#we offset our burst targets randomly by this range when firing at non-visible enemies (zero = never)", null)]
		public float BombardmentRange7;
		[Field("Max special target distance:world units#Specific target regions on a vehicle or unit will be fired upon only under the given distance", null)]
		public float MaxSpecialTargetDistance8;
		[Field("timid combat range:world units", null)]
		public FloatBounds TimidCombatRange9;
		[Field("aggressive combat range:world units", null)]
		public FloatBounds AggressiveCombatRange10;
		[Field("super-ballistic range#we try to aim our shots super-ballistically if target is outside this range (zero = never)", null)]
		public float SuperBallisticRange12;
		[Field("Ballistic firing bounds:world units#At the min range, the min ballistic fraction is used, at the max, the max ballistic fraction is used", null)]
		public FloatBounds BallisticFiringBounds13;
		[Field("Ballistic fraction bounds:[0-1]#Controls speed and degree of arc. 0 = high, slow, 1 = low, fast", null)]
		public FloatBounds BallisticFractionBounds14;
		[Field("", null)]
		public fixed byte _15[24];
		[Field("first burst delay time:seconds", null)]
		public FloatBounds FirstBurstDelayTime17;
		[Field("surprise delay time:seconds", null)]
		public float SurpriseDelayTime18;
		[Field("surprise fire-wildly time:seconds", null)]
		public float SurpriseFireWildlyTime19;
		[Field("death fire-wildly chance:[0,1]", null)]
		public float DeathFireWildlyChance20;
		[Field("death fire-wildly time:seconds", null)]
		public float DeathFireWildlyTime21;
		[Field("", null)]
		public fixed byte _22[12];
		[Field("custom stand gun offset#custom standing gun offset for overriding the default in the base actor", null)]
		public Vector3 CustomStandGunOffset23;
		[Field("custom crouch gun offset#custom crouching gun offset for overriding the default in the base actor", null)]
		public Vector3 CustomCrouchGunOffset24;
		[Field("", null)]
		public fixed byte _25[12];
		[Field("special-fire mode#the type of special weapon fire that we can use", typeof(SpecialFireModeTheTypeOfSpecialWeaponFireThatWeCanUse27Options))]
		public short SpecialFireMode27;
		[Field("special-fire situation#when we will decide to use our special weapon fire mode", typeof(SpecialFireSituationWhenWeWillDecideToUseOurSpecialWeaponFireMode28Options))]
		public short SpecialFireSituation28;
		[Field("special-fire chance:[0,1]#how likely we are to use our special weapon fire mode", null)]
		public float SpecialFireChance29;
		[Field("special-fire delay:seconds#how long we must wait between uses of our special weapon fire mode", null)]
		public float SpecialFireDelay30;
		[Field("special damage modifier:[0,1]#damage modifier for special weapon fire (applied in addition to the normal damage modifier. zero = no change)", null)]
		public float SpecialDamageModifier31;
		[Field("special projectile error:degrees#projectile error angle for special weapon fire (applied in addition to the normal error)", null)]
		public float SpecialProjectileError32;
		[Field("", null)]
		public fixed byte _33[24];
		[Field("drop weapon loaded#amount of ammo loaded into the weapon that we drop (in fractions of a clip, e.g. 0.3 to 0.5)", null)]
		public FloatBounds DropWeaponLoaded35;
		[Field("drop weapon ammo#total number of rounds in the weapon that we drop (ignored for energy weapons)", null)]
		public FloatBounds DropWeaponAmmo36;
		[Field("", null)]
		public fixed byte _37[24];
		[Field("normal accuracy bounds#Indicates starting and ending accuracies at normal difficulty", null)]
		public FloatBounds NormalAccuracyBounds39;
		[Field("normal accuracy time#The amount of time it takes the accuracy to go from starting to ending", null)]
		public float NormalAccuracyTime40;
		[Field("", null)]
		public fixed byte _41[4];
		[Field("heroic accuracy bounds#Indicates starting and ending accuracies at heroic difficulty", null)]
		public FloatBounds HeroicAccuracyBounds42;
		[Field("heroic accuracy time#The amount of time it takes the accuracy to go from starting to ending", null)]
		public float HeroicAccuracyTime43;
		[Field("", null)]
		public fixed byte _44[4];
		[Field("legendary accuracy bounds#Indicates starting and ending accuracies at legendary difficulty", null)]
		public FloatBounds LegendaryAccuracyBounds45;
		[Field("legendary accuracy time#The amount of time it takes the accuracy to go from starting to ending", null)]
		public float LegendaryAccuracyTime46;
		[Field("", null)]
		public fixed byte _47[4];
		[Field("", null)]
		public fixed byte _48[48];
		[Field("firing patterns", null)]
		[Block("Character Firing Pattern Block", 2, typeof(CharacterFiringPatternBlock))]
		public TagBlock FiringPatterns49;
		[Field("weapon melee damage", null)]
		public TagReference WeaponMeleeDamage50;
	}
}
#pragma warning restore CS1591
