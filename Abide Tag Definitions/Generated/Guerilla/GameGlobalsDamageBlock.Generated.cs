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
    using System;
    using Abide.HaloLibrary;
    using Abide.Tag;
    
    /// <summary>
    /// Represents the generated game_globals_damage_block tag block.
    /// </summary>
    public sealed class GameGlobalsDamageBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameGlobalsDamageBlock"/> class.
        /// </summary>
        public GameGlobalsDamageBlock()
        {
            this.Fields.Add(new BlockField<DamageGroupBlock>("damage groups", 2147483647));
        }
        /// <summary>
        /// Gets and returns the name of the game_globals_damage_block tag block.
        /// </summary>
        public override string BlockName
        {
            get
            {
                return "game_globals_damage_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the game_globals_damage_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "game_globals_damage_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the game_globals_damage_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 1;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the game_globals_damage_block tag block.
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