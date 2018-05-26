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
    /// Represents the generated color_block tag block.
    /// </summary>
    public sealed class ColorBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColorBlock"/> class.
        /// </summary>
        public ColorBlock()
        {
            this.Fields.Add(new StringField("name^"));
            this.Fields.Add(new RealArgbColorField("color"));
        }
        /// <summary>
        /// Gets and returns the name of the color_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "color_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the color_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "color_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the color_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 512;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the color_block tag block.
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
