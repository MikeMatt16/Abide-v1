using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("weapon", "weap", "item", typeof(WeaponBlock))]
	[FieldSet(716, 4)]
	public unsafe struct WeaponBlock
	{
		public enum Flags1Options
		{
			VerticalHeatDisplay_0 = 1,
			MutuallyExclusiveTriggers_1 = 2,
			AttacksAutomaticallyOnBump_2 = 4,
			MustBeReadied_3 = 8,
			DoesnTCountTowardMaximum_4 = 16,
			AimAssistsOnlyWhenZoomed_5 = 32,
			PreventsGrenadeThrowing_6 = 64,
			MustBePickedUp_7 = 128,
			HoldsTriggersWhenDropped_8 = 256,
			PreventsMeleeAttack_9 = 512,
			DetonatesWhenDropped_10 = 1024,
			CannotFireAtMaximumAge_11 = 2048,
			SecondaryTriggerOverridesGrenades_12 = 4096,
			OBSOLETEDoesNotDepowerActiveCamoInMultilplayer_13 = 8192,
			EnablesIntegratedNightVision_14 = 16384,
			AIsUseWeaponMeleeDamage_15 = 32768,
			ForcesNoBinoculars_16 = 65536,
			LoopFpFiringAnimation_17 = 131072,
			PreventsSprinting_18 = 262144,
			CannotFireWhileBoosting_19 = 524288,
			PreventsDriving_20 = 1048576,
			PreventsGunning_21 = 2097152,
			CanBeDualWielded_22 = 4194304,
			CanOnlyBeDualWielded_23 = 8388608,
			MeleeOnly_24 = 16777216,
			CantFireIfParentDead_25 = 33554432,
			WeaponAgesWithEachKill_26 = 67108864,
			WeaponUsesOldDualFireErrorCode_27 = 134217728,
			PrimaryTriggerMeleeAttacks_28 = 268435456,
			CannotBeUsedByPlayer_29 = 536870912,
		}
		public enum SecondaryTriggerMode3Options
		{
			Normal_0 = 0,
			SlavedToPrimary_1 = 1,
			InhibitsPrimary_2 = 2,
			LoadsAlterateAmmunition_3 = 3,
			LoadsMultiplePrimaryAmmunition_4 = 4,
		}
		public enum MeleeDamageReportingType28Options
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
		public enum MovementPenalized36Options
		{
			Always_0 = 0,
			WhenZoomed_1 = 1,
			WhenZoomedOrReloading_2 = 2,
		}
		public enum MultiplayerWeaponType61Options
		{
			None_0 = 0,
			CtfFlag_1 = 1,
			OddballBall_2 = 2,
			HeadhunterHead_3 = 3,
			JuggernautPowerup_4 = 4,
		}
		public enum WeaponType63Options
		{
			Undefined_0 = 0,
			Shotgun_1 = 1,
			Needler_2 = 2,
			PlasmaPistol_3 = 3,
			PlasmaRifle_4 = 4,
			RocketLauncher_5 = 5,
		}
		[Field("flags", typeof(Flags1Options))]
		public int Flags1;
		[Field("secondary trigger mode", typeof(SecondaryTriggerMode3Options))]
		public short SecondaryTriggerMode3;
		[Field("maximum alternate shots loaded#if the second trigger loads alternate ammunition, this is the maximum number of shots that can be loaded at a time", null)]
		public short MaximumAlternateShotsLoaded4;
		[Field("turn on time#how long after being readied it takes this weapon to switch its 'turned_on' attachment to 1.0", null)]
		public float TurnOnTime5;
		[Field("ready time:seconds", null)]
		public float ReadyTime7;
		[Field("ready effect", null)]
		public TagReference ReadyEffect8;
		[Field("ready damage effect", null)]
		public TagReference ReadyDamageEffect9;
		[Field("heat recovery threshold:[0,1]#the heat value a weapon must return to before leaving the overheated state, once it has become overheated in the first place", null)]
		public float HeatRecoveryThreshold11;
		[Field("overheated threshold:[0,1]#the heat value over which a weapon first becomes overheated (should be greater than the heat recovery threshold)", null)]
		public float OverheatedThreshold12;
		[Field("heat detonation threshold:[0,1]#the heat value above which the weapon has a chance of exploding each time it is fired", null)]
		public float HeatDetonationThreshold13;
		[Field("heat detonation fraction:[0,1]#the percent chance (between 0.0 and 1.0) the weapon will explode when fired over the heat detonation threshold", null)]
		public float HeatDetonationFraction14;
		[Field("heat loss per second:[0,1]#the amount of heat lost each second when the weapon is not being fired", null)]
		public float HeatLossPerSecond15;
		[Field("heat illumination:[0,1]#the amount of illumination given off when the weapon is overheated", null)]
		public float HeatIllumination16;
		[Field("overheated heat loss per second:[0,1]#the amount of heat lost each second when the weapon is not being fired", null)]
		public float OverheatedHeatLossPerSecond17;
		[Field("overheated", null)]
		public TagReference Overheated18;
		[Field("overheated damage effect", null)]
		public TagReference OverheatedDamageEffect19;
		[Field("detonation", null)]
		public TagReference Detonation20;
		[Field("detonation damage effect", null)]
		public TagReference DetonationDamageEffect21;
		[Field("player melee damage", null)]
		public TagReference PlayerMeleeDamage22;
		[Field("player melee response", null)]
		public TagReference PlayerMeleeResponse23;
		[Field("melee aim assist", typeof(MeleeAimAssistStructBlock))]
		[Block("Melee Aim Assist Struct", 1, typeof(MeleeAimAssistStructBlock))]
		public MeleeAimAssistStructBlock MeleeAimAssist25;
		[Field("melee damage parameters", typeof(MeleeDamageParametersStructBlock))]
		[Block("Melee Damage Parameters Struct", 1, typeof(MeleeDamageParametersStructBlock))]
		public MeleeDamageParametersStructBlock MeleeDamageParameters27;
		[Field("melee damage reporting type", typeof(MeleeDamageReportingType28Options))]
		public byte MeleeDamageReportingType28;
		[Field("", null)]
		public fixed byte _29[1];
		[Field("magnification levels#the number of magnification levels this weapon allows", null)]
		public short MagnificationLevels31;
		[Field("magnification range", null)]
		public FloatBounds MagnificationRange32;
		[Field("weapon aim assist", typeof(AimAssistStructBlock))]
		[Block("Aim Assist Struct", 1, typeof(AimAssistStructBlock))]
		public AimAssistStructBlock WeaponAimAssist34;
		[Field("movement penalized", typeof(MovementPenalized36Options))]
		public short MovementPenalized36;
		[Field("", null)]
		public fixed byte _37[2];
		[Field("forward movement penalty#percent slowdown to forward movement for units carrying this weapon", null)]
		public float ForwardMovementPenalty38;
		[Field("sideways movement penalty#percent slowdown to sideways and backward movement for units carrying this weapon", null)]
		public float SidewaysMovementPenalty39;
		[Field("", null)]
		public fixed byte _40[4];
		[Field("AI scariness", null)]
		public float AIScariness42;
		[Field("weapon power-on time:seconds", null)]
		public float WeaponPowerOnTime44;
		[Field("weapon power-off time:seconds", null)]
		public float WeaponPowerOffTime45;
		[Field("weapon power-on effect", null)]
		public TagReference WeaponPowerOnEffect46;
		[Field("weapon power-off effect", null)]
		public TagReference WeaponPowerOffEffect47;
		[Field("age heat recovery penalty#how much the weapon's heat recovery is penalized as it ages", null)]
		public float AgeHeatRecoveryPenalty48;
		[Field("age rate of fire penalty#how much the weapon's rate of fire is penalized as it ages", null)]
		public float AgeRateOfFirePenalty49;
		[Field("age misfire start:[0,1]#the age threshold when the weapon begins to misfire", null)]
		public float AgeMisfireStart50;
		[Field("age misfire chance:[0,1]#at age 1.0, the misfire chance per shot", null)]
		public float AgeMisfireChance51;
		[Field("pickup sound", null)]
		public TagReference PickupSound52;
		[Field("zoom-in sound", null)]
		public TagReference ZoomInSound53;
		[Field("zoom-out sound", null)]
		public TagReference ZoomOutSound54;
		[Field("active camo ding#how much to decrease active camo when a round is fired", null)]
		public float ActiveCamoDing55;
		[Field("active camo regrowth rate#how fast to increase active camo (per tick) when a round is fired", null)]
		public float ActiveCamoRegrowthRate56;
		[Field("handle node#the node that get's attached to the unit's hand", null)]
		public StringId HandleNode57;
		[Field("weapon class", null)]
		public StringId WeaponClass59;
		[Field("weapon name", null)]
		public StringId WeaponName60;
		[Field("multiplayer weapon type", typeof(MultiplayerWeaponType61Options))]
		public short MultiplayerWeaponType61;
		[Field("weapon type", typeof(WeaponType63Options))]
		public short WeaponType63;
		[Field("tracking", typeof(WeaponTrackingStructBlock))]
		[Block("Weapon Tracking Struct", 1, typeof(WeaponTrackingStructBlock))]
		public WeaponTrackingStructBlock Tracking64;
		[Field("player interface", typeof(WeaponInterfaceStructBlock))]
		[Block("Weapon Interface Struct", 1, typeof(WeaponInterfaceStructBlock))]
		public WeaponInterfaceStructBlock PlayerInterface65;
		[Field("predicted resources", null)]
		[Block("Predicted Resource Block", 2048, typeof(PredictedResourceBlock))]
		public TagBlock PredictedResources66;
		[Field("magazines", null)]
		[Block("Magazines", 2, typeof(Magazines))]
		public TagBlock Magazines67;
		[Field("new triggers", null)]
		[Block("Weapon Triggers", 2, typeof(WeaponTriggers))]
		public TagBlock NewTriggers68;
		[Field("barrels", null)]
		[Block("Weapon Barrels", 2, typeof(WeaponBarrels))]
		public TagBlock Barrels69;
		[Field("", null)]
		public fixed byte _70[8];
		[Field("", null)]
		public fixed byte _71[16];
		[Field("max movement acceleration", null)]
		public float MaxMovementAcceleration73;
		[Field("max movement velocity", null)]
		public float MaxMovementVelocity74;
		[Field("max turning acceleration", null)]
		public float MaxTurningAcceleration75;
		[Field("max turning velocity", null)]
		public float MaxTurningVelocity76;
		[Field("deployed vehicle", null)]
		public TagReference DeployedVehicle77;
		[Field("age effect", null)]
		public TagReference AgeEffect78;
		[Field("aged weapon", null)]
		public TagReference AgedWeapon79;
		[Field("first person weapon offset", null)]
		public Vector3 FirstPersonWeaponOffset80;
		[Field("first person scope size", null)]
		public Vector2 FirstPersonScopeSize81;
	}
}
#pragma warning restore CS1591
