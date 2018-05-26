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
    /// Represents the generated shader_animation_property_block tag block.
    /// </summary>
    public sealed class ShaderAnimationPropertyBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShaderAnimationPropertyBlock"/> class.
        /// </summary>
        public ShaderAnimationPropertyBlock()
        {
            this.Fields.Add(new EnumField("Type^", "Bitmap Scale Uniform", "Bitmap Scale x", "Bitmap Scale y", "Bitmap Scale z", "Bitmap Translation x", "Bitmap Translation y", "Bitmap Translation z", "Bitmap Rotation Angle", "Bitmap Rotation Axis x", "Bitmap Rotation Axis y", "Bitmap Rotation Axis z", "Value", "Color", "Bitmap Index"));
            this.Fields.Add(new PadField("", 2));
            this.Fields.Add(new StringIdField("Input Name"));
            this.Fields.Add(new StringIdField("Range Name"));
            this.Fields.Add(new RealField("Time Period:sec"));
            this.Fields.Add(new ExplanationField("FUNCTION", null));
            this.Fields.Add(new StructField<MappingFunctionBlock>("Function"));
        }
        /// <summary>
        /// Gets and returns the name of the shader_animation_property_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "shader_animation_property_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the shader_animation_property_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "animation property";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the shader_animation_property_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 14;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the shader_animation_property_block tag block.
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
