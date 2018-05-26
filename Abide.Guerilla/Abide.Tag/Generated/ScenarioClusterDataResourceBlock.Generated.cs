//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Abide.Tag.Generated
{
    using Abide.Tag;
    using System.IO;
    
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
        public override string Name
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
        /// <summary>
        /// Writes the scenario_cluster_data_resource_block tag block using the specified binary writer.
        /// </summary>
        // <param name="writer">The <see cref="BinaryWriter"/> used to write the scenario_cluster_data_resource_block tag block.</param>
        public override void Write(BinaryWriter writer)
        {
            // Invoke base write procedure.
            base.Write(writer);
            // Post-write the tag blocks.
            ((BlockField<ScenarioClusterDataBlock>)(this.Fields[0])).WriteChildren(writer);
            ((BlockField<StructureBspBackgroundSoundPaletteBlock>)(this.Fields[1])).WriteChildren(writer);
            ((BlockField<StructureBspSoundEnvironmentPaletteBlock>)(this.Fields[2])).WriteChildren(writer);
            ((BlockField<StructureBspWeatherPaletteBlock>)(this.Fields[3])).WriteChildren(writer);
            ((BlockField<ScenarioAtmosphericFogPalette>)(this.Fields[4])).WriteChildren(writer);
        }
    }
}
