using Abide.Compression;
using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using Abide.HaloLibrary.IO;
using System;
using System.IO;

namespace Abide.Halo2
{
    /// <summary>
    /// Represents an Abide Halo 2 Tag file.
    /// </summary>
    public sealed class AbideTagFile
    {
        /// <summary>
        /// Gets or sets the Halo Tag type string.
        /// </summary>
        public string Tag
        {
            get { return header.TagType; }
            set { header.TagType = value; }
        }
        /// <summary>
        /// Gets or sets the memory address of the Halo Tag.
        /// </summary>
        public long MemoryAddress
        {
            get { return header.MemoryAddress; }
            set { header.MemoryAddress = (uint)value; }
        }
        /// <summary>
        /// Gets and returns the Halo Tag data stream.
        /// </summary>
        public FixedMemoryMappedStream TagData
        {
            get { return data; }
        }

        private Header header;
        private FixedMemoryMappedStream data;

        /// <summary>
        /// Initializes a new <see cref="AbideTagFile"/>.
        /// </summary>
        public AbideTagFile()
        {
            //Setup
            header = new Header() { ATagTag = "ATag" };
            data = new FixedMemoryMappedStream(new byte[0]);
        }
        /// <summary>
        /// Loads an Abide Halo 2 Tag file from the specified Halo 2 Index Entry.
        /// </summary>
        /// <param name="entry">The Halo 2 Index entry to load the Tag from.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entry"/> is null.</exception>
        public void LoadEntry(IndexEntry entry)
        {
            //Check
            if (entry == null) throw new ArgumentNullException(nameof(entry));

            //Setup
            header.TagType = entry.Root;
            header.MemoryAddress = entry.Offset;

            //Read
            byte[] buffer = new byte[entry.Size];
            entry.TagData.Seek(entry.Offset, SeekOrigin.Begin);
            entry.TagData.Read(buffer, 0, buffer.Length);

            //Create
            if (data != null) data.Dispose();
            data = new FixedMemoryMappedStream(buffer, entry.Offset);
        }
        /// <summary>
        /// Loads an Abide Halo 2 Tag file from the specified file name.
        /// </summary>
        /// <param name="filename">The filename of the Abide Halo 2 Tag file.</param>
        /// <exception cref="ArgumentNullException"><paramref name="filename"/> is null.</exception>
        /// <exception cref="FileNotFoundException"><paramref name="filename"/> is not a valid filename.</exception>
        /// <exception cref="AbideTagFileException">File failed to open.</exception>
        public void Load(string filename)
        {
            //Check
            if (filename == null) throw new ArgumentNullException(nameof(filename));
            if (!File.Exists(filename)) throw new FileNotFoundException("Abide Tag File does not exist.", filename);

            //Open
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                try { Load(fs); } catch(Exception ex) { throw new AbideTagFileException("Abide Tag file failed to open.", ex); }
        }
        /// <summary>
        /// Loads an Abide Halo 2 Tag file from the specified stream.
        /// </summary>
        /// <param name="inStream">The stream that contains the Abide Halo 2 Tag file.</param>
        /// <exception cref="ArgumentNullException"><paramref name="inStream"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="inStream"/> does not support seeking or reading.</exception>
        /// <exception cref="AbideTagFileException">File failed to open.</exception>
        public void Load(Stream inStream)
        {
            //Check
            if (inStream == null) throw new ArgumentNullException(nameof(inStream));
            if (!inStream.CanSeek) throw new ArgumentException("Stream does not support seeking.", nameof(inStream));
            if (!inStream.CanRead) throw new ArgumentException("Stream does not support reading.", nameof(inStream));

            //Check
            if (inStream.Length >= 16)
                using(BinaryReader reader = new BinaryReader(inStream))
                {
                    //Read
                    header = reader.Read<Header>();

                    //Load Data
                    byte[] buffer = new byte[header.DataLength];
                    inStream.Read(buffer, 0, buffer.Length);

                    //Prepare
                    if (data != null) data.Dispose();
                    data = new FixedMemoryMappedStream(buffer, header.MemoryAddress);
                }
            else throw new AbideTagFileException("Invalid Abide Tag file.");
        }
        /// <summary>
        /// Saves the Abide Halo 2 Tag file to the specified file name.
        /// </summary>
        /// <param name="filename">The filename to save the Abide Halo 2 Tag file.</param>
        public void Save(string filename)
        {
            //Check
            if (filename == null) throw new ArgumentNullException(nameof(filename));

            //Create
            using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.Read))
                try { Save(fs); } catch(Exception ex) { throw new AbideTagFileException("Abide Tag file failed to save.", ex); }
        }
        /// <summary>
        /// Saves the Abide Halo 2 Tag file to the specified stream.
        /// </summary>
        /// <param name="outStream">The stream to write the Abide Halo 2 Tag file to.</param>
        public void Save(Stream outStream)
        {
            //Setup Header
            header.ATagTag = "ATag";
            header.DataLength = (uint)data.Length;

            //Check
            if (outStream == null) throw new ArgumentNullException(nameof(outStream));
            if (!outStream.CanSeek) throw new ArgumentException("Stream does not support seeking.", nameof(outStream));
            if (!outStream.CanWrite) throw new ArgumentException("Stream does not support writing.", nameof(outStream));

            //Create Binary Writer
            using (BinaryWriter writer = new BinaryWriter(outStream))
            {
                //Write File
                writer.Write(header);
                writer.Write(data.GetBuffer());
            }
        }
        
        /// <summary>
        /// Represents an Abide Halo 2 Tag File header.
        /// </summary>
        private struct Header
        {
            /// <summary>
            /// Gets or sets the four-character-code string for the file header.
            /// </summary>
            public string ATagTag
            {
                get { return aTagTag; }
                set { aTagTag = value; }
            }
            /// <summary>
            /// Gets or sets the four-character-code Halo Tag string.
            /// </summary>
            public string TagType
            {
                get { return tagType; }
                set { tagType = value; }
            }
            /// <summary>
            /// Gets or sets the length of the Tag data in bytes.
            /// </summary>
            public uint DataLength
            {
                get { return dataLength; }
                set { dataLength = value; }
            }
            /// <summary>
            /// Gets or sets the memory address of the Tag data.
            /// </summary>
            public uint MemoryAddress
            {
                get { return memoryAddress; }
                set { memoryAddress = value; }
            }

            private FourCc aTagTag;
            private Tag tagType;
            private uint dataLength;
            private uint memoryAddress;
        }
    }

    /// <summary>
    /// Represents an error that can occur when handling a <see cref="AbideTagFile"/> instance.
    /// </summary>
    public class AbideTagFileException : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="AbideTagFileException"/>.
        /// </summary>
        public AbideTagFileException() : base()
        { }
        /// <summary>
        /// Initializes a new instance of <see cref="AbideTagFileException"/> using the provided exception message.
        /// </summary>
        /// <param name="message">The exception's message.</param>
        public AbideTagFileException(string message) :
            base(message)
        { }
        /// <summary>
        /// Initializes a new instance of <see cref="AbideTagFileException"/> using the provided inner exception.
        /// </summary>
        /// <param name="innerException">The inner exception that triggered the <see cref="AbideTagFileException"/>.</param>
        public AbideTagFileException(Exception innerException) : base(innerException.Message, innerException)
        { }
        /// <summary>
        /// Initializes a new instance of <see cref="AbideTagFileException"/> using the provided exception message and inner exception.
        /// </summary>
        /// <param name="message">The exception's message.</param>
        /// <param name="innerException">The inner exception that triggered the <see cref="AbideTagFileException"/>.</param>
        public AbideTagFileException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
