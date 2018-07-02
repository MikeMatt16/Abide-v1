using System;
using System.IO;

namespace Abide.HaloLibrary.IO
{
    /// <summary>
    /// Represents a fixed-length memory-mapped stream.
    /// This type of stream is used to translate memory-mapped data blocks into file-addressed blocks, or when a fixed length buffer is required.
    /// </summary>
    [Serializable]
    [Obsolete("Use Abide.HaloLibrary.IO.VirtualStream instead.", false)]
    public class FixedMemoryMappedStream : Stream
    {
        /// <summary>
        /// Represents an empty <see cref="FixedMemoryMappedStream"/>.
        /// </summary>
        public static FixedMemoryMappedStream Empty
        {
            get { return new FixedMemoryMappedStream(new byte[0]); }
        }

        private readonly byte[] buffer;
        private long position = 0;

        /// <summary>
        /// Initializes a new <see cref="FixedMemoryMappedStream"/> with the supplied buffer and memory address.
        /// </summary>
        /// <param name="buffer">The buffer to use.</param>
        /// <param name="memoryAddress">The address at which this data begins.</param>
        /// <exception cref="ArgumentNullException"><paramref name="buffer"/> is null.</exception>
        public FixedMemoryMappedStream(byte[] buffer, long memoryAddress)
        {

            //Setup
            this.buffer = buffer ?? throw new ArgumentNullException("buffer");
            MemoryAddress = memoryAddress;
        }
        /// <summary>
        /// Initializes a new <see cref="FixedMemoryMappedStream"/> with the supplied buffer.
        /// </summary>
        /// <param name="buffer">The buffer to use.</param>
        /// <exception cref="ArgumentNullException"><paramref name="buffer"/> is null.</exception>
        public FixedMemoryMappedStream(byte[] buffer) : this(buffer, 0)
        { }

        /// <summary>
        /// Gets the memory-address of the stream.
        /// </summary>
        public long MemoryAddress { get; }
        /// <summary>
        /// Determines if this stream supports reading.
        /// <see cref="CanRead"/> will always return true.
        /// </summary>
        public override bool CanRead
        {
            get { return true; }
        }
        /// <summary>
        /// Determines if this stream supports seeking.
        /// <see cref="CanSeek"/> will always return true.
        /// </summary>
        public override bool CanSeek
        {
            get { return true; }
        }
        /// <summary>
        /// Determines if this stream supports writing.
        /// <see cref="CanWrite"/> will always return true.
        /// </summary>
        public override bool CanWrite
        {
            get { return true; }
        }
        /// <summary>
        /// Gets and returns the length of the buffer.
        /// </summary>
        public override long Length
        {
            get { return buffer.LongLength; }
        }
        /// <summary>
        /// Gets and returns the length of the buffer as a 32-bit signed integer.
        /// </summary>
        public int IntLength
        {
            get { return buffer.Length; }
        }
        /// <summary>
        /// Gets or sets the position within the buffer.
        /// </summary>
        public override long Position
        {
            get { return position + MemoryAddress; }
            set
            {
                long translated = value - MemoryAddress;
                if (translated > buffer.LongLength || translated < 0)
                    throw new ArgumentOutOfRangeException("Value outside of range.", "value");
                else position = translated;
            }
        }
        /// <summary>
        /// Flushes any data to the underlying buffer.
        /// This method does nothing.
        /// </summary>
        public override void Flush() { }
        /// <summary>
        /// Reads a block of bytes from the stream and writes the data in a given buffer.
        /// </summary>
        /// <param name="array">When this method returns, contains the specified byte array with the values between <paramref name="offset"/> and (<paramref name="offset"/> + <paramref name="count"/> - 1) replaced by the bytes read from the current source.</param>
        /// <param name="offset">The byte offset in <paramref name="array"/> at which the read bytes will be placed. </param>
        /// <param name="count">The maximum number of bytes to read. </param>
        /// <returns>The total number of bytes read into the buffer. This might be less than the number of bytes requested if that number of bytes are not currently available, or zero if the end of the stream is reached.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="array"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="offset"/> or <paramref name="count"/> is negative, or the count exceeds the length of <paramref name="array"/>.</exception>
        /// <exception cref="IOException">An I/O eror occured.</exception>
        public override int Read(byte[] array, int offset, int count)
        {
            //Prepare
            int readCount = 0;

            //Check
            if (array == null) throw new ArgumentNullException("array");
            if (count > array.Length || count <= 0) throw new ArgumentOutOfRangeException("count");
            if (offset < 0) throw new ArgumentOutOfRangeException("offset");
            if (position + count > buffer.Length) throw new ArgumentOutOfRangeException("count");

            //Copy
            try { Array.Copy(buffer, position, array, offset, count); readCount = count; }
            catch (Exception ex) { throw new IOException(ex.Message, ex); }

            //Advance
            position += readCount;

            //Return
            return readCount;
        }
        /// <summary>
        /// Sets the current position of this stream to the given value.
        /// If origin specifies <see cref="SeekOrigin.Begin"/> or <see cref="SeekOrigin.End"/> then offset is expected to be memory-addressed.
        /// </summary>
        /// <param name="offset">The point relative to origin from which to begin seeking.</param>
        /// <param name="origin">Specifies the beginning, the end, or the current position as a reference point for offset, using a value of type SeekOrigin. </param>
        /// <returns>The new position in the stream.</returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            long newPosition = position;

