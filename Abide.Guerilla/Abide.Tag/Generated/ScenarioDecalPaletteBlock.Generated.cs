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
    
    /// <summary>
    /// Represents the generated scenario_decal_palette_block tag block.
    /// </summary>
    public sealed class ScenarioDecalPaletteBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScenarioDecalPaletteBlock"/> class.
        /// </summary>
        public ScenarioDecalPaletteBlock()
        {
            this.Fields.Add(new TagReferenceField("Reference^"));
        }
        /// <summary>
        /// Gets and returns the name of the scenario_decal_palette_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "scenario_decal_palette_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the scenario_decal_palette_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "scenario_decal_palette_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the scenario_decal_palette_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 128;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the scenario_decal_palette_block tag block.
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
