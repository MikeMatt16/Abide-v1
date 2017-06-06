using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Abide
{
    internal static class FileHelper
    {
        private const int MAX_CHAR = byte.MaxValue;

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern int GetShortPathName(
            [MarshalAs(UnmanagedType.LPTStr)] string path,
            [MarshalAs(UnmanagedType.LPTStr)] StringBuilder shortPath,
            int shortPathLength);

        [DllImport("Shlwapi.dll", CharSet = CharSet.Auto)]
        private static extern bool PathCompactPathEx(
            [MarshalAs(UnmanagedType.LPTStr)] StringBuilder pszOut,
            [MarshalAs(UnmanagedType.LPTStr)] string pszSrc,
            uint cchMax,
            uint dwFlags);

        /// <summary>
        /// Retrieves the short path string form of the specified path string.
        /// </summary>
        /// <param name="path">The file name to shorten.</param>
        /// <returns>The short form the the path that <paramref name="path"/> specifies.</returns>
        public static string GetShortPath(this string path)
        {
            if (!File.Exists(path)) return path;
            StringBuilder builder = new StringBuilder(MAX_CHAR);
            GetShortPathName(path, builder, MAX_CHAR);
            return builder.ToString();
        }

        /// <summary>
        /// Truncates a path to fit within a certain number of characters by replacing path components with ellipses. 
        /// </summary>
        /// <param name="path">The path to be altered.</param>
        /// <param name="charCount">The maximum number of characters to be contained in the new string.</param>
        /// <returns>A string that has been altered.</returns>
        public static string GetCompactPath(this string path, int charCount)
        {
            StringBuilder builder = new StringBuilder(MAX_CHAR);
            if (PathCompactPathEx(builder, path, (uint)charCount, 0)) return builder.ToString();
            else return path;
        }
    }
}
