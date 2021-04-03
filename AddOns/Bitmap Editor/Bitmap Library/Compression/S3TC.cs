using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;

namespace Bitmap_Library.Compression
{
    public static class S3TC
    {
        public static unsafe void DecompressDxt1(byte[] data, byte[] raw, Size size)
        {
            Dxt1Texel block = new Dxt1Texel();
            int x, y, tX, tY, dIndex, lw = size.Width * 4;

            int hChunks = size.Width / 4;
            if (hChunks < 1)
                hChunks = 1;

            int blockSize = Marshal.SizeOf(block);
            for (int i = 0; i < size.Width * size.Height / 16; i++)
                fixed (byte* memBlock = &raw[i * blockSize])
                {
                    block = *(Dxt1Texel*)memBlock;

                    tX = i % hChunks;
                    tY = i / hChunks;

                    var pixels = block.GetPixels().Select(c => new byte[] { c.B, c.G, c.R, c.A }).ToArray();
                    for (int j = 0; j < pixels.Length; j++)
                    {
                        x = j % 4;
                        y = j / 4;
                        dIndex = (tX * 16) + (tY * hChunks * 64) + (x * 4) + (y * lw);

                        fixed (byte* dest = &data[dIndex]) { *dest = pixels[j][0]; }
                        fixed (byte* dest = &data[dIndex + 1]) { *dest = pixels[j][1]; }
                        fixed (byte* dest = &data[dIndex + 2]) { *dest = pixels[j][2]; }
                        fixed (byte* dest = &data[dIndex + 3]) { *dest = pixels[j][3]; }
                    }
                }
        }

        public static unsafe void DecompressDxt3(byte[] data, byte[] raw, Size size)
        {
            Dxt3Texel block = new Dxt3Texel();
            int x, y, tX, tY, dIndex, lw = size.Width * 4;

            int hChunks = size.Width / 4;
            if (hChunks < 1)
                hChunks = 1;

            int blockSize = Marshal.SizeOf(block);
            for (int i = 0; i < size.Width * size.Height / 16; i++)
                fixed (byte* memBlock = &raw[i * blockSize])
                {
                    block = *(Dxt3Texel*)memBlock;

                    tX = i % hChunks;
                    tY = i / hChunks;

                    var pixels = block.GetPixels().Select(c => new byte[] { c.B, c.G, c.R, c.A }).ToArray();
                    for (int j = 0; j < pixels.Length; j++)
                    {
                        x = j % 4;
                        y = j / 4;
                        dIndex = (tX * 16) + (tY * hChunks * 16 * 4) + (x * 4) + (y * lw);

                        fixed (byte* dest = &data[dIndex]) { *dest = pixels[j][0]; }
                        fixed (byte* dest = &data[dIndex + 1]) { *dest = pixels[j][1]; }
                        fixed (byte* dest = &data[dIndex + 2]) { *dest = pixels[j][2]; }
                        fixed (byte* dest = &data[dIndex + 3]) { *dest = pixels[j][3]; }
                    }
                }
        }

        public static unsafe void DecompressDxt5(byte[] data, byte[] raw, Size size)
        {
            Dxt5Texel block = new Dxt5Texel();
            int x, y, cx, cy, dIndex, lw = size.Width * 4;

            int hChunks = size.Width / 4;
            if (hChunks < 1)
                hChunks = 1;

            int blockSize = Marshal.SizeOf(block);
            for (int i = 0; i < size.Width * size.Height / 16; i++)
                fixed (byte* memBlock = &raw[i * blockSize])
                {
                    block = *(Dxt5Texel*)memBlock;

                    cx = i % hChunks;
                    cy = i / hChunks;

                    var pixels = block.GetPixels().Select(c => new byte[] { c.B, c.G, c.R, c.A }).ToArray();
                    for (int j = 0; j < 16; j++)
                    {
                        x = j % 4;
                        y = j / 4;
                        dIndex = (cx * 16) + (cy * hChunks * 16 * 4) + (x * 4) + (y * lw);

                        fixed (byte* dest = &data[dIndex]) { *dest = pixels[j][0]; }
                        fixed (byte* dest = &data[dIndex + 1]) { *dest = pixels[j][1]; }
                        fixed (byte* dest = &data[dIndex + 2]) { *dest = pixels[j][2]; }
                        fixed (byte* dest = &data[dIndex + 3]) { *dest = pixels[j][3]; }
                    }
                }
        }

