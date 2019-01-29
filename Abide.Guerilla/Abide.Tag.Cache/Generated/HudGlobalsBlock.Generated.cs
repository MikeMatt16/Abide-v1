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
    /// Represents the generated hud_globals_block tag block.
    /// </summary>
    public sealed class HudGlobalsBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HudGlobalsBlock"/> class.
        /// </summary>
        public HudGlobalsBlock()
        {
            this.Fields.Add(new ExplanationField("Messaging parameters", ""));
            this.Fields.Add(new EnumField("anchor", "top left", "top right", "bottom left", "bottom right", "center", "crosshair"));
            this.Fields.Add(new PadField("", 2));
            this.Fields.Add(new PadField("", 32));
            this.Fields.Add(new Point2dField("anchor offset"));
            this.Fields.Add(new RealField("width scale"));
            this.Fields.Add(new RealField("height scale"));
            this.Fields.Add(new WordFlagsField("scaling flags", "don\'t scale offset", "don\'t scale size"));
            this.Fields.Add(new PadField("", 2));
            this.Fields.Add(new PadField("", 20));
            this.Fields.Add(new TagReferenceField("obsolete1", "bitm"));
            this.Fields.Add(new TagReferenceField("obsolete2", "bitm"));
            this.Fields.Add(new RealField("up time"));
            this.Fields.Add(new RealField("fade time"));
            this.Fields.Add(new RealArgbColorField("icon color"));
            this.Fields.Add(new RealArgbColorField("text color"));
            this.Fields.Add(new RealField("text spacing"));
            this.Fields.Add(new TagReferenceField("item message text", "unic"));
            this.Fields.Add(new TagReferenceField("icon bitmap", "bitm"));
            this.Fields.Add(new TagReferenceField("alternate icon text", "unic"));
            this.Fields.Add(new BlockField<HudButtonIconBlock>("button icons", 18));
            this.Fields.Add(new ExplanationField("HUD HELP TEXT COLOR", ""));
            this.Fields.Add(new ArgbColorField("default color"));
            this.Fields.Add(new ArgbColorField("flashing color"));
            this.Fields.Add(new RealField("flash period"));
            this.Fields.Add(new RealField("flash delay#time between flashes"));
            this.Fields.Add(new ShortIntegerField("number of flashes"));
            this.Fields.Add(new WordFlagsField("flash flags", "reverse default/flashing colors"));
            this.Fields.Add(new RealField("flash length#time of each flash"));
            this.Fields.Add(new ArgbColorField("disabled color"));
            this.Fields.Add(new PadField("", 4));
            this.Fields.Add(new ExplanationField("Other hud messaging data", ""));
            this.Fields.Add(new TagReferenceField("hud messages", "hmt "));
            this.Fields.Add(new ExplanationField("Objective colors", ""));
            this.Fields.Add(new ArgbColorField("default color"));
            this.Fields.Add(new ArgbColorField("flashing color"));
            this.Fields.Add(new RealField("flash period"));
            this.Fields.Add(new RealField("flash delay#time between flashes"));
            this.Fields.Add(new ShortIntegerField("number of flashes"));
            this.Fields.Add(new WordFlagsField("flash flags", "reverse default/flashing colors"));
            this.Fields.Add(new RealField("flash length#time of each flash"));
            this.Fields.Add(new ArgbColorField("disabled color"));
            this.Fields.Add(new ShortIntegerField("uptime ticks"));
            this.Fields.Add(new ShortIntegerField("fade ticks"));
            this.Fields.Add(new ExplanationField("Waypoint parameters", "The offset values are how much the waypoint rectangle border is offset from the s" +
                        "afe camera bounds"));
            this.Fields.Add(new RealField("top offset"));
            this.Fields.Add(new RealField("bottom offset"));
            this.Fields.Add(new RealField("left offset"));
            this.Fields.Add(new RealField("right offset"));
            this.Fields.Add(new PadField("", 32));
            this.Fields.Add(new TagReferenceField("arrow bitmap", "bitm"));
            this.Fields.Add(new BlockField<HudGlobalsWaypointArrowBlock>("waypoint arrows", 16));
            this.Fields.Add(new PadField("", 80));
            this.Fields.Add(new ExplanationField("Multiplayer parameters", ""));
            this.Fields.Add(new RealField("hud scale in multiplayer"));
            this.Fields.Add(new PadField("", 256));
            this.Fields.Add(new ExplanationField("Hud globals", ""));
            this.Fields.Add(new PadField("", 16));
            this.Fields.Add(new RealField("motion sensor range"));
            this.Fields.Add(new RealField("motion sensor velocity sensitivity#how fast something moves to show up on the mot" +
                        "ion sensor"));
            this.Fields.Add(new RealField("motion sensor scale [DON\'T TOUCH EVER]*"));
            this.Fields.Add(new Rectangle2dField("default chapter title bounds"));
            this.Fields.Add(new PadField("", 44));
            this.Fields.Add(new ExplanationField("Hud damage indicators", ""));
            this.Fields.Add(new ShortIntegerField("top offset"));
            this.Fields.Add(new ShortIntegerField("bottom offset"));
            this.Fields.Add(new ShortIntegerField("left offset"));
            this.Fields.Add(new ShortIntegerField("right offset"));
            this.Fields.Add(new PadField("", 32));
            this.Fields.Add(new TagReferenceField("indicator bitmap", "bitm"));
            this.Fields.Add(new ShortIntegerField("sequence index"));
            this.Fields.Add(new ShortIntegerField("multiplayer sequence index"));
            this.Fields.Add(new ArgbColorField("color"));
            this.Fields.Add(new PadField("", 16));
            this.Fields.Add(new ExplanationField("Hud timer definitions", ""));
            this.Fields.Add(new ExplanationField("Not much time left flash color", ""));
            this.Fields.Add(new ArgbColorField("default color"));
            this.Fields.Add(new ArgbColorField("flashing color"));
            this.Fields.Add(new RealField("flash period"));
            this.Fields.Add(new RealField("flash delay#time between flashes"));
            this.Fields.Add(new ShortIntegerField("number of flashes"));
            this.Fields.Add(new WordFlagsField("flash flags", "reverse default/flashing colors"));
            this.Fields.Add(new RealField("flash length#time of each flash"));
            this.Fields.Add(new ArgbColorField("disabled color"));
            this.Fields.Add(new PadField("", 4));
            this.Fields.Add(new ExplanationField("Time out flash color", ""));
            this.Fields.Add(new ArgbColorField("default color"));
            this.Fields.Add(new ArgbColorField("flashing color"));
            this.Fields.Add(new RealField("flash period"));
            this.Fields.Add(new RealField("flash delay#time between flashes"));
            this.Fields.Add(new ShortIntegerField("number of flashes"));
            this.Fields.Add(new WordFlagsField("flash flags", "reverse default/flashing colors"));
            this.Fields.Add(new RealField("flash length#time of each flash"));
            this.Fields.Add(new ArgbColorField("disabled color"));
            this.Fields.Add(new PadField("", 4));
            this.Fields.Add(new PadField("", 40));
            this.Fields.Add(new TagReferenceField("carnage report bitmap", "bitm"));
            this.Fields.Add(new ExplanationField("Hud crap that wouldn\'t fit anywhere else", ""));
            this.Fields.Add(new ShortIntegerField("loading begin text"));
            this.Fields.Add(new ShortIntegerField("loading end text"));
            this.Fields.Add(new ShortIntegerField("checkpoint begin text"));
            this.Fields.Add(new ShortIntegerField("checkpoint end text"));
            this.Fields.Add(new TagReferenceField("checkpoint sound", "snd!"));
            this.Fields.Add(new PadField("", 96));
            this.Fields.Add(new StructField<GlobalNewHudGlobalsStructBlock>("new globals"));
        }
        /// <summary>
        /// Gets and returns the name of the hud_globals_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "hud_globals_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the hud_globals_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "hud_globals";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the hud_globals_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 1;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the hud_globals_block tag block.
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
