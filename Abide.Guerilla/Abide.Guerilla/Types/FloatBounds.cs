using System;

namespace Abide.Guerilla.Types
{
    public struct FloatBounds : IEquatable<FloatBounds>
    {
        public float From
        {
            get { return from; }
            set { from = value; }
        }
        public float To
        {
            get { return to; }
            set { to = value; }
        }

        private float from, to;

        public bool Equals(FloatBounds other)
        {
            throw new NotImplementedException();
        }
    }
}
