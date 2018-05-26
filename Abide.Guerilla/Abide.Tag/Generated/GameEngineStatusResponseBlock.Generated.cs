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
    /// Represents the generated game_engine_status_response_block tag block.
    /// </summary>
    public sealed class GameEngineStatusResponseBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameEngineStatusResponseBlock"/> class.
        /// </summary>
        public GameEngineStatusResponseBlock()
        {
            this.Fields.Add(new WordFlagsField("flags", "unused"));
            this.Fields.Add(new PadField("", 2));
            this.Fields.Add(new EnumField("state^", "waiting for space to clear", "observing", "respawning soon", "sitting out", "out of lives", "playing (winning)", "playing (tied)", "playing (losing)", "game over (won)", "game over (tied)", "game over (lost)", "you have flag", "enemy has flag", "flag not home", "carrying oddball", "you are juggy", "you control hill", "switching sides soon", "player recently started", "you have bomb", "flag contested", "bomb contested", "limited lives left (multiple)", "limited lives left (single)", "limited lives left (final)", "playing (winning, unlimited)", "playing (tied, unlimited)", "playing (losing, unlimited)"));
            this.Fields.Add(new PadField("", 2));
            this.Fields.Add(new StringIdField("ffa message"));
            this.Fields.Add(new StringIdField("team message"));
            this.Fields.Add(new TagReferenceField(""));
            this.Fields.Add(new PadField("", 4));
        }
        /// <summary>
        /// Gets and returns the name of the game_engine_status_response_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "game_engine_status_response_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the game_engine_status_response_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "game_engine_status_response_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the game_engine_status_response_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 32;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the game_engine_status_response_block tag block.
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
