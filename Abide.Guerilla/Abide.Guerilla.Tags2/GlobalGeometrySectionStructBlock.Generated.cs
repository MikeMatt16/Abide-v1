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
    
    [Abide.Guerilla.Tags.FieldSetAttribute(108, 4)]
    public class GlobalGeometrySectionStructBlock
    {
        [Abide.Guerilla.Tags.FieldAttribute("Parts*", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Part", 255, typeof(GlobalGeometryPartBlockNew))]
        public TagBlock Parts;
        [Abide.Guerilla.Tags.FieldAttribute("Subparts*", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Subparts", 32768, typeof(GlobalSubpartsBlock))]
        public TagBlock Subparts;
        [Abide.Guerilla.Tags.FieldAttribute("Visibility Bounds*", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Visibility Bounds", 32768, typeof(GlobalVisibilityBoundsBlock))]
        public TagBlock VisibilityBounds;
        [Abide.Guerilla.Tags.FieldAttribute("Raw Vertices*", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Vertex", 32767, typeof(GlobalGeometrySectionRawVertexBlock))]
        public TagBlock RawVertices;
        [Abide.Guerilla.Tags.FieldAttribute("Strip Indices*", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Index", 65535, typeof(GlobalGeometrySectionStripIndexBlock))]
        public TagBlock StripIndices;
        [Abide.Guerilla.Tags.FieldAttribute("Visibility mopp Code*", typeof(Byte[]))]
        public Byte[] VisibilityMoppCode;
        [Abide.Guerilla.Tags.FieldAttribute("mopp Reorder Table*", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Index", 65535, typeof(GlobalGeometrySectionStripIndexBlock))]
        public TagBlock MoppReorderTable;
        [Abide.Guerilla.Tags.FieldAttribute("Vertex Buffers*", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Vertex Buffer", 512, typeof(GlobalGeometrySectionVertexBufferBlock))]
        public TagBlock VertexBuffers;
        [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
        [Abide.Guerilla.Tags.PaddingAttribute(4)]
        public Byte[] EmptyString;
    }
}
#pragma warning restore CS1591