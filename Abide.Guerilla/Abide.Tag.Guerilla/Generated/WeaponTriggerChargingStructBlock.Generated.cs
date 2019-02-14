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
    /// Represents the generated weapon_trigger_charging_struct_block tag block.
    /// </summary>
    public sealed class WeaponTriggerChargingStructBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WeaponTriggerChargingStructBlock"/> class.
        /// </summary>
        public WeaponTriggerChargingStructBlock()
        {
            this.Fields.Add(new ExplanationField("CHARGING", ""));
            this.Fields.Add(new RealField("charging time:seconds#the amount of time it takes for this trigger to become full" +
                        "y charged"));
            this.Fields.Add(new RealField("charged time:seconds#the amount of time this trigger can be charged before becomi" +
                        "ng overcharged"));
            this.Fields.Add(new EnumField("overcharged action", "none", "explode", "discharge"));
            this.Fields.Add(new PadField("", 2));
            this.Fields.Add(new RealField("charged illumination:[0,1]#the amount of illumination given off when the weapon i" +
                        "s fully charged"));
            this.Fields.Add(new RealField("spew time:seconds#length of time the weapon will spew (fire continuously) while d" +
                        "ischarging"));
            this.Fields.Add(new TagReferenceField("charging effect#the charging effect is created once when the trigger begins to ch" +
                        "arge", -3));
            this.Fields.Add(new TagReferenceField("charging damage effect#the charging effect is created once when the trigger begin" +
                        "s to charge", 1785754657));
        }
        /// <summary>
        /// Gets and returns the name of the weapon_trigger_charging_struct_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "weapon_trigger_charging_struct_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the weapon_trigger_charging_struct_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "weapon_trigger_charging_struct";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the weapon_trigger_charging_struct_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 1;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the weapon_trigger_charging_struct_block tag block.
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
