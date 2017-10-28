using System;

namespace Abide.Guerilla.Types
{
    /// <summary>
    /// Represents a vector with four components.
    /// </summary>
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
        /// Gets or sets the the x-component of this vector.
        /// </summary>
        public float X
        {
            get { return x; }
            set { x = value; }
        }
        /// <summary>
        /// Gets or sets the the y-component of this vector.
        /// </summary>
        public float Y
        {
            get { return y; }
            set { y = value; }
        }
        /// <summary>
        /// Gets or sets the the z-component of this vector.
        /// </summary>
        public float Z
        {
            get { return z; }
            set { z = value; }
        }
        /// <summary>
        /// Gets or sets the the w-component of this vector.
        /// </summary>
        public float W
        {
            get { return w; }
            set { w = value; }
        }

        private float x, y, z, w;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4"/> structure using the supplied x, y, z, and w component values.
        /// </summary>
        /// <param name="x">The x-component.</param>
        /// <param name="y">The y-component.</param>
        /// <param name="z">The z-component.</param>
        /// <param name="w">The w-component.</param>
        public Vector4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4"/> structure using the supplied x, y, and z component values.
        /// </summary>
        /// <param name="x">The x-component.</param>
        /// <param name="y">The y-component.</param>
        /// <param name="z">The z-component.</param>
        public Vector4(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = 0;
        }
        /// <summary>
        /// Initializes a new <see cref="Vector4"/> structure using the supplied x and y component values.
        /// </summary>
        /// <param name="x">The x-component.</param>
        /// <param name="y">The y-component.</param>
        public Vector4(float x, float y)
        {
            this.x = x;
            this.y = y;
            z = 0;
            w = 0;
        }
        /// <summary>
        /// Returns a value indicating whether this vector and a specified <see cref="Vector4"/> represent the same value.
        /// </summary>
        /// <param name="vector">The other vector.</param>
        /// <returns>True if the this vector and <paramref name="vector"/> are equal; otherwise false.</returns>
        public bool Equals(Vector4 vector)
        {
            return x.Equals(vector.x) && y.Equals(vector.y) && z.Equals(vector.z) && w.Equals(vector.w);
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
            return $"{x}, {y}, {z}, {w}";
        }
        bool IEquatable<Vector4>.Equals(Vector4 other)
        {
            return Equals(other);
        }

