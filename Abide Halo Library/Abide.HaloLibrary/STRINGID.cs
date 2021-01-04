using System;
using System.Runtime.InteropServices;

namespace Abide.HaloLibrary
{
    /// <summary>
    /// Represents a 32-bit Halo Map string identifier.
    /// </summary>
    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct StringId : IComparable<StringId>, IComparable<int>, IComparable<uint>, IEquatable<StringId>, IEquatable<int>, IEquatable<uint>
    {
        /// <summary>
        /// Represents an zero (or empty) string identifier.
        /// </summary>
        public static readonly StringId Zero = new StringId() { sid = 0x0 };

        /// <summary>
        /// Gets and returns this instance as a 32-bit unsigned integer value.
        /// </summary>
        public uint ID
        {
            get { return sid; }
        }
        /// <summary>
        /// Gets or sets the length of the string that is represented by this instance.
        /// </summary>
        public byte Length
        {
            get { return Convert.ToByte(sid >> 24); }
            set { sid = Convert.ToUInt32(value << 24 | Index); }
        }
        /// <summary>
        /// Gets or sets the index of the string that is represented by this instance.
        /// </summary>
        public ushort Index
        {
            get { return Convert.ToUInt16(sid & 0xFFFF); }
            set { sid = Convert.ToUInt32(Length << 24 | value); }
        }

        private uint sid;

        /// <summary>
        /// Initializes a <see cref="StringId"/> structure using the supplied value.
        /// </summary>
        /// <param name="sid">The 32-bit unsigned string ID value.</param>
        public StringId(uint sid)
        {
            this.sid = sid;
        }
        /// <summary>
        /// Initializes a <see cref="StringId"/> structure using the supplied string index and string length values.
        /// </summary>
        /// <param name="index">The index of the string in the string table.</param>
        /// <param name="length">The length of the string.</param>
        public StringId(ushort index, byte length)
        {
            sid = Convert.ToUInt32(length << 24 | index);
        }

        /// <summary>
        /// Creates a new <see cref="StringId"/> structure from a supplied string and index.
        /// </summary>
        /// <param name="value">The string to use as reference.</param>
        /// <param name="index">The index of the string identifier.</param>
        /// <returns>A new <see cref="StringId"/> structure containing the supplied string length and index.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="index"/> it outside of valid range.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/> length is outside of valid range.</exception>
        public static StringId FromString(string value, int index)
        {
            //Check
            if (value == null)
                throw new ArgumentNullException("value", "String value cannot be null.");
            else if (index > ushort.MaxValue || index < 0)
                throw new ArgumentException("Index is out of possible range.", "index");
            else if (value.Length > 128)
                throw new ArgumentException("String length is out of possible range.", "value");

            //Prepare SID
            StringId sid = Zero;
            sid.Index = (ushort)(index & ushort.MaxValue);
            sid.Length = (byte)(value.Length & byte.MaxValue);
            return sid;
        }
        /// <summary>
        /// Compares this instance to a 32-bit unsigned integer and returns an indication of their relative values.
        /// </summary>
        /// <param name="other">A 32-bit unsigned integer to comapre to.</param>
        /// <returns></returns>
        public int CompareTo(uint other)
        {
            return sid.CompareTo(other);
        }
        /// <summary>
        /// Compares this instance to a 32-bit signed integer and returns an indication of their relative values.
        /// </summary>
        /// <param name="other">A 32-bit signed integer to comapre to.</param>
        public int CompareTo(int other)
        {
            return CompareTo((uint)other);
        }
        /// <summary>
        /// Compares this instance to a 32-bit <see cref="StringId"/> structure and returns an indication of their relative values.
        /// </summary>
        /// <param name="other">A 32-bit <see cref="StringId"/> to comapre to.</param>
        public int CompareTo(StringId other)
        {
            return CompareTo(other.sid);
        }
        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified <see cref="uint"/>.
        /// </summary>
        /// <param name="other">A 32-bit unsigned integer to compare to this instance.</param>
        /// <returns>True if the relative values are equal, otherwise returns false.</returns>
        public bool Equals(uint other)
        {
            return sid.Equals(other);
        }
        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified <see cref="int"/>.
        /// </summary>
        /// <param name="other">A 32-bit signed integer to compare to this instance.</param>
        /// <returns>True if the relative values are equal, otherwise returns false.</returns>
        public bool Equals(int other)
        {
            return Equals((uint)other);
        }
        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified <see cref="StringId"/>.
        /// </summary>
        /// <param name="other">A 32-bit <see cref="StringId"/> to compare to this instance.</param>
        /// <returns>True if the relative values are equal, otherwise returns false.</returns>
        public bool Equals(StringId other)
        {
            return Equals(other.sid);
        }
        /// <summary>
        /// Converts the value of this instance to its equivalent string representation.
        /// </summary>
        public override string ToString()
        {
            return string.Format("string[{0}] Length: {1}", Index, Length);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagId"></param>
        public static implicit operator uint(StringId tagId)
        {
            return tagId.sid;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagId"></param>
        public static implicit operator int(StringId tagId)
        {
            return (int)(tagId.sid);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sid"></param>
        public static implicit operator StringId(uint sid)
        {
            return new StringId() { sid = sid };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sid"></param>
        public static implicit operator StringId(int sid)
        {
            return new StringId() { sid = (uint)(sid) };
        }
    }
}
