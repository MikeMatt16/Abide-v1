using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using Point = System.Drawing.Point;

namespace Abide.Guerilla.Wpf.Ui
{
    public enum GlowWindowEdge
    {
        Top, Left, Right, Bottom
    }

    public class GlowWindow : IDisposable
    {
        public event PaintEventHandler PaintLayeredWindow;
        public Color ActiveColor { get; set; } = Color.FromArgb(0xff, 0x00, 0x7a, 0xcc);
        public Color InactiveColor { get; set; } = Color.FromArgb(0xff, 0x3f, 0x3f, 0x46);
        public bool IsHandleCreated
        {
            get { return Handle != IntPtr.Zero; }
        }
        public ushort ClassAtom { get; } = 0;
        public IntPtr ParentWindow { get; } = IntPtr.Zero;
        public GlowWindowEdge Edge { get; }
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;
        public IntPtr Handle { get; private set; } = IntPtr.Zero;
        public bool IsActive
        {
            get { return isActive; }
        }
        public bool IsParentActive
        {
            get { return isParentActive; }
        }

        private readonly WNDPROC wndProcDelegate;
        private Bitmap layerBitmap;
        private static Bitmap errorBitmap = new Bitmap(1, 1);
        private TextureBrush edgeBrush = null;
        private Image cornerA, cornerB;
        private bool isActive = true;
        private bool isParentActive = true;

        public GlowWindow(IntPtr parentWindow, GlowWindowEdge edge)
        {
            //Setup
            ParentWindow = parentWindow;
            Edge = edge;

            //Check
            if (parentWindow != IntPtr.Zero && GetWindowRect(parentWindow, out RECT wndRect))
                switch (edge)
                {
                    case GlowWindowEdge.Top:
                        X = wndRect.X;
                        Y = wndRect.Y - 9;
                        Width = wndRect.Width;
                        Height = 9;
                        break;
                    case GlowWindowEdge.Left:
                        X = wndRect.X - 9;
                        Y = wndRect.Y - 9;
                        Width = 1;
                        Height = 1;
                        break;
                    case GlowWindowEdge.Right:
                        X = wndRect.Width;
                        Y = wndRect.Y - 9;
                        Width = 1;
                        Height = 1;
                        break;
                    case GlowWindowEdge.Bottom:
                        X = wndRect.X;
                        Y = wndRect.Height;
                        Width = 1;
                        Height = 1;
                        break;
                }

            //Get color
            ResourceDictionary VisualStudioResourceDictionary = new ResourceDictionary
            { Source = new Uri("VisualStudioDark.xaml", UriKind.Relative) };
            var activeAccent = (VisualStudioResourceDictionary["ActiveAccent"] as System.Windows.Media.SolidColorBrush).Color;
            var inactiveAccent = (VisualStudioResourceDictionary["InactiveAccent"] as System.Windows.Media.SolidColorBrush).Color;

            //Setup
            ActiveColor = Color.FromArgb(activeAccent.R, activeAccent.G, activeAccent.B);
            InactiveColor = Color.FromArgb(inactiveAccent.R, inactiveAccent.G, inactiveAccent.B);

            //Set
            SetColor(ActiveColor);

            //Setup
            IntPtr hInstance = Marshal.GetHINSTANCE(GetType().Module);
            string wndClass = string.Join("_", "AbideGlowWindow", Enum.GetName(typeof(GlowWindowEdge), edge));
            uint wndStyle = (0x0 | 0x80000000 | 0x04000000 | 0x02000000 | 0x10000000);
            uint wndExStyle = (0x0 | 0x00000080 | 0x00080000);

            //Create WndClassEx
            wndProcDelegate = WndProc;
            WNDCLASSEX wndClassEx = new WNDCLASSEX()
            {
                cbSize = Marshal.SizeOf<WNDCLASSEX>(),
                style = (0x0002 | 0x0001 | 0x0008),
                lpfnWndProc = Marshal.GetFunctionPointerForDelegate(wndProcDelegate),
                hInstance = hInstance,
                lpszClassName = wndClass,
                hCursor = LoadCursor(IntPtr.Zero, 32513),   //I-beam (IDC_IBEAM)
            };

            //Register window class
            ClassAtom = RegisterClassEx(ref wndClassEx);
            if (ClassAtom != 0)
            {
                //Create Window
                Handle = CreateWindowEx(wndExStyle, ClassAtom, null, wndStyle, X, Y, Width, Height,
                    ParentWindow, IntPtr.Zero, hInstance, IntPtr.Zero);

                //Check
                if (Handle != IntPtr.Zero) layerBitmap = new Bitmap(Width, Height);
            }
        }

