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
    /// Represents the generated weapon_first_person_interface_block tag block.
    /// </summary>
    public sealed class WeaponFirstPersonInterfaceBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WeaponFirstPersonInterfaceBlock"/> class.
        /// </summary>
        public WeaponFirstPersonInterfaceBlock()
        {
            this.Fields.Add(new TagReferenceField("first person model"));
            this.Fields.Add(new TagReferenceField("first person animations"));
        }
        /// <summary>
        /// Gets and returns the name of the weapon_first_person_interface_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "weapon_first_person_interface_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the weapon_first_person_interface_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "weapon_first_person_interface_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the weapon_first_person_interface_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 4;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the weapon_first_person_interface_block tag block.
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
