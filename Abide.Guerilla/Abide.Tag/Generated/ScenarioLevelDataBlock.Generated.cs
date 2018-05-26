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
    /// Represents the generated scenario_level_data_block tag block.
    /// </summary>
    public sealed class ScenarioLevelDataBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScenarioLevelDataBlock"/> class.
        /// </summary>
        public ScenarioLevelDataBlock()
        {
            this.Fields.Add(new TagReferenceField("Level Description"));
            this.Fields.Add(new BlockField<GlobalUiCampaignLevelBlock>("Campaign Level Data", 20));
            this.Fields.Add(new BlockField<GlobalUiMultiplayerLevelBlock>("Multiplayer", 50));
        }
        /// <summary>
        /// Gets and returns the name of the scenario_level_data_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "scenario_level_data_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the scenario_level_data_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "scenario_level_data_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the scenario_level_data_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 1;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the scenario_level_data_block tag block.
        /// </summary>
        public override int Alignment
        {
            get
            {
                return 4;
            }
        }
        /// <summary>
        /// Writes the scenario_level_data_block tag block using the specified binary writer.
        /// </summary>
        // <param name="writer">The <see cref="BinaryWriter"/> used to write the scenario_level_data_block tag block.</param>
        public override void Write(BinaryWriter writer)
        {
            // Invoke base write procedure.
            base.Write(writer);
            // Post-write the tag blocks.
            ((BlockField<GlobalUiCampaignLevelBlock>)(this.Fields[1])).WriteChildren(writer);
            ((BlockField<GlobalUiMultiplayerLevelBlock>)(this.Fields[2])).WriteChildren(writer);
        }
    }
}
