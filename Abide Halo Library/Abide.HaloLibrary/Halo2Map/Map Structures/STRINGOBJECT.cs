using System;
using System.Runtime.InteropServices;

namespace Abide.HaloLibrary.Halo2Map
{
    /// <summary>
    /// Represents a 64-bit Halo Map string object.
    /// </summary>
    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct STRINGOBJECT
    {
        /// <summary>
        /// Represents the length of a <see cref="STRINGOBJECT"/> structure in bytes.
        /// This value is constant and readonly.
        /// </summary>
        public const int Length = 8;

        /// <summary>
        /// Gets or sets the string ID of this string object.
        /// </summary>
        public STRINGID StringID
        {
            get { return sid; }
            set { sid = value; }
        }
        /// <summary>
        /// Gets or sets the length of this string object.
        /// </summary>
        public int Offset
        {
            get { return offset; }
            set { offset = value; }
        }
        
        private STRINGID sid;
        private int offset;

        /// <summary>
        /// Initializes a new <see cref="STRINGOBJECT"/> structure using the supplied string ID and length values.
        /// </summary>
        /// <param name="sid">The string ID of the string object.</param>
        /// <param name="offset">The offset of the string.</param>
        public STRINGOBJECT(STRINGID sid, int offset)
        {
            this.sid = sid;
            this.offset = offset;
        }

        /// <summary>
        /// Returns a string representation of the structure.
        /// </summary>
        /// <returns>A string representation of this structure containing the string ID and offset.</returns>
        public override string ToString()
        {
            return string.Format("{0} Offset: {1}", sid, offset);
        }
    }
}
