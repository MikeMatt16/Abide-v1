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
    /// Represents the generated decorator_set_block tag block.
    /// </summary>
    public sealed class DecoratorSetBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DecoratorSetBlock"/> class.
        /// </summary>
        public DecoratorSetBlock()
        {
            this.Fields.Add(new BlockField<DecoratorShaderReferenceBlock>("shaders", 8));
            this.Fields.Add(new RealField("lighting min scale#0.0 defaults to 0.4"));
            this.Fields.Add(new RealField("lighting max scale#0.0 defaults to 2.0"));
            this.Fields.Add(new BlockField<DecoratorClassesBlock>("classes", 8));
            this.Fields.Add(new BlockField<DecoratorModelsBlock>("models*", 256));
            this.Fields.Add(new BlockField<DecoratorModelVerticesBlock>("raw vertices*", 32768));
            this.Fields.Add(new BlockField<DecoratorModelIndicesBlock>("indices*", 32768));
            this.Fields.Add(new BlockField<CachedDataBlock>("cached data", 1));
            this.Fields.Add(new StructField<GlobalGeometryBlockInfoStructBlock>("geometry section info"));
            this.Fields.Add(new PadField("", 16));
        }
        /// <summary>
        /// Gets and returns the name of the decorator_set_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "decorator_set_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the decorator_set_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "decorator_set";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the decorator_set_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 1;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the decorator_set_block tag block.
        /// </summary>
        public override int Alignment
        {
            get
            {
                return 4;
            }
        }
        /// <summary>
        /// Writes the decorator_set_block tag block using the specified binary writer.
        /// </summary>
        // <param name="writer">The <see cref="BinaryWriter"/> used to write the decorator_set_block tag block.</param>
        public override void Write(BinaryWriter writer)
        {
            // Invoke base write procedure.
            base.Write(writer);
            // Post-write the tag blocks.
            ((BlockField<DecoratorShaderReferenceBlock>)(this.Fields[0])).WriteChildren(writer);
            ((BlockField<DecoratorClassesBlock>)(this.Fields[3])).WriteChildren(writer);
            ((BlockField<DecoratorModelsBlock>)(this.Fields[4])).WriteChildren(writer);
            ((BlockField<DecoratorModelVerticesBlock>)(this.Fields[5])).WriteChildren(writer);
            ((BlockField<DecoratorModelIndicesBlock>)(this.Fields[6])).WriteChildren(writer);
            ((BlockField<CachedDataBlock>)(this.Fields[7])).WriteChildren(writer);
        }
    }
}
