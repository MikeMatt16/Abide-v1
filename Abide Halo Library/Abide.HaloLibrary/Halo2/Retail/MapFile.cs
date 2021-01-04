using Abide.HaloLibrary.Halo2Map;
using Abide.HaloLibrary.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Abide.HaloLibrary.Halo2.Retail
{
    /// <summary>
    /// Represents a Halo 2 map file.
    /// </summary>
    public sealed class MapFile : IDisposable
    {
        private VirtualStream tagData = null;

        /// <summary>
        /// Gets and returns whether or not this instance is disposed of.
        /// </summary>
        public bool IsDisposed { get; private set; }
        /// <summary>
        /// Gets or sets the name of the map.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Gets and returns the Halo map's index entry list.
        /// </summary>
        public IndexEntryList IndexEntries { get; private set; } = null;
        /// <summary>
        /// Gets and returns the Halo map's crazy data.
        /// </summary>
        public HaloMapDataContainer Crazy { get; private set; } = new HaloMapDataContainer();
        /// <summary>
        /// Gets and returns the Halo map's string list.
        /// </summary>
        public StringList Strings { get; } = new StringList();
        /// <summary>
        /// Gets and returns the Halo map's tag list.
        /// </summary>
        public TagList Tags { get; } = new TagList();
        /// <summary>
        /// Gets or sets the Halo map's scenario tag.
        /// </summary>
        public IndexEntry Scenario { get; set; } = null;
        /// <summary>
        /// Gets or sets the Halo map's globals tag.
        /// </summary>
        public IndexEntry Globals { get; set; } = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapFile"/> class using the specified map file name.
        /// </summary>
        /// <param name="fileName">The path of the Halo 2 map file.</param>
        public MapFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentException("Invalid file name.", nameof(fileName));
            if (!File.Exists(fileName)) throw new FileNotFoundException("Unable to find specified file.", fileName);

            using (var fs = File.OpenRead(fileName))
                Load(fs);
        }
        /// <summary>
        /// Creates a new instance of the <see cref="MapFile"/> class using the specified map stream.
        /// </summary>
        /// <param name="inStream">The stream containing the Halo 2 map to load</param>
        public MapFile(Stream inStream)
        {
            IndexEntries = new IndexEntryList();
            Load(inStream);
        }
        /// <summary>
        /// Loads the Halo 2 map file from the specified stream.
        /// </summary>
        /// <param name="inStream">The stream containing the Halo 2 map to load.</param>
        private void Load(Stream inStream)
        {
            if (inStream == null) throw new ArgumentNullException(nameof(inStream));
            if (!inStream.CanRead) throw new ArgumentException("Stream must be readable.", nameof(inStream));
            if (!inStream.CanSeek) throw new ArgumentException("Stream must be seekable.", nameof(inStream));

            using (BinaryReader reader = new BinaryReader(inStream))
            {
                Header header = reader.Read<Header>();
                string[] fileNames = reader.ReadUTF8StringTable(header.FileNamesOffset, header.FileNamesIndexOffset, (int)header.FileNameCount);
                string[] strings = reader.ReadUTF8StringTable(header.StringsOffset, header.StringsIndexOffset, (int)header.StringCount);

                inStream.Seek(header.IndexOffset, SeekOrigin.Begin);
                Index index = reader.Read<Index>();

                Tags.Clear();
                for (int i = 0; i < index.TagCount; i++)
                {
                    Tags.Add(reader.Read<TagHierarchy>());
                }

                IndexEntry[] indexEntries = new IndexEntry[index.ObjectCount];
                for (int i = 0; i < index.ObjectCount; i++)
                {
                    var objectEntry = reader.Read<ObjectEntry>();
                    indexEntries[i] = new IndexEntry()
                    {
                        Id = objectEntry.Id,
                        Address = objectEntry.Offset,
                        Length = objectEntry.Size,
                        Tag = Tags[objectEntry.Tag],
                    };

                    if (i < fileNames.Length)
                    {
                        indexEntries[i].Filename = fileNames[i];
                    }
                }

                if (indexEntries.Length > 0)
                {
                    IndexEntries?.Dispose();
                    IndexEntries = new IndexEntryList(indexEntries);
                }

                Name = header.Name;
            }
        }
        /// <summary>
        /// Releases all resources used by the <see cref="MapFile"/> instance.
        /// </summary>
        public void Dispose()
        {
            if (IsDisposed) return;
            IsDisposed = true;

            foreach (var item in IndexEntries)
                item.Dispose();
            IndexEntries.Clear();

            Name = string.Empty;
            Crazy.Clear();
            Strings.Clear();
            Tags.Clear();
            Scenario = null;
            Globals = null;
        }
    }
}
