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
    /// Represents the generated barrel_firing_effect_block tag block.
    /// </summary>
    public sealed class BarrelFiringEffectBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BarrelFiringEffectBlock"/> class.
        /// </summary>
        public BarrelFiringEffectBlock()
        {
            this.Fields.Add(new ShortIntegerField("shot count lower bound#the minimum number of times this firing effect will be use" +
                        "d, once it has been chosen"));
            this.Fields.Add(new ShortIntegerField("shot count upper bound#the maximum number of times this firing effect will be use" +
                        "d, once it has been chosen"));
            this.Fields.Add(new TagReferenceField("firing effect^#this effect is used when the weapon is loaded and fired normally", -3));
            this.Fields.Add(new TagReferenceField("misfire effect#this effect is used when the weapon is loaded but fired while over" +
                        "heated", -3));
            this.Fields.Add(new TagReferenceField("empty effect#this effect is used when the weapon is not loaded", -3));
            this.Fields.Add(new TagReferenceField("firing damage#this effect is used when the weapon is loaded and fired normally", 1785754657));
            this.Fields.Add(new TagReferenceField("misfire damage#this effect is used when the weapon is loaded but fired while over" +
                        "heated", 1785754657));
            this.Fields.Add(new TagReferenceField("empty damage#this effect is used when the weapon is not loaded", 1785754657));
        }
        /// <summary>
        /// Gets and returns the name of the barrel_firing_effect_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "barrel_firing_effect_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the barrel_firing_effect_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "barrel_firing_effect_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the barrel_firing_effect_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 3;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the barrel_firing_effect_block tag block.
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
