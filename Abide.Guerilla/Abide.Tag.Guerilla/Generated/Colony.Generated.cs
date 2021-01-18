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
    using System;
    using Abide.HaloLibrary;
    using Abide.Tag;
    
    /// <summary>
    /// Represents the generated colony (coln) tag group.
    /// </summary>
    public class Colony : Group
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Colony"/> class.
        /// </summary>
        public Colony()
        {
            // Add tag block to list.
            this.TagBlocks.Add(new ColonyBlock());
        }
        /// <summary>
        /// Gets and returns the name of the colony tag group.
        /// </summary>
        public override string Name
        {
            get
            {
                return "colony";
            }
        }
        /// <summary>
        /// Gets and returns the group tag of the colony tag group.
        /// </summary>
        public override TagFourCc Tag
        {
            get
            {
                return "coln";
            }
        }
    }
}
