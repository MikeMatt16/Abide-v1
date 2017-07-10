using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Bitmap_Library.DirectDraw
{
    /// <summary>
    /// Represents DirectDraw Surface pixel format structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DdsPixelFormat
    {
        public int Size
        {
            get { return (int)dwSize; }
            set { dwSize = (uint)value; }
        }
        public DirectDrawPixelFormatFlags Flags
        {
            get { return (DirectDrawPixelFormatFlags)dwFlags; }
            set { dwFlags = (uint)value; }
        }
        public uint DwordFourCC
        {
            get { return dwFourCC; }
            set { dwFourCC = value; }
        }
        public string FourCC
        {
            get { return GetFourCC(dwFourCC); }
            set { dwFourCC = MakeFourCC(value); }
        }
        public uint RgbBitCount
        {
            get { return dwRGBBitCount; }
            set { dwRGBBitCount = value; }
        }
        public uint RedBitmask
        {
            get { return dwRBitMask; }
            set { dwRBitMask = value; }
        }
        public uint BlueBitmask
        {
            get { return dwBBitMask; }
            set { dwBBitMask = value; }
        }
        public uint GreenBitmask
        {
            get { return dwGBitMask; }
            set { dwGBitMask = value; }
        }
        public uint AlphaBitmask
        {
            get { return dwABitMask; }
            set { dwABitMask = value; }
        }

        private uint dwSize;
        private uint dwFlags;
        private uint dwFourCC;
        private uint dwRGBBitCount;
        private uint dwRBitMask;
        private uint dwGBitMask;
        private uint dwBBitMask;
        private uint dwABitMask;
        
        internal static string GetFourCC(uint fourcc)
        {
            byte[] bytes = BitConverter.GetBytes(fourcc);
            return new string(Encoding.ASCII.GetChars(bytes));
        }
        internal static uint MakeFourCC(string fourcc)
        {
            return MakeFourCC(fourcc.ToCharArray());
        }
        internal static uint MakeFourCC(params char[] fourcc)
        {
            byte[] ch = Encoding.ASCII.GetBytes(fourcc);
            return (uint)(ch[0] | (ch[1] << 8) | (ch[2] << 16) | (ch[3] << 24));
        }
    }

    /// <summary>
    /// Represents an enumeration containing the DirectDraw Pixel Format flags.
    /// </summary>
    [Flags]
    public enum DirectDrawPixelFormatFlags : uint
    {
        None = 0x0,
        AlphaPixels = 0x1,
        Alpha = 0x2,
        FourCC = 0x4,
        Rgb = 0x40,
        Yuv = 0x200,
        Luminance = 0x20000,
        VU = 0x80000,

        Argb = AlphaPixels | Rgb,
        AlphaLuminance = AlphaPixels | Luminance,
    }

    /// <summary>
    /// Represents an enumeration containing the DirectDraw FourCCs
    /// </summary>
    public enum DirectDrawFourCC: uint
    {
        DXT1 = 827611204,
        DXT2 = 844388420,
        DXT3 = 861165636,
        DXT4 = 877942852,
        DXT5 = 894720068,
        DX10 = 808540228,
    }

    /// <summary>
    /// Represents an enumeration containing the DirectDraw formats.
    /// </summary>
    public enum DirectDrawFormat : uint
    {
        UNKNOWN = 0,
        R8G8B8 = 20,
        A8R8G8B8 = 21,
        X8R8G8B8 = 22,
        R5G6B5 = 23,
        X1R5G5B5 = 24,
        A1R5G5B5 = 25,
        A4R4G4B4 = 26,
        R3G3B2 = 27,
        A8 = 28,
        A8R3G3B2 = 29,
        X4R4G4B4 = 30,
        A2B10G10R10 = 31,
        A8B8G8R8 = 32,
        X8B8G8R8 = 33,
        G16R16 = 34,
        A2R10G10B10 = 35,
        A16B16G16R16 = 36,
        A8P8 = 40,
        P8 = 41,
        L8 = 50,
        A8L8 = 51,
        A4L4 = 52,
        V8U8 = 60,
        L6V5U5 = 61,
        X8L8V8U8 = 62,
        Q8W8V8U8 = 63,
        V16U16 = 64,
        A2W10V10U10 = 67,
        UYVY = 1498831189,
        R8G8_B8G8 = 1195525970,
        YUY2 = 844715353,
        G8R8_G8B8 = 1111970375,
        D16_LOCKABLE = 70,
        D32 = 71,
        D15S1 = 73,
        D24S8 = 75,
        D24X8 = 77,
        D24X4S4 = 79,
        D16 = 80,
        D32F_LOCKABLE = 82,
        D24FS8 = 83,
        D32_LOCKABLE = 84,
        S8_LOCKABLE = 85,
        L16 = 81,
        VERTEXDATA = 100,
        INDEX16 = 101,
        INDEX32 = 102,
        Q16W16V16U16 = 110,
        MULTI2_ARGB8 = 827606349,
        R16F = 111,
        G16R16F = 112,
        A16B16G16R16F = 113,
        R32F = 114,
        G32R32F = 115,
        A32B32G32R32F = 116,
        CxV8U8 = 117,
        A1 = 118,
        A2B10G10R10_XR_BIAS = 119,
        BINARYBUFFER = 199,
        FORCE_DWORD = 0x7fffffff,
    }
}