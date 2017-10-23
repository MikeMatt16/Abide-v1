using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("vehicle", "vehi", "unit", typeof(VehicleBlock))]
	[FieldSet(332, 4)]
	public unsafe struct VehicleBlock
	{
		public enum Flags1Options
		{
			SpeedWakesPhysics_0 = 1,
			TurnWakesPhysics_1 = 2,
			DriverPowerWakesPhysics_2 = 4,
			GunnerPowerWakesPhysics_3 = 8,
			ControlOppositeSpeedSetsBrake_4 = 16,
			SlideWakesPhysics_5 = 32,
			KillsRidersAtTerminalVelocity_6 = 64,
			CausesCollisionDamage_7 = 128,
			AiWeaponCannotRotate_8 = 256,
			AiDoesNotRequireDriver_9 = 512,
			AiUnused_10 = 1024,
			AiDriverEnable_11 = 2048,
			AiDriverFlying_12 = 4096,
			AiDriverCanSidestep_13 = 8192,
			AiDriverHovering_14 = 16384,
			VehicleSteersDirectly_15 = 32768,
			Unused_16 = 65536,
			HasEBrake_17 = 131072,
			NoncombatVehicle_18 = 262144,
			NoFrictionWDriver_19 = 524288,
			CanTriggerAutomaticOpeningDoors_20 = 1048576,
			AutoaimWhenTeamless_21 = 2097152,
		}
		public enum Type2Options
		{
			HumanTank_0 = 0,
			HumanJeep_1 = 1,
			HumanBoat_2 = 2,
			HumanPlane_3 = 3,
			AlienScout_4 = 4,
			AlienFighter_5 = 5,
			Turret_6 = 6,
		}
		public enum Control3Options
		{
			VehicleControlNormal_0 = 0,
			VehicleControlUnused_1 = 1,
			VehicleControlTank_2 = 2,
		}
		public enum SpecificTypeIfYourTypeCorrespondsToSomethingInThisListChooseIt13Options
		{
			None_0 = 0,
			Ghost_1 = 1,
			Wraith_2 = 2,
			Spectre_3 = 3,
			SentinelEnforcer_4 = 4,
		}
		public enum PlayerTrainingVehicleType14Options
		{
			None_0 = 0,
			Warthog_1 = 1,
			WarthogTurret_2 = 2,
			Ghost_3 = 3,
			Banshee_4 = 4,
			Tank_5 = 5,
			Wraith_6 = 6,
		}
		public enum VehicleSizeTheSizeDetermineWhatKindOfSeatsInLargerVehiclesItMayOccupyIESmallOrLargeCargoSeats25Options
		{
			Small_0 = 0,
			Large_1 = 1,
		}
		[Field("flags", typeof(Flags1Options))]
		public int Flags1;
		[Field("type", typeof(Type2Options))]
		public short Type2;
		[Field("control", typeof(Control3Options))]
		public short Control3;
		[Field("maximum forward speed", null)]
		public float MaximumForwardSpeed4;
		[Field("maximum reverse speed", null)]
		public float MaximumReverseSpeed5;
		[Field("speed acceleration", null)]
		public float SpeedAcceleration6;
		[Field("speed deceleration", null)]
		public float SpeedDeceleration7;
		[Field("maximum left turn", null)]
		public float MaximumLeftTurn8;
		[Field("maximum right turn (negative)", null)]
		public float MaximumRightTurnNegative9;
		[Field("wheel circumference", null)]
		public float WheelCircumference10;
		[Field("turn rate", null)]
		public float TurnRate11;
		[Field("blur speed", null)]
		public float BlurSpeed12;
		[Field("specific type#if your type corresponds to something in this list choose it", typeof(SpecificTypeIfYourTypeCorrespondsToSomethingInThisListChooseIt13Options))]
		public short SpecificType13;
		[Field("player training vehicle type", typeof(PlayerTrainingVehicleType14Options))]
		public short PlayerTrainingVehicleType14;
		[Field("flip message", null)]
		public StringId FlipMessage15;
		[Field("turn scale", null)]
		public float TurnScale16;
		[Field("speed turn penalty power (0.5 .. 2)", null)]
		public float SpeedTurnPenaltyPower05217;
		[Field("speed turn penalty (0 = none, 1 = can't turn at top speed)", null)]
		public float SpeedTurnPenalty0None1CanTTurnAtTopSpeed18;
		[Field("maximum left slide", null)]
		public float MaximumLeftSlide19;
		[Field("maximum right slide", null)]
		public float MaximumRightSlide20;
		[Field("slide acceleration", null)]
		public float SlideAcceleration21;
		[Field("slide deceleration", null)]
		public float SlideDeceleration22;
		[Field("minimum flipping angular velocity", null)]
		public float MinimumFlippingAngularVelocity23;
		[Field("maximum flipping angular velocity", null)]
		public float MaximumFlippingAngularVelocity24;
		[Field("vehicle size#The size determine what kind of seats in larger vehicles it may occupy (i.e. small or large cargo seats)", typeof(VehicleSizeTheSizeDetermineWhatKindOfSeatsInLargerVehiclesItMayOccupyIESmallOrLargeCargoSeats25Options))]
		public short VehicleSize25;
		[Field("", null)]
		public fixed byte _26[2];
		[Field("", null)]
		public fixed byte _27[20];
		[Field("fixed gun yaw", null)]
		public float FixedGunYaw28;
		[Field("fixed gun pitch", null)]
		public float FixedGunPitch29;
		[Field("overdampen cusp angle:degrees", null)]
		public float OverdampenCuspAngle31;
		[Field("overdampen exponent", null)]
		public float OverdampenExponent32;
		[Field("crouch transition time:seconds", null)]
		public float CrouchTransitionTime33;
		[Field("", null)]
		public fixed byte _34[4];
		[Field("engine moment#higher moments make engine spin up slower", null)]
		public float EngineMoment36;
		[Field("engine max angular velocity#higher moments make engine spin up slower", null)]
		public float EngineMaxAngularVelocity37;
		[Field("gears", null)]
		[Block("Gear Block", 16, typeof(GearBlock))]
		public TagBlock Gears38;
		[Field("flying torque scale#big vehicles need to scale this down.  0 defaults to 1, which is generally a good value.  This is used with alien fighter physics", null)]
		public float FlyingTorqueScale39;
		[Field("seat enterance acceleration scale#how much do we scale the force the biped the applies down on the seat when he enters. 0 == no acceleration", null)]
		public float SeatEnteranceAccelerationScale40;
		[Field("seat exit accelersation scale#how much do we scale the force the biped the applies down on the seat when he exits. 0 == no acceleration", null)]
		public float SeatExitAccelersationScale41;
		[Field("", null)]
		public fixed byte _42[16];
		[Field("air friction deceleration#human plane physics only. 0 is nothing.  1 is like thowing the engine to full reverse", null)]
		public float AirFrictionDeceleration43;
		[Field("thrust scale#human plane physics only. 0 is default (1)", null)]
		public float ThrustScale44;
		[Field("suspension sound", null)]
		public TagReference SuspensionSound46;
		[Field("crash sound", null)]
		public TagReference CrashSound47;
		[Field("UNUSED*", null)]
		public TagReference UNUSED48;
		[Field("special effect", null)]
		public TagReference SpecialEffect49;
		[Field("unused effect", null)]
		public TagReference UnusedEffect50;
		[Field("havok vehicle physics", typeof(HavokVehiclePhysicsStructBlock))]
		[Block("Havok Vehicle Physics Struct", 1, typeof(HavokVehiclePhysicsStructBlock))]
		public HavokVehiclePhysicsStructBlock HavokVehiclePhysics52;
	}
}
#pragma warning restore CS1591
