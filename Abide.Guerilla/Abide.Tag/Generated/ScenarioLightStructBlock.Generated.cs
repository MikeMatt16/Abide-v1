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
    /// Represents the generated scenario_light_struct_block tag block.
    /// </summary>
    public sealed class ScenarioLightStructBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScenarioLightStructBlock"/> class.
        /// </summary>
        public ScenarioLightStructBlock()
        {
            this.Fields.Add(new EnumField("Type", "sphere", "orthogonal", "projective", "pyramid"));
            this.Fields.Add(new WordFlagsField("Flags", "Custom Geometry", "Unused", "cinematic_only"));
            this.Fields.Add(new EnumField("Lightmap Type", "Use Light Tag Setting", "Dynamic Only", "Dynamic with Lightmaps", "Lightmaps Only"));
            this.Fields.Add(new WordFlagsField("Lightmap Flags", "Unused"));
            this.Fields.Add(new RealField("Lightmap Half Life"));
            this.Fields.Add(new RealField("Lightmap Light Scale"));
            this.Fields.Add(new RealPoint3dField("Target Point*"));
            this.Fields.Add(new RealField("Width*:World Units"));
            this.Fields.Add(new RealField("Height Scale*:World Units"));
            this.Fields.Add(new AngleField("Field of View*:Degrees"));
            this.Fields.Add(new RealField("Falloff Distance*:World Units"));
            this.Fields.Add(new RealField("Cutoff Distance*:World Units (from Far Plane)"));
        }
        /// <summary>
        /// Gets and returns the name of the scenario_light_struct_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "scenario_light_struct_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the scenario_light_struct_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "scenario_light_struct";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the scenario_light_struct_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 1;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the scenario_light_struct_block tag block.
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
