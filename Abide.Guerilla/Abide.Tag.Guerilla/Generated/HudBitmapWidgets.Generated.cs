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
    /// Represents the generated hud_bitmap_widgets tag block.
    /// </summary>
    public sealed class HudBitmapWidgets : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HudBitmapWidgets"/> class.
        /// </summary>
        public HudBitmapWidgets()
        {
            this.Fields.Add(new StringIdField("name"));
            this.Fields.Add(new StructField<HudWidgetInputsStructBlock>(""));
            this.Fields.Add(new StructField<HudWidgetStateDefinitionStructBlock>(""));
            this.Fields.Add(new EnumField("anchor", "health and shield", "weapon hud", "motion sensor", "scoreboard", "crosshair", "lock-on target"));
            this.Fields.Add(new WordFlagsField("flags", "flip horizontally", "flip vertically", "(scope) mirror horizontally", "(scope) mirror vertically", "(scope) stretch"));
            this.Fields.Add(new TagReferenceField("bitmap", 1651078253));
            this.Fields.Add(new TagReferenceField("shader", 1936220516));
            this.Fields.Add(new CharIntegerField("fullscreen sequence index"));
            this.Fields.Add(new CharIntegerField("halfscreen sequence index"));
            this.Fields.Add(new CharIntegerField("quarterscreen sequence index"));
            this.Fields.Add(new PadField("", 1));
            this.Fields.Add(new Point2dField("fullscreen offset"));
            this.Fields.Add(new Point2dField("halfscreen offset"));
            this.Fields.Add(new Point2dField("quarterscreen offset"));
            this.Fields.Add(new RealPoint2dField("fullscreen registration point"));
            this.Fields.Add(new RealPoint2dField("halfscreen registration point"));
            this.Fields.Add(new RealPoint2dField("quarterscreen registration point"));
            this.Fields.Add(new BlockField<HudWidgetEffectBlock>("effect", 1));
            this.Fields.Add(new EnumField("special hud type", "unspecial", "s.b. player emblem", "s.b. other player emblem", "s.b. player score meter", "s.b. other player score meter", "unit shield meter", "motion sensor", "territory meter"));
            this.Fields.Add(new PadField("", 2));
        }
        /// <summary>
        /// Gets and returns the name of the hud_bitmap_widgets tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "hud_bitmap_widgets";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the hud_bitmap_widgets tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "hud_bitmap_widgets";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the hud_bitmap_widgets tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 256;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the hud_bitmap_widgets tag block.
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
