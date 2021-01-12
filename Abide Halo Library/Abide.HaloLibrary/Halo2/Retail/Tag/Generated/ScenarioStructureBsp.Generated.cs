//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Abide.HaloLibrary.Halo2.Retail.Tag.Generated
{
    using System;
    using Abide.HaloLibrary;
    using Abide.HaloLibrary.Halo2.Retail.Tag;
    
    /// <summary>
    /// Represents the generated scenario_structure_bsp (sbsp) tag group.
    /// </summary>
    internal class ScenarioStructureBsp : Group
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScenarioStructureBsp"/> class.
        /// </summary>
        public ScenarioStructureBsp()
        {
            // Add tag block to list.
            this.TagBlocks.Add(new ScenarioStructureBspBlock());
        }
        /// <summary>
        /// Gets and returns the name of the scenario_structure_bsp tag group.
        /// </summary>
        public override string GroupName
        {
            get
            {
                return "scenario_structure_bsp";
            }
        }
        /// <summary>
        /// Gets and returns the group tag of the scenario_structure_bsp tag group.
        /// </summary>
        public override TagFourCc GroupTag
        {
            get
            {
                return "sbsp";
            }
        }
    }
}