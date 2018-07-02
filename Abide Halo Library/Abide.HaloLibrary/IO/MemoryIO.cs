using System;
using System.IO;

namespace Abide.HaloLibrary.IO
{
    /// <summary>
    /// Represents a Memory Input Output handler.
    /// </summary>
    internal sealed class MemoryIO : MarshalByRefObject, IDisposable
    {
        /// <summary>
        /// The base stream of the IO instance.
        /// </summary>
        public Stream BaseStream
        {
            get { return ms; }
        }
        /// <summary>
        /// The readeor of this IO instance.
        /// </summary>
        public BinaryReader In { get; }
        /// <summary>
        /// The writer of this IO instance.
        /// </summary>
        public BinaryWriter Out { get; }
        /// <summary>
        /// The position of the underlying stream.
        /// </summary>
        public long Position
        {
            get { return Open ? ms.Position : -1; }
            set { if (Open) ms.Position = value; }
        }
        /// <summary>
        /// Gets if the current instance is open for writing and reading.
        /// </summary>
        public bool Open
        {
            get { return (ms != null && ms.CanRead && ms.CanWrite && !isDisposed); }
        }

        private byte[] buffer;
        private MemoryStream ms;
        private bool isDisposed;

        /// <summary>
        /// Initializes the instance using the supplied buffer.
        /// </summary>
        /// <param name="buffer">The buffer to use for the IO instance.</param>
        public MemoryIO(byte[] buffer)
        {
            //Setup
            isDisposed = false;
            this.buffer = buffer;
            ms = new MemoryStream(this.buffer);
            Out = new BinaryWriter(ms);
            In = new BinaryReader(ms);
        }
        /// <summary>
        /// Closes the IO instance, stopping the ability to write and read.
        /// </summary>
        public void Close()
        {
            //Check
            if (!Open) return;

            //Close
            ms.Close();
            Out.Close();
            In.Close();
        }
        /// <summary>
        /// Releases any resources used by this instance.
        /// </summary>
        public void Dispose()
        {
            //Check
            if (isDisposed)
                return;

            //Dispose
            ms.Dispose();
            Out.Dispose();
            In.Dispose();
            isDisposed = true;

            //Null
            ms = null;
            buffer = null;
        }
    }
}
