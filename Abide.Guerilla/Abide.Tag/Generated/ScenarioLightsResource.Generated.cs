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
    /// Represents the generated scenario_lights_resource (*igh) tag group.
    /// </summary>
    public class ScenarioLightsResource : Group
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScenarioLightsResource"/> class.
        /// </summary>
        public ScenarioLightsResource()
        {
            // Add tag block to list.
            this.TagBlocks.Add(new ScenarioLightsResourceBlock());
        }
        /// <summary>
        /// Gets and returns the name of the scenario_lights_resource tag group.
        /// </summary>
        public override string Name
        {
            get
            {
                return "scenario_lights_resource";
            }
        }
        /// <summary>
        /// Gets and returns the group tag of the scenario_lights_resource tag group.
        /// </summary>
        public override HaloTag GroupTag
        {
            get
            {
                return "*igh";
            }
        }
    }
}
