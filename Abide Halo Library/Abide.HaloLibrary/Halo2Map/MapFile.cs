using Abide.HaloLibrary.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace Abide.HaloLibrary.Halo2Map
{
    /// <summary>
    /// Represents a Halo 2 binary map file.
    /// </summary>
    [Serializable]
    public sealed class MapFile : MarshalByRefObject, IDisposable
    {
        /// <summary>
        /// Represents the minimum length of a Halo 2 map file.
        /// This value is the sum of the length of a <see cref="HEADER"/> structure, and the minimum length of the index table.
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
        /// Gets and returns the Halo 2 map's index entry list.
        /// </summary>
        [Browsable(false)]
        public IndexEntryList IndexEntries
        {
            get { return indexList; }
        }
        /// <summary>
        /// Gets and returns the stream containing the tags' data.
        /// </summary>
        [Browsable(false)]
        public FixedMemoryMappedStream TagDataStream
        {
            get { return tagData; }
        }
        /// <summary>
        /// Gets and returns a list of the Halo 2 map's string IDs.
        /// </summary>
        [Category("Map Data"), Description("The Halo 2 map's string identifiers.")]
        public List<string> Strings
        {
            get { return strings; }
        }
        /// <summary>
        /// Gets and returns a list of the Halo 2 map's tag hierarchies.
        /// </summary>
        [Browsable(false)]
        public TagHierarchyList Tags
        {
            get { return tags; }
        }

        private HEADER header;
        private FixedMemoryMappedStream[] sbspTagDatas;
        private List<string> strings;
        private FixedMemoryMappedStream crazyData;
        private INDEX index;
        private TagHierarchyList tags;
        private FixedMemoryMappedStream tagData;
        private IndexEntryList indexList;
        private Dictionary<TAGID, int> bspIndexLookup;

        /// <summary>
        /// Initializes a new Halo 2 map file instance.
        /// </summary>
        public MapFile()
        {
            //Initialize
            header = HEADER.Create();
            index = INDEX.Create();
            sbspTagDatas = new FixedMemoryMappedStream[0];
            strings = new List<string>();
            crazyData = FixedMemoryMappedStream.Empty;
            tags = new TagHierarchyList();
            indexList = new IndexEntryList();
            tagData = FixedMemoryMappedStream.Empty;
            bspIndexLookup = new Dictionary<TAGID, int>();
        }
        /// <summary>
        /// Loads a Halo 2 map file from a specified file path.
        /// </summary>
        /// <param name="filename">A relative or absolute path for the map file.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="MapFileExcption"></exception>
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
            catch (Exception ex) { throw new MapFileExcption(ex); }
        }
        /// <summary>
        /// Loads a Halo 2 map file from the specified stream.
        /// </summary>
        /// <param name="inStream">The stream containing the Halo 2 map.</param>
        /// <exception cref="MapFileExcption"></exception>
        public void Load(Stream inStream)
        {
            //Check file...
            if (inStream.Length < 6144)
                throw new MapFileExcption("Invalid map file.");

            try
            {
                //Create Reader
                using (BinaryReader reader = new BinaryReader(inStream))
                {
                    //Read Header
                    header = reader.ReadStructure<HEADER>();
                    if (header.HeaderTag == HaloTags.head && header.FooterTag == HaloTags.foot)    //Quick sanity check...
                    {
                        //Read File Names
                        string[] files = reader.ReadUTF8StringTable(header.FilesOffset, header.FilesIndex, header.FileCount);

                        //Read Strings
                        strings = new List<string>(reader.ReadUTF8StringTable(header.StringsOffset, header.StringsIndexOffset, header.StringCount));

                        //Read Index
                        inStream.Seek(header.IndexOffset, SeekOrigin.Begin);
                        index = reader.ReadStructure<INDEX>();

                        //Read Tags
                        TAGHIERARCHY[] tags = new TAGHIERARCHY[index.TagCount];
                        for (int i = 0; i < index.TagCount; i++)
                            tags[i] = reader.ReadStructure<TAGHIERARCHY>();
                        this.tags = new TagHierarchyList(tags);

                        //Read Objects
                        inStream.Seek((index.ObjectOffset - index.IndexAddress) + (header.IndexOffset + INDEX.Length), SeekOrigin.Begin);
                        OBJECT objectEntry = new OBJECT(); IndexEntry[] indexEntries = new IndexEntry[index.ObjectCount];
                        for (int i = 0; i < index.ObjectCount; i++)
                        {
                            objectEntry = reader.ReadStructure<OBJECT>();
                            indexEntries[i] = new IndexEntry(objectEntry, files[i], this.tags[objectEntry.Tag]);
                        }

                        //Setup Index List
                        indexList = new IndexEntryList(indexEntries);

                        //Check
                        if (indexList.Last.Root != HaloTags.ugh_)
                            throw new MapFileExcption("Final tag is not coconuts");

                        //Read Crazy
                        inStream.Seek(header.CrazyOffset, SeekOrigin.Begin);
                        crazyData = new FixedMemoryMappedStream(reader.ReadBytes(header.CrazyLength));

                        //Get Meta Memory-file address
                        uint metaFileMemoryAddress = 0, metaMemoryAddress = 0;
                        if (index.ObjectCount > 0)
                        {
                            metaFileMemoryAddress = (uint)(indexList[0].Offset - (header.IndexOffset + header.IndexLength));
                            metaMemoryAddress = (uint)indexList[0].Offset;
                        }

                        //Read Meta
                        inStream.Seek(header.IndexOffset + header.IndexLength, SeekOrigin.Begin);
                        tagData = new FixedMemoryMappedStream(reader.ReadBytes(header.MetaLength), metaMemoryAddress);

                        //Loop
                        foreach (IndexEntry entry in indexList)
                            if (entry.Offset != 0)
                                entry.TagData = tagData;

                        //Prepare BSP start...
                        int bspStart = header.Strings128Offset;

                        //Prepare
                        StringEntry[] en = null, jp = null, nl = null, fr = null, es = null, it = null, kr = null, zh = null, pr = null;

                        //Read Strings
                        foreach (var indexEntry in indexList)
                            if (indexEntry.ID == index.GlobalsID)
                            {
                                //Read English Strings
                                ReadStringTable(inStream, reader, indexEntry, metaFileMemoryAddress, 400, out en);

                                //Read Japanese Strings
                                ReadStringTable(inStream, reader, indexEntry, metaFileMemoryAddress, 428, out jp);

                                //Read Dutch Strings
                                ReadStringTable(inStream, reader, indexEntry, metaFileMemoryAddress, 456, out nl);

                                //Read French Strings
                                ReadStringTable(inStream, reader, indexEntry, metaFileMemoryAddress, 484, out fr);

                                //Read Spanish Strings
                                ReadStringTable(inStream, reader, indexEntry, metaFileMemoryAddress, 512, out es);

                                //Read Italian Strings
                                ReadStringTable(inStream, reader, indexEntry, metaFileMemoryAddress, 540, out it);

                                //Read Korean Strings
                                ReadStringTable(inStream, reader, indexEntry, metaFileMemoryAddress, 568, out kr);

                                //Read Chinese Strings
                                ReadStringTable(inStream, reader, indexEntry, metaFileMemoryAddress, 596, out zh);

                                //Read Portuguese Strings
                                ReadStringTable(inStream, reader, indexEntry, metaFileMemoryAddress, 624, out pr);

                                //Break
                                break;
                            }

                        //Read BSP Meta
                        if (indexList.ContainsID(index.ScenarioID))
                        {
                            //Goto
                            inStream.Seek(indexList[index.ScenarioID].Offset + 528, metaFileMemoryAddress, SeekOrigin.Begin);
                            TAGBLOCK structureBsps = reader.ReadUInt64();
                            sbspTagDatas = new FixedMemoryMappedStream[structureBsps.Count];
                            for (int i = 0; i < structureBsps.Count; i++)
                            {
                                //Goto
                                inStream.Seek(structureBsps.Offset + (i * 68), metaFileMemoryAddress, SeekOrigin.Begin);
                                int sbspOffset = reader.ReadInt32();
                                int sbspSize = reader.ReadInt32();
                                int bspMagic = reader.ReadInt32();
                                int bspFileMagic = bspMagic - sbspOffset;
                                inStream.Seek(4, SeekOrigin.Current);
                                TAG sbspTag = reader.ReadStructure<TAG>();
                                TAGID sbspId = reader.ReadStructure<TAGID>();
                                TAG ltmpTag = reader.ReadStructure<TAG>();
                                TAGID ltmpId = reader.ReadStructure<TAGID>();

                                //Add
                                bspIndexLookup.Add(sbspId, i);

                                //Goto
                                inStream.Seek(sbspOffset, SeekOrigin.Begin);
                                sbspTagDatas[i] = new FixedMemoryMappedStream(reader.ReadBytes(sbspSize), (uint)bspMagic);
                                SBSPHEADER sbspHeader = new SBSPHEADER();
                                using (BinaryReader bspHeaderReader = new BinaryReader(sbspTagDatas[i]))
                                    sbspHeader = bspHeaderReader.ReadStructure<SBSPHEADER>();

                                //Setup SBSP and Lightmap
                                if (indexList.ContainsID(sbspId))
                                {
                                    indexList[sbspId].TagData = sbspTagDatas[i];
                                    indexList[sbspId].PostProcessedOffset = bspMagic;
                                    indexList[sbspId].PostProcessedSize = sbspSize;
                                }
                                if (indexList.ContainsID(ltmpId))
                                {
                                    indexList[ltmpId].TagData = sbspTagDatas[i];
                                    indexList[ltmpId].PostProcessedOffset = sbspHeader.LightmapOffset;
                                    indexList[ltmpId].PostProcessedSize = sbspHeader.DataLength - (sbspHeader.LightmapOffset - bspMagic);
                                }

                                //Check
                                if (sbspOffset < bspStart)
                                    bspStart = sbspOffset;
                            }
                        }

                        //Read Strings into unicode tags
                        foreach (var unicode in indexList.Where(e => e.Root == HaloTags.unic))
                            using (BinaryReader metaReader = new BinaryReader(unicode.TagData))
                            {
                                //Prepare
                                ushort offset, count;
                                unicode.TagData.Seek(unicode.Offset + 16, SeekOrigin.Begin);

                                //Read English
                                offset = metaReader.ReadUInt16();
                                count = metaReader.ReadUInt16();
                                unicode.Strings.English.AddRange(en.Where((s, i) => i >= offset && i < offset + count));

                                //Read Japanese
                                offset = metaReader.ReadUInt16();
                                count = metaReader.ReadUInt16();
                                unicode.Strings.Japanese.AddRange(jp.Where((s, i) => i >= offset && i < offset + count));

                                //Read German
                                offset = metaReader.ReadUInt16();
                                count = metaReader.ReadUInt16();
                                unicode.Strings.German.AddRange(nl.Where((s, i) => i >= offset && i < offset + count));

                                //Read French
                                offset = metaReader.ReadUInt16();
                                count = metaReader.ReadUInt16();
                                unicode.Strings.French.AddRange(fr.Where((s, i) => i >= offset && i < offset + count));

                                //Read Spanish
                                offset = metaReader.ReadUInt16();
                                count = metaReader.ReadUInt16();
                                unicode.Strings.Spanish.AddRange(es.Where((s, i) => i >= offset && i < offset + count));

                                //Read Italian
                                offset = metaReader.ReadUInt16();
                                count = metaReader.ReadUInt16();
                                unicode.Strings.Italian.AddRange(it.Where((s, i) => i >= offset && i < offset + count));

                                //Read Korean
                                offset = metaReader.ReadUInt16();
                                count = metaReader.ReadUInt16();
                                unicode.Strings.Korean.AddRange(kr.Where((s, i) => i >= offset && i < offset + count));

                                //Read Chinese
                                offset = metaReader.ReadUInt16();
                                count = metaReader.ReadUInt16();
                                unicode.Strings.Chinese.AddRange(zh.Where((s, i) => i >= offset && i < offset + count));

                                //Read Portuguese
                                offset = metaReader.ReadUInt16();
                                count = metaReader.ReadUInt16();
                                unicode.Strings.Portuguese.AddRange(pr.Where((s, i) => i >= offset && i < offset + count));
                            }

                        //Zero-out variables
                        header.FileLength = 0;
                        header.NonRawLength = 0;
                        index.ObjectOffset = 0;

                        //Read Raws
                        ReadRaws(inStream, reader);
                    }
                    else throw new MapFileExcption("Invalid map header.");
                }
            }
            catch (Exception ex) { throw new MapFileExcption(ex); }
        }
        /// <summary>
        /// Attempts to read any internal raw data reference by the supplied tag group.
        /// </summary>
        /// <param name="inStream">The Halo 2 cached map file stream.</param>
        /// <param name="reader">The reader for the map stream.</param>
        /// <param name="tagReader">The reader for the tag data stream.</param>
        private void ReadRaws(Stream inStream, BinaryReader reader)
        {
            //Null
            RawStream rawData = null;
            int rawOffset, rawSize;
            long offsetAddress, lengthAddress;

            //Handle objects...
            foreach (IndexEntry entry in indexList)
            {
                //Clear
                entry.Raws.Clear();

                //Prepare
                using (BinaryReader metaReader = new BinaryReader(entry.TagData))
                using (BinaryWriter metaWriter = new BinaryWriter(entry.TagData))
                    switch (entry.Root)
                    {
                        #region ugh!
                        case HaloTags.ugh_:
                            entry.TagData.Seek(entry.Offset + 64, SeekOrigin.Begin);
                            int soundsCount = metaReader.ReadInt32();
                            int soundsOffset = metaReader.ReadInt32();
                            for (int i = 0; i < soundsCount; i++)
                            {
                                //Goto
                                entry.TagData.Seek(soundsOffset + (i * 12), SeekOrigin.Begin);
                                offsetAddress = entry.TagData.Position;
                                rawOffset = metaReader.ReadInt32();
                                lengthAddress = entry.TagData.Position;
                                rawSize = metaReader.ReadInt32() & 0x7FFFFFFF;   //Mask off the first bit

                                //Check
                                if ((rawOffset & 0xC0000000) == 0)
                                {
                                    //Read
                                    inStream.Seek(rawOffset, SeekOrigin.Begin);
                                    if (entry.Raws[RawSection.Sound].ContainsRawOffset(rawOffset))
                                        rawData = entry.Raws[RawSection.Sound][rawOffset];
                                    else { rawData = new RawStream(reader.ReadBytes(rawSize), rawOffset); entry.Raws[0].Add(rawData); }
                                    rawData.OffsetAddresses.Add(offsetAddress);
                                    rawData.LengthAddresses.Add(lengthAddress);
                                }
                            }

                            entry.TagData.Seek(entry.Offset + 80, SeekOrigin.Begin);
                            int extraInfosCount = metaReader.ReadInt32();
                            int extraInfosOffset = metaReader.ReadInt32();
                            for (int i = 0; i < extraInfosCount; i++)
                            {
                                //Goto
                                entry.TagData.Seek(extraInfosOffset + (i * 44) + 8, SeekOrigin.Begin);
                                offsetAddress = entry.TagData.Position;
                                rawOffset = metaReader.ReadInt32();
                                lengthAddress = entry.TagData.Position;
                                rawSize = metaReader.ReadInt32();

                                //Check
                                if ((rawOffset & 0xC0000000) == 0)
                                {
                                    //Read
                                    inStream.Seek(rawOffset, SeekOrigin.Begin);
                                    if (entry.Raws[RawSection.LipSync].ContainsRawOffset(rawOffset))
                                        rawData = entry.Raws[RawSection.LipSync][rawOffset];
                                    else { rawData = new RawStream(reader.ReadBytes(rawSize), rawOffset); entry.Raws[RawSection.BSP].Add(rawData); }
                                    rawData.OffsetAddresses.Add(offsetAddress);
                                    rawData.LengthAddresses.Add(lengthAddress);
                                }
                            }
                            break;
                        #endregion
                        #region mode
                        case HaloTags.mode:
                            entry.TagData.Seek(entry.Offset + 36, SeekOrigin.Begin);
                            int sectionCount = metaReader.ReadInt32();
                            int sectionOffset = metaReader.ReadInt32();
                            for (int i = 0; i < sectionCount; i++)
                            {
                                entry.TagData.Seek(sectionOffset + (i * 92) + 56, SeekOrigin.Begin);
                                offsetAddress = entry.TagData.Position;
                                rawOffset = metaReader.ReadInt32();
                                lengthAddress = entry.TagData.Position;
                                rawSize = metaReader.ReadInt32();

                                //Check
                                if ((rawOffset & 0xC0000000) == 0)
                                {
                                    //Read
                                    inStream.Seek(rawOffset, SeekOrigin.Begin);
                                    if (entry.Raws[RawSection.Model].ContainsRawOffset(rawOffset))
                                        rawData = entry.Raws[RawSection.Model][rawOffset];
                                    else { rawData = new RawStream(reader.ReadBytes(rawSize), rawOffset); entry.Raws[RawSection.Model].Add(rawData); }
                                    rawData.OffsetAddresses.Add(offsetAddress);
                                    rawData.LengthAddresses.Add(lengthAddress);
                                }
                            }

                            entry.TagData.Seek(entry.Offset + 116, SeekOrigin.Begin);
                            int prtCount = metaReader.ReadInt32();
                            int prtOffset = metaReader.ReadInt32();
                            for (int i = 0; i < prtCount; i++)
                            {
                                entry.TagData.Seek(prtOffset + (i * 88) + 52, SeekOrigin.Begin);
                                offsetAddress = entry.TagData.Position;
                                rawOffset = metaReader.ReadInt32();
                                lengthAddress = entry.TagData.Position;
                                rawSize = metaReader.ReadInt32();

                                //Check
                                if ((rawOffset & 0xC0000000) == 0)
                                {
                                    //Read
                                    inStream.Seek(rawOffset, SeekOrigin.Begin);
                                    if (entry.Raws[RawSection.Model].ContainsRawOffset(rawOffset))
                                        rawData = entry.Raws[RawSection.Model][rawOffset];
                                    else { rawData = new RawStream(reader.ReadBytes(rawSize), rawOffset); entry.Raws[RawSection.Model].Add(rawData); }
                                    rawData.OffsetAddresses.Add(offsetAddress);
                                    rawData.LengthAddresses.Add(lengthAddress);
                                }
                            }
                            break;
                        #endregion
                        #region weat
                        case HaloTags.weat:
                            entry.TagData.Seek(entry.Offset, SeekOrigin.Begin);
                            int particleSystemCount = metaReader.ReadInt32();
                            int particleSystemOffset = metaReader.ReadInt32();
                            for (int i = 0; i < particleSystemCount; i++)
                            {
                                entry.TagData.Seek(particleSystemOffset + (i * 140) + 64, SeekOrigin.Begin);
                                offsetAddress = entry.TagData.Position;
                                rawOffset = metaReader.ReadInt32();
                                lengthAddress = entry.TagData.Position;
                                rawSize = metaReader.ReadInt32();

                                //Check
                                if ((rawOffset & 0xC0000000) == 0)
                                {
                                    //Read
                                    inStream.Seek(rawOffset, SeekOrigin.Begin);
                                    if (entry.Raws[RawSection.Weather].ContainsRawOffset(rawOffset))
                                        rawData = entry.Raws[RawSection.Weather][rawOffset];
                                    else { rawData = new RawStream(reader.ReadBytes(rawSize), rawOffset); entry.Raws[RawSection.Weather].Add(rawData); }
                                    rawData.OffsetAddresses.Add(offsetAddress);
                                    rawData.LengthAddresses.Add(lengthAddress);
                                }
                            }
                            break;
                        #endregion
                        #region DECR
                        case HaloTags.DECR:
                            entry.TagData.Seek(entry.Offset + 56, SeekOrigin.Begin);
                            offsetAddress = entry.TagData.Position;
                            rawOffset = metaReader.ReadInt32();
                            lengthAddress = entry.TagData.Position;
                            rawSize = metaReader.ReadInt32();

                            //Check
                            if ((rawOffset & 0xC0000000) == 0)
                            {
                                //Read
                                inStream.Seek(rawOffset, SeekOrigin.Begin);
                                if (entry.Raws[RawSection.Decorator].ContainsRawOffset(rawOffset))
                                    rawData = entry.Raws[RawSection.Decorator][rawOffset];
                                else { rawData = new RawStream(reader.ReadBytes(rawSize), rawOffset); entry.Raws[RawSection.Decorator].Add(rawData); }
                                rawData.OffsetAddresses.Add(offsetAddress);
                                rawData.LengthAddresses.Add(lengthAddress);
                            }
                            break;
                        #endregion
                        #region PRTM
                        case HaloTags.PRTM:
                            entry.TagData.Seek(entry.Offset + 160, SeekOrigin.Begin);
                            offsetAddress = entry.TagData.Position;
                            rawOffset = metaReader.ReadInt32();
                            lengthAddress = entry.TagData.Position;
                            rawSize = metaReader.ReadInt32();

                            //Check
                            if ((rawOffset & 0xC0000000) == 0)
                            {
                                //Read
                                inStream.Seek(rawOffset, SeekOrigin.Begin);
                                if (entry.Raws[RawSection.ParticleModel].ContainsRawOffset(rawOffset))
                                    rawData = entry.Raws[RawSection.ParticleModel][rawOffset];
                                else { rawData = new RawStream(reader.ReadBytes(rawSize), rawOffset); entry.Raws[RawSection.ParticleModel].Add(rawData); }
                                rawData.OffsetAddresses.Add(offsetAddress);
                                rawData.LengthAddresses.Add(lengthAddress);
                            }
                            break;
                        #endregion
                        #region jmad
                        case HaloTags.jmad:
                            entry.TagData.Seek(entry.Offset + 172, SeekOrigin.Begin);
                            int animationCount = metaReader.ReadInt32();
                            int animationOffset = metaReader.ReadInt32();
                            for (int i = 0; i < animationCount; i++)
                            {
                                entry.TagData.Seek(animationOffset + (i * 20) + 4, SeekOrigin.Begin);
                                lengthAddress = entry.TagData.Position;
                                rawSize = metaReader.ReadInt32();
                                offsetAddress = entry.TagData.Position;
                                rawOffset = metaReader.ReadInt32();

                                //Check
                                if ((rawOffset & 0xC0000000) == 0)
                                {
                                    //Read
                                    inStream.Seek(rawOffset, SeekOrigin.Begin);
                                    if (entry.Raws[RawSection.Animation].ContainsRawOffset(rawOffset))
                                        rawData = entry.Raws[RawSection.Animation][rawOffset];
                                    else { rawData = new RawStream(reader.ReadBytes(rawSize), rawOffset); entry.Raws[RawSection.Animation].Add(rawData); }
                                    rawData.OffsetAddresses.Add(offsetAddress);
                                    rawData.LengthAddresses.Add(lengthAddress);
                                }
                            }
                            break;
                        #endregion
                        #region bitm
                        case HaloTags.bitm:
                            entry.TagData.Seek(entry.Offset + 68, SeekOrigin.Begin);
                            int bitmapCount = metaReader.ReadInt32();
                            int bitmapOffset = metaReader.ReadInt32();
                            for (int i = 0; i < bitmapCount; i++)
                            {
                                //LOD0
                                entry.TagData.Seek(bitmapOffset + (i * 116) + 28, SeekOrigin.Begin);
                                offsetAddress = entry.TagData.Position;
                                rawOffset = metaReader.ReadInt32();
                                entry.TagData.Seek(bitmapOffset + (i * 116) + 52, SeekOrigin.Begin);
                                lengthAddress = entry.TagData.Position;
                                rawSize = metaReader.ReadInt32();

                                //Check
                                if ((rawOffset & 0xC0000000) == 0)
                                {
                                    //Read
                                    inStream.Seek(rawOffset, SeekOrigin.Begin);
                                    if (entry.Raws[RawSection.Bitmap].ContainsRawOffset(rawOffset))
                                        rawData = entry.Raws[RawSection.Bitmap][rawOffset];
                                    else { rawData = new RawStream(reader.ReadBytes(rawSize), rawOffset); entry.Raws[RawSection.Bitmap].Add(rawData); }
                                    rawData.OffsetAddresses.Add(offsetAddress);
                                    rawData.LengthAddresses.Add(lengthAddress);
                                }

                                //LOD1
                                entry.TagData.Seek(bitmapOffset + (i * 116) + 32, SeekOrigin.Begin);
                                offsetAddress = entry.TagData.Position;
                                rawOffset = metaReader.ReadInt32();
                                entry.TagData.Seek(bitmapOffset + (i * 116) + 56, SeekOrigin.Begin);
                                lengthAddress = entry.TagData.Position;
                                rawSize = metaReader.ReadInt32();

                                //Check
                                if ((rawOffset & 0xC0000000) == 0)
                                {
                                    //Read
                                    inStream.Seek(rawOffset, SeekOrigin.Begin);
                                    if (entry.Raws[RawSection.Bitmap].ContainsRawOffset(rawOffset))
                                        rawData = entry.Raws[RawSection.Bitmap][rawOffset];
                                    else { rawData = new RawStream(reader.ReadBytes(rawSize), rawOffset); entry.Raws[RawSection.Bitmap].Add(rawData); }
                                    rawData.OffsetAddresses.Add(offsetAddress);
                                    rawData.LengthAddresses.Add(lengthAddress);
                                }

                                //LOD2
                                entry.TagData.Seek(bitmapOffset + (i * 116) + 36, SeekOrigin.Begin);
                                offsetAddress = entry.TagData.Position;
                                rawOffset = metaReader.ReadInt32();
                                entry.TagData.Seek(bitmapOffset + (i * 116) + 60, SeekOrigin.Begin);
                                lengthAddress = entry.TagData.Position;
                                rawSize = metaReader.ReadInt32();

                                //Check
                                if ((rawOffset & 0xC0000000) == 0)
                                {
                                    //Read
                                    inStream.Seek(rawOffset, SeekOrigin.Begin);
                                    if (entry.Raws[RawSection.Bitmap].ContainsRawOffset(rawOffset))
                                        rawData = entry.Raws[RawSection.Bitmap][rawOffset];
                                    else { rawData = new RawStream(reader.ReadBytes(rawSize), rawOffset); entry.Raws[RawSection.Bitmap].Add(rawData); }
                                    rawData.OffsetAddresses.Add(offsetAddress);
                                    rawData.LengthAddresses.Add(lengthAddress);
                                }

                                //LOD3
                                entry.TagData.Seek(bitmapOffset + (i * 116) + 40, SeekOrigin.Begin);
                                offsetAddress = entry.TagData.Position;
                                rawOffset = metaReader.ReadInt32();
                                entry.TagData.Seek(bitmapOffset + (i * 116) + 64, SeekOrigin.Begin);
                                lengthAddress = entry.TagData.Position;
                                rawSize = metaReader.ReadInt32();

                                //Check
                                if ((rawOffset & 0xC0000000) == 0)
                                {
                                    //Read
                                    inStream.Seek(rawOffset, SeekOrigin.Begin);
                                    if (entry.Raws[RawSection.Bitmap].ContainsRawOffset(rawOffset))
                                        rawData = entry.Raws[RawSection.Bitmap][rawOffset];
                                    else { rawData = new RawStream(reader.ReadBytes(rawSize), rawOffset); entry.Raws[RawSection.Bitmap].Add(rawData); }
                                    rawData.OffsetAddresses.Add(offsetAddress);
                                    rawData.LengthAddresses.Add(lengthAddress);
                                }

                                //LOD4
                                entry.TagData.Seek(bitmapOffset + (i * 116) + 44, SeekOrigin.Begin);
                                offsetAddress = entry.TagData.Position;
                                rawOffset = metaReader.ReadInt32();
                                entry.TagData.Seek(bitmapOffset + (i * 116) + 68, SeekOrigin.Begin);
                                lengthAddress = entry.TagData.Position;
                                rawSize = metaReader.ReadInt32();

                                //Check
                                if ((rawOffset & 0xC0000000) == 0)
                                {
                                    //Read
                                    inStream.Seek(rawOffset, SeekOrigin.Begin);
                                    if (entry.Raws[RawSection.Bitmap].ContainsRawOffset(rawOffset))
                                        rawData = entry.Raws[RawSection.Bitmap][rawOffset];
                                    else { rawData = new RawStream(reader.ReadBytes(rawSize), rawOffset); entry.Raws[RawSection.Bitmap].Add(rawData); }
                                    rawData.OffsetAddresses.Add(offsetAddress);
                                    rawData.LengthAddresses.Add(lengthAddress);
                                }

                                //LOD5
                                entry.TagData.Seek(bitmapOffset + (i * 116) + 48, SeekOrigin.Begin);
                                offsetAddress = entry.TagData.Position;
                                rawOffset = metaReader.ReadInt32();
                                entry.TagData.Seek(bitmapOffset + (i * 116) + 72, SeekOrigin.Begin);
                                lengthAddress = entry.TagData.Position;
                                rawSize = metaReader.ReadInt32();

                                //Check
                                if ((rawOffset & 0xC0000000) == 0)
                                {
                                    //Read
                                    inStream.Seek(rawOffset, SeekOrigin.Begin);
                                    if (entry.Raws[RawSection.Bitmap].ContainsRawOffset(rawOffset))
                                        rawData = entry.Raws[RawSection.Bitmap][rawOffset];
                                    else { rawData = new RawStream(reader.ReadBytes(rawSize), rawOffset); entry.Raws[RawSection.Bitmap].Add(rawData); }
                                    rawData.OffsetAddresses.Add(offsetAddress);
                                    rawData.LengthAddresses.Add(lengthAddress);
                                }
                            }
                            break;
                        #endregion
                        #region sbsp
                        case HaloTags.sbsp:
                            long bspAddress = entry.PostProcessedOffset;

                            //Get Lightmap address
                            entry.TagData.Seek(bspAddress + 8);
                            uint lightmapAddress = metaReader.ReadUInt32();

                            //Goto Clusters
                            entry.TagData.Seek(bspAddress + 172, SeekOrigin.Begin);
                            uint clusterCount = metaReader.ReadUInt32();
                            uint clusterOffset = metaReader.ReadUInt32();
                            for (int i = 0; i < clusterCount; i++)
                            {
                                entry.TagData.Seek(clusterOffset + (i * 176) + 40, SeekOrigin.Begin);
                                offsetAddress = entry.TagData.Position;
                                rawOffset = metaReader.ReadInt32();
                                lengthAddress = entry.TagData.Position;
                                rawSize = metaReader.ReadInt32();

                                //Check
                                if ((rawOffset & 0xC0000000) == 0)
                                {
                                    //Read
                                    inStream.Seek(rawOffset, SeekOrigin.Begin);
                                    if (entry.Raws[RawSection.BSP].ContainsRawOffset(rawOffset))
                                        rawData = entry.Raws[RawSection.BSP][rawOffset];
                                    else { rawData = new RawStream(reader.ReadBytes(rawSize), rawOffset); entry.Raws[RawSection.BSP].Add(rawData); }
                                    rawData.OffsetAddresses.Add(offsetAddress);
                                    rawData.LengthAddresses.Add(lengthAddress);
                                }
                            }

                            //Goto Geometries definitions
                            entry.TagData.Seek(bspAddress + 328, SeekOrigin.Begin);
                            uint geometriesCount = metaReader.ReadUInt32();
                            uint geometriesOffset = metaReader.ReadUInt32();
                            for (int i = 0; i < geometriesCount; i++)
                            {
                                entry.TagData.Seek(geometriesOffset + (i * 200) + 40, SeekOrigin.Begin);
                                offsetAddress = entry.TagData.Position;
                                rawOffset = metaReader.ReadInt32();
                                lengthAddress = entry.TagData.Position;
                                rawSize = metaReader.ReadInt32();

                                //Check
                                if ((rawOffset & 0xC0000000) == 0)
                                {
                                    //Read
                                    inStream.Seek(rawOffset, SeekOrigin.Begin);
                                    if (entry.Raws[RawSection.BSP].ContainsRawOffset(rawOffset))
                                        rawData = entry.Raws[RawSection.BSP][rawOffset];
                                    else { rawData = new RawStream(reader.ReadBytes(rawSize), rawOffset); entry.Raws[RawSection.BSP].Add(rawData); }
                                    rawData.OffsetAddresses.Add(offsetAddress);
                                    rawData.LengthAddresses.Add(lengthAddress);
                                }
                            }

                            //Check for lightmap
                            if (lightmapAddress != 0)
                            {
                                //Goto Lightmap Groups
                                entry.TagData.Seek(lightmapAddress + 128);
                                uint groupsCount = metaReader.ReadUInt32();
                                uint groupsPointer = metaReader.ReadUInt32();
                                for (int i = 0; i < groupsCount; i++)
                                {
                                    //Goto Cluster Definitions
                                    entry.TagData.Seek(groupsPointer + (i * 104) + 32, SeekOrigin.Begin);
                                    uint clustersCount = metaReader.ReadUInt32();
                                    uint clustersOffset = metaReader.ReadUInt32();
                                    for (int j = 0; j < clustersCount; j++)
                                    {
                                        entry.TagData.Seek(clustersOffset + (j * 84) + 40, SeekOrigin.Begin);
                                        offsetAddress = entry.TagData.Position;
                                        rawOffset = metaReader.ReadInt32();
                                        lengthAddress = entry.TagData.Position;
                                        rawSize = metaReader.ReadInt32();

                                        //Check
                                        if ((rawOffset & 0xC0000000) == 0)
                                        {
                                            //Read
                                            inStream.Seek(rawOffset, SeekOrigin.Begin);
                                            if (entry.Raws[RawSection.BSP].ContainsRawOffset(rawOffset))
                                                rawData = entry.Raws[RawSection.BSP][rawOffset];
                                            else { rawData = new RawStream(reader.ReadBytes(rawSize), rawOffset); entry.Raws[RawSection.BSP].Add(rawData); }
                                            rawData.OffsetAddresses.Add(offsetAddress);
                                            rawData.LengthAddresses.Add(lengthAddress);
                                        }
                                    }

                                    //Goto Poop Definitions
                                    entry.TagData.Seek(groupsPointer + (i * 104) + 48, SeekOrigin.Begin);
                                    uint poopsCount = metaReader.ReadUInt32();
                                    uint poopsOffset = metaReader.ReadUInt32();
                                    for (int j = 0; j < poopsCount; j++)
                                    {
                                        entry.TagData.Seek(poopsOffset + (j * 84) + 40, SeekOrigin.Begin);
                                        offsetAddress = entry.TagData.Position;
                                        rawOffset = metaReader.ReadInt32();
                                        lengthAddress = entry.TagData.Position;
                                        rawSize = metaReader.ReadInt32();

                                        //Check
                                        if ((rawOffset & 0xC0000000) == 0)
                                        {
                                            //Read
                                            inStream.Seek(rawOffset, SeekOrigin.Begin);
                                            if (entry.Raws[RawSection.BSP].ContainsRawOffset(rawOffset))
                                                rawData = entry.Raws[RawSection.BSP][rawOffset];
                                            else { rawData = new RawStream(reader.ReadBytes(rawSize), rawOffset); entry.Raws[RawSection.BSP].Add(rawData); }
                                            rawData.OffsetAddresses.Add(offsetAddress);
                                            rawData.LengthAddresses.Add(lengthAddress);
                                        }
                                    }

                                    //Goto Geometry Buckets
                                    entry.TagData.Seek(groupsPointer + (i * 104) + 64, SeekOrigin.Begin);
                                    uint bucketsCount = metaReader.ReadUInt32();
                                    uint bucketsOffset = metaReader.ReadUInt32();
                                    for (int j = 0; j < bucketsCount; j++)
                                    {
                                        entry.TagData.Seek(bucketsOffset + (j * 56) + 12, SeekOrigin.Begin);
                                        offsetAddress = entry.TagData.Position;
                                        rawOffset = metaReader.ReadInt32();
                                        lengthAddress = entry.TagData.Position;
                                        rawSize = metaReader.ReadInt32();

                                        //Check
                                        if ((rawOffset & 0xC0000000) == 0)
                                        {
                                            //Read
                                            inStream.Seek(rawOffset, SeekOrigin.Begin);
                                            if (entry.Raws[RawSection.BSP].ContainsRawOffset(rawOffset))
                                                rawData = entry.Raws[RawSection.BSP][rawOffset];
                                            else { rawData = new RawStream(reader.ReadBytes(rawSize), rawOffset); entry.Raws[RawSection.BSP].Add(rawData); }
                                            rawData.OffsetAddresses.Add(offsetAddress);
                                            rawData.LengthAddresses.Add(lengthAddress);
                                        }
                                    }
                                }
                            }

                            //Goto Water definitions
                            entry.TagData.Seek(bspAddress + 548, SeekOrigin.Begin);
                            uint watersCount = metaReader.ReadUInt32();
                            uint watersOffset = metaReader.ReadUInt32();
                            for (int i = 0; i < watersCount; i++)
                            {
                                entry.TagData.Seek(watersOffset + (i * 172) + 16, SeekOrigin.Begin);
                                offsetAddress = entry.TagData.Position;
                                rawOffset = metaReader.ReadInt32();
                                lengthAddress = entry.TagData.Position;
                                rawSize = metaReader.ReadInt32();

                                //Check
                                if ((rawOffset & 0xC0000000) == 0)
                                {
                                    //Read
                                    inStream.Seek(rawOffset, SeekOrigin.Begin);
                                    if (entry.Raws[RawSection.BSP].ContainsRawOffset(rawOffset))
                                        rawData = entry.Raws[RawSection.BSP][rawOffset];
                                    else { rawData = new RawStream(reader.ReadBytes(rawSize), rawOffset); entry.Raws[RawSection.BSP].Add(rawData); }
                                    rawData.OffsetAddresses.Add(offsetAddress);
                                    rawData.LengthAddresses.Add(lengthAddress);
                                }
                            }

                            //Goto Decorators Definitions
                            entry.TagData.Seek(bspAddress + 580, SeekOrigin.Begin);
                            uint decoratorsCount = metaReader.ReadUInt32();
                            uint decoratorsOffset = metaReader.ReadUInt32();
                            for (int i = 0; i < decoratorsCount; i++)
                            {
                                entry.TagData.Seek(decoratorsOffset + (i * 48) + 16, SeekOrigin.Begin);
                                uint cachesCount = metaReader.ReadUInt32();
                                uint cachesOffset = metaReader.ReadUInt32();
                                for (int j = 0; j < cachesCount; j++)
                                {
                                    entry.TagData.Seek(cachesOffset + (j * 44), SeekOrigin.Begin);
                                    offsetAddress = entry.TagData.Position;
                                    rawOffset = metaReader.ReadInt32();
                                    lengthAddress = entry.TagData.Position;
                                    rawSize = metaReader.ReadInt32();

                                    //Check
                                    if ((rawOffset & 0xC0000000) == 0)
                                    {
                                        //Read
                                        inStream.Seek(rawOffset, SeekOrigin.Begin);
                                        if (entry.Raws[RawSection.BSP].ContainsRawOffset(rawOffset))
                                            rawData = entry.Raws[RawSection.BSP][rawOffset];
                                        else { rawData = new RawStream(reader.ReadBytes(rawSize), rawOffset); entry.Raws[RawSection.BSP].Add(rawData); }
                                        rawData.OffsetAddresses.Add(offsetAddress);
                                        rawData.LengthAddresses.Add(lengthAddress);
                                    }
                                }
                            }
                            break;
                            #endregion
                    }
            }
        }
        /// <summary>
        /// Reads a string table from the file into the string list using the string data at an offset provided.
        /// </summary>
        /// <param name="inStream">The Halo 2 cached map file stream.</param>
        /// <param name="reader">The reader for the map stream.</param>
        /// <param name="offset">The globals-based offset to a strings info data section.</param>
        /// <param name="table">The string entry list to read the strings into.</param>
        private void ReadStringTable(Stream inStream, BinaryReader reader, IndexEntry globals, uint memoryFileAddress, uint offset, out StringEntry[] table)
        {
            //Read
            inStream.Seek(globals.Offset + offset, memoryFileAddress, SeekOrigin.Begin);
            int count = reader.ReadInt32();
            int tableLength = reader.ReadInt32();
            int indexOffset = reader.ReadInt32();
            int tableOffset = reader.ReadInt32();

            //Prepare
            STRINGOBJECT[] stringObjects = new STRINGOBJECT[count];
            table = new StringEntry[count];

            //Read Index
            inStream.Seek(indexOffset, SeekOrigin.Begin);
            for (int i = 0; i < count; i++)
            {
                stringObjects[i] = reader.ReadStructure<STRINGOBJECT>();
                table[i] = new StringEntry() { ID = strings[stringObjects[i].StringID.Index] };
            }

            //Read Strings
            for (int i = 0; i < count; i++)
            {
                inStream.Seek(tableOffset + stringObjects[i].Offset, SeekOrigin.Begin);
                table[i].Value = reader.ReadUTF8NullTerminated();
            }
        }
        /// <summary>
        /// Saves this Halo 2 map file to the specfied file or stream.
        /// </summary>
        /// <param name="filename">A string that contains the name of the file to which to save this Halo 2 map.</param>
        /// <exception cref="ArgumentNullException"><paramref name="filename"/> is null.</exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="IOException"></exception>
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
        /// Saves this Halo 2 map file to the specified stream.
        /// </summary>
        /// <param name="stream">The stream where the Halo 2 map will be saved.</param>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="stream"/> does not support seeking.</exception>
        /// <exception cref="MapFileExcption">A write error occured.</exception>
        public void Save(Stream stream)
        {
            //Check stream
            if (stream == null) throw new ArgumentNullException("stream");
            if (!stream.CanSeek) throw new ArgumentException("Stream does not support seeking.", "stream");
            if (!stream.CanWrite) throw new ArgumentException("Stream does not support writing.", "stream");
            if (!stream.CanRead) throw new ArgumentException("Stream does not support reading.", "stream");

            //Check map
            if (!indexList.ContainsID(index.GlobalsID)) throw new MapFileExcption(new InvalidOperationException("Map does not contain globals tag group."));
            if (!indexList.ContainsID(index.ScenarioID)) throw new MapFileExcption(new InvalidOperationException("Map does not contain scenario tag group."));
            if (indexList.Last.Root != HaloTags.ugh_) throw new MapFileExcption(new InvalidOperationException("Final tag group is not coconuts."));

            //Find
            IndexEntry globals = indexList[index.GlobalsID];
            IndexEntry scenario = indexList[index.ScenarioID];
            IndexEntry coconuts = indexList.Last;

            //Prepare
            int offset = 0;
            char[] string128Buffer = null;

            //Strings tables and indices
            byte[] table = null;
            int enTable = 0, enIndex = 0, enSize = 0;
            int jpTable = 0, jpIndex = 0, jpSize = 0;
            int nlTable = 0, nlIndex = 0, nlSize = 0;
            int frTable = 0, frIndex = 0, frSize = 0;
            int esTable = 0, esIndex = 0, esSize = 0;
            int itTable = 0, itIndex = 0, itSize = 0;
            int krTable = 0, krIndex = 0, krSize = 0;
            int zhTable = 0, zhIndex = 0, zhSize = 0;
            int prTable = 0, prIndex = 0, prSize = 0;

            //Get Datas
            List<RawStream> soundDatas = new List<RawStream>();
            List<RawStream> modelDatas = new List<RawStream>();
            List<RawStream>[] sbspDatas = new List<RawStream>[sbspTagDatas.Length];
            List<RawStream> weatherDatas = new List<RawStream>();
            List<RawStream> decoratorDatas = new List<RawStream>();
            List<RawStream> particleModelDatas = new List<RawStream>();
            List<RawStream> lipSyncDatas = new List<RawStream>();
            List<RawStream> animationDatas = new List<RawStream>();
            List<RawStream> bitmapDatas = new List<RawStream>();
            for (int i = 0; i < sbspTagDatas.Length; i++)
                sbspDatas[i] = new List<RawStream>();

            //Loop
            foreach (var entry in indexList)
                switch (entry.Root)
                {
                    case HaloTags.ugh_: soundDatas.AddRange(entry.Raws[RawSection.Sound]); lipSyncDatas.AddRange(entry.Raws[RawSection.LipSync]); break;
                    case HaloTags.mode: modelDatas.AddRange(entry.Raws[RawSection.Model]); break;
                    case HaloTags.sbsp: sbspDatas[bspIndexLookup[entry.ID]].AddRange(entry.Raws[RawSection.BSP]); break;
                    case HaloTags.weat: weatherDatas.AddRange(entry.Raws[RawSection.Weather]); break;
                    case HaloTags.DECR: decoratorDatas.AddRange(entry.Raws[RawSection.Decorator]); break;
                    case HaloTags.PRTM: particleModelDatas.AddRange(entry.Raws[RawSection.ParticleModel]); break;
                    case HaloTags.jmad: animationDatas.AddRange(entry.Raws[RawSection.Animation]); break;
                    case HaloTags.bitm: bitmapDatas.AddRange(entry.Raws[RawSection.Bitmap]); break;
                }

            //Create I/Os
            using (BinaryReader reader = new BinaryReader(stream))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                //Create Index
                byte[] index = CreateIndex();

                //Prepare Header
                int oldNonRawLength = header.NonRawLength;
                header.NonRawLength = index.Length;
                header.IndexLength = index.Length;

                //Write Raws
                stream.Seek(2048, SeekOrigin.Begin);

                //Setup Meta I/O
                using (BinaryReader metaReader = new BinaryReader(tagData))
                using (BinaryWriter metaWriter = new BinaryWriter(tagData))
                {
                    //Write Sounds and fix addresses
                    foreach (RawStream data in soundDatas)
                    {
                        //Get and write offset and length
                        offset = (int)stream.Seek(stream.Position.PadTo(512), SeekOrigin.Begin);
                        foreach (long address in data.OffsetAddresses)
                        {
                            tagData.Seek(address, SeekOrigin.Begin);
                            metaWriter.Write(offset);
                        }
                        foreach (long address in data.LengthAddresses)
                        {
                            tagData.Seek(address, SeekOrigin.Begin);
                            metaWriter.Write(data.IntLength);
                        }

                        //Write
                        writer.Write(data.GetBuffer());
                    }

                    //Write Models and fix addresses
                    foreach (RawStream data in modelDatas)
                    {
                        //Get and write offset and length
                        offset = (int)stream.Seek(stream.Position.PadTo(512), SeekOrigin.Begin);
                        foreach (long address in data.OffsetAddresses)
                        {
                            tagData.Seek(address, SeekOrigin.Begin);
                            metaWriter.Write(offset);
                        }
                        foreach (long address in data.LengthAddresses)
                        {
                            tagData.Seek(address, SeekOrigin.Begin);
                            metaWriter.Write(data.IntLength);
                        }

                        //Write
                        writer.Write(data.GetBuffer());
                    }

                    //Write BSPs and fix addresses
                    for (int i = 0; i < sbspTagDatas.Length; i++)
                        using (BinaryWriter bspWriter = new BinaryWriter(sbspTagDatas[i]))
                            foreach (RawStream data in sbspDatas[i])
                            {
                                //Get and write offset and length
                                offset = (int)stream.Seek(stream.Position.PadTo(512), SeekOrigin.Begin);
                                foreach (long address in data.OffsetAddresses)
                                {
                                    sbspTagDatas[i].Seek(address, SeekOrigin.Begin);
                                    bspWriter.Write(offset);
                                }
                                foreach (long address in data.LengthAddresses)
                                {
                                    sbspTagDatas[i].Seek(address, SeekOrigin.Begin);
                                    bspWriter.Write(data.IntLength);
                                }

                                //Write
                                writer.Write(data.GetBuffer());
                            }

                    //Write Weather and fix addresses
                    foreach (RawStream data in weatherDatas)
                    {
                        //Get and write offset and length
                        offset = (int)stream.Seek(stream.Position.PadTo(512), SeekOrigin.Begin);
                        foreach (long address in data.OffsetAddresses)
                        {
                            tagData.Seek(address, SeekOrigin.Begin);
                            metaWriter.Write(offset);
                        }
                        foreach (long address in data.LengthAddresses)
                        {
                            tagData.Seek(address, SeekOrigin.Begin);
                            metaWriter.Write(data.IntLength);
                        }

                        //Write
                        writer.Write(data.GetBuffer());
                    }

                    //Write Decorator and fix addresses
                    foreach (RawStream data in decoratorDatas)
                    {
                        //Get and write offset and length
                        offset = (int)stream.Seek(stream.Position.PadTo(512), SeekOrigin.Begin);
                        foreach (long address in data.OffsetAddresses)
                        {
                            tagData.Seek(address, SeekOrigin.Begin);
                            metaWriter.Write(offset);
                        }
                        foreach (long address in data.LengthAddresses)
                        {
                            tagData.Seek(address, SeekOrigin.Begin);
                            metaWriter.Write(data.IntLength);
                        }

                        //Write
                        writer.Write(data.GetBuffer());
                    }

                    //Write Lip-sync and fix addresses
                    foreach (RawStream data in lipSyncDatas)
                    {
                        //Get and write offset and length
                        offset = (int)stream.Seek(stream.Position.PadTo(512), SeekOrigin.Begin);
                        foreach (long address in data.OffsetAddresses)
                        {
                            tagData.Seek(address, SeekOrigin.Begin);
                            metaWriter.Write(offset);
                        }
                        foreach (long address in data.LengthAddresses)
                        {
                            tagData.Seek(address, SeekOrigin.Begin);
                            metaWriter.Write(data.IntLength);
                        }

                        //Write
                        writer.Write(data.GetBuffer());
                    }

                    //Write Particle models and fix addresses
                    foreach (RawStream data in particleModelDatas)
                    {
                        //Get and write offset and length
                        offset = (int)stream.Seek(stream.Position.PadTo(512), SeekOrigin.Begin);
                        foreach (long address in data.OffsetAddresses)
                        {
                            tagData.Seek(address, SeekOrigin.Begin);
                            metaWriter.Write(offset);
                        }
                        foreach (long address in data.LengthAddresses)
                        {
                            tagData.Seek(address, SeekOrigin.Begin);
                            metaWriter.Write(data.IntLength);
                        }

                        //Write
                        writer.Write(data.GetBuffer());
                    }

                    //Write Animations and fix addresses
                    foreach (RawStream data in animationDatas)
                    {
                        //Get and write offset and length
                        offset = (int)stream.Seek(stream.Position.PadTo(512), SeekOrigin.Begin);
                        foreach (long address in data.OffsetAddresses)
                        {
                            tagData.Seek(address, SeekOrigin.Begin);
                            metaWriter.Write(offset);
                        }
                        foreach (long address in data.LengthAddresses)
                        {
                            tagData.Seek(address, SeekOrigin.Begin);
                            metaWriter.Write(data.IntLength);
                        }

                        //Write
                        writer.Write(data.GetBuffer());
                    }

                    //Write Structure BSP and Lightmap
                    int bspLength = 0;
                    tagData.Seek(scenario.Offset + 528, SeekOrigin.Begin);
                    int sbspsCount = metaReader.ReadInt32();
                    int sbspsOffset = metaReader.ReadInt32();
                    for (int i = 0; i < sbspTagDatas.Length; i++)
                    {
                        offset = (int)stream.Seek(stream.Position.PadTo(512), SeekOrigin.Begin);
                        tagData.Seek(sbspsOffset + (i * 68), SeekOrigin.Begin);
                        metaWriter.Write(offset);
                        metaWriter.Write((int)sbspTagDatas[i].Length);
                        metaWriter.Write(INDEX.IndexMemoryAddress + (index.Length - INDEX.Length));
                        writer.Write(sbspTagDatas[i].GetBuffer());
                        if (sbspTagDatas[i].Length > bspLength)
                            bspLength = sbspTagDatas[i].IntLength;
                    }
                    header.NonRawLength += bspLength;

                    //Write Strings 128
                    header.StringCount = strings.Count;
                    header.Strings128Offset = (int)stream.Position.PadTo(512);
                    foreach (string stringId in strings)
                    {
                        string128Buffer = new char[128];
                        for (int i = 0; i < Math.Min(128, stringId.Length); i++)
                            string128Buffer[i] = stringId[i];
                        writer.Write(string128Buffer);
                    }

                    //Write String Index
                    offset = 0;
                    header.StringsIndexOffset = (int)stream.Seek(stream.Position.PadTo(512), SeekOrigin.Begin);
                    foreach (string stringId in strings)
                    {
                        writer.Write(offset);
                        offset += Encoding.UTF8.GetByteCount(stringId) + 1;
                    }

                    //Write String IDs
                    header.StringsOffset = (int)stream.Seek(stream.Position.PadTo(512), SeekOrigin.Begin);
                    foreach (string stringId in strings)
                        writer.WriteUTF8NullTerminated(stringId);
                    header.FilesOffset = (int)stream.Position.PadTo(512);
                    stream.Seek(header.FilesOffset, SeekOrigin.Begin);

                    //Write Files
                    header.FilesOffset = (int)stream.Seek(stream.Position.PadTo(512), SeekOrigin.Begin);
                    foreach (var indexEntry in indexList)
                        writer.WriteUTF8NullTerminated(indexEntry.Filename);

                    //Write Files Index
                    offset = 0;
                    header.FilesIndex = (int)stream.Seek(stream.Position.PadTo(512), SeekOrigin.Begin);
                    foreach (var indexEntry in indexList)
                    {
                        writer.Write(offset);
                        offset += Encoding.UTF8.GetByteCount(indexEntry.Filename) + 1;
                    }

                    //Prepare String Lists
                    List<StringEntry> en = new List<StringEntry>();
                    List<StringEntry> jp = new List<StringEntry>();
                    List<StringEntry> nl = new List<StringEntry>();
                    List<StringEntry> fr = new List<StringEntry>();
                    List<StringEntry> es = new List<StringEntry>();
                    List<StringEntry> it = new List<StringEntry>();
                    List<StringEntry> kr = new List<StringEntry>();
                    List<StringEntry> zh = new List<StringEntry>();
                    List<StringEntry> pr = new List<StringEntry>();

                    //Loop
                    foreach (var unicode in indexList.Where(e => e.Root == HaloTags.unic))
                    {
                        //Goto
                        tagData.Seek(unicode.Offset + 16, SeekOrigin.Begin);

                        //Write English
                        metaWriter.Write((ushort)en.Count);
                        metaWriter.Write((ushort)unicode.Strings.English.Count);
                        en.AddRange(unicode.Strings.English);

                        //Write Japanese
                        metaWriter.Write((ushort)jp.Count);
                        metaWriter.Write((ushort)unicode.Strings.Japanese.Count);
                        jp.AddRange(unicode.Strings.Japanese);

                        //Write German
                        metaWriter.Write((ushort)nl.Count);
                        metaWriter.Write((ushort)unicode.Strings.German.Count);
                        nl.AddRange(unicode.Strings.German);

                        //Write French
                        metaWriter.Write((ushort)fr.Count);
                        metaWriter.Write((ushort)unicode.Strings.French.Count);
                        fr.AddRange(unicode.Strings.French);

                        //Write Spanish
                        metaWriter.Write((ushort)es.Count);
                        metaWriter.Write((ushort)unicode.Strings.Spanish.Count);
                        es.AddRange(unicode.Strings.Spanish);

                        //Write Italian
                        metaWriter.Write((ushort)it.Count);
                        metaWriter.Write((ushort)unicode.Strings.Italian.Count);
                        it.AddRange(unicode.Strings.Italian);

                        //Write Korean
                        metaWriter.Write((ushort)kr.Count);
                        metaWriter.Write((ushort)unicode.Strings.Korean.Count);
                        kr.AddRange(unicode.Strings.Korean);

                        //Write Chinese
                        metaWriter.Write((ushort)zh.Count);
                        metaWriter.Write((ushort)unicode.Strings.Chinese.Count);
                        zh.AddRange(unicode.Strings.Chinese);

                        //Write Portuguese
                        metaWriter.Write((ushort)pr.Count);
                        metaWriter.Write((ushort)unicode.Strings.Portuguese.Count);
                        pr.AddRange(unicode.Strings.Portuguese);
                    }

                    //Write English Table
                    enIndex = (int)stream.Seek(stream.Position.PadTo(512), SeekOrigin.Begin);
                    writer.Write(CreateStringIndex(en));
                    enTable = (int)stream.Seek(stream.Position.PadTo(512), SeekOrigin.Begin);
                    table = CreateStringTable(en);
                    writer.Write(table);
                    enSize = table.Length;

                    //Write Japanese Table
                    jpIndex = (int)stream.Seek(stream.Position.PadTo(512), SeekOrigin.Begin);
                    writer.Write(CreateStringIndex(jp));
                    jpTable = (int)stream.Seek(stream.Position.PadTo(512), SeekOrigin.Begin);
                    table = CreateStringTable(jp);
                    writer.Write(table);
                    jpSize = table.Length;

                    //Write Dutch Table
                    nlIndex = (int)stream.Seek(stream.Position.PadTo(512), SeekOrigin.Begin);
                    writer.Write(CreateStringIndex(nl));
                    nlTable = (int)stream.Seek(stream.Position.PadTo(512), SeekOrigin.Begin);
                    table = CreateStringTable(nl);
                    writer.Write(table);
                    nlSize = table.Length;

                    //Write French Table
                    frIndex = (int)stream.Seek(stream.Position.PadTo(512), SeekOrigin.Begin);
                    writer.Write(CreateStringIndex(fr));
                    frTable = (int)stream.Seek(stream.Position.PadTo(512), SeekOrigin.Begin);
                    table = CreateStringTable(fr);
                    writer.Write(table);
                    frSize = table.Length;

                    //Write Spanish Table
                    esIndex = (int)stream.Seek(stream.Position.PadTo(512), SeekOrigin.Begin);
                    writer.Write(CreateStringIndex(es));
                    esTable = (int)stream.Seek(stream.Position.PadTo(512), SeekOrigin.Begin);
                    table = CreateStringTable(es);
                    writer.Write(table);
                    esSize = table.Length;

                    //Write Italian Table
                    itIndex = (int)stream.Seek(stream.Position.PadTo(512), SeekOrigin.Begin);
                    writer.Write(CreateStringIndex(it));
                    itTable = (int)stream.Seek(stream.Position.PadTo(512), SeekOrigin.Begin);
                    table = CreateStringTable(it);
                    writer.Write(table);
                    itSize = table.Length;

                    //Write Korean Table
                    krIndex = (int)stream.Seek(stream.Position.PadTo(512), SeekOrigin.Begin);
                    writer.Write(CreateStringIndex(kr));
                    krTable = (int)stream.Seek(stream.Position.PadTo(512), SeekOrigin.Begin);
                    table = CreateStringTable(kr);
                    writer.Write(table);
                    krSize = table.Length;

                    //Write Chinese Table
                    zhIndex = (int)stream.Seek(stream.Position.PadTo(512), SeekOrigin.Begin);
                    writer.Write(CreateStringIndex(zh));
                    zhTable = (int)stream.Seek(stream.Position.PadTo(512), SeekOrigin.Begin);
                    table = CreateStringTable(zh);
                    writer.Write(table);
                    zhSize = table.Length;

                    //Write Portuguese Table
                    prIndex = (int)stream.Seek(stream.Position.PadTo(512), SeekOrigin.Begin);
                    writer.Write(CreateStringIndex(pr));
                    prTable = (int)stream.Seek(stream.Position.PadTo(512), SeekOrigin.Begin);
                    table = CreateStringTable(pr);
                    writer.Write(table);
                    prSize = table.Length;

                    //Write Crazy
                    header.CrazyLength = crazyData.IntLength;
                    header.CrazyOffset = (int)stream.Seek(stream.Position.PadTo(512), SeekOrigin.Begin);
                    writer.Write(crazyData.GetBuffer());

                    //Write Bitmaps and fix addresses
                    foreach (RawStream data in bitmapDatas)
                    {
                        //Get and write offset and length
                        offset = (int)stream.Seek(stream.Position.PadTo(512), SeekOrigin.Begin);
                        foreach (long address in data.OffsetAddresses)
                        {
                            tagData.Seek(address, SeekOrigin.Begin);
                            metaWriter.Write(offset);
                        }
                        foreach (long address in data.LengthAddresses)
                        {
                            tagData.Seek(address, SeekOrigin.Begin);
                            metaWriter.Write(data.IntLength);
                        }

                        //Write
                        writer.Write(data.GetBuffer());
                    }

                    //Write Index
                    header.IndexOffset = (int)stream.Seek(stream.Position.PadTo(512), SeekOrigin.Begin);
                    writer.Write(index);

                    //Write English to globals
                    tagData.Seek(globals.Offset + 400, SeekOrigin.Begin);
                    metaWriter.Write(en.Count);
                    metaWriter.Write(enSize);
                    metaWriter.Write(enIndex);
                    metaWriter.Write(enTable);

                    //Write Japanese to globals
                    tagData.Seek(12, SeekOrigin.Current);
                    metaWriter.Write(jp.Count);
                    metaWriter.Write(jpSize);
                    metaWriter.Write(jpIndex);
                    metaWriter.Write(jpTable);

                    //Write Dutch to globals
                    tagData.Seek(12, SeekOrigin.Current);
                    metaWriter.Write(nl.Count);
                    metaWriter.Write(nlSize);
                    metaWriter.Write(nlIndex);
                    metaWriter.Write(nlTable);

                    //Write French to globals
                    tagData.Seek(12, SeekOrigin.Current);
                    metaWriter.Write(fr.Count);
                    metaWriter.Write(frSize);
                    metaWriter.Write(frIndex);
                    metaWriter.Write(frTable);

                    //Write Spanish to globals
                    tagData.Seek(12, SeekOrigin.Current);
                    metaWriter.Write(es.Count);
                    metaWriter.Write(esSize);
                    metaWriter.Write(esIndex);
                    metaWriter.Write(esTable);

                    //Write Italian to globals
                    tagData.Seek(12, SeekOrigin.Current);
                    metaWriter.Write(it.Count);
                    metaWriter.Write(itSize);
                    metaWriter.Write(itIndex);
                    metaWriter.Write(itTable);

                    //Write Korean to globals
                    tagData.Seek(12, SeekOrigin.Current);
                    metaWriter.Write(kr.Count);
                    metaWriter.Write(krSize);
                    metaWriter.Write(krIndex);
                    metaWriter.Write(krTable);

                    //Write Chinese to globals
                    tagData.Seek(12, SeekOrigin.Current);
                    metaWriter.Write(zh.Count);
                    metaWriter.Write(zhSize);
                    metaWriter.Write(zhIndex);
                    metaWriter.Write(zhTable);

                    //Write Portuguese to globals
                    tagData.Seek(12, SeekOrigin.Current);
                    metaWriter.Write(pr.Count);
                    metaWriter.Write(prSize);
                    metaWriter.Write(prIndex);
                    metaWriter.Write(prTable);
                }

                //Write Meta
                header.MetaLength = tagData.IntLength;
                writer.Write(tagData.GetBuffer());
                header.NonRawLength += header.MetaLength;

                //Pad File
                stream.Seek(stream.Position.PadTo(1024), SeekOrigin.Begin);

                //Set
                header.FileLength = (int)stream.Length;

                //Sign
                header.Signature = 0;
                stream.Seek(2048, SeekOrigin.Begin);
                for (int i = 0; i < (header.FileLength - 2048) / 4; i++)
                    header.Signature ^= reader.ReadInt32();

                //Write header
                stream.Seek(0, SeekOrigin.Begin);
                writer.Write(header);

                //Read
                ReadRaws(stream, reader);
            }
        }
        /// <summary>
        /// Creates a padded index table for the current Halo 2 map.
        /// </summary>
        /// <returns>A Halo 2 index table containing the index, tags, and object entries present in this instance.</returns>
        private byte[] CreateIndex()
        {
            //Edit Index
            index.ObjectCount = indexList.Count;
            index.TagCount = tags.Count;
            index.ObjectOffset = tags.Count * TAGHIERARCHY.Length + INDEX.IndexMemoryAddress;
            index.IndexAddress = INDEX.IndexMemoryAddress;

            //Calculate minimum length...
            int length = (INDEX.Length + (TAGHIERARCHY.Length * tags.Count) + (OBJECT.Length * indexList.Count)).PadTo(4096);

            //Check
            if (length > header.IndexLength)
                throw new MapFileExcption("Unable to save map. Index length is larger than expected.");
            else length = header.IndexLength;

            //Write
            byte[] indexTable = new byte[length];
            using (MemoryStream ms = new MemoryStream(indexTable))
            using (BinaryWriter writer = new BinaryWriter(ms))
            {
                writer.Write(index);                    //Write index header
                foreach (TAGHIERARCHY tag in tags)      //Write tags
                    writer.Write(tag);
                foreach (IndexEntry entry in indexList) //Write object entries
                    writer.Write(entry.GetObjectEntry());
            }
            return indexTable;
        }
        /// <summary>
        /// Creates a strings table.
        /// </summary>
        /// <param name="stringList">The strings in the table.</param>
        /// <returns>A byte array containing a 512-byte padded string table.</returns>
        private byte[] CreateStringTable(List<StringEntry> stringList)
        {
            int length = 0;
            for (int i = 0; i < stringList.Count; i++)
                length += Encoding.UTF8.GetByteCount(stringList[i].Value) + 1;
            byte[] stringTable = new byte[length];
            using (MemoryStream stream = new MemoryStream(stringTable))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                for (int i = 0; i < stringList.Count; i++)
                    writer.WriteUTF8NullTerminated(stringList[i].Value);
            }
            return stringTable;
        }
        /// <summary>
        /// Creates a strings index table.
        /// </summary>
        /// <param name="stringTable">The strings in the table.</param>
        /// <param name="ids">The string IDs the strings are assigned to.</param>
        /// <returns>A byte array containing a 512-byte padded string index.</returns>
        private byte[] CreateStringIndex(List<StringEntry> stringTable)
        {
            //Prepare
            int offset = 0;
            int length = 8 * stringTable.Count;
            byte[] index = new byte[length];

            //Write
            using (MemoryStream stream = new MemoryStream(index))
            using (BinaryWriter writer = new BinaryWriter(stream))
                for (int i = 0; i < stringTable.Count; i++)
                {
                    //Write
                    writer.Write(STRINGID.FromString(stringTable[i].ID, strings.IndexOf(stringTable[i].ID)));
                    writer.Write(offset);

                    //Increment
                    offset += Encoding.UTF8.GetByteCount(stringTable[i].Value) + 1;
                }
            return index;
        }
        /// <summary>
        /// Returns the map's name.
        /// </summary>
        /// <returns>The map's name.</returns>
        public override string ToString()
        {
            return header.Name;
        }
        /// <summary>
        /// Closes the map file, clearing any buffers used.
        /// </summary>
        public void Close()
        {
            //Dispose
            foreach (var entry in indexList)
                entry.Dispose();
            foreach (var data in sbspTagDatas)
                if (data != null)
                    data.Dispose();
            if (crazyData != null)
                crazyData.Dispose();
            if (tagData != null)
                tagData.Dispose();

            //Setup
            header = HEADER.Create();
            index = INDEX.Create();
            sbspTagDatas = new FixedMemoryMappedStream[0];
            strings = new List<string>();
            crazyData = FixedMemoryMappedStream.Empty;
            tags = new TagHierarchyList();
            indexList = new IndexEntryList();
            tagData = FixedMemoryMappedStream.Empty;
            bspIndexLookup = new Dictionary<TAGID, int>();

            //Collect
            GC.Collect();
        }
        /// <summary>
        /// Clears all buffers and releases all resources used by this instance.
        /// </summary>
        public void Dispose()
        {
            //Close
            Close();

            //Null buffers
            sbspTagDatas = null;
            strings = null;
            tags = null;
            bspIndexLookup = null;
            indexList = null;
            tagData = null;
        }
        /// <summary>
        /// Adds a new string identifier to the map.
        /// </summary>
        /// <param name="stringId">The string identifier.</param>
        public STRINGID AddStringId(string stringId)
        {
            //Prepare
            int index = -1;

            //Add
            if (!strings.Contains(stringId))
                strings.Add(stringId);

            //Get
            index = strings.IndexOf(stringId);

            //Return
            return STRINGID.FromString(stringId, index);
        }

        /// <summary>
        /// Represents a Halo 2 tag heirarchy list.
        /// </summary>
        [Serializable]
        public sealed class TagHierarchyList : IEnumerable<TAGHIERARCHY>
        {
            /// <summary>
            /// Gets the number of tag hierarchy structures this list contains.
            /// </summary>
            public int Count
            {
                get { return tags.Count; }
            }

            private readonly Dictionary<TAG, TAGHIERARCHY> tags;

            /// <summary>
            /// Initializes the <see cref="TagHierarchyList"/> using the supplied <see cref="TAGHIERARCHY"/> array.
            /// </summary>
            /// <param name="tagHierarchies">The <see cref="TAGHIERARCHY"/> array to copy into the new list.</param>
            public TagHierarchyList(TAGHIERARCHY[] tagHierarchies)
            {
                //Prepare
                tags = new Dictionary<TAG, TAGHIERARCHY>();

                //Add
                foreach (TAGHIERARCHY tagHierarchy in tagHierarchies)
                    tags.Add(tagHierarchy.Root, tagHierarchy);
            }
            /// <summary>
            /// Initializes a new <see cref="TagHierarchyList"/>.
            /// </summary>
            public TagHierarchyList()
            {
                tags = new Dictionary<TAG, TAGHIERARCHY>();
            }
            /// <summary>
            /// Gets and returns the <see cref="TAGHIERARCHY"/> whose root is the specified <see cref="TAG"/>.
            /// </summary>
            /// <param name="tag">The root of the <see cref="TAGHIERARCHY"/> to get.</param>
            /// <returns>A <see cref="TAGHIERARCHY"/> structure whose <see cref="TAGHIERARCHY.Root"/> value is the specified <see cref="TAG"/> value.</returns>
            /// <exception cref="ArgumentException">Occurs when the specified tag root is not found.</exception>
            public TAGHIERARCHY this[TAG tag]
            {
                get
                {
                    if (tags.ContainsKey(tag)) return tags[tag];
                    else throw new ArgumentException("Unable to find tag hierarchy for the supplied tag.", "tag");
                }
            }
            /// <summary>
            /// Checks if the list contains an tag hierarchy whose root matches the supplied tag.
            /// </summary>
            /// <param name="tag">The ID to check.</param>
            /// <returns>True if the list contains an tag hierarchy whose root matches the supplied tag, false if not.</returns>
            public bool ContainsTag(TAG tag)
            {
                return tags.ContainsKey(tag);
            }
            /// <summary>
            /// Returns an enumerator that iterates through the <see cref="TagHierarchyList"/>.
            /// </summary>
            /// <returns>An enumerator.</returns>
            public IEnumerator<TAGHIERARCHY> GetEnumerator()
            {
                return tags.Values.GetEnumerator();
            }
            /// <summary>
            /// Gets a string representation of this list.
            /// </summary>
            /// <returns>A string representation of this list.</returns>
            public override string ToString()
            {
                return string.Format("Count = {0}", tags.Count);
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return tags.Values.GetEnumerator();
            }
        }
        
        /// <summary>
        /// Represents a Halo 2 index entry list.
        /// </summary>
        [Serializable]
        public sealed class IndexEntryList : IEnumerable<IndexEntry>
        {
            /// <summary>
            /// Gets the number of index entries this list contains.
            /// </summary>
            public int Count
            {
                get { return entries.Count; }
            }
            /// <summary>
            /// Gets and returns the first object entry in the list.
            /// </summary>
            /// <exception cref="IndexOutOfRangeException"></exception>
            public IndexEntry First
            {
                get
                {
                    if (entries.Count > 0) return entries[indexLookup[0]];
                    else throw new IndexOutOfRangeException();
                }
            }
            /// <summary>
            /// Gets and returns the last object entry in the list.
            /// </summary>
            /// <exception cref="IndexOutOfRangeException"></exception>
            public IndexEntry Last
            {
                get
                {
                    int index = entries.Count - 1;
                    if (index >= 0) return entries[indexLookup[index]];
                    else throw new IndexOutOfRangeException();
                }
            }
            /// <summary>
            /// Gets and returns the index entry object by its <see cref="TAGID"/>.
            /// </summary>
            /// <param name="id">The <see cref="TAGID"/> of the index entry object.</param>
            /// <returns>An <see cref="IndexEntry"/> whose ID matches the supplied <see cref="TAGID"/>.</returns>
            public IndexEntry this[TAGID id]
            {
                get
                {
                    if (entries.ContainsKey(id)) return entries[id];
                    else return null;
                }
            }
            /// <summary>
            /// Gets and returns the index entry object at a given index.
            /// </summary>
            /// <param name="id">The index of the index entry object.</param>
            /// <returns>An <see cref="IndexEntry"/> at the given index.</returns>
            public IndexEntry this[int index]
            {
                get
                {
                    if (indexLookup.ContainsKey(index)) return entries[indexLookup[index]];
                    else return null;
                }
            }

            private readonly Dictionary<TAGID, IndexEntry> entries;
            private readonly Dictionary<int, TAGID> indexLookup;

            /// <summary>
            /// Intializes a new <see cref="IndexEntryList"/> copying the supplied <see cref="IndexEntry"/> array into the list.
            /// </summary>
            /// <param name="indexEntries">The array to copy into the new list.</param>
            public IndexEntryList(IndexEntry[] indexEntries)
            {
                //Setup
                entries = new Dictionary<TAGID, IndexEntry>();
                indexLookup = new Dictionary<int, TAGID>();

                //Add
                for (int i = 0; i < indexEntries.Length; i++)
                {
                    entries.Add(indexEntries[i].ID, indexEntries[i]);
                    indexLookup.Add(i, indexEntries[i].ID);
                }
            }
            /// <summary>
            /// Initializes a new <see cref="IndexEntryList"/>.
            /// </summary>
            public IndexEntryList()
            {
                //Setup
                entries = new Dictionary<TAGID, IndexEntry>();
                indexLookup = new Dictionary<int, TAGID>();
            }
            /// <summary>
            /// Checks if the list contains an index entry whose ID matches the supplied ID.
            /// </summary>
            /// <param name="id">The ID to check.</param>
            /// <returns>True if the list contains an index entry whose ID is the supplied ID, false if not.</returns>
            public bool ContainsID(TAGID id)
            {
                return entries.ContainsKey(id);
            }
            /// <summary>
            /// Gets a string representation of this list.
            /// </summary>
            /// <returns>A string representation of this list.</returns>
            public override string ToString()
            {
                return string.Format("Count = {0}", entries.Count);
            }
            /// <summary>
            /// Returns an enumerator that iterates through the <see cref="TagHierarchyList"/>.
            /// </summary>
            /// <returns>An enumerator.</returns>
            public IEnumerator<IndexEntry> GetEnumerator()
            {
                return entries.Values.GetEnumerator();
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return entries.Values.GetEnumerator();
            }
        }
    }

    /// <summary>
    /// Represents an error that occurs while handling a map file.
    /// </summary>
    public sealed class MapFileExcption : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MapFileExcption"/>.
        /// </summary>
        public MapFileExcption() : base()
        { }

        /// <summary>
        /// Initializes a new instance of <see cref="MapFileExcption"/> using the provided exception message.
        /// </summary>
        /// <param name="message">The exception's message.</param>
        public MapFileExcption(string message) :
            base(message)
        { }

        /// <summary>
        /// Initializes a new instance of <see cref="MapFileExcption"/> using the provided inner exception.
        /// </summary>
        /// <param name="innerException">The inner exception that triggered the <see cref="MapFileExcption"/>.</param>
        public MapFileExcption(Exception innerException) : base(innerException.Message, innerException)
        { }

        /// <summary>
        /// Initializes a new instance of <see cref="MapFileExcption"/> using the provided exception message and inner exception.
        /// </summary>
        /// <param name="message">The exception's message.</param>
        /// <param name="innerException">The inner exception that triggered the <see cref="MapFileExcption"/>.</param>
        public MapFileExcption(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
