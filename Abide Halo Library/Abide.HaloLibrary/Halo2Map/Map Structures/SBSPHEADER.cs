using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Abide.HaloLibrary.Halo2Map
{
    /// <summary>
    /// Represents a Halo 2 structure bsp tag data header.
    /// </summary>
    [StructLayout(LayoutKind.Sequential), Serializable]
    internal struct SbspHeader
    {
        /// <summary>
        /// Represents the length of a <see cref="SbspHeader"/> structure in bytes.
        /// This value is constant.
        /// </summary>
        public const int Length = 28;

        /// <summary>
        /// Gets or sets the header's tag.
        /// </summary>
        public Tag Tag
        {
            get { return tag; }
            set { tag = value; }
        }
        /// <summary>
        /// Gets or sets the header's data length.
        /// </summary>
        public int DataLength
        {
            get { return dataLength; }
            set { dataLength = value; }
        }
        /// <summary>
        /// Gets or sets the header's lightmap offset.
        /// </summary>
        public int LightmapOffset
        {
            get { return lightmapOffset; }
            set { lightmapOffset = value; }
        }
        
        private int dataLength;
        private int unknown1;
        private int lightmapOffset;
        private Tag tag;
        private TagId id;
        private int unknown2;
        private int unknown3;
    }
}
