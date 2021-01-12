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
    /// Represents the generated text_value_pair_block tag block.
    /// </summary>
    internal sealed class TextValuePairBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextValuePairBlock"/> class.
        /// </summary>
        public TextValuePairBlock()
        {
            this.Fields.Add(new TagReferenceField("value pairs^", 1936288889));
        }
        /// <summary>
        /// Gets and returns the name of the text_value_pair_block tag block.
        /// </summary>
        public override string BlockName
        {
            get
            {
                return "text_value_pair_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the text_value_pair_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "text_value_pair_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the text_value_pair_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 32;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the text_value_pair_block tag block.
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