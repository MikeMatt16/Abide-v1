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
    /// Represents the generated ai_globals_block tag block.
    /// </summary>
    public sealed class AiGlobalsBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AiGlobalsBlock"/> class.
        /// </summary>
        public AiGlobalsBlock()
        {
            this.Fields.Add(new RealField("danger broadly facing"));
            this.Fields.Add(new PadField("", 4));
            this.Fields.Add(new RealField("danger shooting near"));
            this.Fields.Add(new PadField("", 4));
            this.Fields.Add(new RealField("danger shooting at"));
            this.Fields.Add(new PadField("", 4));
            this.Fields.Add(new RealField("danger extremely close"));
            this.Fields.Add(new PadField("", 4));
            this.Fields.Add(new RealField("danger shield damage"));
            this.Fields.Add(new RealField("danger exetended shield damage"));
            this.Fields.Add(new RealField("danger body damage"));
            this.Fields.Add(new RealField("danger extended body damage"));
            this.Fields.Add(new PadField("", 48));
            this.Fields.Add(new TagReferenceField("global dialogue tag", 1633971303));
            this.Fields.Add(new StringIdField("default mission dialogue sound effect"));
            this.Fields.Add(new PadField("", 20));
            this.Fields.Add(new RealField("jump down:wu/tick"));
            this.Fields.Add(new RealField("jump step:wu/tick"));
            this.Fields.Add(new RealField("jump crouch:wu/tick"));
            this.Fields.Add(new RealField("jump stand:wu/tick"));
            this.Fields.Add(new RealField("jump storey:wu/tick"));
            this.Fields.Add(new RealField("jump tower:wu/tick"));
            this.Fields.Add(new RealField("max jump down height down:wu"));
            this.Fields.Add(new RealField("max jump down height step:wu"));
            this.Fields.Add(new RealField("max jump down height crouch:wu"));
            this.Fields.Add(new RealField("max jump down height stand:wu"));
            this.Fields.Add(new RealField("max jump down height storey:wu"));
            this.Fields.Add(new RealField("max jump down height tower:wu"));
            this.Fields.Add(new RealBoundsField("hoist step:wus"));
            this.Fields.Add(new RealBoundsField("hoist crouch:wus"));
            this.Fields.Add(new RealBoundsField("hoist stand:wus"));
            this.Fields.Add(new PadField("", 24));
            this.Fields.Add(new RealBoundsField("vault step:wus"));
            this.Fields.Add(new RealBoundsField("vault crouch:wus"));
            this.Fields.Add(new PadField("", 48));
            this.Fields.Add(new BlockField<AiGlobalsGravemindBlock>("gravemind properties", 1));
            this.Fields.Add(new PadField("", 48));
            this.Fields.Add(new RealField("scary target threhold#A target of this scariness is offically considered scary (b" +
                        "y combat dialogue, etc.)"));
            this.Fields.Add(new RealField("scary weapon threhold#A weapon of this scariness is offically considered scary (b" +
                        "y combat dialogue, etc.)"));
            this.Fields.Add(new RealField("player scariness"));
            this.Fields.Add(new RealField("berserking actor scariness"));
        }
        /// <summary>
        /// Gets and returns the name of the ai_globals_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "ai_globals_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the ai_globals_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "ai_globals_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the ai_globals_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 1;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the ai_globals_block tag block.
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
