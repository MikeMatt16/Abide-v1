using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace Abide.HaloLibrary
{
    /// <summary>
    /// Represents a Halo four-character code tag.
    /// </summary>
    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct Tag : IComparable<string>, IComparable<Tag>, IEquatable<string>, IEquatable<Tag>
    {
        /// <summary>
        /// Gets or sets the tag four-character code.
        /// </summary>
        public string FourCc
        {
            get { return new string(new char[] { (char)a, (char)b, (char)c, (char)d }); }
            set
            {
                string pad = value.PadRight(4, '\0');
                d = (sbyte)pad[0];
                c = (sbyte)pad[1];
                b = (sbyte)pad[2];
                a = (sbyte)pad[3];
            }
        }

        private sbyte d, c, b, a;

        /// <summary>
        /// Initializes a new <see cref="Tag"/> instance using the supplied string.
        /// </summary>
        /// <param name="fourCc">The four-character code string.</param>
        public Tag(string fourCc)
        {
            string pad = fourCc.PadRight(4, '\0');
            d = (sbyte)pad[0];
            c = (sbyte)pad[1];
            b = (sbyte)pad[2];
            a = (sbyte)pad[3];
        }
        /// <summary>
        /// Compares this instance with a specified <see cref="Tag"/> object and indicates whether this instance preceds, follows, or appears in the same position as in the sort order as the specified tag.
        /// </summary>
        /// <param name="tag">The tag to compare with this instance.</param>
        /// <returns>A 32-bit signed integer that indicates whether this instance precedes, follows, or appears in the same position in the sort order as the <paramref name="tag"/> parameter.</returns>
        public int CompareTo(Tag tag)
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
        /// Determines whether this instance and another specified <see cref="Tag"/> object have the same value.
        /// </summary>
        /// <param name="value">The tag to compare to this instance.</param>
        /// <returns>true if the value of the <paramref name="value"/> parameter is the same as the value of this instance; otherwise, false. If <paramref name="value"/> is null, the method returns false.</returns>
        public bool Equals(Tag value)
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
        /// Converts a <see cref="Tag"/> to a <see cref="string"/>.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public static implicit operator string(Tag tag)
        {
            return tag.FourCc;
        }
        /// <summary>
        /// Converts a <see cref="string"/> to a <see cref="Tag"/>.
        /// </summary>
        /// <param name="fourCc">The four-character code string.</param>
        public static implicit operator Tag(string fourCc)
        {
            return new Tag(fourCc);
        }
    }
}
