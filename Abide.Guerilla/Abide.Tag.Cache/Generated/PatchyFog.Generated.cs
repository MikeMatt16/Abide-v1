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
    /// Represents the generated patchy_fog (fpch) tag group.
    /// </summary>
    public class PatchyFog : Group
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PatchyFog"/> class.
        /// </summary>
        public PatchyFog()
        {
            // Add tag block to list.
            this.TagBlocks.Add(new PatchyFogBlock());
        }
        /// <summary>
        /// Gets and returns the name of the patchy_fog tag group.
        /// </summary>
        public override string Name
        {
            get
            {
                return "patchy_fog";
            }
        }
        /// <summary>
        /// Gets and returns the group tag of the patchy_fog tag group.
        /// </summary>
        public override TagFourCc Tag
        {
            get
            {
                return "fpch";
            }
        }
    }
}
