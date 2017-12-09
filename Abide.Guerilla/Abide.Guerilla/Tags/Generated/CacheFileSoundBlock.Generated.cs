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
    using System.IO;
    
    [FieldSetAttribute(20, 4)]
    [TagGroupAttribute("cache_file_sound", 606282027u, 4294967293u, typeof(CacheFileSoundBlock))]
    public sealed class CacheFileSoundBlock : AbideTagBlock
    {
        [FieldAttribute("flags", typeof(FlagsOptions))]
        [OptionsAttribute(typeof(FlagsOptions), true)]
        public FlagsOptions Flags;
        [FieldAttribute("sound class*", typeof(SoundClassOptions))]
        [OptionsAttribute(typeof(SoundClassOptions), false)]
        public SoundClassOptions SoundClass;
        [FieldAttribute("sample rate*", typeof(SampleRateOptions))]
        [OptionsAttribute(typeof(SampleRateOptions), false)]
        public SampleRateOptions SampleRate;
        [FieldAttribute("encoding*", typeof(EncodingOptions))]
        [OptionsAttribute(typeof(EncodingOptions), false)]
        public EncodingOptions Encoding;
        [FieldAttribute("compression*", typeof(CompressionOptions))]
        [OptionsAttribute(typeof(CompressionOptions), false)]
        public CompressionOptions Compression;
        [FieldAttribute("playback index", typeof(Int16))]
        public Int16 PlaybackIndex;
        [FieldAttribute("first pitch range index", typeof(Int16))]
        public Int16 FirstPitchRangeIndex;
        [FieldAttribute("pitch range count", typeof(Byte))]
        public Byte PitchRangeCount;
        [FieldAttribute("scale index", typeof(Byte))]
        public Byte ScaleIndex;
        [FieldAttribute("promotion index", typeof(Byte))]
        public Byte PromotionIndex;
        [FieldAttribute("custom playback index", typeof(Byte))]
        public Byte CustomPlaybackIndex;
        [FieldAttribute("extra info index", typeof(Int16))]
        public Int16 ExtraInfoIndex;
        [FieldAttribute("maximum play time:ms", typeof(Int32))]
        public Int32 MaximumPlayTime;
        public override int Size
        {
            get
            {
                return 20;
            }
        }
        public override void Initialize()
        {
            this.Flags = ((FlagsOptions)(0));
            this.SoundClass = ((SoundClassOptions)(0));
            this.SampleRate = ((SampleRateOptions)(0));
            this.Encoding = ((EncodingOptions)(0));
            this.Compression = ((CompressionOptions)(0));
            this.PlaybackIndex = 0;
            this.FirstPitchRangeIndex = 0;
            this.PitchRangeCount = 0;
            this.ScaleIndex = 0;
            this.PromotionIndex = 0;
            this.CustomPlaybackIndex = 0;
            this.ExtraInfoIndex = 0;
            this.MaximumPlayTime = 0;
        }
        public override void Read(BinaryReader reader)
        {
            this.Flags = ((FlagsOptions)(reader.ReadInt16()));
            this.SoundClass = ((SoundClassOptions)(reader.ReadByte()));
            this.SampleRate = ((SampleRateOptions)(reader.ReadByte()));
            this.Encoding = ((EncodingOptions)(reader.ReadByte()));
            this.Compression = ((CompressionOptions)(reader.ReadByte()));
            this.PlaybackIndex = reader.ReadInt16();
            this.FirstPitchRangeIndex = reader.ReadInt16();
            this.PitchRangeCount = reader.ReadByte();
            this.ScaleIndex = reader.ReadByte();
            this.PromotionIndex = reader.ReadByte();
            this.CustomPlaybackIndex = reader.ReadByte();
            this.ExtraInfoIndex = reader.ReadInt16();
            this.MaximumPlayTime = reader.ReadInt32();
        }
        public override void Write(BinaryWriter writer)
        {
        }
        public enum FlagsOptions : Int16
        {
            FitToAdpcmBlocksize = 1,
            SplitLongSoundIntoPermutations = 2,
            AlwaysSpatialize = 4,
            NeverObstruct = 8,
            InternalDontTouch = 16,
            UseHugeSoundTransmission = 32,
            LinkCountToOwnerUnit = 64,
            PitchRangeIsLanguage = 128,
            DontUseSoundClassSpeakerFlag = 256,
            DontUseLipsyncData = 512,
        }
        public enum SoundClassOptions : Byte
        {
            ProjectileImpact = 0,
            ProjectileDetonation = 1,
            ProjectileFlyby = 2,
            EmptyString = 3,
            WeaponFire = 4,
            WeaponReady = 5,
            WeaponReload = 6,
            WeaponEmpty = 7,
            WeaponCharge = 8,
            WeaponOverheat = 9,
            WeaponIdle = 10,
            WeaponMelee = 11,
            WeaponAnimation = 12,
            ObjectImpacts = 13,
            ParticleImpacts = 14,
            EmptyString1 = 15,
            EmptyString2 = 16,
            EmptyString3 = 17,
            UnitFootsteps = 18,
            UnitDialog = 19,
            UnitAnimation = 20,
            EmptyString4 = 21,
            VehicleCollision = 22,
            VehicleEngine = 23,
            VehicleAnimation = 24,
            EmptyString5 = 25,
            DeviceDoor = 26,
            EmptyString6 = 27,
            DeviceMachinery = 28,
            DeviceStationary = 29,
            EmptyString7 = 30,
            EmptyString8 = 31,
            Music = 32,
            AmbientNature = 33,
            AmbientMachinery = 34,
            EmptyString9 = 35,
            HugeAss = 36,
            ObjectLooping = 37,
            CinematicMusic = 38,
            EmptyString10 = 39,
            EmptyString11 = 40,
            EmptyString12 = 41,
            EmptyString13 = 42,
            EmptyString14 = 43,
            EmptyString15 = 44,
            CortanaMission = 45,
            CortanaCinematic = 46,
            MissionDialog = 47,
            CinematicDialog = 48,
            ScriptedCinematicFoley = 49,
            GameEvent = 50,
            Ui = 51,
            Test = 52,
            MultilingualTest = 53,
        }
        public enum SampleRateOptions : Byte
        {
            _22khz = 0,
            _44khz = 1,
            _32khz = 2,
        }
        public enum EncodingOptions : Byte
        {
            Mono = 0,
            Stereo = 1,
            Codec = 2,
        }
        public enum CompressionOptions : Byte
        {
            NoneBigEndian = 0,
            XboxAdpcm = 1,
            ImaAdpcm = 2,
            NoneLittleEndian = 3,
            Wma = 4,
        }
    }
}
#pragma warning restore CS1591
