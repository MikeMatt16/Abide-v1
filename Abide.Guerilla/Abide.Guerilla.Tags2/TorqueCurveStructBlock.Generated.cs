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
    
    [Abide.Guerilla.Tags.FieldSetAttribute(24, 4)]
    public class TorqueCurveStructBlock
    {
        [Abide.Guerilla.Tags.FieldAttribute("min torque", typeof(Single))]
        public Single MinTorque;
        [Abide.Guerilla.Tags.FieldAttribute("max torque", typeof(Single))]
        public Single MaxTorque;
        [Abide.Guerilla.Tags.FieldAttribute("peak torque scale", typeof(Single))]
        public Single PeakTorqueScale;
        [Abide.Guerilla.Tags.FieldAttribute("past peak torque exponent", typeof(Single))]
        public Single PastPeakTorqueExponent;
        [Abide.Guerilla.Tags.FieldAttribute("torque at max angular velocity#generally 0 for loading torque and something less " +
            "than max torque for cruising torque", typeof(Single))]
        public Single TorqueAtMaxAngularVelocity;
        [Abide.Guerilla.Tags.FieldAttribute("torque at 2x max angular velocity", typeof(Single))]
        public Single TorqueAt2xMaxAngularVelocity;
    }
}
#pragma warning restore CS1591