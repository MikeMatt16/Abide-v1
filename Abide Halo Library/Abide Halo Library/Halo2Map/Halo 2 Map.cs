using Abide_Halo_Library.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Collections;

namespace Abide_Halo_Library.Halo_2_Map
{
    /// <summary>
    /// Represents a Halo 2 Map File.
    /// </summary>
    public class Halo2Map : MarshalByRefObject, IDisposable, IEnumerable<IndexEntry>, ICollection<IndexEntry>
    {
        /// <summary>
        /// Gets and returns an <see cref="IndexEntry"/> instance contained in this map.
        /// </summary>
        /// <param name="index">The zero-based index of the <see cref="IndexEntry"/> contained in the map.</param>
        /// <returns>An <see cref="IndexEntry"/> object at the supplied <paramref name="index"/>, or null if it is out of range.</returns>
        public IndexEntry this[int index]
        {
            get { if (index >= 0 && entries.Length > index) return entries[index]; else return null; }
        }
        /// <summary>
        /// Gets the number of <see cref="IndexEntry"/> elements contained in this instance.
        /// </summary>
        public int Count
        {
            get { return ((ICollection<IndexEntry>)entries).Count; }
        }
        /// <summary>
        /// Gets a value indicating whether the <see cref="Halo2Map"/> instance's entries list is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get { return ((ICollection<IndexEntry>)entries).IsReadOnly; }
        }
        /// <summary>
        /// Gets the <see cref="HaloIO"/> of this instance.
        /// </summary>
        public HaloIO IO
        {
            get { return io; }
        }
        /// <summary>
        /// Gets and returns the map version.
        /// </summary>
        public int Version
        {
            get { return header.Version; }
        }
        /// <summary>
        /// Gets or sets the length in bytes of the map file.
        /// </summary>
        public int FileLength
        {
            get { return header.FileLength; }
            set
            {
                header.FileLength = value;
                OnHeaderEdit(header);
            }
        }
        /// <summary>
        /// Gets or sets the zero-based offset of the map's index.
        /// </summary>
        public int IndexOffset
        {
            get { return header.IndexOffset; }
            set
            {
                header.IndexOffset = value;
                OnHeaderEdit(header);
            }
        }
        /// <summary>
        /// Gets or sets the length in bytes of the map's index.
        /// </summary>
        public int IndexLength
        {
            get { return header.IndexLength; }
            set
            {
                header.IndexLength = value;
                OnHeaderEdit(header);
            }
        }
        /// <summary>
        /// Gets or sets the length in bytes of the map's meta block.
        /// </summary>
        public int MetaLength
        {
            get { return header.MetaLength; }
            set
            {
                header.MetaLength = value;
                OnHeaderEdit(header);
            }
        }
        /// <summary>
        /// Gets or sets the length in bytes of the map's non-raw data.
        /// </summary>
        public int NonRawLength
        {
            get { return header.NonRawLength; }
            set
            {
                header.NonRawLength = value;
                OnHeaderEdit(header);
            }
        }
        /// <summary>
        /// Gets or sets the map's origin. This value is typically unused, but can be used for debugging purposes. 
        /// </summary>
        public string Origin
        {
            get { return header.Origin; }
            set
            {
                header.Origin = value;
                OnHeaderEdit(header);
            }
        }
        /// <summary>
        /// Gets or sets the map's build string.
        /// </summary>
        public string Build
        {
            get { return header.Build; }
        }
        /// <summary>
        /// Gets or sets the zero-based offset of the map's crazy block.
        /// </summary>
        public int Crazy
        {
            get { return header.CrazyOffset; }
            set
            {
                header.CrazyOffset = value;
                OnHeaderEdit(header);
            }
        }
        /// <summary>
        /// Gets or sets the length in bytes of the map's crazy block.
        /// </summary>
        public int CrazySize
        {
            get { return header.CrazyLength; }
            set
            {
                header.CrazyLength = value;
                OnHeaderEdit(header);
            }
        }
        /// <summary>
        /// Gets or sets the zero-based offset of the map's 128-length strings table.
        /// </summary>
        public int Strings128Offset
        {
            get { return header.Strings128Offset; }
            set
            {
                header.Strings128Offset = value;
                OnHeaderEdit(header);
            }
        }
        /// <summary>
        /// Gets or sets the number of strings in the map's strings table.
        /// </summary>
        public int StringCount
        {
            get { return header.StringCount; }
            set
            {
                header.StringCount = value;
                OnHeaderEdit(header);
            }
        }
        /// <summary>
        /// Gets or sets the length in bytes of the map's strings table.
        /// </summary>
        public int StringsLength
        {
            get { return header.StringsLength; }
            set
            {
                header.StringsLength = value;
                OnHeaderEdit(header);
            }
        }
        /// <summary>
        /// Gets or sets the zero-based offset of the map's strings index table.
        /// </summary>
        public int StringsIndex
        {
            get { return header.StringsIndexOffset; }
            set
            {
                header.StringsIndexOffset = value;
                OnHeaderEdit(header);
            }
        }
        /// <summary>
        /// Gets or sets the zero-based offset of the map's strings table.
        /// </summary>
        public int StringsOffset
        {
            get { return header.StringsOffset; }
            set
            {
                header.StringsOffset = value;
                OnHeaderEdit(header);
            }
        }
        /// <summary>
        /// Gets or sets the name of the map.
        /// </summary>
        public string Name
        {
            get { return header.Name; }
            set
            {
                header.Name = value;
                OnHeaderEdit(header);
            }
        }
        /// <summary>
        /// Gets or sets the path of the map's scenario object.
        /// </summary>
        public string ScenarioPath
        {
            get { return header.ScenarioPath; }
            set
            {
                header.ScenarioPath = value;
                OnHeaderEdit(header);
            }
        }
        /// <summary>
        /// Gets or sets the count of file names in the map's file names table.
        /// </summary>
        public int FileCount
        {
            get { return header.FileCount; }
            set
            {
                header.FileCount = value;
                OnHeaderEdit(header);
            }
        }
        /// <summary>
        /// Gets or sets the length in bytes of the map's file names table.
        /// </summary>
        public int FilesLength
        {
            get { return header.FilesLength; }
            set
            {
                header.FilesLength = value;
                OnHeaderEdit(header);
            }
        }
        /// <summary>
        /// Gets or sets the zero-based offset of the map's file names table.
        /// </summary>
        public int FilesOffset
        {
            get { return header.FilesOffset; }
            set
            {
                header.FilesOffset = value;
                OnHeaderEdit(header);
            }
        }
        /// <summary>
        /// Gets or sets the zero-based offset of the map's file names index.s
        /// </summary>
        public int FilesIndex
        {
            get { return header.FilesIndex; }
            set
            {
                header.FilesIndex = value;
                OnHeaderEdit(header);
            }
        }
        /// <summary>
        /// Gets or sets the map's signature.
        /// This value is calculated by performing an XOR operation of every 32-bit integer after the map file's header.
        /// </summary>
        public int Signature
        {
            get { return header.Signature; }
            set
            {
                header.Signature = value;
                OnHeaderEdit(header);
            }
        }
        /// <summary>
        /// Gets and returns the map's primary magic value.
        /// </summary>
        public int PrimaryMagic
        {
            get { return index.IndexAddress - header.IndexOffset + 32; }
        }
        /// <summary>
        /// Gets or sets the map's meta magic value.
        /// </summary>
        public int SecondaryMagic
        {
            get { return magic; }
            set { magic = value; }
        }
        /// <summary>
        /// Gets or sets the number of tag objects in the map's index's tags table.
        /// </summary>
        public int TagListCount
        {
            get { return index.TagCount; }
            set
            {
                index.TagCount = value;
                OnIndexEdit(index);
            }
        }
        /// <summary>
        /// Gets or sets the zero-based offset of the map's index's object index.
        /// </summary>
        public int ObjectIndexOffset
        {
            get { return index.ObjectOffset; }
            set
            {
                index.ObjectOffset = value;
                OnIndexEdit(index);
            }
        }
        /// <summary>
        /// Gets or sets the map's index's scenario identifier.
        /// </summary>
        public int ScenarioID
        {
            get { return index.ScenarioID; }
            set
            {
                index.ScenarioID = value;
                OnIndexEdit(index);
            }
        }
        /// <summary>
        /// Gets or sets the map's index's globals identifier.
        /// </summary>
        public int GlobalsID
        {
            get { return index.GlobalsID; }
            set
            {
                index.GlobalsID = value;
                OnIndexEdit(index);
            }
        }
        /// <summary>
        /// Gets or sets the number of object entries in the map's index's object table.
        /// </summary>
        public int ObjectCount
        {
            get { return index.ObjectCount; }
            set
            {
                index.ObjectCount = value;
                OnIndexEdit(index);
            }
        }
        /// <summary>
        /// Gets and returns the zero-based offset of the map's index's object entries table.
        /// </summary>
        public int ObjectStart
        {
            get { return header.IndexOffset + INDEX.Length + (TAGHIERARCHY.Length * index.TagCount); }
        }
        /// <summary>
        /// Gets the map's <see cref="IndexEntry"/> array.
        /// </summary>
        public IndexEntry[] IndexEntries
        {
            get { return (IndexEntry[])entries.Clone(); }
        }
        /// <summary>
        /// Gets the map's string objects.
        /// </summary>
        public string[] StringIDs
        {
            get { return (string[])stringIDs.Clone(); }
        }
        /// <summary>
        /// Gets the map's string 128 objects.
        /// </summary>
        public string[] String128s
        {
            get { return (string[])string128s.Clone(); }
        }
        /// <summary>
        /// Gets the map's scenario entry.
        /// </summary>
        public IndexEntry Scenario
        {
            get { return scenario; }
        }
        /// <summary>
        /// Gets the map's globals entry.
        /// </summary>
        public IndexEntry Globals
        {
            get { return globals; }
        }
        /// <summary>
        /// Gets the map's file list.
        /// </summary>
        internal string[] FileList
        {
            get { return (string[])fileList.Clone(); }
        }
        
