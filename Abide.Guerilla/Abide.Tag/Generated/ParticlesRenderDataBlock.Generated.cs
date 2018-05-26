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
    /// Represents the generated particles_render_data_block tag block.
    /// </summary>
    public sealed class ParticlesRenderDataBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParticlesRenderDataBlock"/> class.
        /// </summary>
        public ParticlesRenderDataBlock()
        {
            this.Fields.Add(new RealField("position.x*"));
            this.Fields.Add(new RealField("position.y*"));
            this.Fields.Add(new RealField("position.z*"));
            this.Fields.Add(new RealField("size*"));
            this.Fields.Add(new RgbColorField("color*"));
        }
        /// <summary>
        /// Gets and returns the name of the particles_render_data_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "particles_render_data_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the particles_render_data_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "particles_render_data_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the particles_render_data_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 4096;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the particles_render_data_block tag block.
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
