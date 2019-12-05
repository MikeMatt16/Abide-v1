using System;
using System.Runtime.InteropServices;

namespace Abide.Guerilla.Wpf.Ui.Win32
{
    /// <summary>
    /// Represents parameters used to create a native window.
    /// </summary>
    public class CreateParameters
    {
        /// <summary>
        /// Gets or sets the window extended style flags.
        /// </summary>
        /// <remarks>Visit https://docs.microsoft.com/en-us/windows/win32/winmsg/extended-window-styles for more information regarding extended window styles.</remarks>
        public ExtendedWindowStyles ExtendedWindowStyles { get; set; }
        /// <summary>
        /// Gets or sets the window style flags.
        /// </summary>
        /// <remarks>Visit https://docs.microsoft.com/en-us/windows/win32/winmsg/window-styles for more information regarding window styles.</remarks>
        public WindowStyles WindowStyles { get; set; }
        /// <summary>
        /// Gets or sets the window class name.
        /// </summary>
        /// <remarks>Visit https://docs.microsoft.com/en-us/windows/win32/winmsg/about-window-classes#class-name for more information regarding class names.</remarks>
        public string ClassName
        {
            get { return wndClass; }
            set { wndClass = value ?? throw new ArgumentNullException("Window class cannot be null.", nameof(value)); }
        }
        /// <summary>
        /// Gets or sets the window class style flags.
        /// </summary>
        /// <remarks>Visit https://docs.microsoft.com/en-us/windows/win32/winmsg/about-window-classes#class-name for more information regarding class styles</remarks>
        public WindowClassStyles WindowClassStyles
        {
            get { return (WindowClassStyles)wndStyle; }
            set { wndStyle = (uint)value; }
        }

        private string wndClass = string.Empty;
        private uint wndStyle = 0x0;

