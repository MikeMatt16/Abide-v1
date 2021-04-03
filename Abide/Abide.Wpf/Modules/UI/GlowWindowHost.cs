using Abide.Wpf.Modules.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using Color = System.Drawing.Color;
using Point = System.Drawing.Point;
using WpfBrush = System.Windows.Media.Brush;
using WpfBrushes = System.Windows.Media.Brushes;
using WpfColor = System.Windows.Media.Color;
using WpfPoint = System.Windows.Point;

namespace Abide.Wpf.Modules.UI
{
    public class GlowWindowHost : Window
    {
        private const string GlowWindowClassName = "AbideGlowWindow";

        private static readonly WindowProc glowWindowWndProc = GlowWindow_WindowProc;
        private static readonly Dictionary<IntPtr, GlowWindowHost> hostDictionary = new Dictionary<IntPtr, GlowWindowHost>();
        private static readonly WindowClassAtom GlowWindowClassAtom = new WindowClassAtom();

        private readonly WindowHandle windowHandle, leftHandle, topHandle, rightHandle, bottomHandle;
        private GlowTextures glowTextures = new GlowTextures();
        private bool isActive = false;
        private int wmSizeWParam = 0;

        public static readonly RoutedEvent ClientAreaRectangleRequestedEvent =
            EventManager.RegisterRoutedEvent("ClientAreaRectangleRequested", RoutingStrategy.Direct, typeof(ClientAreaEventHandler), typeof(GlowWindowHost));

        public static readonly DependencyProperty NonClientActionProperty =
            DependencyProperty.RegisterAttached("NonClientAction", typeof(NonClientHitAction), typeof(GlowWindowHost), new PropertyMetadata(NonClientHitAction.Inherit));

        public static readonly DependencyProperty ActiveGlowColorProperty =
            DependencyProperty.Register("ActiveGlowColor", typeof(WpfColor), typeof(GlowWindowHost), new PropertyMetadata(WpfColor.FromRgb(0, 122, 204), ActiveGlowColorPropertyChanged));
        public static readonly DependencyProperty InactiveGlowColorProperty =
            DependencyProperty.Register("InactiveGlowColor", typeof(WpfColor), typeof(GlowWindowHost), new PropertyMetadata(WpfColor.FromRgb(63, 63, 70), InactiveGlowColorPropertyChanged));
        public static readonly DependencyProperty BorderVisibleProperty =
            DependencyProperty.Register("BordersVisible", typeof(bool), typeof(GlowWindowHost), new PropertyMetadata(true, BordersVisiblePropertyChanged));

        public event ClientAreaEventHandler ClientAreaRectangleRequested
        {
            add { AddHandler(ClientAreaRectangleRequestedEvent, value); }
            remove { RemoveHandler(ClientAreaRectangleRequestedEvent, value); }
        }

        public bool BordersVisible
        {
            get => (bool)GetValue(BorderVisibleProperty);
            set => SetValue(BorderVisibleProperty, value);
        }
        public WpfColor ActiveGlowColor
        {
            get => (WpfColor)GetValue(ActiveGlowColorProperty);
            set => SetValue(ActiveGlowColorProperty, value);
        }
        public WpfColor InactiveGlowColor
        {
            get => (WpfColor)GetValue(InactiveGlowColorProperty);
            set => SetValue(InactiveGlowColorProperty, value);
        }