            //Handle...
            switch (origin)
            {
                case SeekOrigin.Begin: newPosition = (int)(offset - MemoryAddress); break;
                case SeekOrigin.Current: newPosition += (int)offset; break;
                case SeekOrigin.End: newPosition -= (int)offset; break;
            }

            //Set
            position = newPosition;

            //Return
            return newPosition;
        }
        /// <summary>
        /// Sets the current position of this stream to the given memory-addressed offset value.
        /// </summary>
        /// <param name="offset">The offset to seek to from the beginning.</param>
        /// <returns>The new position in the stream.</returns>
        public long Seek(long offset)
        {
            return Seek(offset, SeekOrigin.Begin);
        }
        /// <summary>
        /// Sets the length of this stream to the given value.
        /// This method will always throw a <see cref="NotSupportedException"/> as this stream does not support changing lengths.
        /// </summary>
        /// <param name="value">The new length of the stream.</param>
        /// <exception cref="NotSupportedException">Stream does not support changing lengths.</exception>
        public override void SetLength(long value)
        {
            throw new NotSupportedException("Stream does not support changing lengths.");
        }
        /// <summary>
        /// Writes a block of bytes to the file stream.
        /// </summary>
        /// <param name="array">The buffer containing data to write to the stream.</param>
        /// <param name="offset">The zero-based byte offset in <paramref name="array"/> from which to begin copying bytes to the stream. </param>
        /// <param name="count">The maximum number of bytes to write.</param>
        /// <exception cref="ArgumentNullException"><paramref name="array"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="offset"/> or <paramref name="count"/> is negative, or the count exceeds the length of <paramref name="array"/>.</exception>
        /// <exception cref="IOException">An I/O eror occured.</exception>
        public override void Write(byte[] array, int offset, int count)
        {
            //Check
            if (array == null) throw new ArgumentNullException("array");
            if (count > array.Length || count <= 0) throw new ArgumentOutOfRangeException("count");
            if (offset < 0) throw new ArgumentOutOfRangeException("offset");

            //Copy
            try { Array.Copy(array, offset, buffer, position, count); }
            catch (Exception ex) { throw new IOException(ex.Message, ex); }

            //Advance
            position += count;
        }
        /// <summary>
        /// Returns a byte array containing the stream's buffered data.
        /// </summary>
        /// <returns>A byte array containing the stream's buffered data.</returns>
        public byte[] GetBuffer()
        {
            return (byte[])buffer.Clone();
        }
        /// <summary>
        /// Returns a string representation of the current stream.
        /// </summary>
        /// <returns>A string containing the length and the position of the stream.</returns>
        public override string ToString()
        {
            return string.Format("Length: {0} Position: {1}", Length, Position);
        }
    }
}