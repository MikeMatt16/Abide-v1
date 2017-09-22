using System;
using System.Runtime.InteropServices;

namespace Mode.Halo2
{
    /// <summary>
    /// Represents a 16-bit normalized floating point number.
    /// </summary>
    public struct Normal16 : IComparable<Normal16>, IEquatable<Normal16>
    {
        public const float MinValue = -1f;
        public const float MaxValue = 1f;

        private short value;

        /// <summary>
        /// Creates a new instance of the <see cref="Normal16"/> structure using the supplied signed 16-bit integer value.
        /// </summary>
        /// <param name="value">The signed 16-bit integer value.</param>
        private Normal16(short value)
        {
            this.value = value;
        }
        /// <summary>
        /// Creates a new instance of the <see cref="Normal16"/> structure using the supplied single precision floating point value.
        /// </summary>
        /// <param name="value">The single precision floating point value.</param>
        private Normal16(float value)
        {
            //Check
            if (value < -1f || value > 1f) throw new ArgumentException("Value cannot be less than negative one or greater than one.", nameof(value));

            //Set
            this.value = (short)(Math.Abs(value) * (value < 0f ? short.MinValue : short.MaxValue));
        }
        /// <summary>
        /// Compares this instance to a specified 16-bit normalized floating point number and returns an integer that indicates whether the
        /// value of this instance is less than, equal to, or greater than the value of the specified 16-bit normalized floating point number.
        /// </summary>
        /// <param name="value">A 16-bit normal to compare.</param>
        /// <returns>A signed number indicating the relative values of this instance and <paramref name="value"/>.</returns>
        public int CompareTo(Normal16 value)
        {
            return this.value.CompareTo(value.value);
        }
        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified 16-bit normalized floating point value.
        /// </summary>
        /// <param name="value">A <see cref="Normal16"/> value to compare to this instance.</param>
        /// <returns><see cref="true"/> if <paramref name="value"/> has the same value as this instance; otherwise <see cref="false"/></returns>
        public bool Equals(Normal16 value)
        {
            return this.value.Equals(value.value);
        }
        /// <summary>
        /// Converts a sigle precision floating point number to a normalized 16-bit number.
        /// </summary>
        /// <param name="value">The sigle precision floating point value.</param>
        public static implicit operator Normal16(float value)
        {
            return new Normal16(value);
        }
        /// <summary>
        /// Converts a 16-bit signed integer to a normalized 16-bit number.
        /// </summary>
        /// <param name="value">The 16-bit signed integer value.</param>
        public static explicit operator Normal16(short value)
        {
            return new Normal16(value);
        }
        /// <summary>
        /// Converts a normalized 16-bit number to a single precision floating point value.
        /// </summary>
        /// <param name="value">The 16-bit normalized number to use.</param>
        public static implicit operator float(Normal16 value)
        {
            return Math.Abs(value.value) / (float)(value.value < 0 ? short.MinValue : short.MaxValue);
        }
        /// <summary>
        /// Converts a normalized 16-bit number to a signed 16-bit integer value.
        /// </summary>
        /// <param name="value">The 16-bit normalized number to use.</param>
        public static explicit operator short(Normal16 value)
        {
            return value.value;
        }
        /// <summary>
        /// Converts this instance to a string.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return (Math.Abs(value) / (float)(value < 0 ? short.MinValue : short.MaxValue)).ToString();
        }
    }

    /// <summary>
    /// Represents a value range.
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
        /// Initializes a new instance of the <see cref="Range"/> structure using the supplied minimum and maximum values.
        /// </summary>
        /// <param name="min">The minimum value of the range.</param>
        /// <param name="max">The maximum value of the range.</param>
        public Range(float min, float max)
        {
            this.min = min;
            this.max = max;
        }
        /// <summary>
        /// Returns a string representation of the range.
        /// </summary>
        /// <returns>The range string.</returns>
        public override string ToString()
        {
            return $"{min} to {max}";
        }
    }

    /// <summary>
    /// Represents a normalized 3-component vector.
    /// The absolute value of each component cannot exceed 1.
    /// </summary>
    public struct NormalVector3
    {
        /// <summary>
        /// Represents a zero-value Vector.
        /// </summary>
        public static readonly NormalVector3 Zero = new NormalVector3(0f, 0f, 0f);

        /// <summary>
        /// Gets or sets the X component of the vector.
        /// </summary>
        public float X
        {
            get { return x; }
            set { x = value; }
        }
        /// <summary>
        /// Gets or sets the Y component of the vector.
        /// </summary>
        public float Y
        {
            get { return y; }
            set { y = value; }
        }
        /// <summary>
        /// Gets or sets the Z component of the vector.
        /// </summary>
        public float Z
        {
            get { return z; }
            set { z = value; }
        }

        private Normal16 x, y, z;

        /// <summary>
        /// Initializes a new instance of the <see cref="NormalVector3"/> structure using the supplied x, y, and z components.
        /// </summary>
        /// <param name="x">The x-component of the vector.</param>
        /// <param name="y">The y-component of the vector.</param>
        /// <param name="z">The z-component of the vector.</param>
        /// <exception cref="ArgumentException">Either <paramref name="x"/>, <paramref name="y"/>, or <paramref name="z"/> are outside of valid range. [-1, 1]</exception>
        public NormalVector3(float x, float y, float z)
        {
            //Check
            if (x < -1f || x > 1) throw new ArgumentException("Value is less than negative one or is greater than one.", nameof(x));
            if (y < -1f || y > 1) throw new ArgumentException("Value is less than negative one or is greater than one.", nameof(y));
            if (z < -1f || z > 1) throw new ArgumentException("Value is less than negative one or is greater than one.", nameof(z));

            //Set
            this.x = x;
            this.y = y;
            this.z = z;
        }
        /// <summary>
        /// Inflates the normalized vector to a standard vector using the supplied component compression.
        /// </summary>
        /// <param name="compression">The compression used to scale the components.</param>
        /// <returns>A 3-component vector.</returns>
        public Vector3 Inflate(ComponentCompression compression)
        {
            return new Vector3(compression.Inflate(Component.X, x), compression.Inflate(Component.Y, y), compression.Inflate(Component.Z, z));
        }
        /// <summary>
        /// Converts this vector to a string.
        /// </summary>
        /// <returns>A string</returns>
        public override string ToString()
        {
            return $"({x}, {y}, {z})";
        }
        /// <summary>
        /// Converts this instance to a <see cref="Vector3"/>.
        /// </summary>
        /// <param name="vector">The vector to convert.</param>
        public static implicit operator Vector3(NormalVector3 vector)
        {
            return new Vector3(vector.x, vector.y, vector.z);
        }
    }

    /// <summary>
    /// Represents a normalized 2-component vector.
    /// The absolute value of each component cannot exceed 1.
    /// </summary>
    public struct NormalVector2
    {
        /// <summary>
        /// Represents a zero-value Vector.
        /// </summary>
        public static readonly NormalVector2 Zero = new NormalVector2(0f, 0f);

        /// <summary>
        /// Gets or sets the X component of the vector.
        /// </summary>
        public float X
        {
            get { return x; }
            set { x = value; }
        }
        /// <summary>
        /// Gets or sets the Y component of the vector.
        /// </summary>
        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        private Normal16 x, y;

        /// <summary>
        /// Initializes a new instance of the <see cref="NormalVector2"/> structure using the supplied x and y components.
        /// </summary>
        /// <param name="x">The x-component of the vector.</param>
        /// <param name="y">The y-component of the vector.</param>
        /// <exception cref="ArgumentException">Either <paramref name="x"/> or <paramref name="y"/> are outside of valid range. [-1, 1]</exception>
        public NormalVector2(float x, float y)
        {
            //Check
            if (x < -1f || x > 1) throw new ArgumentException("Value is less than negative one or is greater than one.", nameof(x));
            if (y < -1f || y > 1) throw new ArgumentException("Value is less than negative one or is greater than one.", nameof(y));

            //Set
            this.x = x;
            this.y = y;
        }
        /// <summary>
        /// Inflates the normalized vector to a standard vector using the supplied X and Y component compressions.
        /// </summary>
        /// <param name="compression">The compression used to scale the components.</param>
        /// <returns>A 2-component vector.</returns>
        public Vector2 InflateXY(ComponentCompression compression)
        {
            return new Vector2(compression.Inflate(Component.X, x), compression.Inflate(Component.Y, y));
        }
        /// <summary>
        /// Inflates the normalized vector to a standard vector using the supplied U and V component compressions.
        /// </summary>
        /// <param name="compression">The compression used to scale the components.</param>
        /// <returns>A 2-component vector.</returns>
        public Vector2 InflateUV(ComponentCompression compression)
        {
            return new Vector2(compression.Inflate(Component.U, x), compression.Inflate(Component.V, y));
        }
        /// <summary>
        /// Converts this vector to a string.
        /// </summary>
        /// <returns>A string</returns>
        public override string ToString()
        {
            return $"({x}, {y})";
        }
        /// <summary>
        /// Converts this instance to a <see cref="Vector2"/>.
        /// </summary>
        /// <param name="vector">The vector to convert.</param>
        public static implicit operator Vector2(NormalVector2 vector)
        {
            return new Vector2(vector.x, vector.y);
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
        /// Inflates a 16-bit normalized component value.
        /// </summary>
        /// <param name="component">The component to inflate.</param>
        /// <param name="value">The normalized value.</param>
        /// <returns>A value based on the current component ranges.</returns>
        public float Inflate(Component component, float value)
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
        /// <summary>
        /// Normalizes a component value.
        /// </summary>
        /// <param name="component">The component to normalize.</param>
        /// <param name="value">The value.</param>
        /// <returns>A normalized value.</returns>
        public float Normailze(Component component, float value)
        {
            switch (component)
            {
                case Component.X: return x.Min + (value / (x.Max - x.Min));
                case Component.Y: return y.Min + (value / (y.Max - y.Min));
                case Component.Z: return z.Min + (value / (z.Max - z.Min));
                case Component.U: return u.Min + (value / (u.Max - u.Min));
                case Component.V: return v.Min + (value / (v.Max - v.Min));
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

}
