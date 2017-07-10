using Bitmap_Library.Compression;
using Bitmap_Library.DirectDraw;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace System.Drawing
{
    public static class DdsExtensions
    {
        public static Color32F ToColorF(this Color color)
        {
            return Color32F.FromColor(color);
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
            get { return r; }
            set { r = value; }
        }
        public float G
        {
            get { return g; }
            set { g = value; }
        }
        public float B
        {
            get { return b; }
            set { b = value; }
        }
        public float A
        {
            get { return a; }
            set { a = value; }
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
                r = ((float)color.R) * EightBitFactor,
                g = ((float)color.G) * EightBitFactor,
                b = ((float)color.B) * EightBitFactor,
                a = ((float)color.A) * EightBitFactor,
            };
        }
        public static Color32F From16BitColor(R5G6B5 color)
        {
            return new Color32F()
            {
                r = ((float)color.R5) * FiveBitFactor,
                g = ((float)color.G6) * SixBitFactor,
                b = ((float)color.B5) * FiveBitFactor,
                a = 1f,
            };
        }
        public Color ToByteColor()
        {
            return Color.FromArgb(
                (int)(a / EightBitFactor),
                (int)(r / EightBitFactor),
                (int)(g / EightBitFactor),
                (int)(b / EightBitFactor)
                );
        }
        public R5G6B5 To16BitColor()
        {
            return new R5G6B5((ushort)(
                Convert.ToUInt16((r / FiveBitFactor)) << 11 |
                Convert.ToUInt16((g / SixBitFactor)) << 5 |
                Convert.ToUInt16((b / FiveBitFactor)) << 0
                ));
        }
        public Color32F Interpolate(Color32F other, float points, float dataPoint)
        {
            float r = 0, g = 0, b = 0, a = 0;
            r = (dataPoint / points) * R + (1f / points) * other.R;
            g = (dataPoint / points) * G + (1f / points) * other.G;
            b = (dataPoint / points) * B + (1f / points) * other.B;
            a = (dataPoint / points) * A + (1f / points) * other.A;
            return new Color32F(r, g, b, a);
        }
        public Color32F GradientTwoOne(Color32F other)
        {
            float r = 0, g = 0, b = 0, a = 0;
            r = 0.6666666f * R + 0.333333333f * other.R;
            g = 0.6666666f * G + 0.333333333f * other.G;
            b = 0.6666666f * B + 0.333333333f * other.B;
            a = 0.6666666f * A + 0.333333333f * other.A;
            return new Color32F(r, g, b, a);
        }
        public Color32F GradientOneHalf(Color32F other)
        {
            float r = 0, g = 0, b = 0, a = 0;
            r = 0.5f * R + 0.5f * other.R;
            g = 0.5f * G + 0.5f * other.G;
            b = 0.5f * B + 0.5f * other.B;
            a = 0.5f * A + 0.5f * other.A;
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
    public struct R5G6B5 : IEquatable<R5G6B5>, IComparable<R5G6B5>, IEquatable<ushort>, IComparable<ushort>
    {
        public static readonly R5G6B5 Zero = new R5G6B5(0);

        private const float ThirtyOneFactor = 255f / 31f;
        private const float SixtyThreeFactor = 255f / 63f;

        public byte R
        {
            get { return (byte)((((value >> 11) & 31) * 255) / 31); }
            set
            {
                ushort c = (byte)(value * ThirtyOneFactor);
                this.value = (ushort)((value & 0x7FF) | (c << 11));
            }
        }
        public byte G
        {
            get { return (byte)((((value >> 5) & 63) * 255) / 63); }
            set
            {
                ushort c = (byte)(value * SixtyThreeFactor);
                this.value = (ushort)((value & 0xF81F) | (c << 5));
            }
        }
        public byte B
        {
            get { return (byte)((((value >> 0) & 31) * 255) / 31); }
            set
            {
                ushort c = (byte)(value * ThirtyOneFactor);
                this.value = (ushort)((value & 0xFFE0) | (c << 0));
            }
        }

        public byte R5
        {
            get { return (byte)((value >> 11) & 31); }
        }
        public byte G6
        {
            get { return (byte)((value >> 5) & 63); }
        }
        public byte B5
        {
            get { return (byte)((value >> 0) & 31); }
        }

        private ushort value;

        public R5G6B5(ushort value)
        {
            this.value = value;
        }
        public R5G6B5(byte r, byte g, byte b)
        {
            byte R = (byte)(r * ThirtyOneFactor);
            byte G = (byte)(g * SixtyThreeFactor);
            byte B = (byte)(b * ThirtyOneFactor);
            value = (ushort)((B) | (G << 5) | (R << 11));
        }

        public R5G6B5 GradientTwoOne(R5G6B5 other)
        {
            byte r = 0, g = 0, b = 0;
            r = (byte)(((2 * R / 3) + (other.R / 3)) / ThirtyOneFactor);
            g = (byte)(((2 * G / 3) + (other.G / 3)) / SixtyThreeFactor);
            b = (byte)(((2 * B / 3) + (other.B / 3)) / ThirtyOneFactor);
            return (ushort)((b) | (g << 5) | (r << 11));
        }
        public R5G6B5 GradientOneHalf(R5G6B5 other)
        {
            byte r = 0, g = 0, b = 0;
            r = (byte)(((R / 3) + (other.R / 2)) / ThirtyOneFactor);
            g = (byte)(((G / 2) + (other.G / 2)) / SixtyThreeFactor);
            b = (byte)(((B / 2) + (other.B / 2)) / ThirtyOneFactor);
            return (ushort)((b) | (g << 5) | (r << 11));
        }
        public byte[] ToBGRA()
        {
            byte[] bgra = new byte[4];
            bgra[0] = B;
            bgra[1] = G;
            bgra[2] = R;
            bgra[3] = 0xFF;
            return bgra;
        }
        public byte[] ToBGRA(byte alpha)
        {
            byte[] bgra = new byte[4];
            bgra[0] = B;
            bgra[1] = G;
            bgra[2] = R;
            bgra[3] = alpha;
            return bgra;
        }
        public byte[] ToBGR()
        {
            byte[] bgr = new byte[3];
            bgr[0] = B;
            bgr[1] = G;
            bgr[2] = R;
            return bgr;
        }
        public int CompareTo(R5G6B5 other)
        {
            return value.CompareTo(other.value);
        }
        public int CompareTo(ushort other)
        {
            return value.CompareTo(other);
        }
        public bool Equals(R5G6B5 other)
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

        public static implicit operator R5G6B5(ushort value)
        {
            return new R5G6B5(value);
        }
        public static implicit operator ushort(R5G6B5 value)
        {
            return value.value;
        }
        public static bool operator >(R5G6B5 c1, R5G6B5 c2)
        {
            return c1.value > c2.value;
        }
        public static bool operator <(R5G6B5 c1, R5G6B5 c2)
        {
            return c1.value < c2.value;
        }
    }
}