        private HaloIO io;
        private HEADER header;
        private INDEX index;
        private int[] tagAddresses;
        private TAGHIERARCHY[] tags;
        private int[] objectAddresses;
        private OBJECT[] objects;
        private IndexEntry[] entries;
        private IndexEntry scenario;
        private IndexEntry globals;
        private int[] fileIndices;
        private int[] stringIndices;
        private string[] fileList;
        private string[] stringIDs;
        private string[] string128s;
        private STRINGID[] ids;
        private int magic;

        /// <summary>
        /// Initializes a new Halo2Map instance.
        /// </summary>
        public Halo2Map()
        {
            //Setup
            tags = new TAGHIERARCHY[0];
            objects = new OBJECT[0];
            entries = new IndexEntry[0];
            fileIndices = new int[0];
            stringIndices = new int[0];
            fileList = new string[0];
            stringIDs = new string[0];
            string128s = new string[0];
            ids = new STRINGID[0];
        }
        /// <summary>
        /// Disposed any resources used by this instance.
        /// </summary>
        public void Dispose()
        {
            io.Dispose();
        }
        /// <summary>
        /// Loads a Halo 2 Map file using the specified file.
        /// </summary>
        /// <param name="fileLocation">The location of the file.</param>
        public void LoadMap(string fileLocation)
        {
            //Check if Nulled...
            if (io != null)
            {
                io.CloseIO();
                io.Dispose();
            }

            //Prepare new IO Handler
            io = new HaloIO(fileLocation);
            LoadMap(io);
        }
        /// <summary>
        /// Loads a Halo 2 Map file using the specified stream.
        /// </summary>
        /// <param name="stream">The stream containing the map data.</param>
        public void LoadMap(Stream stream)
        {
            //Check if Nulled...
            if (io != null)
            {
                io.CloseIO();
                io.Dispose();
            }

            //Prepare new IO Handler
            io = new HaloIO(stream);
            LoadMap(io);
        }
        /// <summary>
        /// Loads a Halo 2 Map file using the specified <see cref="HaloIO"/> instance.
        /// </summary>
        /// <param name="io">The <see cref="HaloIO"/> used to access the map file.</param>
        private void LoadMap(HaloIO io)
        {
            //Open
            io.OpenIO();

            //Read Header
            io.Position = 0;
            header = io.In.ReadStructure<HEADER>();

            //Prepare Index Table Header
            io.Position = header.IndexOffset;
            index = io.In.ReadStructure<INDEX>();

            //Load File Names
            LoadFileNamesInfo(io);

            //Load Tag Objects
            LoadTagsInfo(io);

            //Load Index Objects
            LoadObjectInfo(io);

            //Load Strings
            LoadStringsInfo(io);

            //Fix SBSPs and LTMPs
            FixSpecialEntries(io);

            //Close Map
            io.CloseIO();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="fileLocation"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public IAsyncResult BeginLoadMap(AsyncCallback callback, string fileLocation, object state)
        {
            //Create
            AsyncOperation loadMapOperation = AsyncOperationManager.CreateOperation(state);
            LoadMapFromFileWorker worker = new LoadMapFromFileWorker(LoadMap);
            return worker.BeginInvoke(fileLocation, callback, state);
        }
        public IAsyncResult BeginLoadMap(AsyncCallback callback, Stream mapStream, object state)
        {
            //Create
            AsyncOperation loadMapOperation = AsyncOperationManager.CreateOperation(state);
            LoadMapFromStreamWorker worker = new LoadMapFromStreamWorker(LoadMap);
            return worker.BeginInvoke(mapStream, callback, worker);
        }
        public void EndLoadMap(IAsyncResult ar)
        {
            //Get Worker...
            if (ar.AsyncState is LoadMapFromFileWorker)
            {
                //End
                LoadMapFromFileWorker fileLoadWorker = (LoadMapFromFileWorker)ar.AsyncState;
                fileLoadWorker.EndInvoke(ar);
            }
            else if (ar.AsyncState is LoadMapFromStreamWorker)
            {
                //End
                LoadMapFromStreamWorker streamLoadWorker = (LoadMapFromStreamWorker)ar.AsyncState;
                streamLoadWorker.EndInvoke(ar);
            }
        }
        public void Reload()
        {
            //Close
            if (io.MapStream.CanRead || io.MapStream.CanWrite)
                io.CloseIO();

            //Load
            LoadMap(io);
        }
        public IAsyncResult BeginReload(AsyncCallback callback, object state)
        {
            //Create
            AsyncOperation reloadOperation = AsyncOperationManager.CreateOperation(state);
            ReloadMapWorker worker = new ReloadMapWorker(Reload);
            return worker.BeginInvoke(callback, worker);
        }
        public void EndReload(IAsyncResult ar)
        {
            //Get Worker...
            if (ar.AsyncState is ReloadMapWorker)
            {
                //End
                ReloadMapWorker reloadWorker = (ReloadMapWorker)ar.AsyncState;
                reloadWorker.EndInvoke(ar);
            }
        }
        /// <summary>
        /// Returns an index to an <see cref="IndexEntry"/> whose 32-bit identifier matches the supplied value.
        /// </summary>
        /// <param name="ident">The 32-bit identifier.</param>
        /// <returns>A zero-based index to an <see cref="IndexEntry"/> whose <see cref="IndexEntry.Ident"/> matches <paramref name="ident"/>, or -1 if no match is found.</returns>
        public int GetIndexByIdent(int ident)
        {
            return new List<IndexEntry>(entries).FindIndex(entry => entry.Ident == ident);
        }
        /// <summary>
        /// Returns an <see cref="IndexEntry"/> whose 32-bit identifier matches the supplied value.
        /// </summary>
        /// <param name="ident">The 32-bit identifier.</param>
        /// <returns>An <see cref="IndexEntry"/> whose <see cref="IndexEntry.Ident"/> matches <paramref name="ident"/>, or null if no match is found.</returns>
        public IndexEntry GetEntryByIdent(int ident)
        {
            return new List<IndexEntry>(entries).Find(entry => entry.Ident == ident);
        }
        /// <summary>
        /// Returns an <see cref="IndexEntry"/> whose full filename matches the supplied value.
        /// </summary>
        /// <param name="fileName">The full name of the <see cref="IndexEntry"/>.</param>
        /// <returns>An <see cref="IndexEntry"/> whose full name matches <paramref name="fileName"/>, or null if no match is found.</returns>
        public IndexEntry GetEntryByFilename(string fileName)
        {
            //Split
            string[] parts = fileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            //Check
            if (parts.Length == 2)
                return new List<IndexEntry>(entries).Find(e => e.FileName == parts[0] && e.Class == parts[1]);
            return null;
        }
        /// <summary>
        /// Returns a 32-bit string identifier using the specified string value.
        /// </summary>
        /// <param name="name">The name of the string that the identifer is to reference.</param>
        /// <returns>A 32-bit string identifier, or 0 if the map does not contain a string instance of <paramref name="name"/>.</returns>
        public int GetStringIDFromName(string name)
        {
            for (short i = 0; i < StringCount; i++)
            {
                string s = stringIDs[i];
                if (s == name)
                {
                    int loword = i;
                    int hiword = (s.Length << 8) << 16;
                    return loword | hiword;
                }
            }

            return 0;
        }
        public MemoryIO GetMetaBlock(out long position)
        {
            //Prepare
            MemoryIO meta = null;

            //Open?
            bool open = io.Open;
            if (!open)
                io.OpenIO();

            //Get Position
            position = header.IndexOffset + header.IndexLength;

            //Read
            io.Position = position;
            meta = new MemoryIO(io.In.ReadBytes(header.MetaLength));

            //Close?
            if (!open)
                io.CloseIO();

            //Return
            return meta;
        }
        /// <summary>
        /// Reloads the strings contained in the map using the <see cref="HaloIO"/> stream data.
        /// </summary>
        /// <param name="io">The <see cref="HaloIO"/> instance containing a map file.</param>
        public void LoadStringsInfo(HaloIO io)
        {
            //Prepare
            stringIndices = new int[header.StringCount];
            stringIDs = new string[header.StringCount];
            string128s = new string[header.StringCount];

            //Open?
            bool open = io.Open;
            if (!open)
                io.OpenIO();

            //Read String Index
            io.Position = header.StringsIndexOffset;
            for (int i = 0; i < header.StringCount; i++)
                stringIndices[i] = io.In.ReadInt32();

            //Read String IDs
            for (int i = 0; i < header.StringCount; i++)
            {
                io.Position = stringIndices[i] + header.StringsOffset;
                stringIDs[i] = io.In.ReadUTF8NullTerminated();
            }

            //Read String 128s
            for (int i = 0; i < header.StringCount; i++)
            {
                io.Position = (128 * i) + header.Strings128Offset;
                string128s[i] = io.In.ReadUTF8(128).Replace("/0", string.Empty);
            }

            //Close?
            if (!open)
                io.CloseIO();
        }
        /// <summary>
        /// Reloads the file names contained in the map using the <see cref="HaloIO"/> stream data.
        /// </summary>
        /// <param name="io">The <see cref="HaloIO"/> instance containing a map file.</param>
        public void LoadFileNamesInfo(HaloIO io)
        {
            //Prepare
            fileIndices = new int[header.FileCount];
            fileList = new string[header.FileCount];

            //Open?
            bool open = io.Open;
            if (!open)
                io.OpenIO();
            
            //Read Files Index
            io.Position = header.FilesIndex;
            for (int i = 0; i < header.FileCount; i++)
                fileIndices[i] = io.In.ReadInt32();

            //Read File List
            for (int i = 0; i < header.FileCount; i++)
            {
                io.Position = fileIndices[i] + header.FilesOffset;
                fileList[i] = io.In.ReadUTF8NullTerminated();
            }

            //Close?
            if (!open)
                io.CloseIO();
        }
        /// <summary>
        /// Reloads the tags contained in the map using the <see cref="HaloIO"/> stream data.
        /// </summary>
        /// <param name="io">The <see cref="HaloIO"/> instance containing a map file.</param>
        public void LoadTagsInfo(HaloIO io)
        {
            //Prepare
            tags = new TAGHIERARCHY[index.TagCount];
            tagAddresses = new int[index.TagCount];

            //Open?
            bool open = io.Open;
            if (!open)
                io.OpenIO();

            //Loop
            for (int i = 0; i < index.TagCount; i++)
            {
                tagAddresses[i] = header.IndexOffset + INDEX.Length + (i * TAGHIERARCHY.Length);
                io.Position = tagAddresses[i];
                tags[i] = io.In.ReadStructure<TAGHIERARCHY>();
            }

            //Close?
            if (!open)
                io.CloseIO();
        }
        /// <summary>
        /// Reloads the objects contained in the map using the <see cref="HaloIO"/> stream data.
        /// </summary>
        /// <param name="io">The <see cref="HaloIO"/> instance containing a map file.</param>
        public void LoadObjectInfo(HaloIO io)
        {
            //Prepare
            objectAddresses = new int[index.ObjectCount];
            objects = new OBJECT[index.ObjectCount];
            entries = new IndexEntry[index.ObjectCount];

            //Open?
            bool open = io.Open;
            if (!open)
                io.OpenIO();

            //Prepare
            StringBuilder fileName = null;

            //Determine Magic
            io.Position = ObjectStart + 8;    //Goto Globals Offset
            magic = io.In.ReadInt32() - (header.IndexOffset + header.IndexLength);

            //Loop
            for (int i = 0; i < index.ObjectCount; i++)
            {
                //Get Address
                objectAddresses[i] = ObjectStart + (i * 16);
                io.Position = objectAddresses[i];

                //Read
                objects[i] = io.In.ReadStructure<OBJECT>();

                //Get File Name
                fileName = new StringBuilder();
                if (header.FileCount > i) fileName.Append(fileList[i]);
                else fileName.AppendFormat("Unknown ({0})", i);

                //Create Index Entry
                if (objects[i].Offset != 0) entries[i] = new IndexEntry(this, objects[i], magic, fileName.ToString());
                else entries[i] = new IndexEntry(this, objects[i], fileName.ToString());
            }

            //Get Scenario and Globals
            scenario = GetEntryByIdent(index.ScenarioID);
            globals = GetEntryByIdent(index.GlobalsID);

            //Close?
            if (!open)
                IO.CloseIO();
        }
        /// <summary>
        /// Fixes any special index entries in the map using <see cref="HaloIO"/> stream data.
        /// </summary>
        /// <param name="io">The <see cref="HaloIO"/> instance containing a map file.</param>
        public void FixSpecialEntries(HaloIO io)
        {
            //Open?
            bool open = io.Open;
            if (!open)
                io.OpenIO();

            //Prepare
            uint sbspOffset;
            int sbspLength, sbspIdIndex, sbspId;
            int ltmpOffset, ltmpLength, ltmpId;
            uint bspMagic;

            //Obtain SBSP and LTMP data
            io.Position = scenario.Offset + 528;
            TAGBLOCK bsp = io.In.ReadUInt64();
            for (int i = 0; i < bsp.Count; i++)
            {
                //Go to Position
                io.Position = bsp.Translate(magic) + (i * 68);

                //Get SBSP Data
                sbspOffset = io.In.ReadUInt32();
                sbspLength = io.In.ReadInt32();
                bspMagic = io.In.ReadUInt32() - sbspOffset;
                io.Position += 8;
                sbspId = io.In.ReadInt32();

                //Set SBSP Entry
                if (sbspId != -1)
                {
                    sbspIdIndex = GetIndexByIdent(sbspId);
                    entries[sbspIdIndex] = IndexEntry.Create(this, entries[sbspIdIndex].FileName, entries[sbspIdIndex].Class, 
                        entries[sbspIdIndex].Ident, sbspOffset + bspMagic, (int)bspMagic, (uint)sbspLength);
                }

                //Get LTMP Ident
                io.Position = bsp.Translate(magic) + (i * 68) + 28;
                ltmpId = io.In.ReadInt32();
                io.Position = sbspOffset + 8;

                //Get LTMP Data
                ltmpOffset = 0;
                ltmpLength = 0;
                if (sbspOffset > 0)
                {
                    ltmpOffset = (int)(io.In.ReadInt32() - bspMagic);
                    ltmpLength = (int)(sbspLength + sbspOffset - ltmpOffset);
                }

                //Set LTMP Entry
                if (ltmpId != -1)
                {
                    int LtmpIdIndex = GetIndexByIdent(ltmpId);
                    entries[LtmpIdIndex] = IndexEntry.Create(this, entries[LtmpIdIndex].FileName, entries[LtmpIdIndex].Class, 
                        entries[LtmpIdIndex].Ident, (uint)ltmpOffset + bspMagic, (int)bspMagic, (uint)ltmpLength);
                }
            }

            //Close?
            if (!open)
                io.CloseIO();
        }
        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="IndexEntry"/> instances within the map.
        /// </summary>
        /// <returns>An enumerator for this instance.</returns>
        public IEnumerator<IndexEntry> GetEnumerator()
        {
            return ((IEnumerable<IndexEntry>)entries).GetEnumerator();
        }
        /// <summary>
        /// Determines whether this instance contains a specific index entry.
        /// </summary>
        /// <param name="item">The index entry to locate.</param>
        /// <returns>True if the map contains the specified entry, false if not.</returns>
        public bool Contains(IndexEntry item)
        {
            return ((ICollection<IndexEntry>)entries).Contains(item);
        }
        /// <summary>
        /// Copies the index entries of this instance to an <see cref="IndexEntry"/> array, starting at a particulare <see cref="Array"/> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="IndexEntry"/> array that is the destination of the copied entries.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        public void CopyTo(IndexEntry[] array, int arrayIndex)
        {
            ((ICollection<IndexEntry>)entries).CopyTo(array, arrayIndex);
        }

        internal virtual void OnObjectEdit(OBJECT entry)
        {
            //Get State
            bool ioState = io.Open;
            if (!ioState)
                io.OpenIO();

            //Write
            int index = Array.IndexOf(objects, entry);
            io.Position = objectAddresses[index];
            io.Out.Write(entry);
            
            //Close?
            if (!ioState)
                io.CloseIO();
        }
        internal virtual void OnIndexEdit(INDEX index)
        {
            //Get State
            bool ioState = io.Open;
            if (!ioState)
                io.OpenIO();

            //Write
            io.Position = header.IndexOffset;
            io.Out.Write(index);
            
            //Close?
            if (!ioState)
                io.CloseIO();
        }
        internal virtual void OnHeaderEdit(HEADER header)
        {
            //Get State
            bool ioState = io.Open;
            if (!ioState)
                io.OpenIO();

            //Write
            io.Position = 0;
            io.Out.Write(header);
            
            //Close?
            if (!ioState)
                io.CloseIO();
        }
        
        private delegate void LoadMapFromFileWorker(string fileLocation);
        private delegate void LoadMapFromStreamWorker(Stream stream);
        private delegate void ReloadMapWorker();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<IndexEntry>)entries).GetEnumerator();
        }

        void ICollection<IndexEntry>.Add(IndexEntry item)
        {
            throw new NotImplementedException();
        }
        void ICollection<IndexEntry>.Clear()
        {
            throw new NotImplementedException();
        }
        bool ICollection<IndexEntry>.Remove(IndexEntry item)
        {
            throw new NotImplementedException();
        }
    }
}