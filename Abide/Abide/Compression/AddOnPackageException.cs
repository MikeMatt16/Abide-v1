using System;

namespace Abide.Compression
{
    /// <summary>
    /// Represents an error that occurs while handling an Abide AddOn Package file.
    /// </summary>
    public sealed class AbideAddOnPackageException : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="AbideAddOnPackageException"/>.
        /// </summary>
        public AbideAddOnPackageException() : base()
        { }

        /// <summary>
        /// Initializes a new instance of <see cref="AbideAddOnPackageException"/> using the provided exception message.
        /// </summary>
        /// <param name="message">The exception's message.</param>
        public AbideAddOnPackageException(string message) :
            base(message)
        { }

        /// <summary>
        /// Initializes a new instance of <see cref="AbideAddOnPackageException"/> using the provided inner exception.
        /// </summary>
        /// <param name="innerException">The inner exception that triggered the <see cref="AbideAddOnPackageException"/>.</param>
        public AbideAddOnPackageException(Exception innerException) : base(innerException.Message, innerException)
        { }

        /// <summary>
        /// Initializes a new instance of <see cref="AbideAddOnPackageException"/> using the provided exception message and inner exception.
        /// </summary>
        /// <param name="message">The exception's message.</param>
        /// <param name="innerException">The inner exception that triggered the <see cref="AbideAddOnPackageException"/>.</param>
        public AbideAddOnPackageException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
