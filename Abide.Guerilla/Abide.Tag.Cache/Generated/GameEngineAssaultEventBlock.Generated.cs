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
    /// Represents the generated game_engine_assault_event_block tag block.
    /// </summary>
    public sealed class GameEngineAssaultEventBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameEngineAssaultEventBlock"/> class.
        /// </summary>
        public GameEngineAssaultEventBlock()
        {
            this.Fields.Add(new WordFlagsField("flags", "quantity message"));
            this.Fields.Add(new PadField("", 2));
            this.Fields.Add(new EnumField("event^", "game start", "bomb taken", "bomb dropped", "bomb returned by player", "bomb returned by timeout", "bomb captured", "bomb new defensive team", "bomb return faliure", "side switch tick", "side switch final tick", "side switch 30 seconds", "side switch 10 seconds", "bomb returned by defusing", "bomb placed on enemy post", "bomb arming started", "bomb arming completed", "bomb contested"));
            this.Fields.Add(new EnumField("audience^", "cause player", "cause team", "effect player", "effect team", "all"));
            this.Fields.Add(new PadField("", 2));
            this.Fields.Add(new PadField("", 2));
            this.Fields.Add(new StringIdField("display string"));
            this.Fields.Add(new EnumField("required field", "NONE", "cause player", "cause team", "effect player", "effect team"));
            this.Fields.Add(new EnumField("excluded audience", "NONE", "cause player", "cause team", "effect player", "effect team"));
            this.Fields.Add(new StringIdField("primary string"));
            this.Fields.Add(new LongIntegerField("primary string duration:seconds"));
            this.Fields.Add(new StringIdField("plural display string"));
            this.Fields.Add(new PadField("", 28));
            this.Fields.Add(new RealField("sound delay (announcer only)"));
            this.Fields.Add(new WordFlagsField("sound flags", "announcer sound"));
            this.Fields.Add(new PadField("", 2));
            this.Fields.Add(new TagReferenceField("sound^", 1936614433));
            this.Fields.Add(new StructField<SoundResponseExtraSoundsStructBlock>("extra sounds"));
            this.Fields.Add(new PadField("", 4));
            this.Fields.Add(new PadField("", 16));
            this.Fields.Add(new BlockField<SoundResponseDefinitionBlock>("sound permutations", 10));
        }
        /// <summary>
        /// Gets and returns the name of the game_engine_assault_event_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "game_engine_assault_event_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the game_engine_assault_event_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "game_engine_assault_event_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the game_engine_assault_event_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 128;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the game_engine_assault_event_block tag block.
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
