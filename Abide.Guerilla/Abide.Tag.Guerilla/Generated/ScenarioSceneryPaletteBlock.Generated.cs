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
    using Abide.Tag;
    
    /// <summary>
    /// Represents the generated scenario_scenery_palette_block tag block.
    /// </summary>
    public sealed class ScenarioSceneryPaletteBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScenarioSceneryPaletteBlock"/> class.
        /// </summary>
        public ScenarioSceneryPaletteBlock()
        {
            this.Fields.Add(new TagReferenceField("Name^", 1935893870));
            this.Fields.Add(new PadField("", 32));
        }
        /// <summary>
        /// Gets and returns the name of the scenario_scenery_palette_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "scenario_scenery_palette_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the scenario_scenery_palette_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "scenario_scenery_palette_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the scenario_scenery_palette_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 256;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the scenario_scenery_palette_block tag block.
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
