namespace Abide.Builder.Tags.TagDefinition
{
    /// <summary>
    /// Represents a tag ID.
    /// </summary>
    public sealed class TagIdentifierAttribute : DefinitionAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagIdentifierAttribute"/> class using the specified name and offset.
        /// </summary>
        /// <param name="name">The name of the tag ID.</param>
        /// <param name="offset">The offset of the tag ID.</param>
        public TagIdentifierAttribute(string name, uint offset) : base(name, offset) { }
    }
}
