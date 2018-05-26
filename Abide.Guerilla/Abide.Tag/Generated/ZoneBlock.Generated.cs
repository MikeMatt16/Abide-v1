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
    /// Represents the generated zone_block tag block.
    /// </summary>
    public sealed class ZoneBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ZoneBlock"/> class.
        /// </summary>
        public ZoneBlock()
        {
            this.Fields.Add(new StringField("name^"));
            this.Fields.Add(new LongFlagsField("flags", "manual bsp index"));
            this.Fields.Add(new ShortBlockIndexField("manual bsp"));
            this.Fields.Add(new PadField("", 2));
            this.Fields.Add(new BlockField<FiringPositionsBlock>("firing positions", 512));
            this.Fields.Add(new BlockField<AreasBlock>("areas", 64));
        }
        /// <summary>
        /// Gets and returns the name of the zone_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "zone_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the zone_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "zone_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the zone_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 128;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the zone_block tag block.
        /// </summary>
        public override int Alignment
        {
            get
            {
                return 4;
            }
        }
        /// <summary>
        /// Writes the zone_block tag block using the specified binary writer.
        /// </summary>
        // <param name="writer">The <see cref="BinaryWriter"/> used to write the zone_block tag block.</param>
        public override void Write(BinaryWriter writer)
        {
            // Invoke base write procedure.
            base.Write(writer);
            // Post-write the tag blocks.
            ((BlockField<FiringPositionsBlock>)(this.Fields[4])).WriteChildren(writer);
            ((BlockField<AreasBlock>)(this.Fields[5])).WriteChildren(writer);
        }
    }
}
