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
    /// Represents the generated prt_lod_info_block tag block.
    /// </summary>
    public sealed class PrtLodInfoBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrtLodInfoBlock"/> class.
        /// </summary>
        public PrtLodInfoBlock()
        {
            this.Fields.Add(new LongIntegerField("cluster offset*"));
            this.Fields.Add(new BlockField<PrtSectionInfoBlock>("section info*", 255));
        }
        /// <summary>
        /// Gets and returns the name of the prt_lod_info_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "prt_lod_info_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the prt_lod_info_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "prt lod info";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the prt_lod_info_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 6;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the prt_lod_info_block tag block.
        /// </summary>
        public override int Alignment
        {
            get
            {
                return 4;
            }
        }
        /// <summary>
        /// Writes the prt_lod_info_block tag block using the specified binary writer.
        /// </summary>
        // <param name="writer">The <see cref="BinaryWriter"/> used to write the prt_lod_info_block tag block.</param>
        public override void Write(BinaryWriter writer)
        {
            // Invoke base write procedure.
            base.Write(writer);
            // Post-write the tag blocks.
            ((BlockField<PrtSectionInfoBlock>)(this.Fields[1])).WriteChildren(writer);
        }
    }
}
