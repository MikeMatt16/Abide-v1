//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Abide.Tag.Guerilla.Generated
{
    using Abide.Tag;
    
    /// <summary>
    /// Represents the generated abide_xbox_animation_unknown_block tag block.
    /// </summary>
    public sealed class AbideXboxAnimationUnknownBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AbideXboxAnimationUnknownBlock"/> class.
        /// </summary>
        public AbideXboxAnimationUnknownBlock()
        {
            this.Fields.Add(new LongIntegerField("Unknown1"));
            this.Fields.Add(new LongIntegerField("Unknown2"));
            this.Fields.Add(new LongIntegerField("Unknown3"));
            this.Fields.Add(new LongIntegerField("Unknown4"));
            this.Fields.Add(new LongIntegerField("Unknown5"));
            this.Fields.Add(new LongIntegerField("Unknown6"));
        }
        /// <summary>
        /// Gets and returns the name of the abide_xbox_animation_unknown_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "abide_xbox_animation_unknown_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the abide_xbox_animation_unknown_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "abide_xbox_animation_unknown_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the abide_xbox_animation_unknown_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 16384;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the abide_xbox_animation_unknown_block tag block.
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