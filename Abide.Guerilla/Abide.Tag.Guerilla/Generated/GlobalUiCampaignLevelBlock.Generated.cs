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
    using Abide.Tag;
    
    /// <summary>
    /// Represents the generated global_ui_campaign_level_block tag block.
    /// </summary>
    public sealed class GlobalUiCampaignLevelBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalUiCampaignLevelBlock"/> class.
        /// </summary>
        public GlobalUiCampaignLevelBlock()
        {
            this.Fields.Add(new LongIntegerField("Campaign ID"));
            this.Fields.Add(new LongIntegerField("Map ID"));
            this.Fields.Add(new TagReferenceField("Bitmap", 1651078253));
            this.Fields.Add(new SkipField("", 576));
            this.Fields.Add(new SkipField("", 2304));
        }
        /// <summary>
        /// Gets and returns the name of the global_ui_campaign_level_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "global_ui_campaign_level_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the global_ui_campaign_level_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "global_ui_campaign_level_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the global_ui_campaign_level_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 20;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the global_ui_campaign_level_block tag block.
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
