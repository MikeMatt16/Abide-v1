using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace Abide.Wpf.Modules.Tools.Halo2.Retail.TextureEditor
{
    public static class TextureHelper
    {
        public static void DecompressDxt1(byte[] dest, byte[] srcData, int width, int height)
        {
            //Prepare
            Dxt1Texel block = new Dxt1Texel();
            int x, y, cx, cy, dIndex, lw = width * 4;
            byte[] bgra = new byte[4];

            //Gather Data
            int hChunks = width / 4;
            if (hChunks < 1)
            {
                hChunks = 1;
            }

            unsafe
            {
                int blockSize = Marshal.SizeOf(block);
                for (int i = 0; i < width * height / 16; i++)
                {
                    fixed (byte* memBlock = &srcData[i * blockSize])
                    {
                        //Get Texel
                        Dxt1Texel* texPtr = (Dxt1Texel*)memBlock;
                        block = *texPtr;

                        //Get X and Y
                        cx = i % hChunks;
                        cy = i / hChunks;

                        //Loop
                        for (int j = 0; j < 16; j++)
                        {
                            x = j % 4;
                            y = j / 4;
                            bgra = block.c[block.ColorTable[j]].ToBGRA();
                            if (block.Color3Alpha && block.ColorTable[j] == 3)
                            {
                                bgra[3] = 0;
                            }

                            dIndex = (cx * 16) + (cy * hChunks * 64) + (x * 4) + (y * lw);
                            Array.Copy(bgra, 0, dest, dIndex, 4);
                        }
                    }
                }
            }
        }

        public static void DecompressDxt3(byte[] dest, byte[] srcData, int width, int height)
        {
            //Prepare
            Dxt3Texel block = new Dxt3Texel();
            int x, y, cx, cy, dIndex, lw = width * 4;
            byte[] bgra;

            //Gather Data
            int hChunks = width / 4;
            if (hChunks < 1)
            {
                hChunks = 1;
            }

            unsafe
            {
                int blockSize = Marshal.SizeOf(block);
                for (int i = 0; i < width * height / 16; i++)
                {
                    fixed (byte* memBlock = &srcData[i * blockSize])
                    {
                        //Get Texel
                        Dxt3Texel* texPtr = (Dxt3Texel*)memBlock;
                        block = *texPtr;

                        //Get X and Y
                        cx = i % hChunks;
                        cy = i / hChunks;

                        //Loop
                        for (int j = 0; j < 16; j++)
                        {
                            x = j % 4;
                            y = j / 4;
                            bgra = block.c[block.ColorTable[j]].ToBGRA(block.Alpha[j]);
                            dIndex = (cx * 16) + (cy * hChunks * 16 * 4) + (x * 4) + (y * lw);
                            Array.Copy(bgra, 0, dest, dIndex, 4);
                        }
                    }
                }
            }
        }

        public static void DecompressDxt5(byte[] dest, byte[] srcData, int width, int height)
        {
            //Prepare
            Dxt5Texel block = new Dxt5Texel();
            int x, y, cx, cy, dIndex, lw = width * 4;
            byte[] bgra;

            //Gather Data
            int hChunks = width / 4;
            if (hChunks < 1)
            {
                hChunks = 1;
            }

            unsafe
            {
                int blockSize = Marshal.SizeOf(block);
                for (int i = 0; i < width * height / 16; i++)
                {
                    fixed (byte* memBlock = &srcData[i * blockSize])
                    {
                        //Get Texel
                        Dxt5Texel* texPtr = (Dxt5Texel*)memBlock;
                        block = *texPtr;

                        //Get X and Y
                        cx = i % hChunks;
                        cy = i / hChunks;

                        //Loop
                        for (int j = 0; j < 16; j++)
                        {
                            x = j % 4;
                            y = j / 4;
                            bgra = block.c[block.ColorTable[j]].ToBGRA(block.a[block.AlphaTable[j]]);
                            dIndex = (cx * 16) + (cy * hChunks * 16 * 4) + (x * 4) + (y * lw);
                            Array.Copy(bgra, 0, dest, dIndex, 4);
                        }
                    }
                }
            }
        }

        public static void DecompressAti2(byte[] dest, byte[] srcData, int width, int height)
        {
            //Prepare
            Ati2Texel block = new Ati2Texel();
            int x, y, cx, cy, dIndex, lw = width * 4;
            byte[] bgra;

            //Gather Data
            int hChunks = width / 4;
            if (hChunks < 1)
            {
                hChunks = 1;
            }

            unsafe
            {
                int blockSize = Marshal.SizeOf(block);
                for (int i = 0; i < width * height / 16; i++)
                {
                    fixed (byte* memBlock = &srcData[i * blockSize])
                    {
                        //Get Texel
                        Ati2Texel* texPtr = (Ati2Texel*)memBlock;
                        block = *texPtr;

                        //Get X and Y
                        cx = i % hChunks;
                        cy = i / hChunks;

                        //Loop
                        for (int j = 0; j < 16; j++)
                        {
                            x = j % 4;
                            y = j / 4;
                            bgra = block.c[block.ColorTable[j]].ToBGRA(255);
                            dIndex = (cx * 16) + (cy * hChunks * 16 * 4) + (x * 4) + (y * lw);
                            Array.Copy(bgra, 0, dest, dIndex, 4);
                        }
                    }
                }
            }
        }

        public static Color32F ToColorF(this Color color)
        {
            return Color32F.FromColor(color);
        }

        public static byte[] Swizzle(byte[] data, int width, int height, int depth, int bitCount, bool deswizzle)
        {
            bitCount /= 8;
            int a = 0;
            int b = 0;
            byte[] newData = new byte[data.Length]; //width * height * bitCount;

            MaskSet masks = new MaskSet(width, height, depth);

            int x = 0, y = 0;
            for (int i = 0; i < height * width; i++)
            {
                x = i % width;
                y = i / width;

                if (deswizzle)
                {
                    a = ((y * width) + x) * bitCount;
                    b = Swizzle(x, y, -1, masks) * bitCount;
                }
                else
                {
                    b = ((y * width) + x) * bitCount;
                    a = Swizzle(x, y, -1, masks) * bitCount;
                }

                if (a < newData.Length && b < data.Length)
                {
                    for (int j = 0; j < bitCount; j++)
                    {
                        newData[a + j] = data[b + j];
                    }
                }
                else
                {
                    return null;
                }
            }
            return newData;
        }

        private static int Swizzle(int x, int y, int z, MaskSet masks)
        {
            return SwizzleAxis(x, masks.x) | SwizzleAxis(y, masks.y) | (z == -1 ? 0 : SwizzleAxis(z, masks.z));
        }

        private static int SwizzleAxis(int val, int mask)
        {
            int bit = 1;
            int result = 0;

            while (bit <= mask)
            {
                int tmp = mask & bit;

                if (tmp != 0)
                {
                    result |= val & bit;
                }
                else
                {
                    val <<= 1;
                }

                bit <<= 1;
            }

            return result;
        }

        private struct MaskSet
        {
            public int x;
            public int y;
            public int z;

            public MaskSet(int w, int h, int d)
            {
                //Prepare
                int bit = 1;
                int index = 1;
                x = 0;
                y = 0;
                z = 0;

                //Loop
                while (bit < w || bit < h || bit < d)
                {
                    if (bit < w)
                    {
                        x |= index;
                        index <<= 1;
                    }
                    if (bit < h)
                    {
                        y |= index;
                        index <<= 1;
                    }
                    if (bit < d)
                    {
                        z |= index;
                        index <<= 1;
                    }
                    bit <<= 1;
                }
            }
        }
    }

    /// <summary>
    /// Represents one 64-bit texel in a DXT 1 compressed texture.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Dxt1Texel : IEquatable<Dxt1Texel>, IComparable<Dxt1Texel>, IEquatable<ulong>, IComparable<ulong>
    {
        public static readonly Dxt1Texel Zero = new Dxt1Texel(0);

        public Rgb555[] c
        {
            get
            {
                Rgb555[] c = new Rgb555[4];
                Color32F[] c32 = new Color32F[4];
                ushort c0 = (ushort)(value & 0xFFFF);
                ushort c1 = (ushort)((value >> 16) & 0xFFFF);
                c32[0] = Color32F.From16BitColor(c0);
                c32[1] = Color32F.From16BitColor(c1);

                if (c0 > c1)
                {
                    c32[2] = c32[0].Interpolate(c32[1], 3, 2);
                    c32[3] = c32[1].Interpolate(c32[0], 3, 2);
                }
                else
                {
                    c32[2] = c32[0].Interpolate(c32[1], 2, 1);
                    c32[3] = Color32F.Zero;
                }

                for (int i = 0; i < 4; i++)
                {
                    c[i] = c32[i].To16BitColor();
                }

                return c;
            }
        }
        public bool Color3Alpha => c[0] <= c[1];
        public byte[] ColorTable
        {
            get
            {
                byte[] t = new byte[16];
                uint lut = (uint)(value >> 32);
                for (int i = 0; i < 16; i++)
                {
                    t[i] = (byte)(lut & 0x3);
                    lut >>= 2;
                }
                return t;
            }
        }

        private ulong value;

        public Dxt1Texel(ulong value)
        {
            this.value = value;
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }
        public int CompareTo(Dxt1Texel other)
        {
            return value.CompareTo(other.value);
        }
        public int CompareTo(ulong other)
        {
            return value.CompareTo(other);
        }
        public bool Equals(Dxt1Texel other)
        {
            return value.Equals(other.value);
        }
        public bool Equals(ulong other)
        {
            return value.Equals(other);
        }
        public override bool Equals(object obj)
        {
            return value.Equals(obj);
        }

        public static implicit operator Dxt1Texel(ulong value)
        {
            return new Dxt1Texel(value);
        }
        public static implicit operator ulong(Dxt1Texel value)
        {
            return value.value;
        }
        public static bool operator ==(Dxt1Texel left, Dxt1Texel right)
        {
            return left.value.Equals(right.value);
        }
        public static bool operator !=(Dxt1Texel left, Dxt1Texel right)
        {
            return !left.value.Equals(right.value);
        }
    }

    /// <summary>
    /// Represents one 128-bit texel in a DXT 3 compressed texture.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Dxt3Texel : IEquatable<Dxt3Texel>, IComparable<Dxt3Texel>, IEquatable<byte[]>, IComparable<byte[]>
    {
        public static readonly Dxt3Texel Zero = new Dxt3Texel(0, 0);

        public Rgb555[] c
        {
            get
            {
                Rgb555[] c = new Rgb555[4];
                ushort c0 = (ushort)(high & 0xFFFF);
                ushort c1 = (ushort)((high >> 16) & 0xFFFF);
                Color32F[] c32 = new Color32F[4];
                c32[0] = Color32F.From16BitColor(c0);
                c32[1] = Color32F.From16BitColor(c1);
                c32[2] = c32[0].GradientTwoOne(c32[1]);
                c32[3] = c32[1].GradientTwoOne(c32[0]);
                for (int i = 0; i < 4; i++)
                {
                    c[i] = c32[i].To16BitColor();
                }

                return c;
            }
        }
        public byte[] ColorTable
        {
            get
            {
                byte[] t = new byte[16];
                uint lut = (uint)(high >> 32);
                for (int i = 0; i < 16; i++)
                {
                    t[i] = (byte)(lut & 0x3);
                    lut >>= 2;
                }
                return t;
            }
        }
        public byte[] Alpha
        {
            get
            {
                byte[] t = new byte[16];
                ulong lut = low;
                for (int i = 0; i < 16; i++)
                {
                    t[i] = (byte)((lut & 0xF) * 0x11);
                    lut >>= 4;
                }
                return t;
            }
        }

        private ulong low, high;

        public Dxt3Texel(byte[] value)
        {
            //Convert
            if (value.Length >= 16)
            {
                low = BitConverter.ToUInt64(value, 0);
                high = BitConverter.ToUInt64(value, 8);
            }
            else
            {
                //Zero
                low = 0;
                high = 0;
            }
        }
        public Dxt3Texel(ulong low, ulong high)
        {
            this.low = low;
            this.high = high;
        }

        public int CompareTo(Dxt3Texel other)
        {
            int compare = 0;
            compare += low.CompareTo(other.low);
            compare += high.CompareTo(other.high);
            return compare;
        }
        public int CompareTo(byte[] other)
        {
            int compare = 0;
            compare += low.CompareTo(BitConverter.ToUInt64(other, 0));
            compare += high.CompareTo(BitConverter.ToUInt64(other, 8));
            return compare;
        }
        public bool Equals(Dxt3Texel other)
        {
            return low.Equals(other.low) && high.Equals(other.high);
        }
        public bool Equals(byte[] other)
        {
            return low.Equals(BitConverter.ToUInt64(other, 0)) && high.Equals(BitConverter.ToUInt64(other, 8));
        }
        public byte[] ToByteArray()
        {
            byte[] texel = new byte[16];
            using (MemoryStream ms = new MemoryStream(texel))
            {
                ms.Write(BitConverter.GetBytes(low), 0, 8);
                ms.Write(BitConverter.GetBytes(high), 8, 8);
            }
            return texel;
        }
        public static implicit operator Dxt3Texel(byte[] value)
        {
            if (value.Length >= 16)
            {
                return new Dxt3Texel(value);
            }
            else
            {
                throw new ArgumentException("value is not long enough.");
            }
        }
        public static implicit operator byte[](Dxt3Texel value)
        {
            byte[] b = new byte[16];
            Array.Copy(BitConverter.GetBytes(value.low), 0, b, 0, 8);
            Array.Copy(BitConverter.GetBytes(value.high), 0, b, 8, 8);
            return b;
        }
    }

    /// <summary>
    /// Represents one 128-bit texel in a DXT 5 compressed texture.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Dxt5Texel : IEquatable<Dxt5Texel>, IComparable<Dxt5Texel>, IEquatable<byte[]>, IComparable<byte[]>
    {
        public static readonly Dxt5Texel Zero = new Dxt5Texel(0, 0);

        public Rgb555[] c
        {
            get
            {
                Rgb555[] c = new Rgb555[4];
                ushort c0 = (ushort)(high & 0xFFFF);
                ushort c1 = (ushort)((high >> 16) & 0xFFFF);
                Color32F[] c32 = new Color32F[4];
                c32[0] = Color32F.From16BitColor(c0);
                c32[1] = Color32F.From16BitColor(c1);
                c32[2] = c32[0].GradientTwoOne(c32[1]);
                c32[3] = c32[1].GradientTwoOne(c32[0]);
                for (int i = 0; i < 4; i++)
                {
                    c[i] = c32[i].To16BitColor();
                }

                return c;
            }
        }
        public byte[] ColorTable
        {
            get
            {
                byte[] t = new byte[16];
                uint lut = (uint)(high >> 32);
                for (int i = 0; i < 16; i++)
                {
                    t[i] = (byte)(lut & 0x3);
                    lut >>= 2;
                }
                return t;
            }
        }
        public byte[] a
        {
            get
            {
                byte[] a = new byte[16];
                a[0] = (byte)(low & 0xFF);
                a[1] = (byte)((low >> 8) & 0xFF);
                if (a[0] > a[1])
                {
                    a[2] = (byte)(((6 * a[0]) + (1 * a[1])) / 7);
                    a[3] = (byte)(((5 * a[0]) + (2 * a[1])) / 7);
                    a[4] = (byte)(((4 * a[0]) + (3 * a[1])) / 7);
                    a[5] = (byte)(((3 * a[0]) + (4 * a[1])) / 7);
                    a[6] = (byte)(((2 * a[0]) + (5 * a[1])) / 7);
                    a[7] = (byte)(((1 * a[0]) + (6 * a[1])) / 7);
                }
                else
                {
                    a[2] = (byte)(((4 * a[0]) + (1 * a[1])) / 5);
                    a[3] = (byte)(((3 * a[0]) + (2 * a[1])) / 5);
                    a[4] = (byte)(((2 * a[0]) + (3 * a[1])) / 5);
                    a[5] = (byte)(((1 * a[0]) + (4 * a[1])) / 5);
                    a[6] = 0x0;
                    a[7] = 0xFF;
                }
                return a;
            }
        }
        public byte[] AlphaTable
        {
            get
            {
                byte[] t = new byte[16];
                ulong lut = low >> 16;
                for (int i = 0; i < 16; i++)
                {
                    t[i] = (byte)(lut & 0x7);
                    lut >>= 3;
                }
                return t;
            }
        }

        private ulong low, high;

        public Dxt5Texel(byte[] value)
        {
            //Convert
            if (value.Length >= 16)
            {
                low = BitConverter.ToUInt64(value, 0);
                high = BitConverter.ToUInt64(value, 8);
            }
            else
            {
                //Zero
                low = 0;
                high = 0;
            }
        }
        public Dxt5Texel(ulong low, ulong high)
        {
            this.low = low;
            this.high = high;
        }

        public int CompareTo(Dxt5Texel other)
        {
            int compare = 0;
            compare += low.CompareTo(other.low);
            compare += high.CompareTo(other.high);
            return compare;
        }
        public int CompareTo(byte[] other)
        {
            int compare = 0;
            compare += low.CompareTo(BitConverter.ToUInt64(other, 0));
            compare += high.CompareTo(BitConverter.ToUInt64(other, 8));
            return compare;
        }
        public bool Equals(Dxt5Texel other)
        {
            return low.Equals(other.low) && high.Equals(other.high);
        }
        public bool Equals(byte[] other)
        {
            return low.Equals(BitConverter.ToUInt64(other, 0)) && high.Equals(BitConverter.ToUInt64(other, 8));
        }

        public static implicit operator Dxt5Texel(byte[] value)
        {
            if (value.Length >= 16)
            {
                return new Dxt5Texel(value);
            }
            else
            {
                throw new ArgumentException("value is not long enough.");
            }
        }
        public static implicit operator byte[](Dxt5Texel value)
        {
            byte[] b = new byte[16];
            Array.Copy(BitConverter.GetBytes(value.low), 0, b, 0, 8);
            Array.Copy(BitConverter.GetBytes(value.high), 0, b, 8, 8);
            return b;
        }
    }

    /// <summary>
    /// Represents one 128-bit texel in a 3Dc compressed texture.
    /// </summary>
    public struct Ati2Texel : IEquatable<Ati2Texel>, IComparable<Ati2Texel>, IEquatable<byte[]>, IComparable<byte[]>
    {
        public static readonly Ati2Texel Zero = new Ati2Texel(0, 0);

        public Rgb555[] c
        {
            get
            {
                Rgb555[] c = new Rgb555[6];
                ushort c0 = (ushort)(high & 0xFFFF);
                ushort c1 = (ushort)((high >> 16) & 0xFFFF);
                Color32F[] c32 = new Color32F[6];
                c32[0] = Color32F.From16BitColor(c0);
                c32[1] = Color32F.From16BitColor(c1);
                c32[2] = c32[0].Interpolate(c32[1], 6, 1);
                c32[3] = c32[0].Interpolate(c32[1], 6, 2);
                c32[4] = c32[0].Interpolate(c32[1], 6, 3);
                c32[5] = c32[0].Interpolate(c32[1], 6, 4);
                for (int i = 0; i < 4; i++)
                {
                    c[i] = c32[i].To16BitColor();
                }

                return c;
            }
        }
        public byte[] ColorTable
        {
            get
            {
                byte[] t = new byte[16];
                uint lut = (uint)(high >> 32);
                for (int i = 0; i < 16; i++)
                {
                    t[i] = (byte)(lut & 0x3);
                    lut >>= 2;
                }
                return t;
            }
        }

        private ulong low, high;

        public Ati2Texel(byte[] value)
        {
            //Convert
            if (value.Length >= 16)
            {
                low = BitConverter.ToUInt64(value, 0);
                high = BitConverter.ToUInt64(value, 8);
            }
            else
            {
                //Zero
                low = 0;
                high = 0;
            }
        }
        public Ati2Texel(ulong low, ulong high)
        {
            this.low = low;
            this.high = high;
        }

        public int CompareTo(Ati2Texel other)
        {
            int compare = 0;
            compare += low.CompareTo(other.low);
            compare += high.CompareTo(other.high);
            return compare;
        }
        public int CompareTo(byte[] other)
        {
            int compare = 0;
            compare += low.CompareTo(BitConverter.ToUInt64(other, 0));
            compare += high.CompareTo(BitConverter.ToUInt64(other, 8));
            return compare;
        }
        public bool Equals(Ati2Texel other)
        {
            return low.Equals(other.low) && high.Equals(other.high);
        }
        public bool Equals(byte[] other)
        {
            return low.Equals(BitConverter.ToUInt64(other, 0)) && high.Equals(BitConverter.ToUInt64(other, 8));
        }

        public static implicit operator Ati2Texel(byte[] value)
        {
            if (value.Length >= 16)
            {
                return new Ati2Texel(value);
            }
            else
            {
                throw new ArgumentException("value is not long enough.");
            }
        }
        public static implicit operator byte[](Ati2Texel value)
        {
            byte[] b = new byte[16];
            Array.Copy(BitConverter.GetBytes(value.low), 0, b, 0, 8);
            Array.Copy(BitConverter.GetBytes(value.high), 0, b, 8, 8);
            return b;
        }
    }

    /// <summary>
    /// Represents a 32-bit floating-point Color instance.
    /// </summary>
    public struct Color32F : IEquatable<Color32F>, IComparable<Color32F>
    {
        public static readonly Color32F Zero = new Color32F(0, 0, 0, 0);

        private const float EightBitFactor = 1f / 255f;
        private const float FiveBitFactor = 1f / 31f;
        private const float SixBitFactor = 1f / 63f;

        public float R
        {
            get => r;
            set => r = value;
        }
        public float G
        {
            get => g;
            set => g = value;
        }
        public float B
        {
            get => b;
            set => b = value;
        }
        public float A
        {
            get => a;
            set => a = value;
        }

        private float r, g, b, a;

        public Color32F(float r, float g, float b, float a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }
        public static Color32F FromColor(Color color)
        {
            return new Color32F()
            {
                r = color.R * EightBitFactor,
                g = color.G * EightBitFactor,
                b = color.B * EightBitFactor,
                a = color.A * EightBitFactor,
            };
        }
        public static Color32F From16BitColor(Rgb555 color)
        {
            return new Color32F()
            {
                r = color.R5 * FiveBitFactor,
                g = color.G6 * SixBitFactor,
                b = color.B5 * FiveBitFactor,
                a = 1f,
            };
        }
        public Color ToByteColor()
        {
            return Color.FromArgb(
                Convert.ToByte(Math.Floor(a / EightBitFactor)),
                Convert.ToByte(Math.Floor(r / EightBitFactor)),
                Convert.ToByte(Math.Floor(g / EightBitFactor)),
                Convert.ToByte(Math.Floor(b / EightBitFactor)));
        }
        public Rgb555 To16BitColor()
        {
            return new Rgb555((ushort)(
                (Convert.ToUInt16(Math.Floor(r / FiveBitFactor)) << 11) |
                (Convert.ToUInt16(Math.Floor(g / SixBitFactor)) << 5) |
                (Convert.ToUInt16(Math.Floor(b / FiveBitFactor)))));
        }
        public Color32F Interpolate(Color32F other, float points, float dataPoint)
        {
            float r = (dataPoint / points * R) + (1f / points * other.R);
            float g = (dataPoint / points * G) + (1f / points * other.G);
            float b = (dataPoint / points * B) + (1f / points * other.B);
            float a = (dataPoint / points * A) + (1f / points * other.A);
            return new Color32F(r, g, b, a);
        }
        public Color32F GradientTwoOne(Color32F other)
        {
            float r = 2f / 3f * R + 1f / 3f * other.R;
            float g = (2f / 3f * G) + (1f / 3f * other.G);
            float b = (2f / 3f * B) + (1f / 3f * other.B);
            float a = (2f / 3f * A) + (1f / 3f * other.A);
            return new Color32F(r, g, b, a);
        }
        public Color32F GradientOneHalf(Color32F other)
        {
            float r = (0.5f * R) + (0.5f * other.R);
            float g = (0.5f * G) + (0.5f * other.G);
            float b = (0.5f * B) + (0.5f * other.B);
            float a = (0.5f * A) + (0.5f * other.A);
            return new Color32F(r, g, b, a);
        }

        public override string ToString()
        {
            return string.Format("R:{0:0.00} G:{1:0.00} B:{2:0.00}", R, G, B);
        }

        public bool Equals(Color32F other)
        {
            return r.Equals(other.r) && g.Equals(other.g) && b.Equals(other.b) && a.Equals(other.a);
        }
        public int CompareTo(Color32F other)
        {
            return r.CompareTo(other.r) + g.CompareTo(other.g) + b.CompareTo(other.b) + a.CompareTo(other.a);
        }
    }

    /// <summary>
    /// Represents a 16-bit R5 G6 B5 Color instance.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Rgb555 : IEquatable<Rgb555>, IComparable<Rgb555>, IEquatable<ushort>, IComparable<ushort>
    {
        public static readonly Rgb555 Zero = new Rgb555(0);

        private const float ThirtyOneFactor = 255f / 31f;
        private const float SixtyThreeFactor = 255f / 63f;

        public byte R
        {
            get => (byte)(((value >> 11) & 31) * 255 / 31);
            set
            {
                ushort c = (byte)(value * ThirtyOneFactor);
                this.value = (ushort)((value & 0x7FF) | (c << 11));
            }
        }
        public byte G
        {
            get => (byte)(((value >> 5) & 63) * 255 / 63);
            set
            {
                ushort c = (byte)(value * SixtyThreeFactor);
                this.value = (ushort)((value & 0xF81F) | (c << 5));
            }
        }
        public byte B
        {
            get => (byte)(((value >> 0) & 31) * 255 / 31);
            set
            {
                ushort c = (byte)(value * ThirtyOneFactor);
                this.value = (ushort)((value & 0xFFE0) | (c << 0));
            }
        }

        public byte R5 => (byte)((value >> 11) & 31);
        public byte G6 => (byte)((value >> 5) & 63);
        public byte B5 => (byte)((value >> 0) & 31);

        private ushort value;

        public Rgb555(ushort value)
        {
            this.value = value;
        }
        public Rgb555(byte r, byte g, byte b)
        {
            byte R = (byte)(r * ThirtyOneFactor);
            byte G = (byte)(g * SixtyThreeFactor);
            byte B = (byte)(b * ThirtyOneFactor);
            value = (ushort)((B) | (G << 5) | (R << 11));
        }

        public Rgb555 GradientTwoOne(Rgb555 other)
        {
            byte r = 0, g = 0, b = 0;
            r = (byte)(((2 * R / 3) + (other.R / 3)) / ThirtyOneFactor);
            g = (byte)(((2 * G / 3) + (other.G / 3)) / SixtyThreeFactor);
            b = (byte)(((2 * B / 3) + (other.B / 3)) / ThirtyOneFactor);
            return (ushort)((b) | (g << 5) | (r << 11));
        }
        public Rgb555 GradientOneHalf(Rgb555 other)
        {
            byte r = 0, g = 0, b = 0;
            r = (byte)(((R / 3) + (other.R / 2)) / ThirtyOneFactor);
            g = (byte)(((G / 2) + (other.G / 2)) / SixtyThreeFactor);
            b = (byte)(((B / 2) + (other.B / 2)) / ThirtyOneFactor);
            return (ushort)((b) | (g << 5) | (r << 11));
        }
        public byte[] ToBGRA()
        {
            return ToBGRA(0xff);
        }
        public byte[] ToBGRA(byte alpha)
        {
            return new byte[] { B, G, R, alpha };
        }
        public byte[] ToBGR()
        {
            return new byte[] { B, G, R };
        }
        public int CompareTo(Rgb555 other)
        {
            return value.CompareTo(other.value);
        }
        public int CompareTo(ushort other)
        {
            return value.CompareTo(other);
        }
        public bool Equals(Rgb555 other)
        {
            return value.Equals(other.value);
        }
        public bool Equals(ushort other)
        {
            return value.Equals(other);
        }

        public override string ToString()
        {
            return string.Format("R:{1} G:{2} B:{3} (#{0:X4})", value, R, G, B);
        }

        public static implicit operator Rgb555(ushort value)
        {
            return new Rgb555(value);
        }
        public static implicit operator ushort(Rgb555 value)
        {
            return value.value;
        }
        public static bool operator >(Rgb555 c1, Rgb555 c2)
        {
            return c1.value > c2.value;
        }
        public static bool operator <(Rgb555 c1, Rgb555 c2)
        {
            return c1.value < c2.value;
        }
    }
}
