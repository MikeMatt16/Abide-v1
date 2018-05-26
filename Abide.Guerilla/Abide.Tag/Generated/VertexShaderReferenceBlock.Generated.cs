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
    /// Represents the generated vertex_shader_reference_block tag block.
    /// </summary>
    public sealed class VertexShaderReferenceBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VertexShaderReferenceBlock"/> class.
        /// </summary>
        public VertexShaderReferenceBlock()
        {
            this.Fields.Add(new TagReferenceField("vertex shader"));
        }
        /// <summary>
        /// Gets and returns the name of the vertex_shader_reference_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "vertex_shader_reference_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the vertex_shader_reference_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "vertex_shader_reference_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the vertex_shader_reference_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 32;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the vertex_shader_reference_block tag block.
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
