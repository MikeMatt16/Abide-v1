//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Abide.Tag.Cache.Generated
{
    using Abide.Tag;
    
    /// <summary>
    /// Represents the generated cellular_automata_block tag block.
    /// </summary>
    public sealed class CellularAutomataBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellularAutomataBlock"/> class.
        /// </summary>
        public CellularAutomataBlock()
        {
            this.Fields.Add(new ExplanationField("parameters", ""));
            this.Fields.Add(new ShortIntegerField("updates per second:Hz"));
            this.Fields.Add(new ShortIntegerField("x (width):cells"));
            this.Fields.Add(new ShortIntegerField("y (depth):cells"));
            this.Fields.Add(new ShortIntegerField("z (height):cells"));
            this.Fields.Add(new RealField("x (width):world units"));
            this.Fields.Add(new RealField("y (depth):world units"));
            this.Fields.Add(new RealField("z (height):world units"));
            this.Fields.Add(new PadField("", 32));
            this.Fields.Add(new OldStringIdField("marker"));
            this.Fields.Add(new ExplanationField("cell birth", ""));
            this.Fields.Add(new RealFractionField("cell birth chance:[0,1]"));
            this.Fields.Add(new PadField("", 32));
            this.Fields.Add(new ExplanationField("gene mutation", ""));
            this.Fields.Add(new LongIntegerField("cell gene mutates 1 in:times"));
            this.Fields.Add(new LongIntegerField("virus gene mutations 1 in:times"));
            this.Fields.Add(new PadField("", 32));
            this.Fields.Add(new ExplanationField("cell infection", ""));
            this.Fields.Add(new ShortBoundsField("infected cell lifespan:updates#the lifespan of a cell once infected"));
            this.Fields.Add(new ShortIntegerField("minimum infection age:updates#no cell can be infected before it has been alive th" +
                        "is number of updates"));
            this.Fields.Add(new PadField("", 2));
            this.Fields.Add(new RealFractionField("cell infection chance:[0,1]"));
            this.Fields.Add(new RealFractionField("infection threshold:[0,1]#0.0 is most difficult for the virus, 1.0 means any viru" +
                        "s can infect any cell"));
            this.Fields.Add(new PadField("", 32));
            this.Fields.Add(new ExplanationField("initial state", ""));
            this.Fields.Add(new RealFractionField("new cell filled chance:[0,1]"));
            this.Fields.Add(new RealFractionField("new cell infected chance:[0,1]"));
            this.Fields.Add(new PadField("", 32));
            this.Fields.Add(new ExplanationField("detail texture", ""));
            this.Fields.Add(new RealFractionField("detail texture change chance:[0,1]"));
            this.Fields.Add(new PadField("", 32));
            this.Fields.Add(new ShortIntegerField("detail texture width:cells#the number of cells repeating across the detail textur" +
                        "e in both dimensions"));
            this.Fields.Add(new PadField("", 2));
            this.Fields.Add(new TagReferenceField("detail texture", 1651078253));
            this.Fields.Add(new ExplanationField("mask texture", ""));
            this.Fields.Add(new PadField("", 32));
            this.Fields.Add(new TagReferenceField("mask bitmap", 1651078253));
            this.Fields.Add(new PadField("", 240));
        }
        /// <summary>
        /// Gets and returns the name of the cellular_automata_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "cellular_automata_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the cellular_automata_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "cellular_automata";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the cellular_automata_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 1;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the cellular_automata_block tag block.
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
