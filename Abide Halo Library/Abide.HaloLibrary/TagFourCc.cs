using System;
using System.Runtime.InteropServices;

namespace Abide.HaloLibrary
{
    /// <summary>
    /// Represents a Halo four-character code tag.
    /// </summary>
    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct TagFourCc : IComparable<string>, IComparable<TagFourCc>, IEquatable<string>, IEquatable<TagFourCc>
    {
        /// <summary>
        /// Represetns the size of the <see cref="TagFourCc"/> structure in bytes.
        /// This value is constant.
        /// </summary>
        public const int Size = 4;
        /// <summary>
        /// Gets or sets the tag four-character code.
        /// </summary>
        public string FourCc
        {
            get { return new string(new char[] { (char)a, (char)b, (char)c, (char)d }).Replace("\0", string.Empty); }
            set
            {
                string pad = value.PadRight(4, '\0');
                a = (sbyte)pad[0];
                b = (sbyte)pad[1];
                c = (sbyte)pad[2];
                d = (sbyte)pad[3];
            }
        }
        /// <summary>
        /// Gets or sets the tag dword.
        /// </summary>
        public uint Dword
        {
            get
            {
                int a = this.a, b = this.b, c = this.c, d = this.d;
                return (uint)(d | c << 8 | b << 16 | a << 24);
            }
            set
            {
                uint a = value & 0xff, b = value & 0xff00, c = value & 0xff0000, d = value & 0xff000000;
                this.d = (sbyte)(d >> 24); this.c = (sbyte)(c >> 16); this.b = (sbyte)(b >> 8); this.a = (sbyte)a;
            }
        }

        private sbyte d, c, b, a;

        /// <summary>
        /// Initializes a new <see cref="TagFourCc"/> instance using the specified string.
        /// </summary>
        /// <param name="fourCc">The four-character code string.</param>
        public TagFourCc(string fourCc)
        {
            string pad = fourCc.PadRight(4, '\0');
            a = (sbyte)pad[0];
            b = (sbyte)pad[1];
            c = (sbyte)pad[2];
            d = (sbyte)pad[3];
        }
        /// <summary>
        /// Initializes a new <see cref="TagFourCc"/> instance using the specified 32-bit unsigned integer.
        /// </summary>
        /// <param name="dword"></param>
        public TagFourCc(uint dword)
        {
            uint a = dword & 0xff, b = dword & 0xff00, c = dword & 0xff0000, d = dword & 0xff000000;
            this.d = (sbyte)(d >> 24); this.c = (sbyte)(c >> 16); this.b = (sbyte)(b >> 8); this.a = (sbyte)a;
        }
        /// <summary>
        /// Compares this instance with a specified <see cref="TagFourCc"/> object and indicates whether this instance preceds, follows, or appears in the same position as in the sort order as the specified tag.
        /// </summary>
        /// <param name="tag">The tag to compare with this instance.</param>
        /// <returns>A 32-bit signed integer that indicates whether this instance precedes, follows, or appears in the same position in the sort order as the <paramref name="tag"/> parameter.</returns>
        public int CompareTo(TagFourCc tag)
        {
            return FourCc.CompareTo(tag.FourCc);
        }
        /// <summary>
        /// Compares this instance with a specified <see cref="string"/> object and indicates whether this instance preceds, follows, or appears in the same position as in the sort order as the specified string.
        /// </summary>
        /// <param name="str">The string to compare with this instance.</param>
        /// <returns>A 32-bit signed integer that indicates whether this instance precedes, follows, or appears in the same position in the sort order as the <paramref name="str"/> parameter.</returns>
        public int CompareTo(string str)
        {
            return FourCc.CompareTo(str);
        }
        /// <summary>
        /// Determines whether this instance and another specified <see cref="TagFourCc"/> object have the same value.
        /// </summary>
        /// <param name="value">The tag to compare to this instance.</param>
        /// <returns>true if the value of the <paramref name="value"/> parameter is the same as the value of this instance; otherwise, false. If <paramref name="value"/> is null, the method returns false.</returns>
        public bool Equals(TagFourCc value)
        {
            return FourCc.Equals(value.FourCc);
        }
        /// <summary>
        /// Determines whether this instance and another specified <see cref="string"/> object have the same value.
        /// </summary>
        /// <param name="value">The string to compare to this instance.</param>
        /// <returns>true if the value of the <paramref name="value"/> parameter is the same as the value of this instance; otherwise, false. If <paramref name="value"/> is null, the method returns false.</returns>
        public bool Equals(string value)
        {
            return FourCc.Equals(value);
        }
        /// <summary>
        /// Returns the instance of this tag four-character code.
        /// </summary>
        /// <returns>A string containing the tag fourcc.</returns>
        public override string ToString()
        {
            return FourCc;
        }
        /// <summary>
        /// Converts a <see cref="TagFourCc"/> to a <see cref="string"/>.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public static implicit operator string(TagFourCc tag)
        {
            return tag.FourCc;
        }
        /// <summary>
        /// Converts a <see cref="string"/> to a <see cref="TagFourCc"/>.
        /// </summary>
        /// <param name="fourCc">The four-character code string.</param>
        public static implicit operator TagFourCc(string fourCc)
        {
            return new TagFourCc(fourCc);
        }
        /// <summary>
        /// Converts the <see cref="TagFourCc"/> to an unsigned 32-bit integer.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public static explicit operator uint(TagFourCc tag)
        {
            int a = tag.a, b = tag.b, c = tag.c, d = tag.d;
            return (uint)(d | c << 8 | b << 16 | a << 24);
        }
        /// <summary>
        /// Converts a 32-bit unsigned integer to a <see cref="TagFourCc"/>.
        /// </summary>
        /// <param name="dword">The tag dword.</param>
        public static explicit operator TagFourCc(uint dword)
        {
            uint a = dword & 0xff, b = dword & 0xff00, c = dword & 0xff0000, d = dword & 0xff000000;
            return new TagFourCc() { d = (sbyte)(d >> 24), c = (sbyte)(c >> 16), b = (sbyte)(b >> 8), a = (sbyte)a };
        }
    }
}
