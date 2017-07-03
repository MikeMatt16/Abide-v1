using System;
using System.Linq;

namespace Abide.HaloLibrary
{
    /// <summary>
    /// Represents a Halo four-character code tag.
    /// </summary>
    [Serializable]
    public struct Tag : IEquatable<Tag>, IEquatable<string>, IComparable<Tag>, IComparable<string>
    {
        /// <summary>
        /// Gets or sets the four-character code text string.
        /// </summary>
        public string TagFourCc
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
        /// Creates a new <see cref="HaloLibrary.Tag"/> structure using the supplied tag string.
        /// </summary>
        /// <param name="tagFourcc">The four-character code of the tag.</param>
        public Tag(string tagFourcc)
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
            return TagFourCc.Equals(other);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Tag other)
        {
            return a.Equals(other.a) && b.Equals(other.b) && c.Equals(other.c) && d.Equals(other.d);
        }

        /// <summary>
        /// Returns a string representation of this tag.
        /// </summary>
        /// <returns>The tag as a four-character code.</returns>
        public override string ToString()
        {
            return TagFourCc;
        }

        /// <summary>
        /// Creates a <see cref="HaloLibrary.Tag"/> structure using the supplied tag four-character code.
        /// </summary>
        /// <param name="tagFourcc">The four-character code of the tag.</param>
        /// <returns>A <see cref="HaloLibrary.Tag"/> structure.</returns>
        public static Tag StringToTag(string tagFourcc)
        {
            return new Tag(tagFourcc);
        }

        /// <summary>
        /// Compares this instance with a specified <see cref="HaloLibrary.Tag"/> object and indicates whether this instance preceeds, follows, or appears in the same position in the sort order as the specified <see cref="HaloLibrary.Tag"/>. 
        /// </summary>
        /// <param name="tag">The <see cref="HaloLibrary.Tag"/> to compare with this instance.</param>
        /// <returns>A 32-bit signed integer that indicates whether this instance precedes, follows, or appears in the same position in the sort order as the <paramref name="tag"/> parameter.</returns>
        public int CompareTo(Tag tag)
        {
            return TagFourCc.CompareTo(tag.TagFourCc);
        }

        /// <summary>
        /// Compares this instance with a specified <see cref="string"/> and indicates whether this instance preceeds, follows, or appears in the same position in the sort order as the specified <see cref="string"/>.
        /// </summary>
        /// <param name="str">The <see cref="string"/> to compare with this instance.</param>
        /// <returns>A 32-bit signed integer that indicates whether this instance precedes, follows, or appears in the same position in the sort order as the <paramref name="str"/> parameter.</returns>
        public int CompareTo(string str)
        {
            return TagFourCc.CompareTo(str);
        }

        public static implicit operator Tag (string tagFourcc)
        {
            return new Tag(tagFourcc);
        }
        public static implicit operator string(Tag tag)
        {
            return tag.TagFourCc;
        }
    }
}
