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
    /// Represents the generated shader_template_postprocess_remapping_new_block tag block.
    /// </summary>
    public sealed class ShaderTemplatePostprocessRemappingNewBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShaderTemplatePostprocessRemappingNewBlock"/> class.
        /// </summary>
        public ShaderTemplatePostprocessRemappingNewBlock()
        {
            this.Fields.Add(new SkipField("", 3));
            this.Fields.Add(new CharIntegerField("source index"));
        }
        /// <summary>
        /// Gets and returns the name of the shader_template_postprocess_remapping_new_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "shader_template_postprocess_remapping_new_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the shader_template_postprocess_remapping_new_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "shader_template_postprocess_remapping_new_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the shader_template_postprocess_remapping_new_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 1024;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the shader_template_postprocess_remapping_new_block tag block.
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
