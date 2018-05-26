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
    /// Represents the generated chocolate_mountain_block tag block.
    /// </summary>
    public sealed class ChocolateMountainBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChocolateMountainBlock"/> class.
        /// </summary>
        public ChocolateMountainBlock()
        {
            this.Fields.Add(new BlockField<LightingVariablesBlock>("lighting variables", 13));
        }
        /// <summary>
        /// Gets and returns the name of the chocolate_mountain_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "chocolate_mountain_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the chocolate_mountain_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "chocolate_mountain";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the chocolate_mountain_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 1;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the chocolate_mountain_block tag block.
        /// </summary>
        public override int Alignment
        {
            get
            {
                return 4;
            }
        }
        /// <summary>
        /// Writes the chocolate_mountain_block tag block using the specified binary writer.
        /// </summary>
        // <param name="writer">The <see cref="BinaryWriter"/> used to write the chocolate_mountain_block tag block.</param>
        public override void Write(BinaryWriter writer)
        {
            // Invoke base write procedure.
            base.Write(writer);
            // Post-write the tag blocks.
            ((BlockField<LightingVariablesBlock>)(this.Fields[0])).WriteChildren(writer);
        }
    }
}
