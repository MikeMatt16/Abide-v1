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
    /// Represents the generated chocolate_mountain (gldf) tag group.
    /// </summary>
    public class ChocolateMountain : Group
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChocolateMountain"/> class.
        /// </summary>
        public ChocolateMountain()
        {
            // Add tag block to list.
            this.TagBlocks.Add(new ChocolateMountainBlock());
        }
        /// <summary>
        /// Gets and returns the name of the chocolate_mountain tag group.
        /// </summary>
        public override string Name
        {
            get
            {
                return "chocolate_mountain";
            }
        }
        /// <summary>
        /// Gets and returns the group tag of the chocolate_mountain tag group.
        /// </summary>
        public override TagFourCc Tag
        {
            get
            {
                return "gldf";
            }
        }
    }
}
