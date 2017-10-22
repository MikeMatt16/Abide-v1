using System;

namespace Abide.Builder.Guerilla.Types
{
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
    }
}
