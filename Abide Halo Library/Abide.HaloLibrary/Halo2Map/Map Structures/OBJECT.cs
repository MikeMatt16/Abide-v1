using System;
using System.Runtime.InteropServices;

namespace Abide.HaloLibrary.Halo2Map
{
    /// <summary>
    /// Represents a 16-byte length Halo 2 Object Entry.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = Length), Serializable]
    public struct Object
    {
        /// <summary>
        /// Represents the length of an <see cref="Object"/> structure in bytes.
        /// This value is constant.
        /// </summary>
        public const int Length = 16;
        /// <summary>
        /// Represents an empty <see cref="Object"/> structure.
        /// </summary>
        public static readonly Object Empty = new Object() { tag = new Tag(), id = 0, offset = 0, length = 0 };

        /// <summary>
        /// The object's tag.
        /// </summary>
        public Tag Tag
        {
            get { return tag; }
            set { tag = value; }
        }
        /// <summary>
        /// The object's tag identifier.
        /// </summary>
        public TagId ID
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        /// The object's offset.
        /// </summary>
        public uint Offset
        {
            get { return offset; }
            set { offset = value; }
        }
        /// <summary>
        /// The object's size.
        /// </summary>
        public uint Size
        {
            get { return length; }
            set { length = value; }
        }
        
        private Tag tag;
        private TagId id;
        private uint offset;
        private uint length;

        /// <summary>
        /// Returns a string representation of this structure.
        /// </summary>
        /// <returns>A string representation of the structure.</returns>
        public override string ToString()
        {
            return string.Format("{0} 0x{1:X8}", Tag, id);
        }
    }
}