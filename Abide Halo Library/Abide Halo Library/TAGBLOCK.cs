using System;
using System.Runtime.InteropServices;

namespace AbideHaloLibrary
{
    /// <summary>
    /// Represents a 64-bit tag_block reference.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TAGBLOCK : IComparable<TAGBLOCK>, IComparable<long>, IComparable<ulong>, IEquatable<TAGBLOCK>, IEquatable<long>, IEquatable<ulong>
    {
        /// <summary>
        /// Represents a zero-valued <see cref="TAGBLOCK"/> structure.
        /// </summary>
        public static readonly TAGBLOCK Zero = new TAGBLOCK() { count = 0, offset = 0 };
        /// <summary>
        /// Gets or sets the array length.
        /// </summary>
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        /// <summary>
        /// Gets or sets the offset to the array.
        /// </summary>
        public int Offset
        {
            get { return offset; }
            set { offset = value; }
        }
        /// <summary>
        /// Gets or sets a 64-bit unsigned integer containing this tag block's data.
        /// </summary>
        public ulong Qword
        {
            get
            {
                long count = this.count;
                long pointer = this.offset;
                return (ulong)((pointer << 32) | count);
            }
            set
            {
                count = (int)(value & 0xFFFFFFFF);
                offset = (int)((value >> 32) & 0xFFFFFFFF);
            }
        }

        private int count;
        private int offset;

        /// <summary>
        /// Initializes a new <see cref="TAGBLOCK"/> structure with the specified count and pointer values.
        /// </summary>
        /// <param name="count">The amount of blocks in the array.</param>
        /// <param name="pointer">The pointer to the array.</param>
        public TAGBLOCK(int count, int pointer)
        {
            this.count = count;
            this.offset = pointer;
        }

        /// <summary>
        /// Translates the tag block's memory-addressed pointer using a supplied value.
        /// </summary>
        /// <param name="memoryAddress">The base memory-address.</param>
        /// <returns>A translated pointer address.</returns>
        public long Translate(int memoryAddress)
        {
            return offset - memoryAddress;
        }
        public int CompareTo(ulong other)
        {
            return Qword.CompareTo(other);
        }
        public int CompareTo(long other)
        {
            return Qword.CompareTo((ulong)other);
        }
        public int CompareTo(TAGBLOCK other)
        {
            return Qword.CompareTo(other.Qword);
        }

        public bool Equals(ulong other)
        {
            return Qword.Equals(other);
        }
        public bool Equals(long other)
        {
            return Qword.Equals((ulong)other);
        }
        public bool Equals(TAGBLOCK other)
        {
            return Qword.Equals(other.Qword);
        }

        public override string ToString()
        {
            return string.Format("Count: {0} Pointer: {1}", count, offset);
        }

        public static implicit operator ulong(TAGBLOCK reference)
        {
            return reference.Qword;
        }
        public static implicit operator long(TAGBLOCK reference)
        {
            return (long)reference.Qword;
        }
        public static implicit operator TAGBLOCK(ulong value)
        {
            int count = (int)(value & 0xFFFFFFFF);
            int pointer = (int)((value >> 32) & 0xFFFFFFFF);
            return new TAGBLOCK() { count = count, offset = pointer };
        }
        public static implicit operator TAGBLOCK(long value)
        {
            int count = (int)(value & 0xFFFFFFFF);
            int pointer = (int)((value >> 32) & 0xFFFFFFFF);
            return new TAGBLOCK() { count = count, offset = pointer };
        }
    }
}
