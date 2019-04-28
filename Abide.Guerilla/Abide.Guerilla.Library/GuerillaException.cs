using System;

namespace Abide.Guerilla.Library
{
    public sealed class GuerillaException : Exception
    {
        public GuerillaException(string message) : base(message) { }

        public GuerillaException(string message, Exception innerException) : base(message, innerException) { }
    }
}
