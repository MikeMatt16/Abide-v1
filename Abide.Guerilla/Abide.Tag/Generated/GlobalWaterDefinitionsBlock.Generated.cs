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
    /// Represents the generated global_water_definitions_block tag block.
    /// </summary>
    public sealed class GlobalWaterDefinitionsBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalWaterDefinitionsBlock"/> class.
        /// </summary>
        public GlobalWaterDefinitionsBlock()
        {
            this.Fields.Add(new TagReferenceField("Shader"));
            this.Fields.Add(new BlockField<WaterGeometrySectionBlock>("Section", 1));
            this.Fields.Add(new StructField<GlobalGeometryBlockInfoStructBlock>("Geometry Block Info*"));
            this.Fields.Add(new RealRgbColorField("Sun Spot Color"));
            this.Fields.Add(new RealRgbColorField("Reflection Tint"));
            this.Fields.Add(new RealRgbColorField("Refraction Tint"));
            this.Fields.Add(new RealRgbColorField("Horizon Color"));
            this.Fields.Add(new RealField("Sun Specular Power"));
            this.Fields.Add(new RealField("Reflection Bump Scale"));
            this.Fields.Add(new RealField("Refraction Bump Scale"));
            this.Fields.Add(new RealField("Fresnel Scale"));
            this.Fields.Add(new RealField("Sun Dir Heading"));
            this.Fields.Add(new RealField("Sun Dir Pitch"));
            this.Fields.Add(new RealField("FOV"));
            this.Fields.Add(new RealField("Aspect"));
            this.Fields.Add(new RealField("Height"));
            this.Fields.Add(new RealField("Farz"));
            this.Fields.Add(new RealField("rotate_offset"));
            this.Fields.Add(new RealVector2dField("Center"));
            this.Fields.Add(new RealVector2dField("Extents"));
            this.Fields.Add(new RealField("Fog Near"));
            this.Fields.Add(new RealField("Fog Far"));
            this.Fields.Add(new RealField("dynamic_height_bias"));
        }
        /// <summary>
        /// Gets and returns the name of the global_water_definitions_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "global_water_definitions_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the global_water_definitions_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "global_water_definitions_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the global_water_definitions_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 1;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the global_water_definitions_block tag block.
        /// </summary>
        public override int Alignment
        {
            get
            {
                return 4;
            }
        }
        /// <summary>
        /// Writes the global_water_definitions_block tag block using the specified binary writer.
        /// </summary>
        // <param name="writer">The <see cref="BinaryWriter"/> used to write the global_water_definitions_block tag block.</param>
        public override void Write(BinaryWriter writer)
        {
            // Invoke base write procedure.
            base.Write(writer);
            // Post-write the tag blocks.
            ((BlockField<WaterGeometrySectionBlock>)(this.Fields[1])).WriteChildren(writer);
        }
    }
}
