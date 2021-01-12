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
    /// Represents the generated water_geometry_section_block tag block.
    /// </summary>
    internal sealed class WaterGeometrySectionBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WaterGeometrySectionBlock"/> class.
        /// </summary>
        public WaterGeometrySectionBlock()
        {
            this.Fields.Add(new StructField<GlobalGeometrySectionStructBlock>("Section*"));
        }
        /// <summary>
        /// Gets and returns the name of the water_geometry_section_block tag block.
        /// </summary>
        public override string BlockName
        {
            get
            {
                return "water_geometry_section_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the water_geometry_section_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "water_geometry_section_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the water_geometry_section_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 1;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the water_geometry_section_block tag block.
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