        public GlowWindowHost() : base()
        {
            SnapsToDevicePixels = true;
            Background = new SolidColorBrush(WpfColor.FromRgb(0x2d, 0x2d, 0x30));
            Foreground = WpfBrushes.White;

            windowHandle = new WindowHandle();
            leftHandle = new WindowHandle();
            topHandle = new WindowHandle();
            rightHandle = new WindowHandle();
            bottomHandle = new WindowHandle();
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            if (PresentationSource.FromVisual(this) is HwndSource hWndSource)
            {
                windowHandle.Handle = hWndSource.Handle;
                IntPtr hInstance = Marshal.GetHINSTANCE(typeof(GlowWindowHost).Module);

                //Register window handle
                hostDictionary.Add(hWndSource.Handle, this);

                //Prepare
                uint style = (uint)(WindowStyles.Popup | WindowStyles.Visible | WindowStyles.ClipSiblings | WindowStyles.ClipChildren);
                uint exStyle = (uint)(ExtendedWindowStyles.Left | ExtendedWindowStyles.LeftToRightReading | ExtendedWindowStyles.RightScrollBar | ExtendedWindowStyles.ToolWindow | ExtendedWindowStyles.Layered);

                //Create glow window handles
                leftHandle.Handle = User32.CreateWindowEx(exStyle, GlowWindowClassAtom.ClassAtom, string.Empty, style, 0, 0, 1, 1,
                    hWndSource.Handle, IntPtr.Zero, hInstance, IntPtr.Zero);
                topHandle.Handle = User32.CreateWindowEx(exStyle, GlowWindowClassAtom.ClassAtom, string.Empty, style, 0, 0, 1, 1,
                    hWndSource.Handle, IntPtr.Zero, hInstance, IntPtr.Zero);
                rightHandle.Handle = User32.CreateWindowEx(exStyle, GlowWindowClassAtom.ClassAtom, string.Empty, style, 0, 0, 1, 1,
                    hWndSource.Handle, IntPtr.Zero, hInstance, IntPtr.Zero);
                bottomHandle.Handle = User32.CreateWindowEx(exStyle, GlowWindowClassAtom.ClassAtom, string.Empty, style, 0, 0, 1, 1,
                    hWndSource.Handle, IntPtr.Zero, hInstance, IntPtr.Zero);

                //Lock
                windowHandle.Lock();
                leftHandle.Lock();
                topHandle.Lock();
                rightHandle.Lock();
                bottomHandle.Lock();

                //Add WndProc hook
                hWndSource.AddHook(GlowWindowHost_WndProc);
                GetThemeMargins();
            }
        }
        protected virtual void OnClientAreaRectangleRequested(ClientAreaEventArgs e)
        {
            //Raise event
            RaiseEvent(e);
        }
        private void GetThemeMargins()
        {
            if (Uxtheme.IsThemeActive())
            {
                // #define TMT_WIDTH	2416
                // #define TMT_HEIGHT	2417
            }
        }
        private void LeftGlow_SizePos(IntPtr hwnd, RECT wndRect)
        {
            //Check
            if (hwnd == IntPtr.Zero)
            {
                return;
            }

            int x = wndRect.X - 9;
            int y = wndRect.Y - 9;
            int cx = 9;
            int cy = wndRect.Height + 18;

            //Setup
            _ = User32.SetWindowPos(hwnd, windowHandle.Handle, x, y, cx, cy, 0);
            GlowWindow_DrawLayeredWindow(hwnd);
        }
        private void TopGlow_SizePos(IntPtr hwnd, RECT wndRect)
        {
            //Check
            if (hwnd == IntPtr.Zero)
            {
                return;
            }

            int x = wndRect.X;
            int y = wndRect.Y - 9;
            int cx = wndRect.Width;
            int cy = 9;

            //Setup
            _ = User32.SetWindowPos(hwnd, windowHandle.Handle, x, y, cx, cy, 0);
            GlowWindow_DrawLayeredWindow(hwnd);
        }
        private void RightGlow_SizePos(IntPtr hwnd, RECT wndRect)
        {
            //Check
            if (hwnd == IntPtr.Zero)
            {
                return;
            }

            int x = wndRect.Right;
            int y = wndRect.Y - 9;
            int cx = 9;
            int cy = wndRect.Height + 18;

            //Setup
            _ = User32.SetWindowPos(hwnd, windowHandle.Handle, x, y, cx, cy, 0);
            GlowWindow_DrawLayeredWindow(hwnd);
        }
        private void BottomGlow_SizePos(IntPtr hwnd, RECT wndRect)
        {
            //Check
            if (hwnd == IntPtr.Zero)
            {
                return;
            }

            int x = wndRect.X;
            int y = wndRect.Bottom;
            int cx = wndRect.Width;
            int cy = 9;

            //Setup
            _ = User32.SetWindowPos(hwnd, windowHandle.Handle, x, y, cx, cy, 0);
            GlowWindow_DrawLayeredWindow(hwnd);
        }
        private RECT GlowWindowHost_RequestClientRectangle(RECT clientRect)
        {
            //Get some metrics
            int cyCaption = User32.GetSystemMetrics(4);
            int cxBorder = User32.GetSystemMetrics(5);
            int cyBorder = User32.GetSystemMetrics(6);
            int cxSizeFrame = User32.GetSystemMetrics(32);
            int cySizeFrame = User32.GetSystemMetrics(33);
            int cxEdge = User32.GetSystemMetrics(45);
            int cyEdge = User32.GetSystemMetrics(46);

            //Check
            if (wmSizeWParam == 2)
            {
                clientRect.Top += cyBorder + cyEdge + cySizeFrame;
                clientRect.Bottom -= cyBorder + cyEdge + cySizeFrame;
                clientRect.Left += cxBorder + cxEdge + cxSizeFrame;
                clientRect.Right -= cxBorder + cxEdge + cxSizeFrame;
            }

            //Get System.Drawing.Rectangle from client rect
            Rectangle rect = Rectangle.FromLTRB(clientRect.Left, clientRect.Top, clientRect.Right, clientRect.Bottom);

            //Call virtual function
            ClientAreaEventArgs e = new ClientAreaEventArgs(rect, ClientAreaRectangleRequestedEvent, this);

            //Call OnClientAreaRectangleRequested(ClientAreaEventArgs)
            OnClientAreaRectangleRequested(e);

            //Get rect structure
            return new RECT()
            {
                Top = e.ClientRectangle.Top,
                Left = e.ClientRectangle.Left,
                Right = e.ClientRectangle.Right,
                Bottom = e.ClientRectangle.Bottom
            };
        }
        private IntPtr GlowWindowHost_WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            //Prepare
            RECT rect, clnRect;

