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
    /// Represents the generated screen_effect_bonus_struct_block tag block.
    /// </summary>
    public sealed class ScreenEffectBonusStructBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenEffectBonusStructBlock"/> class.
        /// </summary>
        public ScreenEffectBonusStructBlock()
        {
            this.Fields.Add(new TagReferenceField("halfscreen screen effect", "egor"));
            this.Fields.Add(new TagReferenceField("quarterscreen screen effect", "egor"));
        }
        /// <summary>
        /// Gets and returns the name of the screen_effect_bonus_struct_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "screen_effect_bonus_struct_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the screen_effect_bonus_struct_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "screen_effect_bonus_struct";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the screen_effect_bonus_struct_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 1;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the screen_effect_bonus_struct_block tag block.
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