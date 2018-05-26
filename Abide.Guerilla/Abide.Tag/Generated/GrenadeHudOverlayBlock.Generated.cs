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
    /// Represents the generated grenade_hud_overlay_block tag block.
    /// </summary>
    public sealed class GrenadeHudOverlayBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GrenadeHudOverlayBlock"/> class.
        /// </summary>
        public GrenadeHudOverlayBlock()
        {
            this.Fields.Add(new Point2dField("anchor offset"));
            this.Fields.Add(new RealField("width scale"));
            this.Fields.Add(new RealField("height scale"));
            this.Fields.Add(new WordFlagsField("scaling flags", "don\'t scale offset", "don\'t scale size"));
            this.Fields.Add(new PadField("", 2));
            this.Fields.Add(new PadField("", 20));
            this.Fields.Add(new ArgbColorField("default color"));
            this.Fields.Add(new ArgbColorField("flashing color"));
            this.Fields.Add(new RealField("flash period"));
            this.Fields.Add(new RealField("flash delay#time between flashes"));
            this.Fields.Add(new ShortIntegerField("number of flashes"));
            this.Fields.Add(new WordFlagsField("flash flags", "reverse default/flashing colors"));
            this.Fields.Add(new RealField("flash length#time of each flash"));
            this.Fields.Add(new ArgbColorField("disabled color"));
            this.Fields.Add(new PadField("", 4));
            this.Fields.Add(new RealField("frame rate"));
            this.Fields.Add(new ShortIntegerField("sequence index"));
            this.Fields.Add(new WordFlagsField("type", "show on flashing", "show on empty", "show on default", "show always"));
            this.Fields.Add(new LongFlagsField("flags", "flashes when active"));
            this.Fields.Add(new PadField("", 16));
            this.Fields.Add(new PadField("", 40));
        }
        /// <summary>
        /// Gets and returns the name of the grenade_hud_overlay_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "grenade_hud_overlay_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the grenade_hud_overlay_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "grenade_hud_overlay_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the grenade_hud_overlay_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 16;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the grenade_hud_overlay_block tag block.
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
