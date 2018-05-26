using System;

namespace Abide.Tag
{
    /// <summary>
    /// Returns detailed information about the last exception.
    /// </summary>
    public sealed class TagException : Exception
    {
        /// <summary>
        /// Initializes a new <see cref="TagException"/> using the specified message.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public TagException(string message) : base(message) { }
        /// <summary>
        /// Initializes a new <see cref="TagException"/> using the supplied message and inner exception.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception.</param>
        public TagException(string message, Exception innerException) : base(message, innerException) { }
    }
}
