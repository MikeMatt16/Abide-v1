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
    
    [Abide.Guerilla.Tags.FieldSetAttribute(184, 4)]
    public class ShaderPostprocessDefinitionNewBlock
    {
        [Abide.Guerilla.Tags.FieldAttribute("shader template index", typeof(Int32))]
        public Int32 ShaderTemplateIndex;
        [Abide.Guerilla.Tags.FieldAttribute("bitmaps", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Shader Postprocess Bitmap New Block", 1024, typeof(ShaderPostprocessBitmapNewBlock))]
        public TagBlock Bitmaps;
        [Abide.Guerilla.Tags.FieldAttribute("pixel constants", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Pixel32 Block", 1024, typeof(Pixel32Block))]
        public TagBlock PixelConstants;
        [Abide.Guerilla.Tags.FieldAttribute("vertex constants", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Real Vector4d Block", 1024, typeof(RealVector4dBlock))]
        public TagBlock VertexConstants;
        [Abide.Guerilla.Tags.FieldAttribute("levels of detail", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Shader Postprocess Level Of Detail New Block", 1024, typeof(ShaderPostprocessLevelOfDetailNewBlock))]
        public TagBlock LevelsOfDetail;
        [Abide.Guerilla.Tags.FieldAttribute("layers", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Tag Block Index Block", 1024, typeof(TagBlockIndexBlock))]
        public TagBlock Layers;
        [Abide.Guerilla.Tags.FieldAttribute("passes", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Tag Block Index Block", 1024, typeof(TagBlockIndexBlock))]
        public TagBlock Passes;
        [Abide.Guerilla.Tags.FieldAttribute("implementations", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Shader Postprocess Implementation New Block", 1024, typeof(ShaderPostprocessImplementationNewBlock))]
        public TagBlock Implementations;
        [Abide.Guerilla.Tags.FieldAttribute("overlays", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Shader Postprocess Overlay New Block", 1024, typeof(ShaderPostprocessOverlayNewBlock))]
        public TagBlock Overlays;
        [Abide.Guerilla.Tags.FieldAttribute("overlay references", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Shader Postprocess Overlay Reference New Block", 1024, typeof(ShaderPostprocessOverlayReferenceNewBlock))]
        public TagBlock OverlayReferences;
        [Abide.Guerilla.Tags.FieldAttribute("animated parameters", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Shader Postprocess Animated Parameter New Block", 1024, typeof(ShaderPostprocessAnimatedParameterNewBlock))]
        public TagBlock AnimatedParameters;
        [Abide.Guerilla.Tags.FieldAttribute("animated parameter references", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Shader Postprocess Animated Parameter Reference New Block", 1024, typeof(ShaderPostprocessAnimatedParameterReferenceNewBlock))]
        public TagBlock AnimatedParameterReferences;
        [Abide.Guerilla.Tags.FieldAttribute("bitmap properties", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Shader Postprocess Bitmap Property Block", 5, typeof(ShaderPostprocessBitmapPropertyBlock))]
        public TagBlock BitmapProperties;
        [Abide.Guerilla.Tags.FieldAttribute("color properties", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Shader Postprocess Color Property Block", 2, typeof(ShaderPostprocessColorPropertyBlock))]
        public TagBlock ColorProperties;
        [Abide.Guerilla.Tags.FieldAttribute("value properties", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Shader Postprocess Value Property Block", 6, typeof(ShaderPostprocessValuePropertyBlock))]
        public TagBlock ValueProperties;
        [Abide.Guerilla.Tags.FieldAttribute("old levels of detail", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Shader Postprocess Level Of Detail Block", 1024, typeof(ShaderPostprocessLevelOfDetailBlock))]
        public TagBlock OldLevelsOfDetail;
    }
}
#pragma warning restore CS1591