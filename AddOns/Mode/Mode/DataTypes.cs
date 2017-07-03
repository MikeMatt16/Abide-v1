using System;
using System.Runtime.InteropServices;

namespace Mode
{
    /// <summary>
    /// Represents a 2-component vector.
    /// </summary>
    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct Vector2
    {
        /// <summary>
        /// Represents a zero-value <see cref="Vector2"/> instance
        /// (0, 0).
        /// This value is read only.
        /// </summary>
        public static readonly Vector2 Zero = new Vector2();
        /// <summary>
        /// Represents a vector instance that is 'up' from zero
        /// (0, 1).
        /// This value is read only.
        /// </summary>
        public static readonly Vector2 Up = new Vector2() { y = 1 };
        /// <summary>
        /// Represents a vector instance that is 'down' from zero
        /// (0, -1).
        /// This value is read only.
        /// </summary>
        public static readonly Vector2 Down = new Vector2() { y = -1 };
        /// <summary>
        /// Represents a vector instance that is 'left' from zero
        /// (-1, 0).
        /// This value is read only.
        /// </summary>
        public static readonly Vector2 Left = new Vector2() { x = -1 };
        /// <summary>
        /// Represents a vector instance that is 'right' from zero
        /// (1, 0).
        /// This value is read only.
        /// </summary>
        public static readonly Vector2 Right = new Vector2() { x = 1 };

        /// <summary>
        /// Gets or sets the X component of this vector.
        /// </summary>
        public float X
        {
            get { return x; }
            set { x = value; }
        }
        /// <summary>
        /// Gets or sets the Y component of this vector.
        /// </summary>
        public float Y
        {
            get { return y; }
            set { y = value; }
        }
        /// <summary>
        /// Gets or sets the magnitude of this vector.
        /// </summary>
        public float Magnitude
        {
            get { return (float)Math.Sqrt((x * x) + (y * y)); }
            set
            {
                double rotation = Math.Atan2(y, x);
                x = (float)Math.Cos(rotation) * value;
                y = (float)Math.Sin(rotation) * value;
            }
        }
        /// <summary>
        /// Gets or sets the direction of this vector.
        /// </summary>
        public double Direction
        {
            get { return Math.Atan2(y, x); }
            set
            {
                float magnitude = (float)Math.Sqrt((x * x) + (y * y));
                x = (float)Math.Cos(value) * magnitude;
                y = (float)Math.Sin(value) * magnitude;
            }
        }

        private float x, y;

        /// <summary>
        /// Initializes a new <see cref="Vector2"/> structure with the specified <paramref name="x"/> and <paramref name="y"/> component values.
        /// </summary>
        /// <param name="x">The X component of the vector.</param>
        /// <param name="y">The Y component of the vector.</param>
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        /// <summary>
        /// Initializes a new <see cref="Vector2"/> structure with the supplied <paramref name="magnitude"/> and <paramref name="direction"/> values. 
        /// </summary>
        /// <param name="magnitude">The magnitude of the vector.</param>
        /// <param name="direction">The direction of the vector.</param>
        public Vector2(float magnitude, double direction)
        {
            x = (float)(Math.Cos(direction) * magnitude);
            y = (float)(Math.Sin(direction) * magnitude);
        }

        /// <summary>
        /// Converts this vector instance into a string representation.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("({0}, {1})", x, y);
        }
        /// <summary>
        /// Calculates the dot product of this vector instance and another vector.
        /// </summary>
        /// <param name="vector">The other vector.</param>
        /// <returns>The scalar product.</returns>
        public float Dot(Vector2 vector)
        {
            return Dot(this, vector);
        }
        /// <summary>
        /// Calculates the dot product of two vector values.
        /// </summary>
        /// <param name="v1">The first vector value.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>The scalar product of the two vectors.</returns>
        public static float Dot(Vector2 v1, Vector2 v2)
        {
            return (float)(Math.Abs(v1.Magnitude) * Math.Abs(v2.Magnitude) * Math.Cos(v1.Direction - v2.Direction));
        }

