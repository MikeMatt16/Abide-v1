using System;
using System.Runtime.InteropServices;

namespace Abide.HaloLibrary.Halo2Map
{
    /// <summary>
    /// Represents a 2048-byte length Halo 2 Map Header.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = Length), Serializable]
    internal struct Header
    {
        /// <summary>
        /// Represents the length of a <see cref="Header"/> structure in bytes.
        /// This value is constant.
        /// </summary>
        public const int Length = 2048;
        /// <summary>
        /// Represents the version of a Halo 2 map.
        /// This value is constant and readonly.
        /// </summary>
        public const int Halo2MapVersion = 8;
        /// <summary>
        /// Represents the build number of a Halo 2 map.
        /// This value is constant and readonly.
        /// </summary>
        public const string Halo2MapBuild = "02.09.27.09809";

        /// <summary>
        /// Gets or sets the header's header tag.
        /// </summary>
        public Tag HeaderTag
        {
            get { return headerTag; }
            set { headerTag = value; }
        }
        /// <summary>
        /// Gets or sets the header's version value.
        /// </summary>
        public int Version
        {
            get { return version; }
            set { version = value; }
        }
        /// <summary>
        /// Gets or sets the header's file length value.
        /// </summary>
        public int FileLength
        {
            get { return fileLength; }
            set { fileLength = value; }
        }
        /// <summary>
        /// Gets or sets the header's index table offset.
        /// </summary>
        public int IndexOffset
        {
            get { return indexOffset; }
            set { indexOffset = value; }
        }
        /// <summary>
        /// Gets or sets the header's index length value.
        /// </summary>
        public int IndexLength
        {
            get { return indexLength; }
            set { indexLength = value; }
        }
        /// <summary>
        /// Gets or sets the header's tag data length value.
        /// </summary>
        public int TagDataLength
        {
            get { return metaLength; }
            set { metaLength = value; }
        }
        /// <summary>
        /// Gets or sets the non-raw data length value.
        /// </summary>
        public int NonRawLength
        {
            get { return nonRawLength; }
            set { nonRawLength = value; }
        }
        /// <summary>
        /// Gets or sets the header's map origin string.
        /// </summary>
        public string Origin
        {
            get { return new string(origin).Trim('\0'); }
            set { value.PadRight(256, '\0').CopyTo(0, origin, 0, 256); }
        }
        /// <summary>
        /// Gets or sets the header's build string.
        /// </summary>
        public string Build
        {
            get { return new string(build).Trim('\0'); }
            set { value.PadRight(32, '\0').CopyTo(0, build, 0, 32); }
        }
        /// <summary>
        /// Gets or sets the headers's crazy offset.
        /// </summary>
        public int CrazyOffset
        {
            get { return crazyOffset; }
            set { crazyOffset = value; }
        }
        /// <summary>
        /// Gets or sets the headers's crazy length.
        /// </summary>
        public int CrazyLength
        {
            get { return crazyLength; }
            set { crazyLength = value; }
        }
        /// <summary>
        /// Gets or sets the header's 128-length strings offset.
        /// </summary>
        public int Strings128Offset
        {
            get { return strings128Offset; }
            set { strings128Offset = value; }
        }
        /// <summary>
        /// Gets or sets the header's string count.
        /// </summary>
        public int StringCount
        {
            get { return stringCount; }
            set { stringCount = value; }
        }
        /// <summary>
        /// Gets or sets the header's string table length.
        /// </summary>
        public int StringsLength
        {
            get { return stringsLength; }
            set { stringsLength = value; }
        }
        /// <summary>
        /// Gets or sets the header's string index offset.
        /// </summary>
        public int StringsIndexOffset
        {
            get { return stringsIndexOffset; }
            set { stringsIndexOffset = value; }
        }
        /// <summary>
        /// Gets or sets the header's string table offset.
        /// </summary>
        public int StringsOffset
        {
            get { return stringsOffset; }
            set { stringsOffset = value; }
        }
        /// <summary>
        /// Gets or sets the map's name.
        /// </summary>
        public string Name
        {
            get { return new string(name).Trim('\0'); }
            set { value.PadRight(32, '\0').CopyTo(0, name, 0, 32); }
        }
        /// <summary>
        /// Gets or sets the map's scenario path.
        /// </summary>
        public string ScenarioPath
        {
            get { return new string(scenarioPath).Trim('\0'); }
            set { value.PadRight(256, '\0').CopyTo(0, scenarioPath, 0, 256); }
        }
        /// <summary>
        /// Gets or sets the file count.
        /// </summary>
        public int FileCount
        {
            get { return fileCount; }
            set { fileCount = value; }
        }
        /// <summary>
        /// Gets or sets the file table offset.
        /// </summary>
        public int FilesOffset
        {
            get { return filesOffset; }
            set { filesOffset = value; }
        }
        /// <summary>
        /// Gets or sets the file table index offset.
        /// </summary>
        public int FilesIndex
        {
            get { return filesIndex; }
            set { filesIndex = value; }
        }
        /// <summary>
        /// Gets or sets the file table length.
        /// </summary>
        public int FilesLength
        {
            get { return fileLength; }
            set { filesLength = value; }
        }
        /// <summary>
        /// Gets or sets the header's signature.
        /// </summary>
        public int Signature
        {
            get { return signature; }
            set { signature = value; }
        }
        /// <summary>
        /// Gets or sets the header's footer tag.
        /// </summary>
        public Tag FooterTag
        {
            get { return footerTag; }
            set { footerTag = value; }
        }
        
        private Tag headerTag;
        private int version;
        private int fileLength;
        private int zero_1;
        private int indexOffset;
        private int indexLength;
        private int metaLength;
        private int nonRawLength;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        private char[] origin;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        private char[] build;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        private byte[] unknown_3;
        private int unknown_4;
        private int crazyOffset;
        private int crazyLength;
        private int strings128Offset;
        private int stringCount;
        private int stringsLength;
        private int stringsIndexOffset;
        private int stringsOffset;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 36)]
        private byte[] unknown_6;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        private char[] name;
        private int zero_2;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        private char[] scenarioPath;
        private int unknown_7;
        private int fileCount;
        private int filesOffset;
        private int filesLength;
        private int filesIndex;
        private int signature;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1320)]
        private byte[] zero_4;
        private Tag footerTag;

        /// <summary>
        /// Creates a generic Halo 2 map header structure.
        /// </summary>
        /// <returns>A generic Halo 2 map header.</returns>
        public static Header Create()
        {
            //Create
            Header head = new Header();
            head.headerTag = HaloTags.head;
            head.origin = new char[256];
            head.build = new char[32];
            head.unknown_3 = new byte[20];
            head.unknown_6 = new byte[36];
            head.name = new char[32];
            head.scenarioPath = new char[256];
            head.zero_4 = new byte[1320];
            head.footerTag = HaloTags.foot;

            //Setup Properties
            head.Version = Halo2MapVersion;
            head.Build = Halo2MapBuild;
            head.IndexLength = 4096;
            head.IndexOffset = 2048;
            
            //Return
            return head;
        }
    }
}