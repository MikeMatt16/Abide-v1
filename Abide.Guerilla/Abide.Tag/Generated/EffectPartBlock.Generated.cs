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
    /// Represents the generated effect_part_block tag block.
    /// </summary>
    public sealed class EffectPartBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EffectPartBlock"/> class.
        /// </summary>
        public EffectPartBlock()
        {
            this.Fields.Add(new EnumField("create in", "any environment", "air only", "water only", "space only"));
            this.Fields.Add(new EnumField("create in", "either mode", "violent mode only", "nonviolent mode only"));
            this.Fields.Add(new ShortBlockIndexField("location"));
            this.Fields.Add(new WordFlagsField("flags", "face down regardless of location (decals)", "offset origin away from geometry (lights)", "never attached to object", "disabled for debugging", "draw regardless of distance"));
            this.Fields.Add(new PadField("", 4));
            this.Fields.Add(new TagReferenceField("type^"));
            this.Fields.Add(new RealBoundsField("velocity bounds:world units per second#initial velocity along the location\'s forw" +
                        "ard, for decals the distance at which decal is created (defaults to 0.5)"));
            this.Fields.Add(new AngleField("velocity cone angle:degrees#initial velocity will be inside the cone defined by t" +
                        "his angle."));
            this.Fields.Add(new AngleBoundsField("angular velocity bounds:degrees per second"));
            this.Fields.Add(new RealBoundsField("radius modifier bounds"));
            this.Fields.Add(new ExplanationField("SCALE MODIFIERS", null));
            this.Fields.Add(new LongFlagsField("A scales values:", "velocity", "velocity delta", "velocity cone angle", "angular velocity", "angular velocity delta", "type-specific scale"));
            this.Fields.Add(new LongFlagsField("B scales values:", "velocity", "velocity delta", "velocity cone angle", "angular velocity", "angular velocity delta", "type-specific scale"));
        }
        /// <summary>
        /// Gets and returns the name of the effect_part_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "effect_part_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the effect_part_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "effect_part_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the effect_part_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 32;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the effect_part_block tag block.
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
