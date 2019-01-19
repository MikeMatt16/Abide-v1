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
    using Abide.Tag;
    using HaloTag = Abide.HaloLibrary.TagFourCc;
    
    /// <summary>
    /// Represents the generated meter (metr) tag group.
    /// </summary>
    public class Meter : Group
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Meter"/> class.
        /// </summary>
        public Meter()
        {
            // Add tag block to list.
            this.TagBlocks.Add(new MeterBlock());
        }
        /// <summary>
        /// Gets and returns the name of the meter tag group.
        /// </summary>
        public override string Name
        {
            get
            {
                return "meter";
            }
        }
        /// <summary>
        /// Gets and returns the group tag of the meter tag group.
        /// </summary>
        public override HaloTag GroupTag
        {
            get
            {
                return "metr";
            }
        }
    }
}