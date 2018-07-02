using Abide.Guerilla.Library;
using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using Abide.HaloLibrary.IO;
using Abide.Tag;
using Abide.Tag.Cache.Generated;
using Abide.Tag.Guerilla;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Abide.Decompiler
{
    /// <summary>
    /// Represents a Halo 2 cache map file decompiler.
    /// </summary>
    public sealed class MapDecompiler
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
        public IDecompileHost Host { get; }

        private volatile bool isDecompiling = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapDecompiler"/> class using the specified map file and output directory.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="map">The map file.</param>
        /// <param name="outputDirectory">The output directory.</param>
        public MapDecompiler(IDecompileHost host, MapFile map, string outputDirectory)
        {
            Host = host;
            Map = map;
            OutputDirectory = outputDirectory;
        }

        public void Start()
        {
            //Check
            if (isDecompiling) return;

            //Start
            isDecompiling = true;
            ThreadPool.QueueUserWorkItem(Map_Decompile, null);
        }

        private void Map_Decompile(object state)
        {
            //Prepare
            Group tagGroup = null;
            IndexEntry soundGestalt = null;
            SoundCacheFileGestalt soundCacheFileGestalt = null;
            string tagGroupFileName = string.Empty;

            //Get sound gestalt
            IndexEntry globals = Map.Globals;
            if (globals.Root == HaloTags.matg)
                using (BinaryReader tagReader = globals.TagData.CreateReader())
                {
                    //Goto
                    globals.TagData.Seek((uint)globals.PostProcessedOffset, SeekOrigin.Begin);

                    //Read
                    Group globalsTagBlock = TagLookup.CreateTagGroup(globals.Root);
                    globalsTagBlock.Read(tagReader);

                    //Get sound globals
                    BaseBlockField soundGlobalsTagBlock = (BaseBlockField)globalsTagBlock.TagBlocks[0].Fields[4];
                    if(soundGlobalsTagBlock.BlockList.Count > 0)
                    {
                        //Get sound gestalt index
                        TagId soundGestaltId = new TagId((int)soundGlobalsTagBlock.BlockList[0].Fields[4].Value);

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
                }

            //Loop
            for (int i = 0; i < Map.IndexEntries.Count; i++)
                if ((tagGroup = TagLookup.CreateTagGroup(Map.IndexEntries[i].Root)) != null)    //Lookup cache definition
                {
                    //Get file name
                    tagGroupFileName = Path.Combine(OutputDirectory, $"{Map.IndexEntries[i].Filename}.{tagGroup.Name}");
                    
                    //Read tag
                    using (BinaryReader reader = Map.IndexEntries[i].TagData.CreateReader())
                    {
                        //Goto
                        reader.BaseStream.Seek((uint)Map.IndexEntries[i].PostProcessedOffset, SeekOrigin.Begin);
                        tagGroup.Read(reader);

                        //Preprocess
                        switch (Map.IndexEntries[i].Root)
                        {
                            case HaloTags.snd_:
                                CacheFileSound_ConvertToGuerilla(tagGroup, Map.IndexEntries[i], soundCacheFileGestalt);
                                break;
                        }

                        //Recursive search and convert
                        foreach (Block block in tagGroup.TagBlocks)
                            Block_RecursiveSearch(block);
                    }
                    
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
                        fs.Seek(TagGroupHeader.Size, SeekOrigin.Begin);
                        tagGroup.Write(writer);
                        
                        //Create raws
                        TagGroup_CreateRaws(writer, Map.IndexEntries[i], ref header);

                        //Setup header
                        header.Checksum = (uint)TagGroup_CalculateChecksum(tagGroup);
                        header.GroupTag = tagGroup.GroupTag.FourCc;
                        header.Id = Map.IndexEntries[i].Id.Dword;

                        //Write Header
                        fs.Seek(0, SeekOrigin.Begin);
                        writer.Write(header);
                    }

                    //Report
                    if (Host != null) Host.Report(i / (float)Map.IndexEntries.Count);
                }

            //Set
            isDecompiling = false;

            //Collect
            GC.Collect();

            //Complete
            Host.Complete();
        }

        private int TagGroup_CalculateChecksum(Group tagGroup)
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
        
        private void TagGroup_CreateRaws(BinaryWriter writer, IndexEntry entry, ref TagGroupHeader header)
        {
            //Prepare
            List<int> rawOffsets = new List<int>();
            List<byte[]> rawDatas = new List<byte[]>();

            //Compile raws
            foreach (RawSection section in Enum.GetValues(typeof(RawSection)))
            {
                header.RawsCount += (uint)entry.Raws[section].Count();
                rawOffsets.AddRange(entry.Raws[section].Select(s => s.RawOffset));
                rawDatas.AddRange(entry.Raws[section].Select(s => s.ToArray()));
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
        
        private void Block_RecursiveSearch(ITagBlock tagBlock)
        {
            //Loop through fields
            for (int i = 0; i < tagBlock.Fields.Count; i++)
            {
                //Attempt to convert
                ConvertCacheToGuerilla(tagBlock, tagBlock.Fields[i]);

                //Check
                switch (tagBlock.Fields[i].Type)
                {
                    case Tag.Definition.FieldType.FieldBlock:
                        foreach (Block childTagBlock in ((BaseBlockField)tagBlock.Fields[i]).BlockList)
                            Block_RecursiveSearch(childTagBlock);
                        break;
                    case Tag.Definition.FieldType.FieldStruct:
                        Block_RecursiveSearch((ITagBlock)tagBlock.Fields[i].Value);
                        break;
                }
            }
        }
        
        private void ConvertCacheToGuerilla(ITagBlock tagBlock, Field cacheField)
        {
            //Prepare
            StringId id = StringId.Zero;
            TagReference tagRef = TagReference.Null;

            //Check
            if (!tagBlock.Fields.Contains(cacheField)) return;

            //Get index
            int fieldIndex = tagBlock.Fields.IndexOf(cacheField);
            Field convertedField = cacheField;

            //Handle
            switch (cacheField.Type)
            {
                case Tag.Definition.FieldType.FieldStringId:
                    id = cacheField.Value as StringId? ?? StringId.Zero;
                    convertedField = new StringIdField(cacheField.Name);
                    if (Map.Strings.Count > id.Index && id.Index >= 0)
                        convertedField.Value = (StringValue)Map.Strings[id.Index];
                    break;
                case Tag.Definition.FieldType.FieldOldStringId:
                    id = cacheField.Value as StringId? ?? StringId.Zero;
                    convertedField = new OldStringIdField(cacheField.Name);
                    if (Map.Strings.Count > id.Index && id.Index >= 0)
                        convertedField.Value = (StringValue)Map.Strings[id.Index];
                    break;
                case Tag.Definition.FieldType.FieldTagReference:
                    tagRef = cacheField.Value as TagReference? ?? TagReference.Null;
                    convertedField = new TagReferenceField(cacheField.Name);
                    if(Map.IndexEntries.ContainsID(tagRef.Id))
                    {
                        IndexEntry entry = Map.IndexEntries[tagRef.Id];
                        Group tagGroup = TagLookup.CreateTagGroup(entry.Root);
                        convertedField.Value = (StringValue)Path.Combine(OutputDirectory, $"{entry.Filename}.{tagGroup.Name}");
                    }
                    break;
            }

            //Change
            tagBlock.Fields[fieldIndex] = convertedField;
        }

        private void CacheFileSound_ConvertToGuerilla(Group cacheFileSound, IndexEntry indexEntry, SoundCacheFileGestalt soundGestalt)
        {
            //Get blocks
            Block cacheBlock = (Block)cacheFileSound.TagBlocks[0];
            Block soundBlock = new SoundBlock();
            soundBlock.Initialize();

            //Convert fields
            soundBlock.Fields[0].Value = (int)((short)cacheBlock.Fields[0].Value);  //flags WordFlags -> LongFlags
            soundBlock.Fields[1].Value = cacheBlock.Fields[1].Value;    //class CharEnum -> CharEnum
            soundBlock.Fields[2].Value = cacheBlock.Fields[2].Value;    //sample rate CharEnum -> CharEnum
            soundBlock.Fields[9].Value = cacheBlock.Fields[3].Value;    //encoding CharEnum -> CharEnum
            soundBlock.Fields[10].Value = cacheBlock.Fields[4].Value;   //compression CharEnum -> CharEnum

            //Convert playback index to playback parameters struct block
            if ((short)cacheBlock.Fields[5].Value > -1)
                soundBlock.Fields[5].Value = ((BaseBlockField)soundGestalt.TagBlocks[0].Fields[0]).BlockList[(short)cacheBlock.Fields[5].Value].Fields[0].Value;

            //Convert scale index to scale modifiers struct block
            if ((byte)cacheBlock.Fields[7].Value < byte.MaxValue)
                soundBlock.Fields[6].Value = ((BaseBlockField)soundGestalt.TagBlocks[0].Fields[1]).BlockList[(byte)cacheBlock.Fields[7].Value].Fields[0].Value;

            //Convert promotion index to promotion parameters struct block
            if ((byte)cacheBlock.Fields[8].Value < byte.MaxValue)
                soundBlock.Fields[11].Value = ((BaseBlockField)soundGestalt.TagBlocks[0].Fields[9]).BlockList[(byte)cacheBlock.Fields[8].Value].Fields[0].Value;

            //Replace cache file sound block with sound block
            cacheFileSound.TagBlocks[0] = soundBlock;
        }
    }
}
