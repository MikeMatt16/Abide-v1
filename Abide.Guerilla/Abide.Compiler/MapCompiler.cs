using Abide.Guerilla.Library;
using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using Abide.HaloLibrary.IO;
using Abide.Tag;
using Abide.Tag.Definition;
using Abide.Tag.Guerilla;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Abide.Compiler
{
    /// <summary>
    /// Represents a Halo 2 cache map file compiler.
    /// </summary>
    public sealed class MapCompiler
    {
        private const short C_NullShort = unchecked((short)0xffff);
        private const byte C_NullByte = unchecked(0xff);
        private const uint C_LocationMask = 0xC0000000;
        private const uint C_MainmenuFlag = 0x40000000;
        private const uint C_SharedFlag = 0x80000000;
        private const uint C_SinglePlayerSharedFlag = C_LocationMask;

        /// <summary>
        /// Gets and returns the compiler's sound cache file gestalt tag group file.
        /// </summary>
        public TagGroupFile SoundCacheFileGestaltFile { get; } = new TagGroupFile();
        /// <summary>
        /// Gets and returns the compiler's globals tag group file.
        /// </summary>
        public TagGroupFile GlobalsFile { get; } = new TagGroupFile();
        /// <summary>
        /// Gets and returns the compiler's sound classes tag group file.
        /// </summary>
        public TagGroupFile SoundClasses { get; } = new TagGroupFile();
        /// <summary>
        /// Gets and returns the compiler's combat dialog constants tag group file.
        /// </summary>
        public TagGroupFile CombatDialogueConstant { get; } = new TagGroupFile();
        /// <summary>
        /// Gets and returns the compiler's scenario tag group file.
        /// </summary>
        public TagGroupFile ScenarioFile { get; } = new TagGroupFile();
        /// <summary>
        /// Gets and returns the output Halo 2 cache map file.
        /// </summary>
        public string OutputMapFile { get; private set; }

        private readonly string m_ScenarioFileName;
        private readonly string m_WorkspaceDirectory;
        private MultilingualUnicodeStringsContainer m_MultilingualUnicodeStringsContainer;
        private Dictionary<string, TagGroupFile> m_TagResources;
        private int m_MapType;
        private List<string> m_StringsList;
        private string m_ScenarioPath;
        private string m_MapName;
        private TagId m_CurrentId;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapCompiler"/> class using the specified scenario file.
        /// </summary>
        /// <param name="scenarioFile">The map scenario file name.</param>
        public MapCompiler(string scenarioFileName, string workspaceDirectory)
        {
            m_ScenarioFileName = scenarioFileName;
            m_WorkspaceDirectory = workspaceDirectory;
        }
        /// <summary>
        /// Compiles the scenario file and all references into a single cache map file.
        /// </summary>
        public void Compile()
        {
            //Log
            Console.WriteLine("Starting...");
            DateTime start = DateTime.Now;
            
            //Read scenario
            ScenarioFile.Load(m_ScenarioFileName);
            m_ScenarioPath = m_ScenarioFileName.Replace(Path.Combine(m_WorkspaceDirectory, "tags"), string.Empty).Substring(1).Replace(".scenario", string.Empty);
            m_MapName = Path.GetFileName(m_ScenarioPath);

            //Get map type
            m_MapType = (short)ScenarioFile.TagGroup[0].Fields[2].Value;

            //Save
            OutputMapFile = Path.Combine(m_WorkspaceDirectory, "maps", $"{m_MapName}.map");
            if (!Directory.Exists(Path.Combine(m_WorkspaceDirectory, "maps")))
                Directory.CreateDirectory(Path.Combine(m_WorkspaceDirectory, "maps"));

            //Write
            Console.WriteLine("Map: {0} ({1})", m_MapName, OutputMapFile);

            //Prepare
            m_MultilingualUnicodeStringsContainer = new MultilingualUnicodeStringsContainer();
            m_TagResources = new Dictionary<string, TagGroupFile>();
            m_StringsList = new List<string>(DefaultStrings.GetDefaultStrings());
            m_CurrentId = 0xe1740000;

            //Setup
            SoundCacheFileGestaltFile.TagGroup = new Tag.Guerilla.Generated.SoundCacheFileGestalt();

            //Add flags
            for (int i = 0; i < 686; i++)
            {
                ITagBlock runtimePermutationBitVector = ((BaseBlockField)SoundCacheFileGestaltFile.TagGroup[0].Fields[7]).Add(out bool success);
                if (success) runtimePermutationBitVector.Fields[0].Value = (byte)0;
            }

            //Prepare globals
            Globals_Prepare();

            //Add scenario
            ScenarioFile.Id = m_CurrentId;
            m_TagResources.Add($@"{m_ScenarioPath}.{ScenarioFile.TagGroup.Name}", ScenarioFile);
            m_CurrentId++;

            //Log
            Console.WriteLine("Building scenario tag resources...");

            //Collect scenario referenced tags
            Tag_CollectReferences(ScenarioFile);

            //Log
            Console.WriteLine("Building shared tag resources...");
            Tag_CollectSharedTagReferences();

            //Log
            Console.WriteLine("Buildling globals tag resources...");

            //Collect globals referenced tags
            Tag_CollectReferences(GlobalsFile);
            
            //Build sound gestalt
            SoundCacheFileGestaltFile.Id = m_CurrentId;
            m_TagResources.Add($@"i've got a lovely bunch of coconuts.{SoundCacheFileGestaltFile.TagGroup.Name}", SoundCacheFileGestaltFile);
            m_CurrentId++;

            //Loop
            foreach (KeyValuePair<string, TagGroupFile> resource in m_TagResources)
            {
                //Convert
                string root = resource.Value.TagGroup.GroupTag;
                Console.WriteLine($"Processing {root} {resource.Key}...");

                //Postprocess
                switch (resource.Value.TagGroup.GroupTag)
                {
                    case HaloTags.snd_: Sound_Process(resource.Value); break;
                    case HaloTags.unic: MultilingualUnicodeStringList_Process(resource.Value); break;
                }

                //Convert any child fields to cache format
                Tag_ConvertToCache(resource.Value.TagGroup);
                
                //Write to stream and re-read as cache tag - this will ensure that string IDs, tag references, and tag IDs will be read and written in cache format.
                using (MemoryStream ms = new MemoryStream())
                using (BinaryWriter writer = new BinaryWriter(ms))
                using (BinaryReader reader = new BinaryReader(ms))
                {
                    //Write tag group
                    resource.Value.TagGroup.Write(writer);
                    ms.Seek(0, SeekOrigin.Begin);

                    //Create cache tag group
                    resource.Value.TagGroup = Tag.Cache.Generated.TagLookup.CreateTagGroup(root);
                    resource.Value.TagGroup.Read(reader);
                }
            }

            //Setup sound info
            ITagBlock soundInfo = ((BaseBlockField)GlobalsFile.TagGroup[0].Fields[4]).BlockList[0];
            soundInfo.Fields[4].Value = (TagId)SoundCacheFileGestaltFile.Id;

            //Log
            Console.WriteLine($"Gathered {m_TagResources.Count} tags...");

            //Build file
            Map_BuildFile();
            
            //Empty
            m_MultilingualUnicodeStringsContainer.Clear();
            m_TagResources.Clear();
            m_StringsList.Clear();
            m_ScenarioPath = string.Empty;
            m_MapName = string.Empty;
            m_CurrentId = TagId.Null;
            m_MapType = -1;

            //Done
            Console.WriteLine("Cache file created in {0} seconds.", (DateTime.Now - start).TotalSeconds);
        }
        
        private void Map_BuildFile()
        {
            //Get Tag References
            TagGroupFile[] references = m_TagResources.Select(kvp => kvp.Value).ToArray();
            string[] tagNames = m_TagResources.Select(kvp => kvp.Key.Substring(0, kvp.Key.LastIndexOf('.'))).ToArray();

            //Prepare
            StructureBspBlockHeader bspHeader = new StructureBspBlockHeader();
            Header header = Header.CreateDefault();
            Index index = new Index();
            TagHierarchy[] tags = SharedResources.GetTags();
            ObjectEntry[] objects = new ObjectEntry[references.Length];
            long indexLength = 0, bspAddress = 0, bspLength = 0, tagDataAddress = 0, tagDataLength = 0;

            //Create Map
            using (FileStream fs = new FileStream(OutputMapFile, FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
            using (BinaryWriter mapWriter = new BinaryWriter(fs))
            using (BinaryReader mapReader = new BinaryReader(fs))
            {
                //Setup
                header.ScenarioPath = m_ScenarioPath;
                header.Name = m_MapName;

                //Skip header
                fs.Seek(Header.Length, SeekOrigin.Begin);

                //Write raws
                foreach (var reference in m_TagResources.Where(r => r.Value.TagGroup.GroupTag == HaloTags.ugh_))  //sound cache file gestalt
                    TagGroupFile_WriteRaws(reference.Value, fs, mapWriter);
                foreach (var reference in m_TagResources.Where(r => r.Value.TagGroup.GroupTag == HaloTags.mode))  //render model geometry
                    TagGroupFile_WriteRaws(reference.Value, fs, mapWriter);
                foreach (var reference in m_TagResources.Where(r => r.Value.TagGroup.GroupTag == HaloTags.sbsp))  //structure bsp geometry
                    TagGroupFile_WriteRaws(reference.Value, fs, mapWriter);
                foreach (var reference in m_TagResources.Where(r => r.Value.TagGroup.GroupTag == HaloTags.ltmp))  //structure lightmap geometry
                    TagGroupFile_WriteRaws(reference.Value, fs, mapWriter);
                foreach (var reference in m_TagResources.Where(r => r.Value.TagGroup.GroupTag == HaloTags.weat))  //weather system geometry
                    TagGroupFile_WriteRaws(reference.Value, fs, mapWriter);
                foreach (var reference in m_TagResources.Where(r => r.Value.TagGroup.GroupTag == HaloTags.DECR))  //decorator set geometry
                    TagGroupFile_WriteRaws(reference.Value, fs, mapWriter);
                foreach (var reference in m_TagResources.Where(r => r.Value.TagGroup.GroupTag == HaloTags.ugh_))  //sound gestalt extra info
                    SoundTagGroupFile_WriteExtraInfoRaws(reference.Value, fs, mapWriter);
                foreach (var reference in m_TagResources.Where(r => r.Value.TagGroup.GroupTag == HaloTags.PRTM))  //particle model geometry
                    TagGroupFile_WriteRaws(reference.Value, fs, mapWriter);
                foreach (var reference in m_TagResources.Where(r => r.Value.TagGroup.GroupTag == HaloTags.jmad))  //model animation
                    TagGroupFile_WriteRaws(reference.Value, fs, mapWriter);

                //Build
                Console.WriteLine("Building map tag data table...");

                //Create index
                using (VirtualStream indexStream = new VirtualStream(Index.IndexVirtualAddress))
                using (BinaryWriter indexWriter = new BinaryWriter(indexStream))
                {
                    //Skip header
                    indexStream.Seek(Index.Length, SeekOrigin.Current);

                    //Skip tags
                    indexStream.Seek(tags.Length * TagHierarchy.Length, SeekOrigin.Current);

                    //Skip objects
                    indexStream.Seek(objects.Length * ObjectEntry.Length, SeekOrigin.Current);

                    //Align to 4096 bytes
                    indexStream.Align(4096, 0);

                    //Get length
                    indexLength = indexStream.Length;
                }

                //Setup relevant information
                header.IndexLength = (uint)indexLength;
                index.TagsAddress = Index.IndexTagsAddress;
                index.TagCount = (uint)tags.Length;
                index.ObjectsOffset = (uint)(tags.Length * TagHierarchy.Length) + Index.IndexTagsAddress;
                index.ScenarioId = ScenarioFile.Id;
                index.GlobalsId = GlobalsFile.Id;
                index.ObjectCount = (uint)references.Length;
                index.Tags = "tags";

                //Log
                Console.WriteLine("Writing BSP tag data...");

                //Get Structure BSPs
                foreach (ITagBlock structureBsp in ((BaseBlockField)ScenarioFile.TagGroup[0].Fields[68]).BlockList)
                {
                    //Change field
                    structureBsp.Fields[0] = new StructField<Tag.Cache.Generated.ScenarioStructureBspInfoStructBlock>(string.Empty);
                    ITagBlock scenarioStructureBspInfoStructBlock = (ITagBlock)structureBsp.Fields[0].Value;

                    //Write BSP
                    bspAddress = Index.IndexVirtualAddress + indexLength;
                    TagReference structureBspTagReference = (TagReference)structureBsp.Fields[1].Value;
                    TagReference structureLightmapReference = (TagReference)structureBsp.Fields[2].Value;
                    using (VirtualStream bspDataStream = new VirtualStream(bspAddress))
                    using (BinaryWriter writer = bspDataStream.CreateWriter())
                    {
                        //Skip header
                        bspDataStream.Seek(StructureBspBlockHeader.Length, SeekOrigin.Current);
                        TagGroupFile structureBspFile, structureLightmapFile;
                        if (m_TagResources.Any(kvp => kvp.Value.Id == structureBspTagReference.Id.Dword))
                        {
                            //Write
                            bspHeader.StructureBspOffset = (uint)bspDataStream.Position;
                            structureBspFile = m_TagResources.First(kvp => kvp.Value.Id == structureBspTagReference.Id).Value;
                            structureBspFile.TagGroup.Write(writer);

                            //Align
                            bspDataStream.Align(4096);
                        }
                        bspHeader.StructureLightmapOffset = 0;
                        if (m_TagResources.Any(kvp => kvp.Value.Id == structureLightmapReference.Id.Dword))
                        {
                            //Write
                            bspHeader.StructureLightmapOffset = (uint)bspDataStream.Position;
                            structureLightmapFile = m_TagResources.First(kvp => kvp.Value.Id == structureLightmapReference.Id).Value;
                            structureLightmapFile.TagGroup.Write(writer);

                            //Align
                            bspDataStream.Align(4096);
                        }

                        //Setup header
                        bspHeader.StructureBsp = HaloTags.sbsp;
                        bspHeader.BlockLength = (int)bspDataStream.Length;

                        //Get length
                        if (bspDataStream.Length > bspLength)
                            bspLength = bspDataStream.Length;

                        //Goto
                        bspDataStream.Seek(bspAddress, SeekOrigin.Begin);
                        writer.Write(bspHeader);

                        //Set memory address
                        scenarioStructureBspInfoStructBlock.Fields[0].Value = (int)fs.Position;
                        scenarioStructureBspInfoStructBlock.Fields[1].Value = (int)bspDataStream.Length;
                        scenarioStructureBspInfoStructBlock.Fields[2].Value = (int)bspAddress;

                        //Write
                        mapWriter.Write(bspDataStream.ToArray());
                    }
                }

                //Log
                Console.WriteLine("Writing strings...");

                //Write strings 128
                header.StringCount = (uint)m_StringsList.Count;
                header.Strings128Offset = (uint)fs.Position;
                foreach (string stringId in m_StringsList)
                    mapWriter.WriteUTF8(stringId.PadRight(128, '\0'));

                //Write string index
                int offset = 0;
                header.StringsIndexOffset = (uint)fs.Align(512, 0);
                foreach (string stringId in m_StringsList)
                {
                    mapWriter.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringId) + 1;
                }

                //Write string IDs
                header.StringsOffset = (uint)fs.Align(512, 0);
                foreach (string stringId in m_StringsList)
                    mapWriter.WriteUTF8NullTerminated(stringId);
                header.StringsLength = (uint)m_StringsList.Sum(s => Encoding.UTF8.GetByteCount(s) + 1);

                //Log
                Console.WriteLine("Writing file names...");

                //Write file names
                header.FileNameCount = (uint)tagNames.Length;
                header.FileNamesOffset = (uint)fs.Align(512, 0);
                header.FileNamesLength = (uint)tagNames.Sum(s => Encoding.UTF8.GetByteCount(s) + 1);
                foreach (string fileName in tagNames)
                    mapWriter.WriteUTF8NullTerminated(fileName);

                //Write files Index
                offset = 0;
                header.FileNamesIndexOffset = (uint)fs.Align(512, 0);
                foreach (var fileName in tagNames)
                {
                    mapWriter.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(fileName) + 1;
                }

                //Log
                Console.WriteLine("Writing multilingual unicode strings...");

                //Write english unicode table
                offset = 0;
                uint enIndex = (uint)fs.Align(512, 0);
                for (int i = 0; i < m_MultilingualUnicodeStringsContainer.En.Count; i++)
                {
                    var stringObject = m_MultilingualUnicodeStringsContainer.En[i];
                    mapWriter.Write(StringId.FromString(stringObject.ID, m_StringsList.IndexOf(stringObject.ID)));
                    mapWriter.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringObject.Value) + 1;
                }
                uint enTable = (uint)fs.Align(512, 0);
                using (MemoryStream ms = new MemoryStream())
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    foreach (var stringObject in m_MultilingualUnicodeStringsContainer.En)
                        writer.WriteUTF8NullTerminated(stringObject.Value);
                    m_MultilingualUnicodeStringsContainer.EnSize = (int)ms.Length;
                    mapWriter.Write(ms.ToArray());
                }

                //Write japanese unicode table
                offset = 0;
                uint jpIndex = (uint)fs.Align(512, 0);
                for (int i = 0; i < m_MultilingualUnicodeStringsContainer.Jp.Count; i++)
                {
                    var stringObject = m_MultilingualUnicodeStringsContainer.Jp[i];
                    mapWriter.Write(StringId.FromString(stringObject.ID, m_StringsList.IndexOf(stringObject.ID)));
                    mapWriter.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringObject.Value) + 1;
                }
                uint jpTable = (uint)fs.Align(512, 0);
                using (MemoryStream ms = new MemoryStream())
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    foreach (var stringObject in m_MultilingualUnicodeStringsContainer.Jp)
                        writer.WriteUTF8NullTerminated(stringObject.Value);
                    m_MultilingualUnicodeStringsContainer.JpSize = (int)ms.Length;
                    mapWriter.Write(ms.ToArray());
                }

                //Write german unicode table
                offset = 0;
                uint nlIndex = (uint)fs.Align(512, 0);
                for (int i = 0; i < m_MultilingualUnicodeStringsContainer.Nl.Count; i++)
                {
                    var stringObject = m_MultilingualUnicodeStringsContainer.Nl[i];
                    mapWriter.Write(StringId.FromString(stringObject.ID, m_StringsList.IndexOf(stringObject.ID)));
                    mapWriter.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringObject.Value) + 1;
                }
                uint nlTable = (uint)fs.Align(512, 0);
                using (MemoryStream ms = new MemoryStream())
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    foreach (var stringObject in m_MultilingualUnicodeStringsContainer.Nl)
                        writer.WriteUTF8NullTerminated(stringObject.Value);
                    m_MultilingualUnicodeStringsContainer.NlSize = (int)ms.Length;
                    mapWriter.Write(ms.ToArray());
                }

                //Write french unicode table
                offset = 0;
                uint frIndex = (uint)fs.Align(512, 0);
                for (int i = 0; i < m_MultilingualUnicodeStringsContainer.Fr.Count; i++)
                {
                    var stringObject = m_MultilingualUnicodeStringsContainer.Fr[i];
                    mapWriter.Write(StringId.FromString(stringObject.ID, m_StringsList.IndexOf(stringObject.ID)));
                    mapWriter.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringObject.Value) + 1;
                }
                uint frTable = (uint)fs.Align(512, 0);
                using (MemoryStream ms = new MemoryStream())
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    foreach (var stringObject in m_MultilingualUnicodeStringsContainer.Fr)
                        writer.WriteUTF8NullTerminated(stringObject.Value);
                    m_MultilingualUnicodeStringsContainer.FrSize = (int)ms.Length;
                    mapWriter.Write(ms.ToArray());
                }

                //Write spanish unicode table
                offset = 0;
                uint esIndex = (uint)fs.Align(512, 0);
                for (int i = 0; i < m_MultilingualUnicodeStringsContainer.Es.Count; i++)
                {
                    var stringObject = m_MultilingualUnicodeStringsContainer.Es[i];
                    mapWriter.Write(StringId.FromString(stringObject.ID, m_StringsList.IndexOf(stringObject.ID)));
                    mapWriter.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringObject.Value) + 1;
                }
                uint esTable = (uint)fs.Align(512, 0);
                using (MemoryStream ms = new MemoryStream())
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    foreach (var stringObject in m_MultilingualUnicodeStringsContainer.Es)
                        writer.WriteUTF8NullTerminated(stringObject.Value);
                    m_MultilingualUnicodeStringsContainer.EsSize = (int)ms.Length;
                    mapWriter.Write(ms.ToArray());
                }

                //Write italian unicode table
                offset = 0;
                uint itIndex = (uint)fs.Align(512, 0);
                for (int i = 0; i < m_MultilingualUnicodeStringsContainer.It.Count; i++)
                {
                    var stringObject = m_MultilingualUnicodeStringsContainer.It[i];
                    mapWriter.Write(StringId.FromString(stringObject.ID, m_StringsList.IndexOf(stringObject.ID)));
                    mapWriter.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringObject.Value) + 1;
                }
                uint itTable = (uint)fs.Align(512, 0);
                using (MemoryStream ms = new MemoryStream())
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    foreach (var stringObject in m_MultilingualUnicodeStringsContainer.It)
                        writer.WriteUTF8NullTerminated(stringObject.Value);
                    m_MultilingualUnicodeStringsContainer.ItSize = (int)ms.Length;
                    mapWriter.Write(ms.ToArray());
                }

                //Write korean unicode table
                offset = 0;
                uint krIndex = (uint)fs.Align(512, 0);
                for (int i = 0; i < m_MultilingualUnicodeStringsContainer.Kr.Count; i++)
                {
                    var stringObject = m_MultilingualUnicodeStringsContainer.Kr[i];
                    mapWriter.Write(StringId.FromString(stringObject.ID, m_StringsList.IndexOf(stringObject.ID)));
                    mapWriter.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringObject.Value) + 1;
                }
                uint krTable = (uint)fs.Align(512, 0);
                using (MemoryStream ms = new MemoryStream())
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    foreach (var stringObject in m_MultilingualUnicodeStringsContainer.Kr)
                        writer.WriteUTF8NullTerminated(stringObject.Value);
                    m_MultilingualUnicodeStringsContainer.KrSize = (int)ms.Length;
                    mapWriter.Write(ms.ToArray());
                }

                //Write chinese unicode table
                offset = 0;
                uint zhIndex = (uint)fs.Align(512, 0);
                for (int i = 0; i < m_MultilingualUnicodeStringsContainer.Zh.Count; i++)
                {
                    var stringObject = m_MultilingualUnicodeStringsContainer.Zh[i];
                    mapWriter.Write(StringId.FromString(stringObject.ID, m_StringsList.IndexOf(stringObject.ID)));
                    mapWriter.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringObject.Value) + 1;
                }
                uint zhTable = (uint)fs.Align(512, 0);
                using (MemoryStream ms = new MemoryStream())
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    foreach (var stringObject in m_MultilingualUnicodeStringsContainer.Zh)
                        writer.WriteUTF8NullTerminated(stringObject.Value);
                    m_MultilingualUnicodeStringsContainer.ZhSize = (int)ms.Length;
                    mapWriter.Write(ms.ToArray());
                }

                //Write portuguese unicode table
                offset = 0;
                uint prIndex = (uint)fs.Align(512, 0);
                for (int i = 0; i < m_MultilingualUnicodeStringsContainer.Pr.Count; i++)
                {
                    var stringObject = m_MultilingualUnicodeStringsContainer.Pr[i];
                    mapWriter.Write(StringId.FromString(stringObject.ID, m_StringsList.IndexOf(stringObject.ID)));
                    mapWriter.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringObject.Value) + 1;
                }
                uint prTable = (uint)fs.Align(512, 0);
                using (MemoryStream ms = new MemoryStream())
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    foreach (var stringObject in m_MultilingualUnicodeStringsContainer.Pr)
                        writer.WriteUTF8NullTerminated(stringObject.Value);
                    m_MultilingualUnicodeStringsContainer.PrSize = (int)ms.Length;
                    mapWriter.Write(ms.ToArray());
                }

                //Write credits into crazy
                Console.WriteLine("Writing credits...");
                header.CrazyOffset = (uint)fs.Align(512, 0);
                using (MemoryStream ms = new MemoryStream())
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    //Write credits
                    string credits = string.Format(Properties.Resources.Credits, DateTime.Now.ToLongDateString());
                    writer.WriteUTF8(credits);
                    header.CrazyLength = (uint)ms.Length;
                    mapWriter.Write(ms.ToArray());
                };

                //Write bitmaps
                foreach (TagGroupFile tagGroupFile in references.Where(r => r.TagGroup.GroupTag == HaloTags.bitm))  //bitmap
                    TagGroupFile_WriteRaws(tagGroupFile, fs, mapWriter);

                //Log
                Console.WriteLine("Writing index...");

                //Write tags to virtual stream
                tagDataAddress = bspAddress + bspLength;
                using (VirtualStream tagDataStream = new VirtualStream(tagDataAddress))
                using (BinaryWriter writer = new BinaryWriter(tagDataStream))
                {
                    //Loop through each reference
                    for (int i = 0; i < references.Length; i++)
                    {
                        //Check
                        TagGroupFile tagGroupFile = references[i];
                        objects[i] = new ObjectEntry() { Id = references[i].Id, Tag = references[i].TagGroup.GroupTag };
                        if (objects[i].Tag == HaloTags.sbsp || objects[i].Tag == HaloTags.ltmp) continue;

                        //Write tag to virtual stream
                        using (VirtualStream tagStream = new VirtualStream(tagDataStream.Position))
                        using (BinaryWriter tagWriter = tagStream.CreateWriter())
                        {
                            //Write
                            tagGroupFile.TagGroup.Write(tagWriter);

                            //Get length and address
                            objects[i].Size = (uint)tagStream.Length;
                            objects[i].Offset = (uint)tagStream.MemoryAddress;

                            //Write tag stream to the underlying tag data stream
                            writer.Write(tagStream.ToArray());
                        }
                    }

                    //Align tag data
                    tagDataStream.Align(4096);
                    tagDataLength = tagDataStream.Length;

                    //Write index
                    using (VirtualStream indexStream = new VirtualStream(Index.IndexVirtualAddress, new byte[indexLength]))
                    using (BinaryWriter indexWriter = indexStream.CreateWriter())
                    {
                        //Write header
                        indexWriter.Write(index);

                        //Write tags
                        foreach (TagHierarchy tag in tags)
                            indexWriter.Write(tag);

                        //Write objects
                        foreach (ObjectEntry objectEntry in objects)
                            indexWriter.Write(objectEntry);

                        //Setup header
                        header.IndexOffset = (uint)fs.Align(512, 0);
                        header.MapDataLength = (uint)(indexLength + bspLength + tagDataLength);
                        header.TagDataLength = (uint)tagDataLength;

                        //Write index to map
                        mapWriter.Write(indexStream.ToArray());
                    }

                    //Log
                    Console.WriteLine("Writing tag data...");

                    //Write tag data
                    mapWriter.Write(tagDataStream.ToArray());
                }

                //Align
                fs.Align(1024);

                //Goto string information in globals
                fs.Seek(header.IndexOffset + header.IndexLength + 400, SeekOrigin.Begin);

                //write english
                mapWriter.Write(m_MultilingualUnicodeStringsContainer.En.Count);
                mapWriter.Write(m_MultilingualUnicodeStringsContainer.EnSize);
                mapWriter.Write(enIndex);
                mapWriter.Write(enTable);

                //write japanese
                fs.Seek(12, SeekOrigin.Current);
                mapWriter.Write(m_MultilingualUnicodeStringsContainer.Jp.Count);
                mapWriter.Write(m_MultilingualUnicodeStringsContainer.JpSize);
                mapWriter.Write(jpIndex);
                mapWriter.Write(jpTable);

                //write german
                fs.Seek(12, SeekOrigin.Current);
                mapWriter.Write(m_MultilingualUnicodeStringsContainer.Nl.Count);
                mapWriter.Write(m_MultilingualUnicodeStringsContainer.NlSize);
                mapWriter.Write(nlIndex);
                mapWriter.Write(nlTable);

                //write french
                fs.Seek(12, SeekOrigin.Current);
                mapWriter.Write(m_MultilingualUnicodeStringsContainer.Fr.Count);
                mapWriter.Write(m_MultilingualUnicodeStringsContainer.FrSize);
                mapWriter.Write(frIndex);
                mapWriter.Write(frTable);

                //write spanish
                fs.Seek(12, SeekOrigin.Current);
                mapWriter.Write(m_MultilingualUnicodeStringsContainer.Es.Count);
                mapWriter.Write(m_MultilingualUnicodeStringsContainer.EsSize);
                mapWriter.Write(esIndex);
                mapWriter.Write(esTable);

                //write italian
                fs.Seek(12, SeekOrigin.Current);
                mapWriter.Write(m_MultilingualUnicodeStringsContainer.It.Count);
                mapWriter.Write(m_MultilingualUnicodeStringsContainer.ItSize);
                mapWriter.Write(itIndex);
                mapWriter.Write(itTable);

                //write korean
                fs.Seek(12, SeekOrigin.Current);
                mapWriter.Write(m_MultilingualUnicodeStringsContainer.Kr.Count);
                mapWriter.Write(m_MultilingualUnicodeStringsContainer.KrSize);
                mapWriter.Write(krIndex);
                mapWriter.Write(krTable);

                //write chinese
                fs.Seek(12, SeekOrigin.Current);
                mapWriter.Write(m_MultilingualUnicodeStringsContainer.Zh.Count);
                mapWriter.Write(m_MultilingualUnicodeStringsContainer.ZhSize);
                mapWriter.Write(zhIndex);
                mapWriter.Write(zhTable);

                //write portuguese
                fs.Seek(12, SeekOrigin.Current);
                mapWriter.Write(m_MultilingualUnicodeStringsContainer.Pr.Count);
                mapWriter.Write(m_MultilingualUnicodeStringsContainer.PrSize);
                mapWriter.Write(prIndex);
                mapWriter.Write(prTable);

                //Log
                Console.WriteLine("Finalizing header...");
                header.FileLength = (uint)fs.Length;

                //Sign
                header.Checksum = 0;
                fs.Seek(2048, SeekOrigin.Begin);
                for (int i = 0; i < (header.FileLength - 2048) / 4; i++)
                    header.Checksum ^= mapReader.ReadUInt32();

                //Write header
                fs.Seek(0, SeekOrigin.Begin);
                mapWriter.Write(header);
            }
        }

        private void Globals_Prepare()
        {
            //Create globals tag
            GlobalsFile.Id = m_CurrentId;
            GlobalsFile.TagGroup = new Tag.Cache.Generated.Globals();
            m_TagResources.Add($@"globals\globals.{GlobalsFile.TagGroup.Name}", GlobalsFile);
            m_CurrentId++;

            //Create sound classes tag
            SoundClasses.Id = m_CurrentId;
            SoundClasses.TagGroup = new Tag.Cache.Generated.SoundClasses();
            m_TagResources.Add($@"sound\sound_classes.{SoundClasses.TagGroup.Name}", SoundClasses);
            m_CurrentId++;

            //Create sound classes tag
            CombatDialogueConstant.Id = m_CurrentId;
            CombatDialogueConstant.TagGroup = new Tag.Cache.Generated.SoundDialogueConstants();
            m_TagResources.Add($@"sound\combat_dialogue_constants.{CombatDialogueConstant.TagGroup.Name}", CombatDialogueConstant);
            m_CurrentId++;

            //Read globals file
            using (Stream globalsStream = SharedResources.GetMultiplayerSharedGlobals())
                GlobalsFile.Load(globalsStream);

            //Read sound classes
            using (Stream soundClassesStream = SharedResources.GetSoundClasses())
                SoundClasses.Load(soundClassesStream);

            //Read combat dialog constants
            using (Stream combatDialogConstantsStream = SharedResources.GetCombatDialogConstants())
                CombatDialogueConstant.Load(combatDialogConstantsStream);
        }

        private void Tag_CollectSharedTagReferences()
        {
            //Prepare
            TagGroupFile file = null;
            string fileName = null;

            //Handle
            switch (m_MapType)
            {
                case 1: //multiplayer map
                    foreach (string tagFileName in SharedResources.MultiplayerSharedResourceFileNames)
                        if(!m_TagResources.ContainsKey(tagFileName))    //Check
                        {
                            //Setup
                            file = new TagGroupFile();
                            fileName = Path.Combine(m_WorkspaceDirectory, "tags", tagFileName);
                            if (File.Exists(fileName)) //Check
                            {
                                //Add
                                file.Load(fileName);

                                //Check
                                if (file.TagGroup is Tag.Guerilla.Generated.SoundCacheFileGestalt) break;

                                //Add
                                file.Id = m_CurrentId;
                                m_TagResources.Add(tagFileName, file);
                                m_CurrentId++;

                                //Add references
                                Tag_CollectReferences(file);
                            }
                            else throw new NotImplementedException();
                        }
                    break;
                default:
                    break;
            }
        }

        private void Tag_CollectReferences(TagGroupFile tagGroupFile)
        {
            //Loop
            for (int i = 0; i < tagGroupFile.TagGroup.Count; i++)
                Tag_CollectReferences(tagGroupFile.TagGroup[i]);
        }

        private void Tag_CollectReferences(ITagBlock tagBlock)
        {
            Field field = null;
            for (int i = 0; i < tagBlock.Fields.Count; i++)
            {
                //Get field
                field = tagBlock.Fields[i];

                //Handle
                switch (field.Type)
                {
                    case FieldType.FieldBlock:
                        BaseBlockField blockField = (BaseBlockField)field;
                        foreach (ITagBlock block in blockField.BlockList)
                            Tag_CollectReferences(block);
                        break;

                    case FieldType.FieldStruct:
                        if (field.Value is ITagBlock)
                            Tag_CollectReferences((ITagBlock)field.Value);
                        break;

                    case FieldType.FieldTagIndex:
                    case FieldType.FieldTagReference:
                        if (field.Value is StringValue tagString && !string.IsNullOrEmpty(tagString.Value))
                        {
                            //Prepare
                            TagGroupFile file = new TagGroupFile();
                            string fileName = Path.Combine(RegistrySettings.WorkspaceDirectory, "tags", tagString.Value);
                            if (File.Exists(fileName))
                            {
                                //Check
                                if (!m_TagResources.ContainsKey(tagString))
                                {
                                    //Add
                                    file.Load(fileName);

                                    //Check
                                    if (file.TagGroup is Tag.Guerilla.Generated.SoundCacheFileGestalt) break;

                                    //Add
                                    file.Id = m_CurrentId;
                                    m_TagResources.Add(tagString, file);
                                    m_CurrentId++;

                                    //Add references
                                    Tag_CollectReferences(file);
                                }
                            }
                            else throw new NotImplementedException();
                        }
                        break;

                    case FieldType.FieldOldStringId:
                    case FieldType.FieldStringId:
                        if (field.Value is StringValue stringId && !string.IsNullOrEmpty(stringId.Value))
                        {
                            //Check
                            if (!m_StringsList.Contains(stringId.Value))
                                m_StringsList.Add(stringId.Value);
                        }
                        break;
                }
            }
        }

        private void Tag_ConvertToCache(ITagGroup guerillaTagGroup)
        {
            //Loop
            foreach (ITagBlock tagBlock in guerillaTagGroup)
                Tag_Convert(tagBlock);
        }

        private void Tag_Convert(ITagBlock tagBlock)
        {
            //Prepare
            string stringValue = null;

            //Loop through fields
            for (int i = 0; i < tagBlock.Fields.Count; i++)
            {
                //Get current field
                Field currentField = tagBlock.Fields[i];
                stringValue = currentField.Value?.ToString() ?? string.Empty;   //Get string representation of field
            
                //Handle type
                switch(currentField.Type)
                {
                    case FieldType.FieldBlock:
                        foreach (ITagBlock nestedTagBlock in ((BaseBlockField)currentField).BlockList)
                            Tag_Convert(nestedTagBlock);
                        break;
                    case FieldType.FieldStruct:
                        Tag_Convert((ITagBlock)currentField.Value);
                        break;

                    case FieldType.FieldOldStringId:
                        Tag.Cache.OldStringIdField oldStringIdField = new Tag.Cache.OldStringIdField(currentField.GetName());
                        tagBlock.Fields[i] = oldStringIdField;
                        if (!string.IsNullOrEmpty(stringValue))
                        {
                            if (!m_StringsList.Contains(stringValue)) m_StringsList.Add(stringValue);
                            oldStringIdField.Value = StringId.FromString(stringValue, m_StringsList.IndexOf(stringValue));
                        }
                        break;
                    case FieldType.FieldStringId:
                        Tag.Cache.StringIdField stringIdField = new Tag.Cache.StringIdField(currentField.GetName());
                        tagBlock.Fields[i] = stringIdField;
                        if (!string.IsNullOrEmpty(stringValue))
                        {
                            if (!m_StringsList.Contains(stringValue)) m_StringsList.Add(stringValue);
                            stringIdField.Value = StringId.FromString(stringValue, m_StringsList.IndexOf(stringValue));
                        }
                        break;

                    case FieldType.FieldTagIndex:
                        Tag.Cache.TagIndexField tagIndexField = new Tag.Cache.TagIndexField(currentField.GetName());
                        tagBlock.Fields[i] = tagIndexField;
                        if (!string.IsNullOrEmpty(stringValue) && m_TagResources.ContainsKey(stringValue))
                            tagIndexField.Value = (TagId)m_TagResources[stringValue].Id;
                        break;

                    case FieldType.FieldTagReference:
                        TagReference tagReference = new TagReference() { Tag = ((TagReferenceField)currentField).GroupTag };
                        Tag.Cache.TagReferenceField tagReferenceField = new Tag.Cache.TagReferenceField(currentField.GetName(), ((TagReferenceField)currentField).GroupTag);
                        tagBlock.Fields[i] = tagReferenceField;
                        if (!string.IsNullOrEmpty(stringValue) && m_TagResources.ContainsKey(stringValue))
                            tagReference.Id = (TagId)m_TagResources[stringValue].Id;
                        tagReferenceField.Value = tagReference;
                        break;
                }
            }
        }

        private void MultilingualUnicodeStringList_Process(TagGroupFile value)
        {
            //Prepare
            StringContainer strings = new StringContainer();
            byte[] stringData = ((DataField)value.TagGroup[0].Fields[1]).GetBuffer();
            string unicodeString = string.Empty;
            StringValue stringId = new StringValue();
            int offset = 0;
            
            //Create stream
            using (MemoryStream ms = new MemoryStream(stringData))
            using (BinaryReader reader = new BinaryReader(ms))
            {
                //Loop
                foreach (ITagBlock stringReferenceBlock in ((BaseBlockField)value.TagGroup[0].Fields[0]).BlockList)
                {
                    //Get
                    stringId = (StringValue)stringReferenceBlock.Fields[0].Value;

                    //Goto English
                    offset = (int)stringReferenceBlock.Fields[1].Value;
                    if (offset >= 0)
                    {
                        ms.Seek(offset, SeekOrigin.Begin);
                        unicodeString = reader.ReadUTF8NullTerminated();
                        strings.English.Add(new StringEntry(unicodeString, stringId.Value));
                    }

                    //Goto Japanese
                    offset = (int)stringReferenceBlock.Fields[2].Value;
                    if (offset >= 0)
                    {
                        ms.Seek(offset, SeekOrigin.Begin);
                        unicodeString = reader.ReadUTF8NullTerminated();
                        strings.Japanese.Add(new StringEntry(unicodeString, stringId.Value));
                    }

                    //Goto German
                    offset = (int)stringReferenceBlock.Fields[3].Value;
                    if (offset >= 0)
                    {
                        ms.Seek(offset, SeekOrigin.Begin);
                        unicodeString = reader.ReadUTF8NullTerminated();
                        strings.German.Add(new StringEntry(unicodeString, stringId.Value));
                    }

                    //Goto French
                    offset = (int)stringReferenceBlock.Fields[4].Value;
                    if (offset >= 0)
                    {
                        ms.Seek(offset, SeekOrigin.Begin);
                        unicodeString = reader.ReadUTF8NullTerminated();
                        strings.French.Add(new StringEntry(unicodeString, stringId.Value));
                    }

                    //Goto Spanish
                    offset = (int)stringReferenceBlock.Fields[5].Value;
                    if (offset >= 0)
                    {
                        ms.Seek(offset, SeekOrigin.Begin);
                        unicodeString = reader.ReadUTF8NullTerminated();
                        strings.Spanish.Add(new StringEntry(unicodeString, stringId.Value));
                    }

                    //Goto Italian
                    offset = (int)stringReferenceBlock.Fields[6].Value;
                    if (offset >= 0)
                    {
                        ms.Seek(offset, SeekOrigin.Begin);
                        unicodeString = reader.ReadUTF8NullTerminated();
                        strings.Italian.Add(new StringEntry(unicodeString, stringId.Value));
                    }

                    //Goto Korean
                    offset = (int)stringReferenceBlock.Fields[7].Value;
                    if (offset >= 0)
                    {
                        ms.Seek(offset, SeekOrigin.Begin);
                        unicodeString = reader.ReadUTF8NullTerminated();
                        strings.Korean.Add(new StringEntry(unicodeString, stringId.Value));
                    }

                    //Goto Chinese
                    offset = (int)stringReferenceBlock.Fields[8].Value;
                    if (offset >= 0)
                    {
                        ms.Seek(offset, SeekOrigin.Begin);
                        unicodeString = reader.ReadUTF8NullTerminated();
                        strings.Chinese.Add(new StringEntry(unicodeString, stringId.Value));
                    }

                    //Goto Portuguese
                    offset = (int)stringReferenceBlock.Fields[9].Value;
                    if (offset >= 0)
                    {
                        ms.Seek(offset, SeekOrigin.Begin);
                        unicodeString = reader.ReadUTF8NullTerminated();
                        strings.Portuguese.Add(new StringEntry(unicodeString, stringId.Value));
                    }
                }
            }

            //Remove references and data
            ((BaseBlockField)value.TagGroup[0].Fields[0]).BlockList.Clear();
            ((DataField)value.TagGroup[0].Fields[1]).SetBuffer(new byte[0]);

            //Prepare
            byte[] padding = (byte[])((PadField)value.TagGroup[0].Fields[2]).Value;
            using (MemoryStream ms = new MemoryStream(padding))
            using (BinaryWriter writer = new BinaryWriter(ms))
            {
                //Write English
                writer.Write((ushort)m_MultilingualUnicodeStringsContainer.En.Count);
                writer.Write((ushort)strings.English.Count);
                m_MultilingualUnicodeStringsContainer.En.AddRange(strings.English);

                //Write Japanese
                writer.Write((ushort)m_MultilingualUnicodeStringsContainer.Jp.Count);
                writer.Write((ushort)strings.Japanese.Count);
                m_MultilingualUnicodeStringsContainer.Jp.AddRange(strings.Japanese);

                //Write German
                writer.Write((ushort)m_MultilingualUnicodeStringsContainer.Nl.Count);
                writer.Write((ushort)strings.German.Count);
                m_MultilingualUnicodeStringsContainer.Nl.AddRange(strings.German);

                //Write French
                writer.Write((ushort)m_MultilingualUnicodeStringsContainer.Fr.Count);
                writer.Write((ushort)strings.French.Count);
                m_MultilingualUnicodeStringsContainer.Fr.AddRange(strings.French);

                //Write Spanish
                writer.Write((ushort)m_MultilingualUnicodeStringsContainer.Es.Count);
                writer.Write((ushort)strings.Spanish.Count);
                m_MultilingualUnicodeStringsContainer.Es.AddRange(strings.Spanish);

                //Write Italian
                writer.Write((ushort)m_MultilingualUnicodeStringsContainer.It.Count);
                writer.Write((ushort)strings.Italian.Count);
                m_MultilingualUnicodeStringsContainer.It.AddRange(strings.Italian);

                //Write Korean
                writer.Write((ushort)m_MultilingualUnicodeStringsContainer.Kr.Count);
                writer.Write((ushort)strings.Korean.Count);
                m_MultilingualUnicodeStringsContainer.Kr.AddRange(strings.Korean);

                //Write Chinese
                writer.Write((ushort)m_MultilingualUnicodeStringsContainer.Zh.Count);
                writer.Write((ushort)strings.Chinese.Count);
                m_MultilingualUnicodeStringsContainer.Zh.AddRange(strings.Chinese);

                //Write Portuguese
                writer.Write((ushort)m_MultilingualUnicodeStringsContainer.Pr.Count);
                writer.Write((ushort)strings.Portuguese.Count);
                m_MultilingualUnicodeStringsContainer.Pr.AddRange(strings.Portuguese);
            }
        }

        private void Sound_Process(TagGroupFile soundTag)
        {
            //Prepare
            ITagGroup soundCacheFileGestalt = SoundCacheFileGestaltFile.TagGroup;
            ITagGroup cacheFileSound = new Tag.Cache.Generated.Sound();
            ITagGroup sound = soundTag.TagGroup;
            bool success = false;
            int index = 0;

            //Get tag blocks
            ITagBlock soundCacheFileGestaltBlock = soundCacheFileGestalt[0];
            ITagBlock cacheFileSoundBlock = cacheFileSound[0];
            ITagBlock soundBlock = sound[0];

            //Transfer raws
            foreach (int rawOffset in soundTag.GetRawOffsets()) SoundCacheFileGestaltFile.SetRaw(rawOffset, soundTag.GetRaw(rawOffset));
            
            //Get block fields from sound cache file gestalt
            BaseBlockField playbacks = (BaseBlockField)soundCacheFileGestaltBlock.Fields[0];
            BaseBlockField scales = (BaseBlockField)soundCacheFileGestaltBlock.Fields[1];
            BaseBlockField importNames = (BaseBlockField)soundCacheFileGestaltBlock.Fields[2];
            BaseBlockField pitchRangeParameters = (BaseBlockField)soundCacheFileGestaltBlock.Fields[3];
            BaseBlockField pitchRanges = (BaseBlockField)soundCacheFileGestaltBlock.Fields[4];
            BaseBlockField permutations = (BaseBlockField)soundCacheFileGestaltBlock.Fields[5];
            BaseBlockField customPlaybacks = (BaseBlockField)soundCacheFileGestaltBlock.Fields[6];
            BaseBlockField runtimePermutationFlags = (BaseBlockField)soundCacheFileGestaltBlock.Fields[7];
            BaseBlockField chunks = (BaseBlockField)soundCacheFileGestaltBlock.Fields[8];
            BaseBlockField promotions = (BaseBlockField)soundCacheFileGestaltBlock.Fields[9];
            BaseBlockField extraInfos = (BaseBlockField)soundCacheFileGestaltBlock.Fields[10];
            
            //Change
            soundTag.TagGroup = cacheFileSound;

            //Convert fields
            cacheFileSoundBlock.Fields[0].Value = (short)(int)soundBlock.Fields[0].Value;   //flags
            cacheFileSoundBlock.Fields[1].Value = soundBlock.Fields[1].Value;               //class
            cacheFileSoundBlock.Fields[2].Value = soundBlock.Fields[2].Value;               //sample rate
            cacheFileSoundBlock.Fields[3].Value = soundBlock.Fields[9].Value;               //encoding
            cacheFileSoundBlock.Fields[4].Value = soundBlock.Fields[10].Value;              //compression

            //Read 'extra' data that I chose to store in a pad field
            bool validPromotion = false;
            using (MemoryStream ms = new MemoryStream((byte[])soundBlock.Fields[12].Value))
            using(BinaryReader reader = new BinaryReader(ms))
            {
                cacheFileSoundBlock.Fields[12].Value = reader.ReadInt32();  //max playback time
                validPromotion = reader.ReadByte() != C_NullByte;
            }

            //Add or get playback index
            cacheFileSoundBlock.Fields[5].Value = (short)SoundGestalt_FindPlaybackIndex((ITagBlock)soundBlock.Fields[5].Value);

            //Add scale
            cacheFileSoundBlock.Fields[8].Value = (byte)(sbyte)SoundGestalt_FindScaleIndex((ITagBlock)soundBlock.Fields[6].Value);

            //Add promotion
            ITagBlock soundPromotionParametersStruct = (ITagBlock)soundBlock.Fields[11].Value;
            if(((BaseBlockField)soundPromotionParametersStruct.Fields[0]).BlockList.Count > 0)
                cacheFileSoundBlock.Fields[9].Value = (byte)(sbyte)SoundGestalt_FindPromotionIndex((ITagBlock)soundBlock.Fields[11].Value);
            else cacheFileSoundBlock.Fields[9].Value = C_NullByte;

            //Add custom playback
            if (((BaseBlockField)soundBlock.Fields[14]).BlockList.Count > 0)
            {
                index = customPlaybacks.BlockList.Count;
                ITagBlock customPlayback = customPlaybacks.Add(out success);
                if (success)
                {
                    cacheFileSoundBlock.Fields[10].Value = (byte)index;
                    customPlayback.Fields[0].Value = (ITagBlock)((BaseBlockField)soundBlock.Fields[14]).BlockList[0].Fields[0].Value;
                }
                else cacheFileSoundBlock.Fields[10].Value = C_NullByte;
            }
            else cacheFileSoundBlock.Fields[10].Value = C_NullByte;

            //Add extra info
            if (((BaseBlockField)soundBlock.Fields[15]).BlockList.Count > 0)
            {
                index = extraInfos.BlockList.Count;
                ITagBlock soundExtraInfo = ((BaseBlockField)soundBlock.Fields[15]).BlockList[0];
                ITagBlock extraInfo = extraInfos.Add(out success);
                if (success)
                {
                    cacheFileSoundBlock.Fields[11].Value = (short)index;
                    extraInfo.Fields[1].Value = soundExtraInfo.Fields[2].Value;
                    ((ITagBlock)extraInfo.Fields[1].Value).Fields[6].Value = new StringValue($"i've got a lovely bunch of coconuts.{soundCacheFileGestalt.Name}");
                    foreach (ITagBlock block in ((BaseBlockField)soundExtraInfo.Fields[1]).BlockList)
                        ((BaseBlockField)extraInfo.Fields[0]).BlockList.Add(block);
                }
                else cacheFileSoundBlock.Fields[11].Value = C_NullShort;
            }
            else cacheFileSoundBlock.Fields[11].Value = C_NullShort;

            //Add pitch range
            cacheFileSoundBlock.Fields[7].Value = (byte)((BaseBlockField)soundBlock.Fields[13]).BlockList.Count;
            foreach (var soundPitchRange in ((BaseBlockField)soundBlock.Fields[13]).BlockList)
            {
                index = pitchRanges.BlockList.Count;
                ITagBlock gestaltPitchRange = pitchRanges.Add(out success);
                if (success)
                {
                    //Set pitch range
                    cacheFileSoundBlock.Fields[6].Value = (short)index;

                    //Add import name
                    gestaltPitchRange.Fields[0].Value = (short)SoundGestalt_FindImportNameIndex((StringValue)soundPitchRange.Fields[0].Value);

                    //Add pitch range parameter
                    gestaltPitchRange.Fields[1].Value = (short)SoundGestalt_FindPitchRangeParameter((short)soundPitchRange.Fields[2].Value,
                        (ShortBounds)soundPitchRange.Fields[4].Value, (ShortBounds)soundPitchRange.Fields[5].Value);

                    //Add permutation
                    gestaltPitchRange.Fields[4].Value = (short)permutations.BlockList.Count;
                    gestaltPitchRange.Fields[5].Value = (short)((BaseBlockField)soundPitchRange.Fields[7]).BlockList.Count;

                    //Loop
                    foreach (ITagBlock soundPermutation in ((BaseBlockField)soundPitchRange.Fields[7]).BlockList)
                    {
                        ITagBlock gestaltPermutation = permutations.Add(out success);
                        if (success)
                        {
                            //Add import name
                            gestaltPermutation.Fields[0].Value = (short)SoundGestalt_FindImportNameIndex((StringValue)soundPermutation.Fields[0].Value);

                            //Convert fields
                            gestaltPermutation.Fields[1].Value = (short)((float)soundPermutation.Fields[1].Value * 65535f);
                            gestaltPermutation.Fields[2].Value = (byte)((float)soundPermutation.Fields[2].Value * 255f);
                            gestaltPermutation.Fields[3].Value = (byte)(sbyte)(short)soundPermutation.Fields[4].Value;
                            gestaltPermutation.Fields[4].Value = (short)soundPermutation.Fields[5].Value;
                            gestaltPermutation.Fields[5].Value = (int)soundPermutation.Fields[3].Value;

                            //Add chunks
                            gestaltPermutation.Fields[6].Value = (short)chunks.BlockList.Count;
                            gestaltPermutation.Fields[7].Value = (short)((BaseBlockField)soundPermutation.Fields[6]).BlockList.Count;

                            //Loop
                            foreach (ITagBlock soundChunk in ((BaseBlockField)soundPermutation.Fields[6]).BlockList)
                                chunks.BlockList.Add(soundChunk);
                        }
                        else
                        {
                            gestaltPitchRange.Fields[4].Value = C_NullShort;
                            gestaltPitchRange.Fields[5].Value = (short)0;
                        }
                    }
                }
                else
                {
                    cacheFileSoundBlock.Fields[6].Value = C_NullShort;
                    cacheFileSoundBlock.Fields[7].Value = C_NullByte;
                }
            }
        }

        private int SoundGestalt_FindPitchRangeParameter(short s1, ShortBounds sb1, ShortBounds sb2)
        {
            //Prepare
            ITagBlock soundCacheFileGestaltBlock = SoundCacheFileGestaltFile.TagGroup[0];
            BaseBlockField blockField = (BaseBlockField)soundCacheFileGestaltBlock.Fields[3];
            int index = -1;

            //Check
            foreach (ITagBlock gestaltBlock in blockField.BlockList)
            {
                if ((short)gestaltBlock.Fields[0].Value == s1)
                    if (((ShortBounds)gestaltBlock.Fields[1].Value).Equals(sb1))
                        if (((ShortBounds)gestaltBlock.Fields[2].Value).Equals(sb2))
                        {
                            index = blockField.BlockList.IndexOf(gestaltBlock);
                            break;
                        }
            }

            //Add (or return -1)
            if (index == -1)
            {
                index = blockField.BlockList.Count;
                ITagBlock gestaltBlock = blockField.Add(out bool success);
                if (success)
                {
                    gestaltBlock.Fields[0].Value = s1;
                    gestaltBlock.Fields[1].Value = sb1;
                    gestaltBlock.Fields[2].Value = sb2;
                    index = (byte)index;
                }
                else index = -1;
            }

            //return
            return index;
        }

        private int SoundGestalt_FindImportNameIndex(StringValue stringId)
        {
            //Prepare
            ITagBlock soundCacheFileGestaltBlock = SoundCacheFileGestaltFile.TagGroup[0];
            BaseBlockField blockField = (BaseBlockField)soundCacheFileGestaltBlock.Fields[2];
            int index = -1;

            //Check
            foreach (ITagBlock gestaltBlock in blockField.BlockList)
            {
                if (((StringValue)gestaltBlock.Fields[0].Value).Equals(stringId))
                {
                    index = blockField.BlockList.IndexOf(gestaltBlock);
                    break;
                }
            }

            //Add (or return -1)
            if (index == -1)
            {
                index = blockField.BlockList.Count;
                ITagBlock gestaltBlock = blockField.Add(out bool success);
                if (success) gestaltBlock.Fields[0].Value = stringId;
                else index = -1;
            }

            //return
            return index;
        }

        private int SoundGestalt_FindPromotionIndex(ITagBlock structBlock)
        {
            //Prepare
            ITagBlock soundCacheFileGestaltBlock = SoundCacheFileGestaltFile.TagGroup[0];
            BaseBlockField blockField = (BaseBlockField)soundCacheFileGestaltBlock.Fields[9];
            int index = -1;

            //Check
            foreach (ITagBlock gestaltBlock in blockField.BlockList)
            {
                if (TagBlock_Equals((ITagBlock)gestaltBlock.Fields[0].Value, structBlock))
                {
                    index = blockField.BlockList.IndexOf(gestaltBlock);
                    break;
                }
            }

            //Add (or return -1)
            if (index == -1)
            {
                index = blockField.BlockList.Count;
                ITagBlock gestaltBlock = blockField.Add(out bool success);
                if (success) gestaltBlock.Fields[0].Value = structBlock;
                else index = -1;
            }

            //return
            return index;
        }

        private int SoundGestalt_FindScaleIndex(ITagBlock structBlock)
        {
            //Prepare
            ITagBlock soundCacheFileGestaltBlock = SoundCacheFileGestaltFile.TagGroup[0];
            BaseBlockField blockField = (BaseBlockField)soundCacheFileGestaltBlock.Fields[1];
            int index = -1;

            //Check
            foreach (ITagBlock gestaltBlock in blockField.BlockList)
            {
                if (TagBlock_Equals((ITagBlock)gestaltBlock.Fields[0].Value, structBlock))
                {
                    index = blockField.BlockList.IndexOf(gestaltBlock);
                    break;
                }
            }

            //Add (or return -1)
            if (index == -1)
            {
                index = blockField.BlockList.Count;
                ITagBlock gestaltBlock = blockField.Add(out bool success);
                if (success)
                {
                    gestaltBlock.Fields[0].Value = structBlock;
                    index = (byte)index;
                }
                else index = -1;
            }

            //return
            return index;
        }

        private int SoundGestalt_FindPlaybackIndex(ITagBlock structBlock)
        {
            //Prepare
            ITagBlock soundCacheFileGestaltBlock = SoundCacheFileGestaltFile.TagGroup[0];
            BaseBlockField blockField = (BaseBlockField)soundCacheFileGestaltBlock.Fields[0];
            int index = -1;

            //Check
            foreach (ITagBlock gestaltBlock in blockField.BlockList)
            {
                if (TagBlock_Equals((ITagBlock)gestaltBlock.Fields[0].Value, structBlock))
                {
                    index = blockField.BlockList.IndexOf(gestaltBlock);
                    break;
                }
            }

            //Add (or return -1)
            if (index == -1)
            {
                index = blockField.BlockList.Count;
                ITagBlock gestaltBlock = blockField.Add(out bool success);
                if (success) gestaltBlock.Fields[0].Value = structBlock;
                else index = -1;
            }

            //return
            return index;
        }
        
        private void TagGroupFile_WriteRaws(TagGroupFile tagGroupFile, Stream mapFileStream, BinaryWriter mapFileWriter)
        {
            //Check
            if (tagGroupFile == null && tagGroupFile.TagGroup != null) return;
            ITagGroup tagGroup = tagGroupFile.TagGroup;
            mapFileStream.Align(512, 0);

            //Prepare
            byte[] rawData = null;
            bool modified = false;
            long offsetAddress = 0;
            uint rawAddress = 0;
            using (MemoryStream tagStream = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(tagStream))
            using (BinaryReader reader = new BinaryReader(tagStream))
            {
                //Write tag to tag stream
                tagGroup.Write(writer);

                //Handle
                switch (tagGroup.GroupTag)
                {
                    #region sound cache file gestalt
                    case HaloTags.ugh_:
                        Console.WriteLine("Writing {0} data", tagGroupFile.TagGroup.Name);
                        tagStream.Seek(64, SeekOrigin.Begin);
                        TagBlock chunks = reader.Read<TagBlock>();
                        for (int i = 0; i < chunks.Count; i++)
                        {
                            tagStream.Seek(chunks.Offset + (i * 12), SeekOrigin.Begin);
                            offsetAddress = tagStream.Position;
                            rawAddress = reader.ReadUInt32();

                            if ((rawAddress & C_LocationMask) == 0)
                            {
                                modified = true;
                                rawData = tagGroupFile.GetRaw((int)rawAddress);
                                tagStream.Seek(offsetAddress, SeekOrigin.Begin);
                                writer.Write((uint)mapFileStream.Position);
                                mapFileWriter.Write(rawData);
                                mapFileStream.Align(512, 0);
                            }
                        }

                        tagStream.Seek(80, SeekOrigin.Begin);
                        TagBlock extraInfos = reader.Read<TagBlock>();
                        for (int i = 0; i < extraInfos.Count; i++)
                        {
                            tagStream.Seek(extraInfos.Offset + (i * 44) + 8, SeekOrigin.Begin);
                            offsetAddress = tagStream.Position;
                            rawAddress = reader.ReadUInt32();

                            if ((rawAddress & C_LocationMask) == 0)
                            {
                                modified = true;
                                rawData = tagGroupFile.GetRaw((int)rawAddress);
                                tagStream.Seek(offsetAddress, SeekOrigin.Begin);
                                writer.Write((uint)mapFileStream.Position);
                                mapFileWriter.Write(rawData);
                                mapFileStream.Align(512, 0);
                            }
                        }
                        break;
                    #endregion
                    #region render model
                    case HaloTags.mode:
                        Console.WriteLine("Writing {0} data", tagGroupFile.TagGroup.Name);
                        tagStream.Seek(36, SeekOrigin.Begin);
                        TagBlock sections = reader.Read<TagBlock>();
                        for (int i = 0; i < sections.Count; i++)
                        {
                            tagStream.Seek(sections.Offset + (i * 92) + 56, SeekOrigin.Begin);
                            offsetAddress = tagStream.Position;
                            rawAddress = reader.ReadUInt32();

                            if ((rawAddress & C_LocationMask) == 0)
                            {
                                modified = true;
                                rawData = tagGroupFile.GetRaw((int)rawAddress);
                                tagStream.Seek(offsetAddress, SeekOrigin.Begin);
                                writer.Write((uint)mapFileStream.Position);
                                mapFileWriter.Write(rawData);
                                mapFileStream.Align(512, 0);
                            }
                        }

                        tagStream.Seek(116, SeekOrigin.Begin);
                        TagBlock prtInfo = reader.Read<TagBlock>();
                        for (int i = 0; i < prtInfo.Count; i++)
                        {
                            tagStream.Seek(prtInfo.Offset + (i * 88) + 52, SeekOrigin.Begin);
                            offsetAddress = tagStream.Position;
                            rawAddress = reader.ReadUInt32();

                            if ((rawAddress & C_LocationMask) == 0)
                            {
                                modified = true;
                                rawData = tagGroupFile.GetRaw((int)rawAddress);
                                tagStream.Seek(offsetAddress, SeekOrigin.Begin);
                                writer.Write((uint)mapFileStream.Position);
                                mapFileWriter.Write(rawData);
                                mapFileStream.Align(512, 0);
                            }
                        }
                        break;
                    #endregion
                    #region weather system
                    case HaloTags.weat:
                        Console.WriteLine("Writing {0} data", tagGroupFile.TagGroup.Name);
                        tagStream.Seek(0, SeekOrigin.Begin);
                        TagBlock particleSystem = reader.Read<TagBlock>();
                        for (int i = 0; i < particleSystem.Count; i++)
                        {
                            tagStream.Seek(particleSystem.Offset + (i * 140) + 64, SeekOrigin.Begin);
                            offsetAddress = tagStream.Position;
                            rawAddress = reader.ReadUInt32();

                            if ((rawAddress & C_LocationMask) == 0)
                            {
                                modified = true;
                                rawData = tagGroupFile.GetRaw((int)rawAddress);
                                tagStream.Seek(offsetAddress, SeekOrigin.Begin);
                                writer.Write((uint)mapFileStream.Position);
                                mapFileWriter.Write(rawData);
                                mapFileStream.Align(512, 0);
                            }
                        }
                        break;
                    #endregion
                    #region decorator set
                    case HaloTags.DECR:
                        Console.WriteLine("Writing {0} data", tagGroupFile.TagGroup.Name);
                        tagStream.Seek(56, SeekOrigin.Begin);
                        offsetAddress = tagStream.Position;
                        rawAddress = reader.ReadUInt32();

                        if ((rawAddress & C_LocationMask) == 0)
                        {
                            modified = true;
                            rawData = tagGroupFile.GetRaw((int)rawAddress);
                            tagStream.Seek(offsetAddress, SeekOrigin.Begin);
                            writer.Write((uint)mapFileStream.Position);
                            mapFileWriter.Write(rawData);
                            mapFileStream.Align(512, 0);
                        }
                        break;
                    #endregion
                    #region particle model
                    case HaloTags.PRTM:
                        Console.WriteLine("Writing {0} data", tagGroupFile.TagGroup.Name);
                        tagStream.Seek(160, SeekOrigin.Begin);
                        offsetAddress = tagStream.Position;
                        rawAddress = reader.ReadUInt32();

                        if ((rawAddress & C_LocationMask) == 0)
                        {
                            modified = true;
                            rawData = tagGroupFile.GetRaw((int)rawAddress);
                            tagStream.Seek(offsetAddress, SeekOrigin.Begin);
                            writer.Write((uint)mapFileStream.Position);
                            mapFileWriter.Write(rawData);
                            mapFileStream.Align(512, 0);
                        }
                        break;
                    #endregion
                    #region model animation graph
                    case HaloTags.jmad:
                        Console.WriteLine("Writing {0} data", tagGroupFile.TagGroup.Name);
                        tagStream.Seek(172, SeekOrigin.Begin);
                        TagBlock xboxAnimationData = reader.Read<TagBlock>();
                        for (int i = 0; i < xboxAnimationData.Count; i++)
                        {
                            tagStream.Seek(xboxAnimationData.Offset + (i * 20) + 8, SeekOrigin.Begin);
                            offsetAddress = tagStream.Position;
                            rawAddress = reader.ReadUInt32();

                            if ((rawAddress & C_LocationMask) == 0)
                            {
                                modified = true;
                                rawData = tagGroupFile.GetRaw((int)rawAddress);
                                tagStream.Seek(offsetAddress, SeekOrigin.Begin);
                                writer.Write((uint)mapFileStream.Position);
                                mapFileWriter.Write(rawData);
                                mapFileStream.Align(512, 0);
                            }
                        }
                        break;
                    #endregion
                    #region scenario structure bsp
                    case HaloTags.sbsp:
                        Console.WriteLine("Writing {0} data", tagGroupFile.TagGroup.Name);
                        tagStream.Seek(156, SeekOrigin.Begin);
                        TagBlock clusters = reader.Read<TagBlock>();
                        for (int i = 0; i < clusters.Count; i++)
                        {
                            tagStream.Seek(clusters.Offset + (i * 176) + 40, SeekOrigin.Begin);
                            offsetAddress = tagStream.Position;
                            rawAddress = reader.ReadUInt32();

                            if ((rawAddress & C_LocationMask) == 0)
                            {
                                modified = true;
                                rawData = tagGroupFile.GetRaw((int)rawAddress);
                                tagStream.Seek(offsetAddress, SeekOrigin.Begin);
                                writer.Write((uint)mapFileStream.Position);
                                mapFileWriter.Write(rawData);
                                mapFileStream.Align(512, 0);
                            }
                        }

                        tagStream.Seek(312, SeekOrigin.Begin);
                        TagBlock instancedGeometryDefinitions = reader.Read<TagBlock>();
                        for (int i = 0; i < instancedGeometryDefinitions.Count; i++)
                        {
                            tagStream.Seek(instancedGeometryDefinitions.Offset + (i * 200) + 40, SeekOrigin.Begin);
                            offsetAddress = tagStream.Position;
                            rawAddress = reader.ReadUInt32();

                            if ((rawAddress & C_LocationMask) == 0)
                            {
                                modified = true;
                                rawData = tagGroupFile.GetRaw((int)rawAddress);
                                tagStream.Seek(offsetAddress, SeekOrigin.Begin);
                                writer.Write((uint)mapFileStream.Position);
                                mapFileWriter.Write(rawData);
                                mapFileStream.Align(512, 0);
                            }
                        }

                        tagStream.Seek(532, SeekOrigin.Begin);
                        TagBlock waterDefinitions = reader.Read<TagBlock>();
                        for (int i = 0; i < waterDefinitions.Count; i++)
                        {
                            tagStream.Seek(waterDefinitions.Offset + (i * 172) + 16, SeekOrigin.Begin);
                            offsetAddress = tagStream.Position;
                            rawAddress = reader.ReadUInt32();

                            if ((rawAddress & C_LocationMask) == 0)
                            {
                                modified = true;
                                rawData = tagGroupFile.GetRaw((int)rawAddress);
                                tagStream.Seek(offsetAddress, SeekOrigin.Begin);
                                writer.Write((uint)mapFileStream.Position);
                                mapFileWriter.Write(rawData);
                                mapFileStream.Align(512, 0);
                            }
                        }

                        tagStream.Seek(564, SeekOrigin.Begin);
                        TagBlock decorators = reader.Read<TagBlock>();
                        for (int i = 0; i < decorators.Count; i++)
                        {
                            tagStream.Seek(decorators.Offset + (i * 48) + 16, SeekOrigin.Begin);
                            TagBlock cacheBlocks = reader.Read<TagBlock>();
                            for (int j = 0; j < cacheBlocks.Count; j++)
                            {
                                tagStream.Seek(cacheBlocks.Offset + (j * 44), SeekOrigin.Begin);
                                offsetAddress = tagStream.Position;
                                rawAddress = reader.ReadUInt32();

                                if ((rawAddress & C_LocationMask) == 0)
                                {
                                    modified = true;
                                    rawData = tagGroupFile.GetRaw((int)rawAddress);
                                    tagStream.Seek(offsetAddress, SeekOrigin.Begin);
                                    writer.Write((uint)mapFileStream.Position);
                                    mapFileWriter.Write(rawData);
                                    mapFileStream.Align(512, 0);
                                }
                            }
                        }
                        break;
                    #endregion
                    #region scenario structure lightmap
                    case HaloTags.ltmp:
                        Console.WriteLine("Writing {0} data", tagGroupFile.TagGroup.Name);
                        tagStream.Seek(128, SeekOrigin.Begin);
                        TagBlock lightmapGroups = reader.Read<TagBlock>();
                        for (int i = 0; i < lightmapGroups.Count; i++)
                        {
                            tagStream.Seek(lightmapGroups.Offset + (i * 104) + 32, SeekOrigin.Begin);
                            TagBlock lightmapClusters = reader.Read<TagBlock>();
                            for (int j = 0; j < lightmapClusters.Count; j++)
                            {
                                tagStream.Seek(lightmapClusters.Offset + (j * 84) + 40, SeekOrigin.Begin);
                                offsetAddress = tagStream.Position;
                                rawAddress = reader.ReadUInt32();

                                if ((rawAddress & C_LocationMask) == 0)
                                {
                                    modified = true;
                                    rawData = tagGroupFile.GetRaw((int)rawAddress);
                                    tagStream.Seek(offsetAddress, SeekOrigin.Begin);
                                    writer.Write((uint)mapFileStream.Position);
                                    mapFileWriter.Write(rawData);
                                    mapFileStream.Align(512, 0);
                                }
                            }

                            tagStream.Seek(lightmapGroups.Offset + (i * 104) + 48, SeekOrigin.Begin);
                            TagBlock poopDefinitions = reader.Read<TagBlock>();
                            for (int j = 0; j < poopDefinitions.Count; j++)
                            {
                                tagStream.Seek(poopDefinitions.Offset + (j * 84) + 40, SeekOrigin.Begin);
                                offsetAddress = tagStream.Position;
                                rawAddress = reader.ReadUInt32();

                                if ((rawAddress & C_LocationMask) == 0)
                                {
                                    modified = true;
                                    rawData = tagGroupFile.GetRaw((int)rawAddress);
                                    tagStream.Seek(offsetAddress, SeekOrigin.Begin);
                                    writer.Write((uint)mapFileStream.Position);
                                    mapFileWriter.Write(rawData);
                                    mapFileStream.Align(512, 0);
                                }
                            }

                            tagStream.Seek(lightmapGroups.Offset + (i * 104) + 64, SeekOrigin.Begin);
                            TagBlock geometryBuckets = reader.Read<TagBlock>();
                            for (int j = 0; j < geometryBuckets.Count; j++)
                            {
                                tagStream.Seek(geometryBuckets.Offset + (j * 56) + 12, SeekOrigin.Begin);
                                offsetAddress = tagStream.Position;
                                rawAddress = reader.ReadUInt32();

                                if ((rawAddress & C_LocationMask) == 0)
                                {
                                    modified = true;
                                    rawData = tagGroupFile.GetRaw((int)rawAddress);
                                    tagStream.Seek(offsetAddress, SeekOrigin.Begin);
                                    writer.Write((uint)mapFileStream.Position);
                                    mapFileWriter.Write(rawData);
                                    mapFileStream.Align(512, 0);
                                }
                            }
                        }
                        break;
                    #endregion
                    #region bitmap
                    case HaloTags.bitm:
                        Console.WriteLine("Writing {0} data", tagGroupFile.TagGroup.Name);
                        tagStream.Seek(68, SeekOrigin.Begin);
                        TagBlock bitmapData = reader.Read<TagBlock>();
                        for (int i = 0; i < bitmapData.Count; i++)
                        {
                            tagStream.Seek(bitmapData.Offset + (i * 116) + 28, SeekOrigin.Begin);
                            offsetAddress = tagStream.Position;
                            rawAddress = reader.ReadUInt32();

                            if ((rawAddress & C_LocationMask) == 0)
                            {
                                modified = true;
                                rawData = tagGroupFile.GetRaw((int)rawAddress);
                                tagStream.Seek(offsetAddress, SeekOrigin.Begin);
                                writer.Write((uint)mapFileStream.Position);
                                mapFileWriter.Write(rawData);
                                mapFileStream.Align(512, 0);
                            }

                            tagStream.Seek(bitmapData.Offset + (i * 116) + 32, SeekOrigin.Begin);
                            offsetAddress = tagStream.Position;
                            rawAddress = reader.ReadUInt32();

                            if ((rawAddress & C_LocationMask) == 0)
                            {
                                modified = true;
                                rawData = tagGroupFile.GetRaw((int)rawAddress);
                                tagStream.Seek(offsetAddress, SeekOrigin.Begin);
                                writer.Write((uint)mapFileStream.Position);
                                mapFileWriter.Write(rawData);
                                mapFileStream.Align(512, 0);
                            }

                            tagStream.Seek(bitmapData.Offset + (i * 116) + 36, SeekOrigin.Begin);
                            offsetAddress = tagStream.Position;
                            rawAddress = reader.ReadUInt32();

                            if ((rawAddress & C_LocationMask) == 0)
                            {
                                modified = true;
                                rawData = tagGroupFile.GetRaw((int)rawAddress);
                                tagStream.Seek(offsetAddress, SeekOrigin.Begin);
                                writer.Write((uint)mapFileStream.Position);
                                mapFileWriter.Write(rawData);
                                mapFileStream.Align(512, 0);
                            }

                            tagStream.Seek(bitmapData.Offset + (i * 116) + 40, SeekOrigin.Begin);
                            offsetAddress = tagStream.Position;
                            rawAddress = reader.ReadUInt32();

                            if ((rawAddress & C_LocationMask) == 0)
                            {
                                modified = true;
                                rawData = tagGroupFile.GetRaw((int)rawAddress);
                                tagStream.Seek(offsetAddress, SeekOrigin.Begin);
                                writer.Write((uint)mapFileStream.Position);
                                mapFileWriter.Write(rawData);
                                mapFileStream.Align(512, 0);
                            }

                            tagStream.Seek(bitmapData.Offset + (i * 116) + 44, SeekOrigin.Begin);
                            offsetAddress = tagStream.Position;
                            rawAddress = reader.ReadUInt32();

                            if ((rawAddress & C_LocationMask) == 0)
                            {
                                modified = true;
                                rawData = tagGroupFile.GetRaw((int)rawAddress);
                                tagStream.Seek(offsetAddress, SeekOrigin.Begin);
                                writer.Write((uint)mapFileStream.Position);
                                mapFileWriter.Write(rawData);
                                mapFileStream.Align(512, 0);
                            }

                            tagStream.Seek(bitmapData.Offset + (i * 116) + 48, SeekOrigin.Begin);
                            offsetAddress = tagStream.Position;
                            rawAddress = reader.ReadUInt32();

                            if ((rawAddress & C_LocationMask) == 0)
                            {
                                modified = true;
                                rawData = tagGroupFile.GetRaw((int)rawAddress);
                                tagStream.Seek(offsetAddress, SeekOrigin.Begin);
                                writer.Write((uint)mapFileStream.Position);
                                mapFileWriter.Write(rawData);
                                mapFileStream.Align(512, 0);
                            }
                        }
                        break;
                        #endregion
                }

                //Check
                if (modified)  //if a correction has occured, re-read the tag group
                {
                    tagStream.Seek(0, SeekOrigin.Begin);
                    tagGroup.Read(reader);
                }
            }
        }

        private void SoundTagGroupFile_WriteExtraInfoRaws(TagGroupFile tagGroupFile, Stream mapFileStream, BinaryWriter mapFileWriter)
        {
            //Check
            if (tagGroupFile == null && tagGroupFile.TagGroup != null) return;
            ITagGroup tagGroup = tagGroupFile.TagGroup;

            //Prepare
            byte[] rawData = null;
            bool modified = false;
            long offsetAddress = 0;
            uint rawAddress = 0;
            using (MemoryStream tagStream = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(tagStream))
            using (BinaryReader reader = new BinaryReader(tagStream))
            {
                //Write tag to tag stream
                tagGroup.Write(writer);

                //Handle
                switch (tagGroup.GroupTag)
                {
                    case HaloTags.ugh_:
                        tagStream.Seek(80, SeekOrigin.Begin);
                        TagBlock extraInfo = reader.Read<TagBlock>();
                        for (int i = 0; i < extraInfo.Count; i++)
                        {
                            tagStream.Seek(extraInfo.Offset + (i * 44) + 8, SeekOrigin.Begin);
                            offsetAddress = tagStream.Position;
                            rawAddress = reader.ReadUInt32();

                            if ((rawAddress & C_LocationMask) == 0)
                            {
                                modified = true;
                                rawData = tagGroupFile.GetRaw((int)rawAddress);
                                tagStream.Seek(offsetAddress, SeekOrigin.Begin);
                                writer.Write((uint)mapFileStream.Position);
                                mapFileWriter.Write(rawData);
                                mapFileStream.Align(512);
                            }
                        }
                        break;
                }

                //Check
                if (modified)  //if a correction has occured, re-read the tag group
                {
                    tagStream.Seek(0, SeekOrigin.Begin);
                    tagGroup.Read(reader);
                }
            }
        }
        
        private bool TagBlock_Equals(ITagBlock b1, ITagBlock b2)
        {
            //Start
            bool equals = b1.Fields.Count == b2.Fields.Count && b1.Name == b2.Name;

            //Check
            if (equals)
                for (int i = 0; i < b1.Fields.Count; i++)
                {
                    if (!equals) break;
                    Field f1 = b1.Fields[i], f2 = b2.Fields[i];
                    equals &= f1.Type == f2.Type;
                    if (equals)
                        switch (b1.Fields[i].Type)
                        {
                            case FieldType.FieldBlock:
                                BaseBlockField bf1 = (BaseBlockField)f1;
                                BaseBlockField bf2 = (BaseBlockField)f2;
                                equals &= bf1.BlockList.Count == bf2.BlockList.Count;
                                if (equals)
                                    for (int j = 0; j < bf1.BlockList.Count; j++)
                                        if (equals) equals = TagBlock_Equals(bf1.BlockList[j], bf2.BlockList[j]);
                                break;
                            case FieldType.FieldStruct:
                                equals &= TagBlock_Equals((ITagBlock)f1.Value, (ITagBlock)f2.Value);
                                break;
                            case FieldType.FieldPad:
                                PadField pf1 = (PadField)f1;
                                PadField pf2 = (PadField)f2;
                                for (int j = 0; j < pf1.Length; j++)
                                    if (equals) equals &= ((byte[])pf1.Value)[j] == ((byte[])pf2.Value)[j];
                                break;
                            default:
                                if (f1.Value == null && f2.Value == null) continue;
                                else equals &= f1.Value.Equals(f2.Value);
                                break;
                        }
                }

            //Return
            return equals;
        }
        
        private sealed class MultilingualUnicodeStringsContainer
        {
            public int EnSize { get; set; } = 0;
            public int JpSize { get; set; } = 0;
            public int NlSize { get; set; } = 0;
            public int FrSize { get; set; } = 0;
            public int EsSize { get; set; } = 0;
            public int ItSize { get; set; } = 0;
            public int KrSize { get; set; } = 0;
            public int ZhSize { get; set; } = 0;
            public int PrSize { get; set; } = 0;
            public List<StringEntry> En { get; set; } = new List<StringEntry>();
            public List<StringEntry> Jp { get; set; } = new List<StringEntry>();
            public List<StringEntry> Nl { get; set; } = new List<StringEntry>();
            public List<StringEntry> Fr { get; set; } = new List<StringEntry>();
            public List<StringEntry> Es { get; set; } = new List<StringEntry>();
            public List<StringEntry> It { get; set; } = new List<StringEntry>();
            public List<StringEntry> Kr { get; set; } = new List<StringEntry>();
            public List<StringEntry> Zh { get; set; } = new List<StringEntry>();
            public List<StringEntry> Pr { get; set; } = new List<StringEntry>();

            public void Clear()
            {
                //Reset
                EnSize = 0;
                JpSize = 0;
                NlSize = 0;
                FrSize = 0;
                EsSize = 0;
                ItSize = 0;
                KrSize = 0;
                ZhSize = 0;
                PrSize = 0;
                En.Clear();
                Jp.Clear();
                Nl.Clear();
                Fr.Clear();
                Es.Clear();
                It.Clear();
                Kr.Clear();
                Zh.Clear();
                Pr.Clear();
            }
        }
    }
}
