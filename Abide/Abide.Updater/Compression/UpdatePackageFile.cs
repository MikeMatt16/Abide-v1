using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Abide.Updater.Compression
{
    /// <summary>
    /// Represents an Abide Update Package file.
    /// </summary>
    public sealed class UpdatePackageFile
    {
        /// <summary>
        /// Represents a four-character code 'AUP' string.
        /// </summary>
        private static readonly FOURCC AupFourCc = "AUP";
        
        /// <summary>
        /// Gets and returns the package file entry list.
        /// </summary>
        public FileEntryList Entries
        {
            get { return fileEntries; }
        }
        
        private FileEntryList fileEntries;
        private HEADER header;

        /// <summary>
        /// Initializes a new <see cref="UpdatePackageFile"/> instance.
        /// </summary>
        public UpdatePackageFile()
        {
            //Prepare
            fileEntries = new FileEntryList();
        }

        /// <summary>
        /// Loads an Abide Update package file from a specified file path.
        /// </summary>
        /// <param name="filename">A relative or absolute path for the Abide Update package.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="AbideUpdatePackageException"></exception>
        public void Load(string filename)
        {
            //Check...
            if (filename == null)
                throw new ArgumentNullException("fileName");
            else if (!File.Exists(filename))
                throw new FileNotFoundException("Unable to find the specified file.", filename);

            //Load...?
            try
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
                    Load(fs);
            }
            catch (Exception ex) { throw new AbideUpdatePackageException(ex); }
        }
        /// <summary>
        /// Loads an Abide Update package file from the specified stream.
        /// </summary>
        /// <param name="inStream">The stream containing the Abide Update package.</param>
        /// <exception cref="AbideUpdatePackageException"></exception>
        public void Load(Stream inStream)
        {
            //Check file...
            if (inStream.Length < 32) throw new AbideUpdatePackageException("Invalid AddOn package file.");

            //Prepare
            FileEntry[] fileEntries = null;

            try
            {
                //Create Reader
                using (BinaryReader reader = new BinaryReader(inStream))
                {
                    //Read Header
                    header = reader.ReadStructure<HEADER>();
                    if (header.AupTag == AupFourCc)  //Quick sanity check...
                    {
                        fileEntries = new FileEntry[header.EntryCount];
                        for (uint i = 0; i < header.EntryCount; i++) fileEntries[i] = new FileEntry(reader.ReadStructure<ENTRY>());
                        string[] filenames = reader.ReadUTF8StringTable(header.FileNamesOffset, header.FileIndexOffset, (int)header.EntryCount);

                        //Loop
                        for (int i = 0; i < header.EntryCount; i++)
                        {
                            //Create
                            fileEntries[i].Filename = filenames[i];

                            //Goto
                            inStream.Seek(fileEntries[i].Offset + header.DataOffset, SeekOrigin.Begin);

                            //Read
                            fileEntries[i].Data = reader.ReadBytes(fileEntries[i].Length);

                            //Set Length
                            fileEntries[i].Length = fileEntries[i].Data.Length;
                        }

                        //Setup
                        this.fileEntries = new FileEntryList(fileEntries);
                    }
                    else throw new AbideUpdatePackageException("Invalid file header.");
                }
            }
            catch (Exception ex) { throw new AbideUpdatePackageException(ex); }
        }
        /// <summary>
        /// Saves this Abide Update package file to the specfied file or stream.
        /// </summary>
        /// <param name="filename">A string that contains the name of the file to which to save this package.</param>
        /// <exception cref="ArgumentNullException"><paramref name="filename"/> is null.</exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="AbideUpdatePackageException"></exception>
        public void Save(string filename)
        {
            //Check
            if (filename == null) throw new ArgumentNullException("filename");
            if (!Directory.Exists(Path.GetDirectoryName(filename))) throw new DirectoryNotFoundException();

            //Create for read/write
            using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
                Save(fs);
        }
        /// <summary>
        /// Saves this Abide Update package file to the specified stream.
        /// </summary>
        /// <param name="outStream">The stream where the package will be saved.</param>
        /// <exception cref="ArgumentNullException"><paramref name="outStream"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="outStream"/> does not support seeking.</exception>
        /// <exception cref="AbideUpdatePackageException">A write error occured.</exception>
        public void Save(Stream outStream)
        {
            //Setup Header
            header.AupTag = AupFourCc;
            header.EntryCount = (uint)fileEntries.Count;

            //Prepare
            ENTRY[] entries = new ENTRY[header.EntryCount];
            byte[] data = null;
            uint length = 0;

            //Compress and copy data...
            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(ms))
            {
                //Loop through entries
                for (int i = 0; i < header.EntryCount; i++)
                {
                    //Get Data...
                    data = fileEntries[i].Data;

                    //Check...
                    if (data != null)
                    {
                        //Setup
                        entries[i] = new ENTRY();
                        entries[i].Length = (uint)data.Length;
                        entries[i].Offset = (uint)ms.Position;
                        entries[i].Created = fileEntries[i].Created;
                        entries[i].Modified = fileEntries[i].Modified;
                        entries[i].Accessed = fileEntries[i].Accessed;

                        //Write
                        writer.Write(data);

                        //Increment
                        length += (uint)data.Length;
                    }
                    else throw new ArgumentNullException("data");
                }

                //Copy
                data = new byte[length];
                Array.Copy(ms.GetBuffer(), 0, data, 0, data.Length);
            }

            //Create Writer
            using (BinaryWriter writer = new BinaryWriter(outStream))
            {
                //Goto entries start...
                outStream.Seek(HEADER.RuntimeSize, SeekOrigin.Begin);

                //Get File index offset...
                header.FileIndexOffset = (uint)outStream.Seek(ENTRY.RuntimeSize * entries.Length, SeekOrigin.Current);

                //Get File names offset...
                header.FileNamesOffset = (uint)outStream.Seek(header.EntryCount * 4L, SeekOrigin.Current);

                //Write File Names
                foreach (string filename in fileEntries.Select(e => e.Filename))
                    writer.WriteUTF8NullTerminated(filename);

                //Get Data Offset
                header.DataOffset = (uint)outStream.Position;

                //Write Files Index
                int offset = 0;
                outStream.Seek(header.FileIndexOffset, SeekOrigin.Begin);
                foreach (string filename in fileEntries.Select(e => e.Filename))
                { writer.Write(offset); offset += filename.Length + 1; }

                //Setup File Length
                header.Length = header.DataOffset + (uint)data.Length;

                //Write Header
                outStream.Seek(0, SeekOrigin.Begin);
                writer.Write(header);

                //Write Entries
                foreach (ENTRY entry in entries)
                    writer.Write(entry);

                //Write Data
                outStream.Seek(header.DataOffset, SeekOrigin.Begin);
                writer.Write(data);
            }
        }
        /// <summary>
        /// Adds a file to the package.
        /// </summary>
        /// <param name="filename">The name of the file.</param>
        public void AddFile(string filename, string targetFileName)
        {
            //Prepare
            byte[] buffer = null;
            FileInfo info = null;

            //Check
            if (filename == null) throw new ArgumentNullException(nameof(filename));
            if (targetFileName == null) throw new ArgumentNullException(nameof(targetFileName));
            if (!File.Exists(filename)) throw new FileNotFoundException("Unable to find file.", filename);

            //Load
            try
            {
                //Load Info
                info = new FileInfo(filename);

                using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    //Prepare
                    buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                }
            }
            catch (Exception) { }

            //Create file entry
            if (buffer != null && info != null)
            {
                //Create Entry
                FileEntry entry = new FileEntry(buffer) { Filename = targetFileName };
                entry.Created = info.CreationTime;
                entry.Modified = info.LastWriteTime;
                entry.Accessed = info.LastAccessTime;

                //Add
                fileEntries.Add(entry);
            }
        }
        /// <summary>
        /// Loads a file from the package.
        /// </summary>
        /// <param name="filename">The file name of the file within the package.</param>
        /// <returns>A stream.</returns>
        public Stream LoadFile(string filename)
        {
            //Prepare
            MemoryStream ms = null;

            //Check...
            if (fileEntries.ContainsFilename(filename))
                ms = new MemoryStream(fileEntries[filename].Data);

            //Return
            return ms;
        }

        /// <summary>
        /// Represents an Abide Update Package File Entry list.
        /// </summary>
        public sealed class FileEntryList : IEnumerable<FileEntry>, ICollection<FileEntry>, IList<FileEntry>
        {
            /// <summary>
            /// Gets and returns a <see cref="FileEntry"/> instance from the supplied file name.
            /// </summary>
            /// <param name="filename">The name of the file.</param>
            /// <returns>null if the file is not found, else returns a <see cref="FileEntry"/> instance.</returns>
            public FileEntry this[string filename]
            {
                get
                {
                    if (entryLookup.ContainsKey(filename))
                        return entries[entryLookup[filename]];
                    else return null;
                }
            }
            /// <summary>
            /// Gets or sets a <see cref="FileEntry"/> instance at a specified index in the list.
            /// </summary>
            /// <param name="index">The index to get or set the <see cref="FileEntry"/> instance.</param>
            /// <returns>A <see cref="FileEntry"/> instance located at <paramref name="index"/> within the list.</returns>
            public FileEntry this[int index]
            {
                get { return entries[index]; }
                set { entries[index] = value; }
            }
            /// <summary>
            /// Gets and returns the number of <see cref="FileEntry"/> instances within the list.
            /// </summary>
            public int Count
            {
                get { return entries.Count; }
            }
            /// <summary>
            /// Gets and returns false.
            /// </summary>
            public bool IsReadOnly
            {
                get { return false; }
            }

            private readonly List<FileEntry> entries;
            private readonly Dictionary<string, int> entryLookup;

            /// <summary>
            /// Initializes a new <see cref="FileEntryList"/> instance.
            /// </summary>
            public FileEntryList()
            {
                entries = new List<FileEntry>();
                entryLookup = new Dictionary<string, int>();
            }
            /// <summary>
            /// Initializes a new <see cref="FileEntryList"/> instance with a collection.
            /// </summary>
            /// <param name="entries"></param>
            public FileEntryList(FileEntry[] entries) : this()
            {
                //Loop
                foreach (FileEntry entry in entries)
                    if (!entryLookup.ContainsKey(entry.Filename))
                    { this.entries.Add(entry); entryLookup.Add(entry.Filename, this.entries.IndexOf(entry)); }
            }
            /// <summary>
            /// Adds a file entry the list.
            /// </summary>
            /// <param name="item">The file entry to add.</param>
            public void Add(FileEntry item)
            {
                //Check
                if (!entryLookup.ContainsKey(item.Filename))
                {
                    entries.Add(item);
                    RebuildLookup();
                }
            }
            /// <summary>
            /// Clears all file entries from the list.
            /// </summary>
            public void Clear()
            {
                entries.Clear();
            }
            /// <summary>
            /// Returns whether a specific file entry exists within the list.
            /// </summary>
            /// <param name="item">The file entry to check for.</param>
            /// <returns>true if the list contains <paramref name="item"/>, false if not.</returns>
            public bool Contains(FileEntry item)
            {
                return entries.Contains(item);
            }
            /// <summary>
            /// Returns whether a specific file with a specified file name exists within the list.
            /// </summary>
            /// <param name="filename">The name of the file to check for.</param>
            /// <returns>true if the list contains a file with filename <paramref name="filename"/>, false if not.</returns>
            public bool ContainsFilename(string filename)
            {
                return entryLookup.ContainsKey(filename);
            }
            /// <summary>
            /// Copies the contents of the file entry list to a specified array at a given index.
            /// </summary>
            /// <param name="array">The destination array.</param>
            /// <param name="arrayIndex">The index within the destination array to begin copying the file entries.</param>
            public void CopyTo(FileEntry[] array, int arrayIndex)
            {
                entries.CopyTo(array, arrayIndex);
            }
            /// <summary>
            /// Returns an enumerator that iterates through this list.
            /// </summary>
            /// <returns>An enumerator.</returns>
            public IEnumerator<FileEntry> GetEnumerator()
            {
                return ((IEnumerable<FileEntry>)entries).GetEnumerator();
            }
            /// <summary>
            /// Returns the index of a specified file entry.
            /// </summary>
            /// <param name="item">The file entry whose index is to be retrieved.</param>
            /// <returns>-1 if the file entry is not found, otherwise the index of <paramref name="item"/>.</returns>
            public int IndexOf(FileEntry item)
            {
                return entries.IndexOf(item);
            }
            /// <summary>
            /// Inserts a file entry into the list at a supplied index.
            /// </summary>
            /// <param name="index">The index to insert at.</param>
            /// <param name="item">The item to add.</param>
            public void Insert(int index, FileEntry item)
            {
                //Check
                if (!entryLookup.ContainsKey(item.Filename))
                {
                    entries.Insert(index, item);
                    RebuildLookup();
                }
            }
            /// <summary>
            /// Attempts to remove a file entry from the list.
            /// </summary>
            /// <param name="item">The item to remove.</param>
            /// <returns>true if the file entry was found and removed, otherwise false.</returns>
            public bool Remove(FileEntry item)
            {
                bool removed = entries.Remove(item);
                if (removed) RebuildLookup();
                return removed;
            }
            /// <summary>
            /// Removes a file entry from the list at a given index.
            /// </summary>
            /// <param name="index">The index to remove a file entry.</param>
            public void RemoveAt(int index)
            {
                entries.RemoveAt(index);
                RebuildLookup();
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable<FileEntry>)entries).GetEnumerator();
            }
            private void RebuildLookup()
            {
                entryLookup.Clear();
                foreach (FileEntry entry in entries) entryLookup.Add(entry.Filename, entries.IndexOf(entry));
            }
            internal void Add(object manifestEntry)
            {
                throw new NotImplementedException();
            }
        }
    }

    /// <summary>
    /// Represents a file entry within an Abide Update package file.
    /// </summary>
    public sealed class FileEntry
    {
        /// <summary>
        /// Gets or sets the length of the entry data.
        /// </summary>
        public int Length
        {
            get { return length; }
            set { length = value; }
        }
        /// <summary>
        /// Gets or sets the offset of the entry data.
        /// </summary>
        public int Offset
        {
            get { return offset; }
            set { offset = value; }
        }
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        public string Filename
        {
            get { return filename; }
            set { filename = value; }
        }
        /// <summary>
        /// Gets or sets the uncompressed file data.
        /// </summary>
        public byte[] Data
        {
            get { return data; }
            set { data = value; }
        }
        /// <summary>
        /// Gets or sets the file creation date.
        /// </summary>
        public DateTime Created
        {
            get { return new DateTime(created); }
            set { created = value.Ticks; }
        }
        /// <summary>
        /// Gets or sets the file modified date.
        /// </summary>
        public DateTime Modified
        {
            get { return new DateTime(modified); }
            set { modified = value.Ticks; }
        }
        /// <summary>
        /// Gets or sets the file accessed date.
        /// </summary>
        public DateTime Accessed
        {
            get { return new DateTime(accessed); }
            set { accessed = value.Ticks; }
        }
        
        private int offset, length;
        private long created, modified, accessed;
        private string filename;
        private byte[] data;

        /// <summary>
        /// Initializes a new <see cref="FileEntry"/> instance.
        /// </summary>
        public FileEntry() : this(new ENTRY()) { }
        /// <summary>
        /// Initializes a new <see cref="FileEntry"/> instance using the supplied data buffer.
        /// </summary>
        /// <param name="data">The data buffer for the entry.</param>
        public FileEntry(byte[] data) : this(new ENTRY())
        {
            this.data = data;
            length = data.Length;
        }
        /// <summary>
        /// Initializes a new <see cref="FileEntry"/> instance using the supplied <see cref="ENTRY"/> object.
        /// </summary>
        /// <param name="entry">The entry object.</param>
        internal FileEntry(ENTRY entry)
        {
            filename = string.Empty;
            offset = (int)entry.Offset;
            length = (int)entry.Length;
            created = entry.Created.Ticks;
            modified = entry.Modified.Ticks;
            accessed = entry.Accessed.Ticks;
            data = new byte[0];
        }
        public override string ToString()
        {
            return $"{filename} Length: {length}";
        }
    }

    /// <summary>
    /// Represents an error that occurs while handling an Abide Update Package file.
    /// </summary>
    public sealed class AbideUpdatePackageException : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="AbideUpdatePackageException"/>.
        /// </summary>
        public AbideUpdatePackageException() : base()
        { }
        /// <summary>
        /// Initializes a new instance of <see cref="AbideUpdatePackageException"/> using the provided exception message.
        /// </summary>
        /// <param name="message">The exception's message.</param>
        public AbideUpdatePackageException(string message) :
            base(message)
        { }
        /// <summary>
        /// Initializes a new instance of <see cref="AbideUpdatePackageException"/> using the provided inner exception.
        /// </summary>
        /// <param name="innerException">The inner exception that triggered the <see cref="AbideUpdatePackageException"/>.</param>
        public AbideUpdatePackageException(Exception innerException) : base(innerException.Message, innerException)
        { }
        /// <summary>
        /// Initializes a new instance of <see cref="AbideUpdatePackageException"/> using the provided exception message and inner exception.
        /// </summary>
        /// <param name="message">The exception's message.</param>
        /// <param name="innerException">The inner exception that triggered the <see cref="AbideUpdatePackageException"/>.</param>
        public AbideUpdatePackageException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
