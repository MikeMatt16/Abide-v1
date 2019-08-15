using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using Point = System.Drawing.Point;

namespace Abide.Guerilla.Wpf.Ui
{
    public class CustomBorderWindow : Window
    {
        public event EventHandler WindowMaximized;
        public event EventHandler WindowRestored;
        public event EventHandler WindowMinimized;
        public event EventHandler<ClientAreaEventArgs> ClientAreaRectangleRequested;

        public bool CanClose { get; set; } = true;
        public bool CanMaximizeOrRestore { get; set; } = true;
        public bool CanMinimize { get; set; } = true;
        public int CaptionHeight { get; set; } = 31;
        public ICommand CloseCommand { get; }
        public ICommand MaximizeRestoreCommand { get; }
        public ICommand MinimizeCommand { get; }
        public int WmSizeWParam
        {
            get { return wmSizeWParam; }
            set
            {
                if (wmSizeWParam != value)
                {
                    //Setup
                    EventArgs e = new EventArgs();
                    wmSizeWParam = value;

                    //Check
                    switch (value)
                    {
                        case 0:
                            WindowRestored?.Invoke(this, e);
                            OnWindowRestored(e);
                            break;
                        case 1:
                            WindowMinimized?.Invoke(this, e);
                            OnWindowMinimized(e);
                            break;
                        case 2:
                            WindowMaximized?.Invoke(this, e);
                            OnWindowMaximized(e);
                            break;
                    }
                }
            }
        }

        private int wmSizeWParam = 0;
        private GlowWindow TopGlow, LeftGlow, RightGlow, BottomGlow;
        private HwndSource WindowHandleSource = null;

        public CustomBorderWindow()
        {
            //Minimum
            MinWidth = MinHeight = 200;

            //Setup
            CloseCommand = new RelayCommand(p => Close_Execute(), p => CanClose);
            MaximizeRestoreCommand = new RelayCommand(p => MaximizeRestore_Execute(), p => CanMaximizeOrRestore);
            MinimizeCommand = new RelayCommand(p => Minimize_Execute(), p => CanMinimize);
        }

        public void SetBordersVisible(bool visible)
        {
            uint swp = (uint)(0x0002 | 0x0001 | (visible ? 0x0040 : 0x0080));
            SetWindowPos(TopGlow.Handle, IntPtr.Zero, 0,0,0,0, swp);
            SetWindowPos(LeftGlow.Handle, IntPtr.Zero, 0, 0, 0, 0, swp);
            SetWindowPos(RightGlow.Handle, IntPtr.Zero, 0, 0, 0, 0, swp);
            SetWindowPos(BottomGlow.Handle, IntPtr.Zero, 0, 0, 0, 0, swp);
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            //Get window handle source
            if (PresentationSource.FromVisual(this) is HwndSource source)
                WindowHandleSource = source;

            //Check
            if (WindowHandleSource != null)
            {
                //Initialize
                TopGlow = new GlowWindow(WindowHandleSource.Handle, GlowWindowEdge.Top);
                LeftGlow = new GlowWindow(WindowHandleSource.Handle, GlowWindowEdge.Left);
                RightGlow = new GlowWindow(WindowHandleSource.Handle, GlowWindowEdge.Right);
                BottomGlow = new GlowWindow(WindowHandleSource.Handle, GlowWindowEdge.Bottom);

                //Update

                //Add hook
                WindowHandleSource.AddHook(WndProc);
            }
        }

        private void Close_Execute()
        {
            //Close
            Close();
        }

        private void MaximizeRestore_Execute()
        {
            //Restore or maximize
            if(WmSizeWParam == 2)
                ShowWindow(WindowHandleSource.Handle, 9);
            else
                ShowWindow(WindowHandleSource.Handle, 3);
        }

        private void Minimize_Execute()
        {
            //Minimize
            ShowWindow(WindowHandleSource.Handle, 6);
        }
        
