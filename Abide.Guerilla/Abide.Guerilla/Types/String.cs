using System;
using System.Text;

namespace Abide.Guerilla.Types
{
    /// <summary>
    /// Represents a guerilla string.
    /// This string is encoded with <see cref="Encoding.UTF8"/> encoding.
    /// </summary>
    public unsafe struct String32 : IEquatable<string>, IComparable<string>
    {
        /// <summary>
        /// The size of the string.
        /// </summary>
        public const int Size = 32;
        /// <summary>
        /// Represents an empty string.
        /// </summary>
        public static readonly String32 Empty = new String32();

        /// <summary>
        /// Gets or sets the string value of this guerilla string.
        /// </summary>
        private string Value
        {
            get
            {
                string value = string.Empty;
                fixed (sbyte* pStr = str) value = new string(pStr).Trim();
                return value;
            }
            set
            {
                byte[] buffer = Encoding.UTF8.GetBytes(value.PadRight(Size));
                fixed (byte* pBuffer = buffer)
                fixed (sbyte* pStr = str)
                {
                    byte* src = pBuffer;
                    sbyte* dst = pStr;

                    for (int i = 0; i < Size; i++)
                    {
                        *dst = (sbyte)*src;
                        dst++; src++;
                    }
                }
            }
        }
        
        private fixed sbyte str[Size];
        /// <summary>
        /// Gets a value that determines if this string value and another specified <see cref="string"/> object have the same value. 
        /// </summary>
        /// <param name="s">The other string value.</param>
        /// <returns>true if the strings are equal; otherwise false.</returns>
        public bool Equals(string s)
        {
            return Value.Equals(s);
        }
        /// <summary>
        /// Compares this string with a specified <see cref="string"/> object and indicates whether this object preceds, 
        /// follows, or appears in the same position in the sort order as the specified string.
        /// </summary>
        /// <param name="s">The string to compare this instance to.</param>
        /// <returns>A 32-bit signed integer that indicates whether this instance precedes, follows, or appears in the same position in the sort order as the <paramref name="s"/> parameter.</returns>
        public int CompareTo(string s)
        {
            return Value.CompareTo(s);
        }
        /// <summary>
        /// Gets the string value of this instance.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return Value;
        }
        /// <summary>
        /// Converts a guerilla string instance to a <see cref="string"/> object.
        /// </summary>
        /// <param name="guerillaString">The guerilla string.</param>
        public static implicit operator string(String32 guerillaString)
        {
            return guerillaString.Value;
        }
        /// <summary>
        /// Converts a string object to a guerilla string instance.
        /// </summary>
        /// <param name="str">The string.</param>
        public static implicit operator String32(string str)
        {
            String32 s = new String32();
            s.Value = str;
            return s;
        }
    }

    /// <summary>
    /// Represents a long guerilla string.
    /// This string is encoded with <see cref="Encoding.UTF8"/> encoding.
    /// </summary>
    public unsafe struct String256 : IEquatable<string>, IComparable<string>
    {
        /// <summary>
        /// The size of the string.
        /// </summary>
        public const int Size = 256;
        /// <summary>
        /// Represents an empty string.
        /// </summary>
        public static readonly String32 Empty = new String32();

        /// <summary>
        /// Gets or sets the string value of this guerilla string.
        /// </summary>
        private string Value
        {
            get
            {
                string value = string.Empty;
                fixed (sbyte* pStr = str) value = new string(pStr).Trim();
                return value;
            }
            set
            {
                byte[] buffer = Encoding.UTF8.GetBytes(value.PadRight(Size));
                fixed (byte* pBuffer = buffer)
                fixed (sbyte* pStr = str)
                {
                    byte* src = pBuffer;
                    sbyte* dst = pStr;

                    for (int i = 0; i < Size; i++)
                    {
                        *dst = (sbyte)*src;
                        dst++; src++;
                    }
                }
            }
        }

        private fixed sbyte str[Size];
        /// <summary>
        /// Gets a value that determines if this string value and another specified <see cref="string"/> object have the same value. 
        /// </summary>
        /// <param name="s">The other string value.</param>
        /// <returns>true if the strings are equal; otherwise false.</returns>
        public bool Equals(string s)
        {
            return Value.Equals(s);
        }
        /// <summary>
        /// Compares this string with a specified <see cref="string"/> object and indicates whether this object preceds, 
        /// follows, or appears in the same position in the sort order as the specified string.
        /// </summary>
        /// <param name="s">The string to compare this instance to.</param>
        /// <returns>A 32-bit signed integer that indicates whether this instance precedes, follows, or appears in the same position in the sort order as the <paramref name="s"/> parameter.</returns>
        public int CompareTo(string s)
        {
            return Value.CompareTo(s);
        }
        /// <summary>
        /// Gets the string value of this instance.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return Value;
        }
        /// <summary>
        /// Converts a guerilla string instance to a <see cref="string"/> object.
        /// </summary>
        /// <param name="guerillaString">The guerilla string.</param>
        public static implicit operator string(String256 guerillaString)
        {
            return guerillaString.Value;
        }
        /// <summary>
        /// Converts a string object to a guerilla string instance.
        /// </summary>
        /// <param name="str">The string.</param>
        public static implicit operator String256(string str)
        {
            String256 s = new String256();
            s.Value = str;
            return s;
        }
    }
}
