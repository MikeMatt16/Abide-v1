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
    /// Represents the generated scenario_vehicles_resource (*ehi) tag group.
    /// </summary>
    public class ScenarioVehiclesResource : Group
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScenarioVehiclesResource"/> class.
        /// </summary>
        public ScenarioVehiclesResource()
        {
            // Add tag block to list.
            this.TagBlocks.Add(new ScenarioVehiclesResourceBlock());
        }
        /// <summary>
        /// Gets and returns the name of the scenario_vehicles_resource tag group.
        /// </summary>
        public override string Name
        {
            get
            {
                return "scenario_vehicles_resource";
            }
        }
        /// <summary>
        /// Gets and returns the group tag of the scenario_vehicles_resource tag group.
        /// </summary>
        public override TagFourCc Tag
        {
            get
            {
                return "*ehi";
            }
        }
    }
}
