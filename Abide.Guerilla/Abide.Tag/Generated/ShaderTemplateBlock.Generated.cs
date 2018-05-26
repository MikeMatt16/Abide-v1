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
    /// Represents the generated shader_template_block tag block.
    /// </summary>
    public sealed class ShaderTemplateBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShaderTemplateBlock"/> class.
        /// </summary>
        public ShaderTemplateBlock()
        {
            this.Fields.Add(new DataField("Documentation"));
            this.Fields.Add(new StringIdField("Default Material Name"));
            this.Fields.Add(new ExplanationField("FLAGS", null));
            this.Fields.Add(new PadField("", 2));
            this.Fields.Add(new WordFlagsField("Flags", "Force Active Camo", "Water", "Foliage", "Hide Standard Parameters"));
            this.Fields.Add(new BlockField<ShaderTemplatePropertyBlock>("Properties", 14));
            this.Fields.Add(new BlockField<ShaderTemplateCategoryBlock>("Categories", 16));
            this.Fields.Add(new ExplanationField("LIGHT RESPONSE", null));
            this.Fields.Add(new TagReferenceField("Light Response"));
            this.Fields.Add(new BlockField<ShaderTemplateLevelOfDetailBlock>("LODs", 8));
            this.Fields.Add(new BlockField<ShaderTemplateRuntimeExternalLightResponseIndexBlock>("", 65535));
            this.Fields.Add(new BlockField<ShaderTemplateRuntimeExternalLightResponseIndexBlock>("", 65535));
            this.Fields.Add(new ExplanationField("RECURSIVE RENDERING", null));
            this.Fields.Add(new TagReferenceField("Aux 1 Shader"));
            this.Fields.Add(new EnumField("Aux 1 Layer", "texaccum", "Environment Map", "Self-Illumination", "Overlay", "Transparent", "Lightmap (Indirect)", "Diffuse", "Specular", "Shadow Generate", "Shadow Apply", "Boom", "Fog", "Sh Prt", "Active Camo", "Water Edge Blend", "Decal", "Active Camo Stencil Modulate", "Hologram", "Light Albedo"));
            this.Fields.Add(new PadField("", 2));
            this.Fields.Add(new TagReferenceField("Aux 2 Shader"));
            this.Fields.Add(new EnumField("Aux 2 Layer", "texaccum", "Environment Map", "Self-Illumination", "Overlay", "Transparent", "Lightmap (Indirect)", "Diffuse", "Specular", "Shadow Generate", "Shadow Apply", "Boom", "Fog", "Sh Prt", "Active Camo", "Water Edge Blend", "Decal", "Active Camo Stencil Modulate", "Hologram", "Light Albedo"));
            this.Fields.Add(new PadField("", 2));
            this.Fields.Add(new BlockField<ShaderTemplatePostprocessDefinitionNewBlock>("Postprocess Definition*", 1));
        }
        /// <summary>
        /// Gets and returns the name of the shader_template_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "shader_template_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the shader_template_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "shader_template";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the shader_template_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 1;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the shader_template_block tag block.
        /// </summary>
        public override int Alignment
        {
            get
            {
                return 4;
            }
        }
        /// <summary>
        /// Writes the shader_template_block tag block using the specified binary writer.
        /// </summary>
        // <param name="writer">The <see cref="BinaryWriter"/> used to write the shader_template_block tag block.</param>
        public override void Write(BinaryWriter writer)
        {
            // Invoke base write procedure.
            base.Write(writer);
            // Post-write the tag blocks.
            ((BlockField<ShaderTemplatePropertyBlock>)(this.Fields[5])).WriteChildren(writer);
            ((BlockField<ShaderTemplateCategoryBlock>)(this.Fields[6])).WriteChildren(writer);
            ((BlockField<ShaderTemplateLevelOfDetailBlock>)(this.Fields[9])).WriteChildren(writer);
            ((BlockField<ShaderTemplateRuntimeExternalLightResponseIndexBlock>)(this.Fields[10])).WriteChildren(writer);
            ((BlockField<ShaderTemplateRuntimeExternalLightResponseIndexBlock>)(this.Fields[11])).WriteChildren(writer);
            ((BlockField<ShaderTemplatePostprocessDefinitionNewBlock>)(this.Fields[19])).WriteChildren(writer);
        }
    }
}
