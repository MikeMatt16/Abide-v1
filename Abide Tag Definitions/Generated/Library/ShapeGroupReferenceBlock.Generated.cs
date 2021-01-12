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
    /// Represents the generated shape_group_reference_block tag block.
    /// </summary>
    public sealed class ShapeGroupReferenceBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShapeGroupReferenceBlock"/> class.
        /// </summary>
        public ShapeGroupReferenceBlock()
        {
            this.Fields.Add(new ExplanationField("Unused Debug Geometry Shapes", "This is the old way"));
            this.Fields.Add(new BlockField<ShapeBlockReferenceBlock>("shapes", 32));
            this.Fields.Add(new ExplanationField("Model-Light Groups", "Specify commonly used model/light groups here"));
            this.Fields.Add(new BlockField<UiModelSceneReferenceBlock>("model scene blocks", 32));
            this.Fields.Add(new ExplanationField("Bitmaps", "Specify more flavor bitmaps here"));
            this.Fields.Add(new BlockField<BitmapBlockReferenceBlock>("bitmap blocks", 64));
        }
        /// <summary>
        /// Gets and returns the name of the shape_group_reference_block tag block.
        /// </summary>
        public override string BlockName
        {
            get
            {
                return "shape_group_reference_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the shape_group_reference_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "shape_group_reference_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the shape_group_reference_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 64;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the shape_group_reference_block tag block.
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