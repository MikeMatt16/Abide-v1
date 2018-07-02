using System;
using System.IO;
using System.Text;

namespace Abide.HaloLibrary.IO
{
    /// <summary>
    /// Represents a virtual memory stream.
    /// This type of memory stream is used to read or write virtual-addressed data.
    /// </summary>
    public class VirtualStream : Stream, IDisposable
    {
        /// <summary>
        /// Gets and returns a new empty <see cref="VirtualStream"/> class instance.
        /// </summary>
        public static VirtualStream Empty
        {
            get { return new VirtualStream(new byte[0]); }
        }

        /// <summary>
        /// Gets or sets the number of bytes allocated for this stream.
        /// </summary>
        public int Capacity
        {
            get { return memoryStream.Capacity; }
            set { memoryStream.Capacity = value; }
        }
        /// <summary>
        /// Gets or sets the current position within the stream.
        /// </summary>
        public override long Position
        {
            get { return MemoryAddress + memoryStream.Position; }
            set { memoryStream.Position = value - MemoryAddress; }
        }
        /// <summary>
        /// Gets a value indicating whether the stream supports reading.
        /// </summary>
        public override bool CanRead => memoryStream.CanRead;
        /// <summary>
        /// Gets a value indicating whether the stream supports writing.
        /// </summary>
        public override bool CanWrite => memoryStream.CanWrite;
        /// <summary>
        /// Gets a value indicating whether the stream supports seeking.
        /// </summary>
        public override bool CanSeek => memoryStream.CanSeek;
        /// <summary>
        /// Gets the length of the stream in bytes.
        /// </summary>
        public override long Length => memoryStream.Length;

        /// <summary>
        /// Gets and returns the virtual memory address of the stream.
        /// </summary>
        public long MemoryAddress { get; }
        private MemoryStream memoryStream;

        /// <summary>
        /// Creates a new instance of the <see cref="BinaryReader"/> class based on the current stream and UTF-8 charecter encoding that leaves the current stream open.
        /// </summary>
        /// <returns>A new instance of the <see cref="BinaryReader"/> class whose underlying stream is this instance.</returns>
        public BinaryReader CreateReader()
        {
            return CreateReader(true);
        }
        /// <summary>
        /// Creates a new instance of the <see cref="BinaryReader"/> class based on the current stream and UTF-8 charecter encoding, and optionally leaves the current stream open.
        /// </summary>
        /// <param name="leaveOpen"><see langword="true"/> to leave the current stream open after the <see cref="BinaryReader"/> object is disposed; otherwise, <see langword="false"/>.</param>
        /// <returns>A new instance of the <see cref="BinaryReader"/> class whose underlying stream is this instance.</returns>
        public BinaryReader CreateReader(bool leaveOpen)
        {
            return new BinaryReader(this, Encoding.UTF8, leaveOpen);
        }
        /// <summary>
        /// Creates a new instance of the <see cref="BinaryWriter"/> class based on the current stream and UTF-8 charecter encoding that leaves the current stream open.
        /// </summary>
        /// <returns>A new instance of the <see cref="BinaryWriter"/> class whose underlying stream is this instance.</returns>
        public BinaryWriter CreateWriter()
        {
            return CreateWriter(true);
        }
        /// <summary>
        /// Creates a new instance of the <see cref="BinaryWriter"/> class based on the current stream and UTF-8 charecter encoding, and optionally leaves the current stream open.
        /// </summary>
        /// <param name="leaveOpen"><see langword="true"/> to leave the current stream open after the <see cref="BinaryWriter"/> object is disposed; otherwise, <see langword="false"/>.</param>
        /// <returns>A new instance of the <see cref="BinaryWriter"/> class whose underlying stream is this instance.</returns>
        public BinaryWriter CreateWriter(bool leaveOpen)
        {
            return new BinaryWriter(this, Encoding.UTF8, leaveOpen);
        }
        /// <summary>
        /// Initializes a new non-resizable instance of the <see cref="VirtualStream"/> class based on the supecified byte array at a zero-value memory address.
        /// </summary>
        /// <param name="buffer">The array of unsigned bytes from which to create the current stream. </param>
        public VirtualStream(byte[] buffer) : this(0, buffer)
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualStream"/> class using the supplied virtual memory address.
        /// </summary>
        /// <param name="memoryAddress">The virtual memory address. This is where the stream begins in memory.</param>
        public VirtualStream(long memoryAddress)
        {
            //Setup
            memoryStream = new MemoryStream();
            MemoryAddress = memoryAddress;
        }
        /// <summary>
        /// Initializes a new non-resizable instance of the <see cref="VirtualStream"/> class based on the supecified byte array using the supplied virtual memory address.
        /// </summary>
        /// <param name="memoryAddress">The virtual memory address. This is where the stream begins in memory.</param>
        /// <param name="buffer">The array of unsigned bytes from which to create the current stream. </param>
        public VirtualStream(long memoryAddress, byte[] buffer)
        {
            //Setup
            memoryStream = new MemoryStream(buffer);
            MemoryAddress = memoryAddress;
        }
        /// <summary>
        /// Overrides the <see cref="Stream.Flush"/> method so that no action is performed.
        /// </summary>
        public override void Flush()
        {
            //Flush memory stream
            memoryStream.Flush();
        }
        /// <summary>
        /// Swaps the internal buffer of the virtual stream.
        /// </summary>
        /// <param name="buffer">The array of bytes to replace the underlying memory stream buffer with.</param>
        public void SwapBuffer(byte[] buffer)
        {

        }
        /// <summary>
        /// Sets the position of the stream to the specified value.
        /// </summary>
        /// <param name="offset">A virtual zero-based byte offset.</param>
        /// <returns>The new position within the current stream.</returns>
        public long Seek(long offset)
        {
            //Seek
            return Seek(offset, SeekOrigin.Begin);
        }
        /// <summary>
        /// Sets the position of the stream to the specified value.
        /// </summary>
        /// <param name="offset">A byte offset relative to the <paramref name="origin"/> parameter.</param>
        /// <param name="origin">A value of <see cref="SeekOrigin"/> indicating the reference point used to obtain the new position.</param>
        /// <returns>The new position within the current stream.</returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            //Get offset as an unsigned 32-bit integer cause I don't feel like changing every signed integer to an unsigned integer.
            //This is very hacky but for my purposes it works for now
            uint offsetDword = (uint)offset;

