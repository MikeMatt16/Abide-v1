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
    /// Represents the generated rigid_body_indices_block tag block.
    /// </summary>
    public sealed class RigidBodyIndicesBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RigidBodyIndicesBlock"/> class.
        /// </summary>
        public RigidBodyIndicesBlock()
        {
            this.Fields.Add(new ShortBlockIndexField("rigid body*^"));
        }
        /// <summary>
        /// Gets and returns the name of the rigid_body_indices_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "rigid_body_indices_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the rigid_body_indices_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "rigid_body_indices_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the rigid_body_indices_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 64;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the rigid_body_indices_block tag block.
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
