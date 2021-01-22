using System;
using System.Runtime.InteropServices;
using Abide.HaloLibrary;

namespace Abide.Tag
{
    public struct EmptyStructure { }
    #region Vertex Buffer
    /// <summary>
    /// Represents a vertex buffer.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct VertexBuffer
    {
        /// <summary>
        /// Gets or sets the vertex attribute type.
        /// </summary>
        public VertexAttributeType Type { get; }

        private byte b0, b1, b2, b3, b4, b5, b6, b7, b8, b9, b10, b11, b12, b13, b14, b15, b16, b17, b18, b19, b20, b21, b22, b23, b24, b25, b26, b27, b28, b29;

        /// <summary>
        /// Returns the vertex buffer.
        /// </summary>
        /// <returns>An array of <see cref="byte"/> element.</returns>
        public byte[] GetBuffer()
        {
            return new byte[] {
                b0, b1, b2, b3, b4,
                b5, b6, b7, b8, b9,
                b10, b11, b12, b13, b14,
                b15, b16, b17, b18, b19,
                b20, b21, b22, b23, b24,
                b25, b26, b27, b28, b29,
            };
        }
        /// <summary>
        /// Sets the vertex buffer.
        /// </summary>
        /// <param name="buffer">The vertex buffer.</param>
        public void SetBuffer(byte[] buffer)
        {
            //Check
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
            if (buffer.Length < 30) throw new ArgumentException("Invalid buffer", nameof(buffer));

            //Set
            b0 = buffer[0];
            b1 = buffer[1];
            b2 = buffer[2];
            b3 = buffer[3];
            b4 = buffer[4];
            b5 = buffer[5];
            b6 = buffer[6];
            b7 = buffer[7];
            b8 = buffer[8];
            b9 = buffer[9];
            b10 = buffer[10];
            b11 = buffer[11];
            b12 = buffer[12];
            b13 = buffer[13];
            b14 = buffer[14];
            b15 = buffer[15];
            b16 = buffer[16];
            b17 = buffer[17];
            b18 = buffer[18];
            b19 = buffer[19];
            b20 = buffer[20];
            b21 = buffer[21];
            b22 = buffer[22];
            b23 = buffer[23];
            b24 = buffer[24];
            b25 = buffer[25];
            b26 = buffer[26];
            b27 = buffer[27];
            b28 = buffer[28];
            b29 = buffer[29];
        }
    }
    /// <summary>
    /// Represents an enumeration containing vertex attribute types.
    /// </summary>
    public enum VertexAttributeType : short
    {
        None = 0x0000,
        CoordinateFloat = 0x010C,
        CoordinateCompressed = 0x0206,
        CoordinateWithSingleNode = 0x0408,
        CoordinateWithDoubleNode = 0x060C,
        CoordinateWithTripleNode = 0x080C,

        TextureCoordinateFloatPc = 0x1708,
        TextureCoordinateFloat = 0x1808,
        TextureCoordinateCompressed = 0x1904,

        TangentSpaceUnitVectorsFloat = 0x1924,
        TangentSpaceUnitVectorsCompressed = 0x1B0C,

        LightmapUVCoordinateOne = 0x1F08,
        LightmapUVCoordinateOneXbox = 0x1F04,
        LightmapUVCoordinateTwo = 0x3008,
        LightmapUVCoordinateTwoXbox = 0x3004,

        DiffuseColour = 0x350C,

