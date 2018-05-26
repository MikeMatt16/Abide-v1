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
    /// Represents the generated character_charge_block tag block.
    /// </summary>
    public sealed class CharacterChargeBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterChargeBlock"/> class.
        /// </summary>
        public CharacterChargeBlock()
        {
            this.Fields.Add(new LongFlagsField("Charge flags", "offhand melee allowed", "berserk whenever charge"));
            this.Fields.Add(new RealField("melee consider range"));
            this.Fields.Add(new RealField("melee chance#chance of initiating a melee within a 1 second period"));
            this.Fields.Add(new RealField("melee attack range"));
            this.Fields.Add(new RealField("melee abort range"));
            this.Fields.Add(new RealField("melee attack timeout:seconds#Give up after given amount of time spent charging"));
            this.Fields.Add(new RealField("melee attack delay timer:seconds#don\'t attempt again before given time since last" +
                        " melee"));
            this.Fields.Add(new RealBoundsField("melee leap range"));
            this.Fields.Add(new RealField("melee leap chance"));
            this.Fields.Add(new RealField("ideal leap velocity"));
            this.Fields.Add(new RealField("max leap velocity"));
            this.Fields.Add(new RealField("melee leap ballistic"));
            this.Fields.Add(new RealField("melee delay timer:seconds#time between melee leaps"));
            this.Fields.Add(new TagReferenceField("berserk weapon#when I berserk, I pull out a ..."));
        }
        /// <summary>
        /// Gets and returns the name of the character_charge_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "character_charge_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the character_charge_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "character_charge_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the character_charge_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 3;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the character_charge_block tag block.
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