        public static unsafe void DecompressAti2(byte[] data, byte[] raw, Size size)
        {
            Ati2Texel block = new Ati2Texel();
            int x, y, cx, cy, dIndex, lw = size.Width * 4;
            byte[] bgra;

            int hChunks = size.Width / 4;
            if (hChunks < 1)
                hChunks = 1;

            int blockSize = Marshal.SizeOf(block);
            for (int i = 0; i < size.Width * size.Height / 16; i++)
                fixed (byte* memBlock = &raw[i * blockSize])
                {
                    block = *(Ati2Texel*)memBlock;

                    cx = i % hChunks;
                    cy = i / hChunks;

                    for (int j = 0; j < 16; j++)
                    {
                        x = j % 4;
                        y = j / 4;
                        bgra = block.c[block.ColorTable[j]].ToBGRA(255);
                        dIndex = (cx * 16) + (cy * hChunks * 16 * 4) + (x * 4) + (y * lw);

                        fixed (byte* dest = &data[dIndex]) { *dest = bgra[0]; }
                        fixed (byte* dest = &data[dIndex + 1]) { *dest = bgra[1]; }
                        fixed (byte* dest = &data[dIndex + 2]) { *dest = bgra[2]; }
                        fixed (byte* dest = &data[dIndex + 3]) { *dest = bgra[3]; }
                    }
                }
        }
    }

    /// <summary>
    /// Represents a 64-bit DXT 1 compressed texel element.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Dxt1Texel : IEquatable<Dxt1Texel>
    {
        public static readonly Dxt1Texel Zero = new Dxt1Texel();
        
        private ulong texel;

        public bool Color3Alpha
        {
            get { return (ushort)(texel & 0xFFFF) > (ushort)((texel >> 16) & 0xFFFF); }
        }

        public override bool Equals(object obj)
        {
            if (obj is Dxt1Texel texel)
            {
                return Equals(texel);
            }

            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return texel.GetHashCode();
        }
        public bool Equals(Dxt1Texel other)
        {
            return texel.Equals(other.texel);
        }
        public Color[] GetPixels()
        {
            Color32F[] colorTable = new Color32F[4];
            ushort c0 = (ushort)(texel & 0xFFFF);
            ushort c1 = (ushort)((texel >> 16) & 0xFFFF);
            colorTable[0] = Color32F.From16BitColor(c0);
            colorTable[1] = Color32F.From16BitColor(c1);

            if (c0 > c1)
            {
                colorTable[2] = colorTable[0].Interpolate(colorTable[1], 3, 2);
                colorTable[3] = colorTable[1].Interpolate(colorTable[0], 3, 2);
            }
            else
            {
                colorTable[2] = colorTable[0].Interpolate(colorTable[1], 2, 1);
                colorTable[3] = Color32F.Zero;
            }

            Color[] pixels = new Color[16];
            uint lut = (uint)(texel >> 32);
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = colorTable[(int)(lut & 0x3)].ToColor();
                lut >>= 2;
            }

            return pixels;
        }
        public byte[] GetBits()
        {
            return BitConverter.GetBytes(texel);
        }

        public static Dxt1Texel FromTexel(ulong texel)
        {
            return new Dxt1Texel() { texel = texel };
        }
        public static explicit operator Dxt1Texel(ulong value)
        {
            return new Dxt1Texel() { texel = value };
        }
        public static explicit operator ulong(Dxt1Texel value)
        {
            return value.texel;
        }
        public static bool operator ==(Dxt1Texel left, Dxt1Texel right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(Dxt1Texel left, Dxt1Texel right)
        {
            return !(left == right);
        }
    }

