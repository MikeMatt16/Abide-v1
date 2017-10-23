using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abide.Guerilla.Types
{
    public struct ColorRgb
    {
        private byte r, g, b;
    }
    public struct ColorArgb
    {
        private byte a, r, g, b;
    }
    public struct ColorHsv
    {
        private byte h, s, v;
    }
    public struct ColorAhsv
    {
        private byte a, h, s, v;
    }

    public struct ColorRgbF
    {
        private float r, g, b;
    }
    public struct ColorArgbF
    {
        private float a, r, g, b;
    }
    public struct ColorHsvF
    {
        private float h, s, v;
    }
    public struct ColorAhsvF
    {
        private float a, h, s, v;
    }
}
