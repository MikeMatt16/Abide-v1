using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Abide.HaloLibrary
{
    /// <summary>
    /// Represents a 32-bit tag ID number.
    /// </summary>
    public struct TagId : IComparable<int>, IComparable<uint>, IComparable<TagId>, IEquatable<int>, IEquatable<uint>, IEquatable<TagId>
    {
        /// <summary>
        /// Represents a null tag id.
        /// </summary>
        public static readonly TagId Null = new TagId(-1);

        /// <summary>
        /// Gets and returns the ID as a signed 32-bit integer.
        /// </summary>
        public int Id
        {
            get { return (int)id; }
            set { id = (uint)value; }
        }
        /// <summary>
        /// Gets and returns the ID as an unsigned 32-bit integer.
        /// </summary>
        public uint Dword
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        /// Gets or sets the low 16-bit unsigned integer.
        /// </summary>
        public ushort LoWord
        {
            get { return (ushort)(id & 0xFFFF); }
            set { id = (id & 0x0000FFFF) | value; }
        }
        /// <summary>
        /// Gets or sets the high 16-bit unsigned integer.
        /// </summary>
        public ushort HiWord
        {
            get { return (ushort)(id << 16 & 0xFFFF); }
            set { id = (id & 0xFFFF) | (uint)(value) >> 16; }
        }
        /// <summary>
        /// Gets and returns true if the ID is null, otherwise, false.
        /// </summary>
        public bool IsNull
        {
            get { return id == uint.MaxValue; }
        }

        private uint id;

        /// <summary>
        /// Initializes a new <see cref="TagId"/> instance using the supplied ID.
        /// </summary>
        /// <param name="id">The 32-bit unsigned integer ID.</param>
        public TagId(uint id)
        {
            this.id = id;
        }
        /// <summary>
        /// Initializes a new <see cref="TagId"/> instance using the supplied ID.
        /// </summary>
        /// <param name="id">The 32-bit signed integer ID.</param>
        public TagId(int id)
        {
            this.id = (uint)id;
        }
        /// <summary>
        /// Compares this instance with a specified <see cref="int"/> object and indicates whether this instance preceds, follows, or appears in the same position as in the sort order as the specified id.
        /// </summary>
        /// <param name="id">The id to compare with this instance.</param>
        /// <returns>A 32-bit signed integer that indicates whether this instance precedes, follows, or appears in the same position in the sort order as the <paramref name="id"/> parameter.</returns>
        public int CompareTo(int id)
        {
            return ((int)this.id).CompareTo(id);
        }
        /// <summary>
        /// Compares this instance with a specified <see cref="uint"/> object and indicates whether this instance preceds, follows, or appears in the same position as in the sort order as the specified id.
        /// </summary>
        /// <param name="id">The id to compare with this instance.</param>
        /// <returns>A 32-bit signed integer that indicates whether this instance precedes, follows, or appears in the same position in the sort order as the <paramref name="id"/> parameter.</returns>
        public int CompareTo(uint id)
        {
            return this.id.CompareTo(id);
        }
        /// <summary>
        /// Compares this instance with a specified <see cref="TagId"/> object and indicates whether this instance preceds, follows, or appears in the same position as in the sort order as the specified id.
        /// </summary>
        /// <param name="id">The id to compare with this instance.</param>
        /// <returns>A 32-bit signed integer that indicates whether this instance precedes, follows, or appears in the same position in the sort order as the <paramref name="id"/> parameter.</returns>
        public int CompareTo(TagId id)
        {
            return id.CompareTo(id);
        }
        /// <summary>
        /// Determines whether this instance and another specified <see cref="int"/> value have the same value.
        /// </summary>
        /// <param name="id">The id to compare to this instance.</param>
        /// <returns>true if the value of the <paramref name="id"/> parameter is the same as the value of this instance; otherwise, false.</returns>
        public bool Equals(int id)
        {
            return ((int)this.id).Equals(id);
        }
        /// <summary>
        /// Determines whether this instance and another specified <see cref="uint"/> value have the same value.
        /// </summary>
        /// <param name="id">The id to compare to this instance.</param>
        /// <returns>true if the value of the <paramref name="id"/> parameter is the same as the value of this instance; otherwise, false.</returns>
        public bool Equals(uint id)
        {
            return this.id.Equals(id);
        }
        /// <summary>
        /// Determines whether this instance and another specified <see cref="TagId"/> value have the same value.
        /// </summary>
        /// <param name="id">The id to compare to this instance.</param>
        /// <returns>true if the value of the <paramref name="id"/> parameter is the same as the value of this instance; otherwise, false.</returns>
        public bool Equals(TagId id)
        {
            return id.Equals(id.id);
        }
        /// <summary>
        /// Gets a string representation of this instance.
        /// </summary>
        /// <returns>A 8-digit hex tag ID.</returns>
        public override string ToString()
        {
            return $"{id:X8}";
        }
        /// <summary>
        /// Converts a <see cref="TagId"/> to an unsigned 32-bit integer.
        /// </summary>
        /// <param name="id">The <see cref="TagId"/> object.</param>
        public static implicit operator uint(TagId id)
        {
            return id.id;
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
        public static implicit operator int(TagId id)
        {
            return (int)id.id;
        }
        /// <summary>
        /// Converts a signed 32-bit integer to to a <see cref="TagId"/> instance.
        /// </summary>
        /// <param name="id">The <see cref="int"/> object.</param>
        public static implicit operator TagId(int id)
        {
            return new TagId(id);
        }
    }
}
