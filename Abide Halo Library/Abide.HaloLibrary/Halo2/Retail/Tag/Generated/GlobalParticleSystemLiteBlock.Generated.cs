//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Abide.HaloLibrary.Halo2.Retail.Tag.Generated
{
    using System;
    using Abide.HaloLibrary;
    using Abide.HaloLibrary.Halo2.Retail.Tag;
    
    /// <summary>
    /// Represents the generated global_particle_system_lite_block tag block.
    /// </summary>
    internal sealed class GlobalParticleSystemLiteBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalParticleSystemLiteBlock"/> class.
        /// </summary>
        public GlobalParticleSystemLiteBlock()
        {
            this.Fields.Add(new TagReferenceField("sprites", 1651078253));
            this.Fields.Add(new RealField("view box width"));
            this.Fields.Add(new RealField("view box height"));
            this.Fields.Add(new RealField("view box depth"));
            this.Fields.Add(new RealField("exclusion radius"));
            this.Fields.Add(new RealField("max velocity"));
            this.Fields.Add(new RealField("min mass"));
            this.Fields.Add(new RealField("max mass"));
            this.Fields.Add(new RealField("min size"));
            this.Fields.Add(new RealField("max size"));
            this.Fields.Add(new LongIntegerField("maximum number of particles"));
            this.Fields.Add(new RealVector3dField("initial velocity"));
            this.Fields.Add(new RealField("bitmap animation speed"));
            this.Fields.Add(new StructField<GlobalGeometryBlockInfoStructBlock>("geometry block info*"));
            this.Fields.Add(new BlockField<ParticleSystemLiteDataBlock>("particle system data", 1));
            this.Fields.Add(new EnumField("type", "generic", "snow", "rain", "rain splash", "bugs", "sand storm", "debris", "bubbles"));
            this.Fields.Add(new PadField("", 2));
            this.Fields.Add(new RealField("mininum opacity"));
            this.Fields.Add(new RealField("maxinum opacity"));
            this.Fields.Add(new RealField("rain streak scale"));
            this.Fields.Add(new RealField("rain line width"));
            this.Fields.Add(new PadField("", 4));
            this.Fields.Add(new PadField("", 4));
            this.Fields.Add(new PadField("", 4));
        }
        /// <summary>
        /// Gets and returns the name of the global_particle_system_lite_block tag block.
        /// </summary>
        public override string BlockName
        {
            get
            {
                return "global_particle_system_lite_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the global_particle_system_lite_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "global_particle_system_lite_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the global_particle_system_lite_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 1;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the global_particle_system_lite_block tag block.
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