        ~GlowWindow()
        {
            //Destroy
            if (Handle != IntPtr.Zero)
                if (DestroyWindow(Handle))
                    Handle = IntPtr.Zero;
        }

        public void Close()
        {
            //Close
            if (!IsHandleCreated) return;
            if (CloseWindow(Handle))
                Dispose();
        }

        public void SetParentActivation(bool activation)
        {
            //Get previous value and set parent activation
            bool previousValue = isParentActive || isActive;

            //Check
            if (isParentActive != activation)
            {
                //Set
                isParentActive = activation;

                //Set color
                if (isParentActive)
                    SetColor(ActiveColor);
                else SetColor(InactiveColor);

                //Draw
                DrawLayeredWindow();
            }
        }

        private void SetActivation(bool activation)
        {
            //Get previous value and set parent activation
            bool previousValue = isParentActive || isActive;
            isActive = activation;

            //Check
            if (isParentActive != activation)
            {
                //Set color
                if (isParentActive)
                    SetColor(ActiveColor);
                else SetColor(InactiveColor);

                //Draw
                DrawLayeredWindow();
            }
        }

        [Obsolete("Deprecated use SetActivation(bool) instead.")]
        public void Activate()
        {
            //Set
            isParentActive = true;

            //Set color
            if (isParentActive || isActive)
                SetColor(ActiveColor);
            else SetColor(InactiveColor);

            //Draw
            DrawLayeredWindow();
        }

        [Obsolete("Deprecated use SetActivation(bool) instead.")]
        public void Deactivate()
        {
            //Set
            isParentActive = false;
            isActive = false;

            //Set color
            SetColor(InactiveColor);

            //Draw
            DrawLayeredWindow();
        }

        public void Update(bool visible = true)
        {
            //Prepare
            uint flags = (uint)(visible ? 0x0040 : 0x0080);
            if (!IsHandleCreated) return;

            //Check
            if (GetWindowRect(ParentWindow, out RECT wndRect))
            {
                switch (Edge)
                {
                    case GlowWindowEdge.Top:
                        X = wndRect.X;
                        Y = wndRect.Y - 9;
                        Width = wndRect.Width;
                        Height = 9;
                        break;
                    case GlowWindowEdge.Left:
                        X = wndRect.X - 9;
                        Y = wndRect.Y - 9;
                        Width = 9;
                        Height = wndRect.Height + 18;
                        break;
                    case GlowWindowEdge.Right:
                        X = wndRect.Width + wndRect.X;
                        Y = wndRect.Y - 9;
                        Width = 9;
                        Height = wndRect.Height + 18;
                        break;
                    case GlowWindowEdge.Bottom:
                        X = wndRect.X;
                        Y = wndRect.Height + wndRect.Y;
                        Width = wndRect.Width;
                        Height = 9;
                        break;
                }

                //Setup
                SetWindowPos(Handle, IntPtr.Zero, X, Y, Width, Height, flags);
                
                //Draw
                DrawLayeredWindow();
            }
        }

