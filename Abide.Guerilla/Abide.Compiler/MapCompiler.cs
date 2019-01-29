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
using System.Threading;

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
        /// Gets and returns the compiler's scenario tag group file.
        /// </summary>
        public TagGroupFile ScenarioFile { get; } = new TagGroupFile();
        /// <summary>
        /// Gets and returns the output Halo 2 cache map file.
        /// </summary>
        public string OutputMapFile { get; private set; }
        /// <summary>
        /// Gets and returns the compiler host.
        /// </summary>
        public ICompileHost Host { get; }

        private volatile bool m_IsCompiling;
        private readonly string m_WorkspaceDirectory;
        private readonly string m_ScenarioFileName;
        private MultilingualUnicodeStringsContainer m_MultilingualUnicodeStringsContainer;
        private Dictionary<ResourceTag, TagGroupFile> m_CacheResourceReferences;
        private Dictionary<string, TagGroupFile> m_TagGroupReferences;
        private Dictionary<string, TagId> m_TagIdReferences;
        private List<string> m_StringsList;
        private string m_ScenarioPath;
        private string m_MapName;
        private TagId m_CurrentId;
        private int m_ResourceMapType;
        private MapFile m_ResourceMap;
        private ITagGroup m_ResourceGlobals;
        private ITagGroup m_ResourceSoundCacheFileGestalt;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapCompiler"/> class using the specified host, and scenario file.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="scenarioFile">The map scenario file name.</param>
        public MapCompiler(ICompileHost host, string scenarioFileName, string workspaceDirectory)
        {
            Host = host;
            m_ScenarioFileName = scenarioFileName;
            m_WorkspaceDirectory = workspaceDirectory;
        }

        public void Start()
        {
            //Check
            if (m_IsCompiling) return;

            //Start
            m_IsCompiling = true;
            Map_Compile(null);
            //ThreadPool.QueueUserWorkItem(Map_Compile, null);
        }

        private void Map_Compile(object state)
        {
            //Log
            Host.Log("Beginning compilation...");

            //Prepare
            m_ResourceMap = new MapFile();
            
            //Read scenario
            ScenarioFile.Load(m_ScenarioFileName);
            m_ScenarioPath = m_ScenarioFileName.Replace(Path.Combine(m_WorkspaceDirectory, "tags"), string.Empty).Substring(1).Replace(".scenario", string.Empty);
            m_MapName = Path.GetFileName(m_ScenarioPath);

            //Get map type
            m_ResourceMapType = (short)ScenarioFile.TagGroup[0].Fields[2].Value;

            //Save
            OutputMapFile = Path.Combine(m_WorkspaceDirectory, "maps", $"{m_MapName}.map");
            if (!Directory.Exists(Path.Combine(m_WorkspaceDirectory, "maps")))
                Directory.CreateDirectory(Path.Combine(m_WorkspaceDirectory, "maps"));

            //Prepare
            m_MultilingualUnicodeStringsContainer = new MultilingualUnicodeStringsContainer();
            m_CacheResourceReferences = new Dictionary<ResourceTag, TagGroupFile>();
            m_TagIdReferences = new Dictionary<string, TagId>();
            m_TagGroupReferences = new Dictionary<string, TagGroupFile>();
            m_StringsList = new List<string>(DefaultStrings.GetDefaultStrings());
            m_CurrentId = 0xe1740000;

            //Setup
            SoundCacheFileGestaltFile.TagGroup = new Tag.Cache.Generated.SoundCacheFileGestalt();

            //Add flags
            for (int i = 0; i < 686; i++)
            {
                ITagBlock runtimePermutationBitVector = ((BaseBlockField)SoundCacheFileGestaltFile.TagGroup[0].Fields[7]).Add(out bool success);
                if (success) runtimePermutationBitVector.Fields[0].Value = (byte)0;
            }

            //Collect resource-referenced tags
            CacheResource_CollectReferences();

            //Add scenario
            ScenarioFile.Id = m_CurrentId;
            m_TagIdReferences.Add($@"{m_ScenarioPath}.{ScenarioFile.TagGroup.GroupTag}", m_CurrentId);
            m_TagGroupReferences.Add($@"{m_ScenarioPath}.{ScenarioFile.TagGroup.Name}", ScenarioFile);
            m_CurrentId++;

            //Log
            Host.Log("Building tag references...");

            //Collect referenced tags
            TagGroup_CollectReferences(ScenarioFile);

            //Log
            Host.Log("Discovering shared resources...");

            //Prepare resources
            foreach (KeyValuePair<ResourceTag, TagGroupFile> resource in m_CacheResourceReferences)
                CacheResource_Prepare(resource.Key, resource.Value);

            //Log
            Host.Log("Merging shared resources...");

            //Merge resources
            foreach (KeyValuePair<ResourceTag, TagGroupFile> resource in m_CacheResourceReferences)
                if (resource.Key.Merge) CacheResource_Merge(resource.Key, resource.Value);

            //Build globals tab
            Map_BuildGlobalsTag();

            //Build sound gestalt
            SoundCacheFileGestaltFile.Id = m_CurrentId;
            m_TagIdReferences.Add($@"i've got a lovely bunch of coconuts.{SoundCacheFileGestaltFile.TagGroup.GroupTag}", SoundCacheFileGestaltFile.Id);
            m_TagGroupReferences.Add($@"i've got a lovely bunch of coconuts.{SoundCacheFileGestaltFile.TagGroup.Name}", SoundCacheFileGestaltFile);
            m_CurrentId++;

            //Setup sound info
            ITagBlock soundInfo = ((BaseBlockField)GlobalsFile.TagGroup[0].Fields[4]).BlockList[0];
            soundInfo.Fields[4].Value = (TagId)SoundCacheFileGestaltFile.Id;

            //Get multilingual unicode string list
            KeyValuePair<string, TagGroupFile>[] multilingualUnicodeStringLists = m_TagGroupReferences.Where(t => t.Value.TagGroup.GroupTag == HaloTags.unic).ToArray();
            KeyValuePair<string, TagGroupFile>[] sounds = m_TagGroupReferences.Where(t => t.Value.TagGroup.GroupTag == HaloTags.snd_).ToArray();

            //Postprocess multilingual unicode string lists
            Host.Log("Postprocessing multilingual unicode string lists...");
            foreach (var multilingualUnicodeStringListReference in multilingualUnicodeStringLists)
                MultilingualUnicodeStringList_Postprocess(multilingualUnicodeStringListReference.Value);

            //Postprocess sounds
            Host.Log("Postprocessing sounds...");
            foreach (var sound in sounds)
                SoundGestalt_AddSound(sound.Value);

            //Log
            Host.Log($"Gathered {m_TagIdReferences.Count} tags...");

            //Build file
            Map_BuildFile();

            //Dispose
            m_ResourceMap.Dispose();
            m_ResourceGlobals.Dispose();
            m_ResourceSoundCacheFileGestalt.Dispose();

            //Empty
            m_IsCompiling = false;
            m_MultilingualUnicodeStringsContainer.Clear();
            m_CacheResourceReferences.Clear();
            m_TagGroupReferences.Clear();
            m_TagIdReferences.Clear();
            m_StringsList.Clear();
            m_ScenarioPath = string.Empty;
            m_MapName = string.Empty;
            m_CurrentId = TagId.Null;
            m_ResourceMapType = -1;
            m_ResourceMap = null;

            //Collect
            GC.Collect();
            
            //Done
            Host.Log("Complete!");
            Host.Log($"Output map file: {OutputMapFile}");

            //Complete
            Host.Complete();
        }

        private void Map_BuildGlobalsTag()
        {
            //Set
            GlobalsFile.TagGroup = m_ResourceGlobals;
            foreach (ITagBlock tagBlock in GlobalsFile.TagGroup)
                CacheResource_MergeTagBlock(tagBlock);
        }

        private void Map_BuildFile()
        {
            //Get Tag References
            TagGroupFile[] references = m_TagGroupReferences.Select(kvp => kvp.Value).ToArray();
            string[] tagNames = m_TagGroupReferences.Select(kvp => kvp.Key.Substring(0, kvp.Key.LastIndexOf('.'))).ToArray();

            //Prepare
            StructureBspBlockHeader bspHeader = new StructureBspBlockHeader();
            Header header = Header.CreateDefault();
            Index index = new Index();
            TagHierarchy[] tags = new TagHierarchy[m_ResourceMap.Tags.Count];
            ObjectEntry[] objects = new ObjectEntry[references.Length];
            long indexLength = 0, bspAddress = 0, bspLength = 0, tagDataAddress = 0, tagDataLength = 0;

            //Copy tags from resource map
            int tagIndex = 0;
            foreach (TagHierarchy tag in m_ResourceMap.Tags)
            { tags[tagIndex] = tag; tagIndex++; }

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

                //Log
                Host.Log("Writing raws...");

                //Write raws
                foreach (TagGroupFile tagGroupFile in references.Where(r => r.TagGroup.GroupTag == HaloTags.ugh_))  //sound
                    TagGroupFile_WriteRaws(tagGroupFile, fs, mapWriter);
                foreach (TagGroupFile tagGroupFile in references.Where(r => r.TagGroup.GroupTag == HaloTags.mode))  //render model geometry
                    TagGroupFile_WriteRaws(tagGroupFile, fs, mapWriter);
                foreach (TagGroupFile tagGroupFile in references.Where(r => r.TagGroup.GroupTag == HaloTags.sbsp))  //structure bsp geometry
                    TagGroupFile_WriteRaws(tagGroupFile, fs, mapWriter);
                foreach (TagGroupFile tagGroupFile in references.Where(r => r.TagGroup.GroupTag == HaloTags.ltmp))  //structure lightmap geometry
                    TagGroupFile_WriteRaws(tagGroupFile, fs, mapWriter);
                foreach (TagGroupFile tagGroupFile in references.Where(r => r.TagGroup.GroupTag == HaloTags.weat))  //weather system geometry
                    TagGroupFile_WriteRaws(tagGroupFile, fs, mapWriter);
                foreach (TagGroupFile tagGroupFile in references.Where(r => r.TagGroup.GroupTag == HaloTags.DECR))  //decorator set geometry
                    TagGroupFile_WriteRaws(tagGroupFile, fs, mapWriter);
                foreach (TagGroupFile tagGroupFile in references.Where(r => r.TagGroup.GroupTag == HaloTags.ugh_))  //sound gestalt extra info
                    SoundTagGroupFile_WriteExtraInfoRaws(tagGroupFile, fs, mapWriter);
                foreach (TagGroupFile tagGroupFile in references.Where(r => r.TagGroup.GroupTag == HaloTags.PRTM))  //particle model geometry
                    TagGroupFile_WriteRaws(tagGroupFile, fs, mapWriter);
                foreach (TagGroupFile tagGroupFile in references.Where(r => r.TagGroup.GroupTag == HaloTags.jmad))  //model animation
                    TagGroupFile_WriteRaws(tagGroupFile, fs, mapWriter);

                //Build
                Host.Log("Building map tag data table...");

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
                Host.Log("Writing BSP tag data...");

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
                        if (m_TagGroupReferences.Any(kvp => kvp.Value.Id == structureBspTagReference.Id.Dword))
                        {
                            //Write
                            bspHeader.StructureBspOffset = (uint)bspDataStream.Position;
                            structureBspFile = m_TagGroupReferences.First(kvp => kvp.Value.Id == structureBspTagReference.Id).Value;
                            structureBspFile.TagGroup.Write(writer);

                            //Align
                            bspDataStream.Align(4096);
                        }
                        bspHeader.StructureLightmapOffset = 0;
                        if (m_TagGroupReferences.Any(kvp => kvp.Value.Id == structureLightmapReference.Id.Dword))
                        {
                            //Write
                            bspHeader.StructureLightmapOffset = (uint)bspDataStream.Position;
                            structureLightmapFile = m_TagGroupReferences.First(kvp => kvp.Value.Id == structureLightmapReference.Id).Value;
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
                Host.Log("Writing strings...");

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
                Host.Log("Writing file names...");

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
                Host.Log("Writing multilingual unicode strings...");

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

                //Log
                Host.Log("Write bitmap raw...");

                foreach (TagGroupFile tagGroupFile in references.Where(r => r.TagGroup.GroupTag == HaloTags.bitm))  //bitmap
                    TagGroupFile_WriteRaws(tagGroupFile, fs, mapWriter);

                //Log
                Host.Log("Writing index...");

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
                    Host.Log("Writing tag data...");

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
                Host.Log("Finalizing header...");
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

        private void CacheResource_CollectReferences()
        {
            //Prepare
            TagGroupFile soundClasses, combatDialogConstants;
            IndexEntry globalsEntry, soundClassesEntry, combatDialogConstantsEntry, soundCacheFileGestalt, multiplayerGlobalsEntry, resourceEntry;
            ITagGroup multiplayerGlobals = null;

            //Create globals tag
            GlobalsFile.Id = m_CurrentId;
            GlobalsFile.TagGroup = new Tag.Cache.Generated.Globals();
            m_TagIdReferences.Add($@"globals\globals.{GlobalsFile.TagGroup.GroupTag}", GlobalsFile.Id);
            m_TagGroupReferences.Add($@"globals\globals.{GlobalsFile.TagGroup.Name}", GlobalsFile);
            m_CurrentId++;

            //Get sound classes and combad dialog constants from appropriate resource map (I don't know if this actually matters but oh well)
            if (m_ResourceMapType == 1)   //Multiplayer map, use shared
            {
                //Log
                Host.Log("Reading source files from shared...");

                //Load
                m_ResourceMap.Load(RegistrySettings.SharedFileName);

                //Get entries
                globalsEntry = m_ResourceMap.IndexEntries[0];
                soundClassesEntry = m_ResourceMap.IndexEntries[1];
                combatDialogConstantsEntry = m_ResourceMap.IndexEntries[2];
                soundCacheFileGestalt = m_ResourceMap.IndexEntries.Last;

                //Read tag data
                using (BinaryReader reader = m_ResourceMap.TagDataStream.CreateReader())
                {
                    //Load sound gestalt
                    m_ResourceSoundCacheFileGestalt = new Tag.Cache.Generated.SoundCacheFileGestalt();
                    reader.BaseStream.Seek((uint)soundCacheFileGestalt.PostProcessedOffset, SeekOrigin.Begin);
                    m_ResourceSoundCacheFileGestalt.Read(reader);

                    //Get globals
                    m_ResourceGlobals = new Tag.Cache.Generated.Globals();
                    reader.BaseStream.Seek(globalsEntry.Offset, SeekOrigin.Begin);
                    m_ResourceGlobals.Read(reader);

                    //Load resources
                    foreach (string fileName in SharedResources.MultiplayerSharedResourceFileNames)
                    {
                        resourceEntry = m_ResourceMap.IndexEntries.First(e => $"{e.Filename}.{e.Root}" == fileName);
                        m_CacheResourceReferences.Add(new ResourceTag(resourceEntry.Filename,
                            resourceEntry.Root, resourceEntry.Id), new TagGroupFile());
                    }
                    
                    //Get sound classes
                    soundClasses = new TagGroupFile() { Id = m_CurrentId, TagGroup = new Tag.Cache.Generated.SoundClasses() };
                    reader.BaseStream.Seek(soundClassesEntry.Offset, SeekOrigin.Begin);
                    soundClasses.TagGroup.Read(reader);
                    m_TagIdReferences.Add($@"{soundClassesEntry.Filename}.{soundClassesEntry.Root}", soundClasses.Id);
                    m_TagGroupReferences.Add($@"{soundClassesEntry.Filename}.{soundClasses.TagGroup.Name}", soundClasses);
                    m_CurrentId++;

                    //Get sound dialog constants
                    combatDialogConstants = new TagGroupFile() { Id = m_CurrentId, TagGroup = new Tag.Cache.Generated.SoundDialogueConstants() };
                    reader.BaseStream.Seek(combatDialogConstantsEntry.Offset, SeekOrigin.Begin);
                    combatDialogConstants.TagGroup.Read(reader);
                    m_TagIdReferences.Add($@"{combatDialogConstantsEntry.Filename}.{combatDialogConstantsEntry.Root}", combatDialogConstants.Id);
                    m_TagGroupReferences.Add($@"{combatDialogConstantsEntry.Filename}.{combatDialogConstants.TagGroup.Name}", combatDialogConstants);
                    m_CurrentId++;

                    //Get multiplayer globals references
                    multiplayerGlobals = new Tag.Cache.Generated.MultiplayerGlobals();
                    multiplayerGlobalsEntry = m_ResourceMap.IndexEntries[new TagId(0xEF0E0D99)];
                    reader.BaseStream.Seek(multiplayerGlobalsEntry.Offset, SeekOrigin.Begin);
                    multiplayerGlobals.Read(reader);

                    //Build reference tree
                    CacheResourceTagGroup_BuildReferences(multiplayerGlobals, m_CacheResourceReferences, reader);
                }
            }
            else if (m_ResourceMapType == 0)  //Singleplayer map, use single_player_shared
            {
                //Log
                Host.Log("Reading source files from single player shared...");

                //Load
                m_ResourceMap.Load(RegistrySettings.SinglePlayerSharedFileName);

                //Get entries
                globalsEntry = m_ResourceMap.IndexEntries[0];
                soundClassesEntry = m_ResourceMap.IndexEntries[1];
                combatDialogConstantsEntry = m_ResourceMap.IndexEntries[2];
                soundCacheFileGestalt = m_ResourceMap.IndexEntries.Last;

                //Read tag data
                using (BinaryReader reader = m_ResourceMap.TagDataStream.CreateReader())
                {
                    //Load sound gestalt
                    m_ResourceSoundCacheFileGestalt = new Tag.Cache.Generated.SoundCacheFileGestalt();
                    reader.BaseStream.Seek((uint)soundCacheFileGestalt.PostProcessedOffset, SeekOrigin.Begin);
                    m_ResourceSoundCacheFileGestalt.Read(reader);

                    //Get globals
                    m_ResourceGlobals = new Tag.Cache.Generated.Globals();
                    reader.BaseStream.Seek(globalsEntry.Offset, SeekOrigin.Begin);
                    m_ResourceGlobals.Read(reader);

                    //Load resources
                    foreach (string fileName in SharedResources.SingleplayerSharedResourceFileNames)
                    {
                        resourceEntry = m_ResourceMap.IndexEntries.First(e => $"{e.Filename}.{e.Root}" == fileName);
                        m_CacheResourceReferences.Add(new ResourceTag(resourceEntry.Filename,
                            resourceEntry.Root, resourceEntry.Id), new TagGroupFile());
                    }

                    //Get sound classes
                    soundClasses = new TagGroupFile() { Id = m_CurrentId, TagGroup = new Tag.Cache.Generated.SoundClasses() };
                    reader.BaseStream.Seek(soundClassesEntry.Offset, SeekOrigin.Begin);
                    soundClasses.TagGroup.Read(reader);
                    m_TagIdReferences.Add($@"{soundClassesEntry.Filename}.{soundClassesEntry.Root}", soundClasses.Id);
                    m_TagGroupReferences.Add($@"{soundClassesEntry.Filename}.{soundClasses.TagGroup.Name}", soundClasses);
                    m_CurrentId++;

                    //Get sound dialog constants
                    combatDialogConstants = new TagGroupFile() { Id = m_CurrentId, TagGroup = new Tag.Cache.Generated.SoundDialogueConstants() };
                    reader.BaseStream.Seek(combatDialogConstantsEntry.Offset, SeekOrigin.Begin);
                    combatDialogConstants.TagGroup.Read(reader);
                    m_TagIdReferences.Add($@"{combatDialogConstantsEntry.Filename}.{combatDialogConstantsEntry.Root}", combatDialogConstants.Id);
                    m_TagGroupReferences.Add($@"{combatDialogConstantsEntry.Filename}.{combatDialogConstants.TagGroup.Name}", combatDialogConstants);
                    m_CurrentId++;
                }
            }
            else throw new NotImplementedException("Cannot compile a map of this type.");

            //Load sound cache file gestalt raw
            CacheResource_ModifyRawAddress(m_ResourceSoundCacheFileGestalt);
        }
        
        private ResourceTag CacheResource_Discover(string fileName, string root)
        {
            if (m_CacheResourceReferences.Any(r => r.Key.FileName == fileName && r.Key.Root == root))
                return m_CacheResourceReferences.First(r => r.Key.FileName == fileName && r.Key.Root == root).Key;
            return null;
        }

        private ResourceTag CacheResource_Discover(TagId id)
        {
            if (m_CacheResourceReferences.Any(r => r.Key.Id == id))
                return m_CacheResourceReferences.First(r => r.Key.Id == id).Key;
            return null;
        }
        
        private void CacheResource_Prepare(ResourceTag resource, TagGroupFile tagFile)
        {
            //Prepare
            IndexEntry resourceEntry = null;
            string tagPath = $"{resource.FileName}.{resource.Root}";
            string tagName = string.Empty;
            
            //Check
            if (m_TagIdReferences.ContainsKey(tagPath)) resource.NewId = m_TagIdReferences[tagPath];
            else
            {
                //Read tag
                resourceEntry = m_ResourceMap.IndexEntries[resource.Id];
                using (BinaryReader reader = resourceEntry.TagData.CreateReader())
                {
                    reader.BaseStream.Seek((uint)resourceEntry.PostProcessedOffset, SeekOrigin.Begin);
                    tagFile.TagGroup = Tag.Cache.Generated.TagLookup.CreateTagGroup(resource.Root);
                    tagFile.TagGroup.Read(reader);
                }

                //Get name
                tagName = $"{resource.FileName}.{tagFile.TagGroup.Name}";

                //Postprocess
                if (resourceEntry.Root == HaloTags.snd_)
                    CacheResource_PostprocessSound(tagFile, resourceEntry);
                else if (resourceEntry.Root == HaloTags.unic)
                    CacheResource_PostprocessMultilingualUnicodeStringList(tagFile, resourceEntry.Strings);

                //Create tag
                tagFile.Id = m_CurrentId;
                resource.NewId = m_CurrentId;
                m_TagIdReferences.Add(tagPath, tagFile.Id);
                m_TagGroupReferences.Add(tagName, tagFile);
                m_CurrentId++;

                //Set merge flag
                resource.Merge = true;
            }
        }
        
        private void CacheResource_Merge(ResourceTag key, TagGroupFile value)
        {
            //Convert local raw references to external
            CacheResource_ModifyRawAddress(value.TagGroup);

            //Loop through tag blocks in tag group
            foreach (ITagBlock tagBlock in value.TagGroup)
                CacheResource_MergeTagBlock(tagBlock);
        }

        private void CacheResource_MergeTagBlock(ITagBlock tagBlock)
        {
            //Prepare
            ResourceTag resourceTag = null;
            TagGroupFile tagFile = null;

            //Loop through fields
            foreach (Field field in tagBlock.Fields)
                switch (field.Type)
                {
                    case FieldType.FieldBlock:
                        foreach (ITagBlock block in ((BaseBlockField)field).BlockList)
                            CacheResource_MergeTagBlock(block);
                        break;

                    case FieldType.FieldStruct:
                        CacheResource_MergeTagBlock((ITagBlock)field.Value);
                        break;

                    case FieldType.FieldOldStringId:
                    case FieldType.FieldStringId:
                        if (((StringId)field.Value).Index < m_ResourceMap.Strings.Count)
                        {
                            string stringValue = m_ResourceMap.Strings[((StringId)field.Value).Index];
                            if (!m_StringsList.Contains(stringValue)) m_StringsList.Add(stringValue);
                            field.Value = new StringId((ushort)m_StringsList.IndexOf(stringValue), (byte)stringValue.Length);
                        }
                        else field.Value = StringId.Zero;
                        break;

                    case FieldType.FieldTagIndex:
                        TagId idReference = (TagId)field.Value;
                        if (!idReference.IsNull) resourceTag = CacheResource_Discover(idReference);
                        if (resourceTag != null)
                        {
                            tagFile = m_CacheResourceReferences[resourceTag];
                            field.Value = resourceTag.NewId;
                        }
                        break;

                    case FieldType.FieldTagReference:
                        TagReference tagReference = (TagReference)field.Value;
                        if (!tagReference.Id.IsNull) resourceTag = CacheResource_Discover(tagReference.Id);
                        if (resourceTag != null)
                        {
                            tagFile = m_CacheResourceReferences[resourceTag];
                            tagReference.Id = resourceTag.NewId;
                            field.Value = tagReference;
                        }
                        break;
                }
        }
        
        private void CacheResourceTagGroup_BuildReferences(ITagGroup tagGroup, Dictionary<ResourceTag, TagGroupFile> references, BinaryReader reader)
        {
            //Loop through each tag block
            foreach (ITagBlock tagBlock in tagGroup)
                CacheResourceTagBlock_BuildReferences(tagBlock, references, reader);
        }

        private void CacheResourceTagBlock_BuildReferences(ITagBlock tagBlock, Dictionary<ResourceTag, TagGroupFile> references, BinaryReader reader)
        {
            //Prepare
            IndexEntry referenceEntry = null;
            ITagGroup referenceTagGroup = null;

            //Loop
            foreach (Field field in tagBlock.Fields)
                switch (field.Type)
                {
                    case FieldType.FieldBlock:
                        foreach (ITagBlock block in ((BaseBlockField)field).BlockList)
                            CacheResourceTagBlock_BuildReferences(block, references, reader);
                        break;

                    case FieldType.FieldStruct:
                        CacheResourceTagBlock_BuildReferences((ITagBlock)field.Value, references, reader);
                        break;

                    case FieldType.FieldOldStringId:
                    case FieldType.FieldStringId:
                        StringId stringId = (StringId)field.Value;
                        if (m_ResourceMap.Strings.Count > stringId.Index)
                        {
                            string stringValue = m_ResourceMap.Strings[stringId.Index];
                            if (!m_StringsList.Contains(stringValue))
                                m_StringsList.Add(stringValue);
                        }
                        break;

                    case FieldType.FieldTagIndex:
                        TagId idReference = (TagId)field.Value;
                        if (!idReference.IsNull && !references.Any(r => r.Key.Id == idReference) && m_ResourceMap.IndexEntries.Last.Id != idReference)
                        {
                            //Get reference
                            referenceEntry = m_ResourceMap.IndexEntries[idReference];
                            reader.BaseStream.Seek((uint)referenceEntry.PostProcessedOffset, SeekOrigin.Begin);
                            referenceTagGroup = Tag.Cache.Generated.TagLookup.CreateTagGroup(referenceEntry.Root);
                            referenceTagGroup.Read(reader);

                            //Chekc
                            if (referenceEntry.Root == HaloTags.ugh_) return;

                            //Add
                            references.Add(new ResourceTag(referenceEntry.Filename, referenceEntry.Root, referenceEntry.Id), new TagGroupFile());

                            //Build
                            CacheResourceTagGroup_BuildReferences(referenceTagGroup, references, reader);
                        }
                        break;

                    case FieldType.FieldTagReference:
                        TagReference tagReference = (TagReference)field.Value;
                        if (!tagReference.Id.IsNull && !references.Any(r => r.Key.Id == tagReference.Id))
                        {
                            //Get reference
                            referenceEntry = m_ResourceMap.IndexEntries[tagReference.Id];
                            reader.BaseStream.Seek((uint)referenceEntry.PostProcessedOffset, SeekOrigin.Begin);
                            referenceTagGroup = Tag.Cache.Generated.TagLookup.CreateTagGroup(referenceEntry.Root);
                            referenceTagGroup.Read(reader);

                            //Check
                            if (referenceEntry.Root == HaloTags.ugh_) return;

                            //Add
                            references.Add(new ResourceTag(referenceEntry.Filename, referenceEntry.Root, referenceEntry.Id), new TagGroupFile());

                            //Build
                            CacheResourceTagGroup_BuildReferences(referenceTagGroup, references, reader);
                        }
                        break;
                }
        }

        private void CacheResource_ModifyRawAddress(ITagGroup tagGroup)
        {
            //Check
            if (tagGroup == null) return;

            //Prepare
            bool modified = false;
            long offsetAddress = 0;
            uint rawAddress = 0;
            uint locationFlag = 0;

            //Get raw flag
            switch (m_ResourceMapType)
            {
                case 0: locationFlag = C_SinglePlayerSharedFlag; break;
                case 1: locationFlag = C_SharedFlag; break;
                default: throw new NotImplementedException();
            }

            //Create tag stream
            using (VirtualStream tagStream = new VirtualStream(0))
            using (BinaryWriter writer = tagStream.CreateWriter())
            using (BinaryReader reader = tagStream.CreateReader())
            {
                //Write tag to tag stream
                tagGroup.Write(writer);

                //Handle
                switch (tagGroup.GroupTag)
                {
                    #region sound cache file gestalt
                    case HaloTags.ugh_:
                        tagStream.Seek(64, SeekOrigin.Begin);
                        TagBlock chunks = reader.Read<TagBlock>();
                        for (int i = 0; i < chunks.Count; i++)
                        {
                            tagStream.Seek(chunks.Offset + (i * 12), SeekOrigin.Begin);
                            offsetAddress = tagStream.Position;
                            rawAddress = reader.ReadUInt32();
                            
                            if((rawAddress & C_LocationMask) == 0)
                            {
                                tagStream.Seek(offsetAddress, SeekOrigin.Begin);
                                writer.Write(rawAddress | locationFlag);
                            }
                        }

                        tagStream.Seek(80, SeekOrigin.Begin);
                        TagBlock extraInfo = reader.Read<TagBlock>();
                        for (int i = 0; i < extraInfo.Count; i++)
                        {
                            tagStream.Seek(extraInfo.Offset + (i * 44) + 8, SeekOrigin.Begin);
                            offsetAddress = tagStream.Position;
                            rawAddress = reader.ReadUInt32();
                            
                            if((rawAddress & C_LocationMask) == 0)
                            {
                                tagStream.Seek(offsetAddress, SeekOrigin.Begin);
                                writer.Write(rawAddress | locationFlag);
                            }
                        }
                        break;
                    #endregion
                    #region render model
                    case HaloTags.mode:
                        tagStream.Seek(36, SeekOrigin.Begin);
                        TagBlock sections = reader.Read<TagBlock>();
                        for (int i = 0; i < sections.Count; i++)
                        {
                            tagStream.Seek(sections.Offset + (i * 92) + 56, SeekOrigin.Begin);
                            offsetAddress = tagStream.Position;
                            rawAddress = reader.ReadUInt32();

                            if((rawAddress & C_LocationMask) == 0)
                            {
                                tagStream.Seek(offsetAddress, SeekOrigin.Begin);
                                writer.Write(rawAddress | locationFlag);
                                modified = true;
                            }
                        }

                        tagStream.Seek(116, SeekOrigin.Begin);
                        TagBlock prtInfo = reader.Read<TagBlock>();
                        for (int i = 0; i < prtInfo.Count; i++)
                        {
                            tagStream.Seek(prtInfo.Offset + (i * 88) + 52, SeekOrigin.Begin);
                            offsetAddress = tagStream.Position;
                            rawAddress = reader.ReadUInt32();

                            if((rawAddress & C_LocationMask) == 0)
                            {
                                tagStream.Seek(offsetAddress, SeekOrigin.Begin);
                                writer.Write(rawAddress | locationFlag);
                                modified = true;
                            }
                        }
                        break;
                    #endregion
                    #region weather system
                    case HaloTags.weat:
                        tagStream.Seek(0, SeekOrigin.Begin);
                        TagBlock particleSystem = reader.Read<TagBlock>();
                        for (int i = 0; i < particleSystem.Count; i++)
                        {
                            tagStream.Seek(particleSystem.Offset + (i * 140) + 64, SeekOrigin.Begin);
                            offsetAddress = tagStream.Position;
                            rawAddress = reader.ReadUInt32();

                            if((rawAddress & C_LocationMask) == 0)
                            {
                                tagStream.Seek(offsetAddress, SeekOrigin.Begin);
                                writer.Write(rawAddress | locationFlag);
                                modified = true;
                            }
                        }
                        break;
                    #endregion
                    #region decorator set
                    case HaloTags.DECR:
                        tagStream.Seek(56, SeekOrigin.Begin);
                        offsetAddress = tagStream.Position;
                        rawAddress = reader.ReadUInt32();

                        if((rawAddress & C_LocationMask) == 0)
                        {
                            tagStream.Seek(offsetAddress, SeekOrigin.Begin);
                            writer.Write(rawAddress | locationFlag);
                            modified = true;
                        }
                        break;
                    #endregion
                    #region particle model
                    case HaloTags.PRTM:
                        tagStream.Seek(160, SeekOrigin.Begin);
                        offsetAddress = tagStream.Position;
                        rawAddress = reader.ReadUInt32();

                        if ((rawAddress & C_LocationMask) == 0)
                        {
                            tagStream.Seek(offsetAddress, SeekOrigin.Begin);
                            writer.Write(rawAddress | locationFlag);
                            modified = true;
                        }
                        break;
                    #endregion
                    #region model animation graph
                    case HaloTags.jmad:
                        tagStream.Seek(172, SeekOrigin.Begin);
                        TagBlock xboxAnimationData = reader.Read<TagBlock>();
                        for (int i = 0; i < xboxAnimationData.Count; i++)
                        {
                            tagStream.Seek(xboxAnimationData.Offset + (i * 20) + 8);
                            offsetAddress = tagStream.Position;
                            rawAddress = reader.ReadUInt32();

                            if ((rawAddress & C_LocationMask) == 0)
                            {
                                tagStream.Seek(offsetAddress, SeekOrigin.Begin);
                                writer.Write(rawAddress | locationFlag);
                                modified = true;
                            }
                        }
                        break;
                    #endregion
                    #region bitmap
                    case HaloTags.bitm:
                        tagStream.Seek(68, SeekOrigin.Begin);
                        TagBlock bitmapData = reader.Read<TagBlock>();
                        for (int i = 0; i < bitmapData.Count; i++)
                        {
                            tagStream.Seek(bitmapData.Offset + (i * 116) + 28);
                            offsetAddress = tagStream.Position;
                            rawAddress = reader.ReadUInt32();

                            if ((rawAddress & C_LocationMask) == 0)
                            {
                                tagStream.Seek(offsetAddress, SeekOrigin.Begin);
                                writer.Write(rawAddress | locationFlag);
                                modified = true;
                            }

                            tagStream.Seek(bitmapData.Offset + (i * 116) + 32);
                            offsetAddress = tagStream.Position;
                            rawAddress = reader.ReadUInt32();

                            if ((rawAddress & C_LocationMask) == 0)
                            {
                                tagStream.Seek(offsetAddress, SeekOrigin.Begin);
                                writer.Write(rawAddress | locationFlag);
                                modified = true;
                            }

                            tagStream.Seek(bitmapData.Offset + (i * 116) + 36);
                            offsetAddress = tagStream.Position;
                            rawAddress = reader.ReadUInt32();

                            if ((rawAddress & C_LocationMask) == 0)
                            {
                                tagStream.Seek(offsetAddress, SeekOrigin.Begin);
                                writer.Write(rawAddress | locationFlag);
                                modified = true;
                            }

                            tagStream.Seek(bitmapData.Offset + (i * 116) + 40);
                            offsetAddress = tagStream.Position;
                            rawAddress = reader.ReadUInt32();

                            if ((rawAddress & C_LocationMask) == 0)
                            {
                                tagStream.Seek(offsetAddress, SeekOrigin.Begin);
                                writer.Write(rawAddress | locationFlag);
                                modified = true;
                            }

                            tagStream.Seek(bitmapData.Offset + (i * 116) + 44);
                            offsetAddress = tagStream.Position;
                            rawAddress = reader.ReadUInt32();

                            if ((rawAddress & C_LocationMask) == 0)
                            {
                                tagStream.Seek(offsetAddress, SeekOrigin.Begin);
                                writer.Write(rawAddress | locationFlag);
                                modified = true;
                            }

                            tagStream.Seek(bitmapData.Offset + (i * 116) + 48);
                            offsetAddress = tagStream.Position;
                            rawAddress = reader.ReadUInt32();

                            if ((rawAddress & C_LocationMask) == 0)
                            {
                                tagStream.Seek(offsetAddress, SeekOrigin.Begin);
                                writer.Write(rawAddress | locationFlag);
                                modified = true;
                            }
                        }
                        break;
                    #endregion
                    case HaloTags.snd_:

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

        private void CacheResource_PostprocessSound(TagGroupFile cacheFileSoundFile, IndexEntry indexEntry)
        {
            //Convert
            ITagGroup sound = Guerilla.Library.Convert.ToGuerilla(cacheFileSoundFile.TagGroup, m_ResourceSoundCacheFileGestalt, indexEntry, m_ResourceMap);

            //Loop through pitch ranges
            BaseBlockField soundPitchRanges = (BaseBlockField)sound[0].Fields[13];
            foreach (ITagBlock pitchRange in soundPitchRanges.BlockList)
            {
                //Convert name
                Field_Convert(pitchRange, 0);   //field 0 is import name

                //Loop through permutations
                BaseBlockField pitchRangePermutations = (BaseBlockField)pitchRange.Fields[7];
                foreach (ITagBlock permutation in pitchRangePermutations.BlockList)
                {
                    //Convert name
                    Field_Convert(permutation, 0);

                    //Loop through sound chunks
                    BaseBlockField permutationChunks = (BaseBlockField)permutation.Fields[6];
                    foreach (ITagBlock chunk in permutationChunks.BlockList)
                    {
                        int fileOffset = (int)chunk.Fields[0].Value;
                        if ((fileOffset & C_LocationMask) == 0)
                        {
                            switch (m_ResourceMapType)
                            {
                                case 1: chunk.Fields[0].Value = (int)(fileOffset | C_SharedFlag); break;
                                case 2: chunk.Fields[0].Value = (int)(fileOffset | C_MainmenuFlag); break;
                                case 3: chunk.Fields[0].Value = (int)(fileOffset | C_SinglePlayerSharedFlag); break;
                                default: chunk.Fields[0].Value = -1; break;
                            }
                        }
                    }
                }
            }

            //Loop through extra infos
            BaseBlockField soundExtraInfos = (BaseBlockField)sound[0].Fields[15];
            foreach (ITagBlock extraInfo in soundExtraInfos.BlockList)
            {

            }

            //Set
            cacheFileSoundFile.TagGroup = sound;

            //Return
            return;

            /*
            //Prepare
            ITagGroup sound = new Tag.Guerilla.Generated.Sound();
            ITagBlock soundCacheFileGestaltBlock = m_ResourceSoundCacheFileGestalt[0];
            ITagBlock cacheFileSoundBlock = cacheFileSound[0];
            ITagBlock soundBlock = sound[0];

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

            //Convert fields
            soundBlock.Fields[0].Value = (int)(short)cacheFileSoundBlock.Fields[0].Value;   //flags
            soundBlock.Fields[1].Value = cacheFileSoundBlock.Fields[1].Value;   //class
            soundBlock.Fields[2].Value = cacheFileSoundBlock.Fields[2].Value;   //sample rate
            soundBlock.Fields[9].Value = cacheFileSoundBlock.Fields[3].Value;   //encoding
            soundBlock.Fields[10].Value = cacheFileSoundBlock.Fields[4].Value;  //compression

            //Get playback
            if ((short)cacheFileSoundBlock.Fields[5].Value != C_NullShort)
            {
                ITagBlock playback = playbacks.BlockList[(short)cacheFileSoundBlock.Fields[5].Value];
                soundBlock.Fields[5].Value = playback.Fields[0].Value;
            }

            //Get scale
            if ((byte)cacheFileSoundBlock.Fields[8].Value != C_NullByte)
            {
                ITagBlock scale = scales.BlockList[(byte)cacheFileSoundBlock.Fields[8].Value];
                soundBlock.Fields[6].Value = scale.Fields[0].Value;
            }

            //Get promotion
            if ((byte)cacheFileSoundBlock.Fields[9].Value != C_NullByte)
            {
                ITagBlock promotion = promotions.BlockList[(byte)cacheFileSoundBlock.Fields[9].Value];
                soundBlock.Fields[11].Value = promotion.Fields[0].Value;
            }

            //Get custom playback
            if ((byte)cacheFileSoundBlock.Fields[10].Value != C_NullByte)
            {
                BaseBlockField platformParameters = (BaseBlockField)soundBlock.Fields[14];
                ITagBlock soundPlatformPlaybackBlock = platformParameters.Add(out bool success);
                if (success)
                {
                    ITagBlock customPlayback = customPlaybacks.BlockList[(byte)cacheFileSoundBlock.Fields[10].Value];
                    soundPlatformPlaybackBlock.Fields[0].Value = customPlayback.Fields[0].Value;
                }
            }

            //Get extra infos
            if ((short)cacheFileSoundBlock.Fields[11].Value != C_NullShort)
            {
                BaseBlockField unnamed = (BaseBlockField)soundBlock.Fields[15];
                ITagBlock extraInfo = unnamed.Add(out bool success);
                if (success)
                {
                    //Get encoded permutation section
                    ITagBlock soundGestaltExtraInfo = extraInfos.BlockList[(short)cacheFileSoundBlock.Fields[11].Value];
                    foreach (ITagBlock section in ((BaseBlockField)soundGestaltExtraInfo.Fields[0]).BlockList)
                        ((BaseBlockField)extraInfo.Fields[1]).BlockList.Add(section);

                    //Get geometry block info
                    extraInfo.Fields[2].Value = (ITagBlock)soundGestaltExtraInfo.Fields[1].Value;
                    ((ITagBlock)extraInfo.Fields[2].Value).Fields[6] = new Tag.Cache.TagIndexField(string.Empty) { Value = TagId.Null };
                }
            }

            //Get pitch ranges
            if ((short)cacheFileSoundBlock.Fields[6].Value != C_NullShort)
            {
                short pitchRangeIndex = (short)cacheFileSoundBlock.Fields[6].Value;
                byte pitchRangeCount = (byte)cacheFileSoundBlock.Fields[7].Value;
                BaseBlockField soundPitchRanges = (BaseBlockField)soundBlock.Fields[13];
                for (int i = 0; i < pitchRangeCount; i++)
                {
                    ITagBlock soundPitchRange = soundPitchRanges.Add(out bool success);
                    if (success)
                    {
                        ITagBlock pitchRange = pitchRanges.BlockList[pitchRangeIndex + i];
                        ITagBlock importName = importNames.BlockList[(short)pitchRange.Fields[0].Value];
                        ITagBlock pitchRangeParameter = pitchRangeParameters.BlockList[(short)pitchRange.Fields[1].Value];
                        soundPitchRange.Fields[0] = new Tag.Cache.StringIdField(soundPitchRange.Fields[0].GetName());
                        soundPitchRange.Fields[2].Value = (short)pitchRangeParameter.Fields[0].Value;
                        soundPitchRange.Fields[4].Value = (ShortBounds)pitchRangeParameter.Fields[1].Value;
                        soundPitchRange.Fields[5].Value = (ShortBounds)pitchRangeParameter.Fields[2].Value;

                        //Get sound permutations
                        if ((short)pitchRange.Fields[4].Value != C_NullShort)
                        {
                            short permutationIndex = (short)pitchRange.Fields[4].Value;
                            short permutationCount = (short)pitchRange.Fields[5].Value;
                            BaseBlockField soundPermutations = (BaseBlockField)soundPitchRange.Fields[7];
                            for (int j = 0; j < permutationCount; j++)
                            {
                                ITagBlock soundPermutation = soundPermutations.Add(out success);
                                if (success)
                                {
                                    ITagBlock permutation = permutations.BlockList[permutationIndex + j];
                                    importName = importNames.BlockList[(short)permutation.Fields[0].Value];
                                    soundPermutation.Fields[0] = new Tag.Cache.StringIdField(soundPitchRange.Fields[0].GetName());
                                    soundPermutation.Fields[1].Value = (ushort)(short)permutation.Fields[1].Value / 65535f;
                                    soundPermutation.Fields[2].Value = (byte)permutation.Fields[2].Value / 255f;
                                    soundPermutation.Fields[3].Value = (int)permutation.Fields[5].Value;
                                    soundPermutation.Fields[4].Value = (short)(byte)permutation.Fields[3].Value;
                                    soundPermutation.Fields[5].Value = (short)permutation.Fields[4].Value;

                                    //Copy chunks
                                    if ((short)permutation.Fields[6].Value != C_NullShort)
                                    {
                                        short chunkIndex = (short)permutation.Fields[6].Value;
                                        short chunkCount = (short)permutation.Fields[7].Value;
                                        BaseBlockField soundChunks = (BaseBlockField)soundPermutation.Fields[6];
                                        for (int k = 0; k < chunkCount; k++)
                                            soundChunks.BlockList.Add(chunks.BlockList[chunkIndex + k]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            */
        }

        private void CacheResource_PostprocessMultilingualUnicodeStringList(TagGroupFile multilingualUnicodeStringListFile, StringContainer strings)
        {
            //Add string Ids
            foreach (var stringObject in strings.English)
                if (!m_StringsList.Contains(stringObject.ID))
                    m_StringsList.Add(stringObject.ID);
            foreach (var stringObject in strings.Japanese)
                if (!m_StringsList.Contains(stringObject.ID))
                    m_StringsList.Add(stringObject.ID);
            foreach (var stringObject in strings.German)
                if (!m_StringsList.Contains(stringObject.ID))
                    m_StringsList.Add(stringObject.ID);
            foreach (var stringObject in strings.French)
                if (!m_StringsList.Contains(stringObject.ID))
                    m_StringsList.Add(stringObject.ID);
            foreach (var stringObject in strings.Spanish)
                if (!m_StringsList.Contains(stringObject.ID))
                    m_StringsList.Add(stringObject.ID);
            foreach (var stringObject in strings.Italian)
                if (!m_StringsList.Contains(stringObject.ID))
                    m_StringsList.Add(stringObject.ID);
            foreach (var stringObject in strings.Korean)
                if (!m_StringsList.Contains(stringObject.ID))
                    m_StringsList.Add(stringObject.ID);
            foreach (var stringObject in strings.Chinese)
                if (!m_StringsList.Contains(stringObject.ID))
                    m_StringsList.Add(stringObject.ID);
            foreach (var stringObject in strings.Portuguese)
                if (!m_StringsList.Contains(stringObject.ID))
                    m_StringsList.Add(stringObject.ID);

            //Get multilingual unicode string list
            ITagGroup multilingualUnicodeStringList = multilingualUnicodeStringListFile.TagGroup;
            ITagBlock multilingualUnicodeStringListBlock = multilingualUnicodeStringList[0];
            
            //Modify 'padding' (it actually contains postprocessed data, so we're postprocessing it here)
            byte[] padding = (byte[])((PadField)multilingualUnicodeStringListBlock.Fields[2]).Value;
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

        private void TagGroup_CollectReferences(TagGroupFile tagGroupFile)
        {
            //Loop
            for (int i = 0; i < tagGroupFile.TagGroup.Count; i++)
                TagBlock_GetReferences(tagGroupFile.TagGroup[i]);

            //Check
            if (tagGroupFile.TagGroup.GroupTag == HaloTags.snd_)
                SoundGestalt_AddSound(tagGroupFile);

            //Write and re-read
            ITagGroup tagGroup = Tag.Cache.Generated.TagLookup.CreateTagGroup(tagGroupFile.TagGroup.GroupTag);
            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(ms))
            using (BinaryReader reader = new BinaryReader(ms))
            {
                //Write
                tagGroupFile.TagGroup.Write(writer);

                //Read
                ms.Seek(0, SeekOrigin.Begin);
                tagGroup.Read(reader);

                //Set
                tagGroupFile.TagGroup = tagGroup;
            }
        }

        private void TagBlock_GetReferences(ITagBlock tagBlock)
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
                            TagBlock_GetReferences(block);
                        break;

                    case FieldType.FieldStruct:
                        if (field.Value is ITagBlock)
                            TagBlock_GetReferences((ITagBlock)field.Value);
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
                                if (!m_TagGroupReferences.ContainsKey(tagString))
                                {
                                    //Load
                                    file.Load(fileName);
                                    file.Id = m_CurrentId;

                                    //Add
                                    string tagPath = tagString.Value.Substring(0, tagString.Value.LastIndexOf('.'));
                                    m_TagIdReferences.Add($@"{tagPath}.{file.TagGroup.GroupTag}", m_CurrentId);
                                    m_TagGroupReferences.Add(tagString, file);
                                    m_CurrentId++;

                                    //Add references
                                    TagGroup_CollectReferences(file);
                                }
                            }
                        }

                        //Convert
                        Field_Convert(tagBlock, i);
                        break;

                    case FieldType.FieldOldStringId:
                    case FieldType.FieldStringId:
                        if (field.Value is StringValue str && !string.IsNullOrEmpty(str.Value))
                        {
                            //Check
                            if (!m_StringsList.Contains(str.Value))
                                m_StringsList.Add(str.Value);
                        }

                        //Convert
                        Field_Convert(tagBlock, i);
                        break;
                }
            }
        }

        private void Field_Convert(ITagBlock tagBlock, int fieldIndex)
        {
            //Prepare
            string stringValue = tagBlock.Fields[fieldIndex].Value?.ToString() ?? string.Empty;
            Field field = tagBlock.Fields[fieldIndex];
            string tagPath = string.Empty;
            TagGroupFile file = null;

            //Handle
            switch (tagBlock.Fields[fieldIndex].Type)
            {
                case FieldType.FieldTagReference:
                    var tagReferenceField = new Tag.Cache.TagReferenceField(field.GetName(), ((TagReferenceField)field).GroupTag);
                    tagReferenceField.Value = new TagReference() { Tag = ((TagReferenceField)field).GroupTag, Id = TagId.Null };
                    if (m_TagGroupReferences.ContainsKey(stringValue))
                    {
                        file = m_TagGroupReferences[stringValue];
                        tagPath = $"{stringValue.Substring(0, stringValue.LastIndexOf('.'))}.{file.TagGroup.GroupTag}";
                        tagReferenceField.Value = new TagReference() { Tag = file.TagGroup.GroupTag, Id = m_TagIdReferences[tagPath] };
                    }
                    tagBlock.Fields[fieldIndex] = tagReferenceField;
                    break;

                case FieldType.FieldTagIndex:
                    var tagIndexField = new Tag.Cache.TagIndexField(field.GetName());
                    tagIndexField.Value = TagId.Null;
                    if (m_TagGroupReferences.ContainsKey(stringValue))
                    {
                        file = m_TagGroupReferences[stringValue];
                        tagPath = $"{stringValue.Substring(0, stringValue.LastIndexOf('.'))}.{file.TagGroup.GroupTag}";
                        tagIndexField.Value = m_TagIdReferences[tagPath];
                    }
                    tagBlock.Fields[fieldIndex] = tagIndexField;
                    break;

                case FieldType.FieldOldStringId:
                    var oldStringIdField = new Tag.Cache.OldStringIdField(field.GetName());
                    if (!m_StringsList.Contains(stringValue))
                        m_StringsList.Add(stringValue);
                    oldStringIdField.Value = new StringId((ushort)m_StringsList.IndexOf(stringValue), (byte)stringValue.Length);
                    tagBlock.Fields[fieldIndex] = oldStringIdField;
                    break;
                case FieldType.FieldStringId:
                    var stringIdField = new Tag.Cache.StringIdField(field.GetName());
                    if (!m_StringsList.Contains(stringValue))
                        m_StringsList.Add(stringValue);
                    stringIdField.Value = new StringId((ushort)m_StringsList.IndexOf(stringValue), (byte)stringValue.Length);
                    tagBlock.Fields[fieldIndex] = stringIdField;
                    break;
            }
        }

        private void MultilingualUnicodeStringList_Postprocess(TagGroupFile value)
        {
            //Prepare
            StringContainer strings = new StringContainer();
            byte[] stringData = ((DataField)value.TagGroup[0].Fields[1]).GetBuffer();
            string unicodeString = string.Empty;
            StringId stringId = StringId.Zero;
            int offset = 0;
            
            //Create stream
            using (MemoryStream ms = new MemoryStream(stringData))
            using (BinaryReader reader = new BinaryReader(ms))
            {
                //Loop
                foreach (ITagBlock stringReferenceBlock in ((BaseBlockField)value.TagGroup[0].Fields[0]).BlockList)
                {
                    //Get
                    stringId = (StringId)stringReferenceBlock.Fields[0].Value;

                    //Goto English
                    offset = (int)stringReferenceBlock.Fields[1].Value;
                    if (offset >= 0)
                    {
                        ms.Seek(offset, SeekOrigin.Begin);
                        unicodeString = reader.ReadUTF8NullTerminated();
                        strings.English.Add(new StringEntry(unicodeString, m_StringsList[stringId.Index]));
                    }

                    //Goto Japanese
                    offset = (int)stringReferenceBlock.Fields[2].Value;
                    if (offset >= 0)
                    {
                        ms.Seek(offset, SeekOrigin.Begin);
                        unicodeString = reader.ReadUTF8NullTerminated();
                        strings.Japanese.Add(new StringEntry(unicodeString, m_StringsList[stringId.Index]));
                    }

                    //Goto German
                    offset = (int)stringReferenceBlock.Fields[3].Value;
                    if (offset >= 0)
                    {
                        ms.Seek(offset, SeekOrigin.Begin);
                        unicodeString = reader.ReadUTF8NullTerminated();
                        strings.German.Add(new StringEntry(unicodeString, m_StringsList[stringId.Index]));
                    }

                    //Goto French
                    offset = (int)stringReferenceBlock.Fields[4].Value;
                    if (offset >= 0)
                    {
                        ms.Seek(offset, SeekOrigin.Begin);
                        unicodeString = reader.ReadUTF8NullTerminated();
                        strings.French.Add(new StringEntry(unicodeString, m_StringsList[stringId.Index]));
                    }

                    //Goto Spanish
                    offset = (int)stringReferenceBlock.Fields[5].Value;
                    if (offset >= 0)
                    {
                        ms.Seek(offset, SeekOrigin.Begin);
                        unicodeString = reader.ReadUTF8NullTerminated();
                        strings.Spanish.Add(new StringEntry(unicodeString, m_StringsList[stringId.Index]));
                    }

                    //Goto Italian
                    offset = (int)stringReferenceBlock.Fields[6].Value;
                    if (offset >= 0)
                    {
                        ms.Seek(offset, SeekOrigin.Begin);
                        unicodeString = reader.ReadUTF8NullTerminated();
                        strings.Italian.Add(new StringEntry(unicodeString, m_StringsList[stringId.Index]));
                    }

                    //Goto Korean
                    offset = (int)stringReferenceBlock.Fields[7].Value;
                    if (offset >= 0)
                    {
                        ms.Seek(offset, SeekOrigin.Begin);
                        unicodeString = reader.ReadUTF8NullTerminated();
                        strings.Korean.Add(new StringEntry(unicodeString, m_StringsList[stringId.Index]));
                    }

                    //Goto Chinese
                    offset = (int)stringReferenceBlock.Fields[8].Value;
                    if (offset >= 0)
                    {
                        ms.Seek(offset, SeekOrigin.Begin);
                        unicodeString = reader.ReadUTF8NullTerminated();
                        strings.Chinese.Add(new StringEntry(unicodeString, m_StringsList[stringId.Index]));
                    }

                    //Goto Portuguese
                    offset = (int)stringReferenceBlock.Fields[9].Value;
                    if (offset >= 0)
                    {
                        ms.Seek(offset, SeekOrigin.Begin);
                        unicodeString = reader.ReadUTF8NullTerminated();
                        strings.Portuguese.Add(new StringEntry(unicodeString, m_StringsList[stringId.Index]));
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

        private void SoundGestalt_AddSound(TagGroupFile soundTag)
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

            //Check
            if (soundBlock.Name != "sound_block") return;

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
            if (validPromotion) cacheFileSoundBlock.Fields[9].Value = (byte)(sbyte)SoundGestalt_FindPromotionIndex((ITagBlock)soundBlock.Fields[11].Value);
            else cacheFileSoundBlock.Fields[9].Value = C_NullByte;

            //Add custom playback
            if (((BaseBlockField)soundBlock.Fields[14]).BlockList.Count > 0)
            {
                index = customPlaybacks.BlockList.Count;
                ITagBlock customPlayback = customPlaybacks.Add(out success);
                if (success)
                {
                    cacheFileSoundBlock.Fields[10].Value = (byte)index;
                    customPlayback.Fields[0].Value = ((BaseBlockField)soundBlock.Fields[14]).BlockList[0];
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
                    ((ITagBlock)extraInfo.Fields[1].Value).Fields[6] = new Tag.Cache.TagIndexField(string.Empty) { Value = (TagId)SoundCacheFileGestaltFile.Id };
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
                    gestaltPitchRange.Fields[0].Value = (short)SoundGestalt_FindImportNameIndex((StringId)soundPitchRange.Fields[0].Value);

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
                            gestaltPermutation.Fields[0].Value = (short)SoundGestalt_FindImportNameIndex((StringId)soundPermutation.Fields[0].Value);

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

        private int SoundGestalt_FindImportNameIndex(StringId stringId)
        {
            //Prepare
            ITagBlock soundCacheFileGestaltBlock = SoundCacheFileGestaltFile.TagGroup[0];
            BaseBlockField blockField = (BaseBlockField)soundCacheFileGestaltBlock.Fields[2];
            int index = -1;

            //Check
            foreach (ITagBlock gestaltBlock in blockField.BlockList)
            {
                if ((StringId)gestaltBlock.Fields[0].Value == stringId)
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
                    #region bitmap
                    case HaloTags.bitm:
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
                    #region structure bsp
                    case HaloTags.sbsp:
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

        private sealed class ResourceTag : IEquatable<ResourceTag>
        {
            public bool Merge { get; set; } = false;
            public string FileName { get; }
            public TagFourCc Root { get; }
            public TagId Id { get; set; }
            public TagId NewId { get; set; }
            
            public ResourceTag(string fileName, TagFourCc root, TagId id)
            {
                FileName = fileName;
                Root = root;
                Id = id;
            }
            public override string ToString()
            {
                return $"{FileName}.{Root.FourCc} 0x{Id}";
            }
            public bool Equals(ResourceTag other)
            {
                return FileName.Equals(other.FileName) && Root.Equals(other.Root) && Id.Equals(other.Id);
            }
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
