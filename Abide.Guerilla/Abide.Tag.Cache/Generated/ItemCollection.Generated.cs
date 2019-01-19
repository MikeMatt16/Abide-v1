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
    using HaloTag = Abide.HaloLibrary.TagFourCc;
    
    /// <summary>
    /// Represents the generated item_collection (itmc) tag group.
    /// </summary>
    public class ItemCollection : Group
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemCollection"/> class.
        /// </summary>
        public ItemCollection()
        {
            // Add tag block to list.
            this.TagBlocks.Add(new ItemCollectionBlock());
        }
        /// <summary>
        /// Gets and returns the name of the item_collection tag group.
        /// </summary>
        public override string Name
        {
            get
            {
                return "item_collection";
            }
        }
        /// <summary>
        /// Gets and returns the group tag of the item_collection tag group.
        /// </summary>
        public override HaloTag GroupTag
        {
            get
            {
                return "itmc";
            }
        }
    }
}