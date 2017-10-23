using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("damage_effect", "jpt!", "����", typeof(DamageEffectBlock))]
	[FieldSet(212, 4)]
	public unsafe struct DamageEffectBlock
	{
		public enum Flags2Options
		{
			DonTScaleDamageByDistance_0 = 1,
			AreaDamagePlayersOnlyAreaOfEffectDamageOnlyAffectsPlayers_1 = 2,
		}
		public enum SideEffect4Options
		{
			None_0 = 0,
			Harmless_1 = 1,
			LethalToTheUnsuspecting_2 = 2,
			Emp_3 = 3,
		}
		public enum Category5Options
		{
			None_0 = 0,
			Falling_1 = 1,
			Bullet_2 = 2,
			Grenade_3 = 3,
			HighExplosive_4 = 4,
			Sniper_5 = 5,
			Melee_6 = 6,
			Flame_7 = 7,
			MountedWeapon_8 = 8,
			Vehicle_9 = 9,
			Plasma_10 = 10,
			Needle_11 = 11,
			Shotgun_12 = 12,
		}
		public enum Flags6Options
		{
			DoesNotHurtOwner_0 = 1,
			CanCauseHeadshots_1 = 2,
			PingsResistantUnits_2 = 4,
			DoesNotHurtFriends_3 = 8,
			DoesNotPingUnits_4 = 16,
			DetonatesExplosives_5 = 32,
			OnlyHurtsShields_6 = 64,
			CausesFlamingDeath_7 = 128,
			DamageIndicatorsAlwaysPointDown_8 = 256,
			SkipsShields_9 = 512,
			OnlyHurtsOneInfectionForm_10 = 1024,
			ObsoleteWasCanCauseMultiplayerHeadshots_11 = 2048,
			InfectionFormPop_12 = 4096,
			IgnoreSeatScaleForDirDmg_13 = 8192,
			ForcesHardPing_14 = 16384,
			DoesNotHurtPlayers_15 = 32768,
		}
		public enum FadeFunction33Options
		{
			Linear_0 = 0,
			Late_1 = 1,
			VeryLate_2 = 2,
			Early_3 = 3,
			VeryEarly_4 = 4,
			Cosine_5 = 5,
			Zero_6 = 6,
			One_7 = 7,
		}
		public enum FalloffFunctionAFunctionToEnvelopeTheEffectSMagnitudeOverTime42Options
		{
			Linear_0 = 0,
			Late_1 = 1,
			VeryLate_2 = 2,
			Early_3 = 3,
			VeryEarly_4 = 4,
			Cosine_5 = 5,
			Zero_6 = 6,
			One_7 = 7,
		}
		public enum WobbleFunctionAFunctionToPerturbTheEffectSBehaviorOverTime47Options
		{
			One_0 = 0,
			Zero_1 = 1,
			Cosine_2 = 2,
			CosineVariablePeriod_3 = 3,
			DiagonalWave_4 = 4,
			DiagonalWaveVariablePeriod_5 = 5,
			Slide_6 = 6,
			SlideVariablePeriod_7 = 7,
			Noise_8 = 8,
			Jitter_9 = 9,
			Wander_10 = 10,
			Spark_11 = 11,
		}
		[Field("radius:world units", null)]
		public FloatBounds Radius0;
		[Field("cutoff scale:[0,1]", null)]
		public float CutoffScale1;
		[Field("flags", typeof(Flags2Options))]
		public int Flags2;
		[Field("side effect", typeof(SideEffect4Options))]
		public short SideEffect4;
		[Field("category", typeof(Category5Options))]
		public short Category5;
		[Field("flags", typeof(Flags6Options))]
		public int Flags6;
		[Field("AOE core radius:world units#if this is area of effect damage", null)]
		public float AOECoreRadius7;
		[Field("damage lower bound", null)]
		public float DamageLowerBound8;
		[Field("damage upper bound", null)]
		public FloatBounds DamageUpperBound9;
		[Field("dmg inner cone angle", null)]
		public float DmgInnerConeAngle10;
		[Field("blah", typeof(DamageOuterConeAngleStructBlock))]
		[Block("Damage Outer Cone Angle Struct", 1, typeof(DamageOuterConeAngleStructBlock))]
		public DamageOuterConeAngleStructBlock Blah11;
		[Field("active camouflage damage:[0,1]#how much more visible this damage makes a player who is active camouflaged", null)]
		public float ActiveCamouflageDamage12;
		[Field("stun:[0,1]#amount of stun added to damaged unit", null)]
		public float Stun13;
		[Field("maximum stun:[0,1]#damaged unit's stun will never exceed this amount", null)]
		public float MaximumStun14;
		[Field("stun time:seconds#duration of stun due to this damage", null)]
		public float StunTime15;
		[Field("", null)]
		public fixed byte _16[4];
		[Field("instantaneous acceleration:[0,+inf]", null)]
		public float InstantaneousAcceleration17;
		[Field("", null)]
		public fixed byte _18[4];
		[Field("", null)]
		public fixed byte _19[4];
		[Field("rider direct damage scale", null)]
		public float RiderDirectDamageScale20;
		[Field("rider maximum transfer damage scale", null)]
		public float RiderMaximumTransferDamageScale21;
		[Field("rider minimum transfer damage scale", null)]
		public float RiderMinimumTransferDamageScale22;
		[Field("", null)]
		public fixed byte _23[140];
		[Field("general_damage", null)]
		public StringId GeneralDamage24;
		[Field("specific_damage", null)]
		public StringId SpecificDamage25;
		[Field("AI stun radius:world units", null)]
		public float AIStunRadius26;
		[Field("AI stun bounds:(0-1)", null)]
		public FloatBounds AIStunBounds27;
		[Field("shake radius", null)]
		public float ShakeRadius28;
		[Field("EMP radius", null)]
		public float EMPRadius29;
		[Field("player responses", null)]
		[Block("Damage Effect Player Response Block", 2, typeof(DamageEffectPlayerResponseBlock))]
		public TagBlock PlayerResponses30;
		[Field("duration:seconds", null)]
		public float Duration32;
		[Field("fade function", typeof(FadeFunction33Options))]
		public short FadeFunction33;
		[Field("", null)]
		public fixed byte _34[2];
		[Field("rotation:degrees", null)]
		public float Rotation35;
		[Field("pushback:world units", null)]
		public float Pushback36;
		[Field("jitter:world units", null)]
		public FloatBounds Jitter37;
		[Field("", null)]
		public fixed byte _38[4];
		[Field("", null)]
		public fixed byte _39[24];
		[Field("duration:seconds#the effect will last for this duration.", null)]
		public float Duration41;
		[Field("falloff function#a function to envelope the effect's magnitude over time", typeof(FalloffFunctionAFunctionToEnvelopeTheEffectSMagnitudeOverTime42Options))]
		public short FalloffFunction42;
		[Field("", null)]
		public fixed byte _43[2];
		[Field("random translation:world units#random translation in all directions", null)]
		public float RandomTranslation44;
		[Field("random rotation:degrees#random rotation in all directions", null)]
		public float RandomRotation45;
		[Field("", null)]
		public fixed byte _46[12];
		[Field("wobble function#a function to perturb the effect's behavior over time", typeof(WobbleFunctionAFunctionToPerturbTheEffectSBehaviorOverTime47Options))]
		public short WobbleFunction47;
		[Field("", null)]
		public fixed byte _48[2];
		[Field("wobble function period:seconds", null)]
		public float WobbleFunctionPeriod49;
		[Field("wobble weight#a value of 0.0 signifies that the wobble function has no effect; a value of 1.0 signifies that the effect will not be felt when the wobble function's value is zero.", null)]
		public float WobbleWeight50;
		[Field("", null)]
		public fixed byte _51[4];
		[Field("", null)]
		public fixed byte _52[20];
		[Field("", null)]
		public fixed byte _53[8];
		[Field("sound", null)]
		public TagReference Sound55;
		[Field("", null)]
		public fixed byte _56[112];
		[Field("forward velocity:world units per second", null)]
		public float ForwardVelocity58;
		[Field("forward radius:world units", null)]
		public float ForwardRadius59;
		[Field("forward exponent", null)]
		public float ForwardExponent60;
		[Field("", null)]
		public fixed byte _61[12];
		[Field("outward velocity:world units per second", null)]
		public float OutwardVelocity62;
		[Field("outward radius:world units", null)]
		public float OutwardRadius63;
		[Field("outward exponent", null)]
		public float OutwardExponent64;
		[Field("", null)]
		public fixed byte _65[12];
	}
}
#pragma warning restore CS1591
