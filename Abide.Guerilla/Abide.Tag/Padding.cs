using System.IO;

namespace Abide.Tag
{
    public static class Padding
    {
        public static long Align(this Stream stream, int alignment, byte paddingByte = 0xcd)
        {
            if (stream.Position % alignment == 0)
            {
                return stream.Position;
            }

            byte[] padding = CreatePadding(stream.Position, alignment, paddingByte);
            if (stream.CanSeek)
            {
                return stream.Seek(padding.Length, SeekOrigin.Current);
            }
            else if (stream.CanWrite)
            {
                stream.Write(padding, 0, padding.Length);
            }

            return stream.Position;
        }
        private static byte[] CreatePadding(long position, int alignment, byte paddingByte)
        {
            long remainder = position % alignment;
            byte[] padding = new byte[alignment - remainder];

            for (int i = 0; i < padding.Length; i++)
            {
                padding[i] = paddingByte;
            }

            return padding;
        }
    }
}
