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
    
    /// <summary>
    /// Represents the generated scenario_description_block tag block.
    /// </summary>
    public sealed class ScenarioDescriptionBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScenarioDescriptionBlock"/> class.
        /// </summary>
        public ScenarioDescriptionBlock()
        {
            this.Fields.Add(new ExplanationField("net map info", null));
            this.Fields.Add(new TagReferenceField("descriptive bitmap"));
            this.Fields.Add(new TagReferenceField("displayed map name"));
            this.Fields.Add(new StringField("scenario tag directory path#this is the path to the directory containing the scen" +
                        "ario tag file of the same name"));
            this.Fields.Add(new PadField("", 4));
        }
        /// <summary>
        /// Gets and returns the name of the scenario_description_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "scenario_description_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the scenario_description_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "scenario_description_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the scenario_description_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 32;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the scenario_description_block tag block.
        /// </summary>
        public override int Alignment
        {
            get
            {
                return 4;
            }
        }
    }
}
