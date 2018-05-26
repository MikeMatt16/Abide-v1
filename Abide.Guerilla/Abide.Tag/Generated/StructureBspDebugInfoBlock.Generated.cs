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
    /// Represents the generated structure_bsp_debug_info_block tag block.
    /// </summary>
    public sealed class StructureBspDebugInfoBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StructureBspDebugInfoBlock"/> class.
        /// </summary>
        public StructureBspDebugInfoBlock()
        {
            this.Fields.Add(new PadField("", 64));
            this.Fields.Add(new BlockField<StructureBspClusterDebugInfoBlock>("Clusters*", 512));
            this.Fields.Add(new BlockField<StructureBspFogPlaneDebugInfoBlock>("Fog Planes*", 127));
            this.Fields.Add(new BlockField<StructureBspFogZoneDebugInfoBlock>("Fog Zones*", 127));
        }
        /// <summary>
        /// Gets and returns the name of the structure_bsp_debug_info_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "structure_bsp_debug_info_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the structure_bsp_debug_info_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "structure_bsp_debug_info_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the structure_bsp_debug_info_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 1;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the structure_bsp_debug_info_block tag block.
        /// </summary>
        public override int Alignment
        {
            get
            {
                return 4;
            }
        }
        /// <summary>
        /// Writes the structure_bsp_debug_info_block tag block using the specified binary writer.
        /// </summary>
        // <param name="writer">The <see cref="BinaryWriter"/> used to write the structure_bsp_debug_info_block tag block.</param>
        public override void Write(BinaryWriter writer)
        {
            // Invoke base write procedure.
            base.Write(writer);
            // Post-write the tag blocks.
            ((BlockField<StructureBspClusterDebugInfoBlock>)(this.Fields[1])).WriteChildren(writer);
            ((BlockField<StructureBspFogPlaneDebugInfoBlock>)(this.Fields[2])).WriteChildren(writer);
            ((BlockField<StructureBspFogZoneDebugInfoBlock>)(this.Fields[3])).WriteChildren(writer);
        }
    }
}
