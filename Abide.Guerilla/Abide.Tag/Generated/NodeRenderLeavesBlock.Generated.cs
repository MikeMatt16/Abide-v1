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
    /// Represents the generated node_render_leaves_block tag block.
    /// </summary>
    public sealed class NodeRenderLeavesBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NodeRenderLeavesBlock"/> class.
        /// </summary>
        public NodeRenderLeavesBlock()
        {
            this.Fields.Add(new BlockField<BspLeafBlock>("collision leaves*", 65536));
            this.Fields.Add(new BlockField<BspSurfaceReferenceBlock>("surface references*", 262144));
        }
        /// <summary>
        /// Gets and returns the name of the node_render_leaves_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "node_render_leaves_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the node_render_leaves_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "node_render_leaves_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the node_render_leaves_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 64;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the node_render_leaves_block tag block.
        /// </summary>
        public override int Alignment
        {
            get
            {
                return 4;
            }
        }
        /// <summary>
        /// Writes the node_render_leaves_block tag block using the specified binary writer.
        /// </summary>
        // <param name="writer">The <see cref="BinaryWriter"/> used to write the node_render_leaves_block tag block.</param>
        public override void Write(BinaryWriter writer)
        {
            // Invoke base write procedure.
            base.Write(writer);
            // Post-write the tag blocks.
            ((BlockField<BspLeafBlock>)(this.Fields[0])).WriteChildren(writer);
            ((BlockField<BspSurfaceReferenceBlock>)(this.Fields[1])).WriteChildren(writer);
        }
    }
}
