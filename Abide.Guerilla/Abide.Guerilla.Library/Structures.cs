using Abide.HaloLibrary;
using System.Runtime.InteropServices;

namespace Abide.Guerilla.Library
{
    /// <summary>
    /// Represents a tag group file header
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TagGroupHeader
    {
        public const int Size = 32;

        public TagFourCc AbideTag { get; set; }
        public TagFourCc GroupTag { get; set; }
        public uint TagResourceCount { get; set; }
        public uint RawOffsetsOffset { get; set; }
        public uint RawLengthsOffset { get; set; }
        public uint RawDataOffset { get; set; }
        public TagId Id { get; set; }
        public uint Checksum { get; set; }
    }

    /// <summary>
    /// Represents a guerilla tag file header.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 64)]
    public struct GuerillaTagHeader
    {
        public const int Size = 64;

        public TagFourCc GroupTag
        {
            get { return groupTag; }
            set { groupTag = value; }
        }
        public uint Checksum
        {
            get { return checksum; }
            set { checksum = value; }
        }
        public uint BlockOffset
        {
            get { return blockOffset; }
            set { blockOffset = value; }
        }
        public uint Unknown
        {
            get { return unknown; }
            set { unknown = value; }
        }
        public TagFourCc Blam
        {
            get { return blam; }
            set { blam = value; }
        }
        
        [FieldOffset(36)]
        private TagFourCc groupTag;
        [FieldOffset(40)]
        private uint checksum;
        [FieldOffset(44)]
        private uint blockOffset;
        [FieldOffset(56)]
        private uint unknown;
        [FieldOffset(60)]
        private TagFourCc blam;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GuerillaTagBlockHeader
    {
        public const int Size = 16;

        public TagFourCc Signature { get; set; }
        public uint Count1 { get; set; }
        public uint Count2 { get; set; }
        public uint Length { get; set; }
    }
}
