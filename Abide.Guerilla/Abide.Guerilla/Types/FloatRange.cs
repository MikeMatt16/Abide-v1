using System;

namespace Abide.Guerilla.Types
{
    /// <summary>
    /// Represents a floating point boundaries structure.
    /// </summary>
    public struct FloatRange : IEquatable<FloatRange>
    {
        /// <summary>
        /// Represents a zero value float range.
        /// This value is read-only.
        /// </summary>
        public static FloatRange Zero = new FloatRange() { from = 0, to = 0 };

        /// <summary>
        /// Represents the lower end the boundaries.
        /// </summary>
        public float From
        {
            get { return from; }
            set { from = value; }
        }
        /// <summary>
        /// Represents the upper end of the boundaries.
        /// </summary>
        public float To
        {
            get { return to; }
            set { to = value; }
        }

        private float from, to;

        /// <summary>
        /// Returns a value indicating whether this instance and a specified <see cref="FloatRange"/> object represent the same value.
        /// </summary>
        /// <param name="other">An object to compare to this instance.</param>
        /// <returns>true if the values are the equal; otherwise false.</returns>
        public bool Equals(FloatRange other)
        {
            return from.Equals(other.from) && to.Equals(other.to);
        }
    }
}
