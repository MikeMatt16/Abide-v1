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
    
    [Abide.Guerilla.Tags.FieldSetAttribute(180, 4)]
    [Abide.Guerilla.Tags.TagGroupAttribute("antenna", 1634628641u, 4294967293u, typeof(AntennaBlock))]
    public sealed class AntennaBlock : Abide.Guerilla.Tags.IReadable, Abide.Guerilla.Tags.IWritable
    {
        [Abide.Guerilla.Tags.FieldAttribute("attachment marker name#the marker name where the antenna should be attached", typeof(StringId))]
        public StringId AttachmentMarkerName;
        [Abide.Guerilla.Tags.FieldAttribute("bitmaps", typeof(TagReference))]
        public TagReference Bitmaps;
        [Abide.Guerilla.Tags.FieldAttribute("physics", typeof(TagReference))]
        public TagReference Physics;
        [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
        [Abide.Guerilla.Tags.PaddingAttribute(80)]
        public Byte[] EmptyString;
        [Abide.Guerilla.Tags.FieldAttribute("spring strength coefficient#strength of the spring (larger values make the spring" +
            " stronger)", typeof(Single))]
        public Single SpringStrengthCoefficient;
        [Abide.Guerilla.Tags.FieldAttribute("falloff pixels", typeof(Single))]
        public Single FalloffPixels;
        [Abide.Guerilla.Tags.FieldAttribute("cutoff pixels", typeof(Single))]
        public Single CutoffPixels;
        [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
        [Abide.Guerilla.Tags.PaddingAttribute(40)]
        public Byte[] EmptyString1;
        [Abide.Guerilla.Tags.FieldAttribute("vertices", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("antenna_vertex_block", 20, typeof(AntennaVertexBlock))]
        public TagBlock Vertices;
        public int Size
        {
            get
            {
                return 180;
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
        [Abide.Guerilla.Tags.FieldSetAttribute(128, 4)]
        public sealed class AntennaVertexBlock : Abide.Guerilla.Tags.IReadable, Abide.Guerilla.Tags.IWritable
        {
            [Abide.Guerilla.Tags.FieldAttribute("spring strength coefficient#strength of the spring (larger values make the spring" +
                " stronger)", typeof(Single))]
            public Single SpringStrengthCoefficient;
            [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
            [Abide.Guerilla.Tags.PaddingAttribute(24)]
            public Byte[] EmptyString;
            [Abide.Guerilla.Tags.FieldAttribute("angles#direction toward next vertex", typeof(Vector2))]
            public Vector2 Angles;
            [Abide.Guerilla.Tags.FieldAttribute("length:world units#distance between this vertex and the next", typeof(Single))]
            public Single Length;
            [Abide.Guerilla.Tags.FieldAttribute("sequence index#bitmap group sequence index for this vertex\'s texture", typeof(Int16))]
            public Int16 SequenceIndex;
            [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
            [Abide.Guerilla.Tags.PaddingAttribute(2)]
            public Byte[] EmptyString1;
            [Abide.Guerilla.Tags.FieldAttribute("color#color at this vertex", typeof(ColorArgbF))]
            public ColorArgbF Color;
            [Abide.Guerilla.Tags.FieldAttribute("LOD color#color at this vertex for the low-LOD line primitives", typeof(ColorArgbF))]
            public ColorArgbF LodColor;
            [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
            [Abide.Guerilla.Tags.PaddingAttribute(40)]
            public Byte[] EmptyString2;
            [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
            [Abide.Guerilla.Tags.PaddingAttribute(12)]
            public Byte[] EmptyString3;
            public int Size
            {
                get
                {
                    return 128;
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