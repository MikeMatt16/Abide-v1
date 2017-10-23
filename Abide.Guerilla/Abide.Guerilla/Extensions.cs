using Abide.Guerilla.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.IO
{
    /// <summary>
    /// Me want more bananas.
    /// </summary>
    public static class GuerillaExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="blockType"></param>
        public static void ReadBlock(this BinaryReader reader, Type blockType)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="block"></param>
        public static void ReadBlock(this BinaryReader reader, BlockAttribute block)
        {
        }
    }
}
