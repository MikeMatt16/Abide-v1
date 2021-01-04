using System;
using System.Collections.Generic;
using System.Text;

namespace AbideScript
{
    public class AbideScriptToken
    {
        public string Token { get; set; } = string.Empty;

        public int LineNumber { get; } = -1;

        public int LinePosition { get; } = -1;

        internal AbideScriptToken() { }

        public AbideScriptToken(string token)
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            if (token == string.Empty) throw new ArgumentException("Invalid token.", nameof(token));

            Token = token;
        }

        public AbideScriptToken(string token, int lineNumber, int linePosition) : this(token)
        {
            LineNumber = lineNumber;
            LinePosition = linePosition;
        }

        public override string ToString()
        {
            return $"\"{Token}\"";
        }
    }
}
