using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

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
            if (CanRead)
            {
                int read = xbox.GetMemory(buffer, Position, offset, count);
                Position += read;
                return read;
            }

            return 0;
        }
        public override void Write(byte[] buffer, int offset, int count)
        {
            if (CanWrite)
            {
                xbox.SetMemory(buffer, position, offset, count);
                Position += count;
            }
        }
        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            if (CanRead)
            {
                var task = new Task<int>(() =>
                {
                    return xbox.GetMemory(buffer, position, offset, count);
                }, cancellationToken);

                task.Start();
                return task;
            }

            return null;
        }
        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            if (CanWrite)
            {
                var task = new Task(() =>
                {
                    xbox.SetMemory(buffer, position, offset, count);
                }, cancellationToken);
            }

            return null;
        }
    }
}
