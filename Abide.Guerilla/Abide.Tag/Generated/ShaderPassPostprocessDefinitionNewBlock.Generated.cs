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
    /// Represents the generated shader_pass_postprocess_definition_new_block tag block.
    /// </summary>
    public sealed class ShaderPassPostprocessDefinitionNewBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShaderPassPostprocessDefinitionNewBlock"/> class.
        /// </summary>
        public ShaderPassPostprocessDefinitionNewBlock()
        {
            this.Fields.Add(new BlockField<ShaderPassPostprocessImplementationNewBlock>("implementations", 1024));
            this.Fields.Add(new BlockField<ShaderPassPostprocessTextureNewBlock>("textures", 1024));
            this.Fields.Add(new BlockField<RenderStateBlock>("render states", 1024));
            this.Fields.Add(new BlockField<ShaderPassPostprocessTextureStateBlock>("texture states", 1024));
            this.Fields.Add(new BlockField<PixelShaderFragmentBlock>("ps fragments", 1024));
            this.Fields.Add(new BlockField<PixelShaderPermutationNewBlock>("ps permutations", 1024));
            this.Fields.Add(new BlockField<PixelShaderCombinerBlock>("ps combiners", 1024));
            this.Fields.Add(new BlockField<ShaderPassPostprocessExternNewBlock>("externs", 1024));
            this.Fields.Add(new BlockField<ShaderPassPostprocessConstantNewBlock>("constants", 1024));
            this.Fields.Add(new BlockField<ShaderPassPostprocessConstantInfoNewBlock>("constant info", 1024));
            this.Fields.Add(new BlockField<ShaderPassPostprocessImplementationBlock>("old implementations", 1024));
        }
        /// <summary>
        /// Gets and returns the name of the shader_pass_postprocess_definition_new_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "shader_pass_postprocess_definition_new_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the shader_pass_postprocess_definition_new_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "shader_pass_postprocess_definition_new_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the shader_pass_postprocess_definition_new_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 1;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the shader_pass_postprocess_definition_new_block tag block.
        /// </summary>
        public override int Alignment
        {
            get
            {
                return 4;
            }
        }
        /// <summary>
        /// Writes the shader_pass_postprocess_definition_new_block tag block using the specified binary writer.
        /// </summary>
        // <param name="writer">The <see cref="BinaryWriter"/> used to write the shader_pass_postprocess_definition_new_block tag block.</param>
        public override void Write(BinaryWriter writer)
        {
            // Invoke base write procedure.
            base.Write(writer);
            // Post-write the tag blocks.
            ((BlockField<ShaderPassPostprocessImplementationNewBlock>)(this.Fields[0])).WriteChildren(writer);
            ((BlockField<ShaderPassPostprocessTextureNewBlock>)(this.Fields[1])).WriteChildren(writer);
            ((BlockField<RenderStateBlock>)(this.Fields[2])).WriteChildren(writer);
            ((BlockField<ShaderPassPostprocessTextureStateBlock>)(this.Fields[3])).WriteChildren(writer);
            ((BlockField<PixelShaderFragmentBlock>)(this.Fields[4])).WriteChildren(writer);
            ((BlockField<PixelShaderPermutationNewBlock>)(this.Fields[5])).WriteChildren(writer);
            ((BlockField<PixelShaderCombinerBlock>)(this.Fields[6])).WriteChildren(writer);
            ((BlockField<ShaderPassPostprocessExternNewBlock>)(this.Fields[7])).WriteChildren(writer);
            ((BlockField<ShaderPassPostprocessConstantNewBlock>)(this.Fields[8])).WriteChildren(writer);
            ((BlockField<ShaderPassPostprocessConstantInfoNewBlock>)(this.Fields[9])).WriteChildren(writer);
            ((BlockField<ShaderPassPostprocessImplementationBlock>)(this.Fields[10])).WriteChildren(writer);
        }
    }
}
