namespace Abide.Builder.Tags.TagDefinition
{
    /// <summary>
    /// Represents value definition 
    /// </summary>
    public sealed class ValueDefinitionAttribute : DefinitionAttribute
    {
        /// <summary>
        /// Gets and returns the value type.
        /// </summary>
        public ValueDefinitionType Type
        {
            get { return type; }
        }

        private readonly ValueDefinitionType type;

        /// <summary>
        /// Initializes an instance of the <see cref="ValueDefinitionAttribute"/> class using the supplied name, value type, and offset.
        /// </summary>
        /// <param name="name">The name of the value.</param>
        /// <param name="type">The value type.</param>
        /// <param name="offset">The offset of the value.</param>
        public ValueDefinitionAttribute(string name, ValueDefinitionType type, uint offset) : base(name, offset)
        {
            this.type = type;
        }
    }

    /// <summary>
    /// Represents an enumeration specifying value types.
    /// </summary>
    public enum ValueDefinitionType
    {
        /// <summary>
        /// A signed 8-bit integer.
        /// </summary>
        Int8,
        /// <summary>
        /// An unsigned 8-bit integer.
        /// </summary>
        UInt8,
        /// <summary>
        /// A signed 16-bit ingeter.
        /// </summary>
        Int16,
        /// <summary>
        /// An unsigned 16-bit integer.
        /// </summary>
        UInt16,
        /// <summary>
        /// A signed 32-bit integer.
        /// </summary>
        Int32,
        /// <summary>
        /// An unsigned 32-bit integer.
        /// </summary>
        UInt32,
        /// <summary>
        /// A signed 64-bit integer.
        /// </summary>
        Int64,
        /// <summary>
        /// An unsigned 64-bit integer.
        /// </summary>
        UInt64,
        /// <summary>
        /// A single precision floating point number.
        /// </summary>
        Single,
        /// <summary>
        /// A double precision floating point number.
        /// </summary>
        Double,
    }
}
