using System;
using System.Runtime.InteropServices;

namespace Bitmap_Library.DirectDraw
{
    /// <summary>
    /// Represents a DirectDraw surface file header.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DdsHeader
    {
        public int Size
        {
            get { return (int)dwSize; }
            set { dwSize = (uint)value; }
        }
        public DirectDrawSurfaceDefinitionFlags Flags
        {
            get { return (DirectDrawSurfaceDefinitionFlags)dwFlags; }
            set { dwFlags = (uint)value; }
        }
        public uint Height
        {
            get { return dwHeight; }
            set { dwHeight = value; }
        }
        public uint Width
        {
            get { return dwWidth; }
            set { dwWidth = value; }
        }
        public uint PitchOrLinearSize
        {
            get { return dwPitchOrLinearSize; }
            set { dwPitchOrLinearSize = value; }
        }
        public uint Depth
        {
            get { return dwDepth; }
            set { dwDepth = value; }
        }
        public uint MipmapCount
        {
            get { return dwMipMapCount; }
            set { dwMipMapCount = value; }
        }
        public DirectDrawCaps Caps
        {
            get { return (DirectDrawCaps)dwCaps; }
            set { dwCaps = (uint)value; }
        }
        public DirectDrawCapsTwo CapsTwo
        {
            get { return (DirectDrawCapsTwo)dwCaps2; }
            set { dwCaps2 = (uint)value; }
        }
        public int PixelFormatSize
        {
            get { return ddspf.Size; }
            set { ddspf.Size = value; }
        }
        public DirectDrawPixelFormatFlags PixelFormatFlags
        {
            get { return ddspf.Flags; }
            set { ddspf.Flags = value; }
        }
        public uint DwordFourCC
        {
            get { return ddspf.DwordFourCC; }
            set { ddspf.DwordFourCC = value; }
        }
        public string FourCC
        {
            get { return ddspf.FourCC; }
            set { ddspf.FourCC = value; }
        }
        public uint RgbBitCount
        {
            get { return ddspf.RgbBitCount; }
            set { ddspf.RgbBitCount = value; }
        }
        public uint RedBitmask
        {
            get { return ddspf.RedBitmask; }
            set { ddspf.RedBitmask = value; }
        }
        public uint BlueBitmask
        {
            get { return ddspf.BlueBitmask; }
            set { ddspf.BlueBitmask = value; }
        }
        public uint GreenBitmask
        {
            get { return ddspf.GreenBitmask; }
            set { ddspf.GreenBitmask = value; }
        }
        public uint AlphaBitmask
        {
            get { return ddspf.AlphaBitmask; }
            set { ddspf.AlphaBitmask = value; }
        }

        private uint dwSize;
        private uint dwFlags;
        private uint dwHeight;
        private uint dwWidth;
        private uint dwPitchOrLinearSize;
        private uint dwDepth;
        private uint dwMipMapCount;
        private uint dwReserved1_1;
        private uint dwReserved1_2;
        private uint dwReserved1_3;
        private uint dwReserved1_4;
        private uint dwReserved1_5;
        private uint dwReserved1_6;
        private uint dwReserved1_7;
        private uint dwReserved1_8;
        private uint dwReserved1_9;
        private uint dwReserved1_10;
        private uint dwReserved1_11;
        private DdsPixelFormat ddspf;
        private uint dwCaps;
        private uint dwCaps2;
        private uint dwCaps3;
        private uint dwCaps4;
        private uint dwReserved2;
    }

    /// <summary>
    /// Represents an enumeration containing the DirectDraw Surface defined values flags.
    /// </summary>
    [Flags]
    public enum DirectDrawSurfaceDefinitionFlags : uint
    {
        Caps = 0x1,
        Height = 0x2,
        Width = 0x4,
        Pitch = 0x8,
        PixelFormat = 0x1000,
        MipmapCount = 0x20000,
        LinearSize = 0x80000,
        Depth = 0x800000
    }

    /// <summary>
    /// Represents an enumeration containing the DirectDraw Surface surface complexity.
    /// </summary>
    [Flags]
    public enum DirectDrawCaps : uint
    {
        Complex = 0x8,
        Mipmap = 0x400000,
        Texture = 0x1000
    }

    /// <summary>
    /// Represents an enumeration containing the DirectDraw Surface Surface additional surface details.
    /// </summary>
    [Flags]
    public enum DirectDrawCapsTwo : uint
    {
        Cubemap = 0x200,
        CubemapPositiveX = 0x400,
        CubemapNegativeX = 0x800,
        CubemapPositiveY = 0x1000,
        CubemapNegativeY = 0x2000,
        CubemapPositiveZ = 0x4000,
        CubemapNegativeZ = 0x8000,
        Volume = 0x200000
    }
}