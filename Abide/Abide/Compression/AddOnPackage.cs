﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Abide.Compression
{
    /// <summary>
    /// Represents an Abide AddOn Package file.
    /// </summary>
    public sealed class AddOnPackageFile
    {
        /// <summary>
        /// Represents a four-character code 'AAO' string.
        /// </summary>
        private static readonly FOURCC AaoFourCc = "AAO";

        /// <summary>
        /// Occurs when data has to be decompressed.
        /// </summary>
        public event DataModifyEventHandler DecompressData
        {
            add { decompressData += value; }
            remove { decompressData -= value; }
        }
        /// <summary>
        /// Occurs when data has to be compressed.
        /// </summary>
        public event DataModifyEventHandler CompressData
        {
            add { compressData += value; }
            remove { compressData -= value; }
        }
        /// <summary>
        /// Gets and returns the package file entry list.
        /// </summary>
        public FileEntryList Entries
        {
            get { return fileEntries; }
        }

        private event DataModifyEventHandler compressData;
        private event DataModifyEventHandler decompressData;
        private FileEntryList fileEntries;
        private HEADER header;

        /// <summary>
        /// Initializes a new <see cref="AddOnPackageFile"/> instance.
        /// </summary>
        public AddOnPackageFile()
        {
            //Prepare
            fileEntries = new FileEntryList();
        }

        /// <summary>
        /// Loads an Abide AddOn package file from a specified file path.
        /// </summary>
        /// <param name="filename">A relative or absolute path for the Abide AddOn package.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="AbideAddOnPackageException"></exception>
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
            catch (Exception ex) { throw new AbideAddOnPackageException(ex); }
        }
        /// <summary>
        /// Loads an Abide AddOn package file from the specified stream.
        /// </summary>
        /// <param name="inStream">The stream containing the Abide AddOn package.</param>
        /// <exception cref="AbideAddOnPackageException"></exception>
        public void Load(Stream inStream)
        {
            //Check file...
            if (inStream.Length < 32) throw new AbideAddOnPackageException("Invalid AddOn package file.");

            //Prepare
            FileEntry[] fileEntries = null;

            try
            {
                //Create Reader
                using (BinaryReader reader = new BinaryReader(inStream))
                {
                    //Read Header
                    header = reader.ReadStructure<HEADER>();
                    if (header.AaoTag == AaoFourCc)  //Quick sanity check...
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
                            byte[] data = reader.ReadBytes((int)fileEntries[i].Length);

                            //Decompress?
                            if (fileEntries[i].Compression != FOURCC.Zero) fileEntries[i].Data = decompressData.Invoke(data, this.fileEntries[i].Compression);
                            else fileEntries[i].Data = data;
                        }

                        //Setup
                        this.fileEntries = new FileEntryList(fileEntries);
                    }
                    else throw new AbideAddOnPackageException("Invalid file header.");
                }
            }
            catch (Exception ex) { throw new AbideAddOnPackageException(ex); }
        }
        /// <summary>
        /// Saves this Abide AddOn package file to the specfied file or stream.
        /// </summary>
        /// <param name="filename">A string that contains the name of the file to which to save this package.</param>
        /// <exception cref="ArgumentNullException"><paramref name="filename"/> is null.</exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="AbideAddOnPackageException"></exception>
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
        /// Saves this Abide AddOn package file to the specified stream.
        /// </summary>
        /// <param name="stream">The stream where the package will be saved.</param>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="stream"/> does not support seeking.</exception>
        /// <exception cref="AbideAddOnPackageException">A write error occured.</exception>
        public void Save(Stream outStream)
        {
            //Setup Header
            header.AaoTag = AaoFourCc;
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
                    if (fileEntries[i].Compression != FOURCC.Zero) data = compressData.Invoke(fileEntries[i].Data, fileEntries[i].Compression);
                    else data = fileEntries[i].Data;

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
        /// Adds a file to the AddOn package.
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
            if(buffer != null && info != null)
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

        public sealed class FileEntryList : IEnumerable<FileEntry>, ICollection<FileEntry>, IList<FileEntry>
        {
            public FileEntry this[string filename]
            {
                get { return entries[entryLookup[filename]]; }
            }
            public FileEntry this[int index]
            {
                get { return entries[index]; }
                set { entries[index] = value; }
            }
            public int Count
            {
                get { return entries.Count; }
            }
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
                    if(!entryLookup.ContainsKey(entry.Filename))
                    { this.entries.Add(entry); entryLookup.Add(entry.Filename, this.entries.IndexOf(entry)); }
            }
            public void Add(FileEntry item)
            {
                //Check
                if (!entryLookup.ContainsKey(item.Filename))
                {
                    entries.Add(item);
                    RebuildLookup();
                }
            }
            public void Clear()
            {
                entries.Clear();
            }
            public bool Contains(FileEntry item)
            {
                return entries.Contains(item);
            }
            public bool ContainsFilename(string filename)
            {
                return entryLookup.ContainsKey(filename);
            }
            public void CopyTo(FileEntry[] array, int arrayIndex)
            {
                entries.CopyTo(array, arrayIndex);
            }
            public IEnumerator<FileEntry> GetEnumerator()
            {
                return ((IEnumerable<FileEntry>)entries).GetEnumerator();
            }
            public int IndexOf(FileEntry item)
            {
                return entries.IndexOf(item);
            }
            public void Insert(int index, FileEntry item)
            {
                //Check
                if (!entryLookup.ContainsKey(item.Filename))
                {
                    entries.Insert(index, item);
                    RebuildLookup();
                }
            }
            public bool Remove(FileEntry item)
            {
                bool removed = entries.Remove(item);
                if (removed) RebuildLookup();
                return removed;
            }
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
    /// Represents a file entry within an AddOn Package file.
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
        /// <summary>
        /// Gets or sets the compression four-character code string.
        /// </summary>
        public string Compression
        {
            get { return compression; }
            set { compression = value; }
        }

        private FOURCC compression;
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
            compression = FOURCC.Zero;
            data = new byte[0];
        }
        public override string ToString()
        {
            return $"{filename} Length: {length}";
        }
    }

    /// <summary>
    /// Represents a method containing 
    /// </summary>
    /// <param name="data"></param>
    /// <param name="compressionFourCc"></param>
    /// <returns></returns>
    public delegate byte[] DataModifyEventHandler(byte[] data, string compressionFourCc);
}
