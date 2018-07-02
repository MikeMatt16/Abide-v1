using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace Abide.H2Guerilla.H2Guerilla
{
    /// <summary>
    /// Represents a library class wrapper of an HMODULE.
    /// </summary>
    internal class Library : IDisposable
    {
        /// <summary>
        /// Gets and returns the handle to the library module.
        /// </summary>
        public IntPtr HModule
        {
            get { return hModule; }
        }

        private IntPtr hModule;

        /// <summary>
        /// Initializes a new instance of the <see cref="Library"/> class using the supplied file name.
        /// </summary>
        /// <param name="fileName">The path to the dynamic link library file.</param>
        public Library(string fileName)
        {
            //Load library...
            hModule = LoadLibrary(fileName);

            //Check
            if (hModule == IntPtr.Zero) throw new Win32Exception(GetLastError());
        }
        /// <summary>
        /// Returns a string representation of this instance.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return hModule.ToString();
        }
        /// <summary>
        /// Loads a string resource from the executable file associated with a specified module, copies the string into a buffer, and appends a terminating null character.
        /// </summary>
        /// <param name="uid">The identifier of the string.</param>
        /// <returns>A string.</returns>
        public string LoadString(int uid)
        {
            //Prepare
            StringBuilder builder = new StringBuilder(0x1000);

            //Load String
            if (LoadString(hModule, (uint)uid, builder, builder.Capacity) == 0)
                throw new Win32Exception(GetLastError());

            //Return
            return builder.ToString();
        }
        /// <summary>
        /// Releases all resources used by this instance.
        /// </summary>
        public void Dispose()
        {
            //Check
            if (hModule != IntPtr.Zero)
            {
                FreeLibrary(hModule);
                hModule = IntPtr.Zero;
            }
        }
        
        [DllImport("Kernel32.dll", CharSet = CharSet.Ansi)]
        private static extern IntPtr LoadLibrary(string lpFileName);
        [DllImport("Kernel32.dll")]
        private static extern bool FreeLibrary(IntPtr hModule);
        [DllImport("Kernel32.dll")]
        private static extern int GetLastError();
        [DllImport("User32.dll")]
        private static extern int LoadString(IntPtr hInstance, uint uid, StringBuilder lpBuffer, int nBufferMax);
    }
}
