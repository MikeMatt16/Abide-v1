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
    /// Represents the generated text_value_pair_definition (sily) tag group.
    /// </summary>
    public class TextValuePairDefinition : Group
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextValuePairDefinition"/> class.
        /// </summary>
        public TextValuePairDefinition()
        {
            // Add tag block to list.
            this.TagBlocks.Add(new TextValuePairDefinitionBlock());
        }
        /// <summary>
        /// Gets and returns the name of the text_value_pair_definition tag group.
        /// </summary>
        public override string Name
        {
            get
            {
                return "text_value_pair_definition";
            }
        }
        /// <summary>
        /// Gets and returns the group tag of the text_value_pair_definition tag group.
        /// </summary>
        public override HaloTag GroupTag
        {
            get
            {
                return "sily";
            }
        }
    }
}
