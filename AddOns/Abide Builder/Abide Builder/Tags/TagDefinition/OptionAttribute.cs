using System;

namespace Abide.Builder.Tags.TagDefinition
{
    /// <summary>
    /// Represents an option for an enumeration or flag value.
    /// </summary>
    public sealed class OptionAttribute : Attribute
    {
        /// <summary>
        /// The value of the option.
        /// </summary>
        public long Value
        {
            get { return value; }
        }
        /// <summary>
        /// The name of the option.
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        private readonly long value;
        private readonly string name;

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionAttribute"/> class using the specified name and value.
        /// </summary>
        /// <param name="name">The name of the option.</param>
        /// <param name="value">The option value.</param>
        public OptionAttribute(string name, long value)
        {
            this.name = name;
            this.value = value;
        }
    }
}
