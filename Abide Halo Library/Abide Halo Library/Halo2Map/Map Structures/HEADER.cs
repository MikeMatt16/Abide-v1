using System;
using System.Runtime.InteropServices;

namespace AbideHaloLibrary.Halo2Map
{
    /// <summary>
    /// Represents a 2048-byte length Halo 2 Map Header.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = Length), Serializable]
    public struct HEADER
    {
        /// <summary>
        /// Represents the length of a <see cref="HEADER"/> structure in bytes.
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

        public TAG HeaderTag
        {
            get { return headerTag; }
        }
        public int Version
        {
            get { return version; }
            private set { version = value; }
        }
        public int FileLength
        {
            get { return fileLength; }
            set { fileLength = value; }
        }
        public int IndexOffset
        {
            get { return indexOffset; }
            set { indexOffset = value; }
        }
        public int IndexLength
        {
            get { return indexLength; }
            set { indexLength = value; }
        }
        public int MetaLength
        {
            get { return metaLength; }
            set { metaLength = value; }
        }
        public int NonRawLength
        {
            get { return nonRawLength; }
            set { nonRawLength = value; }
        }
        public string Origin
        {
            get { return new string(origin).Trim('\0'); }
            set
            {
                char[] origin = new char[256];
                for (int i = 0; i < Math.Min(origin.Length, value.Length); i++)
                    origin[i] = value[i];
                this.origin = origin;
            }
        }
        public string Build
        {
            get { return new string(build).Trim('\0'); }
            private set
            {
                char[] build = new char[32];
                for (int i = 0; i < Math.Min(build.Length, value.Length); i++)
                    build[i] = value[i];
                this.build = build;
            }
        }
        public int CrazyOffset
        {
            get { return crazyOffset; }
            set { crazyOffset = value; }
        }
        public int CrazyLength
        {
            get { return crazyLength; }
            set { crazyLength = value; }
        }
        public int Strings128Offset
        {
            get { return strings128Offset; }
            set { strings128Offset = value; }
        }
        public int StringCount
        {
            get { return stringCount; }
            set { stringCount = value; }
        }
        public int StringsLength
        {
            get { return stringsLength; }
            set { stringsLength = value; }
        }
        public int StringsIndexOffset
        {
            get { return stringsIndexOffset; }
            set { stringsIndexOffset = value; }
        }
        public int StringsOffset
        {
            get { return stringsOffset; }
            set { stringsOffset = value; }
        }
        public string Name
        {
            get { return new string(name).Trim('\0'); }
            set
            {
                char[] name = new char[32];
                for (int i = 0; i < Math.Min(name.Length, value.Length); i++)
                    name[i] = value[i];
                this.name = name;
            }
        }
        public string ScenarioPath
        {
            get { return new string(scenarioPath).Trim('\0'); }
            set
            {
                char[] scenarioPath = new char[256];
                for (int i = 0; i < Math.Min(scenarioPath.Length, value.Length); i++)
                    scenarioPath[i] = value[i];
                this.scenarioPath = scenarioPath;
            }
        }
        public int FileCount
        {
            get { return fileCount; }
            set { fileCount = value; }
        }
        public int FilesOffset
        {
            get { return filesOffset; }
            set { filesOffset = value; }
        }
        public int FilesIndex
        {
            get { return filesIndex; }
            set { filesIndex = value; }
        }
        public int FilesLength
        {
            get { return fileLength; }
            set { filesLength = value; }
        }
        public int Signature
        {
            get { return signature; }
            set { signature = value; }
        }
        public TAG FooterTag
        {
            get { return footerTag; }
        }
        
        private TAG headerTag;
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
        private TAG footerTag;

        /// <summary>
        /// Creates a generic Halo 2 map header structure.
        /// </summary>
        /// <returns>A generic Halo 2 map header.</returns>
        public static HEADER Create()
        {
            //Create
            HEADER head = new HEADER();
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