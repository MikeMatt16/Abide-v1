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
    /// Represents the generated global_damage_nodes_block tag block.
    /// </summary>
    public sealed class GlobalDamageNodesBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalDamageNodesBlock"/> class.
        /// </summary>
        public GlobalDamageNodesBlock()
        {
            this.Fields.Add(new PadField("", 2));
            this.Fields.Add(new PadField("", 2));
            this.Fields.Add(new PadField("", 12));
        }
        /// <summary>
        /// Gets and returns the name of the global_damage_nodes_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "global_damage_nodes_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the global_damage_nodes_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "global_damage_nodes_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the global_damage_nodes_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 255;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the global_damage_nodes_block tag block.
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
