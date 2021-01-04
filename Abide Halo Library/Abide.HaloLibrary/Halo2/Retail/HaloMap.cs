using Abide.HaloLibrary.Halo2Map;
using Abide.HaloLibrary.IO;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Abide.HaloLibrary.Halo2.Retail
{
    /// <summary>
    /// Represents a Halo 2 retail map instance.
    /// </summary>
    [Serializable]
    public sealed class HaloMap : IDisposable
    {
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
        public IndexEntryList IndexEntries { get; } = new IndexEntryList();
        /// <summary>
        /// Gets and returns the Halo map's crazy data.
        /// </summary>
        public HaloMapDataContainer Crazy { get; } = new HaloMapDataContainer();
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
        /// 
        /// </summary>
        /// <param name="inStream"></param>
        public HaloMap(Stream inStream)
        {
            //Check
            if (inStream == null) throw new ArgumentNullException(nameof(inStream));
            if (!inStream.CanSeek) throw new ArgumentException("Stream not seekable.");
            if (!inStream.CanRead) throw new ArgumentException("Stream not readable.");

            //Prepare
            StringEntry[] en = null, jp = null, nl = null, fr = null, es = null, it = null, kr = null, zh = null, pr = null;
            Index mapIndex = new Index();
            TagHierarchy[] tags = null;
            ObjectEntry[] objects = null;

            //Sanity check
            if (inStream.Length < Header.Length) throw new InvalidOperationException("Invalid file.");

            //Create binary reader
            using (BinaryReader reader = new BinaryReader(inStream, Encoding.UTF8))
            {
                //Read header
                inStream.Seek(0, SeekOrigin.Begin);
                Header mapHeader = reader.Read<Header>();

                //Check header
                if (mapHeader.Head != "head" || mapHeader.Foot != "foot" || mapHeader.Version != 8 || mapHeader.FileLength != inStream.Length)
                    throw new InvalidOperationException("Invalid map header.");

                //Read crazy
                inStream.Seek(mapHeader.CrazyOffset, SeekOrigin.Begin);
                Crazy.SetBuffer(reader.ReadBytes((int)mapHeader.CrazyLength));

                //Read file names
                string[] filenames = reader.ReadUTF8StringTable(mapHeader.FileNamesOffset, mapHeader.FileNamesIndexOffset,
                    (int)mapHeader.FileNameCount);

                //Read string names
                string[] strings = reader.ReadUTF8StringTable(mapHeader.StringsOffset, mapHeader.StringsIndexOffset,
                    (int)mapHeader.StringCount);

                //Read index
                inStream.Seek(mapHeader.IndexOffset, SeekOrigin.Begin);
                using (VirtualStream indexStream = new VirtualStream(Index.IndexVirtualAddress, reader.ReadBytes((int)mapHeader.IndexLength)))
                using (BinaryReader indexReader = new BinaryReader(indexStream, Encoding.UTF8))
                {
                    //Read index header
                    mapIndex = indexReader.Read<Index>();

                    //Read tags
                    indexStream.Seek(mapIndex.TagsAddress, SeekOrigin.Begin);
                    tags = new TagHierarchy[mapIndex.TagCount];
                    for (int i = 0; i < mapIndex.TagCount; i++)
                        tags[i] = indexReader.Read<TagHierarchy>();

                    //Read entries
                    indexStream.Seek(mapIndex.ObjectsOffset, SeekOrigin.Begin);
                    objects = new ObjectEntry[mapIndex.ObjectCount];
                    for (int i = 0; i < mapIndex.ObjectCount; i++)
                        objects[i] = indexReader.Read<ObjectEntry>();
                }

                //Create index entries
                IndexEntry[] indexEntries = new IndexEntry[mapIndex.ObjectCount];
                for (int i = 0; i < mapIndex.ObjectCount; i++)
                {
                    //Get tag filename
                    string tagFilename = $"0x{objects[i].Id}";
                    if (filenames.Length > i) tagFilename = filenames[i];

                    //Get tag heirarchy
                    TagHierarchy tag = tags.First(t => t.Root == objects[i].Tag);

                    //Setup entry
                    indexEntries[i] = new IndexEntry()
                    {
                        Tag = tag,
                        Filename = tagFilename,
                        Address = objects[i].Offset,
                        Length = objects[i].Size,
                        Id = objects[i].Id,
                    };
                }

                //Get virtual offset of tag data start
                long bspLength = mapHeader.MapDataLength - (mapHeader.IndexLength + Index.Length + mapHeader.TagDataLength);
                long tagDataAddress = mapIndex.TagsAddress + bspLength + mapHeader.IndexLength;

                //Read tag data
                HaloMapDataContainer tagData = new HaloMapDataContainer();
                inStream.Seek(mapHeader.IndexOffset + mapHeader.IndexLength, SeekOrigin.Begin);
                tagData.SetBuffer(tagDataAddress, reader.ReadBytes((int)mapHeader.TagDataLength));

                //Set map name
                Name = mapHeader.Name;

                //Set entry data
                using (BinaryReader tagReader = tagData.GetVirtualStream().CreateReader())
                    foreach (var entry in indexEntries)
                    {
                        //Check
                        if (entry.Address > 0)
                        {
                            //Goto tag group
                            tagReader.BaseStream.Seek(entry.Address, SeekOrigin.Begin);

                            //Read tag group
                            using (var tagGroup = Tag.Generated.TagLookup.CreateTagGroup(entry.Root))
                            {
                                tagGroup.Read(tagReader);

                                //Write to new virtual stream
                                using (VirtualStream tagStream = new VirtualStream(entry.Address))
                                using (BinaryWriter tagWriter = new BinaryWriter(tagStream))
                                {
                                    //Write tag group
                                    tagGroup.Write(tagWriter);

                                    //Set data
                                    entry.Data = new HaloMapDataContainer();
                                    entry.Data.SetBuffer(entry.Address, tagStream.ToArray());
                                    entry.Length = entry.Data.Length;
                                }
                            }

                            if (entry.Data.Length == 0) System.Diagnostics.Debugger.Break();
                        }
                        else
                        {
                            //Set data
                            entry.Data = new HaloMapDataContainer();
                            entry.Data.SetBuffer(entry.Address, new byte[0]);
                        }
                    }

                //Find resources
                Scenario = indexEntries.First(e => e.Id == mapIndex.ScenarioId);
                Globals = indexEntries.First(e => e.Id == mapIndex.GlobalsId);
                IndexEntry soundGestalt = null;

                //Create tag data reader
                using (BinaryReader tagReader = tagData.GetVirtualStream().CreateReader())
                {
                    //Read unicode string tables
                    ReadUnicodeStringTable(Globals, inStream, reader, tagReader, 400, strings, out en);
                    ReadUnicodeStringTable(Globals, inStream, reader, tagReader, 428, strings, out jp);
                    ReadUnicodeStringTable(Globals, inStream, reader, tagReader, 456, strings, out nl);
                    ReadUnicodeStringTable(Globals, inStream, reader, tagReader, 484, strings, out fr);
                    ReadUnicodeStringTable(Globals, inStream, reader, tagReader, 512, strings, out es);
                    ReadUnicodeStringTable(Globals, inStream, reader, tagReader, 540, strings, out it);
                    ReadUnicodeStringTable(Globals, inStream, reader, tagReader, 568, strings, out kr);
                    ReadUnicodeStringTable(Globals, inStream, reader, tagReader, 596, strings, out zh);
                    ReadUnicodeStringTable(Globals, inStream, reader, tagReader, 624, strings, out pr);

                    //Goto sound globals block in globals tag
                    tagReader.BaseStream.Seek(Globals.Address + 192, SeekOrigin.Begin);
                    TagBlock soundGlobals = (TagBlock)tagReader.ReadUInt64();
                    if (soundGlobals.Count > 0) //Check tag block count
                    {
                        //Goto sound resources ID
                        tagReader.BaseStream.Seek(soundGlobals.Offset + 32, SeekOrigin.Begin);
                        TagId resourcesId = tagReader.ReadUInt32();

                        //Get sound gestalt
                        soundGestalt = indexEntries.First(e => e.Id == resourcesId);
                    }

                    //Goto structure BSPs in scenario tag
                    tagReader.BaseStream.Seek(Scenario.Address + 528, SeekOrigin.Begin);
                    TagBlock structureBsps = (TagBlock)tagReader.ReadUInt64();

                    //Loop through each tag block
                    for (int i = 0; i < structureBsps.Count; i++)
                    {
                        //Prepare
                        HaloMapDataContainer bspData = new HaloMapDataContainer();

                        //Goto structure BSP block
                        tagReader.BaseStream.Seek(structureBsps.Offset + (i * 68), SeekOrigin.Begin);

                        //Read scenario structure bsp tag block data
                        long structureBspOffset = tagReader.ReadUInt32();
                        long structureBspLength = tagReader.ReadUInt32();
                        long bspMemoryAddress = tagReader.ReadUInt32();
                        tagReader.ReadBytes(4);
                        TagFourCc sbspTag = tagReader.Read<TagFourCc>();
                        TagId sbspId = tagReader.ReadUInt32();
                        TagFourCc ltmpTag = tagReader.Read<TagFourCc>();
                        TagId ltmpId = tagReader.ReadUInt32();

                        //Check
                        bool hasSbsp = indexEntries.Any(e => e.Id == sbspId && e.Root == sbspTag);
                        bool hasltmp = indexEntries.Any(e => e.Id == ltmpId && e.Root == ltmpTag);

                        //Get scenario structure tags
                        IndexEntry structureBsp = hasSbsp ? indexEntries.First(e => e.Id == sbspId && e.Root == sbspTag) : null;
                        IndexEntry structureLightmap = hasltmp ? indexEntries.First(e => e.Id == ltmpId && e.Root == ltmpTag) : null;

                        //Goto BSP data start
                        inStream.Seek(structureBspOffset, SeekOrigin.Begin);
                        int sbspLength = reader.ReadInt32();
                        long sbspAddress = reader.ReadUInt32();
                        long ltmpAddress = reader.ReadUInt32();
                        TagFourCc bspTag = reader.Read<TagFourCc>();

                        //Check
                        if (ltmpAddress == 0) ltmpAddress = sbspAddress + sbspLength;

                        //Check tag
                        if (bspTag == HaloTags.sbsp)
                        {
                            //Read data
                            inStream.Seek(structureBspOffset, SeekOrigin.Begin);
                            bspData.SetBuffer(bspMemoryAddress, reader.ReadBytes(sbspLength));

                            //Open 
                            using (BinaryReader bspReader = bspData.GetVirtualStream().CreateReader())
                            {
                                //Check sbsp
                                if (hasSbsp)
                                {
                                    //Setup tag entry
                                    structureBsp.Address = sbspAddress;
                                    structureBsp.Length = ltmpAddress - sbspAddress;

                                    //Goto tag group
                                    bspReader.BaseStream.Seek(sbspAddress, SeekOrigin.Begin);

                                    //Read tag group
                                    var tagGroup = new Tag.Generated.ScenarioStructureBsp();
                                    tagGroup.Read(bspReader);

                                    //Write to new virtual stream
                                    using (VirtualStream tagStream = new VirtualStream(sbspAddress))
                                    using (BinaryWriter tagWriter = new BinaryWriter(tagStream))
                                    {
                                        //Write tag group
                                        tagGroup.Write(tagWriter);

                                        //Set data
                                        structureBsp.Data = new HaloMapDataContainer();
                                        structureBsp.Data.SetBuffer(sbspAddress, tagStream.ToArray());
                                        structureBsp.Length = structureBsp.Data.Length;
                                    }
                                }

                                //Check ltmp
                                if (hasltmp)
                                {
                                    //Setup tag entry
                                    structureLightmap.Address = ltmpAddress;
                                    structureLightmap.Length = (sbspAddress + sbspLength) - ltmpAddress;

                                    //Goto tag group
                                    bspReader.BaseStream.Seek(ltmpAddress, SeekOrigin.Begin);

                                    //Read tag group
                                    var tagGroup = new Tag.Generated.ScenarioStructureLightmap();
                                    tagGroup.Read(bspReader);

                                    //Write to new virtual stream
                                    using (VirtualStream tagStream = new VirtualStream(ltmpAddress))
                                    using (BinaryWriter tagWriter = new BinaryWriter(tagStream))
                                    {
                                        //Write tag group
                                        tagGroup.Write(tagWriter);

                                        //Set data
                                        structureLightmap.Data = new HaloMapDataContainer();
                                        structureLightmap.Data.SetBuffer(ltmpAddress, tagStream.ToArray());
                                        structureLightmap.Length = structureLightmap.Data.Length;
                                    }
                                }
                            }
                        }
                        else bspData.Dispose();
                    }
                }

                //Read tag resources
                foreach (IndexEntry entry in indexEntries)
                {
                    //Check
                    switch (entry.Root)
                    {
                        case "ugh!": ReadSoundData(entry, inStream, reader); break;
                        case "mode": ReadRenderModelData(entry, inStream, reader); break;
                        case "sbsp": ReadScenarioStructureBspData(entry, inStream, reader); break;
                        case "ltmp": ReadScenarioStructureLightmapData(entry, inStream, reader); break;
                        case "weat": ReadWeatherSystemData(entry, inStream, reader); break;
                        case "DECR": ReadDecoratorSetData(entry, inStream, reader); break;
                        case "PRTM": ReadParticleModelData(entry, inStream, reader); break;
                        case "jmad": ReadModelAnimationData(entry, inStream, reader); break;
                        case "bitm": ReadBitmapData(entry, inStream, reader); break;
                        case "unic":
                            int offset, count;
                            using (BinaryReader tagReader = entry.Data.GetVirtualStream().CreateReader())
                            {
                                tagReader.BaseStream.Seek(entry.Address + 16, SeekOrigin.Begin);
                                offset = tagReader.ReadUInt16();
                                count = tagReader.ReadUInt16();
                                entry.Strings.English.AddRange(en.Where((s, i) => i >= offset && i < offset + count));
                                offset = tagReader.ReadUInt16();
                                count = tagReader.ReadUInt16();
                                entry.Strings.Japanese.AddRange(jp.Where((s, i) => i >= offset && i < offset + count));
                                offset = tagReader.ReadUInt16();
                                count = tagReader.ReadUInt16();
                                entry.Strings.German.AddRange(nl.Where((s, i) => i >= offset && i < offset + count));
                                offset = tagReader.ReadUInt16();
                                count = tagReader.ReadUInt16();
                                entry.Strings.French.AddRange(fr.Where((s, i) => i >= offset && i < offset + count));
                                offset = tagReader.ReadUInt16();
                                count = tagReader.ReadUInt16();
                                entry.Strings.Spanish.AddRange(es.Where((s, i) => i >= offset && i < offset + count));
                                offset = tagReader.ReadUInt16();
                                count = tagReader.ReadUInt16();
                                entry.Strings.Italian.AddRange(it.Where((s, i) => i >= offset && i < offset + count));
                                offset = tagReader.ReadUInt16();
                                count = tagReader.ReadUInt16();
                                entry.Strings.Korean.AddRange(kr.Where((s, i) => i >= offset && i < offset + count));
                                offset = tagReader.ReadUInt16();
                                count = tagReader.ReadUInt16();
                                entry.Strings.Chinese.AddRange(zh.Where((s, i) => i >= offset && i < offset + count));
                                offset = tagReader.ReadUInt16();
                                count = tagReader.ReadUInt16();
                                entry.Strings.Portuguese.AddRange(pr.Where((s, i) => i >= offset && i < offset + count));
                            }
                            break;
                    }
                }

                //Setup entry list
                IndexEntries = new IndexEntryList(indexEntries);

                //Setup strings list
                Strings = new StringList(strings);

                //Setup tags list
                Tags = new TagList(tags);
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="HaloMap"/> class from the specified file.
        /// </summary>
        /// <param name="filename">Tha Halo map file name and path.</param>
        /// <exception cref="ArgumentException"><paramref name="filename"/> is null.</exception>
        /// <exception cref="FileNotFoundException">The Halo map file is not found.</exception>
        /// <exception cref="InvalidOperationException">The Halo map file is invalid.</exception>
        public HaloMap(string filename) : this(File.OpenRead(filename)) { }
        /// <summary>
        /// Saves this <see cref="HaloMap"/> to the specified stream.
        /// </summary>
        /// <param name="outStream"></param>
        /// <exception cref="ArgumentNullException"><paramref name="outStream"/> is <see langword="null"/>.</exception>
        public void Save(Stream outStream)
        {
            //Check
            if (outStream == null) throw new ArgumentNullException(nameof(outStream));
            if (!outStream.CanSeek) throw new ArgumentException("Stream not seekable.", nameof(outStream));
            if (!outStream.CanRead) throw new ArgumentException("Stream not readable.", nameof(outStream));
            if (!outStream.CanWrite) throw new ArgumentException("Stream not writable.", nameof(outStream));

            //Setup scenario simulation definition table
            using (BinaryReader tagReader = Scenario.Data.GetVirtualStream().CreateReader())
            {
                var scenario = Tag.Generated.TagLookup.CreateTagGroup(Scenario.Root);
                scenario.Read(tagReader);

                Tag.Block tableBlock = null;
                var simulationDefintionTable = scenario.TagBlocks[0].Fields[143] as Tag.BlockField;
                simulationDefintionTable.BlockList.Clear();

                foreach (var entry in IndexEntries)
                {
                    switch (entry.Root)
                    {
                        case "bipd":
                        case "bloc":
                        case "ctrl":
                        case "jpt!":
                        case "mach":
                        case "scen":
                        case "ssce":
                        case "vehi":
                            tableBlock = new Tag.Generated.ScenarioSimulationDefinitionTableBlock();
                            tableBlock.Fields[0].Value = entry.Id;
                            simulationDefintionTable.BlockList.Add(tableBlock);
                            break;
                        case "eqip":
                        case "garb":
                        case "proj":
                            tableBlock = new Tag.Generated.ScenarioSimulationDefinitionTableBlock();
                            tableBlock.Fields[0].Value = entry.Id;
                            simulationDefintionTable.BlockList.Add(tableBlock);
                            tableBlock = new Tag.Generated.ScenarioSimulationDefinitionTableBlock();
                            tableBlock.Fields[0].Value = entry.Id;
                            simulationDefintionTable.BlockList.Add(tableBlock);
                            break;
                        case "weap":
                            tableBlock = new Tag.Generated.ScenarioSimulationDefinitionTableBlock();
                            tableBlock.Fields[0].Value = entry.Id;
                            simulationDefintionTable.BlockList.Add(tableBlock);
                            tableBlock = new Tag.Generated.ScenarioSimulationDefinitionTableBlock();
                            tableBlock.Fields[0].Value = entry.Id;
                            simulationDefintionTable.BlockList.Add(tableBlock);
                            tableBlock = new Tag.Generated.ScenarioSimulationDefinitionTableBlock();
                            tableBlock.Fields[0].Value = entry.Id;
                            simulationDefintionTable.BlockList.Add(tableBlock);
                            break;
                    }
                }

                using (var stream = new VirtualStream(Scenario.Address))
                using (var writer = stream.CreateWriter())
                {
                    scenario.Write(writer);
                    Scenario.Data.SetBuffer(Scenario.Address, stream.ToArray());
                }
            }

            //Prepare
            Header mapHeader = Header.CreateDefault();
            Index mapIndex = Index.CreateDefault();

            //Create file
            using (BinaryReader reader = new BinaryReader(outStream, Encoding.UTF8))
            using (BinaryWriter writer = new BinaryWriter(outStream, Encoding.UTF8))
            {
                //Setup header
                mapHeader.Name = Name;
                mapHeader.ScenarioPath = Scenario.Filename;

                //Skip header
                outStream.Seek(Header.Length, SeekOrigin.Begin);

                //Write first-pass tag resources
                foreach (IndexEntry entry in IndexEntries.Where(e => e.Root == "ugh!"))
                    WriteSoundGestaltSoundData(entry, outStream, writer);
                foreach (IndexEntry entry in IndexEntries.Where(e => e.Root == "mode"))
                    WriteRenderModelData(entry, outStream, writer);
                foreach (IndexEntry entry in IndexEntries.Where(e => e.Root == "sbsp"))
                    WriteScenarioStructureBspData(entry, outStream, writer);
                foreach (IndexEntry entry in IndexEntries.Where(e => e.Root == "ltmp"))
                    WriteScenarioStructureLightmapData(entry, outStream, writer);
                foreach (IndexEntry entry in IndexEntries.Where(e => e.Root == "weat"))
                    WriteWeatherSystemData(entry, outStream, writer);
                foreach (IndexEntry entry in IndexEntries.Where(e => e.Root == "DECR"))
                    WriteDecoratorSetData(entry, outStream, writer);
                foreach (IndexEntry entry in IndexEntries.Where(e => e.Root == "PRTM"))
                    WriteParticleModelData(entry, outStream, writer);
                foreach (IndexEntry entry in IndexEntries.Where(e => e.Root == "ugh!"))
                    WriteSoundGestaltExtraInfosData(entry, outStream, writer);
                foreach (IndexEntry entry in IndexEntries.Where(e => e.Root == "jmad"))
                    WriteModelAnimationData(entry, outStream, writer);

                //Create index
                byte[] indexTable = null;
                using (VirtualStream index = new VirtualStream(Index.IndexVirtualAddress))
                using (BinaryWriter indexWriter = new BinaryWriter(index))
                {
                    //Skip header, tags, and objects
                    index.Seek(Index.Length, SeekOrigin.Current);
                    index.Seek(Tags.Count * TagHierarchy.Length, SeekOrigin.Current);
                    index.Seek(IndexEntries.Count * ObjectEntry.Length, SeekOrigin.Current);

                    //Align to 4096 bytes
                    index.Align(4096);

                    //Get length and index
                    mapHeader.IndexLength = (uint)index.Length;
                    indexTable = index.ToArray();
                }

                //Setup index
                mapIndex.TagsAddress = Index.IndexTagsAddress;
                mapIndex.TagCount = (uint)Tags.Count;
                mapIndex.ObjectsOffset = (uint)(Tags.Count * TagHierarchy.Length) + Index.IndexTagsAddress;
                mapIndex.ScenarioId = Scenario.Id;
                mapIndex.GlobalsId = Globals.Id;
                mapIndex.ObjectCount = (uint)IndexEntries.Count;
                mapIndex.Tags = "tags";

                //Get BSP memory address
                uint bspMemoryAddress = (uint)(Index.IndexVirtualAddress + indexTable.Length);
                int bspLength = 0;

                //Open tag data reader
                using (BinaryReader tagReader = Scenario.Data.GetVirtualStream().CreateReader())
                using (BinaryWriter tagWriter = Scenario.Data.GetVirtualStream().CreateWriter())
                {
                    //Goto structure BSPs in scenario tag
                    tagReader.BaseStream.Seek(Scenario.Address + 528, SeekOrigin.Begin);
                    TagBlock structureBsps = tagReader.ReadTagBlock();

                    //Loop through each tag block
                    for (int i = 0; i < structureBsps.Count; i++)
                    {
                        //Prepare
                        long sbspAddress = 0, ltmpAddress = 0;

                        //Goto structure BSP block
                        tagReader.BaseStream.Seek(structureBsps.Offset + (i * 68), SeekOrigin.Begin);

                        //Read scenario structure BSP tag block data
                        tagReader.ReadUInt32();
                        tagReader.ReadUInt32();
                        tagReader.ReadUInt32();
                        tagReader.ReadUInt32();
                        TagFourCc sbspTag = tagReader.Read<TagFourCc>();
                        TagId sbspId = tagReader.ReadUInt32();
                        TagFourCc ltmpTag = tagReader.Read<TagFourCc>();
                        TagId ltmpId = tagReader.ReadUInt32();

                        //Create stream
                        using (VirtualStream bspStream = new VirtualStream(bspMemoryAddress))
                        using (BinaryWriter bspWriter = new BinaryWriter(bspStream))
                        {
                            //Skip header
                            bspStream.Seek(16, SeekOrigin.Current);

                            //Check for scenario structure bsp
                            if (IndexEntries.ContainsId(sbspId))
                            {
                                //Get scenario structure BSP index entry and offset
                                IndexEntry scenarioStructureBsp = IndexEntries[sbspId];
                                sbspAddress = bspStream.Position;

                                //Create scenario structure BSP tag group
                                var scenarioStructureBspTagGroup = new Tag.Generated.ScenarioStructureBsp();

                                //Read scenario structure BSP
                                using (BinaryReader bspReader = scenarioStructureBsp.Data.GetVirtualStream().CreateReader())
                                {
                                    bspReader.BaseStream.Seek(scenarioStructureBsp.Address, SeekOrigin.Begin);
                                    scenarioStructureBspTagGroup.Read(bspReader);
                                }

                                //Write new scenario structure bsp
                                using (VirtualStream sbspStream = new VirtualStream(sbspAddress))
                                using (BinaryWriter sbspWriter = new BinaryWriter(sbspStream))
                                {
                                    //Goto scenario structure BSP and write tag group
                                    sbspStream.Seek(sbspAddress, SeekOrigin.Begin);
                                    scenarioStructureBspTagGroup.Write(sbspWriter);

                                    //Align
                                    sbspStream.Align(4096);

                                    //Set scenario structure BSP index entry
                                    scenarioStructureBsp.Data.SetBuffer(sbspStream.ToArray());
                                    scenarioStructureBsp.Address = sbspAddress;
                                    scenarioStructureBsp.Length = sbspStream.Length;
                                }

                                //Write to BSP stream
                                scenarioStructureBspTagGroup.Write(bspWriter);

                                //Align to 4096
                                bspStream.Align(4096);
                            }

                            //Check for scenario structure lightmap
                            if (IndexEntries.ContainsId(ltmpId))
                            {
                                //Get scenario structure lightmap index entry and offset
                                IndexEntry scenarioStructureLightmap = IndexEntries[ltmpId];
                                ltmpAddress = (uint)bspStream.Position;

                                //Create scenario structure lightmap tag group
                                var scenarioStructureLightmapTagGroup = new Tag.Generated.ScenarioStructureLightmap();

                                //Read scenario structure lightmap
                                using (BinaryReader bspReader = scenarioStructureLightmap.Data.GetVirtualStream().CreateReader())
                                {
                                    bspReader.BaseStream.Seek(scenarioStructureLightmap.Address, SeekOrigin.Begin);
                                    scenarioStructureLightmapTagGroup.Read(bspReader);
                                }

                                //Write new scenario structure lightmap
                                using (VirtualStream ltmpStream = new VirtualStream(ltmpAddress))
                                using (BinaryWriter ltmpWriter = new BinaryWriter(ltmpStream))
                                {
                                    //Goto scenario structure lightmap and write tag group
                                    ltmpStream.Seek(ltmpAddress, SeekOrigin.Begin);
                                    scenarioStructureLightmapTagGroup.Write(ltmpWriter);

                                    //Align
                                    ltmpStream.Align(4096);

                                    //Set scenario structure lightmap entry
                                    scenarioStructureLightmap.Data.SetBuffer(ltmpStream.ToArray());
                                    scenarioStructureLightmap.Address = ltmpAddress;
                                    scenarioStructureLightmap.Length = ltmpStream.Length;
                                }

                                //Write to BSP stream
                                scenarioStructureLightmapTagGroup.Write(bspWriter);

                                //Align to 4096
                                bspStream.Align(4096);
                            }

                            //Write header
                            bspStream.Seek(bspMemoryAddress, SeekOrigin.Begin);
                            bspWriter.Write((uint)bspStream.Length);
                            bspWriter.Write((uint)sbspAddress);
                            bspWriter.Write((uint)ltmpAddress);
                            bspWriter.Write<TagFourCc>(new TagFourCc("sbsp"));

                            //Get bsp data and check length
                            byte[] bspData = bspStream.ToArray();
                            if (bspLength < bspData.Length) bspLength = bspData.Length;

                            //Goto structure BSP block
                            tagReader.BaseStream.Seek(structureBsps.Offset + (i * 68), SeekOrigin.Begin);

                            //Write BSP information
                            tagWriter.Write((uint)outStream.Align(512));
                            tagWriter.Write(bspData.Length);
                            tagWriter.Write(bspMemoryAddress);

                            //Write BSP to file
                            writer.Write(bspData);
                        }
                    }
                }

                //Write strings 128
                mapHeader.StringCount = (uint)Strings.Count;
                mapHeader.Strings128Offset = (uint)outStream.Align(512);
                foreach (string stringId in Strings)
                    writer.WriteUTF8(stringId.PadRight(128, '\0'));

                //Write strings index
                int offset = 0;
                mapHeader.StringsIndexOffset = (uint)outStream.Align(512);
                foreach (string stringId in Strings)
                {
                    writer.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringId) + 1;
                }

                //Write strings
                mapHeader.StringsOffset = (uint)outStream.Align(512);
                mapHeader.StringsLength = (uint)Strings.Sum(s => Encoding.UTF8.GetByteCount(s) + 1);
                foreach (string stringId in Strings)
                    writer.WriteUTF8NullTerminated(stringId);

                //Write file names
                mapHeader.FileNameCount = (uint)IndexEntries.Count;
                mapHeader.FileNamesOffset = (uint)outStream.Align(512);
                mapHeader.FileNamesLength = (uint)IndexEntries.Sum(e => Encoding.UTF8.GetByteCount(e.Filename) + 1);
                foreach (IndexEntry entry in IndexEntries)
                    writer.WriteUTF8NullTerminated(entry.Filename);

                //Write files Index
                offset = 0;
                mapHeader.FileNamesIndexOffset = (uint)outStream.Align(512);
                foreach (IndexEntry entry in IndexEntries)
                {
                    writer.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(entry.Filename) + 1;
                }

                //Gather strings
                StringContainer unicodeStringContainer = new StringContainer();

                //Get all unic tags
                foreach (IndexEntry entry in IndexEntries.Where(e => e.Root == "unic"))
                    entry.Strings.CopyTo(unicodeStringContainer);

                //Write unicode string tables
                int enSize, jpSize, nlSize, frSize, esSize, itSize, krSize, zhSize, prSize;
                offset = 0;

                //Write english unicode table
                uint enIndex = (uint)outStream.Align(512, 0);
                for (int i = 0; i < unicodeStringContainer.English.Count; i++)
                {
                    var stringObject = unicodeStringContainer.English[i];
                    writer.Write(StringId.FromString(stringObject.ID, Strings.IndexOf(stringObject.ID)));
                    writer.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringObject.Value) + 1;
                }
                uint enTable = (uint)outStream.Align(512, 0);
                using (MemoryStream stringStream = new MemoryStream())
                using (BinaryWriter stringWriter = new BinaryWriter(stringStream))
                {
                    foreach (var stringObject in unicodeStringContainer.English)
                        stringWriter.WriteUTF8NullTerminated(stringObject.Value);
                    enSize = (int)stringStream.Length;
                    writer.Write(stringStream.ToArray());
                }

                //Write japanese unicode table
                offset = 0;
                uint jpIndex = (uint)outStream.Align(512, 0);
                for (int i = 0; i < unicodeStringContainer.Japanese.Count; i++)
                {
                    var stringObject = unicodeStringContainer.Japanese[i];
                    writer.Write(StringId.FromString(stringObject.ID, Strings.IndexOf(stringObject.ID)));
                    writer.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringObject.Value) + 1;
                }
                uint jpTable = (uint)outStream.Align(512, 0);
                using (MemoryStream stringStream = new MemoryStream())
                using (BinaryWriter stringWriter = new BinaryWriter(stringStream))
                {
                    foreach (var stringObject in unicodeStringContainer.Japanese)
                        stringWriter.WriteUTF8NullTerminated(stringObject.Value);
                    jpSize = (int)stringStream.Length;
                    writer.Write(stringStream.ToArray());
                }

                //Write german unicode table
                offset = 0;
                uint nlIndex = (uint)outStream.Align(512, 0);
                for (int i = 0; i < unicodeStringContainer.German.Count; i++)
                {
                    var stringObject = unicodeStringContainer.German[i];
                    writer.Write(StringId.FromString(stringObject.ID, Strings.IndexOf(stringObject.ID)));
                    writer.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringObject.Value) + 1;
                }
                uint nlTable = (uint)outStream.Align(512, 0);
                using (MemoryStream stringStream = new MemoryStream())
                using (BinaryWriter stringWriter = new BinaryWriter(stringStream))
                {
                    foreach (var stringObject in unicodeStringContainer.German)
                        stringWriter.WriteUTF8NullTerminated(stringObject.Value);
                    nlSize = (int)stringStream.Length;
                    writer.Write(stringStream.ToArray());
                }

                //Write french unicode table
                offset = 0;
                uint frIndex = (uint)outStream.Align(512, 0);
                for (int i = 0; i < unicodeStringContainer.French.Count; i++)
                {
                    var stringObject = unicodeStringContainer.French[i];
                    writer.Write(StringId.FromString(stringObject.ID, Strings.IndexOf(stringObject.ID)));
                    writer.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringObject.Value) + 1;
                }
                uint frTable = (uint)outStream.Align(512, 0);
                using (MemoryStream stringStream = new MemoryStream())
                using (BinaryWriter stringWriter = new BinaryWriter(stringStream))
                {
                    foreach (var stringObject in unicodeStringContainer.French)
                        stringWriter.WriteUTF8NullTerminated(stringObject.Value);
                    frSize = (int)stringStream.Length;
                    writer.Write(stringStream.ToArray());
                }

                //Write spanish unicode table
                offset = 0;
                uint esIndex = (uint)outStream.Align(512, 0);
                for (int i = 0; i < unicodeStringContainer.Spanish.Count; i++)
                {
                    var stringObject = unicodeStringContainer.Spanish[i];
                    writer.Write(StringId.FromString(stringObject.ID, Strings.IndexOf(stringObject.ID)));
                    writer.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringObject.Value) + 1;
                }
                uint esTable = (uint)outStream.Align(512, 0);
                using (MemoryStream stringStream = new MemoryStream())
                using (BinaryWriter stringWriter = new BinaryWriter(stringStream))
                {
                    foreach (var stringObject in unicodeStringContainer.Spanish)
                        stringWriter.WriteUTF8NullTerminated(stringObject.Value);
                    esSize = (int)stringStream.Length;
                    writer.Write(stringStream.ToArray());
                }

                //Write italian unicode table
                offset = 0;
                uint itIndex = (uint)outStream.Align(512, 0);
                for (int i = 0; i < unicodeStringContainer.Italian.Count; i++)
                {
                    var stringObject = unicodeStringContainer.Italian[i];
                    writer.Write(StringId.FromString(stringObject.ID, Strings.IndexOf(stringObject.ID)));
                    writer.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringObject.Value) + 1;
                }
                uint itTable = (uint)outStream.Align(512, 0);
                using (MemoryStream stringStream = new MemoryStream())
                using (BinaryWriter stringWriter = new BinaryWriter(stringStream))
                {
                    foreach (var stringObject in unicodeStringContainer.Italian)
                        stringWriter.WriteUTF8NullTerminated(stringObject.Value);
                    itSize = (int)stringStream.Length;
                    writer.Write(stringStream.ToArray());
                }

                //Write korean unicode table
                offset = 0;
                uint krIndex = (uint)outStream.Align(512, 0);
                for (int i = 0; i < unicodeStringContainer.Korean.Count; i++)
                {
                    var stringObject = unicodeStringContainer.Korean[i];
                    writer.Write(StringId.FromString(stringObject.ID, Strings.IndexOf(stringObject.ID)));
                    writer.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringObject.Value) + 1;
                }
                uint krTable = (uint)outStream.Align(512, 0);
                using (MemoryStream stringStream = new MemoryStream())
                using (BinaryWriter stringWriter = new BinaryWriter(stringStream))
                {
                    foreach (var stringObject in unicodeStringContainer.Korean)
                        stringWriter.WriteUTF8NullTerminated(stringObject.Value);
                    krSize = (int)stringStream.Length;
                    writer.Write(stringStream.ToArray());
                }

                //Write chinese unicode table
                offset = 0;
                uint zhIndex = (uint)outStream.Align(512, 0);
                for (int i = 0; i < unicodeStringContainer.Chinese.Count; i++)
                {
                    var stringObject = unicodeStringContainer.Chinese[i];
                    writer.Write(StringId.FromString(stringObject.ID, Strings.IndexOf(stringObject.ID)));
                    writer.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringObject.Value) + 1;
                }
                uint zhTable = (uint)outStream.Align(512, 0);
                using (MemoryStream stringStream = new MemoryStream())
                using (BinaryWriter stringWriter = new BinaryWriter(stringStream))
                {
                    foreach (var stringObject in unicodeStringContainer.Chinese)
                        stringWriter.WriteUTF8NullTerminated(stringObject.Value);
                    zhSize = (int)stringStream.Length;
                    writer.Write(stringStream.ToArray());
                }

                //Write portuguese unicode table
                offset = 0;
                uint prIndex = (uint)outStream.Align(512, 0);
                for (int i = 0; i < unicodeStringContainer.Portuguese.Count; i++)
                {
                    var stringObject = unicodeStringContainer.Portuguese[i];
                    writer.Write(StringId.FromString(stringObject.ID, Strings.IndexOf(stringObject.ID)));
                    writer.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringObject.Value) + 1;
                }
                uint prTable = (uint)outStream.Align(512, 0);
                using (MemoryStream stringStream = new MemoryStream())
                using (BinaryWriter stringWriter = new BinaryWriter(stringStream))
                {
                    foreach (var stringObject in unicodeStringContainer.Portuguese)
                        stringWriter.WriteUTF8NullTerminated(stringObject.Value);
                    prSize = (int)stringStream.Length;
                    writer.Write(stringStream.ToArray());
                }

                //Write crazy
                mapHeader.CrazyOffset = (uint)outStream.Align(512);
                mapHeader.CrazyLength = (uint)Crazy.Length;
                writer.Write(Crazy.GetBuffer());

                //Write bitmap resources
                foreach (IndexEntry entry in IndexEntries.Where(e => e.Root == "bitm"))
                    WriteBitmapData(entry, outStream, writer);

                //Get tag data address
                int tagDataLength = 0;
                long tagDataAddress = bspMemoryAddress + bspLength;
                using (VirtualStream tagDataStream = new VirtualStream(tagDataAddress))
                using (BinaryWriter tagDataWriter = new BinaryWriter(tagDataStream))
                {
                    //Loop through index entries
                    foreach (IndexEntry entry in IndexEntries)
                    {
                        //Check root
                        if (entry.Root == "sbsp" || entry.Root == "ltmp")
                            continue;

                        //Create tag group
                        var tagGroup = Tag.Generated.TagLookup.CreateTagGroup(entry.Root);

                        //Read tag group
                        if (entry.Length > 0)
                            using (BinaryReader tagReader = entry.Data.GetVirtualStream().CreateReader())
                            {
                                //Goto tag address and read
                                tagReader.BaseStream.Seek(entry.Address, SeekOrigin.Begin);
                                tagGroup.Read(tagReader);
                            }

                        //Create virtual stream
                        using (VirtualStream tagData = new VirtualStream(tagDataStream.Position))
                        using (BinaryWriter tagWriter = new BinaryWriter(tagData))
                        {
                            //Write to stream
                            if (entry.Length > 0) tagGroup.Write(tagWriter);

                            //Get tag data
                            byte[] data = tagData.ToArray();

                            //Setup entry
                            entry.Address = tagData.BaseAddress;
                            entry.Length = data.LongLength;
                            entry.Data.SetBuffer(data);

                            //Write tag data to stream
                            tagDataWriter.Write(data);
                        }
                    }

                    //Align to 4096
                    tagDataStream.Align(4096);

                    //Get tag data
                    byte[] tagDataBuffer = tagDataStream.ToArray();

                    //Set tag data length
                    tagDataLength = tagDataBuffer.Length;

                    //Write index
                    using (VirtualStream indexStream = new VirtualStream(Index.IndexVirtualAddress, indexTable))
                    using (BinaryWriter indexWriter = new BinaryWriter(indexStream))
                    {
                        //Write index header
                        indexStream.Seek(Index.IndexVirtualAddress, SeekOrigin.Begin);
                        indexWriter.Write(mapIndex);

                        //Write tags
                        foreach (TagHierarchy tag in Tags)
                            indexWriter.Write(tag);

                        //Write objects
                        foreach (IndexEntry entry in IndexEntries)
                        {
                            //Create object entry
                            ObjectEntry @object = new ObjectEntry() { Id = entry.Id, Tag = entry.Root };
                            if (entry.Root != "sbsp" && entry.Root != "ltmp")
                            {
                                @object.Offset = (uint)entry.Address;
                                @object.Size = (uint)entry.Length;
                            }

                            //Write object entry
                            indexWriter.Write(@object);
                        }

                        //Setup header
                        mapHeader.IndexOffset = (uint)outStream.Align(512);
                        mapHeader.MapDataLength = (uint)(indexTable.Length + bspLength + tagDataLength);
                        mapHeader.TagDataLength = (uint)tagDataLength;
                    }

                    //Write index table to map
                    writer.Write(indexTable);

                    //Write tag data
                    writer.Write(tagDataBuffer);

                    //Pad map
                    outStream.Align(1024, 0xCD);

                    //Goto string information in globals
                    outStream.Seek(mapHeader.IndexOffset + mapHeader.IndexLength + 400, SeekOrigin.Begin);

                    //write english
                    writer.Write(unicodeStringContainer.English.Count);
                    writer.Write(enSize);
                    writer.Write(enIndex);
                    writer.Write(enTable);

                    //write japanese
                    outStream.Seek(12, SeekOrigin.Current);
                    writer.Write(unicodeStringContainer.Japanese.Count);
                    writer.Write(jpSize);
                    writer.Write(jpIndex);
                    writer.Write(jpTable);

                    //write german
                    outStream.Seek(12, SeekOrigin.Current);
                    writer.Write(unicodeStringContainer.German.Count);
                    writer.Write(nlSize);
                    writer.Write(nlIndex);
                    writer.Write(nlTable);

                    //write french
                    outStream.Seek(12, SeekOrigin.Current);
                    writer.Write(unicodeStringContainer.French.Count);
                    writer.Write(frSize);
                    writer.Write(frIndex);
                    writer.Write(frTable);

                    //write spanish
                    outStream.Seek(12, SeekOrigin.Current);
                    writer.Write(unicodeStringContainer.Spanish.Count);
                    writer.Write(esSize);
                    writer.Write(esIndex);
                    writer.Write(esTable);

                    //write italian
                    outStream.Seek(12, SeekOrigin.Current);
                    writer.Write(unicodeStringContainer.Italian.Count);
                    writer.Write(itSize);
                    writer.Write(itIndex);
                    writer.Write(itTable);

                    //write korean
                    outStream.Seek(12, SeekOrigin.Current);
                    writer.Write(unicodeStringContainer.Korean.Count);
                    writer.Write(krSize);
                    writer.Write(krIndex);
                    writer.Write(krTable);

                    //write chinese
                    outStream.Seek(12, SeekOrigin.Current);
                    writer.Write(unicodeStringContainer.Chinese.Count);
                    writer.Write(zhSize);
                    writer.Write(zhIndex);
                    writer.Write(zhTable);

                    //write portuguese
                    outStream.Seek(12, SeekOrigin.Current);
                    writer.Write(unicodeStringContainer.Portuguese.Count);
                    writer.Write(prSize);
                    writer.Write(prIndex);
                    writer.Write(prTable);

                    //Finalize map header
                    mapHeader.FileLength = (uint)outStream.Length;

                    //Sign
                    mapHeader.Checksum = 0;
                    outStream.Seek(2048, SeekOrigin.Begin);
                    for (int i = 0; i < (mapHeader.FileLength - 2048) / 4; i++)
                        mapHeader.Checksum ^= reader.ReadUInt32();

                    //Write header
                    outStream.Seek(0, SeekOrigin.Begin);
                    writer.Write(mapHeader);
                }
            }
        }
        /// <summary>
        /// Saves this <see cref="HaloMap"/> to the specified file.
        /// </summary>
        /// <param name="filename">A string that contains the name of the file to which to save this <see cref="HaloMap"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="filename"/> is <see langword="null"/>.</exception>
        public void Save(string filename)
        {
            //Check
            if (filename == null) throw new ArgumentNullException(nameof(filename));

            //Create file
            using (FileStream fs = File.Create(filename))
                Save(fs);
        }
        /// <summary>
        /// Returns a string representation of this instance.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return Name;
        }
        /// <summary>
        /// Releases all resources used by this instance.
        /// </summary>
        public void Dispose()
        {
            //Check
            if (IsDisposed) throw new ObjectDisposedException("this");

            //Release crazy
            Crazy.Dispose();
            Crazy.SetBuffer(null);

            //Release all index entries
            foreach (var entry in IndexEntries)
                entry.Dispose();

            //Reset
            Tags.Clear();
            Strings.Clear();
            Scenario = null;
            Globals = null;
            Name = null;

            //Set disposed
            IsDisposed = true;
        }
        private void ReadSoundData(IndexEntry entry, Stream fs, BinaryReader reader)
        {
            //Create reader
            using (BinaryReader tagReader = entry.Data.GetVirtualStream().CreateReader())
            {
                //Read sounds
                tagReader.BaseStream.Seek(entry.Address + 64, SeekOrigin.Begin);
                TagBlock sounds = tagReader.Read<TagBlock>();
                for (int i = 0; i < sounds.Count; i++)
                {
                    //Goto resource description
                    tagReader.BaseStream.Seek(sounds.Offset + (i * 12), SeekOrigin.Begin);
                    int rawOffset = tagReader.ReadInt32();
                    int rawSize = tagReader.ReadInt32() & 0x7FFFFFFF;   //Mask off the first bit

                    //Check
                    if ((rawOffset & 0xC0000000) == 0 && rawOffset >= 0 && rawOffset < fs.Length)
                    {
                        //Read resource
                        fs.Seek(rawOffset, SeekOrigin.Begin);
                        if (!entry.Resources.Contains(rawOffset))
                            entry.Resources.AddResource(rawOffset, reader.ReadBytes(rawSize));
                    }
                }

                //Read extra infos
                tagReader.BaseStream.Seek(entry.Address + 80, SeekOrigin.Begin);
                TagBlock extraInfos = tagReader.Read<TagBlock>();
                for (int i = 0; i < extraInfos.Count; i++)
                {
                    //Goto resource description
                    tagReader.BaseStream.Seek(extraInfos.Offset + (i * 44) + 8, SeekOrigin.Begin);
                    int rawOffset = tagReader.ReadInt32();
                    int rawSize = tagReader.ReadInt32();

                    //Check
                    if ((rawOffset & 0xC0000000) == 0 && rawOffset >= 0 && rawOffset < fs.Length)
                    {
                        //Read resource
                        fs.Seek(rawOffset, SeekOrigin.Begin);
                        if (!entry.Resources.Contains(rawOffset))
                            entry.Resources.AddResource(rawOffset, reader.ReadBytes(rawSize));
                    }
                }
            }
        }
        private void ReadRenderModelData(IndexEntry entry, Stream fs, BinaryReader reader)
        {
            //Create reader
            using (BinaryReader tagReader = entry.Data.GetVirtualStream().CreateReader())
            {
                //Read sections
                tagReader.BaseStream.Seek(entry.Address + 36, SeekOrigin.Begin);
                TagBlock sections = tagReader.Read<TagBlock>();
                for (int i = 0; i < sections.Count; i++)
                {
                    //Goto resource description
                    tagReader.BaseStream.Seek(sections.Offset + (i * 92) + 56, SeekOrigin.Begin);
                    int rawOffset = tagReader.ReadInt32();
                    int rawSize = tagReader.ReadInt32();

                    //Check
                    if ((rawOffset & 0xC0000000) == 0)
                    {
                        //Read resource
                        fs.Seek(rawOffset, SeekOrigin.Begin);
                        if (!entry.Resources.Contains(rawOffset))
                            entry.Resources.AddResource(rawOffset, reader.ReadBytes(rawSize));
                    }
                }

                //Read prt info
                tagReader.BaseStream.Seek(entry.Address + 116, SeekOrigin.Begin);
                TagBlock prtInfos = tagReader.Read<TagBlock>();
                for (int i = 0; i < prtInfos.Count; i++)
                {
                    //Goto resource description
                    tagReader.BaseStream.Seek(prtInfos.Offset + (i * 88) + 52, SeekOrigin.Begin);
                    int rawOffset = tagReader.ReadInt32();
                    int rawSize = tagReader.ReadInt32();

                    //Check
                    if ((rawOffset & 0xC0000000) == 0 && rawOffset >= 0 && rawOffset < fs.Length)
                    {
                        //Read resource
                        fs.Seek(rawOffset, SeekOrigin.Begin);
                        if (!entry.Resources.Contains(rawOffset))
                            entry.Resources.AddResource(rawOffset, reader.ReadBytes(rawSize));
                    }
                }
            }
        }
        private void ReadScenarioStructureBspData(IndexEntry entry, Stream fs, BinaryReader reader)
        {
            //Create reader
            using (BinaryReader tagReader = entry.Data.GetVirtualStream().CreateReader())
            {
                //Read clusters
                tagReader.BaseStream.Seek(entry.Address + 156, SeekOrigin.Begin);
                TagBlock clusters = tagReader.Read<TagBlock>();
                for (int i = 0; i < clusters.Count; i++)
                {
                    //Goto resource description
                    tagReader.BaseStream.Seek(clusters.Offset + (i * 176) + 40, SeekOrigin.Begin);
                    int rawOffset = tagReader.ReadInt32();
                    int rawSize = tagReader.ReadInt32();

                    //Check
                    if ((rawOffset & 0xC0000000) == 0 && rawOffset >= 0 && rawOffset < fs.Length)
                    {
                        //Read resource
                        fs.Seek(rawOffset, SeekOrigin.Begin);
                        if (!entry.Resources.Contains(rawOffset))
                            entry.Resources.AddResource(rawOffset, reader.ReadBytes(rawSize));
                    }
                }

                //Read geometries definitions
                tagReader.BaseStream.Seek(entry.Address + 312, SeekOrigin.Begin);
                TagBlock geometryDefinitions = tagReader.Read<TagBlock>();
                for (int i = 0; i < geometryDefinitions.Count; i++)
                {
                    //Goto resource description
                    tagReader.BaseStream.Seek(geometryDefinitions.Offset + (i * 200) + 40, SeekOrigin.Begin);
                    int rawOffset = tagReader.ReadInt32();
                    int rawSize = tagReader.ReadInt32();

                    //Check
                    if ((rawOffset & 0xC0000000) == 0 && rawOffset >= 0 && rawOffset < fs.Length)
                    {
                        //Read resource
                        fs.Seek(rawOffset, SeekOrigin.Begin);
                        if (!entry.Resources.Contains(rawOffset))
                            entry.Resources.AddResource(rawOffset, reader.ReadBytes(rawSize));
                    }
                }

                //Read water definitions
                tagReader.BaseStream.Seek(entry.Address + 532, SeekOrigin.Begin);
                TagBlock waterDefinitions = tagReader.Read<TagBlock>();
                for (int i = 0; i < waterDefinitions.Count; i++)
                {
                    //Goto resource description
                    tagReader.BaseStream.Seek(waterDefinitions.Offset + (i * 172) + 16, SeekOrigin.Begin);
                    int rawOffset = tagReader.ReadInt32();
                    int rawSize = tagReader.ReadInt32();

                    //Check
                    if ((rawOffset & 0xC0000000) == 0 && rawOffset >= 0 && rawOffset < fs.Length)
                    {
                        //Read resource
                        fs.Seek(rawOffset, SeekOrigin.Begin);
                        if (!entry.Resources.Contains(rawOffset))
                            entry.Resources.AddResource(rawOffset, reader.ReadBytes(rawSize));
                    }
                }

                //Read decorators definitions
                tagReader.BaseStream.Seek(entry.Address + 564, SeekOrigin.Begin);
                TagBlock decorators = tagReader.Read<TagBlock>();
                for (int i = 0; i < decorators.Count; i++)
                {
                    //Goto caches
                    tagReader.BaseStream.Seek(decorators.Offset + (i * 48) + 16, SeekOrigin.Begin);
                    TagBlock caches = tagReader.Read<TagBlock>();
                    for (int j = 0; j < caches.Count; j++)
                    {
                        //Goto resource description
                        tagReader.BaseStream.Seek(caches.Offset + (j * 44), SeekOrigin.Begin);
                        int rawOffset = tagReader.ReadInt32();
                        int rawSize = tagReader.ReadInt32();

                        //Check
                        if ((rawOffset & 0xC0000000) == 0 && rawOffset >= 0 && rawOffset < fs.Length)
                        {
                            //Read resource
                            fs.Seek(rawOffset, SeekOrigin.Begin);
                            if (!entry.Resources.Contains(rawOffset))
                                entry.Resources.AddResource(rawOffset, reader.ReadBytes(rawSize));
                        }
                    }
                }
            }
        }
        private void ReadScenarioStructureLightmapData(IndexEntry entry, Stream fs, BinaryReader reader)
        {
            //Create reader
            using (BinaryReader tagReader = entry.Data.GetVirtualStream().CreateReader())
            {
                //Read decorators definitions
                tagReader.BaseStream.Seek(entry.Address + 128, SeekOrigin.Begin);
                TagBlock groups = tagReader.Read<TagBlock>();
                for (int i = 0; i < groups.Count; i++)
                {
                    //Goto clusters
                    tagReader.BaseStream.Seek(groups.Offset + (i * 104) + 32, SeekOrigin.Begin);
                    TagBlock clusters = tagReader.Read<TagBlock>();
                    for (int j = 0; j < clusters.Count; j++)
                    {
                        //Goto resource description
                        tagReader.BaseStream.Seek(clusters.Offset + (j * 84) + 40, SeekOrigin.Begin);
                        int rawOffset = tagReader.ReadInt32();
                        int rawSize = tagReader.ReadInt32();

                        //Check
                        if ((rawOffset & 0xC0000000) == 0 && rawOffset >= 0 && rawOffset < fs.Length)
                        {
                            //Read resource
                            fs.Seek(rawOffset, SeekOrigin.Begin);
                            if (!entry.Resources.Contains(rawOffset))
                                entry.Resources.AddResource(rawOffset, reader.ReadBytes(rawSize));
                        }
                    }

                    //Goto poop definitions
                    tagReader.BaseStream.Seek(groups.Offset + (i * 104) + 48, SeekOrigin.Begin);
                    TagBlock poops = tagReader.Read<TagBlock>();
                    for (int j = 0; j < poops.Count; j++)
                    {
                        //Goto resource description
                        tagReader.BaseStream.Seek(poops.Offset + (j * 84) + 40, SeekOrigin.Begin);
                        int rawOffset = tagReader.ReadInt32();
                        int rawSize = tagReader.ReadInt32();

                        //Check
                        if ((rawOffset & 0xC0000000) == 0 && rawOffset >= 0 && rawOffset < fs.Length)
                        {
                            //Read resource
                            fs.Seek(rawOffset, SeekOrigin.Begin);
                            if (!entry.Resources.Contains(rawOffset))
                                entry.Resources.AddResource(rawOffset, reader.ReadBytes(rawSize));
                        }
                    }

                    //Goto geometry buckets
                    tagReader.BaseStream.Seek(groups.Offset + (i * 104) + 64, SeekOrigin.Begin);
                    TagBlock buckets = tagReader.Read<TagBlock>();
                    for (int j = 0; j < buckets.Count; j++)
                    {
                        //Goto resource description
                        tagReader.BaseStream.Seek(buckets.Offset + (j * 56) + 12, SeekOrigin.Begin);
                        int rawOffset = tagReader.ReadInt32();
                        int rawSize = tagReader.ReadInt32();

                        //Check
                        if ((rawOffset & 0xC0000000) == 0 && rawOffset >= 0 && rawOffset < fs.Length)
                        {
                            //Read resource
                            fs.Seek(rawOffset, SeekOrigin.Begin);
                            if (!entry.Resources.Contains(rawOffset))
                                entry.Resources.AddResource(rawOffset, reader.ReadBytes(rawSize));
                        }
                    }
                }
            }
        }
        private void ReadWeatherSystemData(IndexEntry entry, Stream fs, BinaryReader reader)
        {
            //Create reader
            using (BinaryReader tagReader = entry.Data.GetVirtualStream().CreateReader())
            {
                //Read particle systems
                tagReader.BaseStream.Seek(entry.Address, SeekOrigin.Begin);
                TagBlock particleSystems = tagReader.Read<TagBlock>();
                for (int i = 0; i < particleSystems.Count; i++)
                {
                    //Goto resource description
                    tagReader.BaseStream.Seek(particleSystems.Offset + (i * 140) + 64, SeekOrigin.Begin);
                    int rawOffset = tagReader.ReadInt32();
                    int rawSize = tagReader.ReadInt32();

                    //Check
                    if ((rawOffset & 0xC0000000) == 0 && rawOffset >= 0 && rawOffset < fs.Length)
                    {
                        //Read resource
                        fs.Seek(rawOffset, SeekOrigin.Begin);
                        if (!entry.Resources.Contains(rawOffset))
                            entry.Resources.AddResource(rawOffset, reader.ReadBytes(rawSize));
                    }
                }
            }
        }
        private void ReadDecoratorSetData(IndexEntry entry, Stream fs, BinaryReader reader)
        {
            //Create reader
            using (BinaryReader tagReader = entry.Data.GetVirtualStream().CreateReader())
            {
                //Goto resource description
                tagReader.BaseStream.Seek(entry.Address + 56, SeekOrigin.Begin);
                int rawOffset = tagReader.ReadInt32();
                int rawSize = tagReader.ReadInt32();

                //Check
                if ((rawOffset & 0xC0000000) == 0 && rawOffset >= 0 && rawOffset < fs.Length)
                {
                    //Read resource
                    fs.Seek(rawOffset, SeekOrigin.Begin);
                    if (!entry.Resources.Contains(rawOffset))
                        entry.Resources.AddResource(rawOffset, reader.ReadBytes(rawSize));
                }
            }
        }
        private void ReadParticleModelData(IndexEntry entry, Stream fs, BinaryReader reader)
        {
            //Create reader
            using (BinaryReader tagReader = entry.Data.GetVirtualStream().CreateReader())
            {
                //Goto resource description
                tagReader.BaseStream.Seek(entry.Address + 160, SeekOrigin.Begin);
                int rawOffset = tagReader.ReadInt32();
                int rawSize = tagReader.ReadInt32();

                //Check
                if ((rawOffset & 0xC0000000) == 0 && rawOffset >= 0 && rawOffset < fs.Length)
                {
                    //Read
                    fs.Seek(rawOffset, SeekOrigin.Begin);
                    if (!entry.Resources.Contains(rawOffset))
                        entry.Resources.AddResource(rawOffset, reader.ReadBytes(rawSize));
                }
            }
        }
        private void ReadModelAnimationData(IndexEntry entry, Stream fs, BinaryReader reader)
        {
            //Create reader
            using (BinaryReader tagReader = entry.Data.GetVirtualStream().CreateReader())
            {
                //Read animations
                tagReader.BaseStream.Seek(entry.Address + 172, SeekOrigin.Begin);
                TagBlock animations = tagReader.Read<TagBlock>();
                for (int i = 0; i < animations.Count; i++)
                {
                    //Goto resource description
                    tagReader.BaseStream.Seek(animations.Offset + (i * 20) + 4, SeekOrigin.Begin);
                    int rawSize = tagReader.ReadInt32();
                    int rawOffset = tagReader.ReadInt32();

                    //Check
                    if ((rawOffset & 0xC0000000) == 0 && rawOffset >= 0 && rawOffset < fs.Length)
                    {
                        //Read resource
                        fs.Seek(rawOffset, SeekOrigin.Begin);
                        if (!entry.Resources.Contains(rawOffset))
                            entry.Resources.AddResource(rawOffset, reader.ReadBytes(rawSize));
                    }
                }
            }
        }
        private void ReadBitmapData(IndexEntry entry, Stream fs, BinaryReader reader)
        {
            //Create reader
            using (BinaryReader tagReader = entry.Data.GetVirtualStream().CreateReader())
            {
                //Read bitmap datas
                tagReader.BaseStream.Seek(entry.Address + 68, SeekOrigin.Begin);
                TagBlock bitmapDatas = tagReader.Read<TagBlock>();
                for (int i = 0; i < bitmapDatas.Count; i++)
                {
                    //Goto resource description
                    tagReader.BaseStream.Seek(bitmapDatas.Offset + (i * 116) + 28, SeekOrigin.Begin);
                    int rawOffset = tagReader.ReadInt32();
                    tagReader.BaseStream.Seek(bitmapDatas.Offset + (i * 116) + 52, SeekOrigin.Begin);
                    int rawSize = tagReader.ReadInt32();

                    //Check
                    if ((rawOffset & 0xC0000000) == 0 && rawOffset >= 0 && rawOffset < fs.Length)
                    {
                        //Read resource
                        fs.Seek(rawOffset, SeekOrigin.Begin);
                        if (!entry.Resources.Contains(rawOffset))
                            entry.Resources.AddResource(rawOffset, reader.ReadBytes(rawSize));
                    }

                    //Goto resource description
                    tagReader.BaseStream.Seek(bitmapDatas.Offset + (i * 116) + 32, SeekOrigin.Begin);
                    rawOffset = tagReader.ReadInt32();
                    tagReader.BaseStream.Seek(bitmapDatas.Offset + (i * 116) + 56, SeekOrigin.Begin);
                    rawSize = tagReader.ReadInt32();

                    //Check
                    if ((rawOffset & 0xC0000000) == 0 && rawOffset >= 0 && rawOffset < fs.Length)
                    {
                        //Read resource
                        fs.Seek(rawOffset, SeekOrigin.Begin);
                        if (!entry.Resources.Contains(rawOffset))
                            entry.Resources.AddResource(rawOffset, reader.ReadBytes(rawSize));
                    }

                    //Goto resource description
                    tagReader.BaseStream.Seek(bitmapDatas.Offset + (i * 116) + 36, SeekOrigin.Begin);
                    rawOffset = tagReader.ReadInt32();
                    tagReader.BaseStream.Seek(bitmapDatas.Offset + (i * 116) + 60, SeekOrigin.Begin);
                    rawSize = tagReader.ReadInt32();

                    //Check
                    if ((rawOffset & 0xC0000000) == 0 && rawOffset >= 0 && rawOffset < fs.Length)
                    {
                        //Read resource
                        fs.Seek(rawOffset, SeekOrigin.Begin);
                        if (!entry.Resources.Contains(rawOffset))
                            entry.Resources.AddResource(rawOffset, reader.ReadBytes(rawSize));
                    }

                    //Goto resource description
                    tagReader.BaseStream.Seek(bitmapDatas.Offset + (i * 116) + 40, SeekOrigin.Begin);
                    rawOffset = tagReader.ReadInt32();
                    tagReader.BaseStream.Seek(bitmapDatas.Offset + (i * 116) + 64, SeekOrigin.Begin);
                    rawSize = tagReader.ReadInt32();

                    //Check
                    if ((rawOffset & 0xC0000000) == 0 && rawOffset >= 0 && rawOffset < fs.Length)
                    {
                        //Read resource
                        fs.Seek(rawOffset, SeekOrigin.Begin);
                        if (!entry.Resources.Contains(rawOffset))
                            entry.Resources.AddResource(rawOffset, reader.ReadBytes(rawSize));
                    }

                    //Goto resource description
                    tagReader.BaseStream.Seek(bitmapDatas.Offset + (i * 116) + 44, SeekOrigin.Begin);
                    rawOffset = tagReader.ReadInt32();
                    tagReader.BaseStream.Seek(bitmapDatas.Offset + (i * 116) + 68, SeekOrigin.Begin);
                    rawSize = tagReader.ReadInt32();

                    //Check
                    if ((rawOffset & 0xC0000000) == 0 && rawOffset >= 0 && rawOffset < fs.Length)
                    {
                        //Read resource
                        fs.Seek(rawOffset, SeekOrigin.Begin);
                        if (!entry.Resources.Contains(rawOffset))
                            entry.Resources.AddResource(rawOffset, reader.ReadBytes(rawSize));
                    }

                    //Goto resource description
                    tagReader.BaseStream.Seek(bitmapDatas.Offset + (i * 116) + 48, SeekOrigin.Begin);
                    rawOffset = tagReader.ReadInt32();
                    tagReader.BaseStream.Seek(bitmapDatas.Offset + (i * 116) + 72, SeekOrigin.Begin);
                    rawSize = tagReader.ReadInt32();

                    //Check
                    if ((rawOffset & 0xC0000000) == 0 && rawOffset >= 0 && rawOffset < fs.Length)
                    {
                        //Read resource
                        fs.Seek(rawOffset, SeekOrigin.Begin);
                        if (!entry.Resources.Contains(rawOffset))
                            entry.Resources.AddResource(rawOffset, reader.ReadBytes(rawSize));
                    }
                }
            }
        }
        private void ReadUnicodeStringTable(IndexEntry globals, Stream fs, BinaryReader reader, BinaryReader tagReader, long offset, string[] strings, out StringEntry[] stringTable)
        {
            //Read
            tagReader.BaseStream.Seek(globals.Address + offset, SeekOrigin.Begin);
            int count = tagReader.ReadInt32();
            int tableLength = tagReader.ReadInt32();
            int indexOffset = tagReader.ReadInt32();
            int tableOffset = tagReader.ReadInt32();

            //Prepare
            StringObject[] stringObjects = new StringObject[count];
            stringTable = new StringEntry[count];

            //Read Index
            fs.Seek(indexOffset, SeekOrigin.Begin);
            for (int i = 0; i < count; i++)
            {
                stringObjects[i] = reader.Read<StringObject>();
                stringTable[i] = new StringEntry() { ID = strings[stringObjects[i].StringID.Index] };
            }

            //Read Strings
            for (int i = 0; i < count; i++)
            {
                fs.Seek(tableOffset + stringObjects[i].Offset, SeekOrigin.Begin);
                stringTable[i].Value = reader.ReadUTF8NullTerminated();
            }
        }
        private void WriteSoundGestaltSoundData(IndexEntry entry, Stream fs, BinaryWriter writer)
        {
            //Create reader
            using (BinaryReader tagReader = entry.Data.GetVirtualStream().CreateReader())
            using (BinaryWriter tagWriter = entry.Data.GetVirtualStream().CreateWriter())
            {
                //Read sounds
                tagReader.BaseStream.Seek(entry.Address + 64, SeekOrigin.Begin);
                TagBlock sounds = tagReader.Read<TagBlock>();
                for (int i = 0; i < sounds.Count; i++)
                {
                    //Goto resource description
                    tagReader.BaseStream.Seek(sounds.Offset + (i * 12), SeekOrigin.Begin);
                    int rawOffset = tagReader.ReadInt32();

                    //Check
                    if (entry.Resources.TryGetResource(rawOffset, out HaloMapDataContainer resource))
                    {
                        //Set raw offset
                        int rawSize = resource.Length;
                        rawOffset = (int)fs.Align(512);

                        //Write resource to file
                        writer.Write(resource.GetBuffer());

                        //Goto resource description
                        tagReader.BaseStream.Seek(sounds.Offset + (i * 12), SeekOrigin.Begin);
                        tagWriter.Write(rawOffset);
                        tagWriter.Write(rawSize);
                    }
                }
            }
        }
        private void WriteRenderModelData(IndexEntry entry, Stream fs, BinaryWriter writer)
        {
            //Create reader
            using (BinaryReader tagReader = entry.Data.GetVirtualStream().CreateReader())
            using (BinaryWriter tagWriter = entry.Data.GetVirtualStream().CreateWriter())
            {
                //Read sections
                tagReader.BaseStream.Seek(entry.Address + 36, SeekOrigin.Begin);
                TagBlock sections = tagReader.Read<TagBlock>();
                for (int i = 0; i < sections.Count; i++)
                {
                    //Goto resource description
                    tagReader.BaseStream.Seek(sections.Offset + (i * 92) + 56, SeekOrigin.Begin);
                    int rawOffset = tagReader.ReadInt32();

                    //Check
                    if (entry.Resources.TryGetResource(rawOffset, out HaloMapDataContainer resource))
                    {
                        //Set raw offset
                        int rawSize = resource.Length;
                        rawOffset = (int)fs.Align(512);

                        //Write resource to file
                        writer.Write(resource.GetBuffer());

                        //Goto resource description
                        tagReader.BaseStream.Seek(sections.Offset + (i * 92) + 56, SeekOrigin.Begin);
                        tagWriter.Write(rawOffset);
                        tagWriter.Write(rawSize);
                    }
                }

                //Read prt info
                tagReader.BaseStream.Seek(entry.Address + 116, SeekOrigin.Begin);
                TagBlock prtInfos = tagReader.Read<TagBlock>();
                for (int i = 0; i < prtInfos.Count; i++)
                {
                    //Goto resource description
                    tagReader.BaseStream.Seek(prtInfos.Offset + (i * 88) + 52, SeekOrigin.Begin);
                    int rawOffset = tagReader.ReadInt32();

                    //Check
                    if (entry.Resources.TryGetResource(rawOffset, out HaloMapDataContainer resource))
                    {
                        //Set raw offset
                        int rawSize = resource.Length;
                        rawOffset = (int)fs.Align(512);

                        //Write resource to file
                        writer.Write(resource.GetBuffer());

                        //Goto resource description
                        tagReader.BaseStream.Seek(prtInfos.Offset + (i * 88) + 52, SeekOrigin.Begin);
                        tagWriter.Write(rawOffset);
                        tagWriter.Write(rawSize);
                    }
                }
            }
        }
        private void WriteScenarioStructureBspData(IndexEntry entry, Stream fs, BinaryWriter writer)
        {
            //Create reader
            using (BinaryReader tagReader = entry.Data.GetVirtualStream().CreateReader())
            using (BinaryWriter tagWriter = entry.Data.GetVirtualStream().CreateWriter())
            {
                //Read clusters
                tagReader.BaseStream.Seek(entry.Address + 156, SeekOrigin.Begin);
                TagBlock clusters = tagReader.Read<TagBlock>();
                for (int i = 0; i < clusters.Count; i++)
                {
                    //Goto resource description
                    tagReader.BaseStream.Seek(clusters.Offset + (i * 176) + 40, SeekOrigin.Begin);
                    int rawOffset = tagReader.ReadInt32();

                    //Check
                    if (entry.Resources.TryGetResource(rawOffset, out HaloMapDataContainer resource))
                    {
                        //Set raw offset
                        int rawSize = resource.Length;
                        rawOffset = (int)fs.Align(512);

                        //Write resource to file
                        writer.Write(resource.GetBuffer());

                        //Goto resource description
                        tagReader.BaseStream.Seek(clusters.Offset + (i * 176) + 40, SeekOrigin.Begin);
                        tagWriter.Write(rawOffset);
                        tagWriter.Write(rawSize);
                    }
                }

                //Read geometries definitions
                tagReader.BaseStream.Seek(entry.Address + 312, SeekOrigin.Begin);
                TagBlock geometryDefinitions = tagReader.Read<TagBlock>();
                for (int i = 0; i < geometryDefinitions.Count; i++)
                {
                    //Goto resource description
                    tagReader.BaseStream.Seek(geometryDefinitions.Offset + (i * 200) + 40, SeekOrigin.Begin);
                    int rawOffset = tagReader.ReadInt32();

                    //Check
                    if (entry.Resources.TryGetResource(rawOffset, out HaloMapDataContainer resource))
                    {
                        //Set raw offset
                        int rawSize = resource.Length;
                        rawOffset = (int)fs.Align(512);

                        //Write resource to file
                        writer.Write(resource.GetBuffer());

                        //Goto resource description
                        tagReader.BaseStream.Seek(geometryDefinitions.Offset + (i * 200) + 40, SeekOrigin.Begin);
                        tagWriter.Write(rawOffset);
                        tagWriter.Write(rawSize);
                    }
                }

                //Read water definitions
                tagReader.BaseStream.Seek(entry.Address + 532, SeekOrigin.Begin);
                TagBlock waterDefinitions = tagReader.Read<TagBlock>();
                for (int i = 0; i < waterDefinitions.Count; i++)
                {
                    //Goto resource description
                    tagReader.BaseStream.Seek(waterDefinitions.Offset + (i * 172) + 16, SeekOrigin.Begin);
                    int rawOffset = tagReader.ReadInt32();

                    //Check
                    if (entry.Resources.TryGetResource(rawOffset, out HaloMapDataContainer resource))
                    {
                        //Set raw offset
                        int rawSize = resource.Length;
                        rawOffset = (int)fs.Align(512);

                        //Write resource to file
                        writer.Write(resource.GetBuffer());

                        //Goto resource description
                        tagReader.BaseStream.Seek(waterDefinitions.Offset + (i * 172) + 16, SeekOrigin.Begin);
                        tagWriter.Write(rawOffset);
                        tagWriter.Write(rawSize);
                    }
                }

                //Read decorators definitions
                tagReader.BaseStream.Seek(entry.Address + 564, SeekOrigin.Begin);
                TagBlock decorators = tagReader.Read<TagBlock>();
                for (int i = 0; i < decorators.Count; i++)
                {
                    //Goto caches
                    tagReader.BaseStream.Seek(decorators.Offset + (i * 48) + 16, SeekOrigin.Begin);
                    TagBlock caches = tagReader.Read<TagBlock>();
                    for (int j = 0; j < caches.Count; j++)
                    {
                        //Goto resource description
                        tagReader.BaseStream.Seek(caches.Offset + (j * 44), SeekOrigin.Begin);
                        int rawOffset = tagReader.ReadInt32();

                        //Check
                        if (entry.Resources.TryGetResource(rawOffset, out HaloMapDataContainer resource))
                        {
                            //Set raw offset
                            int rawSize = resource.Length;
                            rawOffset = (int)fs.Align(512);

                            //Write resource to file
                            writer.Write(resource.GetBuffer());

                            //Goto resource description
                            tagReader.BaseStream.Seek(caches.Offset + (j * 44), SeekOrigin.Begin);
                            tagWriter.Write(rawOffset);
                            tagWriter.Write(rawSize);
                        }
                    }
                }
            }
        }
        private void WriteScenarioStructureLightmapData(IndexEntry entry, Stream fs, BinaryWriter writer)
        {
            //Create reader
            using (BinaryReader tagReader = entry.Data.GetVirtualStream().CreateReader())
            using (BinaryWriter tagWriter = entry.Data.GetVirtualStream().CreateWriter())
            {
                //Read decorators definitions
                tagReader.BaseStream.Seek(entry.Address + 128, SeekOrigin.Begin);
                TagBlock groups = tagReader.Read<TagBlock>();
                for (int i = 0; i < groups.Count; i++)
                {
                    //Goto clusters
                    tagReader.BaseStream.Seek(groups.Offset + (i * 104) + 32, SeekOrigin.Begin);
                    TagBlock clusters = tagReader.Read<TagBlock>();
                    for (int j = 0; j < clusters.Count; j++)
                    {
                        //Goto resource description
                        tagReader.BaseStream.Seek(clusters.Offset + (j * 84) + 40, SeekOrigin.Begin);
                        int rawOffset = tagReader.ReadInt32();

                        //Check
                        if (entry.Resources.TryGetResource(rawOffset, out HaloMapDataContainer resource))
                        {
                            //Set raw offset
                            int rawSize = resource.Length;
                            rawOffset = (int)fs.Align(512);

                            //Write resource to file
                            writer.Write(resource.GetBuffer());

                            //Goto resource description
                            tagReader.BaseStream.Seek(clusters.Offset + (j * 84) + 40, SeekOrigin.Begin);
                            tagWriter.Write(rawOffset);
                            tagWriter.Write(rawSize);
                        }
                    }

                    //Goto poop definitions
                    tagReader.BaseStream.Seek(groups.Offset + (i * 104) + 48, SeekOrigin.Begin);
                    TagBlock poops = tagReader.Read<TagBlock>();
                    for (int j = 0; j < poops.Count; j++)
                    {
                        //Goto resource description
                        tagReader.BaseStream.Seek(poops.Offset + (j * 84) + 40, SeekOrigin.Begin);
                        int rawOffset = tagReader.ReadInt32();

                        //Check
                        if (entry.Resources.TryGetResource(rawOffset, out HaloMapDataContainer resource))
                        {
                            //Set raw offset
                            int rawSize = resource.Length;
                            rawOffset = (int)fs.Align(512);

                            //Write resource to file
                            writer.Write(resource.GetBuffer());

                            //Goto resource description
                            tagReader.BaseStream.Seek(poops.Offset + (j * 84) + 40, SeekOrigin.Begin);
                            tagWriter.Write(rawOffset);
                            tagWriter.Write(rawSize);
                        }
                    }

                    //Goto geometry buckets
                    tagReader.BaseStream.Seek(groups.Offset + (i * 104) + 64, SeekOrigin.Begin);
                    TagBlock buckets = tagReader.Read<TagBlock>();
                    for (int j = 0; j < buckets.Count; j++)
                    {
                        //Goto resource description
                        tagReader.BaseStream.Seek(buckets.Offset + (j * 56) + 12, SeekOrigin.Begin);
                        int rawOffset = tagReader.ReadInt32();

                        //Check
                        if (entry.Resources.TryGetResource(rawOffset, out HaloMapDataContainer resource))
                        {
                            //Set raw offset
                            int rawSize = resource.Length;
                            rawOffset = (int)fs.Align(512);

                            //Write resource to file
                            writer.Write(resource.GetBuffer());

                            //Goto resource description
                            tagReader.BaseStream.Seek(buckets.Offset + (j * 56) + 12, SeekOrigin.Begin);
                            tagWriter.Write(rawOffset);
                            tagWriter.Write(rawSize);
                        }
                    }
                }
            }
        }
        private void WriteWeatherSystemData(IndexEntry entry, Stream fs, BinaryWriter writer)
        {
            //Create reader
            using (BinaryReader tagReader = entry.Data.GetVirtualStream().CreateReader())
            using (BinaryWriter tagWriter = entry.Data.GetVirtualStream().CreateWriter())
            {
                //Read particle systems
                tagReader.BaseStream.Seek(entry.Address, SeekOrigin.Begin);
                TagBlock particleSystems = tagReader.Read<TagBlock>();
                for (int i = 0; i < particleSystems.Count; i++)
                {
                    //Goto resource description
                    tagReader.BaseStream.Seek(particleSystems.Offset + (i * 140) + 64, SeekOrigin.Begin);
                    int rawOffset = tagReader.ReadInt32();

                    //Check
                    if (entry.Resources.TryGetResource(rawOffset, out HaloMapDataContainer resource))
                    {
                        //Set raw offset
                        int rawSize = resource.Length;
                        rawOffset = (int)fs.Align(512);

                        //Write resource to file
                        writer.Write(resource.GetBuffer());

                        //Goto resource description
                        tagReader.BaseStream.Seek(particleSystems.Offset + (i * 140) + 64, SeekOrigin.Begin);
                        tagWriter.Write(rawOffset);
                        tagWriter.Write(rawSize);
                    }
                }
            }
        }
        private void WriteDecoratorSetData(IndexEntry entry, Stream fs, BinaryWriter writer)
        {
            //Create reader
            using (BinaryReader tagReader = entry.Data.GetVirtualStream().CreateReader())
            using (BinaryWriter tagWriter = entry.Data.GetVirtualStream().CreateWriter())
            {
                //Goto resource description
                tagReader.BaseStream.Seek(entry.Address + 56, SeekOrigin.Begin);
                int rawOffset = tagReader.ReadInt32();

                //Check
                if (entry.Resources.TryGetResource(rawOffset, out HaloMapDataContainer resource))
                {
                    //Set raw offset
                    int rawSize = resource.Length;
                    rawOffset = (int)fs.Align(512);

                    //Write resource to file
                    writer.Write(resource.GetBuffer());

                    //Goto resource description
                    tagReader.BaseStream.Seek(entry.Address + 56, SeekOrigin.Begin);
                    tagWriter.Write(rawOffset);
                    tagWriter.Write(rawSize);
                }
            }
        }
        private void WriteParticleModelData(IndexEntry entry, Stream fs, BinaryWriter writer)
        {
            //Create reader
            using (BinaryReader tagReader = entry.Data.GetVirtualStream().CreateReader())
            using (BinaryWriter tagWriter = entry.Data.GetVirtualStream().CreateWriter())
            {
                //Goto resource description
                tagReader.BaseStream.Seek(entry.Address + 160, SeekOrigin.Begin);
                int rawOffset = tagReader.ReadInt32();

                //Check
                if (entry.Resources.TryGetResource(rawOffset, out HaloMapDataContainer resource))
                {
                    //Set raw offset
                    int rawSize = resource.Length;
                    rawOffset = (int)fs.Align(512);

                    //Write resource to file
                    writer.Write(resource.GetBuffer());

                    //Goto resource description
                    tagReader.BaseStream.Seek(entry.Address + 160, SeekOrigin.Begin);
                    tagWriter.Write(rawOffset);
                    tagWriter.Write(rawSize);
                }
            }
        }
        private void WriteSoundGestaltExtraInfosData(IndexEntry entry, Stream fs, BinaryWriter writer)
        {
            //Create reader
            using (BinaryReader tagReader = entry.Data.GetVirtualStream().CreateReader())
            using (BinaryWriter tagWriter = entry.Data.GetVirtualStream().CreateWriter())
            {
                //Read extra infos
                tagReader.BaseStream.Seek(entry.Address + 80, SeekOrigin.Begin);
                TagBlock extraInfos = tagReader.Read<TagBlock>();
                for (int i = 0; i < extraInfos.Count; i++)
                {
                    //Goto resource description
                    tagReader.BaseStream.Seek(extraInfos.Offset + (i * 44) + 8, SeekOrigin.Begin);
                    int rawOffset = tagReader.ReadInt32();

                    //Check
                    if (entry.Resources.TryGetResource(rawOffset, out HaloMapDataContainer resource))
                    {
                        //Set raw offset
                        int rawSize = resource.Length;
                        rawOffset = (int)fs.Align(512);

                        //Write resource to file
                        writer.Write(resource.GetBuffer());

                        //Goto resource description
                        tagReader.BaseStream.Seek(extraInfos.Offset + (i * 44) + 8, SeekOrigin.Begin);
                        tagWriter.Write(rawOffset);
                        tagWriter.Write(rawSize);
                    }
                }
            }
        }
        private void WriteModelAnimationData(IndexEntry entry, Stream fs, BinaryWriter writer)
        {
            //Create reader
            using (BinaryReader tagReader = entry.Data.GetVirtualStream().CreateReader())
            using (BinaryWriter tagWriter = entry.Data.GetVirtualStream().CreateWriter())
            {
                //Read animations
                tagReader.BaseStream.Seek(entry.Address + 172, SeekOrigin.Begin);
                TagBlock animations = tagReader.Read<TagBlock>();
                for (int i = 0; i < animations.Count; i++)
                {
                    //Goto resource description
                    tagReader.BaseStream.Seek(animations.Offset + (i * 20) + 8, SeekOrigin.Begin);
                    int rawOffset = tagReader.ReadInt32();

                    //Check
                    if (entry.Resources.TryGetResource(rawOffset, out HaloMapDataContainer resource))
                    {
                        //Set raw offset
                        int rawSize = resource.Length;
                        rawOffset = (int)fs.Align(512);

                        //Write resource to file
                        writer.Write(resource.GetBuffer());

                        //Goto resource description
                        tagReader.BaseStream.Seek(animations.Offset + (i * 20) + 4, SeekOrigin.Begin);
                        tagWriter.Write(rawSize);
                        tagWriter.Write(rawOffset);
                    }
                }
            }
        }
        private void WriteBitmapData(IndexEntry entry, Stream fs, BinaryWriter writer)
        {
            //Create reader
            using (BinaryReader tagReader = entry.Data.GetVirtualStream().CreateReader())
            using (BinaryWriter tagWriter = entry.Data.GetVirtualStream().CreateWriter())
            {
                //Prepare
                HaloMapDataContainer resource = new HaloMapDataContainer();

                //Read bitmap datas
                tagReader.BaseStream.Seek(entry.Address + 68, SeekOrigin.Begin);
                TagBlock bitmapDatas = tagReader.Read<TagBlock>();
                for (int i = 0; i < bitmapDatas.Count; i++)
                {
                    //Goto resource description 1
                    tagReader.BaseStream.Seek(bitmapDatas.Offset + (i * 116) + 28, SeekOrigin.Begin);
                    int rawOffset = tagReader.ReadInt32();

                    //Check
                    if (entry.Resources.TryGetResource(rawOffset, out resource))
                    {
                        //Set raw offset
                        int rawSize = resource.Length;
                        rawOffset = (int)fs.Align(512);

                        //Write resource to file
                        writer.Write(resource.GetBuffer());

                        //Goto resource description 1
                        tagWriter.BaseStream.Seek(bitmapDatas.Offset + (i * 116) + 28, SeekOrigin.Begin);
                        tagWriter.Write(rawOffset);
                        tagWriter.BaseStream.Seek(bitmapDatas.Offset + (i * 116) + 52, SeekOrigin.Begin);
                        tagWriter.Write(rawSize);
                    }

                    //Goto resource description 2
                    tagReader.BaseStream.Seek(bitmapDatas.Offset + (i * 116) + 32, SeekOrigin.Begin);
                    rawOffset = tagReader.ReadInt32();

                    //Check
                    if (entry.Resources.TryGetResource(rawOffset, out resource))
                    {
                        //Set raw offset
                        int rawSize = resource.Length;
                        rawOffset = (int)fs.Align(512);

                        //Write resource to file
                        writer.Write(resource.GetBuffer());

                        //Goto resource description 2
                        tagWriter.BaseStream.Seek(bitmapDatas.Offset + (i * 116) + 32, SeekOrigin.Begin);
                        tagWriter.Write(rawOffset);
                        tagWriter.BaseStream.Seek(bitmapDatas.Offset + (i * 116) + 56, SeekOrigin.Begin);
                        tagWriter.Write(rawSize);
                    }

                    //Goto resource description 3
                    tagReader.BaseStream.Seek(bitmapDatas.Offset + (i * 116) + 36, SeekOrigin.Begin);
                    rawOffset = tagReader.ReadInt32();

                    //Check
                    if (entry.Resources.TryGetResource(rawOffset, out resource))
                    {
                        //Set raw offset
                        int rawSize = resource.Length;
                        rawOffset = (int)fs.Align(512);

                        //Write resource to file
                        writer.Write(resource.GetBuffer());

                        //Goto resource description 3
                        tagWriter.BaseStream.Seek(bitmapDatas.Offset + (i * 116) + 36, SeekOrigin.Begin);
                        tagWriter.Write(rawOffset);
                        tagWriter.BaseStream.Seek(bitmapDatas.Offset + (i * 116) + 60, SeekOrigin.Begin);
                        tagWriter.Write(rawSize);
                    }

                    //Write offset 0xffffffff and length 0 for lod 4-6
                    tagWriter.BaseStream.Seek(bitmapDatas.Offset + (i * 116) + 40, SeekOrigin.Begin);
                    tagWriter.Write(uint.MaxValue);
                    tagWriter.Write(uint.MaxValue);
                    tagWriter.Write(uint.MaxValue);
                    tagWriter.BaseStream.Seek(bitmapDatas.Offset + (i * 116) + 64, SeekOrigin.Begin);
                    tagWriter.Write(0);
                    tagWriter.Write(0);
                    tagWriter.Write(0);
                }
            }
        }
    }
}
