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
    /// Represents the generated map_leaf_connection_index_block tag block.
    /// </summary>
    public sealed class MapLeafConnectionIndexBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapLeafConnectionIndexBlock"/> class.
        /// </summary>
        public MapLeafConnectionIndexBlock()
        {
            this.Fields.Add(new LongIntegerField("connection index*"));
        }
        /// <summary>
        /// Gets and returns the name of the map_leaf_connection_index_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "map_leaf_connection_index_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the map_leaf_connection_index_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "map_leaf_connection_index_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the map_leaf_connection_index_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 512;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the map_leaf_connection_index_block tag block.
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
