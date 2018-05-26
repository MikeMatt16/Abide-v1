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
    /// Represents the generated new_hud_sound_block tag block.
    /// </summary>
    public sealed class NewHudSoundBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewHudSoundBlock"/> class.
        /// </summary>
        public NewHudSoundBlock()
        {
            this.Fields.Add(new TagReferenceField("chief sound^"));
            this.Fields.Add(new LongFlagsField("latched to", "shield recharging", "shield damaged", "shield low", "shield empty", "health low", "health empty", "health minor damage", "health major damage", "rocket locking", "rocket locked"));
            this.Fields.Add(new RealField("scale"));
            this.Fields.Add(new TagReferenceField("dervish sound"));
        }
        /// <summary>
        /// Gets and returns the name of the new_hud_sound_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "new_hud_sound_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the new_hud_sound_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "new_hud_sound_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the new_hud_sound_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 6;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the new_hud_sound_block tag block.
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
