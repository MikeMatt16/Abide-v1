using System.IO;

namespace Abide.Tag
{
    /// <summary>
    /// Provides a set of <see langword="static"/> (<see langword="Shared"/> in Visual Basic) methods for aligning streams to particular offsets.
    /// </summary>
    public static class Padding
    {
        /// <summary>
        /// Aligns the current stream to a specified position-alignment.
        /// </summary>
        /// <param name="stream">The stream to align.</param>
        /// <param name="alignment">The alignment.</param>
        /// <param name="paddingByte">The byte to fill the padded space with.</param>
        /// <returns>The newly-aligned position of the stream.</returns>
        public static long Align(this Stream stream, int alignment, byte paddingByte = 0xcd)
        {
            //Get padding
            if (stream.Position % alignment == 0) return stream.Position;

            byte[] padding = Padding_Create(stream.Position, alignment, paddingByte);
            stream.Write(padding, 0, padding.Length);
            return stream.Position;
        }

        private static byte[] Padding_Create(long position, int alignment, byte paddingByte)
        {
            //Check
            if (position % alignment == 0) return new byte[0];
            long remainder = position % alignment;
            byte[] padding = new byte[alignment - remainder];
            for (int i = 0; i < padding.Length; i++)
                padding[i] = paddingByte;
            return padding;
        }
    }
}
