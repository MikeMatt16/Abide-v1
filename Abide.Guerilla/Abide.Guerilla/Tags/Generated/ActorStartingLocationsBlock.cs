using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(100, 4)]
	public unsafe struct ActorStartingLocationsBlock
	{
		public enum Flags5Options
		{
			InitiallyAsleep_0 = 1,
			InfectionFormExplode_1 = 2,
			NA_2 = 4,
			AlwaysPlace_3 = 8,
			InitiallyHidden_4 = 16,
		}
		public enum SeatType11Options
		{
			DEFAULT_0 = 0,
			Passenger_1 = 1,
			Gunner_2 = 2,
			Driver_3 = 3,
			SmallCargo_4 = 4,
			LargeCargo_5 = 5,
			NODriver_6 = 6,
			NOVehicle_7 = 7,
		}
		public enum GrenadeType12Options
		{
			NONE_0 = 0,
			HumanGrenade_1 = 1,
			CovenantPlasma_2 = 2,
		}
		public enum InitialMovementMode18Options
		{
			Default_0 = 0,
			Climbing_1 = 1,
			Flying_2 = 2,
		}
		public Vector3 Position1;
		[Field("reference frame*", null)]
		public short ReferenceFrame2;
		[Field("", null)]
		public fixed byte _3[2];
		[Field("facing (yaw, pitch):degrees", null)]
		public Vector2 FacingYawPitch4;
		[Field("flags", typeof(Flags5Options))]
		public int Flags5;
		[Field("character type", null)]
		public short CharacterType6;
		[Field("initial weapon", null)]
		public short InitialWeapon7;
		[Field("initial secondary weapon", null)]
		public short InitialSecondaryWeapon8;
		[Field("", null)]
		public fixed byte _9[2];
		[Field("vehicle type", null)]
		public short VehicleType10;
		[Field("seat type", typeof(SeatType11Options))]
		public short SeatType11;
		[Field("grenade type", typeof(GrenadeType12Options))]
		public short GrenadeType12;
		[Field("swarm count#number of cretures in swarm if a swarm is spawned at this location", null)]
		public short SwarmCount13;
		[Field("actor variant name", null)]
		public StringId ActorVariantName14;
		[Field("vehicle variant name", null)]
		public StringId VehicleVariantName15;
		[Field("initial movement distance#before doing anything else, the actor will travel the given distance in its forward direction", null)]
		public float InitialMovementDistance16;
		[Field("emitter vehicle", null)]
		public short EmitterVehicle17;
		[Field("initial movement mode", typeof(InitialMovementMode18Options))]
		public short InitialMovementMode18;
		[Field("Placement script", null)]
		public String PlacementScript19;
		[Field("", null)]
		public fixed byte _20[2];
		[Field("", null)]
		public fixed byte _21[2];
	}
}
#pragma warning restore CS1591