        /// <summary>
        /// Initailizes a new instance of the <see cref="CreateParameters"/> class.
        /// </summary>
        public CreateParameters() { }
        /// <summary>
        /// Returns a new instance of the <see cref="WNDCLASSEX"/> structure to be used to register a window class.
        /// </summary>
        /// <returns>A new <see cref="WNDCLASSEX"/> structure.</returns>
        public WNDCLASSEX GetWndClassEx()
        {
            return new WNDCLASSEX()
            {
                cbSize = Marshal.SizeOf<WNDCLASSEX>(),
                style = wndStyle,
                lpszClassName = wndClass,
                hCursor = User32.LoadCursor(IntPtr.Zero, 32512),
            };
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Flags()]
    public enum WindowStyles : uint
    {
        /// <summary>
        /// The window has a thin-line border.
        /// </summary>
        Border = 0x00800000,
        /// <summary>
        /// The window has a title bar (includes the <see cref="Border"/> style).
        /// </summary>
        Caption = 0x00C00000,
        /// <summary>
        /// The window is a child window. A window with this style cannot have a menu bar. This style cannot be used with the <see cref="Popup"/> style.
        /// </summary>
        Child = 0x40000000,
        /// <summary>
        /// Same as the <see cref="Child"/> style.
        /// </summary>
        ChildWindow = 0x40000000,
        /// <summary>
        /// Excludes the area occupied by child windows when drawing occurs within the parent window.
        /// This style is used when creating the parent window.
        /// </summary>
        ClipChildren = 0x02000000,
        /// <summary>
        /// Clips child windows relative to each other; that is, when a particular child window receives a WM_PAINT message,
        /// the <see cref="ClipSiblings"/> style clips all other overlapping child windows out of the region of the child window to be updated.
        /// </summary>
        ClipSiblings = 0x04000000,
        /// <summary>
        /// The window is initially disabled. A disabled window cannot receive input from the user.
        /// </summary>
        Disabled = 0x08000000,
        /// <summary>
        /// The window has a border of a style typically used with dialog boxes.
        /// A window with this style cannot have a title bar.
        /// </summary>
        DialogFrame = 0x00400000,
        /// <summary>
        /// The window is the first control of a group of controls. The group consists of this first control and all
        /// controls defined after it, up to the next control with the <see cref="Group"/> style.
        /// </summary>
        Group = 0x00020000,
        /// <summary>
        /// The window has a horizontal scroll bar.
        /// </summary>
        HorizontalScroll = 0x00100000,
        /// <summary>
        /// The window is initially minimized. Same as the <see cref="Minimize"/> style.
        /// </summary>
        Iconic = 0x20000000,
        /// <summary>
        /// The window is initially maximized.
        /// </summary>
        Maximize = 0x01000000,
        /// <summary>
        /// he window has a maximize button. The <see cref="SystemMenu"/> style must also be specified. 
        /// </summary>
        MaximizeBox = 0x00010000,
        /// <summary>
        /// The window is initially minimized. Same as the <see cref="Iconic"/> style.
        /// </summary>
        Minimize = 0x20000000,
        /// <summary>
        /// The window has a minimize button. The <see cref="SystemMenu"/> style must also be specified. 
        /// </summary>
        MinimizeBox = 0x00020000,
        /// <summary>
        /// The window is an overlapped window. An overlapped window has a title bar and a border. Same as the <see cref="Tiled"/> style.
        /// </summary>
        Overlapped = 0x00000000,
        /// <summary>
        /// The window is an overlapped window. Same as the <see cref="TiledWindow"/> style. 
        /// </summary>
        OverlappedWindow = Overlapped | Caption | SystemMenu | ThickFrame | MinimizeBox | MaximizeBox,
        /// <summary>
        /// The windows is a pop-up window. This style cannot be used with the <see cref="Child"/> style.
        /// </summary>
        Popup = 0x80000000,
        /// <summary>
        /// The window is a pop-up window. The <see cref="Caption"/> and <see cref="PopupWindow"/> styles
        /// must be combined to make the window menu visible.
        /// </summary>
        PopupWindow = Popup | Border | SystemMenu,
        /// <summary>
        /// The window has a sizing border. Same as the <see cref="ThickFrame"/> style.
        /// </summary>
        SizeBox = 0x00040000,
        /// <summary>
        /// The window has a window menu on its title bar. The <see cref="Caption"/> style must also be specified.
        /// </summary>
        SystemMenu = 0x00080000,
        /// <summary>
        /// The window is a control that can receive the keyboard focus when the user presses the TAB key.
        /// Pressing the TAB key changes the keyboard focus to the next control with the <see cref="TabStop"/> style.
        /// </summary>
        TabStop = 0x00010000,
        /// <summary>
        /// The window has a sizing border. Same as the <see cref="SizeBox"/> style.
        /// </summary>
        ThickFrame = 0x00040000,
        /// <summary>
        /// The window is an overlapped window. An overlapped window has a title bar and a border. Same as the <see cref="Overlapped"/> style. 
        /// </summary>
        Tiled = 0x00000000,
        /// <summary>
        /// The window is an overlapped window. Same as the <see cref="OverlappedWindow"/> style. 
        /// </summary>
        TiledWindow = Overlapped | Caption | SystemMenu | ThickFrame | MinimizeBox | MaximizeBox,
        /// <summary>
        /// The window is initially visible.
        /// </summary>
        Visible = 0x10000000,
        /// <summary>
        /// The window has a vertical scroll bar.
        /// </summary>
        VerticalScroll = 0x00200000,
    }

