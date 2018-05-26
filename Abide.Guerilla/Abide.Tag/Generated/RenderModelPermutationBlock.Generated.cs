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
    /// Represents the generated render_model_permutation_block tag block.
    /// </summary>
    public sealed class RenderModelPermutationBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RenderModelPermutationBlock"/> class.
        /// </summary>
        public RenderModelPermutationBlock()
        {
            this.Fields.Add(new OldStringIdField("name*^"));
            this.Fields.Add(new ShortIntegerField("L1 section index*:(super low)"));
            this.Fields.Add(new ShortIntegerField("L2 section index*:(low)"));
            this.Fields.Add(new ShortIntegerField("L3 section index*:(medium)"));
            this.Fields.Add(new ShortIntegerField("L4 section index*:(high)"));
            this.Fields.Add(new ShortIntegerField("L5 section index*:(super high)"));
            this.Fields.Add(new ShortIntegerField("L6 section index*:(hollywood)"));
        }
        /// <summary>
        /// Gets and returns the name of the render_model_permutation_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "render_model_permutation_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the render_model_permutation_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "permutation";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the render_model_permutation_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 32;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the render_model_permutation_block tag block.
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