        private void DrawLayeredWindow()
        {
            //Get layered bitmap
            Bitmap layerBitmap = this.layerBitmap;
            try
            {
                //Check for changes
                if(layerBitmap.Width != Width || layerBitmap.Height != Height)
                {
                    this.layerBitmap.Dispose();
                    layerBitmap = this.layerBitmap = new Bitmap(Width, Height);
                }

                //Create
                using (Graphics g = Graphics.FromImage(layerBitmap))
                using (PaintEventArgs pe = new PaintEventArgs(g, new Rectangle(0, 0, Width, Height)))
                {
                    //Call OnPaintLayeredWindow
                    OnPaintLayeredWindow(pe);

                    //Raise event
                    PaintLayeredWindow?.Invoke(this, pe);
                }
            }
            catch { layerBitmap = errorBitmap; }

            //Prepare
            IntPtr screenDc = GetDC(IntPtr.Zero);
            IntPtr memDc = CreateCompatibleDC(screenDc);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr oldBitmap = IntPtr.Zero;

            try
            {
                hBitmap = layerBitmap.GetHbitmap(Color.FromArgb(0x0));
                oldBitmap = SelectObject(memDc, hBitmap);

                SIZE size = new SIZE(Width, Height);
                POINT pointSource = new POINT()
                {
                    X = 0,
                    Y = 0
                };
                POINT topPos = new POINT
                {
                    X = X,
                    Y = Y
                };
                BLENDFUNCTION blend = new BLENDFUNCTION
                {
                    BlendOp = 0x0,        //AC_SRC_OVER = 0x0
                    BlendFlags = 0x0,
                    SourceConstantAlpha = 0xff,
                    AlphaFormat = 0x01   //AC_SRC_ALPHA
                };

                //Update
                UpdateLayeredWindow(Handle, screenDc, ref topPos, ref size, memDc, ref pointSource, 0, ref blend, 0x00000002);
            }
            finally
            {
                ReleaseDC(IntPtr.Zero, screenDc);

                if (hBitmap != IntPtr.Zero)
                {
                    SelectObject(memDc, oldBitmap);
                    DeleteObject(hBitmap);
                }

                DeleteDC(memDc);
            }
        }

