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
    
    [Abide.Guerilla.Tags.FieldSetAttribute(136, 4)]
    public class AnimationPoolBlock
    {
        [Abide.Guerilla.Tags.FieldAttribute("name*^", typeof(StringId))]
        public StringId Name;
        [Abide.Guerilla.Tags.FieldAttribute("node list checksum*", typeof(Int32))]
        public Int32 NodeListChecksum;
        [Abide.Guerilla.Tags.FieldAttribute("production checksum*", typeof(Int32))]
        public Int32 ProductionChecksum;
        [Abide.Guerilla.Tags.FieldAttribute("import_checksum*", typeof(Int32))]
        public Int32 ImportChecksum;
        [Abide.Guerilla.Tags.FieldAttribute("type*", typeof(Byte))]
        [Abide.Guerilla.Tags.OptionsAttribute(typeof(Type), false)]
        public Byte Type1;
        [Abide.Guerilla.Tags.FieldAttribute("frame info type*", typeof(Byte))]
        [Abide.Guerilla.Tags.OptionsAttribute(typeof(FrameInfoType), false)]
        public Byte FrameInfoType1;
        [Abide.Guerilla.Tags.FieldAttribute("blend screen", typeof(Byte))]
        public Byte BlendScreen;
        [Abide.Guerilla.Tags.FieldAttribute("node count*", typeof(Byte))]
        public Byte NodeCount;
        [Abide.Guerilla.Tags.FieldAttribute("frame count*", typeof(Int16))]
        public Int16 FrameCount;
        [Abide.Guerilla.Tags.FieldAttribute("internal flags*", typeof(Byte))]
        [Abide.Guerilla.Tags.OptionsAttribute(typeof(InternalFlags), true)]
        public Byte InternalFlags1;
        [Abide.Guerilla.Tags.FieldAttribute("production flags", typeof(Byte))]
        [Abide.Guerilla.Tags.OptionsAttribute(typeof(ProductionFlagsOptions), true)]
        public Byte ProductionFlags;
        [Abide.Guerilla.Tags.FieldAttribute("playback flags", typeof(Int16))]
        [Abide.Guerilla.Tags.OptionsAttribute(typeof(PlaybackFlagsOptions), true)]
        public Int16 PlaybackFlags;
        [Abide.Guerilla.Tags.FieldAttribute("desired compression", typeof(Byte))]
        [Abide.Guerilla.Tags.OptionsAttribute(typeof(DesiredCompressionOptions), false)]
        public Byte DesiredCompression;
        [Abide.Guerilla.Tags.FieldAttribute("current compression*", typeof(Byte))]
        [Abide.Guerilla.Tags.OptionsAttribute(typeof(CurrentCompression), false)]
        public Byte CurrentCompression1;
        [Abide.Guerilla.Tags.FieldAttribute("weight", typeof(Single))]
        public Single Weight;
        [Abide.Guerilla.Tags.FieldAttribute("loop frame index", typeof(Int16))]
        public Int16 LoopFrameIndex;
        [Abide.Guerilla.Tags.FieldAttribute("", typeof(Int16))]
        public Int16 EmptyString;
        [Abide.Guerilla.Tags.FieldAttribute("", typeof(Int16))]
        public Int16 EmptyString1;
        [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
        [Abide.Guerilla.Tags.PaddingAttribute(2)]
        public Byte[] EmptyString2;
        [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
        public Byte[] EmptyString3;
        [Abide.Guerilla.Tags.FieldAttribute("", typeof(PackedDataSizesStructBlock))]
        public PackedDataSizesStructBlock EmptyString4;
        [Abide.Guerilla.Tags.FieldAttribute("frame events|ABCDCC", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Animation Frame Event Block", 512, typeof(AnimationFrameEventBlock))]
        public TagBlock FrameEventsabcdcc;
        [Abide.Guerilla.Tags.FieldAttribute("sound events|ABCDCC", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Animation Sound Event Block", 512, typeof(AnimationSoundEventBlock))]
        public TagBlock SoundEventsabcdcc;
        [Abide.Guerilla.Tags.FieldAttribute("effect events|ABCDCC", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Animation Effect Event Block", 512, typeof(AnimationEffectEventBlock))]
        public TagBlock EffectEventsabcdcc;
        [Abide.Guerilla.Tags.FieldAttribute("object-space parent nodes|ABCDCC", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Object Space Node Data Block", 255, typeof(ObjectSpaceNodeDataBlock))]
        public TagBlock ObjectspaceParentNodesabcdcc;
        public enum Type
        {
            Base = 0,
            Overlay = 1,
            Replacement = 2,
        }
        public enum FrameInfoType
        {
            None = 0,
            Dxdy = 1,
            Dxdydyaw = 2,
            Dxdydzdyaw = 3,
        }
        public enum InternalFlags
        {
            LessThanUnused0GreaterThan = 1,
            WorldRelative = 2,
            LessThanUnused1GreaterThan = 4,
            LessThanUnused2GreaterThan = 8,
            LessThanUnused3GreaterThan = 16,
            CompressionDisabled = 32,
            OldProductionChecksum = 64,
            ValidProductionChecksum = 128,
        }
        public enum ProductionFlagsOptions
        {
            DoNotMonitorChanges = 1,
            VerifySoundEvents = 2,
            DoNotInheritForPlayerGraphs = 4,
        }
        public enum PlaybackFlagsOptions
        {
            DisableInterpolationIn = 1,
            DisableInterpolationOut = 2,
            DisableModeIk = 4,
            DisableWeaponIk = 8,
            DisableWeaponAim1stPerson = 16,
            DisableLookScreen = 32,
            DisableTransitionAdjustment = 64,
        }
        public enum DesiredCompressionOptions
        {
            BestScore = 0,
            BestCompression = 1,
            BestAccuracy = 2,
            BestFullframe = 3,
            BestSmallKeyframe = 4,
            BestLargeKeyframe = 5,
        }
        public enum CurrentCompression
        {
            BestScore = 0,
            BestCompression = 1,
            BestAccuracy = 2,
            BestFullframe = 3,
            BestSmallKeyframe = 4,
            BestLargeKeyframe = 5,
        }
    }
}
#pragma warning restore CS1591