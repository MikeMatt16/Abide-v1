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
    
    [Abide.Guerilla.Tags.FieldSetAttribute(20, 4)]
    public class EffectAccelerationsBlock
    {
        [Abide.Guerilla.Tags.FieldAttribute("create in", typeof(Int16))]
        [Abide.Guerilla.Tags.OptionsAttribute(typeof(CreateInOptions), false)]
        public Int16 CreateIn;
        [Abide.Guerilla.Tags.FieldAttribute("create in", typeof(Int16))]
        [Abide.Guerilla.Tags.OptionsAttribute(typeof(CreateInOptions1), false)]
        public Int16 CreateIn1;
        [Abide.Guerilla.Tags.FieldAttribute("location", typeof(Int16))]
        public Int16 Location;
        [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
        [Abide.Guerilla.Tags.PaddingAttribute(2)]
        public Byte[] EmptyString;
        [Abide.Guerilla.Tags.FieldAttribute("acceleration", typeof(Single))]
        public Single Acceleration;
        [Abide.Guerilla.Tags.FieldAttribute("inner cone angle:degrees", typeof(Single))]
        public Single InnerConeAngle;
        [Abide.Guerilla.Tags.FieldAttribute("outer cone angle:degrees", typeof(Single))]
        public Single OuterConeAngle;
        public enum CreateInOptions
        {
            AnyEnvironment = 0,
            AirOnly = 1,
            WaterOnly = 2,
            SpaceOnly = 3,
        }
        public enum CreateInOptions1
        {
            EitherMode = 0,
            ViolentModeOnly = 1,
            NonviolentModeOnly = 2,
        }
    }
}
#pragma warning restore CS1591