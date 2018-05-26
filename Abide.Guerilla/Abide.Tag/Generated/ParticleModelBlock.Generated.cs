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
    /// Represents the generated particle_model_block tag block.
    /// </summary>
    public sealed class ParticleModelBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParticleModelBlock"/> class.
        /// </summary>
        public ParticleModelBlock()
        {
            this.Fields.Add(new LongFlagsField("flags", "spins", "random u mirror", "random v mirror", "frame animation one shot", "select random sequence", "disable frame blending", "can animate backwards", "receive lightmap lighting", "tint from diffuse texture", "dies at rest", "dies on structure collision", "dies in media", "dies in air", "bitmap authored vertically", "has sweetener"));
            this.Fields.Add(new LongEnumField("orientation", "screen facing", "parallel to direction", "perpendicular to direction", "vertical", "horizontal"));
            this.Fields.Add(new PadField("", 16));
            this.Fields.Add(new TagReferenceField("shader"));
            this.Fields.Add(new ExplanationField("SCALE X", null));
            this.Fields.Add(new StructField<ParticlePropertyScalarStructNewBlock>("scale x"));
            this.Fields.Add(new ExplanationField("SCALE Y", null));
            this.Fields.Add(new StructField<ParticlePropertyScalarStructNewBlock>("scale y"));
            this.Fields.Add(new ExplanationField("SCALE Z", null));
            this.Fields.Add(new StructField<ParticlePropertyScalarStructNewBlock>("scale z"));
            this.Fields.Add(new ExplanationField("ROTATION", null));
            this.Fields.Add(new StructField<ParticlePropertyScalarStructNewBlock>("rotation"));
            this.Fields.Add(new ExplanationField("Spawned Effects", null));
            this.Fields.Add(new TagReferenceField("collision effect#effect, material effect or sound spawned when this particle coll" +
                        "ides with something"));
            this.Fields.Add(new TagReferenceField("death effect#effect, material effect or sound spawned when this particle dies"));
            this.Fields.Add(new ExplanationField("Attached Particle Systems", null));
            this.Fields.Add(new BlockField<EffectLocationsBlock>("locations", 32));
            this.Fields.Add(new BlockField<ParticleSystemDefinitionBlockNew>("attached particle systems", 32));
            this.Fields.Add(new BlockField<ParticleModelsBlock>("models*", 256));
            this.Fields.Add(new BlockField<ParticleModelVerticesBlock>("raw vertices*", 32768));
            this.Fields.Add(new BlockField<ParticleModelIndicesBlock>("indices*", 32768));
            this.Fields.Add(new BlockField<CachedDataBlock>("cached data", 1));
            this.Fields.Add(new StructField<GlobalGeometryBlockInfoStructBlock>("geometry section info"));
            this.Fields.Add(new PadField("", 16));
            this.Fields.Add(new PadField("", 8));
        }
        /// <summary>
        /// Gets and returns the name of the particle_model_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "particle_model_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the particle_model_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "particle_model";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the particle_model_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 1;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the particle_model_block tag block.
        /// </summary>
        public override int Alignment
        {
            get
            {
                return 4;
            }
        }
        /// <summary>
        /// Writes the particle_model_block tag block using the specified binary writer.
        /// </summary>
        // <param name="writer">The <see cref="BinaryWriter"/> used to write the particle_model_block tag block.</param>
        public override void Write(BinaryWriter writer)
        {
            // Invoke base write procedure.
            base.Write(writer);
            // Post-write the tag blocks.
            ((BlockField<EffectLocationsBlock>)(this.Fields[16])).WriteChildren(writer);
            ((BlockField<ParticleSystemDefinitionBlockNew>)(this.Fields[17])).WriteChildren(writer);
            ((BlockField<ParticleModelsBlock>)(this.Fields[18])).WriteChildren(writer);
            ((BlockField<ParticleModelVerticesBlock>)(this.Fields[19])).WriteChildren(writer);
            ((BlockField<ParticleModelIndicesBlock>)(this.Fields[20])).WriteChildren(writer);
            ((BlockField<CachedDataBlock>)(this.Fields[21])).WriteChildren(writer);
        }
    }
}
