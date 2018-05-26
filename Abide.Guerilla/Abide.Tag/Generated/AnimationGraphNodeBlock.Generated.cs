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
    /// Represents the generated animation_graph_node_block tag block.
    /// </summary>
    public sealed class AnimationGraphNodeBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnimationGraphNodeBlock"/> class.
        /// </summary>
        public AnimationGraphNodeBlock()
        {
            this.Fields.Add(new StringIdField("name^"));
            this.Fields.Add(new ShortBlockIndexField("next sibling node index*"));
            this.Fields.Add(new ShortBlockIndexField("first child node index*"));
            this.Fields.Add(new ShortBlockIndexField("parent node index*"));
            this.Fields.Add(new ByteFlagsField("model flags*", "primary model", "secondary model", "local root", "left hand", "right hand", "left arm member"));
            this.Fields.Add(new ByteFlagsField("node joint flags", "ball-socket", "hinge", "no movement"));
            this.Fields.Add(new RealVector3dField("base vector*"));
            this.Fields.Add(new RealField("vector range*"));
            this.Fields.Add(new RealField("z_pos*"));
        }
        /// <summary>
        /// Gets and returns the name of the animation_graph_node_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "animation_graph_node_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the animation_graph_node_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "animation_graph_node_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the animation_graph_node_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 255;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the animation_graph_node_block tag block.
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
