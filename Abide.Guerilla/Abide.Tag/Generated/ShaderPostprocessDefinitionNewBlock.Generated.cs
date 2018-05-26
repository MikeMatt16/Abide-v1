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
    /// Represents the generated shader_postprocess_definition_new_block tag block.
    /// </summary>
    public sealed class ShaderPostprocessDefinitionNewBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShaderPostprocessDefinitionNewBlock"/> class.
        /// </summary>
        public ShaderPostprocessDefinitionNewBlock()
        {
            this.Fields.Add(new LongIntegerField("shader template index"));
            this.Fields.Add(new BlockField<ShaderPostprocessBitmapNewBlock>("bitmaps", 1024));
            this.Fields.Add(new BlockField<Pixel32Block>("pixel constants", 1024));
            this.Fields.Add(new BlockField<RealVector4dBlock>("vertex constants", 1024));
            this.Fields.Add(new BlockField<ShaderPostprocessLevelOfDetailNewBlock>("levels of detail", 1024));
            this.Fields.Add(new BlockField<TagBlockIndexBlock>("layers", 1024));
            this.Fields.Add(new BlockField<TagBlockIndexBlock>("passes", 1024));
            this.Fields.Add(new BlockField<ShaderPostprocessImplementationNewBlock>("implementations", 1024));
            this.Fields.Add(new BlockField<ShaderPostprocessOverlayNewBlock>("overlays", 1024));
            this.Fields.Add(new BlockField<ShaderPostprocessOverlayReferenceNewBlock>("overlay references", 1024));
            this.Fields.Add(new BlockField<ShaderPostprocessAnimatedParameterNewBlock>("animated parameters", 1024));
            this.Fields.Add(new BlockField<ShaderPostprocessAnimatedParameterReferenceNewBlock>("animated parameter references", 1024));
            this.Fields.Add(new BlockField<ShaderPostprocessBitmapPropertyBlock>("bitmap properties", 5));
            this.Fields.Add(new BlockField<ShaderPostprocessColorPropertyBlock>("color properties", 2));
            this.Fields.Add(new BlockField<ShaderPostprocessValuePropertyBlock>("value properties", 6));
            this.Fields.Add(new BlockField<ShaderPostprocessLevelOfDetailBlock>("old levels of detail", 1024));
        }
        /// <summary>
        /// Gets and returns the name of the shader_postprocess_definition_new_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "shader_postprocess_definition_new_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the shader_postprocess_definition_new_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "shader_postprocess_definition_new_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the shader_postprocess_definition_new_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 1;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the shader_postprocess_definition_new_block tag block.
        /// </summary>
        public override int Alignment
        {
            get
            {
                return 4;
            }
        }
        /// <summary>
        /// Writes the shader_postprocess_definition_new_block tag block using the specified binary writer.
        /// </summary>
        // <param name="writer">The <see cref="BinaryWriter"/> used to write the shader_postprocess_definition_new_block tag block.</param>
        public override void Write(BinaryWriter writer)
        {
            // Invoke base write procedure.
            base.Write(writer);
            // Post-write the tag blocks.
            ((BlockField<ShaderPostprocessBitmapNewBlock>)(this.Fields[1])).WriteChildren(writer);
            ((BlockField<Pixel32Block>)(this.Fields[2])).WriteChildren(writer);
            ((BlockField<RealVector4dBlock>)(this.Fields[3])).WriteChildren(writer);
            ((BlockField<ShaderPostprocessLevelOfDetailNewBlock>)(this.Fields[4])).WriteChildren(writer);
            ((BlockField<TagBlockIndexBlock>)(this.Fields[5])).WriteChildren(writer);
            ((BlockField<TagBlockIndexBlock>)(this.Fields[6])).WriteChildren(writer);
            ((BlockField<ShaderPostprocessImplementationNewBlock>)(this.Fields[7])).WriteChildren(writer);
            ((BlockField<ShaderPostprocessOverlayNewBlock>)(this.Fields[8])).WriteChildren(writer);
            ((BlockField<ShaderPostprocessOverlayReferenceNewBlock>)(this.Fields[9])).WriteChildren(writer);
            ((BlockField<ShaderPostprocessAnimatedParameterNewBlock>)(this.Fields[10])).WriteChildren(writer);
            ((BlockField<ShaderPostprocessAnimatedParameterReferenceNewBlock>)(this.Fields[11])).WriteChildren(writer);
            ((BlockField<ShaderPostprocessBitmapPropertyBlock>)(this.Fields[12])).WriteChildren(writer);
            ((BlockField<ShaderPostprocessColorPropertyBlock>)(this.Fields[13])).WriteChildren(writer);
            ((BlockField<ShaderPostprocessValuePropertyBlock>)(this.Fields[14])).WriteChildren(writer);
            ((BlockField<ShaderPostprocessLevelOfDetailBlock>)(this.Fields[15])).WriteChildren(writer);
        }
    }
}
