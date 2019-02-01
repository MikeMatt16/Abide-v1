using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Abide.HaloLibrary.Halo2VistaMap
{
    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct Header
    {
        /// <summary>
        /// Gets and returns the length of a <see cref="Header"/> structure in bytes.
        /// This value is constant.
        /// </summary>
        public const int Length = 2048;
        /// <summary>
        /// Gets and returns the required build string for the header.
        /// This value is constant.
        /// </summary>
        public const string BuildString = "11081.07.04.30.0934.main";

    }
}
