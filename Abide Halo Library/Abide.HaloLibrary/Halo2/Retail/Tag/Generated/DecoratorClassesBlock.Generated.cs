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
    /// Represents the generated decorator_classes_block tag block.
    /// </summary>
    internal sealed class DecoratorClassesBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DecoratorClassesBlock"/> class.
        /// </summary>
        public DecoratorClassesBlock()
        {
            this.Fields.Add(new StringIdField("name"));
            this.Fields.Add(new CharEnumField("type", "model", "floating decal", "projected decal", "screen facing quad", "axis rotating quad", "cross quad"));
            this.Fields.Add(new PadField("", 3));
            this.Fields.Add(new RealField("scale"));
            this.Fields.Add(new BlockField<DecoratorPermutationsBlock>("permutations", 64));
        }
        /// <summary>
        /// Gets and returns the name of the decorator_classes_block tag block.
        /// </summary>
        public override string BlockName
        {
            get
            {
                return "decorator_classes_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the decorator_classes_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "decorator_classes_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the decorator_classes_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 8;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the decorator_classes_block tag block.
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