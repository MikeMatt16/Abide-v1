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
    /// Represents the generated model_variant_object_block tag block.
    /// </summary>
    public sealed class ModelVariantObjectBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelVariantObjectBlock"/> class.
        /// </summary>
        public ModelVariantObjectBlock()
        {
            this.Fields.Add(new StringIdField("parent marker^"));
            this.Fields.Add(new StringIdField("child marker"));
            this.Fields.Add(new TagReferenceField("child object", 1868720741));
        }
        /// <summary>
        /// Gets and returns the name of the model_variant_object_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "model_variant_object_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the model_variant_object_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "object";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the model_variant_object_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 16;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the model_variant_object_block tag block.
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