            //Handle
            switch (origin)
            {
                case SeekOrigin.Current: return MemoryAddress + memoryStream.Seek(offset, SeekOrigin.Current);
                case SeekOrigin.Begin: return MemoryAddress + memoryStream.Seek((offsetDword - MemoryAddress), SeekOrigin.Begin);
                case SeekOrigin.End: return MemoryAddress + memoryStream.Seek(offset, SeekOrigin.End);
            }

            //Return
            return -1;
        }
        /// <summary>
        /// Sets the length of the current stream.
        /// </summary>
        /// <param name="value">The desired length of the current stream in bytes.</param>
        public override void SetLength(long value)
        {
            //Set length
            memoryStream.SetLength(value);
        }
        /// <summary>
        /// Writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
        /// </summary>
        /// <param name="buffer">The array of bytes. This method copies <paramref name="count"/> bytes from <paramref name="buffer"/> to the current stream.</param>
        /// <param name="offset">The zero-based offset in <paramref name="buffer"/> at which to begin copying bytes to the current stream.</param>
        /// <param name="count">The number of bytes to be written to the current stream.</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            //Write
            memoryStream.Write(buffer, offset, count);
        }
        /// <summary>
        /// Reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
        /// </summary>
        /// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between <paramref name="offset"/> and (<paramref name="offset"/> + <paramref name="count"/> - 1) replaced by the bytes read from the current source.</param>
        /// <param name="offset">The zero-based offset in <paramref name="buffer"/> at which to begin storing data read from the current stream.</param>
        /// <param name="count">The maximum number of bytes to be read from the current stream.</param>
        /// <returns>The total number of bytes read into the buffer.</returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            //Read
            return memoryStream.Read(buffer, offset, count);
        }
        /// <summary>
        /// Returns a string that represents the current stream.
        /// </summary>
        /// <returns>The virtual and physical address of the stream, as well as it's current capacity.</returns>
        public override string ToString()
        {
            return $"Position: {memoryStream.Position + MemoryAddress} ({memoryStream.Position}) Size: {memoryStream.Capacity}";
        }
        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="VirtualStream"/> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing"><see langword="true"/> to release both managed and unmanaged resources; <see langword="false"/> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            //Dispose
            base.Dispose(disposing);

            try { if (memoryStream.Length > 0) System.Diagnostics.Debugger.Break(); }
            catch (ObjectDisposedException) { }

            //Dispose managed resources
            if (disposing) memoryStream.Dispose();
        }
        /// <summary>
        /// Returns the array of unsigned bytes from which this stream was created..
        /// </summary>
        /// <returns>The byte array from which this stream was created, or the underlying array if a byte array was not provided to the <see cref="VirtualStream"/> constructor during construction of the current instance.</returns>
        public byte[] GetBuffer()
        {
            return memoryStream.GetBuffer();
        }
        /// <summary>
        /// Writes the stream contents to a byte array, regardless of the <see cref="Position"/> property.
        /// </summary>
        /// <returns>A new byte array.</returns>
        public byte[] ToArray()
        {
            return memoryStream.ToArray();
        }
    }
}
