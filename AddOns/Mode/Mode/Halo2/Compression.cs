using System;
using System.Runtime.InteropServices;

namespace Mode.Halo2
{
    /// <summary>
    /// Represents a 16-bit quantized 3-component vector.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3C
    {
        private const float shortConst = short.MaxValue;
        private const float ushortInverse = 1f / ushort.MaxValue;

        /// <summary>
        /// Gets or sets the 16-bit quantized x-component of this vector.
        /// The absolute value of this value cannot be greater than 1.
        /// </summary>
        /// <exception cref="ArgumentException">absolute value of <paramref name="value"/> cannot be greater than 1.</exception>
        public float X
        {
            get { return (x + shortConst) * ushortInverse; }
            set
            {
                if (Math.Abs(value) > 1f) throw new ArgumentException("Value is greater than expected value.", nameof(value));
                x = (short)((value / ushortInverse) - shortConst);
            }
        }
        /// <summary>
        /// Gets or sets the 16-bit quantized y-component of this vector.
        /// The absolute value of this value cannot be greater than 1.
        /// </summary>
        /// <exception cref="ArgumentException">absolute value of <paramref name="value"/> cannot be greater than 1.</exception>
        public float Y
        {
            get { return (y + shortConst) * ushortInverse; }
            set
            {
                if (Math.Abs(value) > 1f) throw new ArgumentException("Value is greater than expected value.", nameof(value));
                y = (short)((value / ushortInverse) - shortConst);
            }
        }
        /// <summary>
        /// Gets or sets the 16-bit quantized z-component of this vector.
        /// The absolute value of this value cannot be greater than 1.
        /// </summary>
        /// <exception cref="ArgumentException">absolute value of <paramref name="value"/> cannot be greater than 1.</exception>
        public float Z
        {
            get { return (z + shortConst) * ushortInverse; }
            set
            {
                if (Math.Abs(value) > 1f) throw new ArgumentException("Value is greater than expected value.", nameof(value));
                z = (short)((value / ushortInverse) - shortConst);
            }
        }

        private short x, y, z;

        /// <summary>
        /// Initializes a new <see cref="Vector3C"/> structure.
        /// </summary>
        /// <param name="x">The compressed x-component.</param>
        /// <param name="y">The compressed y-component.</param>
        /// <param name="z">The compressed z-component.</param>
        public Vector3C(short x, short y, short z)
        {
            //Set
            this.x = x;
            this.y = y;
            this.z = z;
        }
        /// <summary>
        /// Returns a decompressed vector from this vector.
        /// </summary>
        /// <param name="compression">The vector compression.</param>
        /// <returns>An umcompressed <see cref="Vector3"/> structure.</returns>
        public Vector3 Decompress(ComponentCompression compression)
        {
            return Decompress(this, compression);
        }
        /// <summary>
        /// Returns a decompressed vector from this vector and a component compression structure.
        /// </summary>
        /// <param name="vector">The compressed vector.</param>
        /// <param name="compression">The vector component compression.</param>
        /// <returns>An uncompressed 3-component vector.</returns>
        public static Vector3 Decompress(Vector3C vector, ComponentCompression compression)
        {
            return new Vector3()
            {
                X = compression.Decompress(Component.X, vector.z),
                Y = compression.Decompress(Component.Y, vector.y),
                Z = compression.Decompress(Component.Z, vector.z),
            };
        }
        public static Vector3 operator *(Vector3C vector, ComponentCompression compression)
        {
            return Decompress(vector, compression);
        }
        public static implicit operator Vector3(Vector3C vec)
        {
            return new Vector3(vec.x, vec.y, vec.z);
        }
    }

    /// <summary>
    /// Represents a 16-bit quantized 2-component vector.
    /// </summary>
    public struct Vector2C
    {
        private const float shortConst = short.MaxValue;
        private const float ushortInverse = 1f / ushort.MaxValue;

        /// <summary>
        /// Gets or sets the 16-bit quantized x-component of this vector.
        /// This value cannot have be less than 0.
        /// </summary>
        public float X
        {
            get { return (x + shortConst) * ushortInverse; }
            set
            {
                if (value < 0) throw new InvalidOperationException("Unable to set 16-bit quantized values whose value is less than 0");
                x = (short)((value / ushortInverse) - shortConst);
            }
        }
        /// <summary>
        /// Gets or sets the 16-bit quantized y-component of this vector.
        /// This value cannot have an absolute value > 1.
        /// </summary>
        public float Y
        {
            get { return (y + shortConst) * ushortInverse; }
            set
            {
                if (value < 0) throw new InvalidOperationException("Unable to set 16-bit quantized values whose value is less than 0");
                y = (short)((value / ushortInverse) - shortConst);
            }
        }

