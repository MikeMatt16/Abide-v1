using Abide.HaloLibrary;
using System.Runtime.InteropServices;
using HaloTag = Abide.HaloLibrary.TagFourCc;

namespace Abide.Guerilla.Library
{
    /// <summary>
    /// Represents a uinversal tag group file header
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TagGroupHeader
    {
        public const int Size = 32;

        public HaloTag GroupTag { get; set; }
        public uint RawsCount { get; set; }
        public uint RawOffsetsOffset { get; set; }
        public uint RawLengthsOffset { get; set; }

        public uint RawDataOffset { get; set; }
        public TagId Id { get; set; }
        private int reserved0 { get; set; }
        public uint Checksum { get; set; }
    }
}
