namespace Abide.Builder.Tags.TagDefinition
{
    /// <summary>
    /// Represents a tag string.
    /// </summary>
    public sealed class TagAttribute : DefinitionAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagAttribute"/> class using the specified name and offset.
        /// </summary>
        /// <param name="offset">The offset of the tag string.</param>
        /// <param name="name">The name of the tag string.</param>
        public TagAttribute(string name, uint offset) : base(name, offset) { }
    }
}
