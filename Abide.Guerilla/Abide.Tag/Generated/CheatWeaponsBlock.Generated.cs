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
    /// Represents the generated cheat_weapons_block tag block.
    /// </summary>
    public sealed class CheatWeaponsBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheatWeaponsBlock"/> class.
        /// </summary>
        public CheatWeaponsBlock()
        {
            this.Fields.Add(new TagReferenceField("weapon^"));
        }
        /// <summary>
        /// Gets and returns the name of the cheat_weapons_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "cheat_weapons_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the cheat_weapons_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "cheat_weapons_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the cheat_weapons_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 20;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the cheat_weapons_block tag block.
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