        UnpackedWorldCoordinateData = 0x2120,
        UnpackedTextureCoordinateData = 0x2208,
        UnpackedLightingData = 0x2324,
    }
    #endregion
    #region Tags
    /// <summary>
    /// Represents a tag reference.
    /// </summary>
    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct TagReference : IEquatable<TagReference>
    {
        /// <summary>
        /// Represents a null tag reference.
        /// This value is read-only.
        /// </summary>
        public static readonly TagReference Null = new TagReference() { Tag = "null", Id = TagId.Null };
        /// <summary>
        /// Gets or sets the tag group.
        /// </summary>
        public TagFourCc Tag { get; set; }
        /// <summary>
        /// Gets or sets the tag ID.
        /// </summary>
        public TagId Id { get; set; }

        /// <summary>
        /// Determines whether this instance and another specified <see cref="TagReference"/> have the same value.
        /// </summary>
        /// <param name="reference">The object to compare with the current instance.</param>
        /// <returns><see langword="true"/> if this tag reference and <paramref name="reference"/> are equal; otherwise <see langword="false"/>.</returns>
        public bool Equals(TagReference reference)
        {
            return Tag.Equals(reference.Tag) && Id.Equals(reference.Id);
        }
        /// <summary>
        /// Gets a string representation of this tag reference.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"{Tag} {Id}";
        }
    }
    #endregion
    #region Bounds
    /// <summary>
    /// Represents a short boundaries value.
    /// </summary>
    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct ShortBounds : IEquatable<ShortBounds>
    {
        /// <summary>
        /// Represents a zero-value bounds.
        /// This value is read-only.
        /// </summary>
        public static readonly ShortBounds Zero = new ShortBounds(0, 0);

        /// <summary>
        /// Gets or sets the minimum value in the boundaries.
        /// </summary>
        public short Min { get; }
        /// <summary>
        /// Gets or sets the maximum value in the boundaries.
        /// </summary>
        public short Max { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShortBounds"/> structure.
        /// </summary>
        /// <param name="min">The minumum value.</param>
        /// <param name="max">The maximum value.</param>
        public ShortBounds(short min, short max)
        {
            Min = min;
            Max = max;
        }
        /// <summary>
        /// Returns a value indicating whether this bounds and a specified <see cref="ShortBounds"/> represent the same value.
        /// </summary>
        /// <param name="bounds">The other color.</param>
        /// <returns><see langword="true"/> if the this bounds and <paramref name="bounds"/> are equal; otherwise <see langword="false"/>.</returns>
        public bool Equals(ShortBounds bounds)
        {
            return Min.Equals(bounds.Min) && Max.Equals(bounds.Max);
        }
        /// <summary>
        /// Gets and returns a string representation if this bounds value.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"{Min} - {Max}";
        }
    }
    /// <summary>
    /// Represents a floating point boundaries value.
    /// </summary>
    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct FloatBounds : IEquatable<FloatBounds>
    {
        /// <summary>
        /// Represents a zero-value bounds.
        /// This value is read-only.
        /// </summary>
        public static readonly FloatBounds Zero = new FloatBounds(0, 0);

        /// <summary>
        /// Gets or sets the minimum value in the boundaries.
        /// </summary>
        public float Min { get; }
        /// <summary>
        /// Gets or sets the maximum value in the boundaries.
        /// </summary>
        public float Max { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FloatBounds"/> structure.
        /// </summary>
        /// <param name="min">The minumum value.</param>
        /// <param name="max">The maximum value.</param>
        public FloatBounds(float min, float max)
        {
            Min = min;
            Max = max;
        }
        /// <summary>
        /// Returns a value indicating whether this bounds and a specified <see cref="FloatBounds"/> represent the same value.
        /// </summary>
        /// <param name="bounds">The other color.</param>
        /// <returns><see langword="true"/> if the this bounds and <paramref name="bounds"/> are equal; otherwise <see langword="false"/>.</returns>
        public bool Equals(FloatBounds bounds)
        {
            return Min.Equals(bounds.Min) && Max.Equals(bounds.Max);
        }
        /// <summary>
        /// Gets and returns a string representation if this bounds value.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"{Min} - {Max}";
        }
    }
    #endregion
    #region Colors
    /// <summary>
    /// Represents a RGB color.
    /// </summary>
    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct ColorRgb : IEquatable<ColorRgb>
    {
        /// <summary>
        /// Represents a zero-value (or black) color.
        /// This value is read-only.
        /// </summary>
        public static readonly ColorRgb Zero = new ColorRgb(0, 0, 0);

        /// <summary>
        /// Gets or sets the red component of the color.
        /// </summary>
        public byte Red { get; }
        /// <summary>
        /// Gets or sets the green component of the color.
        /// </summary>
        public byte Green { get; }
        /// <summary>
        /// Gets or sets the blue component of the color.
        /// </summary>
        public byte Blue { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorRgb"/> structure.
        /// </summary>
        /// <param name="r">The red component.</param>
        /// <param name="g">The green component.</param>
        /// <param name="b">The blue component.</param>
        public ColorRgb(byte r, byte g, byte b)
        {
            Red = r;
            Green = g;
            Blue = b;
        }
        /// <summary>
        /// Returns a value indicating whether this color and a specified <see cref="ColorRgb"/> represent the same value.
        /// </summary>
        /// <param name="color">The other color.</param>
        /// <returns><see langword="true"/> if the this color and <paramref name="color"/> are equal; otherwise <see langword="false"/>.</returns>
        public bool Equals(ColorRgb color)
        {
            return Red.Equals(color.Red) && Green.Equals(color.Green) && Blue.Equals(color.Blue);
        }
        /// <summary>
        /// Returns a string representation of this color.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"R: {Red} G: {Green} B: {Blue} (#{Red:x2}{Green:x2}{Blue:x2})";
        }
    }
    /// <summary>
    /// Represents an ARGB color.
    /// </summary>
    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct ColorArgb : IEquatable<ColorArgb>
    {
        /// <summary>
        /// Represents a zero-value color.
        /// This value is read-only.
        /// </summary>
        public static readonly ColorArgb Zero = new ColorArgb(0, 0, 0, 0);

        /// <summary>
        /// Gets or sets the alpha component of the color.
        /// </summary>
        public byte Alpha { get; }
        /// <summary>
        /// Gets or sets the red component of the color.
        /// </summary>
        public byte Red { get; }
        /// <summary>
        /// Gets or sets the green component of the color.
        /// </summary>
        public byte Green { get; }
        /// <summary>
        /// Gets or sets the blue component of the color.
        /// </summary>
        public byte Blue { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorArgb"/> structure.
        /// </summary>
        /// <param name="a">The alpha component.</param>
        /// <param name="r">The red component.</param>
        /// <param name="g">The green component.</param>
        /// <param name="b">The blue component.</param>
        public ColorArgb(byte a, byte r, byte g, byte b)
        {
            Alpha = a;
            Red = r;
            Green = g;
            Blue = b;
        }
        /// <summary>
        /// Returns a value indicating whether this color and a specified <see cref="ColorArgb"/> represent the same value.
        /// </summary>
        /// <param name="color">The other color.</param>
        /// <returns><see langword="true"/> if the this color and <paramref name="color"/> are equal; otherwise <see langword="false"/>.</returns>
        public bool Equals(ColorArgb color)
        {
            return Alpha.Equals(color.Alpha) && Red.Equals(color.Red) && Green.Equals(color.Green) && Blue.Equals(color.Blue);
        }
        /// <summary>
        /// Returns a string representation of this color.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"A: {Alpha} R: {Red} G: {Green} B: {Blue} (#{Alpha:x2}{Red:x2}{Green:x2}{Blue:x2})";
        }
    }
    /// <summary>
    /// Represents a floating point RGB color.
    /// </summary>
    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct ColorRgbF : IEquatable<ColorRgbF>
    {
        /// <summary>
        /// Represents a zero-value (or black) color.
        /// This value is read-only.
        /// </summary>
        public static readonly ColorRgbF Zero = new ColorRgbF(0, 0, 0);

        /// <summary>
        /// Gets or sets the red component of the color.
        /// </summary>
        public float Red { get; }
        /// <summary>
        /// Gets or sets the green component of the color.
        /// </summary>
        public float Green { get; }
        /// <summary>
        /// Gets or sets the blue component of the color.
        /// </summary>
        public float Blue { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorRgb"/> structure.
        /// </summary>
        /// <param name="r">The red component.</param>
        /// <param name="g">The green component.</param>
        /// <param name="b">The blue component.</param>
        public ColorRgbF(float r, float g, float b)
        {
            Red = r;
            Green = g;
            Blue = b;
        }
        /// <summary>
        /// Returns a value indicating whether this color and a specified <see cref="ColorRgbF"/> represent the same value.
        /// </summary>
        /// <param name="color">The other color.</param>
        /// <returns><see langword="true"/> if the this color and <paramref name="color"/> are equal; otherwise <see langword="false"/>.</returns>
        public bool Equals(ColorRgbF color)
        {
            return Red.Equals(color.Red) && Green.Equals(color.Green) && Blue.Equals(color.Blue);
        }
        /// <summary>
        /// Returns a string representation of this color.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"R: {Red} G: {Green} B: {Blue}";
        }
    }
    /// <summary>
    /// Represents a floating point ARGB color.
    /// </summary>
    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct ColorArgbF : IEquatable<ColorArgbF>
    {
        /// <summary>
        /// Represents a zero-value (or black) color.
        /// This value is read-only.
        /// </summary>
        public static readonly ColorArgbF Zero = new ColorArgbF(0, 0, 0, 0);

        /// <summary>
        /// Gets or sets the alpha component of the color.
        /// </summary>
        public float Alpha { get; }
        /// <summary>
        /// Gets or sets the red component of the color.
        /// </summary>
        public float Red { get; }
        /// <summary>
        /// Gets or sets the green component of the color.
        /// </summary>
        public float Green { get; }
        /// <summary>
        /// Gets or sets the blue component of the color.
        /// </summary>
        public float Blue { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorRgb"/> structure.
        /// </summary>
        /// <param name="a">The alpha component.</param>
        /// <param name="r">The red component.</param>
        /// <param name="g">The green component.</param>
        /// <param name="b">The blue component.</param>
        public ColorArgbF(float a, float r, float g, float b)
        {
            Alpha = a;
            Red = r;
            Green = g;
            Blue = b;
        }
        /// <summary>
        /// Returns a value indicating whether this color and a specified <see cref="ColorArgbF"/> represent the same value.
        /// </summary>
        /// <param name="color">The other color.</param>
        /// <returns><see langword="true"/> if the this color and <paramref name="color"/> are equal; otherwise <see langword="false"/>.</returns>
        public bool Equals(ColorArgbF color)
        {
            return Alpha.Equals(color.Alpha) && Red.Equals(color.Red) && Green.Equals(color.Green) && Blue.Equals(color.Blue);
        }
        /// <summary>
        /// Returns a string representation of this color.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"A: {Alpha} R: {Red} G: {Green} B: {Blue}";
        }
    }
    /// <summary>
    /// Represents a floating point HSV color.
    /// </summary>
    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct ColorHsv : IEquatable<ColorHsv>
    {
        /// <summary>
        /// Represents a zero-value color.
        /// </summary>
        public static ColorHsv Zero = new ColorHsv(0, 0, 0);

        /// <summary>
        /// Gets or sets the hue of the color.
        /// </summary>
        public float Hue { get; }
        /// <summary>
        /// Gets or sets the saturation of the color.
        /// </summary>
        public float Saturation { get; }
        /// <summary>
        /// Gets or sets the value of the color.
        /// </summary>
        public float Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorHsv"/> structure using the specified hue, saturation, and value values.
        /// </summary>
        /// <param name="hue">The hue.</param>
        /// <param name="saturation">The saturation.</param>
        /// <param name="value">The value.</param>
        public ColorHsv(float hue, float saturation, float value)
        {
            Hue = hue;
            Saturation = saturation;
            Value = value;
        }
        /// <summary>
        /// Returns a value indicating whether this color and a specified <see cref="ColorHsv"/> represent the same value.
        /// </summary>
        /// <param name="color">The other color.</param>
        /// <returns><see langword="true"/> if the this color and <paramref name="color"/> are equal; otherwise <see langword="false"/>.</returns>
        public bool Equals(ColorHsv color)
        {
            return Hue.Equals(color.Hue) && Saturation.Equals(color.Saturation) && Value.Equals(color.Value);
        }
    }
    /// <summary>
    /// Represents a floating point AHSV color.
    /// </summary>
    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct ColorAhsv : IEquatable<ColorAhsv>
    {
        /// <summary>
        /// Represents a zero-value color.
        /// </summary>
        public static ColorAhsv Zero = new ColorAhsv(0, 0, 0, 0);

        /// <summary>
        /// Gets or sets the alpha.
        /// </summary>
        public float Alpha { get; }
        /// <summary>
        /// Gets or sets the hue of the color.
        /// </summary>
        public float Hue { get; }
        /// <summary>
        /// Gets or sets the saturation of the color.
        /// </summary>
        public float Saturation { get; }
        /// <summary>
        /// Gets or sets the value of the color.
        /// </summary>
        public float Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorAhsv"/> structure using the specified hue, saturation, and value values.
        /// </summary>
        /// <param name="alpha">The alpha.</param>
        /// <param name="hue">The hue.</param>
        /// <param name="saturation">The saturation.</param>
        /// <param name="value">The value.</param>
        public ColorAhsv(float alpha, float hue, float saturation, float value)
        {
            Alpha = alpha;
            Hue = hue;
            Saturation = saturation;
            Value = value;
        }
        /// <summary>
        /// Returns a value indicating whether this color and a specified <see cref="ColorHsv"/> represent the same value.
        /// </summary>
        /// <param name="color">The other color.</param>
        /// <returns><see langword="true"/> if the this color and <paramref name="color"/> are equal; otherwise <see langword="false"/>.</returns>
        public bool Equals(ColorAhsv color)
        {
            return Alpha.Equals(color.Alpha) && Hue.Equals(color.Hue) && Saturation.Equals(color.Saturation) && Value.Equals(color.Value);
        }
    }
    #endregion
    #region Vectors and Quaternion
    /// <summary>
    /// Represents a quaternion.
    /// </summary>
    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct Quaternion : IEquatable<Quaternion>
    {
        /// <summary>
        /// Represents a zero value quaternion.
        /// This value is read-only.
        /// </summary>
        public static readonly Quaternion Zero = new Quaternion() { W = 1, I = 0, J = 0, K = 0 };

        /// <summary>
        /// Gets the w-component of the quaternion.
        /// </summary>
        public float W { get; set; }
        /// <summary>
        /// Gets the i-component of the quaternion.
        /// </summary>
        public float I { get; set; }
        /// <summary>
        /// Gets the j-component of the quaternion.
        /// </summary>
        public float J { get; set; }
        /// <summary>
        /// Gets the k-component of the quaternion.
        /// </summary>
        public float K { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Quaternion"/> structure using the supplied component values.
        /// </summary>
        /// <param name="w">The w-component.</param>
        /// <param name="i">The i-component.</param>
        /// <param name="j">The j-component.</param>
        /// <param name="k">The k-component.</param>
        public Quaternion(float w, float i, float j, float k)
        {
            W = w;
            I = i;
            J = j;
            K = k;
        }
        /// <summary>
        /// Gets a string representation of this quaternion.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"{W}, {I}, {J}, {K}";
        }
        /// <summary>
        /// Returns a value indicating whether this quaternion and a specified <see cref="Quaternion"/> represent the same value.
        /// </summary>
        /// <param name="quaternion">The other quaternion.</param>
        /// <returns><see langword="true"/> if the this quaternion and <paramref name="quaternion"/> are equal; otherwise <see langword="false"/>.</returns>
        public bool Equals(Quaternion quaternion)
        {
            return W.Equals(quaternion.W) && I.Equals(quaternion.I) && J.Equals(quaternion.J) && K.Equals(quaternion.K);
        }
    }
    /// <summary>
    /// Represents a vector with four components.
    /// </summary>
    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct Vector4 : IEquatable<Vector4>
    {
        /// <summary>
        /// Represents a zero vector.
        /// This value is read-only.
        /// </summary>
        public static readonly Vector4 Zero = new Vector4();

        /// <summary>
        /// Gets and returns the magnitude of this vector.
        /// </summary>
        public float Magnitude
        {
            get { return (float)Math.Sqrt(Dot(this, this)); }
        }
        /// <summary>
        /// Gets and returns the squared magnitude of this vector.
        /// </summary>
        public float MagnitudeSquared
        {
            get { return Dot(this, this); }
        }
        /// <summary>
        /// Gets or sets the the i-component of this vector.
        /// </summary>
        public float I { get; set; }
        /// <summary>
        /// Gets or sets the the j-component of this vector.
        /// </summary>
        public float J { get; set; }
        /// <summary>
        /// Gets or sets the the k-component of this vector.
        /// </summary>
        public float K { get; set; }
        /// <summary>
        /// Gets or sets the the w-component of this vector.
        /// </summary>
        public float W { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4"/> structure using the supplied i, j, k, and w component values.
        /// </summary>
        /// <param name="i">The i-component.</param>
        /// <param name="j">The j-component.</param>
        /// <param name="k">The k-component.</param>
        /// <param name="w">The w-component.</param>
        public Vector4(float i, float j, float k, float w)
        {
            I = i;
            J = j;
            K = k;
            W = w;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4"/> structure using the supplied i, j, and k component values.
        /// </summary>
        /// <param name="i">The i-component.</param>
        /// <param name="j">The j-component.</param>
        /// <param name="k">The k-component.</param>
        public Vector4(float i, float j, float k)
        {
            I = i;
            J = j;
            K = k;
            W = 0;
        }
        /// <summary>
        /// Initializes a new <see cref="Vector4"/> structure using the supplied i and j component values.
        /// </summary>
        /// <param name="i">The i-component.</param>
        /// <param name="j">The j-component.</param>
        public Vector4(float i, float j)
        {
            I = i;
            J = j;
            K = 0;
            W = 0;
        }
        /// <summary>
        /// Returns a value indicating whether this vector and a specified <see cref="Vector4"/> represent the same value.
        /// </summary>
        /// <param name="vector">The other vector.</param>
        /// <returns><see langword="true"/> if the this vector and <paramref name="vector"/> are equal; otherwise <see langword="false"/>.</returns>
        public bool Equals(Vector4 vector)
        {
            return I.Equals(vector.I) && J.Equals(vector.J) && K.Equals(vector.K) && W.Equals(vector.W);
        }
        /// <summary>
        /// Calculates the dot product of this vector and a supplied vector.
        /// </summary>
        /// <param name="vector">The vector to dot this vector with.</param>
        /// <returns>The dot product.</returns>
        public float Dot(Vector4 vector)
        {
            return Dot(this, vector);
        }
        /// <summary>
        /// Returns a string representation of this vector.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"{I}, {J}, {K}, {W}";
        }

        /// <summary>
        /// Calculates the dot product of two supplied vectors.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>The dot product.</returns>
        public static float Dot(Vector4 v1, Vector4 v2)
        {
            return v1.I * v2.I + v1.J + v2.J * v1.K * v2.K + v1.W * v2.W;
        }
        /// <summary>
        /// Multiplies a vector and a scalar. The resulting vector's components will be the product of the vector components and the scalar value.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>A vector.</returns>
        public static Vector4 operator *(Vector4 vector, float scalar)
        {
            return new Vector4(vector.I * scalar, vector.J * scalar, vector.K * scalar, vector.W * scalar);
        }
        /// <summary>
        /// Divides a vector by a scalar. The resulting vector's components will be the quotient of the vector components and the scalar value.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>A vector.</returns>
        public static Vector4 operator /(Vector4 vector, float scalar)
        {
            return new Vector4(vector.I / scalar, vector.J / scalar, vector.K / scalar, vector.W / scalar);
        }
        /// <summary>
        /// Adds a vector and a scalar. The resulting vector's components will be the sum of the vector components and the scalar value.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>A vector.</returns>
        public static Vector4 operator +(Vector4 vector, float scalar)
        {
            return new Vector4(vector.I + scalar, vector.J + scalar, vector.K + scalar, vector.W + scalar);
        }
        /// <summary>
        /// Subtracts a vector and a scalar. The resulting vector's components will be the difference of the vector components and the scalar value.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>A vector.</returns>
        public static Vector4 operator -(Vector4 vector, float scalar)
        {
            return new Vector4(vector.I - scalar, vector.J - scalar, vector.K - scalar, vector.W - scalar);
        }
        /// <summary>
        /// Multiplies two vectors. The resulting vector's components will be the product of the first vector's components and the second vector's components.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>A vector.</returns>
        public static Vector4 operator *(Vector4 v1, Vector4 v2)
        {
            return new Vector4(v1.I * v2.I, v1.J * v2.J, v1.K * v2.K, v1.W * v2.W);
        }
        /// <summary>
        /// Divides two vectors. The resulting vector's components will be the quotient of the first vector's components and the second vector's components.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>A vector.</returns>
        public static Vector4 operator /(Vector4 v1, Vector4 v2)
        {
            return new Vector4(v1.I / v2.I, v1.J / v2.J, v1.K / v2.K, v1.W / v2.W);
        }
        /// <summary>
        /// Adds two vectors. The resulting vector's components will be the sum of the first vector's components and the second vector's components.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>A vector.</returns>
        public static Vector4 operator +(Vector4 v1, Vector4 v2)
        {
            return new Vector4(v1.I + v2.I, v1.J + v2.J, v1.K + v2.K, v1.W + v2.W);
        }
        /// <summary>
        /// Subracts two vectors. The resulting vector's components will be the difference of the first vector's components and the second vector's components.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>A vector.</returns>
        public static Vector4 operator -(Vector4 v1, Vector4 v2)
        {
            return new Vector4(v1.I - v2.I, v1.J - v2.J, v1.K - v2.K, v1.W - v2.W);
        }
    }
    /// <summary>
    /// Represents a vector with three components.
    /// </summary>
    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct Vector3 : IEquatable<Vector3>
    {
        /// <summary>
        /// Represents a zero vector.
        /// This value is read-only.
        /// </summary>
        public static readonly Vector3 Zero = new Vector3();

        /// <summary>
        /// Gets and returns the magnitude of this vector.
        /// </summary>
        public float Magnitude
        {
            get { return (float)Math.Sqrt(Dot(this, this)); }
        }
        /// <summary>
        /// Gets and returns the squared magnitude of this vector.
        /// </summary>
        public float MagnitudeSquared
        {
            get { return Dot(this, this); }
        }
        /// <summary>
        /// Gets or sets the the i-component of this vector.
        /// </summary>
        public float I { get; set; }
        /// <summary>
        /// Gets or sets the the j-component of this vector.
        /// </summary>
        public float J { get; set; }
        /// <summary>
        /// Gets or sets the the k-component of this vector.
        /// </summary>
        public float K { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3"/> structure using the supplied i, j, and k component values.
        /// </summary>
        /// <param name="i">The i-component.</param>
        /// <param name="j">The j-component.</param>
        /// <param name="k">The k-component.</param>
        public Vector3(float i, float j, float k)
        {
            I = i;
            J = j;
            K = k;
        }
        /// <summary>
        /// Initializes a new <see cref="Vector3"/> structure using the supplied i and j component values.
        /// </summary>
        /// <param name="i">The i-component.</param>
        /// <param name="j">The j-component.</param>
        public Vector3(float i, float j)
        {
            I = i;
            J = j;
            K = 0;
        }
        /// <summary>
        /// Returns a value indicating whether this vector and a specified <see cref="Vector3"/> represent the same value.
        /// </summary>
        /// <param name="vector">The other vector.</param>
        /// <returns>True if the this vector and <paramref name="vector"/> are equal; otherwise false.</returns>
        public bool Equals(Vector3 vector)
        {
            return I.Equals(vector.I) && J.Equals(vector.J) && K.Equals(vector.K);
        }
        /// <summary>
        /// Calculates the dot product of this vector and a supplied vector.
        /// </summary>
        /// <param name="vector">The vector to dot this vector with.</param>
        /// <returns>The dot product.</returns>
        public float Dot(Vector3 vector)
        {
            return Dot(this, vector);
        }
        /// <summary>
        /// Calculates the cross product of this vector and a supplied vector.
        /// </summary>
        /// <param name="vector">The vector to cross multiply with this vector with</param>
        /// <returns></returns>
        public Vector3 Cross(Vector3 vector)
        {
            return Cross(this, vector);
        }
        /// <summary>
        /// Returns a string representation of this vector.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"{I}, {J}, {K}";
        }
        /// <summary>
        /// Converts this vector to a 3-dimensional point.
        /// </summary>
        /// <returns>A new <see cref="Point3F"/> value.</returns>
        public Point3F ToPoint()
        {
            return new Point3F(I, J, K);
        }

        /// <summary>
        /// Calculates the dot product of two supplied vectors.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>The dot product.</returns>
        public static float Dot(Vector3 v1, Vector3 v2)
        {
            return v1.I * v2.I + v1.J + v2.J * v1.K * v2.K;
        }
        /// <summary>
        /// Calculates the cross product of two supplied vectors.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Vector3 Cross(Vector3 v1, Vector3 v2)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Multiplies a vector and a scalar. The resulting vector's components will be the product of the vector components and the scalar value.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>A vector.</returns>
        public static Vector3 operator *(Vector3 vector, float scalar)
        {
            return new Vector3(vector.I * scalar, vector.J * scalar, vector.K * scalar);
        }
        /// <summary>
        /// Divides a vector by a scalar. The resulting vector's components will be the quotient of the vector components and the scalar value.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>A vector.</returns>
        public static Vector3 operator /(Vector3 vector, float scalar)
        {
            return new Vector3(vector.I / scalar, vector.J / scalar, vector.K / scalar);
        }
        /// <summary>
        /// Adds a vector and a scalar. The resulting vector's components will be the sum of the vector components and the scalar value.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>A vector.</returns>
        public static Vector3 operator +(Vector3 vector, float scalar)
        {
            return new Vector3(vector.I + scalar, vector.J + scalar, vector.K + scalar);
        }
        /// <summary>
        /// Subtracts a vector and a scalar. The resulting vector's components will be the difference of the vector components and the scalar value.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>A vector.</returns>
        public static Vector3 operator -(Vector3 vector, float scalar)
        {
            return new Vector3(vector.I - scalar, vector.J - scalar, vector.K - scalar);
        }
        /// <summary>
        /// Multiplies two vectors. The resulting vector's components will be the product of the first vector's components and the second vector's components.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>A vector.</returns>
        public static Vector3 operator *(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.I * v2.I, v1.J * v2.J, v1.K * v2.K);
        }
        /// <summary>
        /// Divides two vectors. The resulting vector's components will be the quotient of the first vector's components and the second vector's components.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>A vector.</returns>
        public static Vector3 operator /(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.I / v2.I, v1.J / v2.J, v1.K / v2.K);
        }
        /// <summary>
        /// Adds two vectors. The resulting vector's components will be the sum of the first vector's components and the second vector's components.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>A vector.</returns>
        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.I + v2.I, v1.J + v2.J, v1.K + v2.K);
        }
        /// <summary>
        /// Subracts two vectors. The resulting vector's components will be the difference of the first vector's components and the second vector's components.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>A vector.</returns>
        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.I - v2.I, v1.J - v2.J, v1.K - v2.K);
        }
    }
    /// <summary>
    /// Represents a vector with two components.
    /// </summary>
    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct Vector2 : IEquatable<Vector2>
    {
        /// <summary>
        /// Represents a zero vector.
        /// This value is read-only.
        /// </summary>
        public static readonly Vector2 Zero = new Vector2();

        /// <summary>
        /// Gets and returns the magnitude of this vector.
        /// </summary>
        public float Magnitude
        {
            get { return (float)Math.Sqrt(Dot(this, this)); }
        }
        /// <summary>
        /// Gets and returns the squared magnitude of this vector.
        /// </summary>
        public float MagnitudeSquared
        {
            get { return Dot(this, this); }
        }
        /// <summary>
        /// Gets or sets the the i-component of this vector.
        /// </summary>
        public float I { get; set; }
        /// <summary>
        /// Gets or sets the the j-component of this vector.
        /// </summary>
        public float J { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2"/> structure using the supplied i and j component values.
        /// </summary>
        /// <param name="i">The i-component.</param>
        /// <param name="j">The j-component.</param>
        public Vector2(float i, float j)
        {
            I = i;
            J = j;
        }
        /// <summary>
        /// Returns a value indicating whether this vector and a specified <see cref="Vector2"/> represent the same value.
        /// </summary>
        /// <param name="vector">The other vector.</param>
        /// <returns>True if the this vector and <paramref name="vector"/> are equal; otherwise false.</returns>
        public bool Equals(Vector2 vector)
        {
            return I.Equals(vector.I) && J.Equals(vector.J);
        }
        /// <summary>
        /// Calculates the dot product of this vector and a supplied vector.
        /// </summary>
        /// <param name="vector">The vector to dot this vector with.</param>
        /// <returns>The dot product.</returns>
        public float Dot(Vector2 vector)
        {
            return Dot(this, vector);
        }
        /// <summary>
        /// Returns a string representation of this vector.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"{I}, {J}";
        }
        /// <summary>
        /// Converts this vector to a 2-dimensional point.
        /// </summary>
        /// <returns>A new <see cref="Point2F"/> value.</returns>
        public Point2F ToPoint()
        {
            return new Point2F(I, J);
        }

        /// <summary>
        /// Calculates the dot product of two supplied vectors.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>The dot product.</returns>
        public static float Dot(Vector2 v1, Vector2 v2)
        {
            return v1.I * v2.I + v1.J + v2.J;
        }
        /// <summary>
        /// Multiplies a vector and a scalar. The resulting vector's components will be the product of the vector components and the scalar value.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>A vector.</returns>
        public static Vector2 operator *(Vector2 vector, float scalar)
        {
            return new Vector2(vector.I * scalar, vector.J * scalar);
        }
        /// <summary>
        /// Divides a vector by a scalar. The resulting vector's components will be the quotient of the vector components and the scalar value.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>A vector.</returns>
        public static Vector2 operator /(Vector2 vector, float scalar)
        {
            return new Vector2(vector.I / scalar, vector.J / scalar);
        }
        /// <summary>
        /// Adds a vector and a scalar. The resulting vector's components will be the sum of the vector components and the scalar value.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>A vector.</returns>
        public static Vector2 operator +(Vector2 vector, float scalar)
        {
            return new Vector2(vector.I + scalar, vector.J + scalar);
        }
        /// <summary>
        /// Subtracts a vector and a scalar. The resulting vector's components will be the difference of the vector components and the scalar value.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>A vector.</returns>
        public static Vector2 operator -(Vector2 vector, float scalar)
        {
            return new Vector2(vector.I - scalar, vector.J - scalar);
        }
        /// <summary>
        /// Multiplies two vectors. The resulting vector's components will be the product of the first vector's components and the second vector's components.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>A vector.</returns>
        public static Vector2 operator *(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.I * v2.I, v1.J * v2.J);
        }
        /// <summary>
        /// Divides two vectors. The resulting vector's components will be the quotient of the first vector's components and the second vector's components.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>A vector.</returns>
        public static Vector2 operator /(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.I / v2.I, v1.J / v2.J);
        }
        /// <summary>
        /// Adds two vectors. The resulting vector's components will be the sum of the first vector's components and the second vector's components.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>A vector.</returns>
        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.I + v2.I, v1.J + v2.J);
        }
        /// <summary>
        /// Subracts two vectors. The resulting vector's components will be the difference of the first vector's components and the second vector's components.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>A vector.</returns>
        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.I - v2.I, v1.J - v2.J);
        }
    }
    #endregion
    #region Points & Rectangles
    /// <summary>
    /// Represents a 2 dimensional rectangle.
    /// </summary>
    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct Rectangle2 : IEquatable<Rectangle2>
    {
        /// <summary>
        /// Represents a zero-value <see cref="Rectangle2"/> value.
        /// This value is read-only.
        /// </summary>
        public static readonly Rectangle2 Empty = new Rectangle2();

        /// <summary>
        /// Gets or sets the distance from the top of the screen.
        /// </summary>
        public short Top { get; }
        /// <summary>
        /// Gets or sets the distance from the left of the screen.
        /// </summary>
        public short Left { get; }
        /// <summary>
        /// Gets or sets the distance from the bottom of the screen.
        /// </summary>
        public short Bottom { get; }
        /// <summary>
        /// Gets or sets the distance from the right of the screen.
        /// </summary>
        public short Right { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle2"/> structure.
        /// </summary>
        /// <param name="top">The distance from the top of the screen to the top of the rectangle.</param>
        /// <param name="left">The distance from the left of the screen to the left of the rectangle.</param>
        /// <param name="bottom">The distance from the bottom of the screen to the bottom of the rectangle.</param>
        /// <param name="right">The distance from the right of the screen to the right of the rectangle.</param>
        public Rectangle2(short top, short left, short bottom, short right)
        {
            Top = top;
            Left = left;
            Bottom = bottom;
            Right = right;
        }
        /// <summary>
        /// Returns a value indicating whether this rectangle and a specified rectangle represent the same value.
        /// </summary>
        /// <param name="rectangle">The other rectangle.</param>
        /// <returns><see langword="true"/> if this rectangle and <paramref name="rectangle"/> are equal; otherwise, <see langword="false"/>.</returns>
        public bool Equals(Rectangle2 rectangle)
        {
            return Top.Equals(rectangle.Top) && Left.Equals(rectangle.Left) && Bottom.Equals(rectangle.Bottom) && Right.Equals(rectangle.Right);
        }
    }
    /// <summary>
    /// Represents a 2 dimensional point.
    /// </summary>
    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct Point2 : IEquatable<Point2>
    {
        /// <summary>
        /// Represents a zero point.
        /// This value is read-only.
        /// </summary>
        public static readonly Point2 Zero = new Point2();

        /// <summary>
        /// Gets or sets the the x-component of this point.
        /// </summary>
        public short X { get; set; }
        /// <summary>
        /// Gets or sets the the y-component of this point.
        /// </summary>
        public short Y { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point2"/> structure using the supplied x, and y component values.
        /// </summary>
        /// <param name="x">The x-component.</param>
        /// <param name="y">The y-component.</param>
        public Point2(short x, short y)
        {
            X = x;
            Y = y;
        }
        /// <summary>
        /// Returns a value indicating whether this point and a specified <see cref="Point2"/> represent the same value.
        /// </summary>
        /// <param name="point">The other point.</param>
        /// <returns>True if the this point and <paramref name="point"/> are equal; otherwise false.</returns>
        public bool Equals(Point2 point)
        {
            return X.Equals(point.X) && Y.Equals(point.Y);
        }
        /// <summary>
        /// Returns a string representation of this point.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"{X}, {Y}";
        }
    }
    /// <summary>
    /// Represents a floating point 2 dimensional point.
    /// </summary>
    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct Point2F : IEquatable<Point2F>
    {
        /// <summary>
        /// Represents a zero point.
        /// This value is read-only.
        /// </summary>
        public static readonly Point2F Zero = new Point2F();

        /// <summary>
        /// Gets or sets the the x-component of this point.
        /// </summary>
        public float X { get; }
        /// <summary>
        /// Gets or sets the the y-component of this point.
        /// </summary>
        public float Y { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point2F"/> structure using the supplied x, and y component values.
        /// </summary>
        /// <param name="x">The x-component.</param>
        /// <param name="y">The y-component.</param>
        public Point2F(float x, float y)
        {
            X = x;
            Y = y;
        }
        /// <summary>
        /// Returns a value indicating whether this point and a specified <see cref="Point2F"/> represent the same value.
        /// </summary>
        /// <param name="point">The other point.</param>
        /// <returns>True if the this point and <paramref name="point"/> are equal; otherwise false.</returns>
        public bool Equals(Point2F point)
        {
            return X.Equals(point.X) && Y.Equals(point.Y);
        }
        /// <summary>
        /// Returns a string representation of this point.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"{X}, {Y}";
        }
        /// <summary>
        /// Converts this point to a 2-dimensional vector.
        /// </summary>
        /// <returns>A new <see cref="Vector2"/> value.</returns>
        public Vector2 ToVector()
        {
            return new Vector2(X, Y);
        }
    }
    /// <summary>
    /// Represents a floating point 3 dimensional point.
    /// </summary>
    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct Point3F : IEquatable<Point3F>
    {
        /// <summary>
        /// Represents a zero point.
        /// This value is read-only.
        /// </summary>
        public static readonly Point3F Zero = new Point3F();

        /// <summary>
        /// Gets or sets the the x-component of this point.
        /// </summary>
        public float X { get; }
        /// <summary>
        /// Gets or sets the the y-component of this point.
        /// </summary>
        public float Y { get; }
        /// <summary>
        /// Gets or sets the the y-component of this point.
        /// </summary>
        public float Z { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point3F"/> structure using the supplied x, y, and z component values.
        /// </summary>
        /// <param name="x">The x-component.</param>
        /// <param name="y">The y-component.</param>
        /// <param name="z">The z-component.</param>
        public Point3F(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        /// <summary>
        /// Returns a value indicating whether this point and a specified <see cref="Point3F"/> represent the same value.
        /// </summary>
        /// <param name="point">The other point.</param>
        /// <returns><see langword="true"/> if the this point and <paramref name="point"/> are equal; otherwise, <see langword="false"/>.</returns>
        public bool Equals(Point3F point)
        {
            return X.Equals(point.X) && Y.Equals(point.Y) && Z.Equals(point.Z);
        }
        /// <summary>
        /// Returns a string representation of this point.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"{X}, {Y}, {Z}";
        }
        /// <summary>
        /// Converts this point to a 3-dimensional vector.
        /// </summary>
        /// <returns>A new <see cref="Vector3"/> value.</returns>
        public Vector3 ToVector()
        {
            return new Vector3(X, Y, Z);
        }
    }
    #endregion
    #region Strings
    /// <summary>
    /// Represents a 32-byte length string.
    /// </summary>
    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct String32
    {
        /// <summary>
        /// Gets or sets the string value.
        /// </summary>
        public string String
        {
            get
            {
                byte[] buffer = new byte[] {
                string0,
                string1,
                string2,
                string3,
                string4,
                string5,
                string6,
                string7,
                string8,
                string9,
                string10,
                string11,
                string12,
                string13,
                string14,
                string15,
                string16,
                string17,
                string18,
                string19,
                string20,
                string21,
                string22,
                string23,
                string24,
                string25,
                string26,
                string27,
                string28,
                string29,
                string30,
                string31};
                return System.Text.Encoding.UTF8.GetString(buffer).Trim('\0');
            }
            set
            {
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(value.PadRight(32, '\0'));
                string0 = buffer[0];
                string1 = buffer[1];
                string2 = buffer[2];
                string3 = buffer[3];
                string4 = buffer[4];
                string5 = buffer[5];
                string6 = buffer[6];
                string7 = buffer[7];
                string8 = buffer[8];
                string9 = buffer[9];
                string10 = buffer[10];
                string11 = buffer[11];
                string12 = buffer[12];
                string13 = buffer[13];
                string14 = buffer[14];
                string15 = buffer[15];
                string16 = buffer[16];
                string17 = buffer[17];
                string18 = buffer[18];
                string19 = buffer[19];
                string20 = buffer[20];
                string21 = buffer[21];
                string22 = buffer[22];
                string23 = buffer[23];
                string24 = buffer[24];
                string25 = buffer[25];
                string26 = buffer[26];
                string27 = buffer[27];
                string28 = buffer[28];
                string29 = buffer[29];
                string30 = buffer[30];
                string31 = buffer[31];
            }
        }
        private byte string0;
        private byte string1;
        private byte string2;
        private byte string3;
        private byte string4;
        private byte string5;
        private byte string6;
        private byte string7;
        private byte string8;
        private byte string9;
        private byte string10;
        private byte string11;
        private byte string12;
        private byte string13;
        private byte string14;
        private byte string15;
        private byte string16;
        private byte string17;
        private byte string18;
        private byte string19;
        private byte string20;
        private byte string21;
        private byte string22;
        private byte string23;
        private byte string24;
        private byte string25;
        private byte string26;
        private byte string27;
        private byte string28;
        private byte string29;
        private byte string30;
        private byte string31;
        /// <summary>
        /// Returns the string value.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return String;
        }
        public static bool operator ==(String32 s1, String32 s2)
        {
            return s1.String == s2.String;
        }
        public static bool operator !=(String32 s1, String32 s2)
        {
            return s1.String != s2.String;
        }
    }
    /// <summary>
    /// Represents a 256-byte length string.
    /// </summary>
    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct String256
    {
        /// <summary>
        /// Gets or sets the string value.
        /// </summary>
        public string String
        {
            get
            {
                byte[] buffer = new byte[] {
                string0,
                string1,
                string2,
                string3,
                string4,
                string5,
                string6,
                string7,
                string8,
                string9,
                string10,
                string11,
                string12,
                string13,
                string14,
                string15,
                string16,
                string17,
                string18,
                string19,
                string20,
                string21,
                string22,
                string23,
                string24,
                string25,
                string26,
                string27,
                string28,
                string29,
                string30,
                string31,
                string32,
                string33,
                string34,
                string35,
                string36,
                string37,
                string38,
                string39,
                string40,
                string41,
                string42,
                string43,
                string44,
                string45,
                string46,
                string47,
                string48,
                string49,
                string50,
                string51,
                string52,
                string53,
                string54,
                string55,
                string56,
                string57,
                string58,
                string59,
                string60,
                string61,
                string62,
                string63,
                string64,
                string65,
                string66,
                string67,
                string68,
                string69,
                string70,
                string71,
                string72,
                string73,
                string74,
                string75,
                string76,
                string77,
                string78,
                string79,
                string80,
                string81,
                string82,
                string83,
                string84,
                string85,
                string86,
                string87,
                string88,
                string89,
                string90,
                string91,
                string92,
                string93,
                string94,
                string95,
                string96,
                string97,
                string98,
                string99,
                string100,
                string101,
                string102,
                string103,
                string104,
                string105,
                string106,
                string107,
                string108,
                string109,
                string110,
                string111,
                string112,
                string113,
                string114,
                string115,
                string116,
                string117,
                string118,
                string119,
                string120,
                string121,
                string122,
                string123,
                string124,
                string125,
                string126,
                string127,
                string128,
                string129,
                string130,
                string131,
                string132,
                string133,
                string134,
                string135,
                string136,
                string137,
                string138,
                string139,
                string140,
                string141,
                string142,
                string143,
                string144,
                string145,
                string146,
                string147,
                string148,
                string149,
                string150,
                string151,
                string152,
                string153,
                string154,
                string155,
                string156,
                string157,
                string158,
                string159,
                string160,
                string161,
                string162,
                string163,
                string164,
                string165,
                string166,
                string167,
                string168,
                string169,
                string170,
                string171,
                string172,
                string173,
                string174,
                string175,
                string176,
                string177,
                string178,
                string179,
                string180,
                string181,
                string182,
                string183,
                string184,
                string185,
                string186,
                string187,
                string188,
                string189,
                string190,
                string191,
                string192,
                string193,
                string194,
                string195,
                string196,
                string197,
                string198,
                string199,
                string200,
                string201,
                string202,
                string203,
                string204,
                string205,
                string206,
                string207,
                string208,
                string209,
                string210,
                string211,
                string212,
                string213,
                string214,
                string215,
                string216,
                string217,
                string218,
                string219,
                string220,
                string221,
                string222,
                string223,
                string224,
                string225,
                string226,
                string227,
                string228,
                string229,
                string230,
                string231,
                string232,
                string233,
                string234,
                string235,
                string236,
                string237,
                string238,
                string239,
                string240,
                string241,
                string242,
                string243,
                string244,
                string245,
                string246,
                string247,
                string248,
                string249,
                string250,
                string251,
                string252,
                string253,
                string254,
                string255};
                return System.Text.Encoding.UTF8.GetString(buffer).Trim('\0');
            }
            set
            {
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(value.PadRight(256, '\0'));
                string0 = buffer[0];
                string1 = buffer[1];
                string2 = buffer[2];
                string3 = buffer[3];
                string4 = buffer[4];
                string5 = buffer[5];
                string6 = buffer[6];
                string7 = buffer[7];
                string8 = buffer[8];
                string9 = buffer[9];
                string10 = buffer[10];
                string11 = buffer[11];
                string12 = buffer[12];
                string13 = buffer[13];
                string14 = buffer[14];
                string15 = buffer[15];
                string16 = buffer[16];
                string17 = buffer[17];
                string18 = buffer[18];
                string19 = buffer[19];
                string20 = buffer[20];
                string21 = buffer[21];
                string22 = buffer[22];
                string23 = buffer[23];
                string24 = buffer[24];
                string25 = buffer[25];
                string26 = buffer[26];
                string27 = buffer[27];
                string28 = buffer[28];
                string29 = buffer[29];
                string30 = buffer[30];
                string31 = buffer[31];
                string32 = buffer[32];
                string33 = buffer[33];
                string34 = buffer[34];
                string35 = buffer[35];
                string36 = buffer[36];
                string37 = buffer[37];
                string38 = buffer[38];
                string39 = buffer[39];
                string40 = buffer[40];
                string41 = buffer[41];
                string42 = buffer[42];
                string43 = buffer[43];
                string44 = buffer[44];
                string45 = buffer[45];
                string46 = buffer[46];
                string47 = buffer[47];
                string48 = buffer[48];
                string49 = buffer[49];
                string50 = buffer[50];
                string51 = buffer[51];
                string52 = buffer[52];
                string53 = buffer[53];
                string54 = buffer[54];
                string55 = buffer[55];
                string56 = buffer[56];
                string57 = buffer[57];
                string58 = buffer[58];
                string59 = buffer[59];
                string60 = buffer[60];
                string61 = buffer[61];
                string62 = buffer[62];
                string63 = buffer[63];
                string64 = buffer[64];
                string65 = buffer[65];
                string66 = buffer[66];
                string67 = buffer[67];
                string68 = buffer[68];
                string69 = buffer[69];
                string70 = buffer[70];
                string71 = buffer[71];
                string72 = buffer[72];
                string73 = buffer[73];
                string74 = buffer[74];
                string75 = buffer[75];
                string76 = buffer[76];
                string77 = buffer[77];
                string78 = buffer[78];
                string79 = buffer[79];
                string80 = buffer[80];
                string81 = buffer[81];
                string82 = buffer[82];
                string83 = buffer[83];
                string84 = buffer[84];
                string85 = buffer[85];
                string86 = buffer[86];
                string87 = buffer[87];
                string88 = buffer[88];
                string89 = buffer[89];
                string90 = buffer[90];
                string91 = buffer[91];
                string92 = buffer[92];
                string93 = buffer[93];
                string94 = buffer[94];
                string95 = buffer[95];
                string96 = buffer[96];
                string97 = buffer[97];
                string98 = buffer[98];
                string99 = buffer[99];
                string100 = buffer[100];
                string101 = buffer[101];
                string102 = buffer[102];
                string103 = buffer[103];
                string104 = buffer[104];
                string105 = buffer[105];
                string106 = buffer[106];
                string107 = buffer[107];
                string108 = buffer[108];
                string109 = buffer[109];
                string110 = buffer[110];
                string111 = buffer[111];
                string112 = buffer[112];
                string113 = buffer[113];
                string114 = buffer[114];
                string115 = buffer[115];
                string116 = buffer[116];
                string117 = buffer[117];
                string118 = buffer[118];
                string119 = buffer[119];
                string120 = buffer[120];
                string121 = buffer[121];
                string122 = buffer[122];
                string123 = buffer[123];
                string124 = buffer[124];
                string125 = buffer[125];
                string126 = buffer[126];
                string127 = buffer[127];
                string128 = buffer[128];
                string129 = buffer[129];
                string130 = buffer[130];
                string131 = buffer[131];
                string132 = buffer[132];
                string133 = buffer[133];
                string134 = buffer[134];
                string135 = buffer[135];
                string136 = buffer[136];
                string137 = buffer[137];
                string138 = buffer[138];
                string139 = buffer[139];
                string140 = buffer[140];
                string141 = buffer[141];
                string142 = buffer[142];
                string143 = buffer[143];
                string144 = buffer[144];
                string145 = buffer[145];
                string146 = buffer[146];
                string147 = buffer[147];
                string148 = buffer[148];
                string149 = buffer[149];
                string150 = buffer[150];
                string151 = buffer[151];
                string152 = buffer[152];
                string153 = buffer[153];
                string154 = buffer[154];
                string155 = buffer[155];
                string156 = buffer[156];
                string157 = buffer[157];
                string158 = buffer[158];
                string159 = buffer[159];
                string160 = buffer[160];
                string161 = buffer[161];
                string162 = buffer[162];
                string163 = buffer[163];
                string164 = buffer[164];
                string165 = buffer[165];
                string166 = buffer[166];
                string167 = buffer[167];
                string168 = buffer[168];
                string169 = buffer[169];
                string170 = buffer[170];
                string171 = buffer[171];
                string172 = buffer[172];
                string173 = buffer[173];
                string174 = buffer[174];
                string175 = buffer[175];
                string176 = buffer[176];
                string177 = buffer[177];
                string178 = buffer[178];
                string179 = buffer[179];
                string180 = buffer[180];
                string181 = buffer[181];
                string182 = buffer[182];
                string183 = buffer[183];
                string184 = buffer[184];
                string185 = buffer[185];
                string186 = buffer[186];
                string187 = buffer[187];
                string188 = buffer[188];
                string189 = buffer[189];
                string190 = buffer[190];
                string191 = buffer[191];
                string192 = buffer[192];
                string193 = buffer[193];
                string194 = buffer[194];
                string195 = buffer[195];
                string196 = buffer[196];
                string197 = buffer[197];
                string198 = buffer[198];
                string199 = buffer[199];
                string200 = buffer[200];
                string201 = buffer[201];
                string202 = buffer[202];
                string203 = buffer[203];
                string204 = buffer[204];
                string205 = buffer[205];
                string206 = buffer[206];
                string207 = buffer[207];
                string208 = buffer[208];
                string209 = buffer[209];
                string210 = buffer[210];
                string211 = buffer[211];
                string212 = buffer[212];
                string213 = buffer[213];
                string214 = buffer[214];
                string215 = buffer[215];
                string216 = buffer[216];
                string217 = buffer[217];
                string218 = buffer[218];
                string219 = buffer[219];
                string220 = buffer[220];
                string221 = buffer[221];
                string222 = buffer[222];
                string223 = buffer[223];
                string224 = buffer[224];
                string225 = buffer[225];
                string226 = buffer[226];
                string227 = buffer[227];
                string228 = buffer[228];
                string229 = buffer[229];
                string230 = buffer[230];
                string231 = buffer[231];
                string232 = buffer[232];
                string233 = buffer[233];
                string234 = buffer[234];
                string235 = buffer[235];
                string236 = buffer[236];
                string237 = buffer[237];
                string238 = buffer[238];
                string239 = buffer[239];
                string240 = buffer[240];
                string241 = buffer[241];
                string242 = buffer[242];
                string243 = buffer[243];
                string244 = buffer[244];
                string245 = buffer[245];
                string246 = buffer[246];
                string247 = buffer[247];
                string248 = buffer[248];
                string249 = buffer[249];
                string250 = buffer[250];
                string251 = buffer[251];
                string252 = buffer[252];
                string253 = buffer[253];
                string254 = buffer[254];
                string255 = buffer[255];
            }
        }
        private byte string0;
        private byte string1;
        private byte string2;
        private byte string3;
        private byte string4;
        private byte string5;
        private byte string6;
        private byte string7;
        private byte string8;
        private byte string9;
        private byte string10;
        private byte string11;
        private byte string12;
        private byte string13;
        private byte string14;
        private byte string15;
        private byte string16;
        private byte string17;
        private byte string18;
        private byte string19;
        private byte string20;
        private byte string21;
        private byte string22;
        private byte string23;
        private byte string24;
        private byte string25;
        private byte string26;
        private byte string27;
        private byte string28;
        private byte string29;
        private byte string30;
        private byte string31;
        private byte string32;
        private byte string33;
        private byte string34;
        private byte string35;
        private byte string36;
        private byte string37;
        private byte string38;
        private byte string39;
        private byte string40;
        private byte string41;
        private byte string42;
        private byte string43;
        private byte string44;
        private byte string45;
        private byte string46;
        private byte string47;
        private byte string48;
        private byte string49;
        private byte string50;
        private byte string51;
        private byte string52;
        private byte string53;
        private byte string54;
        private byte string55;
        private byte string56;
        private byte string57;
        private byte string58;
        private byte string59;
        private byte string60;
        private byte string61;
        private byte string62;
        private byte string63;
        private byte string64;
        private byte string65;
        private byte string66;
        private byte string67;
        private byte string68;
        private byte string69;
        private byte string70;
        private byte string71;
        private byte string72;
        private byte string73;
        private byte string74;
        private byte string75;
        private byte string76;
        private byte string77;
        private byte string78;
        private byte string79;
        private byte string80;
        private byte string81;
        private byte string82;
        private byte string83;
        private byte string84;
        private byte string85;
        private byte string86;
        private byte string87;
        private byte string88;
        private byte string89;
        private byte string90;
        private byte string91;
        private byte string92;
        private byte string93;
        private byte string94;
        private byte string95;
        private byte string96;
        private byte string97;
        private byte string98;
        private byte string99;
        private byte string100;
        private byte string101;
        private byte string102;
        private byte string103;
        private byte string104;
        private byte string105;
        private byte string106;
        private byte string107;
        private byte string108;
        private byte string109;
        private byte string110;
        private byte string111;
        private byte string112;
        private byte string113;
        private byte string114;
        private byte string115;
        private byte string116;
        private byte string117;
        private byte string118;
        private byte string119;
        private byte string120;
        private byte string121;
        private byte string122;
        private byte string123;
        private byte string124;
        private byte string125;
        private byte string126;
        private byte string127;
        private byte string128;
        private byte string129;
        private byte string130;
        private byte string131;
        private byte string132;
        private byte string133;
        private byte string134;
        private byte string135;
        private byte string136;
        private byte string137;
        private byte string138;
        private byte string139;
        private byte string140;
        private byte string141;
        private byte string142;
        private byte string143;
        private byte string144;
        private byte string145;
        private byte string146;
        private byte string147;
        private byte string148;
        private byte string149;
        private byte string150;
        private byte string151;
        private byte string152;
        private byte string153;
        private byte string154;
        private byte string155;
        private byte string156;
        private byte string157;
        private byte string158;
        private byte string159;
        private byte string160;
        private byte string161;
        private byte string162;
        private byte string163;
        private byte string164;
        private byte string165;
        private byte string166;
        private byte string167;
        private byte string168;
        private byte string169;
        private byte string170;
        private byte string171;
        private byte string172;
        private byte string173;
        private byte string174;
        private byte string175;
        private byte string176;
        private byte string177;
        private byte string178;
        private byte string179;
        private byte string180;
        private byte string181;
        private byte string182;
        private byte string183;
        private byte string184;
        private byte string185;
        private byte string186;
        private byte string187;
        private byte string188;
        private byte string189;
        private byte string190;
        private byte string191;
        private byte string192;
        private byte string193;
        private byte string194;
        private byte string195;
        private byte string196;
        private byte string197;
        private byte string198;
        private byte string199;
        private byte string200;
        private byte string201;
        private byte string202;
        private byte string203;
        private byte string204;
        private byte string205;
        private byte string206;
        private byte string207;
        private byte string208;
        private byte string209;
        private byte string210;
        private byte string211;
        private byte string212;
        private byte string213;
        private byte string214;
        private byte string215;
        private byte string216;
        private byte string217;
        private byte string218;
        private byte string219;
        private byte string220;
        private byte string221;
        private byte string222;
        private byte string223;
        private byte string224;
        private byte string225;
        private byte string226;
        private byte string227;
        private byte string228;
        private byte string229;
        private byte string230;
        private byte string231;
        private byte string232;
        private byte string233;
        private byte string234;
        private byte string235;
        private byte string236;
        private byte string237;
        private byte string238;
        private byte string239;
        private byte string240;
        private byte string241;
        private byte string242;
        private byte string243;
        private byte string244;
        private byte string245;
        private byte string246;
        private byte string247;
        private byte string248;
        private byte string249;
        private byte string250;
        private byte string251;
        private byte string252;
        private byte string253;
        private byte string254;
        private byte string255;
        /// <summary>
        /// Returns the string value.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return String;
        }
        public static bool operator ==(String256 s1, String256 s2)
        {
            return s1.String == s2.String;
        }
        public static bool operator !=(String256 s1, String256 s2)
        {
            return s1.String != s2.String;
        }
    }
    #endregion
}
