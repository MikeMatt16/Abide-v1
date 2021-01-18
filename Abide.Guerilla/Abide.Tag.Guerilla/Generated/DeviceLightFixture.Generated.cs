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
    /// Represents the generated device_light_fixture (lifi) tag group.
    /// </summary>
    public class DeviceLightFixture : Group
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceLightFixture"/> class.
        /// </summary>
        public DeviceLightFixture()
        {
            // Add parent object tag block to list.
            this.TagBlocks.Add(new ObjectBlock());
            // Add parent device tag block to list.
            this.TagBlocks.Add(new DeviceBlock());
            // Add tag block to list.
            this.TagBlocks.Add(new DeviceLightFixtureBlock());
        }
        /// <summary>
        /// Gets and returns the name of the device_light_fixture tag group.
        /// </summary>
        public override string Name
        {
            get
            {
                return "device_light_fixture";
            }
        }
        /// <summary>
        /// Gets and returns the group tag of the device_light_fixture tag group.
        /// </summary>
        public override TagFourCc Tag
        {
            get
            {
                return "lifi";
            }
        }
    }
}
