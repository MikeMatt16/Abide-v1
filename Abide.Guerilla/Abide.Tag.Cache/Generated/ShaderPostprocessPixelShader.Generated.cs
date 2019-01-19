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
    
    /// <summary>
    /// Represents the generated shader_postprocess_pixel_shader tag block.
    /// </summary>
    public sealed class ShaderPostprocessPixelShader : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShaderPostprocessPixelShader"/> class.
        /// </summary>
        public ShaderPostprocessPixelShader()
        {
            this.Fields.Add(new LongIntegerField("pixel shader handle (runtime)"));
            this.Fields.Add(new LongIntegerField("pixel shader handle (runtime)"));
            this.Fields.Add(new LongIntegerField("pixel shader handle (runtime)"));
            this.Fields.Add(new BlockField<ShaderPostprocessPixelShaderConstantDefaults>("constant register defaults", 32));
            this.Fields.Add(new DataField("compiled shader", 1));
            this.Fields.Add(new DataField("compiled shader", 1));
            this.Fields.Add(new DataField("compiled shader", 1));
        }
        /// <summary>
        /// Gets and returns the name of the shader_postprocess_pixel_shader tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "shader_postprocess_pixel_shader";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the shader_postprocess_pixel_shader tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "shader_postprocess_pixel_shader";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the shader_postprocess_pixel_shader tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 100;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the shader_postprocess_pixel_shader tag block.
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