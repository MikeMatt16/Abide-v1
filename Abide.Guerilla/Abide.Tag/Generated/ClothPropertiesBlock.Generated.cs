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
    /// Represents the generated cloth_properties_block tag block.
    /// </summary>
    public sealed class ClothPropertiesBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClothPropertiesBlock"/> class.
        /// </summary>
        public ClothPropertiesBlock()
        {
            this.Fields.Add(new EnumField("Integration type*", "verlet"));
            this.Fields.Add(new ShortIntegerField("Number iterations#[1-8] sug 1"));
            this.Fields.Add(new RealField("weight#[-10.0 - 10.0] sug 1.0"));
            this.Fields.Add(new RealField("drag#[0.0 - 0.5] sug 0.07"));
            this.Fields.Add(new RealField("wind_scale#[0.0 - 3.0] sug 1.0"));
            this.Fields.Add(new RealField("wind_flappiness_scale#[0.0 - 1.0] sug 0.75"));
            this.Fields.Add(new RealField("longest_rod#[1.0 - 10.0] sug 3.5"));
            this.Fields.Add(new PadField("", 24));
        }
        /// <summary>
        /// Gets and returns the name of the cloth_properties_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "cloth_properties_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the cloth_properties_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "cloth_properties";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the cloth_properties_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 1;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the cloth_properties_block tag block.
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
