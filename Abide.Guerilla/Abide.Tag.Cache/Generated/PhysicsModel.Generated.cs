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
    using System;
    using Abide.HaloLibrary;
    using Abide.Tag;
    
    /// <summary>
    /// Represents the generated physics_model (phmo) tag group.
    /// </summary>
    public class PhysicsModel : Group
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhysicsModel"/> class.
        /// </summary>
        public PhysicsModel()
        {
            // Add tag block to list.
            this.TagBlocks.Add(new PhysicsModelBlock());
        }
        /// <summary>
        /// Gets and returns the name of the physics_model tag group.
        /// </summary>
        public override string Name
        {
            get
            {
                return "physics_model";
            }
        }
        /// <summary>
        /// Gets and returns the group tag of the physics_model tag group.
        /// </summary>
        public override TagFourCc Tag
        {
            get
            {
                return "phmo";
            }
        }
    }
}
