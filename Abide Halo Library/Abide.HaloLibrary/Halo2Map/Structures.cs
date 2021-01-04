using System;
using System.Runtime.InteropServices;

namespace Abide.HaloLibrary.Halo2Map
{
    /// <summary>
    /// Represents a Halo 2 map file header.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = Length), Serializable]
    public struct Header
    {
        /// <summary>
        /// Gets and returns the length of a <see cref="Header"/> structure in bytes.
        /// This value is constant.
        /// </summary>
        public const int Length = 2048;
        /// <summary>
        /// Gets and returns the required build string for the header.
        /// This value is constant.
        /// </summary>
        public const string BuildString = "02.09.27.09809";

        /// <summary>
        /// Gets or sets the head four-character code.
        /// </summary>
        public TagFourCc Head
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
        /// Gets or sets the length of the map's total tag data.
        /// This value is the sum of map's index length, bsp tag data length, tag data length. 
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
            get { return origin.String; }
            set { origin.String = value; }
        }
        /// <summary>
        /// Gets or sets the map's build.
        /// </summary>
        public string Build
        {
            get { return build.String; }
            set { build.String = value; }
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
            get { return name.String; }
            set { name.String = value; }
        }
        /// <summary>
        /// Gets or sets the map's scenario path.
        /// </summary>
        public string ScenarioPath
        {
            get { return scenarioPath.String; }
            set { scenarioPath.String = value; }
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
        public TagFourCc Foot
        {
            get { return foot; }
            set { foot = value; }
        }

        [FieldOffset(0)]
        private TagFourCc head;
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
        private String256 origin;
        [FieldOffset(288)]
        private String32 build;
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
        private String32 name;
        [FieldOffset(444)]
        private String256 scenarioPath;
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
        private TagFourCc foot;

        /// <summary>
        /// Creates a <see cref="Header"/> structure instance with pre-defined values.
        /// </summary>
        /// <returns>A <see cref="Header"/> instance.</returns>
        public static Header CreateDefault()
        {
            return new Header
            {
                Head = "head",
                Version = 8,
                Build = "02.09.27.09809",
                IndexOffset = 2048,
                IndexLength = 4096,
                Foot = "foot"
            };
        }
    }

    /// <summary>
    /// Represents a Halo 2 index table header.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 32), Serializable]
    public struct Index
    {
        /// <summary>
        /// Represents the address of the index table in Xbox memory.
        /// This value is constant.
        /// </summary>
        public const uint IndexVirtualAddress = 0x80061000;
        /// <summary>
        /// Represents the address of the tags start in the index table in Xbox memory.
        /// This value is constant.
        /// </summary>
        public const uint IndexTagsAddress = 0x80061020;
        /// <summary>
        /// Represents the address of the index table in Xbox memory.
        /// This value is constant.
        /// </summary>
        [Obsolete("IndexMemoryAddress is deprecated. Use IndexTagsAddress instead.")]
        public const uint IndexMemoryAddress = 2147880992;
        /// <summary>
        /// Represents the length of an <see cref="Index"/> structure in bytes.
        /// This value is constant.
        /// </summary>
        public const int Length = 32;

        /// <summary>
        /// Gets or sets the offset to the map's tags entries.
        /// </summary>
        public uint TagsAddress
        {
            get { return tagsOffset; }
            set { tagsOffset = value; }
        }
        /// <summary>
        /// Gets or sets the memory address of the index.
        /// This value should remain consistant across all maps.
        /// </summary>
        [Obsolete("IndexAddress is obsolete. Use TagsAddress instead.")]
        public uint IndexAddress
        {
            get { return tagsOffset; }
            set { tagsOffset = value; }
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
        public TagFourCc Tags
        {
            get { return tags; }
            set { tags = value; }
        }
        
        [FieldOffset(0)]
        private uint tagsOffset;
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
        private TagFourCc tags;

        /// <summary>
        /// Creates an <see cref="Index"/> structure instance with pre-defined values.
        /// </summary>
        /// <returns>An <see cref="Index"/> instance.</returns>
        public static Index CreateDefault()
        {
            return new Index
            {
                TagsAddress = IndexTagsAddress,
                ScenarioId = TagId.Null,
                GlobalsId = TagId.Null,
                Tags = "tags"
            };
        }
    }

