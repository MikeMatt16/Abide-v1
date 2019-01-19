using System;

namespace Abide.DebugXbox
{
    /// <summary>
    /// Represents a debug Xbox command argument.
    /// </summary>
    public struct CommandArgument
    {
        private string name;
        private object value;

        /// <summary>
        /// Gets or sets the name of the argument.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value ?? string.Empty; }
        }
        /// <summary>
        /// Gets or sets the value of the argument.
        /// </summary>
        public object Value
        {
            get { return value; }
            set { this.value = value ?? string.Empty; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandArgument"/> structure using the specified name.
        /// </summary>
        /// <param name="name">The name of the argument.</param>
        public CommandArgument(string name)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            value = null;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandArgument"/> structure using the specified name and value.
        /// </summary>
        /// <param name="name">The name of the argument.</param>
        /// <param name="value">The value of the argument.</param>
        public CommandArgument(string name, object value)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.value = value ?? throw new ArgumentNullException(nameof(value));
        }
        /// <summary>
        /// Returns a string that represents the current <see cref="CommandArgument"/>.
        /// </summary>
        /// <returns>The argument string.</returns>
        public override string ToString()
        {
            if (value == null) return Name;
            else if (value.ToString().Contains(" ")) return $"{name}=\"{value}\"";
            else return $"{name}={value}";
        }
    }
}
