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
    /// Represents the generated structure_bsp_fake_lightprobes_block tag block.
    /// </summary>
    public sealed class StructureBspFakeLightprobesBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StructureBspFakeLightprobesBlock"/> class.
        /// </summary>
        public StructureBspFakeLightprobesBlock()
        {
            this.Fields.Add(new StructField<ScenarioObjectIdStructBlock>("Object Identifier*"));
            this.Fields.Add(new StructField<RenderLightingStructBlock>("Render Lighting*"));
        }
        /// <summary>
        /// Gets and returns the name of the structure_bsp_fake_lightprobes_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "structure_bsp_fake_lightprobes_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the structure_bsp_fake_lightprobes_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "structure_bsp_fake_lightprobes_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the structure_bsp_fake_lightprobes_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 2048;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the structure_bsp_fake_lightprobes_block tag block.
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