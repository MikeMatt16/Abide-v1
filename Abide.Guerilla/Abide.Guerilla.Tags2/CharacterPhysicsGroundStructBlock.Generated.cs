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
    
    [Abide.Guerilla.Tags.FieldSetAttribute(48, 4)]
    public class CharacterPhysicsGroundStructBlock
    {
        [Abide.Guerilla.Tags.FieldAttribute("maximum slope angle:degrees", typeof(Single))]
        public Single MaximumSlopeAngle;
        [Abide.Guerilla.Tags.FieldAttribute("downhill falloff angle:degrees", typeof(Single))]
        public Single DownhillFalloffAngle;
        [Abide.Guerilla.Tags.FieldAttribute("downhill cutoff angle:degrees", typeof(Single))]
        public Single DownhillCutoffAngle;
        [Abide.Guerilla.Tags.FieldAttribute("uphill falloff angle:degrees", typeof(Single))]
        public Single UphillFalloffAngle;
        [Abide.Guerilla.Tags.FieldAttribute("uphill cutoff angle:degrees", typeof(Single))]
        public Single UphillCutoffAngle;
        [Abide.Guerilla.Tags.FieldAttribute("downhill velocity scale", typeof(Single))]
        public Single DownhillVelocityScale;
        [Abide.Guerilla.Tags.FieldAttribute("uphill velocity scale", typeof(Single))]
        public Single UphillVelocityScale;
        [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
        [Abide.Guerilla.Tags.PaddingAttribute(20)]
        public Byte[] EmptyString2;
    }
}
#pragma warning restore CS1591