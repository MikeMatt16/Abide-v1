namespace Abide.Guerilla.Types
{
    public struct Rectangle2
    {
        public float X
        {
            get { return left; }
        }
        public float Y
        {
            get { return top; }
        }
        public float Width
        {
            get { return right - left; }
        }
        public float Height
        {
            get { return bottom - top; }
        }

        private float top, left, right, bottom;
    }
}
