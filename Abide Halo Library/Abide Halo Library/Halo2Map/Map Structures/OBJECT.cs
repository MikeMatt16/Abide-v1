using System;
using System.Runtime.InteropServices;

namespace AbideHaloLibrary.Halo2Map
{
    /// <summary>
    /// Represents a 16-byte length Halo 2 Object Entry.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = Length), Serializable]
    public struct OBJECT
    {
        /// <summary>
        /// Represents the length of an <see cref="OBJECT"/> structure in bytes.
        /// This value is constant.
        /// </summary>
        public const int Length = 16;
        /// <summary>
        /// Represents an empty <see cref="OBJECT"/> structure.
        /// </summary>
        public static readonly OBJECT Empty = new OBJECT() { tag = new TAG(), id = 0, offset = 0, length = 0 };

        /// <summary>
        /// The object's tag.
        /// </summary>
        public TAG Tag
        {
            get { return tag; }
            set { tag = value; }
        }
        /// <summary>
        /// The object's tag identifier.
        /// </summary>
        public TAGID ID
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
        
        private TAG tag;
        private TAGID id;
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