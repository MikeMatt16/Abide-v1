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
    public class ShaderPassImplementationBlock
    {
        [Abide.Guerilla.Tags.FieldAttribute("Flags", typeof(Int16))]
        [Abide.Guerilla.Tags.OptionsAttribute(typeof(FlagsOptions), true)]
        public Int16 Flags;
        [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
        [Abide.Guerilla.Tags.PaddingAttribute(2)]
        public Byte[] EmptyString;
        [Abide.Guerilla.Tags.FieldAttribute("Textures", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Texture Stage", 8, typeof(ShaderPassTextureBlock))]
        public TagBlock Textures;
        [Abide.Guerilla.Tags.FieldAttribute("Vertex Shader", typeof(TagReference))]
        public TagReference VertexShader1;
        [Abide.Guerilla.Tags.FieldAttribute("vs Constants", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Vs Constant", 32, typeof(ShaderPassVertexShaderConstantBlock))]
        public TagBlock VsConstants;
        [Abide.Guerilla.Tags.FieldAttribute("Pixel Shader Code [NO LONGER USED]", typeof(Byte[]))]
        public Byte[] PixelShaderCodeNoLongerUsed;
        [Abide.Guerilla.Tags.FieldAttribute("channels", typeof(Int16))]
        [Abide.Guerilla.Tags.OptionsAttribute(typeof(ChannelsOptions), false)]
        public Int16 Channels;
        [Abide.Guerilla.Tags.FieldAttribute("alpha-blend", typeof(Int16))]
        [Abide.Guerilla.Tags.OptionsAttribute(typeof(AlphablendOptions), false)]
        public Int16 Alphablend;
        [Abide.Guerilla.Tags.FieldAttribute("depth", typeof(Int16))]
        [Abide.Guerilla.Tags.OptionsAttribute(typeof(DepthOptions), false)]
        public Int16 Depth;
        [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
        [Abide.Guerilla.Tags.PaddingAttribute(2)]
        public Byte[] EmptyString3;
        [Abide.Guerilla.Tags.FieldAttribute("channel state", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Channels", 1, typeof(ShaderStateChannelsStateBlock))]
        public TagBlock ChannelState;
        [Abide.Guerilla.Tags.FieldAttribute("alpha-blend state", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Alpha-blend State", 1, typeof(ShaderStateAlphaBlendStateBlock))]
        public TagBlock AlphablendState;
        [Abide.Guerilla.Tags.FieldAttribute("alpha-test state", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Alpha-test State", 1, typeof(ShaderStateAlphaTestStateBlock))]
        public TagBlock AlphatestState;
        [Abide.Guerilla.Tags.FieldAttribute("depth state", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Depth State", 1, typeof(ShaderStateDepthStateBlock))]
        public TagBlock DepthState;
        [Abide.Guerilla.Tags.FieldAttribute("cull state", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Cull State", 1, typeof(ShaderStateCullStateBlock))]
        public TagBlock CullState;
        [Abide.Guerilla.Tags.FieldAttribute("fill state", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Fill State", 1, typeof(ShaderStateFillStateBlock))]
        public TagBlock FillState;
        [Abide.Guerilla.Tags.FieldAttribute("misc state", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Misc State", 1, typeof(ShaderStateMiscStateBlock))]
        public TagBlock MiscState;
        [Abide.Guerilla.Tags.FieldAttribute("constants", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Render State Constant", 7, typeof(ShaderStateConstantBlock))]
        public TagBlock Constants;
        [Abide.Guerilla.Tags.FieldAttribute("Pixel Shader", typeof(TagReference))]
        public TagReference PixelShader;
        public enum FlagsOptions
        {
            DeleteFromCacheFile = 1,
            Critical = 2,
        }
        public enum ChannelsOptions
        {
            All = 0,
            ColorOnly = 1,
            AlphaOnly = 2,
            Custom = 3,
        }
        public enum AlphablendOptions
        {
            Disabled = 0,
            Add = 1,
            Multiply = 2,
            AddSrcTimesDstalpha = 3,
            AddSrcTimesSrcalpha = 4,
            AddDstTimesSrcalphaInverse = 5,
            AlphaBlend = 6,
            Custom = 7,
        }
        public enum DepthOptions
        {
            Disabled = 0,
            DefaultOpaque = 1,
            DefaultOpaqueWrite = 2,
            DefaultTransparent = 3,
            Custom = 4,
        }
    }
}
#pragma warning restore CS1591