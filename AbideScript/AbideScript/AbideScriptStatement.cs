using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace AbideScript
{
    public sealed class AbideScriptStatement : IEnumerable<AbideScriptToken>
    {
        private readonly List<AbideScriptToken> tokens = null;

        public AbideScriptToken this[int index]
        {
            get { if (index >= tokens.Count || index < 0) throw new ArgumentOutOfRangeException(nameof(index)); return tokens[index]; }
        }
        public int Count
        {
            get { return tokens.Count; }
        }
        internal AbideScriptStatement(IEnumerable<AbideScriptToken> tokens)
        {
            //Check
            if (tokens == null) throw new ArgumentNullException(nameof(tokens));
            this.tokens = new List<AbideScriptToken>(tokens);
        }
        [DebuggerStepThrough]
        public IEnumerator<AbideScriptToken> GetEnumerator()
        {
            return tokens.GetEnumerator();
        }
        [DebuggerStepThrough]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
