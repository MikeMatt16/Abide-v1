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
    /// Represents the generated recorded_animation_block tag block.
    /// </summary>
    internal sealed class RecordedAnimationBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordedAnimationBlock"/> class.
        /// </summary>
        public RecordedAnimationBlock()
        {
            this.Fields.Add(new StringField("name^"));
            this.Fields.Add(new CharIntegerField("version*"));
            this.Fields.Add(new CharIntegerField("raw animation data*"));
            this.Fields.Add(new CharIntegerField("unit control data version*"));
            this.Fields.Add(new PadField("", 1));
            this.Fields.Add(new ShortIntegerField("length of animation*:ticks"));
            this.Fields.Add(new PadField("", 2));
            this.Fields.Add(new PadField("", 4));
            this.Fields.Add(new DataField("recorded animation event stream*", 1, 4));
        }
        /// <summary>
        /// Gets and returns the name of the recorded_animation_block tag block.
        /// </summary>
        public override string BlockName
        {
            get
            {
                return "recorded_animation_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the recorded_animation_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "recorded_animation_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the recorded_animation_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 1024;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the recorded_animation_block tag block.
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