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
    
    [Abide.Guerilla.Tags.FieldSetAttribute(76, 4)]
    [Abide.Guerilla.Tags.TagGroupAttribute("colony", "coln", "����", typeof(ColonyBlock))]
    public class ColonyBlock
    {
        [Abide.Guerilla.Tags.FieldAttribute("flags", typeof(Int16))]
        [Abide.Guerilla.Tags.OptionsAttribute(typeof(FlagsOptions), true)]
        public Int16 Flags;
        [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
        [Abide.Guerilla.Tags.PaddingAttribute(2)]
        public Byte[] EmptyString;
        [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
        [Abide.Guerilla.Tags.PaddingAttribute(4)]
        public Byte[] EmptyString1;
        [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
        [Abide.Guerilla.Tags.PaddingAttribute(12)]
        public Byte[] EmptyString2;
        [Abide.Guerilla.Tags.FieldAttribute("debug color", typeof(ColorArgbF))]
        public ColorArgbF DebugColor;
        [Abide.Guerilla.Tags.FieldAttribute("base map", typeof(TagReference))]
        public TagReference BaseMap;
        [Abide.Guerilla.Tags.FieldAttribute("detail map", typeof(TagReference))]
        public TagReference DetailMap;
        public enum FlagsOptions
        {
            Unused = 1,
        }
    }
}
#pragma warning restore CS1591