        private void SetColor(Color targetColor)
        {
            //Create color matrix
            float sr = targetColor.R / 255f;
            float sg = targetColor.G / 255f;
            float sb = targetColor.B / 255f;
            float sa = targetColor.A / 255f;
            ColorMatrix cm = new ColorMatrix(new float[][]
            {
                new float[] { sr, 00, 00, 00, 00},
                new float[] { 00, sg, 00, 00, 00},
                new float[] { 00, 00, sb, 00, 00},
                new float[] { 00, 00, 00, sa, 00},
                new float[] { 00, 00, 00, 00, 01},
            });

            //Prepare
            Bitmap edge = null;

            //Cleanup
            if (cornerA != null) cornerA.Dispose();
            if (cornerB != null) cornerB.Dispose();
            if (edgeBrush != null) edgeBrush.Dispose();

            //Handle
            switch (Edge)
            {
                case GlowWindowEdge.Left:
                case GlowWindowEdge.Right:
                    cornerA = new Bitmap(9, 16);
                    cornerB = new Bitmap(9, 16);
                    edge = new Bitmap(9, 1);
                    break;
                case GlowWindowEdge.Top:
                case GlowWindowEdge.Bottom:
                    cornerA = new Bitmap(7, 9);
                    cornerB = new Bitmap(7, 9);
                    edge = new Bitmap(1, 9);
                    break;
            }

            //Create
            using (ImageAttributes imgAttr = new ImageAttributes())
            {
                //Set
                imgAttr.SetColorMatrix(cm);

                //Handle
                switch (Edge)
                {
                    case GlowWindowEdge.Top:
                        using (Graphics g = Graphics.FromImage(cornerA))
                            g.DrawImage(Properties.Resources.topwindow_top_left, new Rectangle(0, 0, 7, 9), 0, 0, 7, 9, GraphicsUnit.Pixel, imgAttr);
                        using (Graphics g = Graphics.FromImage(cornerB))
                            g.DrawImage(Properties.Resources.topwindow_top_right, new Rectangle(0, 0, 7, 9), 0, 0, 7, 9, GraphicsUnit.Pixel, imgAttr);
                        using (Graphics g = Graphics.FromImage(edge))
                            g.DrawImage(Properties.Resources.topwindow_top, new Rectangle(0, 0, 1, 9), 0, 0, 1, 9, GraphicsUnit.Pixel, imgAttr);
                        edgeBrush = new TextureBrush(edge, WrapMode.Tile);
                        break;
                    case GlowWindowEdge.Left:
                        using (Graphics g = Graphics.FromImage(cornerA))
                            g.DrawImage(Properties.Resources.leftwindow_top_left, new Rectangle(0, 0, 9, 16), 0, 0, 9, 16, GraphicsUnit.Pixel, imgAttr);
                        using (Graphics g = Graphics.FromImage(cornerB))
                            g.DrawImage(Properties.Resources.leftwindow_bottom_left, new Rectangle(0, 0, 9, 16), 0, 0, 9, 16, GraphicsUnit.Pixel, imgAttr);
                        using (Graphics g = Graphics.FromImage(edge))
                            g.DrawImage(Properties.Resources.leftwindow_left, new Rectangle(0, 0, 9, 1), 0, 0, 9,1, GraphicsUnit.Pixel, imgAttr);
                        edgeBrush = new TextureBrush(edge, WrapMode.Tile);
                        break;
                    case GlowWindowEdge.Right:
                        using (Graphics g = Graphics.FromImage(cornerA))
                            g.DrawImage(Properties.Resources.rightwindow_top_right, new Rectangle(0, 0, 9, 16), 0, 0, 9, 16, GraphicsUnit.Pixel, imgAttr);
                        using (Graphics g = Graphics.FromImage(cornerB))
                            g.DrawImage(Properties.Resources.rightwindow_bottom_right, new Rectangle(0, 0, 9, 16), 0, 0, 9, 16, GraphicsUnit.Pixel, imgAttr);
                        using (Graphics g = Graphics.FromImage(edge))
                            g.DrawImage(Properties.Resources.rightwindow_right, new Rectangle(0, 0, 9, 1), 0, 0, 9, 1, GraphicsUnit.Pixel, imgAttr);
                        edgeBrush = new TextureBrush(edge, WrapMode.Tile);
                        break;
                    case GlowWindowEdge.Bottom:
                        using (Graphics g = Graphics.FromImage(cornerA))
                            g.DrawImage(Properties.Resources.bottomwindow_bottomleft, new Rectangle(0, 0, 7, 9), 0, 0, 7, 9, GraphicsUnit.Pixel, imgAttr);
                        using (Graphics g = Graphics.FromImage(cornerB))
                            g.DrawImage(Properties.Resources.bottomwindow_bottomright, new Rectangle(0, 0, 7, 9), 0, 0, 7, 9, GraphicsUnit.Pixel, imgAttr);
                        using (Graphics g = Graphics.FromImage(edge))
                            g.DrawImage(Properties.Resources.bottomwindow_bottom, new Rectangle(0, 0, 1, 9), 0, 0, 1, 9, GraphicsUnit.Pixel, imgAttr);
                        edgeBrush = new TextureBrush(edge, WrapMode.Tile);
                        break;
                }
            }
        }

