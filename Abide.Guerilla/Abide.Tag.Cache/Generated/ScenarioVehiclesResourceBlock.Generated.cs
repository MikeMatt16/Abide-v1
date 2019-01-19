//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Abide.Tag.Cache.Generated
{
    using Abide.Tag;
    
    /// <summary>
    /// Represents the generated scenario_vehicles_resource_block tag block.
    /// </summary>
    public sealed class ScenarioVehiclesResourceBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScenarioVehiclesResourceBlock"/> class.
        /// </summary>
        public ScenarioVehiclesResourceBlock()
        {
            this.Fields.Add(new BlockField<ScenarioObjectNamesBlock>("Names", 640));
            this.Fields.Add(new BlockField<DontUseMeScenarioEnvironmentObjectBlock>("*", 4096));
            this.Fields.Add(new BlockField<ScenarioStructureBspReferenceBlock>("Structure References", 16));
            this.Fields.Add(new BlockField<ScenarioVehiclePaletteBlock>("Palette", 256));
            this.Fields.Add(new BlockField<ScenarioVehicleBlock>("Objects", 256));
            this.Fields.Add(new LongIntegerField("Next Object ID Salt*"));
            this.Fields.Add(new BlockField<GScenarioEditorFolderBlock>("Editor Folders*", 32767));
        }
        /// <summary>
        /// Gets and returns the name of the scenario_vehicles_resource_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "scenario_vehicles_resource_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the scenario_vehicles_resource_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "scenario_vehicles_resource";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the scenario_vehicles_resource_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 1;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the scenario_vehicles_resource_block tag block.
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