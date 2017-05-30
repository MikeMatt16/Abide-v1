using System;

namespace Abide.Ifp
{
    /// <summary>
    /// Returns detailed information about the last exception.
    /// </summary>
    public sealed class IfpException : Exception
    {
        /// <summary>
        /// Initializes a new <see cref="IfpException"/> using the specified message.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public IfpException(string message) : base(message) { }
        /// <summary>
        /// Initializes a new <see cref="IfpException"/> using the supplied message and inner exception.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception.</param>
        public IfpException(string message, Exception innerException) : base(message, innerException) { }
    }
}
