using Abide.HaloLibrary.Halo2Map;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Abide.HaloLibrary.Halo2Map.MapFile;

namespace Abide.HaloLibrary.Halo2VistaMap
{
    /// <summary>
    /// Represents a Halo 2 vista binary map file
    /// </summary>
    [Serializable]
    public sealed class MapFile : IDisposable
    {
        /// <summary>
        /// Represents the minimum length of a Halo 2 map file.
        /// This value is the sum of the length of a <see cref="Header"/> structure, and the minimum length of the index table.
        /// </summary>
        private const int MinLength = 6144;

        /// <summary>
        /// Gets or sets the Halo 2 map's name.
        /// </summary>
        [Category("Map Properties"), Description("The name of the Halo 2 map.")]
        public string Name
        {
            get { return header.Name; }
            set { header.Name = value; }
        }
        /// <summary>
        /// Gets and returns the Halo 2 map's build string.
        /// </summary>
        [Browsable(false)]
        public string Build
        {
            get { return header.Build; }
        }
        /// <summary>
        /// Gets and returns a list of the Halo 2 map's string IDs.
        /// </summary>
        [Category("Map Data"), Description("The Halo 2 map's strings.")]
        public StringList Strings { get; private set; }

        private Halo2Map.Header header;
        private Index index;
        
        /// <summary>
        /// Loads a Halo 2 map file from a specified file path.
        /// </summary>
        /// <param name="filename">A relative or absolute path for the map file.</param>
        /// <exception cref="ArgumentNullException"><paramref name="filename"/> is null.</exception>
        /// <exception cref="FileNotFoundException"><paramref name="filename"/> does not exist.</exception>
        /// <exception cref="MapFileExcption">Exception occured while loading the map.</exception>
        public void Load(string filename)
        {
            //Prepare
            FileStream fs = null;

            //Check...
            if (filename == null) throw new ArgumentNullException(nameof(filename));
            else if (!File.Exists(filename)) throw new FileNotFoundException("Unable to find the specified file.", filename);

            //Open Stream...?
            try { fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read); }
            catch (Exception ex) { throw new MapFileExcption(ex); }

            //Check
            if (fs != null) { Load(fs); fs.Close(); fs.Dispose(); }
        }
        /// <summary>
        /// Loads a Halo 2 map file from the specified stream.
        /// </summary>
        /// <param name="inStream">The stream containing the Halo 2 map.</param>
        /// <exception cref="ArgumentNullException"><paramref name="inStream"/> is null.</exception>
        /// <exception cref="ArgumentException">Exception occured while handling <paramref name="inStream"/>.</exception>
        /// <exception cref="MapFileExcption">Exception occured while loading map.</exception>
        public void Load(Stream inStream)
        {
            //Check
            if (inStream == null) throw new ArgumentNullException(nameof(inStream));
            if (!inStream.CanRead) throw new ArgumentException("Stream does not support reading.", nameof(inStream));
            if (!inStream.CanSeek) throw new ArgumentException("Stream does not support seeking.", nameof(inStream));

            //Check file...
            if (inStream.Length < MinLength)
                throw new MapFileExcption("Invalid map file.");

            try
            {
                using (BinaryReader reader = new BinaryReader(inStream, Encoding.UTF8, true))
                {
                    //Read Header
                    inStream.Seek(0, SeekOrigin.Begin);
                    header = reader.Read<Halo2Map.Header>();

                    //Check...
                    if (header.Head != HaloTags.head || header.Foot != HaloTags.foot)    //Quick sanity check...
                        throw new MapFileExcption("Invalid map header.");

                    //Read File Names
                    string[] files = reader.ReadUTF8StringTable(header.FileNamesOffset, header.FileNamesIndexOffset, (int)header.FileNameCount);

                    //Read Strings
                    Strings = new StringList(reader.ReadUTF8StringTable(header.StringsOffset, header.StringsIndexOffset, (int)header.StringCount));

                    //Read Index
                    inStream.Seek(header.IndexOffset, SeekOrigin.Begin);
                    index = reader.Read<Index>();
                }
            }
            catch { }
        }
        public void Save(string fileName)
        {
            throw new NotImplementedException();
        }
        public void Dispose()
        {
            return;
            throw new NotImplementedException();
        }
    }
}
