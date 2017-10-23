using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(192, 4)]
	public unsafe struct UnitSeatBlock
	{
		public enum Flags0Options
		{
			Invisible_0 = 1,
			Locked_1 = 2,
			Driver_2 = 4,
			Gunner_3 = 8,
			ThirdPersonCamera_4 = 16,
			AllowsWeapons_5 = 32,
			ThirdPersonOnEnter_6 = 64,
			FirstPersonCameraSlavedToGun_7 = 128,
			AllowVehicleCommunicationAnimations_8 = 256,
			NotValidWithoutDriver_9 = 512,
			AllowAINoncombatants_10 = 1024,
			BoardingSeat_11 = 2048,
			AiFiringDisabledByMaxAcceleration_12 = 4096,
			BoardingEntersSeat_13 = 8192,
			BoardingNeedAnyPassenger_14 = 16384,
			ControlsOpenAndClose_15 = 32768,
			InvalidForPlayer_16 = 65536,
			InvalidForNonPlayer_17 = 131072,
			GunnerPlayerOnly_18 = 262144,
			InvisibleUnderMajorDamage_19 = 524288,
		}
		public enum AiSeatType12Options
		{
			NONE_0 = 0,
			Passenger_1 = 1,
			Gunner_2 = 2,
			SmallCargo_3 = 3,
			LargeCargo_4 = 4,
			Driver_5 = 5,
		}
		[Field("flags", typeof(Flags0Options))]
		public int Flags0;
		[Field("entry marker(s) name", null)]
		public StringId EntryMarkerSName3;
		[Field("boarding grenade marker", null)]
		public StringId BoardingGrenadeMarker4;
		[Field("boarding grenade string", null)]
		public StringId BoardingGrenadeString5;
		[Field("boarding melee string", null)]
		public StringId BoardingMeleeString6;
		[Field("ping scale#nathan is too lazy to make pings for each seat.", null)]
		public float PingScale7;
		[Field("", null)]
		public fixed byte _8[8];
		[Field("turnover time:seconds#how much time it takes to evict a rider from a flipped vehicle", null)]
		public float TurnoverTime9;
		[Field("acceleration", typeof(UnitSeatAccelerationStructBlock))]
		[Block("Unit Seat Acceleration Struct", 1, typeof(UnitSeatAccelerationStructBlock))]
		public UnitSeatAccelerationStructBlock Acceleration10;
		[Field("AI scariness", null)]
		public float AIScariness11;
		[Field("ai seat type", typeof(AiSeatType12Options))]
		public short AiSeatType12;
		[Field("boarding seat", null)]
		public short BoardingSeat13;
		[Field("listener interpolation factor#how far to interpolate listener position from camera to occupant's head", null)]
		public float ListenerInterpolationFactor14;
		[Field("yaw rate bounds:degrees per second", null)]
		public FloatBounds YawRateBounds16;
		[Field("pitch rate bounds:degrees per second", null)]
		public FloatBounds PitchRateBounds17;
		[Field("min speed reference", null)]
		public float MinSpeedReference18;
		[Field("max speed reference", null)]
		public float MaxSpeedReference19;
		[Field("speed exponent", null)]
		public float SpeedExponent20;
		[Field("", null)]
		public fixed byte _21[12];
		[Field("unit camera", typeof(UnitCameraStructBlock))]
		[Block("Unit Camera Struct", 1, typeof(UnitCameraStructBlock))]
		public UnitCameraStructBlock UnitCamera23;
		[Field("unit hud interface", null)]
		[Block("Unit Hud Reference Block", 2, typeof(UnitHudReferenceBlock))]
		public TagBlock UnitHudInterface24;
		[Field("enter seat string", null)]
		public StringId EnterSeatString25;
		[Field("", null)]
		public fixed byte _26[4];
		[Field("yaw minimum", null)]
		public float YawMinimum27;
		[Field("yaw maximum", null)]
		public float YawMaximum28;
		[Field("built-in gunner", null)]
		public TagReference BuiltInGunner29;
		[Field("", null)]
		public fixed byte _30[20];
		[Field("entry radius#how close to the entry marker a unit must be", null)]
		public float EntryRadius32;
		[Field("entry marker cone angle#angle from marker forward the unit must be", null)]
		public float EntryMarkerConeAngle33;
		[Field("entry marker facing angle#angle from unit facing the marker must be", null)]
		public float EntryMarkerFacingAngle34;
		[Field("maximum relative velocity", null)]
		public float MaximumRelativeVelocity35;
		[Field("", null)]
		public fixed byte _36[20];
		[Field("invisible seat region", null)]
		public StringId InvisibleSeatRegion37;
		[Field("runtime invisible seat region index*", null)]
		public int RuntimeInvisibleSeatRegionIndex38;
	}
}
#pragma warning restore CS1591