            //Handle message
            switch ((WindowMessages)msg)
            {
                case WindowMessages.WM_MOVE:
                    if (User32.GetWindowRect(hwnd, out rect))
                    {
                        //Set position
                        LeftGlow_SizePos(leftHandle.Handle, rect);
                        TopGlow_SizePos(topHandle.Handle, rect);
                        RightGlow_SizePos(rightHandle.Handle, rect);
                        BottomGlow_SizePos(bottomHandle.Handle, rect);
                    }
                    break;

                case WindowMessages.WM_SIZE:
                    if (User32.GetWindowRect(hwnd, out rect))
                    {
                        //Set visibility
                        int wmSizeWParam = wParam.ToInt32();
                        switch (wmSizeWParam)
                        {
                            case 1:
                            case 2: BordersVisible = false; break;
                            default: BordersVisible = true; break;
                        }

                        //Set size
                        LeftGlow_SizePos(leftHandle.Handle, rect);
                        TopGlow_SizePos(topHandle.Handle, rect);
                        RightGlow_SizePos(rightHandle.Handle, rect);
                        BottomGlow_SizePos(bottomHandle.Handle, rect);

                        //Check
                        if (wmSizeWParam != this.wmSizeWParam)
                        {
                            this.wmSizeWParam = wmSizeWParam;
                            Point sz = new Point(lParam.ToInt32());
                            _ = User32.SetWindowPos(hwnd, IntPtr.Zero, 0, 0, sz.X, sz.Y, 0x0020 | 0x0002);
                        }
                    }
                    break;

                case WindowMessages.WM_NCCALCSIZE:
                    handled = true;
                    if (wParam != IntPtr.Zero)  //check if wParam is TRUE
                    {
                        //Copy NCCALCSIZE_PARAMS from memory 
                        NCCALCSIZE_PARAMS nccsp = Marshal.PtrToStructure<NCCALCSIZE_PARAMS>(lParam);

                        //Request client rectangle
                        clnRect = GlowWindowHost_RequestClientRectangle(nccsp.rgrc0);
                        nccsp.rgrc0 = clnRect;

                        //Copy NCCALCSIZE_PARAMS into memory
                        Marshal.StructureToPtr(nccsp, lParam, true);
                    }
                    else
                    {
                        //Get window rect from lParam
                        rect = Marshal.PtrToStructure<RECT>(lParam);

                        //Request client rectangle
                        clnRect = GlowWindowHost_RequestClientRectangle(rect);

                        //Copy RECT into memory
                        Marshal.StructureToPtr(clnRect, lParam, true);
                    }

                    //Return
                    return IntPtr.Zero;

                case WindowMessages.WM_NCHITTEST:
                    Point htPoint = new Point(lParam.ToInt32());
                    if (User32.GetWindowRect(hwnd, out RECT wndRect))
                    {
                        //Hit-test
                        DpiScale dpi = VisualTreeHelper.GetDpi(this);
                        HitTestResult result = VisualTreeHelper.HitTest(this, new WpfPoint(
                            (htPoint.X - wndRect.Left) * dpi.DpiScaleX,
                            (htPoint.Y - wndRect.Top) * dpi.DpiScaleY));

                        if (result?.VisualHit is UIElement element)
                        {
                            if (element.GetValue(NonClientActionProperty) is NonClientHitAction action)
                            {
                                handled = true;

                                if (action == NonClientHitAction.Inherit)
                                {
                                    NonClientHitAction parentAction = action;
                                    DependencyObject parent = VisualTreeHelper.GetParent(element);
                                    while (parent != null)
                                    {
                                        parentAction = (NonClientHitAction)parent.GetValue(NonClientActionProperty);
                                        if (parentAction != NonClientHitAction.Inherit)
                                        {
                                            break;
                                        }

                                        parent = VisualTreeHelper.GetParent(parent);
                                    }

                                    if (parentAction == NonClientHitAction.Inherit)
                                    {
                                        action = NonClientHitAction.Client;
                                    }
                                    else
                                    {
                                        action = parentAction;
                                    }
                                }

                                switch (action)
                                {
                                    case NonClientHitAction.Caption:
                                        return (IntPtr)2;
                                    case NonClientHitAction.Icon:
                                        return (IntPtr)3;
                                    case NonClientHitAction.Top:
                                        return (IntPtr)12;
                                    case NonClientHitAction.Left:
                                        return (IntPtr)10;
                                    case NonClientHitAction.Right:
                                        return (IntPtr)11;
                                    case NonClientHitAction.Bottom:
                                        return (IntPtr)15;
                                    case NonClientHitAction.TopLeft:
                                        return (IntPtr)13;
                                    case NonClientHitAction.TopRight:
                                        return (IntPtr)14;
                                    case NonClientHitAction.BottomLeft:
                                        return (IntPtr)16;
                                    case NonClientHitAction.BottomRight:
                                        return (IntPtr)17;
                                    case NonClientHitAction.Client:
                                    default: return (IntPtr)1;
                                }
                            }
                        }
                    }
                    break;

                case WindowMessages.WM_ACTIVATE:
                    bool isActive = true;
                    bool isGlowWindowActive = (lParam == leftHandle.Handle || lParam == topHandle.Handle || lParam == rightHandle.Handle || lParam == bottomHandle.Handle);
                    if (wParam == IntPtr.Zero && !isGlowWindowActive)
                    {
                        isActive = false;
                    }

                    if (isActive != this.isActive)
                    {
                        WpfColor color;
                        if (isActive)
                        {
                            color = ActiveGlowColor;
                        }
                        else
                        {
                            color = InactiveGlowColor;
                        }

                        glowTextures.Render(Color.FromArgb(color.R, color.G, color.B));

                        GlowWindow_DrawLayeredWindow(leftHandle.Handle);
                        GlowWindow_DrawLayeredWindow(topHandle.Handle);
                        GlowWindow_DrawLayeredWindow(rightHandle.Handle);
                        GlowWindow_DrawLayeredWindow(bottomHandle.Handle);

                        this.isActive = isActive;
                        InvalidateVisual();
                    }
                    break;

                case WindowMessages.WM_DESTROY:
                    _ = hostDictionary.Remove(hwnd);
                    break;

                case (WindowMessages)0x031A:    //wm_themechanged
                    System.Diagnostics.Debugger.Break();    // this is unlikely to occur but we'll break here so we can see what's goin' on
                    GetThemeMargins();
                    User32.SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0, 0x0001 | 0x0002 | 0x0004 | 0x0010 | 0x0020 | 0x0400);
                    break;
            }

