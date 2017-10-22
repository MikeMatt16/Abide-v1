using Abide.HaloLibrary;
using System;

namespace Abide.Builder.Guerilla.Definitions
{
    /// <summary>
    /// Represents a guerilla tag group definition.
    /// This attribute is used to define the behabior of a tag group.
    /// </summary>
    public sealed class TagGroupDefinitionAttribute : Attribute
    {
        /// <summary>
        /// Gets and returns the name of the tag group.
        /// </summary>
        public string Name
        {
            get { return name; }
        }
        /// <summary>
        /// Gets and returns the tag of the tag group.
        /// </summary>
        public Tag GroupTag
        {
            get { return groupTag; }
        }
        /// <summary>
        /// Gets and returns the size of the tag group.
        /// </summary>
        public uint Size
        {
            get { return size; }
        }
        /// <summary>
        /// Gets and returns the alignment of the tag group.
        /// </summary>
        public uint Alignment
        {
            get { return alignment; }
        }

        private readonly string name;
        private readonly Tag groupTag;
        private readonly uint size;
        private readonly uint alignment;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagGroupDefinitionAttribute"/> class using the supplied name, group tag, size, and alignment.
        /// </summary>
        /// <param name="name">The name of the tag group.</param>
        /// <param name="groupTag">The group tag.</param>
        /// <param name="size">The size of the tag group.</param>
        public TagGroupDefinitionAttribute(string name, string groupTag, uint size, uint alignment)
        {
            this.name = name;
            this.groupTag = groupTag;
            this.size = size;
            this.alignment = alignment;
        }
    }

    /// <summary>
    /// Represents a guerilla tag block definition.
    /// This attribute is used to define the behavior of a tag block.
    /// </summary>
    public sealed class TagBlockDefinitionAttribute : Attribute
    {
        /// <summary>
        /// Gets and returns the name of the tag block.
        /// </summary>
        public string Name
        {
            get { return name; }
        }
        /// <summary>
        /// Gets and returns the length of the tag block.
        /// </summary>
        public uint Length
        {
            get { return length; }
        }
        /// <summary>
        /// Gets and returns the tag block's max count.
        /// </summary>
        public uint MaxCount
        {
            get { return maxCount; }
        }
        /// <summary>
        /// Gets and returns the tag block's alignment.
        /// </summary>
        public uint Alignment
        {
            get { return alignment; }
        }

        private readonly string name;
        private readonly uint length;
        private readonly uint maxCount;
        private readonly uint alignment;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagBlockDefinitionAttribute"/> class using the supplied name, length, max count, and alignment.
        /// </summary>
        /// <param name="name">The name of the tag block.</param>
        /// <param name="length">The length of the tag block</param>
        /// <param name="maxCount">The tag block's max count.</param>
        /// <param name="alignment">The tag block's alignment.</param>
        public TagBlockDefinitionAttribute(string name, uint length, uint maxCount, uint alignment)
        {
            this.name = name;
            this.length = length;
            this.maxCount = maxCount;
            this.alignment = alignment;
        }
    }

    /// <summary>
    /// Represents a tag ID reference.
    /// </summary>
    public sealed class TagIdReferenceAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagIdReferenceAttribute"/> class.
        /// </summary>
        public TagIdReferenceAttribute() { }
    }

    /// <summary>
    /// Represents a string ID reference.
    /// </summary>
    public sealed class StringIdReferenceAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringIdReferenceAttribute"/> class.
        /// </summary>
        public StringIdReferenceAttribute() { }
    }
}
