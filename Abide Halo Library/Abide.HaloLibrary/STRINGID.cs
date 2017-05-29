using System;
using System.Runtime.InteropServices;

namespace Abide.HaloLibrary
{
    /// <summary>
    /// Represents a 32-bit Halo Map string identifier.
    /// </summary>
    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct STRINGID : IComparable<STRINGID>, IComparable<int>, IComparable<uint>, IEquatable<STRINGID>, IEquatable<int>, IEquatable<uint>
    {
        /// <summary>
        /// Represents an zero (or empty) string identifier.
        /// </summary>
        public static readonly STRINGID Zero = new STRINGID() { sid = 0x0 };

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
        /// Initializes a <see cref="STRINGID"/> structure using the supplied value.
        /// </summary>
        /// <param name="sid">The 32-bit unsigned string ID value.</param>
        public STRINGID(uint sid)
        {
            this.sid = sid;
        }
        /// <summary>
        /// Initializes a <see cref="STRINGID"/> structure using the supplied string index and string length values.
        /// </summary>
        /// <param name="index">The index of the string in the string table.</param>
        /// <param name="length">The length of the string.</param>
        public STRINGID(ushort index, byte length)
        {
            sid = Convert.ToUInt32(length << 24 | index);
        }

        /// <summary>
        /// Creates a new <see cref="STRINGID"/> structure from a supplied string and index.
        /// </summary>
        /// <param name="value">The string to use as reference.</param>
        /// <param name="index">The index of the string identifier.</param>
        /// <returns>A new <see cref="STRINGID"/> structure containing the supplied string length and index.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="index"/> it outside of valid range.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/> length is outside of valid range.</exception>
        public static STRINGID FromString(string value, int index)
        {
            //Check
            if (value == null)
                throw new ArgumentNullException("value", "String value cannot be null.");
            else if (index > ushort.MaxValue || index < 0)
                throw new ArgumentException("Index is out of possible range.", "index");
            else if (value.Length > 128)
                throw new ArgumentException("String length is out of possible range.", "value");

            //Prepare SID
            STRINGID sid = Zero;
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
        /// Compares this instance to a 32-bit <see cref="STRINGID"/> structure and returns an indication of their relative values.
        /// </summary>
        /// <param name="other">A 32-bit <see cref="STRINGID"/> to comapre to.</param>
        public int CompareTo(STRINGID other)
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
        /// Returns a value indicating whether this instance is equal to a specified <see cref="STRINGID"/>.
        /// </summary>
        /// <param name="other">A 32-bit <see cref="STRINGID"/> to compare to this instance.</param>
        /// <returns>True if the relative values are equal, otherwise returns false.</returns>
        public bool Equals(STRINGID other)
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

        public static implicit operator uint(STRINGID tagId)
        {
            return tagId.sid;
        }
        public static implicit operator int(STRINGID tagId)
        {
            return (int)(tagId.sid);
        }
        public static implicit operator STRINGID(uint sid)
        {
            return new STRINGID() { sid = sid };
        }
        public static implicit operator STRINGID(int sid)
        {
            return new STRINGID() { sid = (uint)(sid) };
        }
    }
}