        protected virtual void OnPaintLayeredWindow(PaintEventArgs e)
        {
            //Clear
            e.Graphics.Clear(Color.Transparent);
            e.Graphics.PageUnit = GraphicsUnit.Pixel;

            //Handle edge
            switch (Edge)
            {
                case GlowWindowEdge.Top:
                    edgeBrush.ResetTransform();
                    e.Graphics.DrawImage(cornerA, Rectangle.FromLTRB(0, 0, 7, 9));
                    e.Graphics.DrawImage(cornerB, Rectangle.FromLTRB(Width - 7, 0, Width, 9));
                    e.Graphics.FillRectangle(edgeBrush, Rectangle.FromLTRB(7, 0, Width - 7, 9));
                    break;
                case GlowWindowEdge.Left:
                    edgeBrush.ResetTransform();
                    e.Graphics.DrawImage(cornerA, Rectangle.FromLTRB(0, 0, 9, 16));
                    e.Graphics.DrawImage(cornerB, Rectangle.FromLTRB(0, Height - 16, 9, Height));
                    e.Graphics.FillRectangle(edgeBrush, Rectangle.FromLTRB(0, 16, 9, Height - 16));
                    break;
                case GlowWindowEdge.Bottom:
                    edgeBrush.ResetTransform();
                    e.Graphics.DrawImage(cornerA, Rectangle.FromLTRB(0, 0, 7, 9));
                    e.Graphics.DrawImage(cornerB, Rectangle.FromLTRB(Width - 7, 0, Width, 9));
                    e.Graphics.FillRectangle(edgeBrush, Rectangle.FromLTRB(7, 0, Width - 7, 9));
                    break;
                case GlowWindowEdge.Right:
                    edgeBrush.ResetTransform();
                    e.Graphics.DrawImage(cornerA, Rectangle.FromLTRB(0, 0, 9, 16));
                    e.Graphics.DrawImage(cornerB, Rectangle.FromLTRB(0, Height - 16, 9, Height));
                    e.Graphics.FillRectangle(edgeBrush, Rectangle.FromLTRB(0, 16, 9, Height - 16));
                    break;
            }
        }

        protected virtual IntPtr WndProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            //Prepare
            RECT wndRect = new RECT();

            //Handle Message
            switch (msg)
            {
                case 0x0001:
                    DrawLayeredWindow();
                    return IntPtr.Zero;
                case 0x0024:
                    MINMAXINFO mmi = Marshal.PtrToStructure<MINMAXINFO>(lParam);
                    mmi.MinTrackSize = new POINT(1, 1);
                    Marshal.StructureToPtr(mmi, lParam, true);
                    return IntPtr.Zero;
                case 0x0084:
                    if (GetWindowRect(ParentWindow, out wndRect))
                    {
                        //Get hit point
                        Point hitPt = new Point(lParam.ToInt32());

                        //Check corners
                        if (Rectangle.FromLTRB(wndRect.Left - 8, wndRect.Top - 8, wndRect.Left + 8, wndRect.Top + 8).Contains(hitPt))
                            return (IntPtr)13;
                        if (Rectangle.FromLTRB(wndRect.Right - 8, wndRect.Top - 8, wndRect.Right + 8, wndRect.Top + 8).Contains(hitPt))
                            return (IntPtr)14;
                        if (Rectangle.FromLTRB(wndRect.Left - 8, wndRect.Bottom - 8, wndRect.Left + 8, wndRect.Bottom + 8).Contains(hitPt))
                            return (IntPtr)16;
                        if (Rectangle.FromLTRB(wndRect.Right - 8, wndRect.Bottom - 8, wndRect.Right + 8, wndRect.Bottom + 8).Contains(hitPt))
                            return (IntPtr)17;

                        //Check edges
                        if (Rectangle.FromLTRB(wndRect.Left + 8, wndRect.Bottom, wndRect.Right - 8, wndRect.Bottom + 8).Contains(hitPt))
                            return (IntPtr)15;
                        if (Rectangle.FromLTRB(wndRect.Left + 8, wndRect.Top - 8, wndRect.Right - 8, wndRect.Top).Contains(hitPt))
                            return (IntPtr)12;
                        if (Rectangle.FromLTRB(wndRect.Right, wndRect.Top + 8, wndRect.Right + 8, wndRect.Bottom - 8).Contains(hitPt))
                            return (IntPtr)11;
                        if (Rectangle.FromLTRB(wndRect.Left - 8, wndRect.Top + 8, wndRect.Left, wndRect.Bottom - 8).Contains(hitPt))
                            return (IntPtr)10;

                        return (IntPtr)0;
                    }
                    break;

                case 0x00a1:
                case 0x00a2:
                    SendNotifyMessage(ParentWindow, msg, wParam, lParam);
                    return IntPtr.Zero;
            }

