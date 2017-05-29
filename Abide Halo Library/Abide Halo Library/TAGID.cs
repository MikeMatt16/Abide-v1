using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace AbideHaloLibrary
{
    /// <summary>
    /// Represents a 32-bit halo map tag identifier.
    /// </summary>
    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct TAGID : IComparable<TAGID>, IComparable<int>, IComparable<uint>, IEquatable<TAGID>, IEquatable<int>, IEquatable<uint>
    {
        /// <summary>
        /// Represents a nulled (or empty) tag identifier.
        /// </summary>
        public static readonly TAGID Null = 0xFFFFFFFF;
        /// <summary>
        /// Represents the first tag identifier in a Halo map.
        /// </summary>
        public static readonly TAGID First = 0xE1740000;

        /// <summary>
        /// Returns <see cref="true"/> if the identifier is <see cref="null"/> or <see cref="false"/> if it is not.
        /// </summary>
        public bool IsNull
        {
            get { return id == 0xffffffff; }
        }
        /// <summary>
        /// Gets or sets the identifier as a 32-bit unsigned integer value.
        /// </summary>
        public uint Dword
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        /// Gets or sets the identifier as a 32-bit signed integer value.
        /// </summary>
        public int ID
        {
            get { return (int)id; }
            set { id = (uint)value; }
        }
        /// <summary>
        /// Gets or sets the low 16-bit integer value of this instance.
        /// </summary>
        public ushort LowWord
        {
            get { return Convert.ToUInt16(id & 0xFFFF); }
            set { id = Convert.ToUInt32((HighWord << 16 | value) & uint.MaxValue); }
        }
        /// <summary>
        /// Gets or sets the high 16-bit integer value of this instance;
        /// </summary>
        public ushort HighWord
        {
            get { return Convert.ToUInt16(id >> 16 & 0xFFFF); }
            set { id = Convert.ToUInt32((value << 16 | LowWord) & uint.MaxValue); }
        }

        private uint id;

        /// <summary>
        /// Converts the string representation of a <see cref="TAGID"/> in <see cref="NumberStyles.HexNumber"/> style to it's <see cref="TAGID"/> equivalent.
        /// </summary>
        /// <param name="s">A string that represents the <see cref="NumberStyles.HexNumber"/> to convert.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="OverflowException"></exception>
        public static TAGID Parse(string s)
        {
            return uint.Parse(s, NumberStyles.HexNumber);
        }
        /// <summary>
        /// Increments the tag identifier of this structure by 1.
        /// </summary>
        public void Increment()
        {
            Increment(1);
        }
        /// <summary>
        /// Decrements the tag identifier of this structure by 1.
        /// </summary>
        public void Decrement()
        {
            Decrement(1);
        }
        /// <summary>
        /// Increments the tag identifier of this structure by a specified amount.
        /// </summary>
        /// <param name="value">The amount to increment.</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void Increment(int value)
        {
            //Get high and low values
            uint high = HighWord;
            uint low = LowWord;
            high += Convert.ToUInt32(value);
            low += Convert.ToUInt32(value);

            //Check...
            if (high > ushort.MaxValue || high < ushort.MinValue)
                throw new InvalidOperationException();
            else if (low > ushort.MaxValue || low < ushort.MinValue)
                throw new InvalidOperationException();

            //Set ID
            id = Convert.ToUInt32((high << 16 | low) & uint.MaxValue);
        }
        /// <summary>
        /// Decrements the tag identifier of this structure by a specified amount.
        /// </summary>
        /// <param name="value">The amount to decrement.</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void Decrement(int value)
        {
            //Get high and low values
            uint high = HighWord;
            uint low = LowWord;
            high -= Convert.ToUInt32(value);
            low -= Convert.ToUInt32(value);

            //Check...
            if (high > ushort.MaxValue || high < ushort.MinValue)
                throw new InvalidOperationException();
            else if (low > ushort.MaxValue || low < ushort.MinValue)
                throw new InvalidOperationException();

            //Set ID
            id = Convert.ToUInt32((high << 16 | low) & uint.MaxValue);
        }
        /// <summary>
        /// Compares this instance to a 32-bit unsigned integer and returns an indication of their relative values.
        /// </summary>
        /// <param name="other">A 32-bit unsigned integer to comapre to.</param>
        /// <returns></returns>
        public int CompareTo(uint other)
        {
            return id.CompareTo(other);
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
        /// Compares this instance to a 32-bit <see cref="TAGID"/> structure and returns an indication of their relative values.
        /// </summary>
        /// <param name="other">A 32-bit <see cref="TAGID"/> to comapre to.</param>
        public int CompareTo(TAGID other)
        {
            return CompareTo(other.id);
        }
        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified <see cref="uint"/>.
        /// </summary>
        /// <param name="other">A 32-bit unsigned integer to compare to this instance.</param>
        /// <returns>True if the relative values are equal, otherwise returns false.</returns>
        public bool Equals(uint other)
        {
            return id.Equals(other);
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
        /// Returns a value indicating whether this instance is equal to a specified <see cref="TAGID"/>.
        /// </summary>
        /// <param name="other">A 32-bit <see cref="TAGID"/> to compare to this instance.</param>
        /// <returns>True if the relative values are equal, otherwise returns false.</returns>
        public bool Equals(TAGID other)
        {
            return Equals(other.id);
        }
        /// <summary>
        /// Converts the value of this instance to its equivalent string representation.
        /// </summary>
        public override string ToString()
        {
            return id.ToString("X8");
        }

        public static implicit operator uint(TAGID tagId)
        {
            return tagId.id;
        }
        public static implicit operator int(TAGID tagId)
        {
            return (int)(tagId.id);
        }
        public static implicit operator TAGID(uint id)
        {
            return new TAGID() { id = id };
        }
        public static implicit operator TAGID(int id)
        {
            return new TAGID() { id = (uint)(id) };
        }
    }
}
