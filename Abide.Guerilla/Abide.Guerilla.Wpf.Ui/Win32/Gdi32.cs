using System;
using System.Runtime.InteropServices;

namespace Abide.Guerilla.Wpf.Ui.Win32
{
    public static class Gdi32
    {
        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr ho);

        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr h);

        [DllImport("gdi32.dll")]
        public static extern bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);
    }
}
