//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Abide.Tag.Guerilla.Generated
{
    using Abide.Tag;
    using HaloTag = Abide.HaloLibrary.TagFourCc;
    
    /// <summary>
    /// Represents the generated point_physics (pphy) tag group.
    /// </summary>
    public class PointPhysics : Group
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PointPhysics"/> class.
        /// </summary>
        public PointPhysics()
        {
            // Add tag block to list.
            this.TagBlocks.Add(new PointPhysicsBlock());
        }
        /// <summary>
        /// Gets and returns the name of the point_physics tag group.
        /// </summary>
        public override string Name
        {
            get
            {
                return "point_physics";
            }
        }
        /// <summary>
        /// Gets and returns the group tag of the point_physics tag group.
        /// </summary>
        public override HaloTag GroupTag
        {
            get
            {
                return "pphy";
            }
        }
    }
}