        private short x, y;

        /// <summary>
        /// Initializes a new <see cref="Vector2C"/> structure.
        /// </summary>
        /// <param name="x">The compressed x-component.</param>
        /// <param name="y">The compressed y-component.</param>
        public Vector2C(short x, short y)
        {
            //Set
            this.x = x;
            this.y = y;
        }
        /// <summary>
        /// Returns a decompressed vector (X, Y) from this vector.
        /// </summary>
        /// <param name="compression">The vector compression.</param>
        /// <returns>An umcompressed <see cref="Vector2"/> structure.</returns>
        public Vector2 DecompressXY(ComponentCompression compression)
        {
            return new Vector2()
            {
                X = compression.Decompress(Component.X, x),
                Y = compression.Decompress(Component.Y, y),
            };
        }
        /// <summary>
        /// Returns a decompressed vector (U, V) from this vector.
        /// </summary>
        /// <param name="compression">The vector compression.</param>
        /// <returns>An umcompressed <see cref="Vector2"/> structure.</returns>
        public Vector2 DecompressUV(ComponentCompression compression)
        {
            return new Vector2()
            {
                X = compression.Decompress(Component.U, x),
                Y = compression.Decompress(Component.V, y),
            };
        }

        public static implicit operator Vector2(Vector2C vec)
        {
            return new Vector2(vec.x, vec.y);
        }
    }

    /// <summary>
    /// Represents an X-Y-Z-U-V compression range structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ComponentCompression
    {
        private const float shortConst = short.MaxValue;
        private const float ushortInverse = 1f / ushort.MaxValue;

        /// <summary>
        /// Gets or sets the X-component range.
        /// </summary>
        public Range X
        {
            get { return x; }
            set { x = value; }
        }
        /// <summary>
        /// Gets or sets the Y-component range.
        /// </summary>
        public Range Y
        {
            get { return y; }
            set { y = value; }
        }
        /// <summary>
        /// Gets or sets the Z-component range.
        /// </summary>
        public Range Z
        {
            get { return z; }
            set { z = value; }
        }
        /// <summary>
        /// Gets or sets the U-component range.
        /// </summary>
        public Range U
        {
            get { return u; }
            set { u = value; }
        }
        /// <summary>
        /// Gets or sets the V-component range.
        /// </summary>
        public Range V
        {
            get { return v; }
            set { v = value; }
        }

        private Range x, y, z, u, v;

        /// <summary>
        /// Decompresses a compressed 16-bit quantized component value.
        /// </summary>
        /// <param name="component">The component to decompress.</param>
        /// <param name="value">The uncompressed value.</param>
        /// <returns>A normalized, uncompressed value based on the current component ranges.</returns>
        public float Decompress(Component component, float value)
        {
            switch (component)
            {
                case Component.X: return (value + shortConst) * ushortInverse + (X.Max - x.Min) + x.Min;
                case Component.Y: return (value + shortConst) * ushortInverse + (Y.Max - y.Min) + y.Min;
                case Component.Z: return (value + shortConst) * ushortInverse + (Z.Max - z.Min) + z.Min;
                case Component.U: return (value + shortConst) * ushortInverse + (U.Max - u.Min) + u.Min;
                case Component.V: return (value + shortConst) * ushortInverse + (V.Max - v.Min) + v.Min;
                default: return 0;
            }
        }
    }

    /// <summary>
    /// Represents an enumeration containing compressable components.
    /// </summary>
    public enum Component : byte
    {
        /// <summary>
        /// The X-component.
        /// </summary>
        X,
        /// <summary>
        /// The Y-component.
        /// </summary>
        Y,
        /// <summary>
        /// The Z-component.
        /// </summary>
        Z,
        /// <summary>
        /// The U-component.
        /// </summary>
        U,
        /// <summary>
        /// The Z-component.
        /// </summary>
        V
    }

    /// <summary>
    /// Represents a single-precision floating point range.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Range
    {
        /// <summary>
        /// The minimum value of the range.
        /// </summary>
        public float Min
        {
            get { return min; }
            set { min = value; }
        }
        /// <summary>
        /// The maximum value of the range.
        /// </summary>
        public float Max
        {
            get { return max; }
            set { max = value; }
        }

        private float min, max;

        /// <summary>
        /// Initializes a new <see cref="Range"/> structure.
        /// </summary>
        /// <param name="min">The minimum value of the range.</param>
        /// <param name="max">The maximum value of the range.</param>
        public Range(float min, float max)
        {
            this.min = min;
            this.max = max;
        }
    }
}
