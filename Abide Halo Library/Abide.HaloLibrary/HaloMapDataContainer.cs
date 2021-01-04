using Abide.HaloLibrary.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Abide.HaloLibrary
{
    /// <summary>
    /// Represents a basic Halo map data container.
    /// </summary>
    [Serializable]
    public sealed class HaloMapDataContainer : IEnumerable<byte>, IDisposable
    {
        [NonSerialized]
        private VirtualStream dataStream;
        private byte[] data = new byte[0];

        /// <summary>
        /// Gets and returns a boolean value that determines whether or not this instance is disposed.
        /// </summary>
        public bool IsDisposed { get; private set; } = false;
        /// <summary>
        /// Gets or sets the virtual address used by the <see cref="GetVirtualStream()"/> call.
        /// </summary>
        public long VirtualAddress { get; set; }
        /// <summary>
        /// Gets or sets a byte
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public byte this[int index]
        {
            get
            {
                if (index < 0 || index >= data.Length) throw new ArgumentOutOfRangeException(nameof(index));
                return data[index];
            }
            set
            {
                if (index < 0 || index >= data.Length) throw new ArgumentOutOfRangeException(nameof(index));
                data[index] = value;
            }
        }
        /// <summary>
        /// Gets and returns the length of the data in bytes.
        /// </summary>
        public int Length
        {
            get { return data.Length; }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="HaloMapDataContainer"/> class.
        /// </summary>
        public HaloMapDataContainer()
        {
            //Set dataStream
            dataStream = new VirtualStream(data);
        }
        /// <summary>
        /// Returns a copy of the data contained within this instance.
        /// </summary>
        /// <returns>An array of <see cref="byte"/> elements.</returns>
        public byte[] GetBuffer()
        {
            return (byte[])data.Clone();
        }
        /// <summary>
        /// Sets the data contained within this instance.
        /// </summary>
        /// <param name="buffer">An array of <see cref="byte"/> elements. This value can be <see langword="null"/>.</param>
        public void SetBuffer(byte[] buffer)
        {
            //Check
            if (buffer == null) buffer = new byte[0];

            //Set
            data = (byte[])buffer.Clone();

            //Dispose
            dataStream?.Dispose();
            dataStream = new VirtualStream(data);
        }
        /// <summary>
        /// Sets the data contained within this instance.
        /// </summary>
        /// <param name="memoryAddress">The memory address to set the buffer at.</param>
        /// <param name="buffer">An array of <see cref="byte"/> elements. This value can be <see langword="null"/>.</param>
        public void SetBuffer(long memoryAddress, byte[] buffer)
        {
            //Check
            if (buffer == null) buffer = new byte[0];

            //Set
            data = (byte[])buffer.Clone();

            //Dispose
            dataStream?.Dispose();
            dataStream = new VirtualStream(memoryAddress, data);
        }
        /// <summary>
        /// Returns a virtual stream using the data contained within this container.
        /// </summary>
        /// <returns>A new instance of a <see cref="VirtualStream"/> class.</returns>
        public VirtualStream GetVirtualStream()
        {
            return dataStream;
        }
        /// <summary>
        /// Clears the data within this instance.
        /// </summary>
        public void Clear()
        {
            SetBuffer(null);
        }
        /// <summary>
        /// Returns an enumerator that iterates through this instance.
        /// </summary>
        /// <returns>An enumerator.</returns>
        public IEnumerator<byte> GetEnumerator()
        {
            for (int i = 0; i < data.Length; i++)
                yield return data[i];
        }
        /// <summary>
        /// Returns a string representation of this instance.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"Data Length: {Length}";
        }
        /// <summary>
        /// Releases all resources used by this instance.
        /// </summary>
        public void Dispose()
        {
            if (IsDisposed) return;
            IsDisposed = true;

            dataStream.Dispose();
            dataStream = null;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
