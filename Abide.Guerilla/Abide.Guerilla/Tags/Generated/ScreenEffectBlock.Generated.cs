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
    
    [Abide.Guerilla.Tags.FieldSetAttribute(156, 4)]
    [Abide.Guerilla.Tags.TagGroupAttribute("screen_effect", 1701277554u, 4294967293u, typeof(ScreenEffectBlock))]
    public sealed class ScreenEffectBlock : Abide.Guerilla.Tags.IReadable, Abide.Guerilla.Tags.IWritable
    {
        [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
        [Abide.Guerilla.Tags.PaddingAttribute(64)]
        public Byte[] EmptyString;
        [Abide.Guerilla.Tags.FieldAttribute("shader", typeof(TagReference))]
        public TagReference Shader;
        [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
        [Abide.Guerilla.Tags.PaddingAttribute(64)]
        public Byte[] EmptyString1;
        [Abide.Guerilla.Tags.FieldAttribute("pass references", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("pass reference", 8, typeof(RasterizerScreenEffectPassReferenceBlock))]
        public TagBlock PassReferences;
        public int Size
        {
            get
            {
                return 156;
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
        [Abide.Guerilla.Tags.FieldSetAttribute(192, 4)]
        public sealed class RasterizerScreenEffectPassReferenceBlock : Abide.Guerilla.Tags.IReadable, Abide.Guerilla.Tags.IWritable
        {
            [Abide.Guerilla.Tags.FieldAttribute("layer pass index*:leave as -1 unless debugging", typeof(Int16))]
            public Int16 LayerPassIndex;
            [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
            [Abide.Guerilla.Tags.PaddingAttribute(2)]
            public Byte[] EmptyString;
            [Abide.Guerilla.Tags.FieldAttribute("primary=0 and secondary=0:implementation index", typeof(Byte))]
            public Byte PrimaryEquals0AndSecondaryEquals0;
            [Abide.Guerilla.Tags.FieldAttribute("primary>0 and secondary=0:implementation index", typeof(Byte))]
            public Byte PrimaryGreaterThan0AndSecondaryEquals0;
            [Abide.Guerilla.Tags.FieldAttribute("primary=0 and secondary>0:implementation index", typeof(Byte))]
            public Byte PrimaryEquals0AndSecondaryGreaterThan0;
            [Abide.Guerilla.Tags.FieldAttribute("primary>0 and secondary>0:implementation index", typeof(Byte))]
            public Byte PrimaryGreaterThan0AndSecondaryGreaterThan0;
            [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
            [Abide.Guerilla.Tags.PaddingAttribute(64)]
            public Byte[] EmptyString1;
            [Abide.Guerilla.Tags.FieldAttribute("stage 0 mode", typeof(Int16))]
            [Abide.Guerilla.Tags.OptionsAttribute(typeof(Stage0ModeOptions), false)]
            public Int16 Stage0Mode;
            [Abide.Guerilla.Tags.FieldAttribute("stage 1 mode", typeof(Int16))]
            [Abide.Guerilla.Tags.OptionsAttribute(typeof(Stage1ModeOptions), false)]
            public Int16 Stage1Mode;
            [Abide.Guerilla.Tags.FieldAttribute("stage 2 mode", typeof(Int16))]
            [Abide.Guerilla.Tags.OptionsAttribute(typeof(Stage2ModeOptions), false)]
            public Int16 Stage2Mode;
            [Abide.Guerilla.Tags.FieldAttribute("stage 3 mode", typeof(Int16))]
            [Abide.Guerilla.Tags.OptionsAttribute(typeof(Stage3ModeOptions), false)]
            public Int16 Stage3Mode;
            [Abide.Guerilla.Tags.FieldAttribute("advanced control", typeof(TagBlock))]
            [Abide.Guerilla.Tags.BlockAttribute("advanced control", 1, typeof(RasterizerScreenEffectTexcoordGenerationAdvancedControlBlock))]
            public TagBlock AdvancedControl;
            [Abide.Guerilla.Tags.FieldAttribute("target", typeof(Int16))]
            [Abide.Guerilla.Tags.OptionsAttribute(typeof(TargetOptions), false)]
            public Int16 Target1;
            [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
            [Abide.Guerilla.Tags.PaddingAttribute(2)]
            public Byte[] EmptyString2;
            [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
            [Abide.Guerilla.Tags.PaddingAttribute(64)]
            public Byte[] EmptyString3;
            [Abide.Guerilla.Tags.FieldAttribute("convolution", typeof(TagBlock))]
            [Abide.Guerilla.Tags.BlockAttribute("convolution", 2, typeof(RasterizerScreenEffectConvolutionBlock))]
            public TagBlock Convolution;
            public int Size
            {
                get
                {
                    return 192;
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
            [Abide.Guerilla.Tags.FieldSetAttribute(72, 4)]
            public sealed class RasterizerScreenEffectTexcoordGenerationAdvancedControlBlock : Abide.Guerilla.Tags.IReadable, Abide.Guerilla.Tags.IWritable
            {
                [Abide.Guerilla.Tags.FieldAttribute("stage 0 flags", typeof(Int16))]
                [Abide.Guerilla.Tags.OptionsAttribute(typeof(Stage0FlagsOptions), true)]
                public Int16 Stage0Flags;
                [Abide.Guerilla.Tags.FieldAttribute("stage 1 flags", typeof(Int16))]
                [Abide.Guerilla.Tags.OptionsAttribute(typeof(Stage1FlagsOptions), true)]
                public Int16 Stage1Flags;
                [Abide.Guerilla.Tags.FieldAttribute("stage 2 flags", typeof(Int16))]
                [Abide.Guerilla.Tags.OptionsAttribute(typeof(Stage2FlagsOptions), true)]
                public Int16 Stage2Flags;
                [Abide.Guerilla.Tags.FieldAttribute("stage 3 flags", typeof(Int16))]
                [Abide.Guerilla.Tags.OptionsAttribute(typeof(Stage3FlagsOptions), true)]
                public Int16 Stage3Flags;
                [Abide.Guerilla.Tags.FieldAttribute("stage 0 offset", typeof(Vector4))]
                public Vector4 Stage0Offset;
                [Abide.Guerilla.Tags.FieldAttribute("stage 1 offset", typeof(Vector4))]
                public Vector4 Stage1Offset;
                [Abide.Guerilla.Tags.FieldAttribute("stage 2 offset", typeof(Vector4))]
                public Vector4 Stage2Offset;
                [Abide.Guerilla.Tags.FieldAttribute("stage 3 offset", typeof(Vector4))]
                public Vector4 Stage3Offset;
                public int Size
                {
                    get
                    {
                        return 72;
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
                public enum Stage0FlagsOptions
                {
                    XyScaledByZFar = 1,
                }
                public enum Stage1FlagsOptions
                {
                    XyScaledByZFar = 1,
                }
                public enum Stage2FlagsOptions
                {
                    XyScaledByZFar = 1,
                }
                public enum Stage3FlagsOptions
                {
                    XyScaledByZFar = 1,
                }
            }
            [Abide.Guerilla.Tags.FieldSetAttribute(92, 4)]
            public sealed class RasterizerScreenEffectConvolutionBlock : Abide.Guerilla.Tags.IReadable, Abide.Guerilla.Tags.IWritable
            {
                [Abide.Guerilla.Tags.FieldAttribute("flags", typeof(Int16))]
                [Abide.Guerilla.Tags.OptionsAttribute(typeof(FlagsOptions), true)]
                public Int16 Flags;
                [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
                [Abide.Guerilla.Tags.PaddingAttribute(2)]
                public Byte[] EmptyString;
                [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
                [Abide.Guerilla.Tags.PaddingAttribute(64)]
                public Byte[] EmptyString1;
                [Abide.Guerilla.Tags.FieldAttribute("convolution amount:[0,+inf)", typeof(Single))]
                public Single ConvolutionAmount;
                [Abide.Guerilla.Tags.FieldAttribute("filter scale", typeof(Single))]
                public Single FilterScale;
                [Abide.Guerilla.Tags.FieldAttribute("filter box factor:[0,1] not used for zoom", typeof(Single))]
                public Single FilterBoxFactor;
                [Abide.Guerilla.Tags.FieldAttribute("zoom falloff radius", typeof(Single))]
                public Single ZoomFalloffRadius;
                [Abide.Guerilla.Tags.FieldAttribute("zoom cutoff radius", typeof(Single))]
                public Single ZoomCutoffRadius;
                [Abide.Guerilla.Tags.FieldAttribute("resolution scale:[0,1]", typeof(Single))]
                public Single ResolutionScale;
                public int Size
                {
                    get
                    {
                        return 92;
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
                public enum FlagsOptions
                {
                    OnlyWhenPrimaryIsActive = 1,
                    OnlyWhenSecondaryIsActive = 2,
                    PredatorZoom = 4,
                }
            }
            public enum Stage0ModeOptions
            {
                Default = 0,
                ViewportNormalized = 1,
                ViewportRelative = 2,
                FramebufferRelative = 3,
                Zero = 4,
            }
            public enum Stage1ModeOptions
            {
                Default = 0,
                ViewportNormalized = 1,
                ViewportRelative = 2,
                FramebufferRelative = 3,
                Zero = 4,
            }
            public enum Stage2ModeOptions
            {
                Default = 0,
                ViewportNormalized = 1,
                ViewportRelative = 2,
                FramebufferRelative = 3,
                Zero = 4,
            }
            public enum Stage3ModeOptions
            {
                Default = 0,
                ViewportNormalized = 1,
                ViewportRelative = 2,
                FramebufferRelative = 3,
                Zero = 4,
            }
            public enum TargetOptions
            {
                Framebuffer = 0,
                Texaccum = 1,
                TexaccumSmall = 2,
                TexaccumTiny = 3,
                CopyFbToTexaccum = 4,
            }
        }
    }
}
#pragma warning restore CS1591