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
    
    [Abide.Guerilla.Tags.FieldSetAttribute(52, 4)]
    public class GlobalShaderParameterBlock
    {
        [Abide.Guerilla.Tags.FieldAttribute("Name^", typeof(StringId))]
        public StringId Name;
        [Abide.Guerilla.Tags.FieldAttribute("Type", typeof(Int16))]
        [Abide.Guerilla.Tags.OptionsAttribute(typeof(TypeOptions), false)]
        public Int16 Type;
        [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
        [Abide.Guerilla.Tags.PaddingAttribute(2)]
        public Byte[] EmptyString;
        [Abide.Guerilla.Tags.FieldAttribute("Bitmap", typeof(TagReference))]
        public TagReference Bitmap;
        [Abide.Guerilla.Tags.FieldAttribute("Const Value", typeof(Single))]
        public Single ConstValue;
        [Abide.Guerilla.Tags.FieldAttribute("Const Color", typeof(ColorRgbF))]
        public ColorRgbF ConstColor;
        [Abide.Guerilla.Tags.FieldAttribute("Animation Properties", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Animation Property", 14, typeof(ShaderAnimationPropertyBlock))]
        public TagBlock AnimationProperties;
        public enum TypeOptions
        {
            Bitmap = 0,
            Value = 1,
            Color = 2,
            Switch = 3,
        }
    }
}
#pragma warning restore CS1591