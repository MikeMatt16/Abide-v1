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
    /// Represents the generated decorator_models_block tag block.
    /// </summary>
    public sealed class DecoratorModelsBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DecoratorModelsBlock"/> class.
        /// </summary>
        public DecoratorModelsBlock()
        {
            this.Fields.Add(new StringIdField("model name*"));
            this.Fields.Add(new ShortIntegerField("index start*"));
            this.Fields.Add(new ShortIntegerField("index count*"));
        }
        /// <summary>
        /// Gets and returns the name of the decorator_models_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "decorator_models_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the decorator_models_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "decorator_models_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the decorator_models_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 256;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the decorator_models_block tag block.
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