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
    /// Represents the generated global_geometry_section_vertex_buffer_block tag block.
    /// </summary>
    public sealed class GlobalGeometrySectionVertexBufferBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalGeometrySectionVertexBufferBlock"/> class.
        /// </summary>
        public GlobalGeometrySectionVertexBufferBlock()
        {
            this.Fields.Add(new VertexBufferField("vertex buffer*"));
        }
        /// <summary>
        /// Gets and returns the name of the global_geometry_section_vertex_buffer_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "global_geometry_section_vertex_buffer_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the global_geometry_section_vertex_buffer_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "vertex buffer";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the global_geometry_section_vertex_buffer_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 512;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the global_geometry_section_vertex_buffer_block tag block.
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
