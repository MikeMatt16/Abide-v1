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
    /// Represents the generated old_object_function_block tag block.
    /// </summary>
    public sealed class OldObjectFunctionBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OldObjectFunctionBlock"/> class.
        /// </summary>
        public OldObjectFunctionBlock()
        {
            this.Fields.Add(new PadField("", 76));
            this.Fields.Add(new OldStringIdField(""));
        }
        /// <summary>
        /// Gets and returns the name of the old_object_function_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "old_object_function_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the old_object_function_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "old_object_function_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the old_object_function_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 4;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the old_object_function_block tag block.
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