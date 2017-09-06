using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Abide
{
    /// <summary>
    /// Handles the painting for Abide's Aero Tool Strip.
    /// </summary>
    internal sealed class AeroToolStripRenderer : ToolStripProfessionalRenderer
    {
        private static readonly Color Abide1 = Color.FromArgb(213, 221, 229);
        private static readonly Color Abide2 = Color.FromArgb(186, 203, 219);

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            //e.Graphics.Clear(Color.Transparent);
            TextRenderer.DrawText(e.Graphics, e.Text, e.TextFont, e.TextRectangle, Color.Blue, e.TextFormat);
        }
        
        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            base.OnRenderImageMargin(e);
            using (Brush b = new LinearGradientBrush(e.AffectedBounds, Abide1, Abide2, LinearGradientMode.Vertical))
                e.Graphics.FillRectangle(b, e.AffectedBounds);
            e.Graphics.DrawRectangle(Pens.LightGray, e.AffectedBounds);
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            //Fill
            e.Graphics.FillRectangle(Brushes.Transparent, e.AffectedBounds);
        }

        [DllImport("Uxtheme.dll")]
        private static extern IntPtr DrawThemeTextEx(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, string pszText, int iCharCount, uint dwFlags, RECT pRect, ref DTTOPTS pOption);
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr CreateCompatibleDC(IntPtr hDC);
        [DllImport("gdi32.dll", ExactSpelling = true)]
        private static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern bool DeleteObject(IntPtr hObject);
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern bool DeleteDC(IntPtr hdc);
        [DllImport("gdi32.dll")]
        private static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, uint dwRop);
        [DllImport("UxTheme.dll", CharSet = CharSet.Unicode)]
        private static extern int DrawThemeTextEx(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, string text, int iCharCount, int dwFlags, ref RECT pRect, ref DTTOPTS pOptions);

        private struct RECT
        {
            public int X
            {
                get { return left; }
                set { left = value; }
            }
            public int Y
            {
                get { return top; }
                set { top = value; }
            }
            public int Width
            {
                get { return right - left; }
                set { right = left + value; }
            }
            public int Height
            {
                get { return bottom - top; }
                set { bottom = top + value; }
            }

            public int Top
            {
                get { return top; }
            }
            public int Left
            {
                get { return left; }
            }
            public int Right
            {
                get { return right; }
            }
            public int Bottom
            {
                get { return bottom; }
            }

            private int top, left, right, bottom;
            
            public RECT(int x, int y, int width, int height)
            {
                top = y;
                left = x;
                right = x + width;
                bottom = y + height;
            }
        }

        private struct DTTOPTS
        {
            public int Size
            {
                get { return (int)dwSize; }
                set { dwSize = (uint)value; }
            }
            public Color TextColor
            {
                get { return crText; }
                set { crText = value; }
            }
            public Color BorderColor
            {
                get { return crBorder; }
                set { crBorder = value; }
            }
            public Color ShadowColor
            {
                get { return crShadow; }
                set { crShadow = value; }
            }
            public Point ShadowOffset
            {
                get { return ptShadowOffset; }
                set { ptShadowOffset = value; }
            }
            public bool ApplyOverlay
            {
                get { return fApplyOverlay; }
                set { fApplyOverlay = value; }
            }
            public int GlowSize
            {
                get { return iGlowSize; }
                set { iGlowSize = value; }
            }

            private uint dwSize;
            private uint dwFlags;
            private Color crText;
            private Color crBorder;
            private Color crShadow;
            private int iTextShadowType;
            private Point ptShadowOffset;
            private int iBorderSize;
            private int iFontPropId;
            private int iColorPropId;
            private int iStateId;
            private bool fApplyOverlay;
            private int iGlowSize;
            private int pfnDrawTextCallback;
            private IntPtr lParam;
        }
    }
}
