using System;
using System.Runtime.InteropServices;

namespace Abide.HaloLibrary
{
    /// <summary>
    /// Represents a 32-bit tag ID number.
    /// </summary>
    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct TagId : IComparable<int>, IComparable<uint>, IComparable<TagId>, IEquatable<int>, IEquatable<uint>, IEquatable<TagId>
    {
        /// <summary>
        /// Represents a null tag id.
        /// This value is read-only.
        /// </summary>
        public static readonly TagId Null = new TagId(-1);

        private short index;
        private short salt;

        /// <summary>
        /// Gets and returns the ID as an unsigned 32-bit integer.
        /// </summary>
        public uint Dword
        {
            get => (uint)(((ushort)salt << 16) | (ushort)index);
        }
        /// <summary>
        /// Gets or sets the salt of the tag.
        /// </summary>
        public short Salt
        {
            get => salt;
            set => salt = value;
        }
        /// <summary>
        /// Gets or sets the absolute index of the tag.
        /// </summary>
        public short Index
        {
            get => index;
            set => index = value;
        }
        /// <summary>
        /// Gets and returns <see langword="true"/> if the ID is null, otherwise, <see langword="false"/>.
        /// </summary>
        public bool IsNull
        {
            get { return Dword == uint.MaxValue; }
        }

        private TagId(int id)
        {
            index = (short)(id & 0xffff);
            salt = (short)(id >> 16);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="salt"></param>
        /// <param name="index"></param>
        public TagId(int salt, int index)
        {
            this.salt = (short)salt;
            this.index = (short)index;
        }
        /// <summary>
        /// Initializes a new <see cref="TagId"/> instance using the supplied ID.
        /// </summary>
        /// <param name="id">The 32-bit unsigned integer ID.</param>
        public TagId(uint id)
        {
            index = (short)(id & 0xffff);
            salt = (short)(id >> 16);
        }
        /// <summary>
        /// Compares this instance with a specified <see cref="int"/> object and indicates whether this instance preceds, follows, or appears in the same position as in the sort order as the specified id.
        /// </summary>
        /// <param name="id">The id to compare with this instance.</param>
        /// <returns>A 32-bit signed integer that indicates whether this instance precedes, follows, or appears in the same position in the sort order as the <paramref name="id"/> parameter.</returns>
        public int CompareTo(int id)
        {
            return ((int)Dword).CompareTo(id);
        }
        /// <summary>
        /// Compares this instance with a specified <see cref="uint"/> object and indicates whether this instance preceds, follows, or appears in the same position as in the sort order as the specified id.
        /// </summary>
        /// <param name="id">The id to compare with this instance.</param>
        /// <returns>A 32-bit signed integer that indicates whether this instance precedes, follows, or appears in the same position in the sort order as the <paramref name="id"/> parameter.</returns>
        public int CompareTo(uint id)
        {
            return Dword.CompareTo(id);
        }
        /// <summary>
        /// Compares this instance with a specified <see cref="TagId"/> object and indicates whether this instance preceds, follows, or appears in the same position as in the sort order as the specified id.
        /// </summary>
        /// <param name="id">The id to compare with this instance.</param>
        /// <returns>A 32-bit signed integer that indicates whether this instance precedes, follows, or appears in the same position in the sort order as the <paramref name="id"/> parameter.</returns>
        public int CompareTo(TagId id)
        {
            return Dword.CompareTo(id.Dword);
        }
        /// <summary>
        /// Determines whether this instance and another specified <see cref="int"/> value have the same value.
        /// </summary>
        /// <param name="id">The id to compare to this instance.</param>
        /// <returns>true if the value of the <paramref name="id"/> parameter is the same as the value of this instance; otherwise, false.</returns>
        public bool Equals(int id)
        {
            return ((int)Dword).Equals(id);
        }
        /// <summary>
        /// Determines whether this instance and another specified <see cref="uint"/> value have the same value.
        /// </summary>
        /// <param name="id">The id to compare to this instance.</param>
        /// <returns>true if the value of the <paramref name="id"/> parameter is the same as the value of this instance; otherwise, false.</returns>
        public bool Equals(uint id)
        {
            return Dword.Equals(id);
        }
        /// <summary>
        /// Determines whether this instance and another specified <see cref="TagId"/> value have the same value.
        /// </summary>
        /// <param name="id">The id to compare to this instance.</param>
        /// <returns>true if the value of the <paramref name="id"/> parameter is the same as the value of this instance; otherwise, false.</returns>
        public bool Equals(TagId id)
        {
            return salt.Equals(id.salt) && index.Equals(id.Index);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is TagId tagId)
                return Equals(tagId);
            else if (obj is int i)
                return Equals(i);
            else if (obj is uint u)
                return Equals(u);

            return base.Equals(obj);
        }
        /// <summary>
        /// Gets a string representation of this instance.
        /// </summary>
        /// <returns>A 8-digit hex tag ID.</returns>
        public override string ToString()
        {
            return $"{Dword:X8}";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return salt.GetHashCode() ^ index.GetHashCode();
        }
        /// <summary>
        /// Converts a <see cref="TagId"/> to an unsigned 32-bit integer.
        /// </summary>
        /// <param name="id">The <see cref="TagId"/> object.</param>
        public static implicit operator uint(TagId id)
        {
            return id.Dword;
        }
        /// <summary>
        /// Converts a unsigned 32-bit integer to a <see cref="TagId"/> instance.
        /// </summary>
        /// <param name="id">The <see cref="uint"/> object.</param>
        public static implicit operator TagId(uint id)
        {
            return new TagId(id);
        }
        /// <summary>
        /// Converts a <see cref="TagId"/> to a signed 32-bit integer.
        /// </summary>
        /// <param name="id">The <see cref="TagId"/> object.</param>
        public static explicit operator int(TagId id)
        {
            return (int)id.Dword;
        }
        /// <summary>
        /// Converts a signed 32-bit integer to to a <see cref="TagId"/> instance.
        /// </summary>
        /// <param name="id">The <see cref="int"/> object.</param>
        public static explicit operator TagId(int id)
        {
            return new TagId(id);
        }
        /// <summary>
        /// Increases the tag ID salt and index by one.
        /// </summary>
        /// <param name="id">The tag ID.</param>
        /// <returns>A new tag ID value.</returns>
        public static TagId operator ++(TagId id)
        {
            TagId newId = new TagId(id.salt + 1, id.index + 1);
            newId.Salt++;
            newId.Index++;
            return newId;
        }
        /// <summary>
        /// Decreases the tag ID salt and index by one.
        /// </summary>
        /// <param name="id">The tag ID.</param>
        /// <returns>A new tag ID value.</returns>
        public static TagId operator --(TagId id)
        {
            TagId newId = new TagId(id.Dword);
            newId.Salt--;
            newId.Index--;
            return newId;
        }
        public static bool operator ==(TagId left, TagId right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(TagId left, TagId right)
        {
            return !(left == right);
        }
    }
}
