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
    /// Represents the generated scalar_function_struct_block tag block.
    /// </summary>
    public sealed class ScalarFunctionStructBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScalarFunctionStructBlock"/> class.
        /// </summary>
        public ScalarFunctionStructBlock()
        {
            this.Fields.Add(new StructField<MappingFunctionBlock>("function"));
        }
        /// <summary>
        /// Gets and returns the name of the scalar_function_struct_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "scalar_function_struct_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the scalar_function_struct_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "scalar_function_struct";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the scalar_function_struct_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 1;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the scalar_function_struct_block tag block.
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
