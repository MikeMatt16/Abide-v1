#pragma warning disable CS1591
namespace Abide.Guerilla.Types
{
    public struct ColorRgb
    {
        public static ColorRgb Zero = new ColorRgb() { r = 0, g = 0, b = 0 };

        private byte r, g, b;
    }
    public struct ColorArgb
    {
        public static ColorArgb Zero = new ColorArgb() {a = 0, r = 0, g = 0, b = 0 };

        private byte a, r, g, b;
    }
    public struct ColorHsv
    {
        public static ColorHsv Zero = new ColorHsv() { h = 0, s = 0, v = 0 };

        private byte h, s, v;
    }
    public struct ColorAhsv
    {
        public static ColorAhsv Zero = new ColorAhsv() { a = 0, h = 0, s = 0, v = 0 };

        private byte a, h, s, v;
    }

    public struct ColorRgbF
    {
        public static ColorRgbF Zero = new ColorRgbF() { r = 0, g = 0, b = 0 };

        private float r, g, b;
    }
    public struct ColorArgbF
    {
        public static ColorArgbF Zero = new ColorArgbF() { a = 0, r = 0, g = 0, b = 0 };

        private float a, r, g, b;
    }
    public struct ColorHsvF
    {
        public static ColorHsvF Zero = new ColorHsvF() { h = 0, s = 0, v = 0 };

        private float h, s, v;
    }
    public struct ColorAhsvF
    {
        public static ColorAhsvF Zero = new ColorAhsvF() { a = 0, h = 0, s = 0, v = 0 };

        private float a, h, s, v;
    }
}
#pragma warning restore CS1591
