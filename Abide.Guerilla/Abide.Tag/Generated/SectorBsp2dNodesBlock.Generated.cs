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
    /// Represents the generated sector_bsp2d_nodes_block tag block.
    /// </summary>
    public sealed class SectorBsp2dNodesBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SectorBsp2dNodesBlock"/> class.
        /// </summary>
        public SectorBsp2dNodesBlock()
        {
            this.Fields.Add(new RealPlane2dField("plane*"));
            this.Fields.Add(new LongIntegerField("left child*"));
            this.Fields.Add(new LongIntegerField("right child*"));
        }
        /// <summary>
        /// Gets and returns the name of the sector_bsp2d_nodes_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "sector_bsp2d_nodes_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the sector_bsp2d_nodes_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "sector_bsp2d_nodes_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the sector_bsp2d_nodes_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 131072;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the sector_bsp2d_nodes_block tag block.
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
