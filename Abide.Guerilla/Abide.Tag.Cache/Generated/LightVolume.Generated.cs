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
    /// Represents the generated light_volume (MGS2) tag group.
    /// </summary>
    public class LightVolume : Group
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LightVolume"/> class.
        /// </summary>
        public LightVolume()
        {
            // Add tag block to list.
            this.TagBlocks.Add(new LightVolumeBlock());
        }
        /// <summary>
        /// Gets and returns the name of the light_volume tag group.
        /// </summary>
        public override string Name
        {
            get
            {
                return "light_volume";
            }
        }
        /// <summary>
        /// Gets and returns the group tag of the light_volume tag group.
        /// </summary>
        public override TagFourCc Tag
        {
            get
            {
                return "MGS2";
            }
        }
    }
}