            //Return
            return DefWindowProc(hwnd, msg, wParam, lParam);
        }

        public void Dispose()
        {
            //Destroy
            if (Handle != IntPtr.Zero)
                if (DestroyWindow(Handle))
                    Handle = IntPtr.Zero;
        }
        
        private delegate IntPtr WNDPROC(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr ho);

        [DllImport("gdi32.dll")]
        private static extern IntPtr SelectObject(IntPtr hdc, IntPtr h);

        [DllImport("gdi32.dll")]
        private static extern bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("user32.dll")]
        private static extern bool CloseWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        private static extern bool UpdateLayeredWindow(IntPtr hWnd, IntPtr hdcDst, ref POINT pptDst, ref SIZE psize, 
            IntPtr hdcSource, ref POINT pptSrc, uint crKey, ref BLENDFUNCTION pblend, uint dwFlags);

        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hWnd, IntPtr hdc);

        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern IntPtr DefWindowProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern ushort RegisterClassEx(ref WNDCLASSEX lpWndClass);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr CreateWindowEx(uint dwExStyle, string lpClassName, string lpWindowName, uint dwStyle, int X, int Y,
            int Width, int Height, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr CreateWindowEx(uint dwExStyle, ushort lpClassName, string lpWindowName, uint dwStyle, int X, int Y,
            int Width, int Height, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);

        [DllImport("user32.dll")]
        private static extern bool DestroyWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        private static extern bool SendNotifyMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential)]
        private struct MINMAXINFO
        {
            public POINT MaxPosition
            {
                get { return ptMaxPosition; }
                set { ptMaxPosition = value; }
            }
            public POINT MaxSize
            {
                get { return ptMaxSize; }
                set { ptMaxSize = value; }
            }
            public POINT MinTrackSize
            {
                get { return ptMinTrackSize; }
                set { ptMinTrackSize = value; }
            }
            public POINT MaxTrackSize
            {
                get { return ptMaxTrackSize; }
                set { ptMaxTrackSize = value; }
            }

            private POINT ptReserved;
            private POINT ptMaxSize;
            private POINT ptMaxPosition;
            private POINT ptMinTrackSize;
            private POINT ptMaxTrackSize;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct BLENDFUNCTION
        {
            public byte BlendOp, BlendFlags, SourceConstantAlpha, AlphaFormat;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct SIZE
        {
            public int Width, Height;

            public SIZE(int width, int height)
            {
                Width = width;
                Height = height;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int X, Y;

            public POINT(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct WNDCLASSEX
        {
            public int cbSize;
            public uint style;
            public IntPtr lpfnWndProc;
            public int cbClsExtra;
            public int cbWndExtra;
            public IntPtr hInstance;
            public IntPtr hIcon;
            public IntPtr hCursor;
            public IntPtr hbrBackground;
            public string lpszMenuName;
            public string lpszClassName;
            public IntPtr hIconSm;
        }

        [StructLayout(LayoutKind.Sequential)]
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
            public int Left
            {
                get { return left; }
                set { left = value; }
            }
            public int Top
            {
                get { return top; }
                set { top = value; }
            }
            public int Right
            {
                get { return right; }
                set { right = value; }
            }
            public int Bottom
            {
                get { return bottom; }
                set { bottom = value; }
            }

            private int left, top, right, bottom;

            public override string ToString()
            {
                return $"({left}, {top}, {right}, {bottom}) Location: ({X}, {Y}) Size: ({Width}, {Height})";
            }
        }
    }
}
