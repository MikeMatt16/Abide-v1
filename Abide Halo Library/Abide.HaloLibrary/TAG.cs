using System;
using System.Linq;

namespace Abide.HaloLibrary
{
    /// <summary>
    /// Represents a Halo four-character code tag.
    /// </summary>
    [Serializable]
    public struct TAG : IEquatable<TAG>, IEquatable<string>, IComparable<TAG>, IComparable<string>
    {
        /// <summary>
        /// Gets or sets the four-character code text string.
        /// </summary>
        public string Tag
        {
            get { return new string(new char[] { a, b, c, d }).Trim('\0').Trim('\xff').Reverse(); }
            set
            {
                //Get four-cc
                char[] fourcc = new char[4];
                string reversed = value.Reverse();
                for (int i = 0; i < Math.Min(reversed.Length, fourcc.Length); i++)
                    fourcc[i] = reversed[i];

                //Set
                a = fourcc[0];
                b = fourcc[1];
                c = fourcc[2];
                d = fourcc[3];
            }
        }

        private char a, b, c, d;

        /// <summary>
        /// Creates a new <see cref="TAG"/> structure using the supplied tag string.
        /// </summary>
        /// <param name="tagFourcc">The four-character code of the tag.</param>
        public TAG(string tagFourcc)
        {
            //Get four-cc
            char[] fourcc = new char[4];
            string reversed = tagFourcc.Reverse();
            for (int i = 0; i < Math.Min(reversed.Length, fourcc.Length); i++)
                fourcc[i] = reversed[i];

            //Set
            a = fourcc[0];
            b = fourcc[1];
            c = fourcc[2];
            d = fourcc[3];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(string other)
        {
            return Tag.Equals(other);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(TAG other)
        {
            return a.Equals(other.a) && b.Equals(other.b) && c.Equals(other.c) && d.Equals(other.d);
        }

        /// <summary>
        /// Returns a string representation of this tag.
        /// </summary>
        /// <returns>The tag as a four-character code.</returns>
        public override string ToString()
        {
            return Tag;
        }

        /// <summary>
        /// Creates a <see cref="TAG"/> structure using the supplied tag four-character code.
        /// </summary>
        /// <param name="tagFourcc">The four-character code of the tag.</param>
        /// <returns>A <see cref="TAG"/> structure.</returns>
        public static TAG StringToTag(string tagFourcc)
        {
            return new TAG(tagFourcc);
        }

        /// <summary>
        /// Compares this instance with a specified <see cref="TAG"/> object and indicates whether this instance preceeds, follows, or appears in the same position in the sort order as the specified <see cref="TAG"/>. 
        /// </summary>
        /// <param name="tag">The <see cref="TAG"/> to compare with this instance.</param>
        /// <returns>A 32-bit signed integer that indicates whether this instance precedes, follows, or appears in the same position in the sort order as the <paramref name="tag"/> parameter.</returns>
        public int CompareTo(TAG tag)
        {
            return Tag.CompareTo(tag.Tag);
        }

        /// <summary>
        /// Compares this instance with a specified <see cref="string"/> and indicates whether this instance preceeds, follows, or appears in the same position in the sort order as the specified <see cref="string"/>.
        /// </summary>
        /// <param name="str">The <see cref="string"/> to compare with this instance.</param>
        /// <returns>A 32-bit signed integer that indicates whether this instance precedes, follows, or appears in the same position in the sort order as the <paramref name="str"/> parameter.</returns>
        public int CompareTo(string str)
        {
            return Tag.CompareTo(str);
        }

        public static implicit operator TAG (string tagFourcc)
        {
            return new TAG(tagFourcc);
        }
        public static implicit operator string(TAG tag)
        {
            return tag.Tag;
        }
    }
}
