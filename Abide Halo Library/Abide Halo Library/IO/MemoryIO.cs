using System;
using System.IO;

namespace AbideHaloLibrary.IO
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
        public BinaryReader In
        {
            get { return reader; }
        }
        /// <summary>
        /// The writer of this IO instance.
        /// </summary>
        public BinaryWriter Out
        {
            get { return writer; }
        }
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

        private readonly byte[] buffer;
        private readonly MemoryStream ms;
        private readonly BinaryWriter writer;
        private readonly BinaryReader reader;
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
            writer = new BinaryWriter(ms);
            reader = new BinaryReader(ms);
        }
        /// <summary>
        /// Closes the IO instance, stopping the ability to write and read.
        /// </summary>
        public void Close()
        {
            //Check
            if (!Open)
                return;

            //Close
            ms.Close();
            writer.Close();
            reader.Close();
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
            writer.Dispose();
            reader.Dispose();
            isDisposed = true;
        }
    }
}
