using Abide.HaloLibrary;
using System;

namespace Abide.Builder.Tags.TagDefinition
{
    /// <summary>
    /// Represents a tag definition
    /// </summary>
    public sealed class TagDefinitionAttribute : Attribute
    {
        /// <summary>
        /// Gets and returns the tag.
        /// </summary>
        public Tag Tag
        {
            get { return tag; }
        }
        /// <summary>
        /// Gets and returns the definition size.
        /// </summary>
        public uint Size
        {
            get { return size; }
        }

        private readonly Tag tag;
        private readonly uint size;

        /// <summary>
        /// Initializes a new <see cref="TagDefinitionAttribute"/> class using the supplied tag and size.
        /// </summary>
        /// <param name="tag">The tag of the definition.</param>
        /// <param name="size">The size of the tag.</param>
        public TagDefinitionAttribute(string tag, uint size)
        {
            this.tag = new Tag(tag);
            this.size = size;
        }
    }
}
