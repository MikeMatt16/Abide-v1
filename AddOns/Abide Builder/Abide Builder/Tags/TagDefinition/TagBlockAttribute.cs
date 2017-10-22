using System;

namespace Abide.Builder.Tags.TagDefinition
{
    /// <summary>
    /// Represents a tag block.
    /// </summary>
    public sealed class TagBlockAttribute : DefinitionAttribute
    {
        /// <summary>
        /// Gets and returns the size or length (in bytes) of the tag block.
        /// </summary>
        public uint Size
        {
            get { return size; }
        }
        /// <summary>
        /// Gets and returns the maximum block count or the tag block.
        /// </summary>
        public uint MaxBlockCount
        {
            get { return maxBlockCount; }
        }
        /// <summary>
        /// Gets and returns the byte alignment of the tag block.
        /// </summary>
        public uint Alignment
        {
            get { return alignment; }
        }
        
        private readonly uint size;
        private readonly uint maxBlockCount;
        private readonly uint alignment;

        /// <summary>
        /// Initializes an instance of the <see cref="TagBlockAttribute"/> class using the supplied name, offset, size, maximum block count, and alignment values.
        /// </summary>
        /// <param name="name">The name of the tag block.</param>
        /// <param name="offset">The offset of the tag block.</param>
        /// <param name="size">The size of the tag block.</param>
        /// <param name="maxBlockCount">The maximum number of blocks in the tag block.</param>
        /// <param name="alignment">The byte-alignment of the tag block.</param>
        public TagBlockAttribute(string name, uint offset, uint size, uint maxBlockCount, uint alignment) : base(name, offset)
        {
            this.size = size;
            this.maxBlockCount = maxBlockCount;
            this.alignment = alignment;
        }
    }
}
