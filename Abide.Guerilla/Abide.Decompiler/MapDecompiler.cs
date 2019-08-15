using Abide.Guerilla.Library;
using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using Abide.HaloLibrary.IO;
using Abide.Tag;
using Abide.Tag.Cache.Generated;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Convert = Abide.Guerilla.Library.Convert;

namespace Abide.Decompiler
{
    /// <summary>
    /// Represents a Halo 2 cache map file decompiler.
    /// </summary>
    public sealed class MapDecompiler : IDisposable
    {
        /// <summary>
        /// Gets and returns the map used by the decompiler.
        /// </summary>
        public MapFile Map { get; }
        /// <summary>
        /// Gets and returns the output directory.
        /// </summary>
        public string OutputDirectory { get; }
        /// <summary>
        /// Gets and returns the decompiler host.
        /// </summary>
        public IDecompileHost Host { get; set; }
        
        private volatile bool m_IsDecompiling = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapDecompiler"/> class using the specified map file and output directory.
        /// </summary>
        /// <param name="map">The map file.</param>
        /// <param name="outputDirectory">The output directory.</param>
        public MapDecompiler(MapFile map, string outputDirectory)
        {
            //Setup
            Map = map ?? throw new ArgumentNullException(nameof(map));
            OutputDirectory = outputDirectory ?? throw new ArgumentNullException(nameof(outputDirectory));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MapDecompiler"/> class using the specified host, map file name and output directory.
        /// </summary>
        /// <param name="mapFileName">The map file name.</param>
        /// <param name="outputDirectory">The output directory.</param>
        public MapDecompiler(string mapFileName, string outputDirectory)
        {
            //Setup
            OutputDirectory = outputDirectory ?? throw new ArgumentNullException(nameof(outputDirectory));

            //Load
            using (FileStream fs = new FileStream(mapFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                //Load
                Map = new MapFile();
                Map.Load(fs);
            }
        }

        public void Start()
        {
            //Check
            if (m_IsDecompiling) return;
            DirectoryInfo targetDirectory = null;

            //Create folder
            try { targetDirectory = Directory.CreateDirectory(OutputDirectory); }
            catch(Exception ex) { throw new InvalidOperationException("Unable to create directory for output files.", ex); }

            //Start
            m_IsDecompiling = true;
            ThreadPool.QueueUserWorkItem(Map_Decompile, null);
        }

        private void Map_Decompile(object state)
        {
            //Prepare
            ITagGroup tagGroup = null, guerillaTagGroup = null;
            IndexEntry soundGestalt = null;
            SoundCacheFileGestalt soundCacheFileGestalt = null;
            string tagGroupFileName = string.Empty;
            string localFileName = string.Empty;

            //Begin
            Console.WriteLine($"Beginning decompilation of {Map.Name}.");

            //Get sound gestalt
            IndexEntry globals = Map.Globals;
            if (globals.Root == HaloTags.matg)
                using (BinaryReader tagReader = globals.TagData.CreateReader())
                {
                    //Goto
                    globals.TagData.Seek((uint)globals.PostProcessedOffset, SeekOrigin.Begin);

                    //Read
                    ITagGroup globalsTagBlock = TagLookup.CreateTagGroup(globals.Root);
                    globalsTagBlock.Read(tagReader);

                    //Get sound globals
                    BlockField soundGlobalsTagBlock = (BlockField)globalsTagBlock[0].Fields[4];
                    if (soundGlobalsTagBlock.BlockList.Count > 0)
                    {
                        //Get sound gestalt index
                        TagId soundGestaltId = (TagId)soundGlobalsTagBlock.BlockList[0].Fields[4].Value;

                        //Get index entry
                        soundGestalt = Map.IndexEntries[soundGestaltId];

                        //Null sound gestalt
                        soundGlobalsTagBlock.BlockList[0].Fields[4].Value = (int)TagId.Null;
                    }
                }

            //Check
            if (soundGestalt != null)
                using (BinaryReader reader = soundGestalt.TagData.CreateReader())
                {
                    //Prepare
                    soundCacheFileGestalt = new SoundCacheFileGestalt();

                    //Goto
                    soundGestalt.TagData.Seek((uint)soundGestalt.PostProcessedOffset, SeekOrigin.Begin);
                    soundCacheFileGestalt.Read(reader);

                    //Found
                    Console.WriteLine($"Found {soundCacheFileGestalt.Name} ({soundGestalt.Filename}.{soundGestalt.Root}).");
                }

            //Loop
            for (int i = 0; i < Map.IndexEntries.Count; i++)
                if ((tagGroup = TagLookup.CreateTagGroup(Map.IndexEntries[i].Root)) != null)    //Lookup cache definition
                {
                    //Read tag
                    using (BinaryReader reader = Map.IndexEntries[i].TagData.CreateReader())
                    {
                        //Decompile
                        Console.WriteLine($"Reading cache tag {Map.IndexEntries[i].Filename}.{Map.IndexEntries[i].Root}...");
                        if (Map.IndexEntries[i].Root == HaloTags.tdtl) System.Diagnostics.Debugger.Break();

                        //Goto
                        reader.BaseStream.Seek((uint)Map.IndexEntries[i].PostProcessedOffset, SeekOrigin.Begin);
                        tagGroup.Read(reader);

                        //Convert
                        guerillaTagGroup = Convert.ToGuerilla(tagGroup, soundCacheFileGestalt, Map.IndexEntries[i], Map);
                    }
                    
                    //Get file name
                    localFileName = Path.Combine($"{Map.IndexEntries[i].Filename}.{guerillaTagGroup.Name}");
                    tagGroupFileName = Path.Combine(OutputDirectory, localFileName);

                    //Create Directory
                    if (!Directory.Exists(Path.GetDirectoryName(tagGroupFileName)))
                        Directory.CreateDirectory(Path.GetDirectoryName(tagGroupFileName));

                    //Write
                    TagGroupHeader header = new TagGroupHeader();
                    using (FileStream fs = new FileStream(tagGroupFileName, FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
                    using (BinaryWriter writer = new BinaryWriter(fs))
                    using (BinaryReader reader = new BinaryReader(fs))
                    {
                        //Write
                        Console.WriteLine($"Writing guerilla tag {tagGroupFileName}.");

                        //Write
                        fs.Seek(TagGroupHeader.Size, SeekOrigin.Begin);
                        guerillaTagGroup.Write(writer);

                        //Create raws
                        if (guerillaTagGroup.GroupTag == HaloTags.snd_) TagGroup_CreateRaws(guerillaTagGroup, soundGestalt, writer, ref header);
                        else TagGroup_CreateRaws(guerillaTagGroup, Map.IndexEntries[i], writer, ref header);

                        //Setup header
                        header.Checksum = (uint)TagGroup_CalculateChecksum(guerillaTagGroup);
                        header.GroupTag = guerillaTagGroup.GroupTag.FourCc;
                        header.Id = Map.IndexEntries[i].Id.Dword;
                        header.AbideTag = "atag";

                        //Write Header
                        fs.Seek(0, SeekOrigin.Begin);
                        writer.Write(header);
                    }

                    //Report
                    if (Host != null) Host.Report(i / (float)Map.IndexEntries.Count);
                }

            //Set
            m_IsDecompiling = false;

            //Collect
            GC.Collect();

            //Complete
            if (Host != null) Host.Complete();
        }
        
        private int TagGroup_CalculateChecksum(ITagGroup tagGroup)
        {
            //Prepare
            int checksum = 0;

            //Create virtual stream
            using (VirtualStream tagStream = new VirtualStream(TagGroupHeader.Size))
            using (BinaryWriter writer = new BinaryWriter(tagStream))
            {
                //Write
                tagGroup.Write(writer);
                tagStream.Align(4);

                //Check
                if (tagStream.Length == 0) return 0;

                //Get buffer
                byte[] tagGroupBuffer = tagStream.ToArray();
                checksum = BitConverter.ToInt32(tagGroupBuffer, 0);
                for (int i = 1; i < tagGroupBuffer.Length / 4; i++)
                    checksum ^= BitConverter.ToInt32(tagGroupBuffer, i * 4);
            }

            //Return
            return checksum;
        }
        
        private void TagGroup_CreateRaws(ITagGroup tagGroup, IndexEntry entry, BinaryWriter writer, ref TagGroupHeader header)
        {
            //Prepare
            List<int> rawOffsets = new List<int>();
            List<byte[]> rawDatas = new List<byte[]>();

            //Check
            if (tagGroup.GroupTag == HaloTags.snd_)
            {
                //Loop through pitch ranges
                foreach (ITagBlock pitchRange in ((BlockField)tagGroup[0].Fields[13]).BlockList)
                {
                    //Loop through permutations
                    foreach (ITagBlock permutation in ((BlockField)pitchRange.Fields[7]).BlockList)
                    {
                        //Loop through chunks
                        foreach (ITagBlock chunk in ((BlockField)permutation.Fields[6]).BlockList)
                        {
                            int address = (int)chunk.Fields[0].Value;
                            if(entry.Raws[RawSection.Sound].ContainsRawOffset(address))
                            {
                                header.RawsCount++;
                                rawOffsets.Add(address);
                                rawDatas.Add(entry.Raws[RawSection.Sound][address].ToArray());
                            }
                        }
                    }
                }

                //Loop through extra infos
                foreach (ITagBlock extraInfo in ((BlockField)tagGroup[0].Fields[15]).BlockList)
                {
                    ITagBlock geometryInfo = (ITagBlock)extraInfo.Fields[2].Value;
                    int address = (int)geometryInfo.Fields[1].Value;
                    if (entry.Raws[RawSection.LipSync].ContainsRawOffset(address))
                    {
                        header.RawsCount++;
                        rawOffsets.Add(address);
                        rawDatas.Add(entry.Raws[RawSection.LipSync][address].ToArray());
                    }
                }
            }
            else
            {
                //Compile raws
                foreach (RawSection section in Enum.GetValues(typeof(RawSection)))
                {
                    header.RawsCount += (uint)entry.Raws[section].Count();
                    rawOffsets.AddRange(entry.Raws[section].Select(s => s.RawOffset));
                    rawDatas.AddRange(entry.Raws[section].Select(s => s.ToArray()));
                }
            }

            //Check
            if (header.RawsCount > 0)
            {
                //Write Offsets
                header.RawOffsetsOffset = (uint)writer.BaseStream.Position;
                for (int i = 0; i < header.RawsCount; i++)
                    writer.Write(rawOffsets[i]);

                //Write Lengths
                header.RawLengthsOffset = (uint)writer.BaseStream.Position;
                for (int i = 0; i < header.RawsCount; i++)
                    writer.Write(rawDatas[i].Length);

                //Write Data
                header.RawDataOffset = (uint)writer.BaseStream.Position;
                for (int i = 0; i < header.RawsCount; i++)
                    writer.Write(rawDatas[i]);
            }
        }

        public void Dispose()
        {
            //Dispose
            if (Map != null) Map.Dispose();
        }
    }
}
