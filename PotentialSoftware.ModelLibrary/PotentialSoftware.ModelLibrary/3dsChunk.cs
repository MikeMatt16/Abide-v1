using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotentialSoftware.ModelLibrary
{
    /// <summary>
    /// Represents a 3ds file chunk.
    /// </summary>
    public abstract class _3dsChunk
    {
        /// <summary>
        /// Gets and returns the 2-byte identifier number of the chunk.
        /// </summary>
        public abstract ushort Identifier { get; }
        /// <summary>
        /// Gets and returns the length of the chunk.
        /// </summary>
        /// <returns>An unsigned 16-bit value containing the length of the chunk.</returns>
        public abstract ushort GetLength();
        /// <summary>
        /// Compiles the chunk and it's children 
        /// </summary>
        /// <returns></returns>
        public abstract byte[] Compile();
    }
}
