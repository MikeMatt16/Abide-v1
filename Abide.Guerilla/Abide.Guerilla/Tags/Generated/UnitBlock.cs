using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("unit", "unit", "obje", typeof(UnitBlock))]
	[FieldSet(396, 4)]
	public unsafe struct UnitBlock
	{
		public enum Flags1Options
		{
			CircularAiming_0 = 1,
			DestroyedAfterDying_1 = 2,
			HalfSpeedInterpolation_2 = 4,
			FiresFromCamera_3 = 8,
			EntranceInsideBoundingSphere_4 = 16,
			DoesnTShowReadiedWeapon_5 = 32,
			CausesPassengerDialogue_6 = 64,
			ResistsPings_7 = 128,
			MeleeAttackIsFatal_8 = 256,
			DonTRefaceDuringPings_9 = 512,
			HasNoAiming_10 = 1024,
			SimpleCreature_11 = 2048,
			ImpactMeleeAttachesToUnit_12 = 4096,
			ImpactMeleeDiesOnShields_13 = 8192,
			CannotOpenDoorsAutomatically_14 = 16384,
			MeleeAttackersCannotAttach_15 = 32768,
			NotInstantlyKilledByMelee_16 = 65536,
			ShieldSapping_17 = 131072,
			RunsAroundFlaming_18 = 262144,
			Inconsequential_19 = 524288,
			SpecialCinematicUnit_20 = 1048576,
			IgnoredByAutoaiming_21 = 2097152,
			ShieldsFryInfectionForms_22 = 4194304,
			Unused_23 = 8388608,
			Unused_24 = 16777216,
			ActsAsGunnerForParent_25 = 33554432,
			ControlledByParentGunner_26 = 67108864,
			ParentSPrimaryWeapon_27 = 134217728,
			UnitHasBoost_28 = 268435456,
		}
		public enum DefaultTeam2Options
		{
			Default_0 = 0,
			Player_1 = 1,
			Human_2 = 2,
			Covenant_3 = 3,
			Flood_4 = 4,
			Sentinel_5 = 5,
			Heretic_6 = 6,
			Prophet_7 = 7,
			Unused8_8 = 8,
			Unused9_9 = 9,
			Unused10_10 = 10,
			Unused11_11 = 11,
			Unused12_12 = 12,
			Unused13_13 = 13,
			Unused14_14 = 14,
			Unused15_15 = 15,
		}
		public enum ConstantSoundVolume3Options
		{
			Silent_0 = 0,
			Medium_1 = 1,
			Loud_2 = 2,
			Shout_3 = 3,
			Quiet_4 = 4,
		}
		public enum MotionSensorBlipSize39Options
		{
			Medium_0 = 0,
			Small_1 = 1,
			Large_2 = 2,
		}
		public enum GrenadeType45Options
		{
			HumanFragmentation_0 = 0,
			CovenantPlasma_1 = 1,
		}
		[Field("flags", typeof(Flags1Options))]
		public int Flags1;
		[Field("default team", typeof(DefaultTeam2Options))]
		public short DefaultTeam2;
		[Field("constant sound volume", typeof(ConstantSoundVolume3Options))]
		public short ConstantSoundVolume3;
		[Field("", null)]
		public fixed byte _4[4];
		[Field("integrated light toggle", null)]
		public TagReference IntegratedLightToggle5;
		[Field("", null)]
		public fixed byte _6[8];
		[Field("camera field of view:degrees", null)]
		public float CameraFieldOfView7;
		[Field("camera stiffness", null)]
		public float CameraStiffness8;
		[Field("unit camera", typeof(UnitCameraStructBlock))]
		[Block("Unit Camera Struct", 1, typeof(UnitCameraStructBlock))]
		public UnitCameraStructBlock UnitCamera9;
		[Field("acceleration", typeof(UnitSeatAccelerationStructBlock))]
		[Block("Unit Seat Acceleration Struct", 1, typeof(UnitSeatAccelerationStructBlock))]
		public UnitSeatAccelerationStructBlock Acceleration10;
		[Field("", null)]
		public fixed byte _11[4];
		[Field("soft ping threshold:[0,1]", null)]
		public float SoftPingThreshold12;
		[Field("soft ping interrupt time:seconds", null)]
		public float SoftPingInterruptTime13;
		[Field("hard ping threshold:[0,1]", null)]
		public float HardPingThreshold14;
		[Field("hard ping interrupt time:seconds", null)]
		public float HardPingInterruptTime15;
		[Field("hard death threshold:[0,1]", null)]
		public float HardDeathThreshold16;
		[Field("feign death threshold:[0,1]", null)]
		public float FeignDeathThreshold17;
		[Field("feign death time:seconds", null)]
		public float FeignDeathTime18;
		[Field("distance of evade anim:world units#this must be set to tell the AI how far it should expect our evade animation to move us", null)]
		public float DistanceOfEvadeAnim19;
		[Field("distance of dive anim:world units#this must be set to tell the AI how far it should expect our dive animation to move us", null)]
		public float DistanceOfDiveAnim20;
		[Field("", null)]
		public fixed byte _21[4];
		[Field("stunned movement threshold:[0,1]#if we take this much damage in a short space of time we will play our 'stunned movement' animations", null)]
		public float StunnedMovementThreshold22;
		[Field("feign death chance:[0,1]", null)]
		public float FeignDeathChance23;
		[Field("feign repeat chance:[0,1]", null)]
		public float FeignRepeatChance24;
		[Field("spawned turret character#automatically created character when this unit is driven", null)]
		public TagReference SpawnedTurretCharacter25;
		[Field("spawned actor count#number of actors which we spawn", null)]
		public FloatBounds SpawnedActorCount26;
		[Field("spawned velocity#velocity at which we throw spawned actors", null)]
		public float SpawnedVelocity27;
		[Field("aiming velocity maximum:degrees per second", null)]
		public float AimingVelocityMaximum28;
		[Field("aiming acceleration maximum:degrees per second squared", null)]
		public float AimingAccelerationMaximum29;
		[Field("casual aiming modifier:[0,1]", null)]
		public float CasualAimingModifier30;
		[Field("looking velocity maximum:degrees per second", null)]
		public float LookingVelocityMaximum31;
		[Field("looking acceleration maximum:degrees per second squared", null)]
		public float LookingAccelerationMaximum32;
		[Field("right_hand_node#where the primary weapon is attached", null)]
		public StringId RightHandNode33;
		[Field("left_hand_node#where the seconday weapon is attached (for dual-pistol modes)", null)]
		public StringId LeftHandNode34;
		[Field("more damn nodes", typeof(UnitAdditionalNodeNamesStructBlock))]
		[Block("Unit Additional Node Names Struct", 1, typeof(UnitAdditionalNodeNamesStructBlock))]
		public UnitAdditionalNodeNamesStructBlock MoreDamnNodes35;
		[Field("", null)]
		public fixed byte _36[8];
		[Field("melee damage", null)]
		public TagReference MeleeDamage37;
		[Field("your momma", typeof(UnitBoardingMeleeStructBlock))]
		[Block("Unit Boarding Melee Struct", 1, typeof(UnitBoardingMeleeStructBlock))]
		public UnitBoardingMeleeStructBlock YourMomma38;
		[Field("motion sensor blip size", typeof(MotionSensorBlipSize39Options))]
		public short MotionSensorBlipSize39;
		[Field("", null)]
		public fixed byte _40[2];
		[Field("postures", null)]
		[Block("Unit Postures Block", 20, typeof(UnitPosturesBlock))]
		public TagBlock Postures41;
		[Field("NEW HUD INTERFACES", null)]
		[Block("Unit Hud Reference Block", 2, typeof(UnitHudReferenceBlock))]
		public TagBlock NEWHUDINTERFACES42;
		[Field("dialogue variants", null)]
		[Block("Dialogue Variant Block", 16, typeof(DialogueVariantBlock))]
		public TagBlock DialogueVariants43;
		[Field("grenade velocity:world units per second", null)]
		public float GrenadeVelocity44;
		[Field("grenade type", typeof(GrenadeType45Options))]
		public short GrenadeType45;
		[Field("grenade count", null)]
		public short GrenadeCount46;
		[Field("", null)]
		public fixed byte _47[4];
		[Field("powered seats", null)]
		[Block("Powered Seat Block", 2, typeof(PoweredSeatBlock))]
		public TagBlock PoweredSeats48;
		[Field("weapons", null)]
		[Block("Unit Weapon Block", 4, typeof(UnitWeaponBlock))]
		public TagBlock Weapons49;
		[Field("seats", null)]
		[Block("Unit Seat Block", 32, typeof(UnitSeatBlock))]
		public TagBlock Seats50;
		[Field("boost", typeof(UnitBoostStructBlock))]
		[Block("Unit Boost Struct", 1, typeof(UnitBoostStructBlock))]
		public UnitBoostStructBlock Boost52;
		[Field("lipsync", typeof(UnitLipsyncScalesStructBlock))]
		[Block("Unit Lipsync Scales Struct", 1, typeof(UnitLipsyncScalesStructBlock))]
		public UnitLipsyncScalesStructBlock Lipsync54;
	}
}
#pragma warning restore CS1591
