//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Abide.Tag.Guerilla.Generated
{
    using System;
    using Abide.HaloLibrary;
    using Abide.Tag;
    
    /// <summary>
    /// Represents the generated scenario_cluster_data_resource_block tag block.
    /// </summary>
    public sealed class ScenarioClusterDataResourceBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScenarioClusterDataResourceBlock"/> class.
        /// </summary>
        public ScenarioClusterDataResourceBlock()
        {
            this.Fields.Add(new BlockField<ScenarioClusterDataBlock>("Cluster Data", 16));
            this.Fields.Add(new BlockField<StructureBspBackgroundSoundPaletteBlock>("Background Sound Palette", 64));
            this.Fields.Add(new BlockField<StructureBspSoundEnvironmentPaletteBlock>("Sound Environment Palette", 64));
            this.Fields.Add(new BlockField<StructureBspWeatherPaletteBlock>("Weather Palette", 32));
            this.Fields.Add(new BlockField<ScenarioAtmosphericFogPalette>("Atmospheric Fog Palette", 127));
        }
        /// <summary>
        /// Gets and returns the name of the scenario_cluster_data_resource_block tag block.
        /// </summary>
        public override string BlockName
        {
            get
            {
                return "scenario_cluster_data_resource_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the scenario_cluster_data_resource_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "scenario_cluster_data_resource";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the scenario_cluster_data_resource_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 1;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the scenario_cluster_data_resource_block tag block.
        /// </summary>
        public override int Alignment
        {
            get
            {
                return 4;
            }
        }
    }
}