        /// <summary>
        /// Calculates the dot product of two supplied vectors.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>The dot product.</returns>
        public static float Dot(Vector4 v1, Vector4 v2)
        {
            return v1.x * v2.x + v1.y + v2.y * v1.z * v2.z + v1.w * v2.w;
        }
        /// <summary>
        /// Multiplies a vector and a scalar. The resulting vector's components will be the product of the vector components and the scalar value.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>A vector.</returns>
        public static Vector4 operator *(Vector4 vector, float scalar)
        {
            return new Vector4(vector.x * scalar, vector.y * scalar, vector.z * scalar, vector.w * scalar);
        }
        /// <summary>
        /// Divides a vector by a scalar. The resulting vector's components will be the quotient of the vector components and the scalar value.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>A vector.</returns>
        public static Vector4 operator /(Vector4 vector, float scalar)
        {
            return new Vector4(vector.x / scalar, vector.y / scalar, vector.z / scalar, vector.w / scalar);
        }
        /// <summary>
        /// Adds a vector and a scalar. The resulting vector's components will be the sum of the vector components and the scalar value.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>A vector.</returns>
        public static Vector4 operator +(Vector4 vector, float scalar)
        {
            return new Vector4(vector.x + scalar, vector.y + scalar, vector.z + scalar, vector.w + scalar);
        }
        /// <summary>
        /// Subtracts a vector and a scalar. The resulting vector's components will be the difference of the vector components and the scalar value.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>A vector.</returns>
        public static Vector4 operator -(Vector4 vector, float scalar)
        {
            return new Vector4(vector.x - scalar, vector.y - scalar, vector.z - scalar, vector.w - scalar);
        }
        /// <summary>
        /// Multiplies two vectors. The resulting vector's components will be the product of the first vector's components and the second vector's components.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>A vector.</returns>
        public static Vector4 operator *(Vector4 v1, Vector4 v2)
        {
            return new Vector4(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z, v1.w * v2.w);
        }
        /// <summary>
        /// Divides two vectors. The resulting vector's components will be the quotient of the first vector's components and the second vector's components.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>A vector.</returns>
        public static Vector4 operator /(Vector4 v1, Vector4 v2)
        {
            return new Vector4(v1.x / v2.x, v1.y / v2.y, v1.z / v2.z, v1.w / v2.w);
        }
        /// <summary>
        /// Adds two vectors. The resulting vector's components will be the sum of the first vector's components and the second vector's components.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>A vector.</returns>
        public static Vector4 operator +(Vector4 v1, Vector4 v2)
        {
            return new Vector4(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z, v1.w + v2.w);
        }
        /// <summary>
        /// Subracts two vectors. The resulting vector's components will be the difference of the first vector's components and the second vector's components.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>A vector.</returns>
        public static Vector4 operator -(Vector4 v1, Vector4 v2)
        {
            return new Vector4(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z, v1.w - v2.w);
        }
    }

    /// <summary>
    /// Represents a vector with three components.
    /// </summary>
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
        /// Gets or sets the the x-component of this vector.
        /// </summary>
        public float X
        {
            get { return x; }
            set { x = value; }
        }
        /// <summary>
        /// Gets or sets the the y-component of this vector.
        /// </summary>
        public float Y
        {
            get { return y; }
            set { y = value; }
        }
        /// <summary>
        /// Gets or sets the the z-component of this vector.
        /// </summary>
        public float Z
        {
            get { return z; }
            set { z = value; }
        }

        private float x, y, z;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3"/> structure using the supplied x, y, and z component values.
        /// </summary>
        /// <param name="x">The x-component.</param>
        /// <param name="y">The y-component.</param>
        /// <param name="z">The z-component.</param>
        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        /// <summary>
        /// Initializes a new <see cref="Vector3"/> structure using the supplied x and y component values.
        /// </summary>
        /// <param name="x">The x-component.</param>
        /// <param name="y">The y-component.</param>
        public Vector3(float x, float y)
        {
            this.x = x;
            this.y = y;
            z = 0;
        }
        /// <summary>
        /// Returns a value indicating whether this vector and a specified <see cref="Vector3"/> represent the same value.
        /// </summary>
        /// <param name="vector">The other vector.</param>
        /// <returns>True if the this vector and <paramref name="vector"/> are equal; otherwise false.</returns>
        public bool Equals(Vector3 vector)
        {
            return x.Equals(vector.x) && y.Equals(vector.y) && z.Equals(vector.z);
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
        /// Returns a string representation of this vector.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"{x}, {y}, {z}";
        }
        bool IEquatable<Vector3>.Equals(Vector3 other)
        {
            return Equals(other);
        }

        /// <summary>
        /// Calculates the dot product of two supplied vectors.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>The dot product.</returns>
        public static float Dot(Vector3 v1, Vector3 v2)
        {
            return v1.x * v2.x + v1.y + v2.y * v1.z * v2.z;
        }
        /// <summary>
        /// Multiplies a vector and a scalar. The resulting vector's components will be the product of the vector components and the scalar value.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>A vector.</returns>
        public static Vector3 operator *(Vector3 vector, float scalar)
        {
            return new Vector3(vector.x * scalar, vector.y * scalar, vector.z * scalar);
        }
        /// <summary>
        /// Divides a vector by a scalar. The resulting vector's components will be the quotient of the vector components and the scalar value.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>A vector.</returns>
        public static Vector3 operator /(Vector3 vector, float scalar)
        {
            return new Vector3(vector.x / scalar, vector.y / scalar, vector.z / scalar);
        }
        /// <summary>
        /// Adds a vector and a scalar. The resulting vector's components will be the sum of the vector components and the scalar value.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>A vector.</returns>
        public static Vector3 operator +(Vector3 vector, float scalar)
        {
            return new Vector3(vector.x + scalar, vector.y + scalar, vector.z + scalar);
        }
        /// <summary>
        /// Subtracts a vector and a scalar. The resulting vector's components will be the difference of the vector components and the scalar value.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>A vector.</returns>
        public static Vector3 operator -(Vector3 vector, float scalar)
        {
            return new Vector3(vector.x - scalar, vector.y - scalar, vector.z - scalar);
        }
        /// <summary>
        /// Multiplies two vectors. The resulting vector's components will be the product of the first vector's components and the second vector's components.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>A vector.</returns>
        public static Vector3 operator *(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
        }
        /// <summary>
        /// Divides two vectors. The resulting vector's components will be the quotient of the first vector's components and the second vector's components.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>A vector.</returns>
        public static Vector3 operator /(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x / v2.x, v1.y / v2.y, v1.z / v2.z);
        }
        /// <summary>
        /// Adds two vectors. The resulting vector's components will be the sum of the first vector's components and the second vector's components.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>A vector.</returns>
        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        }
        /// <summary>
        /// Subracts two vectors. The resulting vector's components will be the difference of the first vector's components and the second vector's components.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>A vector.</returns>
        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        }
    }

    /// <summary>
    /// Represents a vector with two components.
    /// </summary>
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
        /// Gets or sets the the x-component of this vector.
        /// </summary>
        public float X
        {
            get { return x; }
            set { x = value; }
        }
        /// <summary>
        /// Gets or sets the the y-component of this vector.
        /// </summary>
        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        private float x, y;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2"/> structure using the supplied x, and y component values.
        /// </summary>
        /// <param name="x">The x-component.</param>
        /// <param name="y">The y-component.</param>
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        /// <summary>
        /// Returns a value indicating whether this vector and a specified <see cref="Vector2"/> represent the same value.
        /// </summary>
        /// <param name="vector">The other vector.</param>
        /// <returns>True if the this vector and <paramref name="vector"/> are equal; otherwise false.</returns>
        public bool Equals(Vector2 vector)
        {
            return x.Equals(vector.x) && y.Equals(vector.y);
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
            return $"{x}, {y}";
        }
        bool IEquatable<Vector2>.Equals(Vector2 other)
        {
            return Equals(other);
        }

        /// <summary>
        /// Calculates the dot product of two supplied vectors.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>The dot product.</returns>
        public static float Dot(Vector2 v1, Vector2 v2)
        {
            return v1.x * v2.x + v1.y + v2.y;
        }
        /// <summary>
        /// Multiplies a vector and a scalar. The resulting vector's components will be the product of the vector components and the scalar value.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>A vector.</returns>
        public static Vector2 operator *(Vector2 vector, float scalar)
        {
            return new Vector2(vector.x * scalar, vector.y * scalar);
        }
        /// <summary>
        /// Divides a vector by a scalar. The resulting vector's components will be the quotient of the vector components and the scalar value.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>A vector.</returns>
        public static Vector2 operator /(Vector2 vector, float scalar)
        {
            return new Vector2(vector.x / scalar, vector.y / scalar);
        }
        /// <summary>
        /// Adds a vector and a scalar. The resulting vector's components will be the sum of the vector components and the scalar value.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>A vector.</returns>
        public static Vector2 operator +(Vector2 vector, float scalar)
        {
            return new Vector2(vector.x + scalar, vector.y + scalar);
        }
        /// <summary>
        /// Subtracts a vector and a scalar. The resulting vector's components will be the difference of the vector components and the scalar value.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>A vector.</returns>
        public static Vector2 operator -(Vector2 vector, float scalar)
        {
            return new Vector2(vector.x - scalar, vector.y - scalar);
        }
        /// <summary>
        /// Multiplies two vectors. The resulting vector's components will be the product of the first vector's components and the second vector's components.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>A vector.</returns>
        public static Vector2 operator *(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x * v2.x, v1.y * v2.y);
        }
        /// <summary>
        /// Divides two vectors. The resulting vector's components will be the quotient of the first vector's components and the second vector's components.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>A vector.</returns>
        public static Vector2 operator /(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x / v2.x, v1.y / v2.y);
        }
        /// <summary>
        /// Adds two vectors. The resulting vector's components will be the sum of the first vector's components and the second vector's components.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>A vector.</returns>
        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x + v2.x, v1.y + v2.y);
        }
        /// <summary>
        /// Subracts two vectors. The resulting vector's components will be the difference of the first vector's components and the second vector's components.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>A vector.</returns>
        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x - v2.x, v1.y - v2.y);
        }
    }
}