        /// <summary>
        /// Multiplies the components of a vector and a scalar value.
        /// </summary>
        /// <param name="vec">The vector.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>A vector whose individual components are a product of the supplied vector's components and the supplied scalar.</returns>
        public static Vector2 operator *(Vector2 vec, float scalar)
        {
            return new Vector2(vec.x * scalar, vec.y * scalar);
        }
        /// <summary>
        /// Multiplies the components of two supplied vector values.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>A vector whose individual components are a product of the supplied vectors' components.</returns>
        public static Vector2 operator *(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x * v2.x, v1.y * v2.y);
        }
        /// <summary>
        /// Adds the components of a vector and a scalar value.
        /// </summary>
        /// <param name="vec">The vector.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>A Vector whose individiual components are a sum of the supplied vector's components and the supplied scalar.</returns>
        public static Vector2 operator +(Vector2 vec, float scalar)
        {
            return new Vector2(vec.x + scalar, vec.y + scalar);
        }
        /// <summary>
        /// Adds the components of two supplied vector values.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>A vector whose individual components are a sum of the supplied vectors' components.</returns>
        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x + v2.x, v1.y + v2.y);
        }
        /// <summary>
        /// Divides the components of a vector and a scalar value.
        /// </summary>
        /// <param name="vec">The vector.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>A vector whose individual components are a quotient of the supplied vector's components and the supplied scalar.</returns>
        /// <exception cref="DivideByZeroException">Occurs if the scalar is zero.</exception>
        public static Vector2 operator /(Vector2 vec, float scalar)
        {
            if (scalar == 0)
                throw new DivideByZeroException();
            return new Vector2(vec.x / scalar, vec.y / scalar);
        }
        /// <summary>
        /// Divides the components of two supplied vector values.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>A vector whose individual components are a quotient of the supplied vectors' components.</returns>
        /// <exception cref="DivideByZeroException">Occurs when either component of v2 is zero.</exception>
        public static Vector2 operator /(Vector2 v1, Vector2 v2)
        {
            if (v2.x == 0 || v2.y == 0)
                throw new DivideByZeroException();
            return new Vector2(v1.x / v2.x, v1.y / v2.y);
        }
        /// <summary>
        /// Subtracts the components of a vector and a scalar value.
        /// </summary>
        /// <param name="vec">The vector.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>A Vector whose individiual components are a difference of the supplied vector's components and the supplied scalar.</returns>
        public static Vector2 operator -(Vector2 vec, float scalar)
        {
            return new Vector2(vec.x - scalar, vec.y - scalar);
        }
        /// <summary>
        /// Subtracts the components of two supplied vector values.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>A vector whose individual components are a difference of the supplied vectors' components.</returns>
        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x - v2.x, v1.y - v2.y);
        }
    }

    /// <summary>
    /// Represents a 3-component vector.
    /// </summary>
    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct Vector3
    {
        /// <summary>
        /// Represents a zero-value <see cref="Vector3"/> instance
        /// (0, 0, 0).
        /// This value is read only.
        /// </summary>
        public static readonly Vector3 Zero = new Vector3();

        /// <summary>
        /// Gets or sets the X component of this vector.
        /// </summary>
        public float X
        {
            get { return x; }
            set { x = value; }
        }
        /// <summary>
        /// Gets or sets the Y component of this vector.
        /// </summary>
        public float Y
        {
            get { return y; }
            set { y = value; }
        }
        /// <summary>
        /// Gets or sets the Z component of this vector;
        /// </summary>
        public float Z
        {
            get { return z; }
            set { z = value; }
        }
        /// <summary>
        /// Gets or sets the magnitude of this vector.
        /// </summary>
        public float Magnitude
        {
            get { return (float)Math.Sqrt((x * x) + (y * y) * (z * z)); }
            set
            {
                double alpha = Math.Atan2(z, x);
                double beta = Math.Atan2(z, y);
                x = (float)Math.Cos(alpha) * value;
                z = (float)Math.Sin(alpha) * value;
                y = (float)Math.Cos(beta) * value;
            }
        }
        /// <summary>
        /// Gets or sets the α-rotation of this vector.
        /// </summary>
        public double Alpha
        {
            get { return Math.Atan2(z, x); }
            set
            {
                float magnitude = (float)Math.Sqrt((x * x) + (y * y) * (z * z));
                x = (float)Math.Cos(value) * magnitude;
                z = (float)Math.Sin(value) * magnitude;
            }
        }
        /// <summary>
        /// Gets or sets the β-rotation of this vector.
        /// </summary>
        public double Beta
        {
            get { return Math.Atan2(z, y);}
            set
            {
                float magnitude = (float)Math.Sqrt((z * z) + (y * y) * (z * z));
                y = (float)Math.Cos(value) * magnitude;
                z = (float)Math.Sin(value) * magnitude;
            }
        }
        /// <summary>
        /// Gets or sets the γ-rotation of this vector.
        /// </summary>
        public double Gamma
        {
            get { return Math.Atan2(y, x); }
            set
            {
                float magnitude = (float)Math.Sqrt((x * x) + (y * y) * (z * z));
                x = (float)Math.Cos(value) * magnitude;
                y = (float)Math.Sin(value) * magnitude;
            }
        }

        private float x, y, z;

        /// <summary>
        /// Initializes a new <see cref="Vector3"/> structure with the specified <paramref name="x"/> and <paramref name="y"/> component values.
        /// </summary>
        /// <param name="x">The X component of the vector.</param>
        /// <param name="y">The Y component of the vector.</param>
        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// Converts this vector instance into a string representation.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"({x}, {y}, {z})";
        }

        /// <summary>
        /// Multiplies the components of a vector and a scalar value.
        /// </summary>
        /// <param name="vec">The vector.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>A vector whose individual components are a product of the supplied vector's components and the supplied scalar.</returns>
        public static Vector3 operator *(Vector3 vec, float scalar)
        {
            return new Vector3(vec.x * scalar, vec.y * scalar, vec.z * scalar);
        }
        /// <summary>
        /// Multiplies the components of two supplied vector values.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>A vector whose individual components are a product of the supplied vectors' components.</returns>
        public static Vector3 operator *(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
        }
        /// <summary>
        /// Adds the components of a vector and a scalar value.
        /// </summary>
        /// <param name="vec">The vector.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>A Vector whose individiual components are a sum of the supplied vector's components and the supplied scalar.</returns>
        public static Vector3 operator +(Vector3 vec, float scalar)
        {
            return new Vector3(vec.x + scalar, vec.y + scalar, vec.z + scalar);
        }
        /// <summary>
        /// Adds the components of two supplied vector values.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>A vector whose individual components are a sum of the supplied vectors' components.</returns>
        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        }
        /// <summary>
        /// Divides the components of a vector and a scalar value.
        /// </summary>
        /// <param name="vec">The vector.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>A vector whose individual components are a quotient of the supplied vector's components and the supplied scalar.</returns>
        /// <exception cref="DivideByZeroException">Occurs if the scalar is zero.</exception>
        public static Vector3 operator /(Vector3 vec, float scalar)
        {
            if (scalar == 0)
                throw new DivideByZeroException();
            return new Vector3(vec.x / scalar, vec.y / scalar, vec.z / scalar);
        }
        /// <summary>
        /// Divides the components of two supplied vector values.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>A vector whose individual components are a quotient of the supplied vectors' components.</returns>
        /// <exception cref="DivideByZeroException">Occurs when either component of v2 is zero.</exception>
        public static Vector3 operator /(Vector3 v1, Vector3 v2)
        {
            if (v2.x == 0 || v2.y == 0)
                throw new DivideByZeroException();
            return new Vector3(v1.x / v2.x, v1.y / v2.y, v1.z / v2.z);
        }
        /// <summary>
        /// Subtracts the components of a vector and a scalar value.
        /// </summary>
        /// <param name="vec">The vector.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>A Vector whose individiual components are a difference of the supplied vector's components and the supplied scalar.</returns>
        public static Vector3 operator -(Vector3 vec, float scalar)
        {
            return new Vector3(vec.x - scalar, vec.y - scalar, vec.z - scalar);
        }
        /// <summary>
        /// Subtracts the components of two supplied vector values.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>A vector whose individual components are a difference of the supplied vectors' components.</returns>
        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        }
    }
}
