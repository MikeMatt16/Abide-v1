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
    /// Represents the generated liquid_arc_block tag block.
    /// </summary>
    public sealed class LiquidArcBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LiquidArcBlock"/> class.
        /// </summary>
        public LiquidArcBlock()
        {
            this.Fields.Add(new ExplanationField("LIQUID ARC", null));
            this.Fields.Add(new WordFlagsField("flags", "basis marker-relative", "spread by external input", "collide with stuff", "no perspective midpoints"));
            this.Fields.Add(new EnumField("sprite count", "4 sprites", "8 sprites", "16 sprites", "32 sprites", "64 sprites", "128 sprites", "256 sprites"));
            this.Fields.Add(new RealField("natural length:world units"));
            this.Fields.Add(new ShortIntegerField("instances"));
            this.Fields.Add(new PadField("", 2));
            this.Fields.Add(new AngleField("instance spread angle:degrees"));
            this.Fields.Add(new RealField("instance rotation period:seconds"));
            this.Fields.Add(new PadField("", 8));
            this.Fields.Add(new TagReferenceField("material effects"));
            this.Fields.Add(new TagReferenceField("bitmap"));
            this.Fields.Add(new PadField("", 8));
            this.Fields.Add(new ExplanationField("HORIZONTAL RANGE", null));
            this.Fields.Add(new StructField<ScalarFunctionStructBlock>("horizontal range"));
            this.Fields.Add(new ExplanationField("VERTICAL RANGE", null));
            this.Fields.Add(new StructField<ScalarFunctionStructBlock>("vertical range"));
            this.Fields.Add(new RealFractionField("vertical negative scale:[0,1]"));
            this.Fields.Add(new ExplanationField("ROUGHNESS", null));
            this.Fields.Add(new StructField<ScalarFunctionStructBlock>("roughness"));
            this.Fields.Add(new PadField("", 64));
            this.Fields.Add(new ExplanationField("NOISE FREQUENCIES", null));
            this.Fields.Add(new RealField("octave 1 frequency:cycles/second"));
            this.Fields.Add(new RealField("octave 2 frequency:cycles/second"));
            this.Fields.Add(new RealField("octave 3 frequency:cycles/second"));
            this.Fields.Add(new RealField("octave 4 frequency:cycles/second"));
            this.Fields.Add(new RealField("octave 5 frequency:cycles/second"));
            this.Fields.Add(new RealField("octave 6 frequency:cycles/second"));
            this.Fields.Add(new RealField("octave 7 frequency:cycles/second"));
            this.Fields.Add(new RealField("octave 8 frequency:cycles/second"));
            this.Fields.Add(new RealField("octave 9 frequency:cycles/second"));
            this.Fields.Add(new PadField("", 28));
            this.Fields.Add(new WordFlagsField("octave flags", "octave 1", "octave 2", "octave 3", "octave 4", "octave 5", "octave 6", "octave 7", "octave 8", "octave 9"));
            this.Fields.Add(new PadField("", 2));
            this.Fields.Add(new BlockField<LiquidCoreBlock>("cores", 4));
            this.Fields.Add(new ExplanationField("RANGE-COLLISION SCALE", null));
            this.Fields.Add(new StructField<ScalarFunctionStructBlock>("range-scale"));
            this.Fields.Add(new ExplanationField("BRIGHTNESS-COLLISION SCALE", null));
            this.Fields.Add(new StructField<ScalarFunctionStructBlock>("brightness-scale"));
        }
        /// <summary>
        /// Gets and returns the name of the liquid_arc_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "liquid_arc_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the liquid_arc_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "arc";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the liquid_arc_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 3;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the liquid_arc_block tag block.
        /// </summary>
        public override int Alignment
        {
            get
            {
                return 4;
            }
        }
        /// <summary>
        /// Writes the liquid_arc_block tag block using the specified binary writer.
        /// </summary>
        // <param name="writer">The <see cref="BinaryWriter"/> used to write the liquid_arc_block tag block.</param>
        public override void Write(BinaryWriter writer)
        {
            // Invoke base write procedure.
            base.Write(writer);
            // Post-write the tag blocks.
            ((BlockField<LiquidCoreBlock>)(this.Fields[33])).WriteChildren(writer);
        }
    }
}
