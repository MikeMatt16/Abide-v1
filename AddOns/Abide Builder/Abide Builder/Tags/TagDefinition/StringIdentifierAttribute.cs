namespace Abide.Builder.Tags.TagDefinition
{
    /// <summary>
    /// Represents a String ID.
    /// </summary>
    public sealed class StringIdentifierAttribute : DefinitionAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringIdentifierAttribute"/> class using the specified name and offset.
        /// </summary>
        /// <param name="offset">The offset of the string ID.</param>
        /// <param name="name">The name of the string ID.</param>
        public StringIdentifierAttribute(string name, uint offset) : base(name, offset) { }
    }
}
