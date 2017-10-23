using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("projectile", "proj", "obje", typeof(ProjectileBlock))]
	[FieldSet(348, 4)]
	public unsafe struct ProjectileBlock
	{
		public enum Flags1Options
		{
			OrientedAlongVelocity_0 = 1,
			AIMustUseBallisticAiming_1 = 2,
			DetonationMaxTimeIfAttached_2 = 4,
			HasSuperCombiningExplosion_3 = 8,
			DamageScalesBasedOnDistance_4 = 16,
			TravelsInstantaneously_5 = 32,
			SteeringAdjustsOrientation_6 = 64,
			DonTNoiseUpSteering_7 = 128,
			CanTrackBehindItself_8 = 256,
			ROBOTRONSTEERING_9 = 512,
			FasterWhenOwnedByPlayer_10 = 1024,
		}
		public enum DetonationTimerStarts2Options
		{
			Immediately_0 = 0,
			AfterFirstBounce_1 = 1,
			WhenAtRest_2 = 2,
			AfterFirstBounceOffAnySurface_3 = 3,
		}
		public enum ImpactNoise3Options
		{
			Silent_0 = 0,
			Medium_1 = 1,
			Loud_2 = 2,
			Shout_3 = 3,
			Quiet_4 = 4,
		}
		public enum DetonationNoise13Options
		{
			Silent_0 = 0,
			Medium_1 = 1,
			Loud_2 = 2,
			Shout_3 = 3,
			Quiet_4 = 4,
		}
		public enum DamageReportingType23Options
		{
			TehGuardians11_0 = 0,
			FallingDamage_1 = 1,
			GenericCollisionDamage_2 = 2,
			GenericMeleeDamage_3 = 3,
			GenericExplosion_4 = 4,
			MagnumPistol_5 = 5,
			PlasmaPistol_6 = 6,
			Needler_7 = 7,
			Smg_8 = 8,
			PlasmaRifle_9 = 9,
			BattleRifle_10 = 10,
			Carbine_11 = 11,
			Shotgun_12 = 12,
			SniperRifle_13 = 13,
			BeamRifle_14 = 14,
			RocketLauncher_15 = 15,
			FlakCannon_16 = 16,
			BruteShot_17 = 17,
			Disintegrator_18 = 18,
			BrutePlasmaRifle_19 = 19,
			EnergySword_20 = 20,
			FragGrenade_21 = 21,
			PlasmaGrenade_22 = 22,
			FlagMeleeDamage_23 = 23,
			BombMeleeDamage_24 = 24,
			BombExplosionDamage_25 = 25,
			BallMeleeDamage_26 = 26,
			HumanTurret_27 = 27,
			PlasmaTurret_28 = 28,
			Banshee_29 = 29,
			Ghost_30 = 30,
			Mongoose_31 = 31,
			Scorpion_32 = 32,
			SpectreDriver_33 = 33,
			SpectreGunner_34 = 34,
			WarthogDriver_35 = 35,
			WarthogGunner_36 = 36,
			Wraith_37 = 37,
			Tank_38 = 38,
			SentinelBeam_39 = 39,
			SentinelRpg_40 = 40,
			Teleporter_41 = 41,
		}
		[Field("flags", typeof(Flags1Options))]
		public int Flags1;
		[Field("detonation timer starts", typeof(DetonationTimerStarts2Options))]
		public short DetonationTimerStarts2;
		[Field("impact noise", typeof(ImpactNoise3Options))]
		public short ImpactNoise3;
		[Field("", null)]
		public fixed byte _4[8];
		[Field("AI perception radius:world units", null)]
		public float AIPerceptionRadius5;
		[Field("collision radius:world units", null)]
		public float CollisionRadius6;
		[Field("arming time:seconds#won't detonate before this time elapses", null)]
		public float ArmingTime8;
		[Field("danger radius:world units", null)]
		public float DangerRadius9;
		[Field("timer:seconds#detonation countdown (zero is untimed)", null)]
		public FloatBounds Timer10;
		[Field("minimum velocity:world units per second#detonates when slowed below this velocity", null)]
		public float MinimumVelocity11;
		[Field("maximum range:world units#detonates after travelling this distance", null)]
		public float MaximumRange12;
		[Field("detonation noise", typeof(DetonationNoise13Options))]
		public short DetonationNoise13;
		[Field("super det. projectile count", null)]
		public short SuperDetProjectileCount14;
		[Field("detonation started", null)]
		public TagReference DetonationStarted15;
		[Field("detonation effect (airborne)", null)]
		public TagReference DetonationEffectAirborne16;
		[Field("detonation effect (ground)", null)]
		public TagReference DetonationEffectGround17;
		[Field("detonation damage", null)]
		public TagReference DetonationDamage18;
		[Field("attached detonation damage", null)]
		public TagReference AttachedDetonationDamage19;
		[Field("super detonation", null)]
		public TagReference SuperDetonation20;
		[Field("your momma!", typeof(SuperDetonationDamageStructBlock))]
		[Block("Super Detonation Damage Struct", 1, typeof(SuperDetonationDamageStructBlock))]
		public SuperDetonationDamageStructBlock YourMomma21;
		[Field("detonation sound", null)]
		public TagReference DetonationSound22;
		[Field("damage reporting type", typeof(DamageReportingType23Options))]
		public byte DamageReportingType23;
		[Field("", null)]
		public fixed byte _24[3];
		[Field("super attached detonation damage", null)]
		public TagReference SuperAttachedDetonationDamage25;
		[Field("", null)]
		public fixed byte _26[40];
		[Field("material effect radius#radius within we will generate material effects", null)]
		public float MaterialEffectRadius27;
		[Field("flyby sound", null)]
		public TagReference FlybySound29;
		[Field("impact effect", null)]
		public TagReference ImpactEffect30;
		[Field("impact damage", null)]
		public TagReference ImpactDamage31;
		[Field("boarding detonation time", null)]
		public float BoardingDetonationTime33;
		[Field("boarding detonation damage", null)]
		public TagReference BoardingDetonationDamage34;
		[Field("boarding attached detonation damage", null)]
		public TagReference BoardingAttachedDetonationDamage35;
		[Field("", null)]
		public fixed byte _36[28];
		[Field("air gravity scale#the proportion of normal gravity applied to the projectile when in air.", null)]
		public float AirGravityScale38;
		[Field("air damage range:world units#the range over which damage is scaled when the projectile is in air.", null)]
		public FloatBounds AirDamageRange39;
		[Field("water gravity scale#the proportion of normal gravity applied to the projectile when in water.", null)]
		public float WaterGravityScale40;
		[Field("water damage range:world units#the range over which damage is scaled when the projectile is in water.", null)]
		public FloatBounds WaterDamageRange41;
		[Field("initial velocity:world units per second#bullet's velocity when inflicting maximum damage", null)]
		public float InitialVelocity42;
		[Field("final velocity:world units per second#bullet's velocity when inflicting minimum damage", null)]
		public float FinalVelocity43;
		[Field("blah", typeof(AngularVelocityLowerBoundStructBlock))]
		[Block("Angular Velocity Lower Bound Struct", 1, typeof(AngularVelocityLowerBoundStructBlock))]
		public AngularVelocityLowerBoundStructBlock Blah44;
		[Field("guided angular velocity (upper):degrees per second", null)]
		public float GuidedAngularVelocityUpper45;
		[Field("acceleration range:world units#what distance range the projectile goes from initial velocity to final velocity", null)]
		public FloatBounds AccelerationRange46;
		[Field("", null)]
		public fixed byte _47[4];
		[Field("targeted leading fraction", null)]
		public float TargetedLeadingFraction48;
		[Field("", null)]
		public fixed byte _49[48];
		[Field("material responses", null)]
		[Block("Projectile Material Response Block", 200, typeof(ProjectileMaterialResponseBlock))]
		public TagBlock MaterialResponses50;
	}
}
#pragma warning restore CS1591
