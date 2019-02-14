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
    
    /// <summary>
    /// Represents the generated hud_waypoint_arrow_block tag block.
    /// </summary>
    public sealed class HudWaypointArrowBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HudWaypointArrowBlock"/> class.
        /// </summary>
        public HudWaypointArrowBlock()
        {
            this.Fields.Add(new TagReferenceField("bitmap", 1651078253));
            this.Fields.Add(new TagReferenceField("shader", 1936220516));
            this.Fields.Add(new ShortIntegerField("sequence index"));
            this.Fields.Add(new PadField("", 2));
            this.Fields.Add(new RealField("smallest size"));
            this.Fields.Add(new RealField("smallest distance"));
            this.Fields.Add(new TagReferenceField("border bitmap", 1651078253));
        }
        /// <summary>
        /// Gets and returns the name of the hud_waypoint_arrow_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "hud_waypoint_arrow_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the hud_waypoint_arrow_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "hud_waypoint_arrow_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the hud_waypoint_arrow_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 4;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the hud_waypoint_arrow_block tag block.
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
