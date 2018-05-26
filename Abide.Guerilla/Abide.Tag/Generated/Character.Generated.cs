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
    using HaloTag = Abide.HaloLibrary.Tag;
    
    /// <summary>
    /// Represents the generated character (char) tag group.
    /// </summary>
    public class Character : Group
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Character"/> class.
        /// </summary>
        public Character()
        {
            // Add tag block to list.
            this.TagBlocks.Add(new CharacterBlock());
        }
        /// <summary>
        /// Gets and returns the name of the character tag group.
        /// </summary>
        public override string Name
        {
            get
            {
                return "character";
            }
        }
        /// <summary>
        /// Gets and returns the group tag of the character tag group.
        /// </summary>
        public override HaloTag GroupTag
        {
            get
            {
                return "char";
            }
        }
    }
}
