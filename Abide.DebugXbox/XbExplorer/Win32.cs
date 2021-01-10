using System;
using System.Runtime.InteropServices;
using System.Text;

namespace XbExplorer
{
    internal static class Win32
    {
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern int SHFileOperation(SHFILEOPSTRUCT lpFileOp);
        [DllImport("Shlwapi.dll", CharSet = CharSet.Auto)]
        public static extern long StrFormatByteSize(long fileSize, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder buffer, int bufferSize);
        [DllImport("Shlwapi.dll", CharSet = CharSet.Auto)]
        public static extern long StrFormatKBSize(long fileSize, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder buffer, int bufferSize);
        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr ExtractIcon(IntPtr hwnd, string pszExeFileName, uint nIconIndex);
        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        public static extern int ExtractIconEx(string lpszFile, int nIconIndex, out IntPtr phiconLarge, out IntPtr phiconSmall, uint nIcons);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern uint PrivateExtractIcons(string szFileName, int nIconIndex, int cxIcon, int cyIcon, out IntPtr phicon, out IntPtr piconid, uint nIcons, uint flags);
        [DllImport("user32.dll")]
        public static extern bool DestroyIcon(IntPtr hIcon);
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct SHFILEOPSTRUCT
    {
        public IntPtr hwnd;
        public uint wFunc;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pFrom;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pTo;
        public ushort fFlags;
        public bool fAnyOperationsAborted;
        public IntPtr hNameMappings;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpszProgressTitle;
    }
}
