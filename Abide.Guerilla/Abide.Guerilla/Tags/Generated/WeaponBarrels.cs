using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(256, 4)]
	public unsafe struct WeaponBarrels
	{
		public enum Flags0Options
		{
			TracksFiredProjectilePooPooCaCaPeePee_0 = 1,
			RandomFiringEffectsRatherThanBeingChosenSequentiallyFiringEffectsArePickedRandomly_1 = 2,
			CanFireWithPartialAmmoAllowsAWeaponToBeFiredAsLongAsThereIsANonZeroAmountOfAmmunitionLoaded_2 = 4,
			ProjectilesUseWeaponOriginInsteadOfComingOutOfTheMagicFirstPersonCameraOriginTheProjectilesForThisWeaponActuallyComeOutOfTheGun_3 = 8,
			EjectsDuringChamberThisTriggerSEjectionPortIsStartedDuringTheKeyFrameOfItsChamberAnimation_4 = 16,
			UseErrorWhenUnzoomed_5 = 32,
			ProjectileVectorCannotBeAdjustedProjectilesFiredByThisWeaponCannotHaveTheirDirectionAdjustedByTheAIToHitTheTarget_6 = 64,
			ProjectilesHaveIdenticalError_7 = 128,
			ProjectilesFireParallelIfThereAreMultipleGunsForThisTriggerTheProjectilesEmergeInParallelBeamsRatherThanIndependantAiming_8 = 256,
			CantFireWhenOthersFiring_9 = 512,
			CantFireWhenOthersRecovering_10 = 1024,
			DonTClearFireBitAfterRecovering_11 = 2048,
			StaggerFireAcrossMultipleMarkers_12 = 4096,
			FiresLockedProjectiles_13 = 8192,
		}
		public enum PredictionType16Options
		{
			None_0 = 0,
			Continuous_1 = 1,
			Instant_2 = 2,
		}
		public enum FiringNoiseHowLoudThisWeaponAppearsToTheAI17Options
		{
			Silent_0 = 0,
			Medium_1 = 1,
			Loud_2 = 2,
			Shout_3 = 3,
			Quiet_4 = 4,
		}
		public enum DistributionFunction30Options
		{
			Point_0 = 0,
			HorizontalFan_1 = 1,
		}
		public enum DamageEffectReportingType36Options
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
		public enum AngleChangeFunctionFunctionUsedToScaleBetweenInitialAndFinalAngleChangePerShot50Options
		{
			Linear_0 = 0,
			Early_1 = 1,
			VeryEarly_2 = 2,
			Late_3 = 3,
			VeryLate_4 = 4,
			Cosine_5 = 5,
			One_6 = 6,
			Zero_7 = 7,
		}
		[Field("flags", typeof(Flags0Options))]
		public int Flags0;
		[Field("rounds per second#the number of firing effects created per second", null)]
		public FloatBounds RoundsPerSecond2;
		[Field("acceleration time:seconds#the continuous firing time it takes for the weapon to achieve its final rounds per second", null)]
		public float AccelerationTime3;
		[Field("deceleration time:seconds#the continuous idle time it takes for the weapon to return from its final rounds per second to its initial", null)]
		public float DecelerationTime4;
		[Field("barrel spin scale#scale the barrel spin speed by this amount", null)]
		public float BarrelSpinScale5;
		[Field("blurred rate of fire#a percentage between 0 and 1 which controls how soon in its firing animation the weapon blurs", null)]
		public float BlurredRateOfFire6;
		[Field("shots per fire#allows designer caps to the shots you can fire from one firing action", null)]
		public FloatBounds ShotsPerFire7;
		[Field("fire recovery time:seconds#how long after a set of shots it takes before the barrel can fire again", null)]
		public float FireRecoveryTime8;
		[Field("soft recovery fraction#how much of the recovery allows shots to be queued", null)]
		public float SoftRecoveryFraction9;
		[Field("magazine#the magazine from which this trigger draws its ammunition", null)]
		public short Magazine10;
		[Field("rounds per shot#the number of rounds expended to create a single firing effect", null)]
		public short RoundsPerShot11;
		[Field("minimum rounds loaded#the minimum number of rounds necessary to fire the weapon", null)]
		public short MinimumRoundsLoaded12;
		[Field("rounds between tracers#the number of non-tracer rounds fired between tracers", null)]
		public short RoundsBetweenTracers13;
		[Field("optional barrel marker name", null)]
		public StringId OptionalBarrelMarkerName14;
		[Field("prediction type", typeof(PredictionType16Options))]
		public short PredictionType16;
		[Field("firing noise#how loud this weapon appears to the AI", typeof(FiringNoiseHowLoudThisWeaponAppearsToTheAI17Options))]
		public short FiringNoise17;
		[Field("acceleration time:seconds#the continuous firing time it takes for the weapon to achieve its final error", null)]
		public float AccelerationTime19;
		[Field("deceleration time:seconds#the continuous idle time it takes for the weapon to return to its initial error", null)]
		public float DecelerationTime20;
		[Field("damage error#the range of angles (in degrees) that a damaged weapon will skew fire", null)]
		public FloatBounds DamageError21;
		[Field("acceleration time:seconds#the continuous firing time it takes for the weapon to achieve its final error", null)]
		public float AccelerationTime23;
		[Field("deceleration time:seconds#the continuous idle time it takes for the weapon to return to its initial error", null)]
		public float DecelerationTime24;
		[Field("", null)]
		public fixed byte _25[8];
		[Field("minimum error:degrees", null)]
		public float MinimumError26;
		[Field("error angle:degrees", null)]
		public FloatBounds ErrorAngle27;
		[Field("dual wield damage scale", null)]
		public float DualWieldDamageScale28;
		[Field("distribution function", typeof(DistributionFunction30Options))]
		public short DistributionFunction30;
		[Field("projectiles per shot", null)]
		public short ProjectilesPerShot31;
		[Field("distribution angle:degrees", null)]
		public float DistributionAngle32;
		[Field("minimum error:degrees", null)]
		public float MinimumError33;
		[Field("error angle:degrees", null)]
		public FloatBounds ErrorAngle34;
		public Vector3 FirstPersonOffset35;
		[Field("damage effect reporting type", typeof(DamageEffectReportingType36Options))]
		public byte DamageEffectReportingType36;
		[Field("", null)]
		public fixed byte _37[3];
		[Field("projectile", null)]
		public TagReference Projectile38;
		[Field("eh", typeof(WeaponBarrelDamageEffectStructBlock))]
		[Block("Weapon Barrel Damage Effect Struct", 1, typeof(WeaponBarrelDamageEffectStructBlock))]
		public WeaponBarrelDamageEffectStructBlock Eh39;
		[Field("ejection port recovery time#the amount of time (in seconds) it takes for the ejection port to transition from 1.0 (open) to 0.0 (closed) after a shot has been fired", null)]
		public float EjectionPortRecoveryTime41;
		[Field("illumination recovery time#the amount of time (in seconds) it takes the illumination function to transition from 1.0 (bright) to 0.0 (dark) after a shot has been fired", null)]
		public float IlluminationRecoveryTime42;
		[Field("heat generated per round:[0,1]#the amount of heat generated each time the trigger is fired", null)]
		public float HeatGeneratedPerRound43;
		[Field("age generated per round:[0,1]#the amount the weapon ages each time the trigger is fired", null)]
		public float AgeGeneratedPerRound44;
		[Field("overload time:seconds#the next trigger fires this often while holding down this trigger", null)]
		public float OverloadTime45;
		[Field("angle change per shot#angle change per shot of the weapon during firing", null)]
		public FloatBounds AngleChangePerShot47;
		[Field("acceleration time:seconds#the continuous firing time it takes for the weapon to achieve its final angle change per shot", null)]
		public float AccelerationTime48;
		[Field("deceleration time:seconds#the continuous idle time it takes for the weapon to return to its initial angle change per shot", null)]
		public float DecelerationTime49;
		[Field("angle change function#function used to scale between initial and final angle change per shot", typeof(AngleChangeFunctionFunctionUsedToScaleBetweenInitialAndFinalAngleChangePerShot50Options))]
		public short AngleChangeFunction50;
		[Field("", null)]
		public fixed byte _51[2];
		[Field("", null)]
		public fixed byte _52[8];
		[Field("", null)]
		public fixed byte _53[24];
		[Field("firing effects#firing effects determine what happens when this trigger is fired", null)]
		[Block("Barrel Firing Effect Block", 3, typeof(BarrelFiringEffectBlock))]
		public TagBlock FiringEffects54;
	}
}
#pragma warning restore CS1591
