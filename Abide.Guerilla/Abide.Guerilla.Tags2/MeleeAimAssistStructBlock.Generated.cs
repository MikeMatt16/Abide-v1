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
    public class MeleeAimAssistStructBlock
    {
        [Abide.Guerilla.Tags.FieldAttribute("magnetism angle:degrees#the maximum angle that magnetism works at full strength", typeof(Single))]
        public Single MagnetismAngle;
        [Abide.Guerilla.Tags.FieldAttribute("magnetism range:world units#the maximum distance that magnetism works at full str" +
            "ength", typeof(Single))]
        public Single MagnetismRange;
        [Abide.Guerilla.Tags.FieldAttribute("throttle magnitude", typeof(Single))]
        public Single ThrottleMagnitude;
        [Abide.Guerilla.Tags.FieldAttribute("throttle minimum distance", typeof(Single))]
        public Single ThrottleMinimumDistance;
        [Abide.Guerilla.Tags.FieldAttribute("throttle maximum adjustment angle:degrees", typeof(Single))]
        public Single ThrottleMaximumAdjustmentAngle;
    }
}
#pragma warning restore CS1591