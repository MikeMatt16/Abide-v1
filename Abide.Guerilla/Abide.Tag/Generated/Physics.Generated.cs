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
    using HaloTag = Abide.HaloLibrary.Tag;
    
    /// <summary>
    /// Represents the generated physics (phys) tag group.
    /// </summary>
    public class Physics : Group
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Physics"/> class.
        /// </summary>
        public Physics()
        {
            // Add tag block to list.
            this.TagBlocks.Add(new PhysicsBlock());
        }
        /// <summary>
        /// Gets and returns the name of the physics tag group.
        /// </summary>
        public override string Name
        {
            get
            {
                return "physics";
            }
        }
        /// <summary>
        /// Gets and returns the group tag of the physics tag group.
        /// </summary>
        public override HaloTag GroupTag
        {
            get
            {
                return "phys";
            }
        }
    }
}
