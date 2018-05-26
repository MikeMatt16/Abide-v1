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
    /// Represents the generated shader_postprocess_level_of_detail_block tag block.
    /// </summary>
    public sealed class ShaderPostprocessLevelOfDetailBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShaderPostprocessLevelOfDetailBlock"/> class.
        /// </summary>
        public ShaderPostprocessLevelOfDetailBlock()
        {
            this.Fields.Add(new RealField("projected height percentage"));
            this.Fields.Add(new LongIntegerField("available layers"));
            this.Fields.Add(new BlockField<ShaderPostprocessLayerBlock>("layers", 25));
            this.Fields.Add(new BlockField<ShaderPostprocessPassBlock>("passes", 1024));
            this.Fields.Add(new BlockField<ShaderPostprocessImplementationBlock>("implementations", 1024));
            this.Fields.Add(new BlockField<ShaderPostprocessBitmapBlock>("bitmaps", 1024));
            this.Fields.Add(new BlockField<ShaderPostprocessBitmapTransformBlock>("bitmap transforms", 1024));
            this.Fields.Add(new BlockField<ShaderPostprocessValueBlock>("values", 1024));
            this.Fields.Add(new BlockField<ShaderPostprocessColorBlock>("colors", 1024));
            this.Fields.Add(new BlockField<ShaderPostprocessBitmapTransformOverlayBlock>("bitmap transform overlays", 1024));
            this.Fields.Add(new BlockField<ShaderPostprocessValueOverlayBlock>("value overlays", 1024));
            this.Fields.Add(new BlockField<ShaderPostprocessColorOverlayBlock>("color overlays", 1024));
            this.Fields.Add(new BlockField<ShaderPostprocessVertexShaderConstantBlock>("vertex shader constants", 1024));
            this.Fields.Add(new StructField<ShaderGpuStateStructBlock>("GPU State"));
        }
        /// <summary>
        /// Gets and returns the name of the shader_postprocess_level_of_detail_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "shader_postprocess_level_of_detail_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the shader_postprocess_level_of_detail_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "shader_postprocess_level_of_detail_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the shader_postprocess_level_of_detail_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 1024;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the shader_postprocess_level_of_detail_block tag block.
        /// </summary>
        public override int Alignment
        {
            get
            {
                return 4;
            }
        }
        /// <summary>
        /// Writes the shader_postprocess_level_of_detail_block tag block using the specified binary writer.
        /// </summary>
        // <param name="writer">The <see cref="BinaryWriter"/> used to write the shader_postprocess_level_of_detail_block tag block.</param>
        public override void Write(BinaryWriter writer)
        {
            // Invoke base write procedure.
            base.Write(writer);
            // Post-write the tag blocks.
            ((BlockField<ShaderPostprocessLayerBlock>)(this.Fields[2])).WriteChildren(writer);
            ((BlockField<ShaderPostprocessPassBlock>)(this.Fields[3])).WriteChildren(writer);
            ((BlockField<ShaderPostprocessImplementationBlock>)(this.Fields[4])).WriteChildren(writer);
            ((BlockField<ShaderPostprocessBitmapBlock>)(this.Fields[5])).WriteChildren(writer);
            ((BlockField<ShaderPostprocessBitmapTransformBlock>)(this.Fields[6])).WriteChildren(writer);
            ((BlockField<ShaderPostprocessValueBlock>)(this.Fields[7])).WriteChildren(writer);
            ((BlockField<ShaderPostprocessColorBlock>)(this.Fields[8])).WriteChildren(writer);
            ((BlockField<ShaderPostprocessBitmapTransformOverlayBlock>)(this.Fields[9])).WriteChildren(writer);
            ((BlockField<ShaderPostprocessValueOverlayBlock>)(this.Fields[10])).WriteChildren(writer);
            ((BlockField<ShaderPostprocessColorOverlayBlock>)(this.Fields[11])).WriteChildren(writer);
            ((BlockField<ShaderPostprocessVertexShaderConstantBlock>)(this.Fields[12])).WriteChildren(writer);
        }
    }
}