    /// <summary>
    /// Represents the possible extended window styles used by the <see cref="CreateParameters.ExtendedWindowStyles"/> property.
    /// </summary>
    [Flags()]
    public enum ExtendedWindowStyles : uint
    {
        /// <summary>
        /// The window accepts drag-drop files.
        /// </summary>
        AcceptFiles = 0x00000010,
        /// <summary>
        /// orces a top-level window onto the taskbar when the window is visible. 
        /// </summary>
        AppWindow = 0x00040000,
        /// <summary>
        /// The window has a border with a sunken edge.
        /// </summary>
        ClientEdge = 0x00000200,
        /// <summary>
        /// Paints all descendants of a window in bottom-to-top painting order using double-buffering. This cannot be used
        /// if the window has a class style of either <see cref="WindowClassStyles.OwnDC"/> or <see cref="WindowClassStyles.ClassDC"/>. 
        /// </summary>
        Composited = 0x02000000,
        /// <summary>
        /// The title bar of the window includes a question mark. When the user clicks the question mark, the cursor changes to a
        /// question mark with a pointer. If the user then clicks a child window, the child receives a WM_HELP message.
        /// </summary>
        ContextHelp = 0x00000400,
        /// <summary>
        /// The window itself contains child windows that should take part in dialog box navigation. If this style is specified,
        /// the dialog manager recurses into children of this window when performing navigation operations such as handling the 
        /// TAB key, an arrow key, or a keyboard mnemonic.
        /// </summary>
        ControlParent = 0x00010000,
        /// <summary>
        /// The window has a double border; the window can, optionally, be created with a title bar by
        /// specifying the <see cref="WindowStyles.Caption"/> style in the <see cref="CreateParameters.WindowStyles"/> property.
        /// </summary>
        DialogModalFrame = 0x00000001,
        /// <summary>
        /// The window is a layered window. This style cannot be used if the window has a class style of either
        /// <see cref="WindowClassStyles.OwnDC"/> or <see cref="WindowClassStyles.ClassDC"/>.
        /// </summary>
        Layered = 0x00080000,
        /// <summary>
        /// If the shell language is Hebrew, Arabic, or another language that supports reading order alignment, the horizontal origin of the 
        /// window is on the right edge. Increasing horizontal values advance to the left. 
        /// </summary>
        LayoutRightToLeft = 0x00400000,
        /// <summary>
        /// The window has generic left-aligned properties.
        /// This is the default.
        /// </summary>
        Left = 0x00000000,
        /// <summary>
        /// If the shell language is Hebrew, Arabic, or another language that supports reading order alignment, the vertical scroll bar (if present)
        /// is to the left of the client area. For other languages, the style is ignored.
        /// </summary>
        LeftScrollBar = 0x00004000,
        /// <summary>
        /// The window text is displayed using left-to-right reading-order properties.
        /// This is the default.
        /// </summary>
        LeftToRightReading = 0x00000000,
        /// <summary>
        /// The window is a MDI child window.
        /// </summary>
        MdiChild = 0x00000040,
        /// <summary>
        /// A top-level window created with this style does not become the foreground window when the user clicks it.
        /// The system does not bring this window to the foreground when the user minimizes or closes the foreground window.
        /// </summary>
        NoActivate = 0x08000000,
        /// <summary>
        /// The window does not pass its window layout to its child windows.
        /// </summary>
        NoInheritLayout = 0x00100000,
        /// <summary>
        /// The child window created with this style does not send the WM_PARENTNOTIFY message to its
        /// parent window when it is created or destroyed.
        /// </summary>
        NoParentNotify = 0x00000004,
        /// <summary>
        /// The window does not render to a redirection surface. This is for windows that do not have visible content or
        /// that use mechanisms other than surfaces to provide their visual.
        /// </summary>
        NoRedirectionBitmap = 0x00200000,
        /// <summary>
        /// The window is an overlapped window.
        /// </summary>
        OverlappedWindow = WindowEdge | ClientEdge,
        /// <summary>
        /// The window is palette window, which is a modeless dialog box that presents an array of commands. 
        /// </summary>
        PaletteWindow = WindowEdge | ToolWindow | TopMost,
        /// <summary>
        /// The window has generic "right-aligned" properties. This depends on the window class. This style has an effect only if the
        /// shell language is Hebrew, Arabic, or another language that supports reading-order alignment; otherwise, the style is ignored.
        /// </summary>
        Right = 0x00001000,
        /// <summary>
        /// The vertical scroll bar (if present) is to the right of the client area.
        /// This is the default.
        /// </summary>
        RightScrollBar = 0x00000000,
        /// <summary>
        /// If the shell language is Hebrew, Arabic, or another language that supports reading-order alignment, the window text is displayed using
        /// right-to-left reading-order properties. For other languages, the style is ignored.
        /// </summary>
        RightToLeftReading = 0x00002000,
        /// <summary>
        /// The window has a three-dimensional border style intended to be used for items that do not accept user input.
        /// </summary>
        StaticEdge = 0x00020000,
        /// <summary>
        /// The window is intended to be used as a floating toolbar. A tool window has a title bar that is shorter than a
        /// normal title bar, and the window title is drawn using a smaller font. A tool window does not appear in the taskbar
        /// or in the dialog that appears when the user presses ALT+TAB.
        /// </summary>
        ToolWindow = 0x00000080,
        /// <summary>
        /// The window should be placed above all non-topmost windows and should stay above them, even when the window is deactivated.
        /// </summary>
        TopMost = 0x00000008,
        /// <summary>
        /// The window should not be painted until siblings beneath the window (that were created by the same
        /// thread) have been painted. The window appears transparent because the bits of underlying sibling
        /// windows have already been painted.
        /// </summary>
        Transparent = 0x00000020,
        /// <summary>
        /// The window has a border with a raised edge.
        /// </summary>
        WindowEdge = 0x00000100,
    }