    /// <summary>
    /// Represents a 64-bit Halo Map string object.
    /// </summary>
    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct StringObject
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
    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct ObjectEntry
    {
        /// <summary>
        /// Represents the length of an <see cref="ObjectEntry"/> structure in bytes.
        /// </summary>
        public const int Length = 16;

        /// <summary>
        /// The object entry's tag.
        /// </summary>
        public TagFourCc Tag
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

        private TagFourCc tag;
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
    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct TagHierarchy : IEquatable<TagHierarchy>
    {
        /// <summary>
        /// Represents the length of a <see cref="TagHierarchy"/> structure in bytes.
        /// </summary>
        public const int Length = 12;

        /// <summary>
        /// The root of the tag hierarchy.
        /// </summary>
        public TagFourCc Root
        {
            get { return root; }
            set { root = value; }
        }
        /// <summary>
        /// The parent of the tag hierarchy.
        /// </summary>
        public TagFourCc Parent
        {
            get { return parent; }
            set { parent = value; }
        }
        /// <summary>
        /// The class of the tag hierarchy.
        /// </summary>
        public TagFourCc Class
        {
            get { return @class; }
            set { @class = value; }
        }

        private TagFourCc root, parent, @class;

        /// <summary>
        /// Initializes a new <see cref="TagHierarchy"/> instance using the supplied root, parent, and class tags.
        /// </summary>
        /// <param name="root">The root tag.</param>
        /// <param name="parent">The parent tag.</param>
        /// <param name="class">The tag's class.</param>
        public TagHierarchy(TagFourCc root, TagFourCc parent, TagFourCc @class)
        {
            this.root = root;
            this.parent = parent;
            this.@class = @class;
        }
        /// <summary>
        /// Determines whether this instance and another specified <see cref="TagHierarchy"/> object have the same value.
        /// </summary>
        /// <param name="other">The tag to compare to this instance.</param>
        /// <returns>true if the value of the <paramref name="other"/> parameter is the same as the value of this instance; otherwise, false.</returns>
        public bool Equals(TagHierarchy other)
        {
            return root.Equals(other.root) && parent.Equals(other.parent) && @class.Equals(other.@class);
        }
        /// <summary>
        /// Gets a string representation of this <see cref="TagHierarchy"/> instance.
        /// </summary>
        /// <returns>A string representation of this instance.</returns>
        public override string ToString()
        {
            return $"{@class.FourCc.Replace("\0", string.Empty)}.{parent.FourCc.Replace("\0", string.Empty)}.{root.FourCc.Replace("\0", string.Empty)}";
        }
    }
    
    /// <summary>
    /// Represents a Halo 2 scenario structure BSP block header.
    /// </summary>
    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct StructureBspBlockHeader
    {
        /// <summary>
        /// Represents the length of a <see cref="StructureBspBlockHeader"/> structure in bytes.
        /// This value is constant.
        /// </summary>
        public const int Length = 16;

        /// <summary>
        /// Gets or sets the block length in bytes.
        /// </summary>
        public int BlockLength { get; set; }
        /// <summary>
        /// Gets or sets the structure BSP virtual offset.
        /// </summary>
        public uint StructureBspOffset { get; set; }
        /// <summary>
        /// Gets or sets the lightmap virtual offset.
        /// </summary>
        public uint StructureLightmapOffset { get; set; }
        /// <summary>
        /// Gets or sets the Structure BSP header tag.
        /// </summary>
        public TagFourCc StructureBsp { get; set; }
    }

    /// <summary>
    /// Represents a 256-byte length string.
    /// </summary>
    [StructLayout(LayoutKind.Sequential), Serializable]
    internal struct String256
    {
        public string String
        {
            get
            {
                byte[] buffer = new byte[] {
                this.string0,
                this.string1,
                this.string2,
                this.string3,
                this.string4,
                this.string5,
                this.string6,
                this.string7,
                this.string8,
                this.string9,
                this.string10,
                this.string11,
                this.string12,
                this.string13,
                this.string14,
                this.string15,
                this.string16,
                this.string17,
                this.string18,
                this.string19,
                this.string20,
                this.string21,
                this.string22,
                this.string23,
                this.string24,
                this.string25,
                this.string26,
                this.string27,
                this.string28,
                this.string29,
                this.string30,
                this.string31,
                this.string32,
                this.string33,
                this.string34,
                this.string35,
                this.string36,
                this.string37,
                this.string38,
                this.string39,
                this.string40,
                this.string41,
                this.string42,
                this.string43,
                this.string44,
                this.string45,
                this.string46,
                this.string47,
                this.string48,
                this.string49,
                this.string50,
                this.string51,
                this.string52,
                this.string53,
                this.string54,
                this.string55,
                this.string56,
                this.string57,
                this.string58,
                this.string59,
                this.string60,
                this.string61,
                this.string62,
                this.string63,
                this.string64,
                this.string65,
                this.string66,
                this.string67,
                this.string68,
                this.string69,
                this.string70,
                this.string71,
                this.string72,
                this.string73,
                this.string74,
                this.string75,
                this.string76,
                this.string77,
                this.string78,
                this.string79,
                this.string80,
                this.string81,
                this.string82,
                this.string83,
                this.string84,
                this.string85,
                this.string86,
                this.string87,
                this.string88,
                this.string89,
                this.string90,
                this.string91,
                this.string92,
                this.string93,
                this.string94,
                this.string95,
                this.string96,
                this.string97,
                this.string98,
                this.string99,
                this.string100,
                this.string101,
                this.string102,
                this.string103,
                this.string104,
                this.string105,
                this.string106,
                this.string107,
                this.string108,
                this.string109,
                this.string110,
                this.string111,
                this.string112,
                this.string113,
                this.string114,
                this.string115,
                this.string116,
                this.string117,
                this.string118,
                this.string119,
                this.string120,
                this.string121,
                this.string122,
                this.string123,
                this.string124,
                this.string125,
                this.string126,
                this.string127,
                this.string128,
                this.string129,
                this.string130,
                this.string131,
                this.string132,
                this.string133,
                this.string134,
                this.string135,
                this.string136,
                this.string137,
                this.string138,
                this.string139,
                this.string140,
                this.string141,
                this.string142,
                this.string143,
                this.string144,
                this.string145,
                this.string146,
                this.string147,
                this.string148,
                this.string149,
                this.string150,
                this.string151,
                this.string152,
                this.string153,
                this.string154,
                this.string155,
                this.string156,
                this.string157,
                this.string158,
                this.string159,
                this.string160,
                this.string161,
                this.string162,
                this.string163,
                this.string164,
                this.string165,
                this.string166,
                this.string167,
                this.string168,
                this.string169,
                this.string170,
                this.string171,
                this.string172,
                this.string173,
                this.string174,
                this.string175,
                this.string176,
                this.string177,
                this.string178,
                this.string179,
                this.string180,
                this.string181,
                this.string182,
                this.string183,
                this.string184,
                this.string185,
                this.string186,
                this.string187,
                this.string188,
                this.string189,
                this.string190,
                this.string191,
                this.string192,
                this.string193,
                this.string194,
                this.string195,
                this.string196,
                this.string197,
                this.string198,
                this.string199,
                this.string200,
                this.string201,
                this.string202,
                this.string203,
                this.string204,
                this.string205,
                this.string206,
                this.string207,
                this.string208,
                this.string209,
                this.string210,
                this.string211,
                this.string212,
                this.string213,
                this.string214,
                this.string215,
                this.string216,
                this.string217,
                this.string218,
                this.string219,
                this.string220,
                this.string221,
                this.string222,
                this.string223,
                this.string224,
                this.string225,
                this.string226,
                this.string227,
                this.string228,
                this.string229,
                this.string230,
                this.string231,
                this.string232,
                this.string233,
                this.string234,
                this.string235,
                this.string236,
                this.string237,
                this.string238,
                this.string239,
                this.string240,
                this.string241,
                this.string242,
                this.string243,
                this.string244,
                this.string245,
                this.string246,
                this.string247,
                this.string248,
                this.string249,
                this.string250,
                this.string251,
                this.string252,
                this.string253,
                this.string254,
                this.string255};
                return System.Text.Encoding.UTF8.GetString(buffer).Trim('\0');
            }
            set
            {
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(value.PadRight(256, '\0'));
                this.string0 = buffer[0];
                this.string1 = buffer[1];
                this.string2 = buffer[2];
                this.string3 = buffer[3];
                this.string4 = buffer[4];
                this.string5 = buffer[5];
                this.string6 = buffer[6];
                this.string7 = buffer[7];
                this.string8 = buffer[8];
                this.string9 = buffer[9];
                this.string10 = buffer[10];
                this.string11 = buffer[11];
                this.string12 = buffer[12];
                this.string13 = buffer[13];
                this.string14 = buffer[14];
                this.string15 = buffer[15];
                this.string16 = buffer[16];
                this.string17 = buffer[17];
                this.string18 = buffer[18];
                this.string19 = buffer[19];
                this.string20 = buffer[20];
                this.string21 = buffer[21];
                this.string22 = buffer[22];
                this.string23 = buffer[23];
                this.string24 = buffer[24];
                this.string25 = buffer[25];
                this.string26 = buffer[26];
                this.string27 = buffer[27];
                this.string28 = buffer[28];
                this.string29 = buffer[29];
                this.string30 = buffer[30];
                this.string31 = buffer[31];
                this.string32 = buffer[32];
                this.string33 = buffer[33];
                this.string34 = buffer[34];
                this.string35 = buffer[35];
                this.string36 = buffer[36];
                this.string37 = buffer[37];
                this.string38 = buffer[38];
                this.string39 = buffer[39];
                this.string40 = buffer[40];
                this.string41 = buffer[41];
                this.string42 = buffer[42];
                this.string43 = buffer[43];
                this.string44 = buffer[44];
                this.string45 = buffer[45];
                this.string46 = buffer[46];
                this.string47 = buffer[47];
                this.string48 = buffer[48];
                this.string49 = buffer[49];
                this.string50 = buffer[50];
                this.string51 = buffer[51];
                this.string52 = buffer[52];
                this.string53 = buffer[53];
                this.string54 = buffer[54];
                this.string55 = buffer[55];
                this.string56 = buffer[56];
                this.string57 = buffer[57];
                this.string58 = buffer[58];
                this.string59 = buffer[59];
                this.string60 = buffer[60];
                this.string61 = buffer[61];
                this.string62 = buffer[62];
                this.string63 = buffer[63];
                this.string64 = buffer[64];
                this.string65 = buffer[65];
                this.string66 = buffer[66];
                this.string67 = buffer[67];
                this.string68 = buffer[68];
                this.string69 = buffer[69];
                this.string70 = buffer[70];
                this.string71 = buffer[71];
                this.string72 = buffer[72];
                this.string73 = buffer[73];
                this.string74 = buffer[74];
                this.string75 = buffer[75];
                this.string76 = buffer[76];
                this.string77 = buffer[77];
                this.string78 = buffer[78];
                this.string79 = buffer[79];
                this.string80 = buffer[80];
                this.string81 = buffer[81];
                this.string82 = buffer[82];
                this.string83 = buffer[83];
                this.string84 = buffer[84];
                this.string85 = buffer[85];
                this.string86 = buffer[86];
                this.string87 = buffer[87];
                this.string88 = buffer[88];
                this.string89 = buffer[89];
                this.string90 = buffer[90];
                this.string91 = buffer[91];
                this.string92 = buffer[92];
                this.string93 = buffer[93];
                this.string94 = buffer[94];
                this.string95 = buffer[95];
                this.string96 = buffer[96];
                this.string97 = buffer[97];
                this.string98 = buffer[98];
                this.string99 = buffer[99];
                this.string100 = buffer[100];
                this.string101 = buffer[101];
                this.string102 = buffer[102];
                this.string103 = buffer[103];
                this.string104 = buffer[104];
                this.string105 = buffer[105];
                this.string106 = buffer[106];
                this.string107 = buffer[107];
                this.string108 = buffer[108];
                this.string109 = buffer[109];
                this.string110 = buffer[110];
                this.string111 = buffer[111];
                this.string112 = buffer[112];
                this.string113 = buffer[113];
                this.string114 = buffer[114];
                this.string115 = buffer[115];
                this.string116 = buffer[116];
                this.string117 = buffer[117];
                this.string118 = buffer[118];
                this.string119 = buffer[119];
                this.string120 = buffer[120];
                this.string121 = buffer[121];
                this.string122 = buffer[122];
                this.string123 = buffer[123];
                this.string124 = buffer[124];
                this.string125 = buffer[125];
                this.string126 = buffer[126];
                this.string127 = buffer[127];
                this.string128 = buffer[128];
                this.string129 = buffer[129];
                this.string130 = buffer[130];
                this.string131 = buffer[131];
                this.string132 = buffer[132];
                this.string133 = buffer[133];
                this.string134 = buffer[134];
                this.string135 = buffer[135];
                this.string136 = buffer[136];
                this.string137 = buffer[137];
                this.string138 = buffer[138];
                this.string139 = buffer[139];
                this.string140 = buffer[140];
                this.string141 = buffer[141];
                this.string142 = buffer[142];
                this.string143 = buffer[143];
                this.string144 = buffer[144];
                this.string145 = buffer[145];
                this.string146 = buffer[146];
                this.string147 = buffer[147];
                this.string148 = buffer[148];
                this.string149 = buffer[149];
                this.string150 = buffer[150];
                this.string151 = buffer[151];
                this.string152 = buffer[152];
                this.string153 = buffer[153];
                this.string154 = buffer[154];
                this.string155 = buffer[155];
                this.string156 = buffer[156];
                this.string157 = buffer[157];
                this.string158 = buffer[158];
                this.string159 = buffer[159];
                this.string160 = buffer[160];
                this.string161 = buffer[161];
                this.string162 = buffer[162];
                this.string163 = buffer[163];
                this.string164 = buffer[164];
                this.string165 = buffer[165];
                this.string166 = buffer[166];
                this.string167 = buffer[167];
                this.string168 = buffer[168];
                this.string169 = buffer[169];
                this.string170 = buffer[170];
                this.string171 = buffer[171];
                this.string172 = buffer[172];
                this.string173 = buffer[173];
                this.string174 = buffer[174];
                this.string175 = buffer[175];
                this.string176 = buffer[176];
                this.string177 = buffer[177];
                this.string178 = buffer[178];
                this.string179 = buffer[179];
                this.string180 = buffer[180];
                this.string181 = buffer[181];
                this.string182 = buffer[182];
                this.string183 = buffer[183];
                this.string184 = buffer[184];
                this.string185 = buffer[185];
                this.string186 = buffer[186];
                this.string187 = buffer[187];
                this.string188 = buffer[188];
                this.string189 = buffer[189];
                this.string190 = buffer[190];
                this.string191 = buffer[191];
                this.string192 = buffer[192];
                this.string193 = buffer[193];
                this.string194 = buffer[194];
                this.string195 = buffer[195];
                this.string196 = buffer[196];
                this.string197 = buffer[197];
                this.string198 = buffer[198];
                this.string199 = buffer[199];
                this.string200 = buffer[200];
                this.string201 = buffer[201];
                this.string202 = buffer[202];
                this.string203 = buffer[203];
                this.string204 = buffer[204];
                this.string205 = buffer[205];
                this.string206 = buffer[206];
                this.string207 = buffer[207];
                this.string208 = buffer[208];
                this.string209 = buffer[209];
                this.string210 = buffer[210];
                this.string211 = buffer[211];
                this.string212 = buffer[212];
                this.string213 = buffer[213];
                this.string214 = buffer[214];
                this.string215 = buffer[215];
                this.string216 = buffer[216];
                this.string217 = buffer[217];
                this.string218 = buffer[218];
                this.string219 = buffer[219];
                this.string220 = buffer[220];
                this.string221 = buffer[221];
                this.string222 = buffer[222];
                this.string223 = buffer[223];
                this.string224 = buffer[224];
                this.string225 = buffer[225];
                this.string226 = buffer[226];
                this.string227 = buffer[227];
                this.string228 = buffer[228];
                this.string229 = buffer[229];
                this.string230 = buffer[230];
                this.string231 = buffer[231];
                this.string232 = buffer[232];
                this.string233 = buffer[233];
                this.string234 = buffer[234];
                this.string235 = buffer[235];
                this.string236 = buffer[236];
                this.string237 = buffer[237];
                this.string238 = buffer[238];
                this.string239 = buffer[239];
                this.string240 = buffer[240];
                this.string241 = buffer[241];
                this.string242 = buffer[242];
                this.string243 = buffer[243];
                this.string244 = buffer[244];
                this.string245 = buffer[245];
                this.string246 = buffer[246];
                this.string247 = buffer[247];
                this.string248 = buffer[248];
                this.string249 = buffer[249];
                this.string250 = buffer[250];
                this.string251 = buffer[251];
                this.string252 = buffer[252];
                this.string253 = buffer[253];
                this.string254 = buffer[254];
                this.string255 = buffer[255];
            }
        }
        private byte string0;
        private byte string1;
        private byte string2;
        private byte string3;
        private byte string4;
        private byte string5;
        private byte string6;
        private byte string7;
        private byte string8;
        private byte string9;
        private byte string10;
        private byte string11;
        private byte string12;
        private byte string13;
        private byte string14;
        private byte string15;
        private byte string16;
        private byte string17;
        private byte string18;
        private byte string19;
        private byte string20;
        private byte string21;
        private byte string22;
        private byte string23;
        private byte string24;
        private byte string25;
        private byte string26;
        private byte string27;
        private byte string28;
        private byte string29;
        private byte string30;
        private byte string31;
        private byte string32;
        private byte string33;
        private byte string34;
        private byte string35;
        private byte string36;
        private byte string37;
        private byte string38;
        private byte string39;
        private byte string40;
        private byte string41;
        private byte string42;
        private byte string43;
        private byte string44;
        private byte string45;
        private byte string46;
        private byte string47;
        private byte string48;
        private byte string49;
        private byte string50;
        private byte string51;
        private byte string52;
        private byte string53;
        private byte string54;
        private byte string55;
        private byte string56;
        private byte string57;
        private byte string58;
        private byte string59;
        private byte string60;
        private byte string61;
        private byte string62;
        private byte string63;
        private byte string64;
        private byte string65;
        private byte string66;
        private byte string67;
        private byte string68;
        private byte string69;
        private byte string70;
        private byte string71;
        private byte string72;
        private byte string73;
        private byte string74;
        private byte string75;
        private byte string76;
        private byte string77;
        private byte string78;
        private byte string79;
        private byte string80;
        private byte string81;
        private byte string82;
        private byte string83;
        private byte string84;
        private byte string85;
        private byte string86;
        private byte string87;
        private byte string88;
        private byte string89;
        private byte string90;
        private byte string91;
        private byte string92;
        private byte string93;
        private byte string94;
        private byte string95;
        private byte string96;
        private byte string97;
        private byte string98;
        private byte string99;
        private byte string100;
        private byte string101;
        private byte string102;
        private byte string103;
        private byte string104;
        private byte string105;
        private byte string106;
        private byte string107;
        private byte string108;
        private byte string109;
        private byte string110;
        private byte string111;
        private byte string112;
        private byte string113;
        private byte string114;
        private byte string115;
        private byte string116;
        private byte string117;
        private byte string118;
        private byte string119;
        private byte string120;
        private byte string121;
        private byte string122;
        private byte string123;
        private byte string124;
        private byte string125;
        private byte string126;
        private byte string127;
        private byte string128;
        private byte string129;
        private byte string130;
        private byte string131;
        private byte string132;
        private byte string133;
        private byte string134;
        private byte string135;
        private byte string136;
        private byte string137;
        private byte string138;
        private byte string139;
        private byte string140;
        private byte string141;
        private byte string142;
        private byte string143;
        private byte string144;
        private byte string145;
        private byte string146;
        private byte string147;
        private byte string148;
        private byte string149;
        private byte string150;
        private byte string151;
        private byte string152;
        private byte string153;
        private byte string154;
        private byte string155;
        private byte string156;
        private byte string157;
        private byte string158;
        private byte string159;
        private byte string160;
        private byte string161;
        private byte string162;
        private byte string163;
        private byte string164;
        private byte string165;
        private byte string166;
        private byte string167;
        private byte string168;
        private byte string169;
        private byte string170;
        private byte string171;
        private byte string172;
        private byte string173;
        private byte string174;
        private byte string175;
        private byte string176;
        private byte string177;
        private byte string178;
        private byte string179;
        private byte string180;
        private byte string181;
        private byte string182;
        private byte string183;
        private byte string184;
        private byte string185;
        private byte string186;
        private byte string187;
        private byte string188;
        private byte string189;
        private byte string190;
        private byte string191;
        private byte string192;
        private byte string193;
        private byte string194;
        private byte string195;
        private byte string196;
        private byte string197;
        private byte string198;
        private byte string199;
        private byte string200;
        private byte string201;
        private byte string202;
        private byte string203;
        private byte string204;
        private byte string205;
        private byte string206;
        private byte string207;
        private byte string208;
        private byte string209;
        private byte string210;
        private byte string211;
        private byte string212;
        private byte string213;
        private byte string214;
        private byte string215;
        private byte string216;
        private byte string217;
        private byte string218;
        private byte string219;
        private byte string220;
        private byte string221;
        private byte string222;
        private byte string223;
        private byte string224;
        private byte string225;
        private byte string226;
        private byte string227;
        private byte string228;
        private byte string229;
        private byte string230;
        private byte string231;
        private byte string232;
        private byte string233;
        private byte string234;
        private byte string235;
        private byte string236;
        private byte string237;
        private byte string238;
        private byte string239;
        private byte string240;
        private byte string241;
        private byte string242;
        private byte string243;
        private byte string244;
        private byte string245;
        private byte string246;
        private byte string247;
        private byte string248;
        private byte string249;
        private byte string250;
        private byte string251;
        private byte string252;
        private byte string253;
        private byte string254;
        private byte string255;
    }

    /// <summary>
    /// Represents a 32-byte length string.
    /// </summary>
    [StructLayout(LayoutKind.Sequential), Serializable]
    internal struct String32
    {
        public string String
        {
            get
            {
                byte[] buffer = new byte[] {
                this.string0,
                this.string1,
                this.string2,
                this.string3,
                this.string4,
                this.string5,
                this.string6,
                this.string7,
                this.string8,
                this.string9,
                this.string10,
                this.string11,
                this.string12,
                this.string13,
                this.string14,
                this.string15,
                this.string16,
                this.string17,
                this.string18,
                this.string19,
                this.string20,
                this.string21,
                this.string22,
                this.string23,
                this.string24,
                this.string25,
                this.string26,
                this.string27,
                this.string28,
                this.string29,
                this.string30,
                this.string31};
                return System.Text.Encoding.UTF8.GetString(buffer).Trim('\0');
            }
            set
            {
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(value.PadRight(32, '\0'));
                this.string0 = buffer[0];
                this.string1 = buffer[1];
                this.string2 = buffer[2];
                this.string3 = buffer[3];
                this.string4 = buffer[4];
                this.string5 = buffer[5];
                this.string6 = buffer[6];
                this.string7 = buffer[7];
                this.string8 = buffer[8];
                this.string9 = buffer[9];
                this.string10 = buffer[10];
                this.string11 = buffer[11];
                this.string12 = buffer[12];
                this.string13 = buffer[13];
                this.string14 = buffer[14];
                this.string15 = buffer[15];
                this.string16 = buffer[16];
                this.string17 = buffer[17];
                this.string18 = buffer[18];
                this.string19 = buffer[19];
                this.string20 = buffer[20];
                this.string21 = buffer[21];
                this.string22 = buffer[22];
                this.string23 = buffer[23];
                this.string24 = buffer[24];
                this.string25 = buffer[25];
                this.string26 = buffer[26];
                this.string27 = buffer[27];
                this.string28 = buffer[28];
                this.string29 = buffer[29];
                this.string30 = buffer[30];
                this.string31 = buffer[31];
            }
        }
        private byte string0;
        private byte string1;
        private byte string2;
        private byte string3;
        private byte string4;
        private byte string5;
        private byte string6;
        private byte string7;
        private byte string8;
        private byte string9;
        private byte string10;
        private byte string11;
        private byte string12;
        private byte string13;
        private byte string14;
        private byte string15;
        private byte string16;
        private byte string17;
        private byte string18;
        private byte string19;
        private byte string20;
        private byte string21;
        private byte string22;
        private byte string23;
        private byte string24;
        private byte string25;
        private byte string26;
        private byte string27;
        private byte string28;
        private byte string29;
        private byte string30;
        private byte string31;
    }
}
