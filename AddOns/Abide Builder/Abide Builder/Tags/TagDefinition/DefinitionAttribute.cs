using System;

namespace Abide.Builder.Tags.TagDefinition
{
    /// <summary>
    /// Represents a basic tag definition 
    /// </summary>
    public abstract class DefinitionAttribute : Attribute
    {
        /// <summary>
        /// Gets and returns the name of the definition.
        /// </summary>
        public virtual string Name
        {
            get { return name; }
        }
        /// <summary>
        /// Gets and returns the offset of the definition.
        /// </summary>
        public virtual uint Offset
        {
            get { return offset; }
        }

        private readonly string name;
        private readonly uint offset;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefinitionAttribute"/> class using the supplied definition name and offset.
        /// </summary>
        /// <param name="name">The name of the definition.</param>
        /// <param name="offset">The offset of the definition.</param>
        public DefinitionAttribute(string name, uint offset)
        {
            this.name = name;
            this.offset = offset;
        }
    }
}
