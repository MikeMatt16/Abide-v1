using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace Bitmap_Library.Compression
{
    public static class S3TC
    {
        public static void DecompressDxt1(ref byte[] data, byte[] raw, Size size)
        {
            //Prepare
            DXT1TEXEL block = new DXT1TEXEL();
            int x, y, cx, cy, dIndex, lw = size.Width * 4;
            byte[] bgra = new byte[4];
            
            //Gather Data
            int hChunks = size.Width / 4;
            if (hChunks < 1)
                hChunks = 1;

            unsafe
            {
                for (int i = 0; i < size.Width * size.Height / 16; i++)
                    fixed (byte* memBlock = &raw[i * Marshal.SizeOf(block)])
                    {
                        //Get Texel
                        DXT1TEXEL* texPtr = (DXT1TEXEL*)memBlock;
                        block = (*texPtr);

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
                                bgra[3] = 0;
                            dIndex = (cx * 16) + (cy * hChunks * (64)) + (x * 4) + (y * lw);
                            Array.Copy(bgra, 0, data, dIndex, 4);
                        }
                    }
            }
        }

        public static void DecompressDxt3(ref byte[] data, byte[] raw, Size size)
        {
            //Prepare
            DXT3TEXEL block = new DXT3TEXEL();
            int x, y, cx, cy, dIndex, lw = size.Width * 4;
            byte[] bgra;

            //Gather Data
            int hChunks = size.Width / 4;
            if (hChunks < 1)
                hChunks = 1;

            unsafe
            {
                for (int i = 0; i < size.Width * size.Height / 16; i++)
                    fixed (byte* memBlock = &raw[i * Marshal.SizeOf(block)])
                    {
                        //Get Texel
                        DXT3TEXEL* texPtr = (DXT3TEXEL*)memBlock;
                        block = (*texPtr);

                        //Get X and Y
                        cx = i % hChunks;
                        cy = i / hChunks;

                        //Loop
                        for (int j = 0; j < 16; j++)
                        {
                            x = j % 4;
                            y = j / 4;
                            bgra = block.c[block.ColorTable[j]].ToBGRA(block.Alpha[j]);
                            dIndex = (cx * 16) + (cy * hChunks * (16 * 4)) + (x * 4) + (y * lw);
                            Array.Copy(bgra, 0, data, dIndex, 4);
                        }
                    }
            }
        }

        public static void DecompressDxt5(ref byte[] data, byte[] raw, Size size)
        {
            //Prepare
            DXT5TEXEL block = new DXT5TEXEL();
            int x, y, cx, cy, dIndex, lw = size.Width * 4;
            byte[] bgra;

            //Gather Data
            int hChunks = size.Width / 4;
            if (hChunks < 1)
                hChunks = 1;

            unsafe
            {
                for (int i = 0; i < size.Width * size.Height / 16; i++)
                    fixed (byte* memBlock = &raw[i * Marshal.SizeOf(block)])
                    {
                        //Get Texel
                        DXT5TEXEL* texPtr = (DXT5TEXEL*)memBlock;
                        block = (*texPtr);

                        //Get X and Y
                        cx = i % hChunks;
                        cy = i / hChunks;

                        //Loop
                        for (int j = 0; j < 16; j++)
                        {
                            x = j % 4;
                            y = j / 4;
                            bgra = block.c[block.ColorTable[j]].ToBGRA(block.a[block.AlphaTable[j]]);
                            dIndex = (cx * 16) + (cy * hChunks * (16 * 4)) + (x * 4) + (y * lw);
                            Array.Copy(bgra, 0, data, dIndex, 4);
                        }
                    }
            }
        }

        public static void DecompressAti2(ref byte[] data, byte[] raw, Size size)
        {
            //Prepare
            ATI2TEXEL block = new ATI2TEXEL();
            int x, y, cx, cy, dIndex, lw = size.Width * 4;
            byte[] bgra;

            //Gather Data
            int hChunks = size.Width / 4;
            if (hChunks < 1)
                hChunks = 1;

            unsafe
            {
                for (int i = 0; i < size.Width * size.Height / 16; i++)
                    fixed (byte* memBlock = &raw[i * Marshal.SizeOf(block)])
                    {
                        //Get Texel
                        ATI2TEXEL* texPtr = (ATI2TEXEL*)memBlock;
                        block = (*texPtr);

                        //Get X and Y
                        cx = i % hChunks;
                        cy = i / hChunks;

                        //Loop
                        for (int j = 0; j < 16; j++)
                        {
                            x = j % 4;
                            y = j / 4;
                            bgra = block.c[block.ColorTable[j]].ToBGRA(255);
                            dIndex = (cx * 16) + (cy * hChunks * (16 * 4)) + (x * 4) + (y * lw);
                            Array.Copy(bgra, 0, data, dIndex, 4);
                        }
                    }
            }
        }
    }
    
    /// <summary>
    /// Represents one 64-bit texel in a DXT 1 compressed texture.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DXT1TEXEL : IEquatable<DXT1TEXEL>, IComparable<DXT1TEXEL>, IEquatable<ulong>, IComparable<ulong>
    {
        public static readonly DXT1TEXEL Zero = new DXT1TEXEL(0);

        public R5G6B5[] c
        {
            get
            {
                R5G6B5[] c = new R5G6B5[4];
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
                    c[i] = c32[i].To16BitColor();

                return c;
            }
        }
        public bool Color3Alpha
        {
            get { return c[0] <= c[1]; }
        }
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

        public DXT1TEXEL(ulong value)
        {
            this.value = value;
        }

        public int CompareTo(DXT1TEXEL other)
        {
            return value.CompareTo(other.value);
        }
        public int CompareTo(ulong other)
        {
            return value.CompareTo(other);
        }
        public bool Equals(DXT1TEXEL other)
        {
            return value.Equals(other.value);
        }
        public bool Equals(ulong other)
        {
            return value.Equals(other);
        }

        public static implicit operator DXT1TEXEL(ulong value)
        {
            return new DXT1TEXEL(value);
        }
        public static implicit operator ulong(DXT1TEXEL value)
        {
            return value.value;
        }
    }

    /// <summary>
    /// Represents one 128-bit texel in a DXT 3 compressed texture.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DXT3TEXEL : IEquatable<DXT3TEXEL>, IComparable<DXT3TEXEL>, IEquatable<byte[]>, IComparable<byte[]>
    {
        public static readonly DXT3TEXEL Zero = new DXT3TEXEL(0, 0);

        public R5G6B5[] c
        {
            get
            {
                R5G6B5[] c = new R5G6B5[4];
                ushort c0 = (ushort)(high & 0xFFFF);
                ushort c1 = (ushort)((high >> 16) & 0xFFFF);
                Color32F[] c32 = new Color32F[4];
                c32[0] = Color32F.From16BitColor(c0);
                c32[1] = Color32F.From16BitColor(c1);
                c32[2] = c32[0].GradientTwoOne(c32[1]);
                c32[3] = c32[1].GradientTwoOne(c32[0]);
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

        public DXT3TEXEL(byte[] value)
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
        public DXT3TEXEL(ulong low, ulong high)
        {
            this.low = low;
            this.high = high;
        }
        
        public int CompareTo(DXT3TEXEL other)
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
        public bool Equals(DXT3TEXEL other)
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
        public static implicit operator DXT3TEXEL(byte[] value)
        {
            if (value.Length >= 16)
                return new DXT3TEXEL(value);
            else
                throw new ArgumentException("value is not long enough.");
        }
        public static implicit operator byte[] (DXT3TEXEL value)
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
    public struct DXT5TEXEL : IEquatable<DXT5TEXEL>, IComparable<DXT5TEXEL>, IEquatable<byte[]>, IComparable<byte[]>
    {
        public static readonly DXT5TEXEL Zero = new DXT5TEXEL(0, 0);

        public R5G6B5[] c
        {
            get
            {
                R5G6B5[] c = new R5G6B5[4];
                ushort c0 = (ushort)(high & 0xFFFF);
                ushort c1 = (ushort)((high >> 16) & 0xFFFF);
                Color32F[] c32 = new Color32F[4];
                c32[0] = Color32F.From16BitColor(c0);
                c32[1] = Color32F.From16BitColor(c1);
                c32[2] = c32[0].GradientTwoOne(c32[1]);
                c32[3] = c32[1].GradientTwoOne(c32[0]);
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
        public byte[] a
        {
            get
            {
                byte[] a = new byte[16];
                a[0] = (byte)(low & 0xFF);
                a[1] = (byte)((low >> 8) & 0xFF);
                if (a[0] > a[1])
                {
                    a[2] = (byte)((6 * a[0] + 1 * a[1]) / 7);
                    a[3] = (byte)((5 * a[0] + 2 * a[1]) / 7);
                    a[4] = (byte)((4 * a[0] + 3 * a[1]) / 7);
                    a[5] = (byte)((3 * a[0] + 4 * a[1]) / 7);
                    a[6] = (byte)((2 * a[0] + 5 * a[1]) / 7);
                    a[7] = (byte)((1 * a[0] + 6 * a[1]) / 7);
                }
                else
                {
                    a[2] = (byte)((4 * a[0] + 1 * a[1]) / 5);
                    a[3] = (byte)((3 * a[0] + 2 * a[1]) / 5);
                    a[4] = (byte)((2 * a[0] + 3 * a[1]) / 5);
                    a[5] = (byte)((1 * a[0] + 4 * a[1]) / 5);
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

        public DXT5TEXEL(byte[] value)
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
        public DXT5TEXEL(ulong low, ulong high)
        {
            this.low = low;
            this.high = high;
        }

        public int CompareTo(DXT5TEXEL other)
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
        public bool Equals(DXT5TEXEL other)
        {
            return low.Equals(other.low) && high.Equals(other.high);
        }
        public bool Equals(byte[] other)
        {
            return low.Equals(BitConverter.ToUInt64(other, 0)) && high.Equals(BitConverter.ToUInt64(other, 8));
        }

        public static implicit operator DXT5TEXEL(byte[] value)
        {
            if (value.Length >= 16)
                return new DXT5TEXEL(value);
            else
                throw new ArgumentException("value is not long enough.");
        }
        public static implicit operator byte[] (DXT5TEXEL value)
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
    public struct ATI2TEXEL : IEquatable<ATI2TEXEL>, IComparable<ATI2TEXEL>, IEquatable<byte[]>, IComparable<byte[]>
    {
        public static readonly ATI2TEXEL Zero = new ATI2TEXEL(0, 0);

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

        private ulong low, high;

        public ATI2TEXEL(byte[] value)
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
        public ATI2TEXEL(ulong low, ulong high)
        {
            this.low = low;
            this.high = high;
        }

        public int CompareTo(ATI2TEXEL other)
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
        public bool Equals(ATI2TEXEL other)
        {
            return low.Equals(other.low) && high.Equals(other.high);
        }
        public bool Equals(byte[] other)
        {
            return low.Equals(BitConverter.ToUInt64(other, 0)) && high.Equals(BitConverter.ToUInt64(other, 8));
        }

        public static implicit operator ATI2TEXEL(byte[] value)
        {
            if (value.Length >= 16)
                return new ATI2TEXEL(value);
            else
                throw new ArgumentException("value is not long enough.");
        }
        public static implicit operator byte[] (ATI2TEXEL value)
        {
            byte[] b = new byte[16];
            Array.Copy(BitConverter.GetBytes(value.low), 0, b, 0, 8);
            Array.Copy(BitConverter.GetBytes(value.high), 0, b, 8, 8);
            return b;
        }
    }
}
