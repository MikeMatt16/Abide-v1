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
    /// Represents the generated model_variant_state_block tag block.
    /// </summary>
    public sealed class ModelVariantStateBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelVariantStateBlock"/> class.
        /// </summary>
        public ModelVariantStateBlock()
        {
            this.Fields.Add(new StringIdField("permutation name"));
            this.Fields.Add(new PadField("", 1));
            this.Fields.Add(new ByteFlagsField("property flags", "blurred", "hella blurred", "shielded"));
            this.Fields.Add(new EnumField("state^", "default", "minor damage", "medium damage", "major damage", "destroyed"));
            this.Fields.Add(new TagReferenceField("looping effect#played while the model is in this state", 1701209701));
            this.Fields.Add(new StringIdField("looping effect marker name"));
            this.Fields.Add(new RealFractionField("initial probability"));
        }
        /// <summary>
        /// Gets and returns the name of the model_variant_state_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "model_variant_state_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the model_variant_state_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "model_variant_state_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the model_variant_state_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 10;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the model_variant_state_block tag block.
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
