using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abide.Guerilla
{
    /// <summary>
    /// 
    /// </summary>
    public static class Guerilla
    {
        /// <summary>
        /// The image load address used for translating virtual addresses into physical addresses.
        /// </summary>
        public const int BaseAddress = 0x400000;
        /// <summary>
        /// The virtual addrss of the tag layout table.
        /// </summary>
        public const int TagLayoutTableAddress = 0x00901B90;
        /// <summary>
        /// The number of tag layouts in the layout table.
        /// </summary>
        public const int NumberOfTagLayouts = 120;
    }
}
