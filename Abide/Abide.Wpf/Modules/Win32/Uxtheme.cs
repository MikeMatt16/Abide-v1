using System;
using System.Runtime.InteropServices;

namespace Abide.Wpf.Modules.Win32
{
    internal static class Uxtheme
    {
        [DllImport("Uxtheme.dll")]
        public static extern bool IsThemeActive();

        [DllImport("Uxtheme.dll")]
        public static extern IntPtr GetWindowTheme(IntPtr hwnd);

        [DllImport("Uxtheme.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr OpenThemeData(IntPtr hwnd, string pszClassList);

        [DllImport("Uxtheme.dll")]
        public static extern int CloseThemeData(IntPtr hTheme);

        [DllImport("Uxtheme.dll")]
        public static extern int GetThemeMetric(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, int iPropId, out int piVal);
    }
}
