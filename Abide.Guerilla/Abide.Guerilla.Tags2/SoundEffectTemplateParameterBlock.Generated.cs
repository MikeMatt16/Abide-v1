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
    
    [Abide.Guerilla.Tags.FieldSetAttribute(40, 4)]
    public class SoundEffectTemplateParameterBlock
    {
        [Abide.Guerilla.Tags.FieldAttribute("name", typeof(StringId))]
        public StringId Name;
        [Abide.Guerilla.Tags.FieldAttribute("type", typeof(Int16))]
        [Abide.Guerilla.Tags.OptionsAttribute(typeof(TypeOptions), false)]
        public Int16 Type;
        [Abide.Guerilla.Tags.FieldAttribute("flags", typeof(Int16))]
        [Abide.Guerilla.Tags.OptionsAttribute(typeof(FlagsOptions), true)]
        public Int16 Flags;
        [Abide.Guerilla.Tags.FieldAttribute("hardware offset", typeof(Int32))]
        public Int32 HardwareOffset;
        [Abide.Guerilla.Tags.FieldAttribute("default enum integer value", typeof(Int32))]
        public Int32 DefaultEnumIntegerValue;
        [Abide.Guerilla.Tags.FieldAttribute("default scalar value", typeof(Single))]
        public Single DefaultScalarValue;
        [Abide.Guerilla.Tags.FieldAttribute("default function", typeof(MappingFunctionBlock))]
        public MappingFunctionBlock DefaultFunction1;
        [Abide.Guerilla.Tags.FieldAttribute("minimum scalar value", typeof(Single))]
        public Single MinimumScalarValue;
        [Abide.Guerilla.Tags.FieldAttribute("maximum scalar value", typeof(Single))]
        public Single MaximumScalarValue;
        public enum TypeOptions
        {
            Integer = 0,
            Real = 1,
            FilterType = 2,
        }
        public enum FlagsOptions
        {
            ExposeAsFunction = 1,
        }
    }
}
#pragma warning restore CS1591