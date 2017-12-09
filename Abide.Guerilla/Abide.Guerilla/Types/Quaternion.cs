namespace Abide.Guerilla.Types
{
    /// <summary>
    /// Represents a quaternion.
    /// </summary>
    public struct Quaternion
    {
        /// <summary>
        /// Represents a zero value quaternion.
        /// This value is read-only.
        /// </summary>
        public static readonly Quaternion Zero = new Quaternion() { w = 1, i = 0, j = 0, k = 0 };

        /// <summary>
        /// Gets the w-component of the quaternion.
        /// </summary>
        public float W
        {
            get { return w; }
            set { w = value; }
        }
        /// <summary>
        /// Gets the i-component of the quaternion.
        /// </summary>
        public float I
        {
            get { return i; }
            set { i = value; }
        }
        /// <summary>
        /// Gets the j-component of the quaternion.
        /// </summary>
        public float J
        {
            get { return j; }
            set { j = value; }
        }
        /// <summary>
        /// Gets the k-component of the quaternion.
        /// </summary>
        public float K
        {
            get { return k; }
            set { k = value; }
        }
        
        private float w, i, j, k;

        /// <summary>
        /// Initializes a new instance of the <see cref="Quaternion"/> structure using the supplied component values.
        /// </summary>
        /// <param name="w">The w-component.</param>
        /// <param name="i">The i-component.</param>
        /// <param name="j">The j-component.</param>
        /// <param name="k">The k-component.</param>
        public Quaternion(float w, float i, float j, float k)
        {
            this.w = w;
            this.i = i;
            this.j = j;
            this.k = k;
        }
        /// <summary>
        /// Gets a string representation of this quaternion.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"{w}, {i}, {j}, {k}";
        }
    }
}
