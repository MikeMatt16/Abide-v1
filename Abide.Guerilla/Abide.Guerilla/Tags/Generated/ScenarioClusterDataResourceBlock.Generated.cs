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
    
    [Abide.Guerilla.Tags.FieldSetAttribute(60, 4)]
    [Abide.Guerilla.Tags.TagGroupAttribute("scenario_cluster_data_resource", 1668052266u, 4294967293u, typeof(ScenarioClusterDataResourceBlock))]
    public sealed class ScenarioClusterDataResourceBlock : Abide.Guerilla.Tags.IReadable, Abide.Guerilla.Tags.IWritable
    {
        [Abide.Guerilla.Tags.FieldAttribute("Cluster Data", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("scenario_cluster_data_block", 16, typeof(ScenarioClusterDataBlock))]
        public TagBlock ClusterData;
        [Abide.Guerilla.Tags.FieldAttribute("Background Sound Palette", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("structure_bsp_background_sound_palette_block", 64, typeof(StructureBspBackgroundSoundPaletteBlock))]
        public TagBlock BackgroundSoundPalette;
        [Abide.Guerilla.Tags.FieldAttribute("Sound Environment Palette", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("structure_bsp_sound_environment_palette_block", 64, typeof(StructureBspSoundEnvironmentPaletteBlock))]
        public TagBlock SoundEnvironmentPalette;
        [Abide.Guerilla.Tags.FieldAttribute("Weather Palette", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("structure_bsp_weather_palette_block", 32, typeof(StructureBspWeatherPaletteBlock))]
        public TagBlock WeatherPalette;
        [Abide.Guerilla.Tags.FieldAttribute("Atmospheric Fog Palette", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("scenario_atmospheric_fog_palette", 127, typeof(ScenarioAtmosphericFogPalette))]
        public TagBlock AtmosphericFogPalette;
        public int Size
        {
            get
            {
                return 60;
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
        [Abide.Guerilla.Tags.FieldSetAttribute(80, 4)]
        public sealed class ScenarioClusterDataBlock : Abide.Guerilla.Tags.IReadable, Abide.Guerilla.Tags.IWritable
        {
            [Abide.Guerilla.Tags.FieldAttribute("BSP*", typeof(TagReference))]
            public TagReference Bsp;
            [Abide.Guerilla.Tags.FieldAttribute("Background Sounds*", typeof(TagBlock))]
            [Abide.Guerilla.Tags.BlockAttribute("scenario_cluster_background_sounds_block", 512, typeof(ScenarioClusterBackgroundSoundsBlock))]
            public TagBlock BackgroundSounds;
            [Abide.Guerilla.Tags.FieldAttribute("Sound Environments*", typeof(TagBlock))]
            [Abide.Guerilla.Tags.BlockAttribute("scenario_cluster_sound_environments_block", 512, typeof(ScenarioClusterSoundEnvironmentsBlock))]
            public TagBlock SoundEnvironments;
            [Abide.Guerilla.Tags.FieldAttribute("BSP Checksum*", typeof(Int32))]
            public Int32 BspChecksum;
            [Abide.Guerilla.Tags.FieldAttribute("Cluster Centroids*", typeof(TagBlock))]
            [Abide.Guerilla.Tags.BlockAttribute("scenario_cluster_points_block", 512, typeof(ScenarioClusterPointsBlock))]
            public TagBlock ClusterCentroids;
            [Abide.Guerilla.Tags.FieldAttribute("Weather Properties*", typeof(TagBlock))]
            [Abide.Guerilla.Tags.BlockAttribute("scenario_cluster_weather_properties_block", 512, typeof(ScenarioClusterWeatherPropertiesBlock))]
            public TagBlock WeatherProperties;
            [Abide.Guerilla.Tags.FieldAttribute("Atmospheric Fog Properties*", typeof(TagBlock))]
            [Abide.Guerilla.Tags.BlockAttribute("scenario_cluster_atmospheric_fog_properties_block", 512, typeof(ScenarioClusterAtmosphericFogPropertiesBlock))]
            public TagBlock AtmosphericFogProperties;
            public int Size
            {
                get
                {
                    return 80;
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
            [Abide.Guerilla.Tags.FieldSetAttribute(4, 4)]
            public sealed class ScenarioClusterBackgroundSoundsBlock : Abide.Guerilla.Tags.IReadable, Abide.Guerilla.Tags.IWritable
            {
                [Abide.Guerilla.Tags.FieldAttribute("Type^", typeof(Int16))]
                public Int16 Type;
                [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
                [Abide.Guerilla.Tags.PaddingAttribute(2)]
                public Byte[] EmptyString;
                public int Size
                {
                    get
                    {
                        return 4;
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
            [Abide.Guerilla.Tags.FieldSetAttribute(4, 4)]
            public sealed class ScenarioClusterSoundEnvironmentsBlock : Abide.Guerilla.Tags.IReadable, Abide.Guerilla.Tags.IWritable
            {
                [Abide.Guerilla.Tags.FieldAttribute("Type^", typeof(Int16))]
                public Int16 Type;
                [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
                [Abide.Guerilla.Tags.PaddingAttribute(2)]
                public Byte[] EmptyString;
                public int Size
                {
                    get
                    {
                        return 4;
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
            [Abide.Guerilla.Tags.FieldSetAttribute(12, 4)]
            public sealed class ScenarioClusterPointsBlock : Abide.Guerilla.Tags.IReadable, Abide.Guerilla.Tags.IWritable
            {
                [Abide.Guerilla.Tags.FieldAttribute("Centroid*", typeof(Vector3))]
                public Vector3 Centroid;
                public int Size
                {
                    get
                    {
                        return 12;
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
            [Abide.Guerilla.Tags.FieldSetAttribute(4, 4)]
            public sealed class ScenarioClusterWeatherPropertiesBlock : Abide.Guerilla.Tags.IReadable, Abide.Guerilla.Tags.IWritable
            {
                [Abide.Guerilla.Tags.FieldAttribute("Type^", typeof(Int16))]
                public Int16 Type;
                [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
                [Abide.Guerilla.Tags.PaddingAttribute(2)]
                public Byte[] EmptyString;
                public int Size
                {
                    get
                    {
                        return 4;
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
            [Abide.Guerilla.Tags.FieldSetAttribute(4, 4)]
            public sealed class ScenarioClusterAtmosphericFogPropertiesBlock : Abide.Guerilla.Tags.IReadable, Abide.Guerilla.Tags.IWritable
            {
                [Abide.Guerilla.Tags.FieldAttribute("Type^", typeof(Int16))]
                public Int16 Type;
                [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
                [Abide.Guerilla.Tags.PaddingAttribute(2)]
                public Byte[] EmptyString;
                public int Size
                {
                    get
                    {
                        return 4;
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
        [Abide.Guerilla.Tags.FieldSetAttribute(116, 4)]
        public sealed class StructureBspBackgroundSoundPaletteBlock : Abide.Guerilla.Tags.IReadable, Abide.Guerilla.Tags.IWritable
        {
            [Abide.Guerilla.Tags.FieldAttribute("Name^", typeof(String32))]
            public String32 Name;
            [Abide.Guerilla.Tags.FieldAttribute("Background Sound", typeof(TagReference))]
            public TagReference BackgroundSound;
            [Abide.Guerilla.Tags.FieldAttribute("Inside Cluster Sound#Play only when player is inside cluster.", typeof(TagReference))]
            public TagReference InsideClusterSound;
            [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
            [Abide.Guerilla.Tags.PaddingAttribute(20)]
            public Byte[] EmptyString;
            [Abide.Guerilla.Tags.FieldAttribute("Cutoff Distance", typeof(Single))]
            public Single CutoffDistance;
            [Abide.Guerilla.Tags.FieldAttribute("Scale Flags", typeof(Int32))]
            [Abide.Guerilla.Tags.OptionsAttribute(typeof(ScaleFlagsOptions), true)]
            public Int32 ScaleFlags;
            [Abide.Guerilla.Tags.FieldAttribute("Interior Scale", typeof(Single))]
            public Single InteriorScale;
            [Abide.Guerilla.Tags.FieldAttribute("Portal Scale", typeof(Single))]
            public Single PortalScale;
            [Abide.Guerilla.Tags.FieldAttribute("Exterior Scale", typeof(Single))]
            public Single ExteriorScale;
            [Abide.Guerilla.Tags.FieldAttribute("Interpolation Speed:1/sec", typeof(Single))]
            public Single InterpolationSpeed;
            [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
            [Abide.Guerilla.Tags.PaddingAttribute(8)]
            public Byte[] EmptyString1;
            public int Size
            {
                get
                {
                    return 116;
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
            public enum ScaleFlagsOptions
            {
                OverrideDefaultScale = 1,
                UseAdjacentClusterAsPortalScale = 2,
                UseAdjacentClusterAsExteriorScale = 4,
                ScaleWithWeatherIntensity = 8,
            }
        }
        [Abide.Guerilla.Tags.FieldSetAttribute(80, 4)]
        public sealed class StructureBspSoundEnvironmentPaletteBlock : Abide.Guerilla.Tags.IReadable, Abide.Guerilla.Tags.IWritable
        {
            [Abide.Guerilla.Tags.FieldAttribute("Name^", typeof(String32))]
            public String32 Name;
            [Abide.Guerilla.Tags.FieldAttribute("Sound Environment", typeof(TagReference))]
            public TagReference SoundEnvironment;
            [Abide.Guerilla.Tags.FieldAttribute("Cutoff Distance", typeof(Single))]
            public Single CutoffDistance;
            [Abide.Guerilla.Tags.FieldAttribute("Interpolation Speed:1/sec", typeof(Single))]
            public Single InterpolationSpeed;
            [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
            [Abide.Guerilla.Tags.PaddingAttribute(24)]
            public Byte[] EmptyString;
            public int Size
            {
                get
                {
                    return 80;
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
        [Abide.Guerilla.Tags.FieldSetAttribute(152, 4)]
        public sealed class StructureBspWeatherPaletteBlock : Abide.Guerilla.Tags.IReadable, Abide.Guerilla.Tags.IWritable
        {
            [Abide.Guerilla.Tags.FieldAttribute("Name^", typeof(String32))]
            public String32 Name;
            [Abide.Guerilla.Tags.FieldAttribute("Weather System", typeof(TagReference))]
            public TagReference WeatherSystem;
            [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
            [Abide.Guerilla.Tags.PaddingAttribute(2)]
            public Byte[] EmptyString;
            [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
            [Abide.Guerilla.Tags.PaddingAttribute(2)]
            public Byte[] EmptyString1;
            [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
            [Abide.Guerilla.Tags.PaddingAttribute(32)]
            public Byte[] EmptyString2;
            [Abide.Guerilla.Tags.FieldAttribute("Wind", typeof(TagReference))]
            public TagReference Wind;
            [Abide.Guerilla.Tags.FieldAttribute("Wind Direction", typeof(Vector3))]
            public Vector3 WindDirection;
            [Abide.Guerilla.Tags.FieldAttribute("Wind Magnitude", typeof(Single))]
            public Single WindMagnitude;
            [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
            [Abide.Guerilla.Tags.PaddingAttribute(4)]
            public Byte[] EmptyString3;
            [Abide.Guerilla.Tags.FieldAttribute("Wind Scale Function", typeof(String32))]
            public String32 WindScaleFunction;
            public int Size
            {
                get
                {
                    return 152;
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
        [Abide.Guerilla.Tags.FieldSetAttribute(256, 4)]
        public sealed class ScenarioAtmosphericFogPalette : Abide.Guerilla.Tags.IReadable, Abide.Guerilla.Tags.IWritable
        {
            [Abide.Guerilla.Tags.FieldAttribute("Name^", typeof(StringId))]
            public StringId Name;
            [Abide.Guerilla.Tags.FieldAttribute("Color", typeof(ColorRgbF))]
            public ColorRgbF Color;
            [Abide.Guerilla.Tags.FieldAttribute("Spread Distance:World Units#How far fog spreads into adjacent clusters: 0 default" +
                "s to 1.", typeof(Single))]
            public Single SpreadDistance;
            [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
            [Abide.Guerilla.Tags.PaddingAttribute(4)]
            public Byte[] EmptyString;
            [Abide.Guerilla.Tags.FieldAttribute("Maximum Density:[0,1]#Fog density clamps to this value.", typeof(Single))]
            public Single MaximumDensity;
            [Abide.Guerilla.Tags.FieldAttribute("Start Distance:World Units#Before this distance, there is no fog.", typeof(Single))]
            public Single StartDistance;
            [Abide.Guerilla.Tags.FieldAttribute("Opaque Distance:World Units#Fog becomes opaque (maximum density) at this distance" +
                " from viewer.", typeof(Single))]
            public Single OpaqueDistance;
            [Abide.Guerilla.Tags.FieldAttribute("Color", typeof(ColorRgbF))]
            public ColorRgbF Color1;
            [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
            [Abide.Guerilla.Tags.PaddingAttribute(4)]
            public Byte[] EmptyString1;
            [Abide.Guerilla.Tags.FieldAttribute("Maximum Density:[0,1]#Fog density clamps to this value.", typeof(Single))]
            public Single MaximumDensity1;
            [Abide.Guerilla.Tags.FieldAttribute("Start Distance:World Units#Before this distance, there is no fog.", typeof(Single))]
            public Single StartDistance1;
            [Abide.Guerilla.Tags.FieldAttribute("Opaque Distance:World Units#Fog becomes opaque (maximum density) at this distance" +
                " from viewer.", typeof(Single))]
            public Single OpaqueDistance1;
            [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
            [Abide.Guerilla.Tags.PaddingAttribute(4)]
            public Byte[] EmptyString2;
            [Abide.Guerilla.Tags.FieldAttribute("Planar Color", typeof(ColorRgbF))]
            public ColorRgbF PlanarColor;
            [Abide.Guerilla.Tags.FieldAttribute("Planar Max Density:[0,1]", typeof(Single))]
            public Single PlanarMaxDensity;
            [Abide.Guerilla.Tags.FieldAttribute("Planar Override Amount:[0,1]", typeof(Single))]
            public Single PlanarOverrideAmount;
            [Abide.Guerilla.Tags.FieldAttribute("Planar Min Distance Bias:World Units#Don\'t ask.", typeof(Single))]
            public Single PlanarMinDistanceBias;
            [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
            [Abide.Guerilla.Tags.PaddingAttribute(44)]
            public Byte[] EmptyString3;
            [Abide.Guerilla.Tags.FieldAttribute("Patchy Color", typeof(ColorRgbF))]
            public ColorRgbF PatchyColor;
            [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
            [Abide.Guerilla.Tags.PaddingAttribute(12)]
            public Byte[] EmptyString4;
            [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
            [Abide.Guerilla.Tags.PaddingAttribute(32)]
            public Byte[] EmptyString5;
            [Abide.Guerilla.Tags.FieldAttribute("Patchy Fog", typeof(TagReference))]
            public TagReference PatchyFog1;
            [Abide.Guerilla.Tags.FieldAttribute("Mixers", typeof(TagBlock))]
            [Abide.Guerilla.Tags.BlockAttribute("mixers", 2, typeof(ScenarioAtmosphericFogMixerBlock))]
            public TagBlock Mixers;
            [Abide.Guerilla.Tags.FieldAttribute("Amount:[0,1]", typeof(Single))]
            public Single Amount;
            [Abide.Guerilla.Tags.FieldAttribute("Threshold:[0,1]", typeof(Single))]
            public Single Threshold;
            [Abide.Guerilla.Tags.FieldAttribute("Brightness:[0,1]", typeof(Single))]
            public Single Brightness;
            [Abide.Guerilla.Tags.FieldAttribute("Gamma Power", typeof(Single))]
            public Single GammaPower;
            [Abide.Guerilla.Tags.FieldAttribute("Camera Immersion Flags", typeof(Int16))]
            [Abide.Guerilla.Tags.OptionsAttribute(typeof(CameraImmersionFlagsOptions), true)]
            public Int16 CameraImmersionFlags;
            [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
            [Abide.Guerilla.Tags.PaddingAttribute(2)]
            public Byte[] EmptyString6;
            public int Size
            {
                get
                {
                    return 256;
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
            public sealed class ScenarioAtmosphericFogMixerBlock : Abide.Guerilla.Tags.IReadable, Abide.Guerilla.Tags.IWritable
            {
                [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
                [Abide.Guerilla.Tags.PaddingAttribute(4)]
                public Byte[] EmptyString;
                [Abide.Guerilla.Tags.FieldAttribute("Atmospheric Fog Source:From Scenario Atmospheric Fog Palette", typeof(StringId))]
                public StringId AtmosphericFogSource;
                [Abide.Guerilla.Tags.FieldAttribute("Interpolator:From Scenario Interpolators", typeof(StringId))]
                public StringId Interpolator;
                [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
                [Abide.Guerilla.Tags.PaddingAttribute(2)]
                public Byte[] EmptyString1;
                [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
                [Abide.Guerilla.Tags.PaddingAttribute(2)]
                public Byte[] EmptyString2;
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
            public enum CameraImmersionFlagsOptions
            {
                DisableAtmosphericFog = 1,
                DisableSecondaryFog = 2,
                DisablePlanarFog = 4,
                InvertPlanarFogPriorities = 8,
                DisableWater = 16,
            }
        }
    }
}
#pragma warning restore CS1591