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
    /// Represents the generated planes_block tag block.
    /// </summary>
    public sealed class PlanesBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlanesBlock"/> class.
        /// </summary>
        public PlanesBlock()
        {
            this.Fields.Add(new RealPlane3dField("Plane*"));
        }
        /// <summary>
        /// Gets and returns the name of the planes_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "planes_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the planes_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "planes_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the planes_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 65536;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the planes_block tag block.
        /// </summary>
        public override int Alignment
        {
            get
            {
                return 16;
            }
        }
    }
}
