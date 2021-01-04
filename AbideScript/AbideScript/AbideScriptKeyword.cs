using System;

namespace AbideScript
{
    public sealed class AbideScriptKeyword
    {
        private string keyword = string.Empty;

        public string Keyword
        {
            get { return keyword; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                keyword = value;
            }
        }

        public override string ToString()
        {
            return $"{keyword}";
        }
    }

    public static class Keywords
    {
        public static AbideScriptKeyword StringKeyword
        {
            get { return new AbideScriptKeyword() { Keyword = "string" }; }
        }
        public static AbideScriptKeyword CharKeyword
        {
            get { return new AbideScriptKeyword() { Keyword = "char" }; }
        }
        public static AbideScriptKeyword IntKeyword
        {
            get { return new AbideScriptKeyword() { Keyword = "int" }; }
        }
        public static AbideScriptKeyword UIntKeyword
        {
            get { return new AbideScriptKeyword() { Keyword = "uint" }; }
        }
        public static AbideScriptKeyword ShortKeyword
        {
            get { return new AbideScriptKeyword() { Keyword = "short" }; }
        }
        public static AbideScriptKeyword UShortKeyword
        {
            get { return new AbideScriptKeyword() { Keyword = "ushort" }; }
        }
        public static AbideScriptKeyword ByteKeyword
        {
            get { return new AbideScriptKeyword() { Keyword = "byte" }; }
        }
        public static AbideScriptKeyword SByteKeyword
        {
            get { return new AbideScriptKeyword() { Keyword = "sbyte" }; }
        }
        public static AbideScriptKeyword LongKeyword
        {
            get { return new AbideScriptKeyword() { Keyword = "long" }; }
        }
        public static AbideScriptKeyword ULongKeyword
        {
            get { return new AbideScriptKeyword() { Keyword = "ulong" }; }
        }
        public static AbideScriptKeyword ClassKeyword
        {
            get { return new AbideScriptKeyword() { Keyword = "class" }; }
        }
    }
}
