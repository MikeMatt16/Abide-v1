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
    /// Represents the generated phantom_types_block tag block.
    /// </summary>
    internal sealed class PhantomTypesBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhantomTypesBlock"/> class.
        /// </summary>
        public PhantomTypesBlock()
        {
            this.Fields.Add(new LongFlagsField("flags", "generates effects", "use acc as force", "negates gravity", "ignores players", "ignores nonplayers", "ignores bipeds", "ignores vehicles", "ignores weapons", "ignores equipment", "ignores garbage", "ignores projectiles", "ignores scenery", "ignores machines", "ignores controls", "ignores light fixtures", "ignores sound scenery", "ignores crates", "ignores creatures", "", "", "", "", "", "", "localizes physics", "disable linear damping", "disable angular damping", "ignores dead bipeds"));
            this.Fields.Add(new CharEnumField("minimum size", "default", "tiny", "small", "medium", "large", "huge", "extra huge"));
            this.Fields.Add(new CharEnumField("maximum size", "default", "tiny", "small", "medium", "large", "huge", "extra huge"));
            this.Fields.Add(new PadField("", 2));
            this.Fields.Add(new StringIdField("marker name#you don\'t need this if you\'re just generating effects.  If empty it d" +
                        "efaults to the up of the object"));
            this.Fields.Add(new StringIdField("alignment marker name#you don\'t need this if you\'re just generating effects.  If " +
                        "empty it defaults to \"marker name\""));
            this.Fields.Add(new ExplanationField("Linear Motion", "0 - means do nothing\nCENTER: motion towards marker position \nAXIS: motion towards" +
                        " marker axis, such that object is on the axis\nDIRECTION: motion along marker dir" +
                        "ection"));
            this.Fields.Add(new PadField("", 8));
            this.Fields.Add(new RealField("hookes law e#0 if you don\'t want this to behave like spring.  1 is a good startin" +
                        "g point if you do."));
            this.Fields.Add(new RealField("linear dead radius#radius from linear motion origin in which acceleration is dead" +
                        "."));
            this.Fields.Add(new RealField("center acc"));
            this.Fields.Add(new RealField("center max vel"));
            this.Fields.Add(new RealField("axis acc"));
            this.Fields.Add(new RealField("axis max vel"));
            this.Fields.Add(new RealField("direction acc"));
            this.Fields.Add(new RealField("direction max vel"));
            this.Fields.Add(new PadField("", 28));
            this.Fields.Add(new ExplanationField("Angular Motion", "0 - means do nothing\nALIGNMENT: algin objects in the phantom with the marker\nSPIN" +
                        ": spin the object about the marker axis"));
            this.Fields.Add(new RealField("alignment hookes law e#0 if you don\'t want this to behave like spring.  1 is a go" +
                        "od starting point if you do."));
            this.Fields.Add(new RealField("alignment acc"));
            this.Fields.Add(new RealField("alignment max vel"));
            this.Fields.Add(new PadField("", 8));
        }
        /// <summary>
        /// Gets and returns the name of the phantom_types_block tag block.
        /// </summary>
        public override string BlockName
        {
            get
            {
                return "phantom_types_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the phantom_types_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "phantom_types_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the phantom_types_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 16;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the phantom_types_block tag block.
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