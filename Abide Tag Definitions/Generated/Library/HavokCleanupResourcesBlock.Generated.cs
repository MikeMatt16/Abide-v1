//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Abide.HaloLibrary.Halo2.Retail.Tag.Generated
{
    using System;
    using Abide.HaloLibrary;
    using Abide.HaloLibrary.Halo2.Retail.Tag;
    
    /// <summary>
    /// Represents the generated havok_cleanup_resources_block tag block.
    /// </summary>
    public sealed class HavokCleanupResourcesBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HavokCleanupResourcesBlock"/> class.
        /// </summary>
        public HavokCleanupResourcesBlock()
        {
            this.Fields.Add(new TagReferenceField("object cleanup effect", 1701209701));
        }
        /// <summary>
        /// Gets and returns the name of the havok_cleanup_resources_block tag block.
        /// </summary>
        public override string BlockName
        {
            get
            {
                return "havok_cleanup_resources_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the havok_cleanup_resources_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "havok_cleanup_resources_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the havok_cleanup_resources_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 1;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the havok_cleanup_resources_block tag block.
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