    /// <summary>
    /// Represents a 128-bit DXT 3 compressed texel element
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Dxt3Texel : IEquatable<Dxt3Texel>
    {
        public static readonly Dxt3Texel Zero = new Dxt3Texel();

        private ulong low, high;

        public bool Equals(Dxt3Texel other)
        {
            return low.Equals(other.low) && high.Equals(other.high);
        }
        public override bool Equals(object obj)
        {
            if (obj is Dxt3Texel texel)
            {
                return Equals(texel);
            }

            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return low.GetHashCode() ^ high.GetHashCode();
        }
        public Color[] GetPixels()
        {
            ushort c0 = (ushort)(high & 0xFFFF);
            ushort c1 = (ushort)((high >> 16) & 0xFFFF);
            Color32F[] colorTable = new Color32F[4];
            colorTable[0] = Color32F.From16BitColor(c0);
            colorTable[1] = Color32F.From16BitColor(c1);
            colorTable[2] = colorTable[0].GradientTwoOne(colorTable[1]);
            colorTable[3] = colorTable[1].GradientTwoOne(colorTable[0]);

            byte[] alphaTable = new byte[16];
            ulong lut = low;
            for (int i = 0; i < 16; i++)
            {
                alphaTable[i] = (byte)((lut & 0xF) * 0x11);
                lut >>= 4;
            }

            Color[] t = new Color[16];
            lut = high >> 32;
            for (int i = 0; i < 16; i++)
            {
                int j = (int)(lut & 0x3);
                t[i] = Color.FromArgb(alphaTable[j], colorTable[j].ToColor());
                lut >>= 2;
            }

            return t;
        }
        public byte[] GetBits()
        {
            return BitConverter.GetBytes(low).Union(BitConverter.GetBytes(high)).ToArray();
        }

        public static Dxt3Texel FromTexel(ulong low, ulong high)
        {
            return new Dxt3Texel() { low = low, high = high };
        }
        public static bool operator ==(Dxt3Texel left, Dxt3Texel right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(Dxt3Texel left, Dxt3Texel right)
        {
            return !(left == right);
        }
    }

    /// <summary>
    /// Represents a 128-bit DXT 5 compressed texel element
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Dxt5Texel : IEquatable<Dxt5Texel>
    {
        public static readonly Dxt5Texel Zero = new Dxt5Texel();

        private ulong low, high;

        public bool Equals(Dxt5Texel other)
        {
            return low.Equals(other.low) && high.Equals(other.high);
        }
        public override bool Equals(object obj)
        {
            if (obj is Dxt5Texel texel)
            {
                return Equals(texel);
            }

            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return low.GetHashCode() ^ high.GetHashCode();
        }
        public Color[] GetPixels()
        {
            ushort c0 = (ushort)(high & 0xFFFF);
            ushort c1 = (ushort)((high >> 16) & 0xFFFF);
            Color32F[] colorTable = new Color32F[4];
            colorTable[0] = Color32F.From16BitColor(c0);
            colorTable[1] = Color32F.From16BitColor(c1);
            colorTable[2] = colorTable[0].GradientTwoOne(colorTable[1]);
            colorTable[3] = colorTable[1].GradientTwoOne(colorTable[0]);

            byte[] alphaTable = new byte[16];
            alphaTable[0] = (byte)(low & 0xFF);
            alphaTable[1] = (byte)((low >> 8) & 0xFF);
            if (alphaTable[0] > alphaTable[1])
            {
                alphaTable[2] = (byte)((6 * alphaTable[0] + 1 * alphaTable[1]) / 7);
                alphaTable[3] = (byte)((5 * alphaTable[0] + 2 * alphaTable[1]) / 7);
                alphaTable[4] = (byte)((4 * alphaTable[0] + 3 * alphaTable[1]) / 7);
                alphaTable[5] = (byte)((3 * alphaTable[0] + 4 * alphaTable[1]) / 7);
                alphaTable[6] = (byte)((2 * alphaTable[0] + 5 * alphaTable[1]) / 7);
                alphaTable[7] = (byte)((1 * alphaTable[0] + 6 * alphaTable[1]) / 7);
            }
            else
            {
                alphaTable[2] = (byte)((4 * alphaTable[0] + 1 * alphaTable[1]) / 5);
                alphaTable[3] = (byte)((3 * alphaTable[0] + 2 * alphaTable[1]) / 5);
                alphaTable[4] = (byte)((2 * alphaTable[0] + 3 * alphaTable[1]) / 5);
                alphaTable[5] = (byte)((1 * alphaTable[0] + 4 * alphaTable[1]) / 5);
                alphaTable[6] = 0x0;
                alphaTable[7] = 0xFF;
            }

            Color[] t = new Color[16];
            ulong lut = high >> 32;
            for (int i = 0; i < 16; i++)
            {
                int j = (int)(lut & 0x3);
                t[i] = Color.FromArgb(alphaTable[j], colorTable[j].ToColor());
                lut >>= 2;
            }

            return t;
        }
        public byte[] GetBits()
        {
            return BitConverter.GetBytes(low).Union(BitConverter.GetBytes(high)).ToArray();
        }

        public static Dxt5Texel FromTexel(ulong low, ulong high)
        {
            return new Dxt5Texel() { low = low, high = high };
        }
        public static bool operator ==(Dxt5Texel left, Dxt5Texel right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(Dxt5Texel left, Dxt5Texel right)
        {
            return !(left == right);
        }
    }

    /// <summary>
    /// Represents a 128-bit texel in a 3Dc compressed texture.
    /// </summary>
    public struct Ati2Texel : IEquatable<Ati2Texel>
    {
        public static readonly Ati2Texel Zero = new Ati2Texel(0, 0);

        public R5G6B5[] c
        {
            get
            {
                R5G6B5[] c = new R5G6B5[6];
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
                    c[i] = c32[i].To16BitColor();
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

        private readonly ulong low;
        private readonly ulong high;

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
                return new Ati2Texel(value);
            else
                throw new ArgumentException("value is not long enough.");
        }
        public static implicit operator byte[](Ati2Texel value)
        {
            byte[] b = new byte[16];
            Array.Copy(BitConverter.GetBytes(value.low), 0, b, 0, 8);
            Array.Copy(BitConverter.GetBytes(value.high), 0, b, 8, 8);
            return b;
        }
    }
}
