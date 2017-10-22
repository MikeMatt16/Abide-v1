using System;

namespace Abide.Guerilla.Types
{
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
    }
}
