#pragma warning disable CS1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Abide.Guerilla.Tags2
{
    using Abide.Guerilla.Types;
    using Abide.HaloLibrary;
    using System;
    
    [Abide.Guerilla.Tags.FieldSetAttribute(100, 4)]
    public class ActorStartingLocationsBlock
    {
        [Abide.Guerilla.Tags.FieldAttribute("name^", typeof(StringId))]
        public StringId Name;
        [Abide.Guerilla.Tags.FieldAttribute("position", typeof(Vector3))]
        public Vector3 Position;
        [Abide.Guerilla.Tags.FieldAttribute("reference frame*", typeof(Int16))]
        public Int16 ReferenceFrame;
        [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
        [Abide.Guerilla.Tags.PaddingAttribute(2)]
        public Byte[] EmptyString;
        [Abide.Guerilla.Tags.FieldAttribute("facing (yaw, pitch):degrees", typeof(Vector2))]
        public Vector2 FacingYawPitch;
        [Abide.Guerilla.Tags.FieldAttribute("flags", typeof(Int32))]
        [Abide.Guerilla.Tags.OptionsAttribute(typeof(FlagsOptions), true)]
        public Int32 Flags;
        [Abide.Guerilla.Tags.FieldAttribute("character type", typeof(Int16))]
        public Int16 CharacterType;
        [Abide.Guerilla.Tags.FieldAttribute("initial weapon", typeof(Int16))]
        public Int16 InitialWeapon;
        [Abide.Guerilla.Tags.FieldAttribute("initial secondary weapon", typeof(Int16))]
        public Int16 InitialSecondaryWeapon;
        [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
        [Abide.Guerilla.Tags.PaddingAttribute(2)]
        public Byte[] EmptyString1;
        [Abide.Guerilla.Tags.FieldAttribute("vehicle type", typeof(Int16))]
        public Int16 VehicleType;
        [Abide.Guerilla.Tags.FieldAttribute("seat type", typeof(Int16))]
        [Abide.Guerilla.Tags.OptionsAttribute(typeof(SeatTypeOptions), false)]
        public Int16 SeatType;
        [Abide.Guerilla.Tags.FieldAttribute("grenade type", typeof(Int16))]
        [Abide.Guerilla.Tags.OptionsAttribute(typeof(GrenadeTypeOptions), false)]
        public Int16 GrenadeType;
        [Abide.Guerilla.Tags.FieldAttribute("swarm count#number of cretures in swarm if a swarm is spawned at this location", typeof(Int16))]
        public Int16 SwarmCount;
        [Abide.Guerilla.Tags.FieldAttribute("actor variant name", typeof(StringId))]
        public StringId ActorVariantName;
        [Abide.Guerilla.Tags.FieldAttribute("vehicle variant name", typeof(StringId))]
        public StringId VehicleVariantName;
        [Abide.Guerilla.Tags.FieldAttribute("initial movement distance#before doing anything else, the actor will travel the g" +
            "iven distance in its forward direction", typeof(Single))]
        public Single InitialMovementDistance;
        [Abide.Guerilla.Tags.FieldAttribute("emitter vehicle", typeof(Int16))]
        public Int16 EmitterVehicle;
        [Abide.Guerilla.Tags.FieldAttribute("initial movement mode", typeof(Int16))]
        [Abide.Guerilla.Tags.OptionsAttribute(typeof(InitialMovementModeOptions), false)]
        public Int16 InitialMovementMode;
        [Abide.Guerilla.Tags.FieldAttribute("Placement script", typeof(String32))]
        public String32 PlacementScript;
        [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
        [Abide.Guerilla.Tags.PaddingAttribute(2)]
        public Byte[] EmptyString2;
        [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
        [Abide.Guerilla.Tags.PaddingAttribute(2)]
        public Byte[] EmptyString3;
        public enum FlagsOptions
        {
            InitiallyAsleep = 1,
            InfectionFormExplode = 2,
            Na = 4,
            AlwaysPlace = 8,
            InitiallyHidden = 16,
        }
        public enum SeatTypeOptions
        {
            Default = 0,
            Passenger = 1,
            Gunner = 2,
            Driver = 3,
            SmallCargo = 4,
            LargeCargo = 5,
            NoDriver = 6,
            NoVehicle = 7,
        }
        public enum GrenadeTypeOptions
        {
            None = 0,
            HumanGrenade = 1,
            CovenantPlasma = 2,
        }
        public enum InitialMovementModeOptions
        {
            Default = 0,
            Climbing = 1,
            Flying = 2,
        }
    }
}
#pragma warning restore CS1591