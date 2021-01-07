using System;
using System.IO;

namespace Abide.DebugXbox
{
    internal sealed class XboxMemoryStream : Stream
    {
        private readonly Xbox xbox;
        private uint position = 0x10000;

        public override long Position
        {
            get => position;
            set => position = (uint)value;
        }
        public override bool CanRead => xbox.Connected;
        public override bool CanWrite => xbox.Connected;
        public override bool CanSeek => xbox.Connected;
        public override bool CanTimeout => true;
        public sealed override long Length => throw new InvalidOperationException();

        public XboxMemoryStream(Xbox xbox)
        {
            this.xbox = xbox;
        }
        public override void SetLength(long value)
        {
            throw new InvalidOperationException();
        }
        public override void Flush()
        {
        }
        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    Position = offset;
                    return position;
                    
                case SeekOrigin.Current:
                    Position += offset;
                    return position;

                default: throw new ArgumentException(nameof(origin));
            }
        }
        public override int Read(byte[] buffer, int offset, int count)
        {
            return xbox.GetMemory(buffer, Position, offset, count);
        }
        public override void Write(byte[] buffer, int offset, int count)
        {
            byte[] data = new byte[count];
            Array.Copy(buffer, offset, data, 0, count);
            xbox.SetMemory(position, data, count);
            Position += count;
        }
    }
}
