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
    /// Represents the generated animation_effect_event_block tag block.
    /// </summary>
    public sealed class AnimationEffectEventBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnimationEffectEventBlock"/> class.
        /// </summary>
        public AnimationEffectEventBlock()
        {
            this.Fields.Add(new ShortBlockIndexField("effect"));
            this.Fields.Add(new ShortIntegerField("frame"));
        }
        /// <summary>
        /// Gets and returns the name of the animation_effect_event_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "animation_effect_event_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the animation_effect_event_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "animation_effect_event_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the animation_effect_event_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 512;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the animation_effect_event_block tag block.
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