            return IntPtr.Zero;
        }

        static GlowWindowHost()
        {
            IntPtr hInstance = Marshal.GetHINSTANCE(typeof(GlowWindowHost).Module);
            IntPtr wndProc = Marshal.GetFunctionPointerForDelegate(glowWindowWndProc);

            WNDCLASSEX wcex = new WNDCLASSEX()
            {
                cbSize = Marshal.SizeOf<WNDCLASSEX>(),
                lpfnWndProc = wndProc,
                hInstance = hInstance,
                lpszClassName = GlowWindowClassName,
            };

            ushort classAtom = User32.RegisterClassEx(ref wcex);
            if (classAtom == 0)
            {
                throw new InvalidOperationException($"Unable to create \"{GlowWindowClassName}\" class atom.");
            }

            GlowWindowClassAtom.ClassAtom = classAtom;
            GlowWindowClassAtom.Lock();
        }
        private static IntPtr GlowWindow_WindowProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            //Prepare
            RECT wndRect = new RECT();
            IntPtr parentHWnd = User32.GetParent(hwnd);

            //Check
            switch ((WindowMessages)unchecked((int)msg))
            {
                case WindowMessages.WM_CREATE:
                    GlowWindow_DrawLayeredWindow(hwnd);
                    return IntPtr.Zero;

                case WindowMessages.WM_GETMINMAXINFO:
                    MINMAXINFO mmi = Marshal.PtrToStructure<MINMAXINFO>(lParam);
                    mmi.MinTrackSize = new POINT(1, 1);
                    Marshal.StructureToPtr(mmi, lParam, true);
                    return IntPtr.Zero;

                case WindowMessages.WM_NCHITTEST:
                    if (User32.GetWindowRect(parentHWnd, out wndRect))
                    {
                        //Get hit point
                        Point hitPt = new Point(lParam.ToInt32());

                        //Check corners
                        if (Rectangle.FromLTRB(wndRect.Left - 8, wndRect.Top - 8, wndRect.Left + 8, wndRect.Top + 8).Contains(hitPt))
                        {
                            return (IntPtr)13;
                        }

                        if (Rectangle.FromLTRB(wndRect.Right - 8, wndRect.Top - 8, wndRect.Right + 8, wndRect.Top + 8).Contains(hitPt))
                        {
                            return (IntPtr)14;
                        }

                        if (Rectangle.FromLTRB(wndRect.Left - 8, wndRect.Bottom - 8, wndRect.Left + 8, wndRect.Bottom + 8).Contains(hitPt))
                        {
                            return (IntPtr)16;
                        }

                        if (Rectangle.FromLTRB(wndRect.Right - 8, wndRect.Bottom - 8, wndRect.Right + 8, wndRect.Bottom + 8).Contains(hitPt))
                        {
                            return (IntPtr)17;
                        }

                        //Check edges
                        if (Rectangle.FromLTRB(wndRect.Left + 8, wndRect.Bottom, wndRect.Right - 8, wndRect.Bottom + 8).Contains(hitPt))
                        {
                            return (IntPtr)15;
                        }

                        if (Rectangle.FromLTRB(wndRect.Left + 8, wndRect.Top - 8, wndRect.Right - 8, wndRect.Top).Contains(hitPt))
                        {
                            return (IntPtr)12;
                        }

                        if (Rectangle.FromLTRB(wndRect.Right, wndRect.Top + 8, wndRect.Right + 8, wndRect.Bottom - 8).Contains(hitPt))
                        {
                            return (IntPtr)11;
                        }

                        if (Rectangle.FromLTRB(wndRect.Left - 8, wndRect.Top + 8, wndRect.Left, wndRect.Bottom - 8).Contains(hitPt))
                        {
                            return (IntPtr)10;
                        }

                        return IntPtr.Zero;
                    }
                    break;

                case WindowMessages.WM_NCLBUTTONDOWN:
                case WindowMessages.WM_NCLBUTTONUP:
                    _ = User32.SendNotifyMessage(parentHWnd, msg, wParam, lParam);
                    return IntPtr.Zero;
            }

            return User32.DefWindowProc(hwnd, msg, wParam, lParam);
        }
        private static void GlowWindow_DrawLayeredWindow(IntPtr hwnd)
        {
            //Prepare
            Bitmap bitmap = null;

            //Get window rect
            if (User32.GetWindowRect(hwnd, out RECT wndRect))
            {
                //Get parent
                IntPtr parentHWnd = User32.GetParent(hwnd);

                try
                {
                    //Create bitmap
                    bitmap = new Bitmap(wndRect.Width, wndRect.Height);

                    //Draw
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        //Clear
                        g.Clear(Color.Transparent);
                        g.PageUnit = GraphicsUnit.Pixel;

                        //Check
                        if (hostDictionary.ContainsKey(parentHWnd))
                        {
                            GlowWindowHost host = hostDictionary[parentHWnd];

                            //Draw
                            if (hwnd == host.leftHandle.Handle)
                            {
                                LeftGlow_DrawLayeredWindow(host.glowTextures, g, wndRect.Width, wndRect.Height);
                            }

                            if (hwnd == host.topHandle.Handle)
                            {
                                TopGlow_DrawLayeredWindow(host.glowTextures, g, wndRect.Width, wndRect.Height);
                            }

                            if (hwnd == host.rightHandle.Handle)
                            {
                                RightGlow_DrawLayeredWindow(host.glowTextures, g, wndRect.Width, wndRect.Height);
                            }

                            if (hwnd == host.bottomHandle.Handle)
                            {
                                BottomGlow_DrawLayeredWindow(host.glowTextures, g, wndRect.Width, wndRect.Height);
                            }
                        }
                    }
                }
                catch { }

                //Prepare
                IntPtr screenDc = User32.GetDC(IntPtr.Zero);
                IntPtr memDc = Gdi32.CreateCompatibleDC(screenDc);
                IntPtr hBitmap = IntPtr.Zero;
                IntPtr oldBitmap = IntPtr.Zero;

                try
                {
                    hBitmap = bitmap.GetHbitmap(Color.FromArgb(0x0));
                    oldBitmap = Gdi32.SelectObject(memDc, hBitmap);

                    SIZE sz = new SIZE(wndRect.Width, wndRect.Height);
                    POINT ptSrc = new POINT(0, 0);
                    POINT topPos = new POINT(wndRect.X, wndRect.Y);
                    BLENDFUNCTION blend = new BLENDFUNCTION()
                    {
                        BlendOp = 0x0,
                        BlendFlags = 0x0,
                        SourceConstantAlpha = 0xFF,
                        AlphaFormat = 0x01,
                    };

                    //Update layered window
                    _ = User32.UpdateLayeredWindow(hwnd, screenDc, ref topPos, ref sz, memDc, ref ptSrc, 0, ref blend, 0x00000002);
                }
                finally
                {
                    _ = User32.ReleaseDC(IntPtr.Zero, screenDc);
                    if (hBitmap != IntPtr.Zero)
                    {
                        _ = Gdi32.SelectObject(memDc, oldBitmap);
                        _ = Gdi32.DeleteDC(hBitmap);
                    }
                    _ = Gdi32.DeleteDC(memDc);

                    //Dispose of bitmap
                    bitmap.Dispose();
                }
            }
        }
        private static void LeftGlow_DrawLayeredWindow(GlowTextures glowTextures, Graphics g, int width, int height)
        {
            glowTextures.LeftBrush.ResetTransform();
            g.DrawImage(glowTextures.LeftWindowTop, Rectangle.FromLTRB(0, 0, 9, 18));
            g.DrawImage(glowTextures.LeftWindowBottom, Rectangle.FromLTRB(0, height - 18, 9, height));
            g.FillRectangle(glowTextures.LeftBrush, Rectangle.FromLTRB(0, 18, 9, height - 18));
        }
        private static void TopGlow_DrawLayeredWindow(GlowTextures glowTextures, Graphics g, int width, int height)
        {
            glowTextures.TopBrush.ResetTransform();
            g.DrawImage(glowTextures.TopWindowLeft, Rectangle.FromLTRB(0, 0, 9, 9));
            g.DrawImage(glowTextures.TopWindowRight, Rectangle.FromLTRB(width - 9, 0, width, 9));
            g.FillRectangle(glowTextures.TopBrush, Rectangle.FromLTRB(9, 0, width - 9, 9));
        }
        private static void RightGlow_DrawLayeredWindow(GlowTextures glowTextures, Graphics g, int width, int height)
        {
            glowTextures.RightBrush.ResetTransform();
            g.DrawImage(glowTextures.RightWindowTop, Rectangle.FromLTRB(0, 0, 9, 18));
            g.DrawImage(glowTextures.RightWindowBottom, Rectangle.FromLTRB(0, height - 18, 9, height));
            g.FillRectangle(glowTextures.RightBrush, Rectangle.FromLTRB(0, 18, 9, height - 18));
        }
        private static void BottomGlow_DrawLayeredWindow(GlowTextures glowTextures, Graphics g, int width, int height)
        {
            glowTextures.BottomBrush.ResetTransform();
            g.DrawImage(glowTextures.BottomWindowLeft, Rectangle.FromLTRB(0, 0, 9, 9));
            g.DrawImage(glowTextures.BottomWindowRight, Rectangle.FromLTRB(width - 9, 0, width, 9));
            g.FillRectangle(glowTextures.BottomBrush, Rectangle.FromLTRB(9, 0, width - 9, 9));
        }
        private static void BordersVisiblePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Check
            if (d is GlowWindowHost host && e.NewValue is bool visible)
            {
                //Get cmdShow
                int cmdShow = visible ? 5 : 0;

                //Show or hide
                _ = User32.ShowWindow(host.leftHandle.Handle, cmdShow);
                _ = User32.ShowWindow(host.topHandle.Handle, cmdShow);
                _ = User32.ShowWindow(host.rightHandle.Handle, cmdShow);
                _ = User32.ShowWindow(host.bottomHandle.Handle, cmdShow);

                //Redraw
                host.InvalidateVisual();
            }
        }
        private static void ActiveGlowColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Check
            if (d is GlowWindowHost host && host.IsActive)
            {
                //Re-render glow textures
                WpfColor wpfColor = (WpfColor)e.NewValue;
                host.glowTextures.Render(Color.FromArgb(wpfColor.R, wpfColor.G, wpfColor.B));

                //Re-draw edges
                GlowWindow_DrawLayeredWindow(host.leftHandle.Handle);
                GlowWindow_DrawLayeredWindow(host.topHandle.Handle);
                GlowWindow_DrawLayeredWindow(host.rightHandle.Handle);
                GlowWindow_DrawLayeredWindow(host.bottomHandle.Handle);
            }
        }
        private static void InactiveGlowColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Check
            if (d is GlowWindowHost host && !host.IsActive)
            {
                //Re-render glow textures
                WpfColor wpfColor = (WpfColor)e.NewValue;
                host.glowTextures.Render(Color.FromArgb(wpfColor.R, wpfColor.G, wpfColor.B));

                //Re-draw edges
                GlowWindow_DrawLayeredWindow(host.leftHandle.Handle);
                GlowWindow_DrawLayeredWindow(host.topHandle.Handle);
                GlowWindow_DrawLayeredWindow(host.rightHandle.Handle);
                GlowWindow_DrawLayeredWindow(host.bottomHandle.Handle);
            }
        }
        public static void SetNonClientAction(UIElement element, NonClientHitAction action)
        {
            element.SetValue(NonClientActionProperty, action);
        }
        public static NonClientHitAction GetNonClientAction(UIElement element)
        {
            return (NonClientHitAction)element.GetValue(NonClientActionProperty);
        }
    }
    
    public enum NonClientHitAction : int
    {
        Client = 0,
        Caption = 1,
        Icon = 2,
        Top = 3,
        Left = 4,
        Right = 5,
        Bottom = 6,
        TopLeft = 7,
        TopRight = 8,
        BottomLeft = 9,
        BottomRight = 10,
        Inherit= 11,
    };

    public delegate void ClientAreaEventHandler(object sender, ClientAreaEventArgs e);

    public sealed class ClientAreaEventArgs : RoutedEventArgs
    {
        public Rectangle WindowRectangle { get; }
        public Rectangle ClientRectangle { get; set; }
        public ClientAreaEventArgs(Rectangle windowRectangle, RoutedEvent routedEvent, object source) : base(routedEvent, source)
        {
            WindowRectangle = windowRectangle;
            ClientRectangle = windowRectangle;
        }
    }

    public sealed class WindowHandle : Lockable<IntPtr>
    {
        public bool IsHandleCreated => Object != IntPtr.Zero;
        public IntPtr Handle
        {
            get => Object;
            set => Object = value;
        }

        public WindowHandle()
        {
            Object = IntPtr.Zero;
        }
    }

    public sealed class WindowClassAtom : Lockable<ushort>
    {
        private new ushort Object
        {
            get => base.Object;
            set => base.Object = value;
        }

        public bool IsValid => Object != 0;
        public ushort ClassAtom
        {
            get => Object;
            set => Object = value;
        }

        public WindowClassAtom()
        {
            Object = 0;
        }
        public WindowClassAtom(ushort classAtom) : base(classAtom) { }
        public override string ToString()
        {
            //Return
            return $"0x{ClassAtom:X4} Locked: {(IsLocked ? "True" : "False")}";
        }
    }

    public abstract class Lockable<T>
    {
        private T obj;

        public bool IsLocked { get; private set; } = false;
        protected T Object
        {
            get => obj;
            set
            {
                if (!IsLocked)
                {
                    obj = value;
                }
                else
                {
                    throw new InvalidOperationException("Object is locked and cannot be modified.");
                }
            }
        }

        protected Lockable() { }
        protected Lockable(T obj)
        {
            this.obj = obj;
            IsLocked = true;
        }
        public void Lock()
        {
            if (!IsLocked)
            {
                IsLocked = true;
            }
        }
        public override string ToString()
        {
            return $"{obj.ToString()} Locked: {(IsLocked ? "True" : "False")}";
        }
    }

    internal class GlowTextures : IDisposable
    {
        private bool isDisposed = false;

        public TextureBrush LeftBrush;
        public TextureBrush TopBrush;
        public TextureBrush RightBrush;
        public TextureBrush BottomBrush;

        public Bitmap LeftWindowTop;
        public Bitmap LeftWindow;
        public Bitmap LeftWindowBottom;
        public Bitmap TopWindowRight;
        public Bitmap TopWindow;
        public Bitmap TopWindowLeft;
        public Bitmap RightWindowTop;
        public Bitmap RightWindow;
        public Bitmap RightWindowBottom;
        public Bitmap BottomWindowLeft;
        public Bitmap BottomWindow;
        public Bitmap BottomWindowRight;

        public GlowTextures()
        {
            //Render
            Render(Color.FromArgb(0x9839ff));
        }
        public void Render(Color glowColor)
        {
            if (isDisposed)
            {
                throw new ObjectDisposedException(nameof(GlowTextures));
            }

            LeftWindowTop?.Dispose();
            LeftWindow?.Dispose();
            LeftWindowBottom?.Dispose();
            TopWindowRight?.Dispose();
            TopWindow?.Dispose();
            TopWindowLeft?.Dispose();
            RightWindowTop?.Dispose();
            RightWindow?.Dispose();
            RightWindowBottom?.Dispose();
            BottomWindowLeft?.Dispose();
            BottomWindow?.Dispose();
            BottomWindowRight?.Dispose();
            LeftBrush?.Dispose();
            TopBrush?.Dispose();
            RightBrush?.Dispose();
            BottomBrush?.Dispose();

            //Prepare
            var glowWindow = Properties.Resources.Glow_Window;
            Bitmap bitm = null;

            //Get sRGB
            float sR = glowColor.R / 255f, sG = glowColor.G / 255f, sB = glowColor.B / 255f;

            //Create color matrix
            ColorMatrix cm = new ColorMatrix(new float[][]
            {
                new float[] { sR, 00, 00, 00, 00},
                new float[] { 00, sG, 00, 00, 00},
                new float[] { 00, 00, sB, 00, 00},
                new float[] { 00, 00, 00, 01, 00},
                new float[] { 00, 00, 00, 00, 01},
            });

            //Create ImageAttributes
            using (ImageAttributes imgAttr = new ImageAttributes())
            {
                //Set color matrix
                imgAttr.SetColorMatrix(cm);

                //Prepare left textures
                bitm = LeftWindowTop = new Bitmap(9, 18);
                using (Graphics g = Graphics.FromImage(bitm))
                {
                    g.DrawImage(glowWindow, new Rectangle(0, 0, 9, 18), 0, 0, 9, 18, GraphicsUnit.Pixel, imgAttr);
                }

                bitm = LeftWindow = new Bitmap(9, 1);
                using (Graphics g = Graphics.FromImage(bitm))
                {
                    g.DrawImage(glowWindow, new Rectangle(0, 0, 9, 1), 0, 18, 9, 1, GraphicsUnit.Pixel, imgAttr);
                }

                bitm = LeftWindowBottom = new Bitmap(9, 18);
                using (Graphics g = Graphics.FromImage(bitm))
                {
                    g.DrawImage(glowWindow, new Rectangle(0, 0, 9, 18), 0, 19, 9, 18, GraphicsUnit.Pixel, imgAttr);
                }

                //Prepare top textures
                bitm = TopWindowLeft = new Bitmap(9, 9);
                using (Graphics g = Graphics.FromImage(bitm))
                {
                    g.DrawImage(glowWindow, new Rectangle(0, 0, 9, 9), 9, 0, 9, 9, GraphicsUnit.Pixel, imgAttr);
                }

                bitm = TopWindow = new Bitmap(1, 9);
                using (Graphics g = Graphics.FromImage(bitm))
                {
                    g.DrawImage(glowWindow, new Rectangle(0, 0, 1, 9), 18, 0, 1, 9, GraphicsUnit.Pixel, imgAttr);
                }

                bitm = TopWindowRight = new Bitmap(9, 9);
                using (Graphics g = Graphics.FromImage(bitm))
                {
                    g.DrawImage(glowWindow, new Rectangle(0, 0, 9, 9), 19, 0, 9, 9, GraphicsUnit.Pixel, imgAttr);
                }

                //Prepare right textures
                bitm = RightWindowTop = new Bitmap(9, 18);
                using (Graphics g = Graphics.FromImage(bitm))
                {
                    g.DrawImage(glowWindow, new Rectangle(0, 0, 9, 18), 28, 0, 9, 18, GraphicsUnit.Pixel, imgAttr);
                }

                bitm = RightWindow = new Bitmap(9, 1);
                using (Graphics g = Graphics.FromImage(bitm))
                {
                    g.DrawImage(glowWindow, new Rectangle(0, 0, 9, 1), 28, 18, 9, 1, GraphicsUnit.Pixel, imgAttr);
                }

                bitm = RightWindowBottom = new Bitmap(9, 18);
                using (Graphics g = Graphics.FromImage(bitm))
                {
                    g.DrawImage(glowWindow, new Rectangle(0, 0, 9, 18), 28, 19, 9, 18, GraphicsUnit.Pixel, imgAttr);
                }

                //Prepare bottom textures
                bitm = BottomWindowLeft = new Bitmap(9, 9);
                using (Graphics g = Graphics.FromImage(bitm))
                {
                    g.DrawImage(glowWindow, new Rectangle(0, 0, 9, 9), 9, 28, 9, 9, GraphicsUnit.Pixel, imgAttr);
                }

                bitm = BottomWindow = new Bitmap(1, 9);
                using (Graphics g = Graphics.FromImage(bitm))
                {
                    g.DrawImage(glowWindow, new Rectangle(0, 0, 1, 9), 18, 28, 1, 9, GraphicsUnit.Pixel, imgAttr);
                }

                bitm = BottomWindowRight = new Bitmap(9, 9);
                using (Graphics g = Graphics.FromImage(bitm))
                {
                    g.DrawImage(glowWindow, new Rectangle(0, 0, 9, 9), 19, 28, 9, 9, GraphicsUnit.Pixel, imgAttr);
                }

                //Prepare edge brushes
                LeftBrush = new TextureBrush(LeftWindow, WrapMode.Tile);
                TopBrush = new TextureBrush(TopWindow, WrapMode.Tile);
                RightBrush = new TextureBrush(RightWindow, WrapMode.Tile);
                BottomBrush = new TextureBrush(BottomWindow, WrapMode.Tile);
            }
        }
        public void Dispose()
        {
            if (isDisposed)
            {
                return;
            }

            isDisposed = true;

            LeftWindowTop?.Dispose();
            LeftWindow?.Dispose();
            LeftWindowBottom?.Dispose();
            TopWindowRight?.Dispose();
            TopWindow?.Dispose();
            TopWindowLeft?.Dispose();
            RightWindowTop?.Dispose();
            RightWindow?.Dispose();
            RightWindowBottom?.Dispose();
            BottomWindowLeft?.Dispose();
            BottomWindow?.Dispose();
            BottomWindowRight?.Dispose();

            LeftBrush?.Dispose();
            TopBrush?.Dispose();
            RightBrush?.Dispose();
            BottomBrush?.Dispose();
        }
    }
}
