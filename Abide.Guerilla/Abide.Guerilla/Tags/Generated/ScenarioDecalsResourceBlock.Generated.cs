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

namespace Abide.Guerilla.Tags
{
    using Abide.Guerilla.Types;
    using Abide.HaloLibrary;
    using System;
    
    [Abide.Guerilla.Tags.FieldSetAttribute(24, 4)]
    [Abide.Guerilla.Tags.TagGroupAttribute("scenario_decals_resource", 1684366122u, 4294967293u, typeof(ScenarioDecalsResourceBlock))]
    public sealed class ScenarioDecalsResourceBlock : Abide.Guerilla.Tags.IReadable, Abide.Guerilla.Tags.IWritable
    {
        [Abide.Guerilla.Tags.FieldAttribute("Palette", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("scenario_decal_palette_block", 128, typeof(ScenarioDecalPaletteBlock))]
        public TagBlock Palette;
        [Abide.Guerilla.Tags.FieldAttribute("Decals", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("scenario_decals_block", 65536, typeof(ScenarioDecalsBlock))]
        public TagBlock Decals;
        public int Size
        {
            get
            {
                return 24;
            }
        }
        public void Initialize()
        {
        }
        public void Read(System.IO.BinaryReader reader)
        {
        }
        public void Write(System.IO.BinaryWriter writer)
        {
        }
        [Abide.Guerilla.Tags.FieldSetAttribute(16, 4)]
        public sealed class ScenarioDecalPaletteBlock : Abide.Guerilla.Tags.IReadable, Abide.Guerilla.Tags.IWritable
        {
            [Abide.Guerilla.Tags.FieldAttribute("Reference^", typeof(TagReference))]
            public TagReference Reference;
            public int Size
            {
                get
                {
                    return 16;
                }
            }
            public void Initialize()
            {
            }
            public void Read(System.IO.BinaryReader reader)
            {
            }
            public void Write(System.IO.BinaryWriter writer)
            {
            }
        }
        [Abide.Guerilla.Tags.FieldSetAttribute(16, 4)]
        public sealed class ScenarioDecalsBlock : Abide.Guerilla.Tags.IReadable, Abide.Guerilla.Tags.IWritable
        {
            [Abide.Guerilla.Tags.FieldAttribute("Decal Type^", typeof(Int16))]
            public Int16 DecalType;
            [Abide.Guerilla.Tags.FieldAttribute("Yaw[-127,127]*", typeof(Byte))]
            public Byte Yaw127127;
            [Abide.Guerilla.Tags.FieldAttribute("Pitch[-127,127]*", typeof(Byte))]
            public Byte Pitch127127;
            [Abide.Guerilla.Tags.FieldAttribute("Position*", typeof(Vector3))]
            public Vector3 Position;
            public int Size
            {
                get
                {
                    return 16;
                }
            }
            public void Initialize()
            {
            }
            public void Read(System.IO.BinaryReader reader)
            {
            }
            public void Write(System.IO.BinaryWriter writer)
            {
            }
        }
    }
}
#pragma warning restore CS1591