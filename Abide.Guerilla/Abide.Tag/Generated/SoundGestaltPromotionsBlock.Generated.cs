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
    /// Represents the generated sound_gestalt_promotions_block tag block.
    /// </summary>
    public sealed class SoundGestaltPromotionsBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SoundGestaltPromotionsBlock"/> class.
        /// </summary>
        public SoundGestaltPromotionsBlock()
        {
            this.Fields.Add(new StructField<SoundPromotionParametersStructBlock>(""));
        }
        /// <summary>
        /// Gets and returns the name of the sound_gestalt_promotions_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "sound_gestalt_promotions_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the sound_gestalt_promotions_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "sound_gestalt_promotions_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the sound_gestalt_promotions_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 32767;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the sound_gestalt_promotions_block tag block.
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
