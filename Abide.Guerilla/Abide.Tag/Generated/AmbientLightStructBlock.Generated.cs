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
    /// Represents the generated ambient_light_struct_block tag block.
    /// </summary>
    public sealed class AmbientLightStructBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AmbientLightStructBlock"/> class.
        /// </summary>
        public AmbientLightStructBlock()
        {
            this.Fields.Add(new ExplanationField("Ambient light", null));
            this.Fields.Add(new RealRgbColorField("Min lightmap sample"));
            this.Fields.Add(new RealRgbColorField("Max lightmap sample"));
            this.Fields.Add(new ExplanationField("Ambient light function", null));
            this.Fields.Add(new StructField<MappingFunctionBlock>("function"));
        }
        /// <summary>
        /// Gets and returns the name of the ambient_light_struct_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "ambient_light_struct_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the ambient_light_struct_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "ambient_light_struct";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the ambient_light_struct_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 1;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the ambient_light_struct_block tag block.
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
