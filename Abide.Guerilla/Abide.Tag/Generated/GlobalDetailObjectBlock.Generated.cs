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
    /// Represents the generated global_detail_object_block tag block.
    /// </summary>
    public sealed class GlobalDetailObjectBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalDetailObjectBlock"/> class.
        /// </summary>
        public GlobalDetailObjectBlock()
        {
            this.Fields.Add(new CharIntegerField("*"));
            this.Fields.Add(new CharIntegerField("*"));
            this.Fields.Add(new CharIntegerField("*"));
            this.Fields.Add(new CharIntegerField("*"));
            this.Fields.Add(new ShortIntegerField("*"));
        }
        /// <summary>
        /// Gets and returns the name of the global_detail_object_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "global_detail_object_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the global_detail_object_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "global_detail_object_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the global_detail_object_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 2097152;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the global_detail_object_block tag block.
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
