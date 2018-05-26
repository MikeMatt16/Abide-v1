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
    using System.IO;
    
    /// <summary>
    /// Represents the generated model_animation_runtime_data_struct_block tag block.
    /// </summary>
    public sealed class ModelAnimationRuntimeDataStructBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelAnimationRuntimeDataStructBlock"/> class.
        /// </summary>
        public ModelAnimationRuntimeDataStructBlock()
        {
            this.Fields.Add(new ExplanationField("RUN-TIME DATA", null));
            this.Fields.Add(new BlockField<InheritedAnimationBlock>("inheritence list*", 8));
            this.Fields.Add(new BlockField<WeaponClassLookupBlock>("weapon list*", 64));
            this.Fields.Add(new PadField("", 32));
            this.Fields.Add(new PadField("", 32));
        }
        /// <summary>
        /// Gets and returns the name of the model_animation_runtime_data_struct_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "model_animation_runtime_data_struct_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the model_animation_runtime_data_struct_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "model_animation_runtime_data_struct";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the model_animation_runtime_data_struct_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 1;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the model_animation_runtime_data_struct_block tag block.
        /// </summary>
        public override int Alignment
        {
            get
            {
                return 4;
            }
        }
        /// <summary>
        /// Writes the model_animation_runtime_data_struct_block tag block using the specified binary writer.
        /// </summary>
        // <param name="writer">The <see cref="BinaryWriter"/> used to write the model_animation_runtime_data_struct_block tag block.</param>
        public override void Write(BinaryWriter writer)
        {
            // Invoke base write procedure.
            base.Write(writer);
            // Post-write the tag blocks.
            ((BlockField<InheritedAnimationBlock>)(this.Fields[1])).WriteChildren(writer);
            ((BlockField<WeaponClassLookupBlock>)(this.Fields[2])).WriteChildren(writer);
        }
    }
}
