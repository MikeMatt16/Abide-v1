using Abide.HaloLibrary.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Abide.HaloLibrary.Halo2Map
{
    /// <summary>
    /// Contains an enumeration of the Halo 2 raw data types.
    /// </summary>
    public enum RawSection
    {
        /// <summary>
        /// Represents Halo 2 sound raw data.
        /// </summary>
        Sound,
        /// <summary>
        /// Represents Halo 2 geometry raw data.
        /// </summary>
        Model,
        /// <summary>
        /// Represents Halo 2 BSP raw data.
        /// </summary>
        BSP,
        /// <summary>
        /// Represents Halo 2 weather raw data.
        /// </summary>
        Weather,
        /// <summary>
        /// Represents Halo 2 decorator set raw data.
        /// </summary>
        DecoratorSet,
        /// <summary>
        /// Represents Halo 2 particle model raw data.
        /// </summary>
        ParticleModel,
        /// <summary>
        /// Represents Halo 2 lip sync raw data.
        /// </summary>
        LipSync,
        /// <summary>
        /// Represents Halo 2 animation raw data.
        /// </summary>
        Animation,
        /// <summary>
        /// Represents Halo 2 bitmap raw data.
        /// </summary>
        Bitmap,
    }

    /// <summary>
    /// Represents a Halo 2 raw data container.
    /// </summary>
    [Serializable]
    public sealed class RawContainer : IEnumerable<RawList>, IDisposable, ICloneable
    {
        private readonly List<RawList> lists;
        private readonly Dictionary<RawSection, int> sectionLookup;

        /// <summary>
        /// Gets and returns the raw list for a supplied <see cref="RawSection"/>.
        /// </summary>
        /// <param name="section">The <see cref="RawSection"/> to get a list of raws for.</param>
        /// <returns>A <see cref="RawList"/> containing raws of the given <see cref="RawSection"/>.</returns>
        public RawList this[RawSection section]
        {
            get
            {
                RawList list = null;
                if (!sectionLookup.ContainsKey(section))
                { list = new RawList(); lists.Add(list); sectionLookup.Add(section, lists.IndexOf(list)); }
                else list = lists[sectionLookup[section]];
                return list;
            }
        }
        
        /// <summary>
        /// Initializes a new <see cref="RawContainer"/>.
        /// </summary>
        public RawContainer()
        {
            //Setup
            lists = new List<RawList>();
            sectionLookup = new Dictionary<RawSection, int>();

            //Load
            foreach (RawSection section in Enum.GetValues(typeof(RawSection)))
            {
                RawList list = new RawList();
                lists.Add(list);
                sectionLookup.Add(section, lists.IndexOf(list));
            }
        }
        /// <summary>
        /// Copies all raws from another raw container.
        /// </summary>
        /// <param name="other">The other raw container.</param>
        public void CopyFrom(RawContainer other)
        {
            //Copy from for each list
            for (int i = 0; i < lists.Count; i++)
                lists[i].CopyFrom(other.lists[i]);
        }
        /// <summary>
        /// Clears all raws from the container.
        /// </summary>
        public void Clear()
        {
            //Clear
            lists.ForEach(l => l.Clear());
        }
        /// <summary>
        /// Creates a shallow copy of the container.
        /// </summary>
        /// <returns>A copy of this container.</returns>
        public object Clone()
        {
            //Prepare
            RawContainer container = new RawContainer();
            for (int i = 0; i < container.lists.Count; i++)
                container.lists[i] = (RawList)lists[i].Clone();

            //Return
            return container;
        }
        /// <summary>
        /// Releases all resources used by this container.
        /// </summary>
        public void Dispose()
        {
            //Cleanup
            foreach (RawList list in lists)
                list.Dispose();
            lists.Clear();
        }
        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="RawContainer"/>. 
        /// </summary>
        /// <returns>An enumerator.</returns>
        public IEnumerator<RawList> GetEnumerator()
        {
            return lists.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return lists.GetEnumerator();
        }
    }

    /// <summary>
    /// Represents a Halo 2 raw data list.
    /// </summary>
    [Serializable]
    public sealed class RawList : IEnumerable<RawStream>, IDisposable, ICloneable
    {
        private readonly List<RawStream> raws;
        private readonly Dictionary<int, int> offsetLookup;

        /// <summary>
        /// Gets and returns the raw stream at the given offset.
        /// </summary>
        /// <param name="offset">The raw at a given offset to retrieve.</param>
        /// <returns>A <see cref="RawStream"/> instance whose <see cref="RawStream.RawOffset"/> value is the supplied <paramref name="offset"/>, or null if a raw could not be found.</returns>
        public RawStream this[int offset]
        {
            get
            {
                if (offsetLookup.ContainsKey(offset)) return raws[offsetLookup[offset]];
                else return null;
            }
        }

        /// <summary>
        /// Initializes a new <see cref="RawList"/> instance copying the supplied <see cref="RawStream"/> array into the list.
        /// </summary>
        /// <param name="raws">The raw streams to copy into the list.</param>
        /// <exception cref="ArgumentNullException"><paramref name="raws"/> is null.</exception>
        public RawList(RawStream[] raws)
        {
            //Check
            if (raws == null) throw new ArgumentNullException("raws");

            //Setup
            this.raws = new List<RawStream>(raws);
            offsetLookup = new Dictionary<int, int>();

            //Add
            for (int i = 0; i < raws.Length; i++)
                offsetLookup.Add(raws[i].RawOffset, i);
        }
        /// <summary>
        /// Initializes a new <see cref="RawList"/> instance.
        /// </summary>
        public RawList()
        {
            //Setup
            raws = new List<RawStream>();
            offsetLookup = new Dictionary<int, int>();
        }
        /// <summary>
        /// Checks if a raw at a given offset exists within this container.
        /// </summary>
        /// <param name="offset">The offset of the raw data.</param>
        /// <returns>True if a raw at the given offset is found, otherwise false.</returns>
        public bool ContainsRawOffset(int offset)
        {
            return offsetLookup.ContainsKey(offset);
        }
        /// <summary>
        /// Attempts to add a new raw stream to the raw container.
        /// </summary>
        /// <param name="raw">The raw to add.</param>
        /// <returns>True if the raw to add is not already within the container. Otherwise false.</returns>
        public bool Add(RawStream raw)
        {
            //Check
            if (raw == null) throw new ArgumentNullException(nameof(raw));

            //Check
            if (offsetLookup.ContainsKey(raw.RawOffset))
                return false;

            //Add
            raws.Add(raw);
            offsetLookup.Add(raw.RawOffset, raws.IndexOf(raw));
            return true;
        }
        /// <summary>
        /// Removes and disposes a raw data stream whose offset matches the supplied offset.
        /// </summary>
        /// <param name="offset">The offset of the raw data.</param>
        /// <returns>True if a raw data was found and removed, otherwise returns false.</returns>
        public bool Delete(int offset)
        {
            //Remove
            if (offsetLookup.ContainsKey(offset))
            {
                //Dispose
                raws[offsetLookup[offset]].Dispose();

                //Remove
                raws.RemoveAt(offsetLookup[offset]);

                //Rebuild lookup table
                offsetLookup.Clear();
                for (int i = 0; i < raws.Count; i++) offsetLookup.Add(raws[i].RawOffset, i);
                return true;
            }
            else return false;
        }
        /// <summary>
        /// Swaps the raw data buffer of a raw data stream with the supplied buffer.
        /// </summary>
        /// <param name="offset">The offset of the raw data.</param>
        /// <param name="buffer">The raw data buffer.</param>
        public void SwapBuffer(int offset, byte[] buffer)
        {
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
            if (!offsetLookup.ContainsKey(offset)) throw new ArgumentException("Raw container does not contain a raw stream at the given offset.", nameof(offset));

            //Get Index
            int rawIndex = offsetLookup[offset];

            //Get length and offset references
            long[] offsetAddresses = raws[rawIndex].OffsetAddresses.ToArray();
            long[] lengthAddresses = raws[rawIndex].LengthAddresses.ToArray();

            //Dispose
            raws[rawIndex].Dispose();

            //Initialize
            raws[rawIndex] = new RawStream(buffer, offset);
            raws[rawIndex].OffsetAddresses.AddRange(offsetAddresses);
            raws[rawIndex].LengthAddresses.AddRange(lengthAddresses);
        }
        /// <summary>
        /// Releases all resources used by this list.
        /// </summary>
        public void Dispose()
        {
            //Dispose
            foreach (var raw in raws)
                raw.Dispose();
            
            //Clear
            offsetLookup.Clear();
            raws.Clear();
        }
        /// <summary>
        /// Gets a string representation of this list.
        /// </summary>
        /// <returns>A string representation of this list.</returns>
        public override string ToString()
        {
            return string.Format("Count = {0}", raws.Count);
        }
        /// <summary>
        /// Creates a shallow copy of the list.
        /// </summary>
        /// <returns>A copy of this list.</returns>
        public object Clone()
        {
            //Return
            return new RawList(raws.ToArray());
        }
        /// <summary>
        /// Copies the contents of another raw list into this raw list.
        /// </summary>
        /// <param name="other">The other raw list to copy the elements from.</param>
        public void CopyFrom(RawList other)
        {
            //Clear
            raws.Clear();
            offsetLookup.Clear();

            //Add
            foreach (var item in other.raws)
            {
                RawStream newRaw = (RawStream)item.Clone();
                offsetLookup.Add(item.RawOffset, raws.Count);
                raws.Add(newRaw);
            }
        }
        /// <summary>
        /// Clears all raw buffers and empties the raw container.
        /// </summary>
        public void Clear()
        {
            //Dispose
            raws.ForEach(r => r.Dispose());

            //Clear
            raws.Clear();
            offsetLookup.Clear();
        }
        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="RawList"/>. 
        /// </summary>
        /// <returns>An enumerator.</returns>
        public IEnumerator<RawStream> GetEnumerator()
        {
            return raws.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return raws.GetEnumerator();
        }
    }

    /// <summary>
    /// Represents a Halo 2 raw data stream.
    /// </summary>
    [Serializable]
    public sealed class RawStream : MemoryStream, ICloneable
    {
        /// <summary>
        /// Gets and returns the offset of the raw data.
        /// </summary>
        public int RawOffset { get; }
        /// <summary>
        /// Gets and returns a list of addresses where this raw data's offset is referenced.
        /// </summary>
        public List<long> OffsetAddresses { get; } = new List<long>();
        /// <summary>
        /// Gets and returns a list of addresses where this raw data's length is referenced.
        /// </summary>
        public List<long> LengthAddresses { get; } = new List<long>();

        /// <summary>
        /// Initializes a new <see cref="RawStream"/> using the supplied raw data buffer and raw data offset.
        /// </summary>
        /// <param name="rawBuffer">The byte array that contains the raw data.</param>
        /// <param name="rawOffset">The zero-based raw data offst.</param>
        public RawStream(byte[] rawBuffer, int rawOffset) :
            base(rawBuffer)
        {
            RawOffset = rawOffset;
        }
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
        /// Creates a copy of the current.
        /// </summary>
        /// <returns>A copy of the current.</returns>
        public object Clone()
        {
            //Create
            RawStream stream = new RawStream(ToArray(), RawOffset);
            stream.OffsetAddresses.AddRange(OffsetAddresses.ToArray());
            stream.LengthAddresses.AddRange(LengthAddresses.ToArray());

            //Return
            return stream;
        }
    }
}
