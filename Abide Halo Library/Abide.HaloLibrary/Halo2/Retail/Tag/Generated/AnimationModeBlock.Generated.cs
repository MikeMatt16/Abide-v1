//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Abide.HaloLibrary.Halo2.Retail.Tag.Generated
{
    using System;
    using Abide.HaloLibrary;
    using Abide.HaloLibrary.Halo2.Retail.Tag;
    
    /// <summary>
    /// Represents the generated animation_mode_block tag block.
    /// </summary>
    internal sealed class AnimationModeBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnimationModeBlock"/> class.
        /// </summary>
        public AnimationModeBlock()
        {
            this.Fields.Add(new StringIdField("label^"));
            this.Fields.Add(new BlockField<WeaponClassBlock>("weapon class|AABBCC", 64));
            this.Fields.Add(new BlockField<AnimationIkBlock>("mode ik|AABBCC", 8));
        }
        /// <summary>
        /// Gets and returns the name of the animation_mode_block tag block.
        /// </summary>
        public override string BlockName
        {
            get
            {
                return "animation_mode_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the animation_mode_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "animation_mode_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the animation_mode_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 512;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the animation_mode_block tag block.
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