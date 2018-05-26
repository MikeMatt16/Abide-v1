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
    /// Represents the generated multiplayer_universal_block tag block.
    /// </summary>
    public sealed class MultiplayerUniversalBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiplayerUniversalBlock"/> class.
        /// </summary>
        public MultiplayerUniversalBlock()
        {
            this.Fields.Add(new TagReferenceField("random player names"));
            this.Fields.Add(new TagReferenceField("team names"));
            this.Fields.Add(new BlockField<MultiplayerColorBlock>("team colors", 32));
            this.Fields.Add(new TagReferenceField("multiplayer text"));
        }
        /// <summary>
        /// Gets and returns the name of the multiplayer_universal_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "multiplayer_universal_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the multiplayer_universal_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "multiplayer_universal_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the multiplayer_universal_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 1;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the multiplayer_universal_block tag block.
        /// </summary>
        public override int Alignment
        {
            get
            {
                return 4;
            }
        }
        /// <summary>
        /// Writes the multiplayer_universal_block tag block using the specified binary writer.
        /// </summary>
        // <param name="writer">The <see cref="BinaryWriter"/> used to write the multiplayer_universal_block tag block.</param>
        public override void Write(BinaryWriter writer)
        {
            // Invoke base write procedure.
            base.Write(writer);
            // Post-write the tag blocks.
            ((BlockField<MultiplayerColorBlock>)(this.Fields[2])).WriteChildren(writer);
        }
    }
}
