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
    /// Represents the generated decorator_placement_block tag block.
    /// </summary>
    internal sealed class DecoratorPlacementBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DecoratorPlacementBlock"/> class.
        /// </summary>
        public DecoratorPlacementBlock()
        {
            this.Fields.Add(new LongIntegerField("Internal Data 1"));
            this.Fields.Add(new LongIntegerField("Compressed Position"));
            this.Fields.Add(new RgbColorField("Tint Color"));
            this.Fields.Add(new RgbColorField("Lightmap Color"));
            this.Fields.Add(new LongIntegerField("Compressed Light Direction"));
            this.Fields.Add(new LongIntegerField("Compressed Light 2 Direction"));
        }
        /// <summary>
        /// Gets and returns the name of the decorator_placement_block tag block.
        /// </summary>
        public override string BlockName
        {
            get
            {
                return "decorator_placement_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the decorator_placement_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "decorator_placement_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the decorator_placement_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 32768;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the decorator_placement_block tag block.
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