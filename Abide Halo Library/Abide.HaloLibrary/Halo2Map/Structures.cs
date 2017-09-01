using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Abide.HaloLibrary.Halo2Map
{
    /// <summary>
    /// Represents a Halo 2 map file header.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = Length), Serializable]
    public unsafe struct Header
    {
        /// <summary>
        /// Gets and returns the length of a <see cref="Header"/> structure in bytes.
        /// This value is constant.
        /// </summary>
        public const int Length = 2048;

        /// <summary>
        /// Gets or sets the head four-character code.
        /// </summary>
        public Tag Head
        {
            get { return head; }
            set { head = value; }
        }
        /// <summary>
        /// Gets or sets the map's version number.
        /// </summary>
        public uint Version
        {
            get { return version; }
            set { version = value; }
        }
        /// <summary>
        /// Gets or sets the length of the map file.
        /// </summary>
        public uint FileLength
        {
            get { return fileLength; }
            set { fileLength = value; }
        }
        /// <summary>
        /// Gets or sets the offset of the map's index table.
        /// </summary>
        public uint IndexOffset
        {
            get { return indexOffset; }
            set { indexOffset = value; }
        }
        /// <summary>
        /// Gets or sets the length map's index table.
        /// </summary>
        public uint IndexLength
        {
            get { return indexLength; }
            set { indexLength = value; }
        }
        /// <summary>
        /// Gets or sets the length of the map's tag data.
        /// </summary>
        public uint TagDataLength
        {
            get { return tagDataLength; }
            set { tagDataLength = value; }
        }
        /// <summary>
        /// Gets or sets the length of the map's data.
        /// This value is the sum of map's index length, the map's bsp allocation length, and the map's tag data length. 
        /// </summary>
        public uint MapDataLength
        {
            get { return nonRawLength; }
            set { nonRawLength = value; }
        }
        /// <summary>
        /// Gets or sets the origin of the map file.
        /// </summary>
        public string Origin
        {
            get { fixed (sbyte* buffer = origin) return new string(buffer); }
            set
            {
                string origin = value.PadRight(256, '\0');
                fixed (sbyte* buffer = this.origin)
                {
                    sbyte* pb = buffer;
                    for (int i = 0; i < 256; i++)
                    {
                        *pb = (sbyte)origin[i];
                        pb++;
                    }
                }
            }
        }
        /// <summary>
        /// Gets or sets the map's build.
        /// </summary>
        public string Build
        {
            get { fixed (sbyte* buffer = build) return new string(buffer); }
            set
            {
                string build = value.PadRight(32, '\0');
                fixed (sbyte* buffer = this.build)
                {
                    sbyte* pb = buffer;
                    for (int i = 0; i < 32; i++)
                    {
                        *pb = (sbyte)build[i];
                        pb++;
                    }
                }
            }
        }
        /// <summary>
        /// Gets or sets the offset of the map's crazy data.
        /// </summary>
        public uint CrazyOffset
        {
            get { return crazyOffset; }
            set { crazyOffset = value; }
        }
        /// <summary>
        /// Gets or sets the length of the map's crazy data.
        /// </summary>
        public uint CrazyLength
        {
            get { return crazyLength; }
            set { crazyLength = value; }
        }
        /// <summary>
        /// Gets or sets the offset of the map's 128-byte length strings.
        /// </summary>
        public uint Strings128Offset
        {
            get { return strings128Offset; }
            set { strings128Offset = value; }
        }
        /// <summary>
        /// Gets or sets the number of strings in the map.
        /// </summary>
        public uint StringCount
        {
            get { return stringCount; }
            set { stringCount = value; }
        }
        /// <summary>
        /// Gets or sets the length of the map's strings table.
        /// </summary>
        public uint StringsLength
        {
            get { return stringsLength; }
            set { stringsLength = value; }
        }
        /// <summary>
        /// Gets or sets the offset of the map's strings index table.
        /// </summary>
        public uint StringsIndexOffset
        {
            get { return stringsIndexOffset; }
            set { stringsIndexOffset = value; }
        }
        /// <summary>
        /// Gets or sets the offset of the map's strings table.
        /// </summary>
        public uint StringsOffset
        {
            get { return stringsOffset; }
            set { stringsOffset = value; }
        }
        /// <summary>
        /// Gets or sets the map's name.
        /// </summary>
        public string Name
        {
            get { fixed (sbyte* buffer = name) return new string(buffer); }
            set
            {
                string name = value.PadRight(32, '\0');
                fixed (sbyte* buffer = this.name)
                {
                    sbyte* pb = buffer;
                    for (int i = 0; i < 32; i++)
                    {
                        *pb = (sbyte)name[i];
                        pb++;
                    }
                }
            }
        }
        /// <summary>
        /// Gets or sets the map's scenario path.
        /// </summary>
        public string ScenarioPath
        {
            get { fixed (sbyte* buffer = scenarioPath) return new string(buffer); }
            set
            {
                string scenarioPath = value.PadRight(256, '\0');
                fixed (sbyte* buffer = this.scenarioPath)
                {
                    sbyte* pb = buffer;
                    for (int i = 0; i < 256; i++)
                    {
                        *pb = (sbyte)scenarioPath[i];
                        pb++;
                    }
                }
            }
        }
        /// <summary>
        /// Gets or sets the number of files in the map.
        /// </summary>
        public uint FileNameCount
        {
            get { return fileNameCount; }
            set { fileNameCount = value; }
        }
        /// <summary>
        /// Gets or sets the offset of the map's file names table.
        /// </summary>
        public uint FileNamesOffset
        {
            get { return fileNamesOffset; }
            set { fileNamesOffset = value; }
        }
        /// <summary>
        /// Gets or sets the length of the map's file names table.
        /// </summary>
        public uint FileNamesLength
        {
            get { return fileNamesLength; }
            set { fileNamesLength = value; }
        }
        /// <summary>
        /// Gets or sets the offset of the map's file names index table.
        /// </summary>
        public uint FileNamesIndexOffset
        {
            get { return fileNamesIndexOffset; }
            set { fileNamesIndexOffset = value; }
        }
        /// <summary>
        /// Gets or sets the map's checksum.
        /// This value is calculating by XORing every byte past offset 2048.
        /// </summary>
        public uint Checksum
        {
            get { return checksum; }
            set { checksum = value; }
        }
        /// <summary>
        /// Gets or sets the foot four-character code.
        /// </summary>
        public Tag Foot
        {
            get { return foot; }
            set { foot = value; }
        }

        [FieldOffset(0)]
        private Tag head;
        [FieldOffset(4)]
        private uint version;
        [FieldOffset(8)]
        private uint fileLength;
        [FieldOffset(16)]
        private uint indexOffset;
        [FieldOffset(20)]
        private uint indexLength;
        [FieldOffset(24)]
        private uint tagDataLength;
        [FieldOffset(28)]
        private uint nonRawLength;
        [FieldOffset(32)]
        private fixed sbyte origin[256];
        [FieldOffset(288)]
        private fixed sbyte build[32];
        [FieldOffset(344)]
        private uint crazyOffset;
        [FieldOffset(348)]
        private uint crazyLength;
        [FieldOffset(352)]
        private uint strings128Offset;
        [FieldOffset(356)]
        private uint stringCount;
        [FieldOffset(360)]
        private uint stringsLength;
        [FieldOffset(364)]
        private uint stringsIndexOffset;
        [FieldOffset(368)]
        private uint stringsOffset;
        [FieldOffset(408)]
        private fixed sbyte name[32];
        [FieldOffset(444)]
        private fixed sbyte scenarioPath[256];
        [FieldOffset(704)]
        private uint fileNameCount;
        [FieldOffset(708)]
        private uint fileNamesOffset;
        [FieldOffset(712)]
        private uint fileNamesLength;
        [FieldOffset(716)]
        private uint fileNamesIndexOffset;
        [FieldOffset(720)]
        private uint checksum;
        [FieldOffset(2044)]
        private Tag foot;

        /// <summary>
        /// Creates a <see cref="Header"/> structure instance with pre-defined values.
        /// </summary>
        /// <returns>A <see cref="Header"/> instance.</returns>
        public static Header CreateDefault()
        {
            Header header = new Header();
            header.Head = "head";
            header.Version = 8;
            header.Build = "02.09.27.09809";
            header.IndexOffset = 2048;
            header.IndexLength = 4096;
            header.Foot = "foot";
            return header;
        }
    }

    /// <summary>
    /// Represents a Halo 2 index table header.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 32)]
    internal unsafe struct Index
    {
        /// <summary>
        /// Represents the address of the index table in Xbox memory.
        /// This value is constant.
        /// </summary>
        public const uint IndexMemoryAddress = 2147880992;
        /// <summary>
        /// Represents the length of an <see cref="Index"/> structure in bytes.
        /// This value is constant.
        /// </summary>
        public const int Length = 32;

        /// <summary>
        /// Gets or sets the memory address of the index.
        /// This value should remain consistant across all maps.
        /// </summary>
        public uint IndexAddress
        {
            get { return indexAddress; }
            set { indexAddress = value; }
        }
        /// <summary>
        /// Gets or sets the number of tag hierarchy structures in the map's index table.
        /// </summary>
        public uint TagCount
        {
            get { return tagCount; }
            set { tagCount = value; }
        }
        /// <summary>
        /// Gets or sets the offset to the map's object entries.
        /// </summary>
        public uint ObjectsOffset
        {
            get { return objectsOffset; }
            set { objectsOffset = value; }
        }
        /// <summary>
        /// Gets or sets the map's scenario ID.
        /// </summary>
        public TagId ScenarioId
        {
            get { return scenarioId; }
            set { scenarioId = value; }
        }
        /// <summary>
        /// Gets or sets the map's globals ID.
        /// </summary>
        public TagId GlobalsId
        {
            get { return globalsId; }
            set { globalsId = value; }
        }
        /// <summary>
        /// Gets or sets the number of object entries in the map's index table.
        /// </summary>
        public uint ObjectCount
        {
            get { return objectCount; }
            set { objectCount = value; }
        }
        /// <summary>
        /// Gets or sets the index table's tags four character-code.
        /// </summary>
        public Tag Tags
        {
            get { return tags; }
            set { tags = value; }
        }
        
        [FieldOffset(0)]
        private uint indexAddress;
        [FieldOffset(4)]
        private uint tagCount;
        [FieldOffset(8)]
        private uint objectsOffset;
        [FieldOffset(12)]
        private TagId scenarioId;
        [FieldOffset(16)]
        private TagId globalsId;
        [FieldOffset(24)]
        private uint objectCount;
        [FieldOffset(28)]
        private Tag tags;

        /// <summary>
        /// Creates an <see cref="Index"/> structure instance with pre-defined values.
        /// </summary>
        /// <returns>An <see cref="Index"/> instance.</returns>
        public static Index CreateDefault()
        {
            Index index = new Index();
            index.IndexAddress = IndexMemoryAddress;
            index.ScenarioId = TagId.Null;
            index.GlobalsId = TagId.Null;
            index.Tags = "tags";
            return index;
        }
    }

    /// <summary>
    /// Represents a 64-bit Halo Map string object.
    /// </summary>
    [StructLayout(LayoutKind.Sequential), Serializable]
    internal struct StringObject
    {
        /// <summary>
        /// Represents the length of a <see cref="StringObject"/> structure in bytes.
        /// This value is constant and readonly.
        /// </summary>
        public const int Length = 8;

        /// <summary>
        /// Gets or sets the string ID of this string object.
        /// </summary>
        public StringId StringID
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

        private StringId sid;
        private int offset;

        /// <summary>
        /// Initializes a new <see cref="StringObject"/> structure using the supplied string ID and length values.
        /// </summary>
        /// <param name="sid">The string ID of the string object.</param>
        /// <param name="offset">The offset of the string.</param>
        public StringObject(StringId sid, int offset)
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

    /// <summary>
    /// Represents an object entry.
    /// </summary>
    public struct ObjectEntry
    {
        /// <summary>
        /// Represents the length of an <see cref="ObjectEntry"/> structure in bytes.
        /// </summary>
        public const int Length = 16;

        /// <summary>
        /// The object entry's tag.
        /// </summary>
        public Tag Tag
        {
            get { return tag; }
            set { tag = value; }
        }
        /// <summary>
        /// The object entry's tag identifier.
        /// </summary>
        public TagId Id
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        /// The object entry's offset.
        /// </summary>
        public uint Offset
        {
            get { return offset; }
            set { offset = value; }
        }
        /// <summary>
        /// The object entry's size.
        /// </summary>
        public uint Size
        {
            get { return size; }
            set { size = value; }
        }

        private Tag tag;
        private TagId id;
        private uint offset;
        private uint size;

        /// <summary>
        /// Returns a string representation of this instance.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"{tag} 0x{id.Dword.ToString("X8")} {offset} {size}";
        }
    }

    /// <summary>
    /// Represents a tag hierarchy structure.
    /// </summary>
    public struct TagHierarchy
    {
        /// <summary>
        /// Represents the length of a <see cref="TagHierarchy"/> structure in bytes.
        /// </summary>
        public const int Length = 12;

        /// <summary>
        /// The root of the tag hierarchy.
        /// </summary>
        public Tag Root
        {
            get { return root; }
            set { root = value; }
        }
        /// <summary>
        /// The parent of the tag hierarchy.
        /// </summary>
        public Tag Parent
        {
            get { return parent; }
            set { parent = value; }
        }
        /// <summary>
        /// The class of the tag hierarchy.
        /// </summary>
        public Tag Class
        {
            get { return @class; }
            set { @class = value; }
        }

        private Tag root, parent, @class;

        /// <summary>
        /// Initializes a new <see cref="TagHierarchy"/> instance using the supplied root, parent, and class tags.
        /// </summary>
        /// <param name="root">The root tag.</param>
        /// <param name="parent">The parent tag.</param>
        /// <param name="class">The tag's class.</param>
        public TagHierarchy(Tag root, Tag parent, Tag @class)
        {
            this.root = root;
            this.parent = parent;
            this.@class = @class;
        }
        /// <summary>
        /// Gets a string representation of this <see cref="TagHierarchy"/> instance.
        /// </summary>
        /// <returns>A string representation of this instance.</returns>
        public override string ToString()
        {
            return $"{@class}.{parent}.{root}";
        }
    }
    
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
