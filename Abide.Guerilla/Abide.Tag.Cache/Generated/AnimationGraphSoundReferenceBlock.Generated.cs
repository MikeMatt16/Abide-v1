//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Abide.Tag.Cache.Generated
{
    using Abide.Tag;
    
    /// <summary>
    /// Represents the generated animation_graph_sound_reference_block tag block.
    /// </summary>
    public sealed class AnimationGraphSoundReferenceBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnimationGraphSoundReferenceBlock"/> class.
        /// </summary>
        public AnimationGraphSoundReferenceBlock()
        {
            this.Fields.Add(new TagReferenceField("sound^", 1936614433));
            this.Fields.Add(new WordFlagsField("flags", "allow on player", "left arm only", "right arm only", "first-person only", "forward only", "reverse only"));
            this.Fields.Add(new PadField("", 2));
        }
        /// <summary>
        /// Gets and returns the name of the animation_graph_sound_reference_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "animation_graph_sound_reference_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the animation_graph_sound_reference_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "animation_graph_sound_reference_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the animation_graph_sound_reference_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 512;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the animation_graph_sound_reference_block tag block.
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