    /// <summary>
    /// Represents the possible class styles used by the <see cref="CreateParameters.WindowClassStyles"/> property.
    /// </summary>
    [Flags()]
    public enum WindowClassStyles : uint
    {
        /// <summary>
        /// Aligns the window's client area on a byte boundary (in the x direction).
        /// This style affects the width of the window and its horizontal placement on the display.
        /// </summary>
        ByteAlignClient = 0x1000,
        /// <summary>
        /// Aligns the window on a byte boundary (in the x direction). This style affects the width of the window and its horizontal
        /// placement on the display.
        /// </summary>
        ByteAlignWindow = 0x2000,
        /// <summary>
        /// Allocates one device context to be shared by all windows in the class. Because window classes are process specific,
        /// it is possible for multiple threads of an application to create a window of the same class. It is also possible for
        /// the threads to attempt to use the device context simultaneously. When this happens, the system allows only one thread
        /// to successfully finish its drawing operation. 
        /// </summary>
        ClassDC = 0x0040,
        /// <summary>
        /// Sends a double-click message to the window procedure when the user double-clicks the mouse while the cursor is
        /// within a window belonging to the class. 
        /// </summary>
        DblClks = 0x0008,
        /// <summary>
        /// Enables the drop shadow effect on a window. The effect is turned on and off through SPI_SETDROPSHADOW.
        /// Typically, this is enabled for small, short-lived windows such as menus to emphasize their Z-order relationship
        /// to other windows. Windows created from a class with this style must be top-level windows; they may not be child windows.
        /// </summary>
        DropShadow = 0x00020000,
        /// <summary>
        /// Indicates that the window class is an application global class.
        /// </summary>
        GlobalClass = 0x4000,
        /// <summary>
        /// Redraws the entire window if a movement or size adjustment changes the width of the client area.
        /// </summary>
        HRedraw = 0x0002,
        /// <summary>
        /// Disables Close on the window menu.
        /// </summary>
        NoClose = 0x0200,
        /// <summary>
        /// Allocates a unique device context for each window in the class. 
        /// </summary>
        OwnDC = 0x0020,
        /// <summary>
        /// Sets the clipping rectangle of the child window to that of the parent window so that the child
        /// can draw on the parent. A window with the CS_PARENTDC style bit receives a regular device context
        /// from the system's cache of device contexts. It does not give the child the parent's device context
        /// or device context settings. Specifying CS_PARENTDC enhances an application's performance. 
        /// </summary>
        ParentDC = 0x0080,
        /// <summary>
        /// Saves, as a bitmap, the portion of the screen image obscured by a window of this class.
        /// When the window is removed, the system uses the saved bitmap to restore the screen image,
        /// including other windows that were obscured. Therefore, the system does not send WM_PAINT
        /// messages to windows that were obscured if the memory used by the bitmap has not been discarded
        /// and if other screen actions have not invalidated the stored image. 
        /// </summary>
        SaveBits = 0x0800,
        /// <summary>
        /// Redraws the entire window if a movement or size adjustment changes the height of the client area.
        /// </summary>
        VRedraw = 0x0001,
    }
}