        private RECT RequestClientRectangle(RECT clientRect)
        {
            //Get some metrics
            int cyCaption = GetSystemMetrics(4);
            int cxBorder = GetSystemMetrics(5);
            int cyBorder = GetSystemMetrics(6);
            int cxSizeFrame = GetSystemMetrics(32);
            int cySizeFrame = GetSystemMetrics(33);
            int cxEdge = GetSystemMetrics(45);
            int cyEdge = GetSystemMetrics(46);

            //Check
            if (WmSizeWParam == 2)
            {
                clientRect.Top += cyBorder + cyEdge + cySizeFrame;
                clientRect.Bottom -= cyBorder + cyEdge + cySizeFrame;
                clientRect.Left += cxBorder + cxEdge + cxSizeFrame;
                clientRect.Right -= cxBorder + cxEdge + cxSizeFrame;
            }

            //Get System.Drawing.Rectangle from client rect
            Rectangle rect = Rectangle.FromLTRB(clientRect.Left, clientRect.Top, clientRect.Right, clientRect.Bottom);

            //Call virtual function
            ClientAreaEventArgs e = new ClientAreaEventArgs(rect);

            //Raise event
            ClientAreaRectangleRequested?.Invoke(this, e);

            //Call OnClientAreaRectangleRequested
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

        protected virtual void OnClientAreaRectangleRequested(ClientAreaEventArgs e)
        {
        }

        protected virtual void OnWindowMaximized(EventArgs e)
        {
        }

        protected virtual void OnWindowMinimized(EventArgs e)
        {
        }

        protected virtual void OnWindowRestored(EventArgs e)
        {
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            //Prepare
            RECT rect, clientRect;

            //Handle
            switch (msg)
            {
                case 0x0003:
                    TopGlow.Update();
                    LeftGlow.Update();
                    BottomGlow.Update();
                    RightGlow.Update();
                    break;
                case 0x0005:
                    int wp = wParam.ToInt32();
                    switch (wp)
                    {
                        case 1:
                        case 2:
                            TopGlow.Update(false);
                            LeftGlow.Update(false);
                            BottomGlow.Update(false);
                            RightGlow.Update(false);
                            break;
                        case 0:
                            TopGlow.Update(true);
                            LeftGlow.Update(true);
                            BottomGlow.Update(true);
                            RightGlow.Update(true);
                            break;
                    }

                    OnWmSize(wp, lParam.ToInt32());
                    break;
                //case 0x0006:
                //    switch (wParam.ToInt32())
                //    {
                //        case 0:
                //            TopGlow.Deactivate();
                //            LeftGlow.Deactivate();
                //            BottomGlow.Deactivate();
                //            RightGlow.Deactivate();
                //            break;
                //        case 1:
                //        case 2:
                //            TopGlow.Activate();
                //            LeftGlow.Activate();
                //            BottomGlow.Activate();
                //            RightGlow.Activate();
                //            break;
                //    }
                //    break;
                case 0x0083:
                    handled = true;
                    if (wParam != IntPtr.Zero)  //check if wParam is TRUE
                    {
                        //Copy NCCALCSIZE_PARAMS from memory 
                        NCCALCSIZE_PARAMS nccsp = Marshal.PtrToStructure<NCCALCSIZE_PARAMS>(lParam);

                        //Request client rectangle
                        clientRect = RequestClientRectangle(nccsp.rgrc0);
                        nccsp.rgrc0 = clientRect;

                        //Copy NCCALCSIZE_PARAMS into memory
                        Marshal.StructureToPtr(nccsp, lParam, true);
                    }
                    else
                    {
                        //Get window rect from lParam
                        rect = Marshal.PtrToStructure<RECT>(lParam);

                        //Request client rectangle
                        clientRect = RequestClientRectangle(rect);

                        //Copy RECT into memory
                        Marshal.StructureToPtr(clientRect, lParam, true);
                    }

                    //Return
                    return IntPtr.Zero;
                case 0x0084:
                    Point htPoint = new Point(lParam.ToInt32());
                    if (GetWindowRect(hwnd, out RECT wndRect))
                    {
                        //Check if caption area contains cursor
                        if (Rectangle.FromLTRB(wndRect.Left, wndRect.Top, wndRect.Right, wndRect.Top + CaptionHeight).Contains(htPoint))
                        {
                            //Check if over caption button area
                            if (Rectangle.FromLTRB(wndRect.Right - 102, wndRect.Top, wndRect.Right, wndRect.Top + 26).Contains(htPoint))
                                return IntPtr.Zero;

                            //Check if over icon area
                            if (Rectangle.FromLTRB(wndRect.Left, wndRect.Top, wndRect.Left + 32, wndRect.Top + 32).Contains(htPoint))
                            {
                                handled = true;
                                return (IntPtr)3;
                            }

                            //Return HTCAPTION
                            handled = true;
                            return new IntPtr(2);
                        }
                    }
                    break;

                case 0x0086:
                    handled = true;
                    if(wParam != IntPtr.Zero)
                    {
                        TopGlow.SetParentActivation(true);
                        LeftGlow.SetParentActivation(true);
                        BottomGlow.SetParentActivation(true);
                        RightGlow.SetParentActivation(true);
                    }
                    else
                    {
                        TopGlow.SetParentActivation(false);
                        LeftGlow.SetParentActivation(false);
                        BottomGlow.SetParentActivation(false);
                        RightGlow.SetParentActivation(false);
                        return new IntPtr(1);
                    }
                    return IntPtr.Zero;
            }

            //Return
            return IntPtr.Zero;
        }

        private void OnWmSize(int wParam, int lParam)
        {
            //Check
            if (WmSizeWParam != wParam)
            {
                //Set
                WmSizeWParam = wParam;
                Point sz = new Point(lParam);

                //Set
                SetWindowPos(WindowHandleSource.Handle, IntPtr.Zero, 0, 0, sz.X, sz.Y, 0x0020 | 0x0002);
            }
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

        [StructLayout(LayoutKind.Sequential)]
        private struct WINDOWPOS
        {
            public IntPtr hwndInsertAfter;
            public IntPtr hwnd;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public uint flags;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct NCCALCSIZE_PARAMS
        {
            public RECT rgrc0, rgrc1, rgrc2;
            public WINDOWPOS lppos;
        }

        [DllImport("user32.dll")]
        private static extern bool SendNotifyMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        
        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("user32.dll")]
        private static extern IntPtr DefWindowProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(int nIndex);
    }

    public class ClientAreaEventArgs : EventArgs
    {
        public Rectangle WindowRectangle { get; }
        public Rectangle ClientRectangle { get; set; }

        public ClientAreaEventArgs(Rectangle windowRectangle)
        {
            WindowRectangle = windowRectangle;
            ClientRectangle = windowRectangle;
        }
    }
}
