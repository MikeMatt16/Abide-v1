using Abide.Guerilla.Library;
using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2.Retail;
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
    /// Represents a Halo 2 cache map file compiler
    /// </summary>
    public sealed class MapCompiler
    {
        private const short NullShort = unchecked((short)0xffff);
        private const byte NullByte = 0xff;
        private const uint LocationMask = 0xC0000000;

        private readonly MultilingualUnicodeStringsContainer multilingualUnicodeStringsContainer = new MultilingualUnicodeStringsContainer();
        private readonly Dictionary<string, AbideTagGroupFile> tagResources = new Dictionary<string, AbideTagGroupFile>();
        private readonly string workspaceDirectory;
        private readonly string scenarioFileName;
        private HaloMap resourceMapFile;
        private List<string> stringsList = new List<string>();
        private TagId currentId = TagId.Null;
        private int mapType = -1;

        public string OutputMapFileName { get; }
        public string MapName { get; }
        public string ScenarioPath { get; }
        public AbideTagGroupFile ScenarioFile { get; } = new AbideTagGroupFile();
        public AbideTagGroupFile SoundCacheFileGestaltFile { get; } = new AbideTagGroupFile();
        public AbideTagGroupFile GlobalsFile { get; } = new AbideTagGroupFile();
        public AbideTagGroupFile SoundClassesFile { get; } = new AbideTagGroupFile();
        public AbideTagGroupFile CombatDialogueConstantFile { get; } = new AbideTagGroupFile();
        public AbideTagGroupFile MultiplayerGlobalsFile { get; } = new AbideTagGroupFile();
        
        public MapCompiler(string scenarioFileName, string workspaceDirectory)
        {
            this.scenarioFileName = scenarioFileName;
            this.workspaceDirectory = workspaceDirectory;
            
            ScenarioPath = scenarioFileName.Replace(Path.Combine(workspaceDirectory, "tags"), string.Empty).Substring(1).Replace(".scenario", string.Empty);
            MapName = Path.GetFileName(ScenarioPath);
            OutputMapFileName = Path.Combine(this.workspaceDirectory, "maps", $"{MapName}.map");
        }
        public void Compile()
        {
            DateTime start = DateTime.Now;

            ScenarioFile.Load(scenarioFileName);
            mapType = (short)ScenarioFile.TagGroup.TagBlocks[0].Fields[2].Value;

            stringsList = new List<string>(CommonStrings.GetCommonStrings());
            currentId = 0xe1740000;

            Console.WriteLine("Compile Map: {0} ({1})", MapName, OutputMapFileName);
            Console.WriteLine("Preparing tag resources");
            Globals_Prepare();
            Scenario_Prepare();
            SoundCacheFileGestalt_Prepare();

            Console.WriteLine("Discovering tag references");
            TagResources_Discover(ScenarioFile.TagGroup);
            TagResources_Discover(MultiplayerGlobalsFile.TagGroup);
            MultiplayerGlobalsFile.Id = currentId++;
            TagResources_Discover(GlobalsFile.TagGroup);
            SoundCacheFileGestaltFile.Id = currentId++;

            Dictionary<string, AbideTagGroupFile> orderedTagResources = tagResources
                .OrderBy(kvp => kvp.Value.Id)
                .ToDictionary(x => x.Key, x => x.Value);

            foreach (KeyValuePair<string, AbideTagGroupFile> resource in orderedTagResources)
            {
                Console.WriteLine("Processing {0} tag \'{1}\'", resource.Value.TagGroup.GroupName, resource.Key);
                TagFourCc groupTag = resource.Value.TagGroup.GroupTag;

                switch (resource.Value.TagGroup.GroupTag)
                {
                    case HaloTags.unic:
                        MultilingualUnicodeStringList_Process(resource.Value);
                        break;
                    case HaloTags.snd_:
                        Sound_Process(resource.Value);
                        break;
                    default:
                        Tag_Process(resource.Value);
                        break;
                }

                using (MemoryStream ms = new MemoryStream())
                using (BinaryWriter writer = new BinaryWriter(ms))
                using (BinaryReader reader = new BinaryReader(ms))
                {
                    resource.Value.TagGroup.Write(writer);
                    ms.Seek(0, SeekOrigin.Begin);

                    resource.Value.TagGroup = Tag.Cache.Generated.TagLookup.CreateTagGroup(groupTag);
                    resource.Value.TagGroup.Read(reader);
                }
            }

            Map_BuildFile(orderedTagResources);

            resourceMapFile.Dispose();
            tagResources.Clear();
            multilingualUnicodeStringsContainer.Clear();
            stringsList.Clear();
            currentId = TagId.Null;
            mapType = -1;

            Console.WriteLine("Cache file created in {0} seconds.", (DateTime.Now - start).TotalSeconds);
        }

        private void Map_BuildFile(Dictionary<string, AbideTagGroupFile> tagResources)
        {
            AbideTagGroupFile[] references = tagResources.Select(kvp => kvp.Value).ToArray();
            string[] tagNames = tagResources.Select(kvp => kvp.Key.Substring(0, kvp.Key.LastIndexOf('.'))).ToArray();

            StructureBspBlockHeader bspHeader = new StructureBspBlockHeader();
            Header header = Header.CreateDefault();
            Index index = new Index();
            TagHierarchy[] tags = SharedResources.GetTags();
            ObjectEntry[] objects = new ObjectEntry[references.Length];
            long indexLength = 0, bspAddress = 0, bspLength = 0, tagDataAddress = 0, tagDataLength = 0;

            string outputDirectory = Path.Combine(workspaceDirectory, "maps");
            if (!Directory.Exists(outputDirectory))
                Directory.CreateDirectory(outputDirectory);

            using (FileStream fs = new FileStream(OutputMapFileName, FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
            using (BinaryWriter mapWriter = new BinaryWriter(fs))
            using (BinaryReader mapReader = new BinaryReader(fs))
            {
                header.ScenarioPath = ScenarioPath;
                header.Name = MapName;

                fs.Seek(Header.Length, SeekOrigin.Begin);
                foreach (var reference in tagResources.Where(r => r.Value.TagGroup.GroupTag == HaloTags.ugh_))  //sound cache file gestalt
                    TagGroupFile_WriteRaws(reference.Value, fs, mapWriter);
                foreach (var reference in tagResources.Where(r => r.Value.TagGroup.GroupTag == HaloTags.mode))  //render model geometry
                    TagGroupFile_WriteRaws(reference.Value, fs, mapWriter);
                foreach (var reference in tagResources.Where(r => r.Value.TagGroup.GroupTag == HaloTags.sbsp))  //structure bsp geometry
                    TagGroupFile_WriteRaws(reference.Value, fs, mapWriter);
                foreach (var reference in tagResources.Where(r => r.Value.TagGroup.GroupTag == HaloTags.ltmp))  //structure lightmap geometry
                    TagGroupFile_WriteRaws(reference.Value, fs, mapWriter);
                foreach (var reference in tagResources.Where(r => r.Value.TagGroup.GroupTag == HaloTags.weat))  //weather system geometry
                    TagGroupFile_WriteRaws(reference.Value, fs, mapWriter);
                foreach (var reference in tagResources.Where(r => r.Value.TagGroup.GroupTag == HaloTags.DECR))  //decorator set geometry
                    TagGroupFile_WriteRaws(reference.Value, fs, mapWriter);
                foreach (var reference in tagResources.Where(r => r.Value.TagGroup.GroupTag == HaloTags.PRTM))  //particle model geometry
                    TagGroupFile_WriteRaws(reference.Value, fs, mapWriter);
                foreach (var reference in tagResources.Where(r => r.Value.TagGroup.GroupTag == HaloTags.ugh_))  //sound gestalt extra info geometry
                    SoundTagGroupFile_WriteExtraInfoRaws(reference.Value, fs, mapWriter);
                foreach (var reference in tagResources.Where(r => r.Value.TagGroup.GroupTag == HaloTags.jmad))  //model animation
                    TagGroupFile_WriteRaws(reference.Value, fs, mapWriter);

                Console.WriteLine("Building map tag data table...");
                using (VirtualStream indexStream = new VirtualStream(Index.IndexVirtualAddress))
                using (BinaryWriter indexWriter = new BinaryWriter(indexStream))
                {
                    indexStream.Seek(Index.Length, SeekOrigin.Current);
                    indexStream.Seek(tags.Length * TagHierarchy.Length, SeekOrigin.Current);
                    indexStream.Seek(objects.Length * ObjectEntry.Length, SeekOrigin.Current);
                    indexStream.Align(4096, 0);

                    indexLength = indexStream.Length;
                }

                header.IndexLength = (uint)indexLength;
                index.TagsAddress = Index.IndexTagsAddress;
                index.TagCount = (uint)tags.Length;
                index.ObjectsOffset = (uint)(tags.Length * TagHierarchy.Length) + Index.IndexTagsAddress;
                index.ScenarioId = ScenarioFile.Id;
                index.GlobalsId = GlobalsFile.Id;
                index.ObjectCount = (uint)references.Length;
                index.Tags = "tags";

                Console.WriteLine("Writing BSP tag data...");
                foreach (Block structureBsp in ScenarioFile.TagGroup.TagBlocks[0].Fields[68].GetBlockList())
                {
                    structureBsp.Fields[0] = new StructField<Tag.Cache.Generated.ScenarioStructureBspInfoStructBlock>(string.Empty);
                    Block scenarioStructureBspInfoStructBlock = (Block)structureBsp.Fields[0].Value;

                    bspAddress = Index.IndexVirtualAddress + indexLength;
                    TagReference structureBspTagReference = (TagReference)structureBsp.Fields[1].Value;
                    TagReference structureLightmapReference = (TagReference)structureBsp.Fields[2].Value;
                    using (VirtualStream bspDataStream = new VirtualStream(bspAddress))
                    using (BinaryWriter writer = bspDataStream.CreateWriter())
                    {
                        bspDataStream.Seek(StructureBspBlockHeader.Length, SeekOrigin.Current);
                        AbideTagGroupFile structureBspFile, structureLightmapFile;
                        if (tagResources.Any(kvp => kvp.Value.Id == structureBspTagReference.Id.Dword))
                        {
                            bspHeader.StructureBspOffset = (uint)bspDataStream.Position;
                            structureBspFile = tagResources.First(kvp => kvp.Value.Id == structureBspTagReference.Id).Value;
                            structureBspFile.TagGroup.Write(writer);
                            bspDataStream.Align(4096);
                        }

                        if (tagResources.Any(kvp => kvp.Value.Id == structureLightmapReference.Id.Dword))
                        {
                            bspHeader.StructureLightmapOffset = (uint)bspDataStream.Position;
                            structureLightmapFile = tagResources.First(kvp => kvp.Value.Id == structureLightmapReference.Id).Value;
                            structureLightmapFile.TagGroup.Write(writer);
                            bspDataStream.Align(4096);
                        }

                        bspHeader.StructureBsp = HaloTags.sbsp;
                        bspHeader.BlockLength = (int)bspDataStream.Length;

                        if (bspDataStream.Length > bspLength)
                            bspLength = bspDataStream.Length;

                        bspDataStream.Seek(bspAddress, SeekOrigin.Begin);
                        writer.Write(bspHeader);

                        scenarioStructureBspInfoStructBlock.Fields[0].Value = (int)fs.Position;
                        scenarioStructureBspInfoStructBlock.Fields[1].Value = (int)bspDataStream.Length;
                        scenarioStructureBspInfoStructBlock.Fields[2].Value = (int)bspAddress;

                        mapWriter.Write(bspDataStream.ToArray());
                    }
                }

                Console.WriteLine("Writing strings...");
                header.StringCount = (uint)stringsList.Count;
                header.Strings128Offset = (uint)fs.Position;
                foreach (string stringId in stringsList)
                    mapWriter.WriteUTF8(stringId.PadRight(128, '\0'));

                int offset = 0;
                header.StringsIndexOffset = (uint)fs.Align(512, 0);
                foreach (string stringId in stringsList)
                {
                    mapWriter.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringId) + 1;
                }

                header.StringsOffset = (uint)fs.Align(512, 0);
                foreach (string stringId in stringsList)
                    mapWriter.WriteUTF8NullTerminated(stringId);
                header.StringsLength = (uint)stringsList.Sum(s => Encoding.UTF8.GetByteCount(s) + 1);

                Console.WriteLine("Writing file names...");
                header.FileNameCount = (uint)tagNames.Length;
                header.FileNamesOffset = (uint)fs.Align(512, 0);
                header.FileNamesLength = (uint)tagNames.Sum(s => Encoding.UTF8.GetByteCount(s) + 1);
                foreach (string fileName in tagNames)
                    mapWriter.WriteUTF8NullTerminated(fileName);

                offset = 0;
                header.FileNamesIndexOffset = (uint)fs.Align(512, 0);
                foreach (var fileName in tagNames)
                {
                    mapWriter.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(fileName) + 1;
                }

                Console.WriteLine("Writing multilingual unicode strings...");
                offset = 0;
                uint enIndex = (uint)fs.Align(512, 0);
                for (int i = 0; i < multilingualUnicodeStringsContainer.En.Count; i++)
                {
                    var stringObject = multilingualUnicodeStringsContainer.En[i];
                    mapWriter.Write(StringId.FromString(stringObject.ID, stringsList.IndexOf(stringObject.ID)));
                    mapWriter.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringObject.Value) + 1;
                }
                uint enTable = (uint)fs.Align(512, 0);
                using (MemoryStream ms = new MemoryStream())
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    foreach (var stringObject in multilingualUnicodeStringsContainer.En)
                        writer.WriteUTF8NullTerminated(stringObject.Value);
                    multilingualUnicodeStringsContainer.EnSize = (int)ms.Length;
                    mapWriter.Write(ms.ToArray());
                }

                offset = 0;
                uint jpIndex = (uint)fs.Align(512, 0);
                for (int i = 0; i < multilingualUnicodeStringsContainer.Jp.Count; i++)
                {
                    var stringObject = multilingualUnicodeStringsContainer.Jp[i];
                    mapWriter.Write(StringId.FromString(stringObject.ID, stringsList.IndexOf(stringObject.ID)));
                    mapWriter.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringObject.Value) + 1;
                }
                uint jpTable = (uint)fs.Align(512, 0);
                using (MemoryStream ms = new MemoryStream())
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    foreach (var stringObject in multilingualUnicodeStringsContainer.Jp)
                        writer.WriteUTF8NullTerminated(stringObject.Value);
                    multilingualUnicodeStringsContainer.JpSize = (int)ms.Length;
                    mapWriter.Write(ms.ToArray());
                }

                offset = 0;
                uint nlIndex = (uint)fs.Align(512, 0);
                for (int i = 0; i < multilingualUnicodeStringsContainer.Nl.Count; i++)
                {
                    var stringObject = multilingualUnicodeStringsContainer.Nl[i];
                    mapWriter.Write(StringId.FromString(stringObject.ID, stringsList.IndexOf(stringObject.ID)));
                    mapWriter.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringObject.Value) + 1;
                }
                uint nlTable = (uint)fs.Align(512, 0);
                using (MemoryStream ms = new MemoryStream())
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    foreach (var stringObject in multilingualUnicodeStringsContainer.Nl)
                        writer.WriteUTF8NullTerminated(stringObject.Value);
                    multilingualUnicodeStringsContainer.NlSize = (int)ms.Length;
                    mapWriter.Write(ms.ToArray());
                }

                offset = 0;
                uint frIndex = (uint)fs.Align(512, 0);
                for (int i = 0; i < multilingualUnicodeStringsContainer.Fr.Count; i++)
                {
                    var stringObject = multilingualUnicodeStringsContainer.Fr[i];
                    mapWriter.Write(StringId.FromString(stringObject.ID, stringsList.IndexOf(stringObject.ID)));
                    mapWriter.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringObject.Value) + 1;
                }
                uint frTable = (uint)fs.Align(512, 0);
                using (MemoryStream ms = new MemoryStream())
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    foreach (var stringObject in multilingualUnicodeStringsContainer.Fr)
                        writer.WriteUTF8NullTerminated(stringObject.Value);
                    multilingualUnicodeStringsContainer.FrSize = (int)ms.Length;
                    mapWriter.Write(ms.ToArray());
                }

                offset = 0;
                uint esIndex = (uint)fs.Align(512, 0);
                for (int i = 0; i < multilingualUnicodeStringsContainer.Es.Count; i++)
                {
                    var stringObject = multilingualUnicodeStringsContainer.Es[i];
                    mapWriter.Write(StringId.FromString(stringObject.ID, stringsList.IndexOf(stringObject.ID)));
                    mapWriter.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringObject.Value) + 1;
                }
                uint esTable = (uint)fs.Align(512, 0);
                using (MemoryStream ms = new MemoryStream())
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    foreach (var stringObject in multilingualUnicodeStringsContainer.Es)
                        writer.WriteUTF8NullTerminated(stringObject.Value);
                    multilingualUnicodeStringsContainer.EsSize = (int)ms.Length;
                    mapWriter.Write(ms.ToArray());
                }

                offset = 0;
                uint itIndex = (uint)fs.Align(512, 0);
                for (int i = 0; i < multilingualUnicodeStringsContainer.It.Count; i++)
                {
                    var stringObject = multilingualUnicodeStringsContainer.It[i];
                    mapWriter.Write(StringId.FromString(stringObject.ID, stringsList.IndexOf(stringObject.ID)));
                    mapWriter.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringObject.Value) + 1;
                }
                uint itTable = (uint)fs.Align(512, 0);
                using (MemoryStream ms = new MemoryStream())
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    foreach (var stringObject in multilingualUnicodeStringsContainer.It)
                        writer.WriteUTF8NullTerminated(stringObject.Value);
                    multilingualUnicodeStringsContainer.ItSize = (int)ms.Length;
                    mapWriter.Write(ms.ToArray());
                }

                offset = 0;
                uint krIndex = (uint)fs.Align(512, 0);
                for (int i = 0; i < multilingualUnicodeStringsContainer.Kr.Count; i++)
                {
                    var stringObject = multilingualUnicodeStringsContainer.Kr[i];
                    mapWriter.Write(StringId.FromString(stringObject.ID, stringsList.IndexOf(stringObject.ID)));
                    mapWriter.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringObject.Value) + 1;
                }
                uint krTable = (uint)fs.Align(512, 0);
                using (MemoryStream ms = new MemoryStream())
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    foreach (var stringObject in multilingualUnicodeStringsContainer.Kr)
                        writer.WriteUTF8NullTerminated(stringObject.Value);
                    multilingualUnicodeStringsContainer.KrSize = (int)ms.Length;
                    mapWriter.Write(ms.ToArray());
                }

                offset = 0;
                uint zhIndex = (uint)fs.Align(512, 0);
                for (int i = 0; i < multilingualUnicodeStringsContainer.Zh.Count; i++)
                {
                    var stringObject = multilingualUnicodeStringsContainer.Zh[i];
                    mapWriter.Write(StringId.FromString(stringObject.ID, stringsList.IndexOf(stringObject.ID)));
                    mapWriter.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringObject.Value) + 1;
                }
                uint zhTable = (uint)fs.Align(512, 0);
                using (MemoryStream ms = new MemoryStream())
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    foreach (var stringObject in multilingualUnicodeStringsContainer.Zh)
                        writer.WriteUTF8NullTerminated(stringObject.Value);
                    multilingualUnicodeStringsContainer.ZhSize = (int)ms.Length;
                    mapWriter.Write(ms.ToArray());
                }

                offset = 0;
                uint prIndex = (uint)fs.Align(512, 0);
                for (int i = 0; i < multilingualUnicodeStringsContainer.Pr.Count; i++)
                {
                    var stringObject = multilingualUnicodeStringsContainer.Pr[i];
                    mapWriter.Write(StringId.FromString(stringObject.ID, stringsList.IndexOf(stringObject.ID)));
                    mapWriter.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringObject.Value) + 1;
                }
                uint prTable = (uint)fs.Align(512, 0);
                using (MemoryStream ms = new MemoryStream())
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    foreach (var stringObject in multilingualUnicodeStringsContainer.Pr)
                        writer.WriteUTF8NullTerminated(stringObject.Value);
                    multilingualUnicodeStringsContainer.PrSize = (int)ms.Length;
                    mapWriter.Write(ms.ToArray());
                }

                Console.WriteLine("Writing crazy...");
                header.CrazyOffset = (uint)fs.Align(512, 0);
                using (MemoryStream ms = new MemoryStream())
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    string credits = string.Format(Properties.Resources.Credits, DateTime.Now.ToLongDateString());
                    writer.WriteUTF8(credits);
                    header.CrazyLength = (uint)ms.Length;
                    mapWriter.Write(ms.ToArray());
                };

                foreach (AbideTagGroupFile tagGroupFile in references.Where(r => r.TagGroup.GroupTag == HaloTags.bitm))  //bitmap
                    TagGroupFile_WriteRaws(tagGroupFile, fs, mapWriter);

                Console.WriteLine("Writing index...");
                tagDataAddress = bspAddress + bspLength;
                using (VirtualStream tagDataStream = new VirtualStream(tagDataAddress))
                using (BinaryWriter writer = new BinaryWriter(tagDataStream))
                {
                    for (int i = 0; i < references.Length; i++)
                    {
                        AbideTagGroupFile tagGroupFile = references[i];
                        objects[i] = new ObjectEntry() { Id = references[i].Id, Tag = references[i].TagGroup.GroupTag };
                        if (objects[i].Tag == HaloTags.sbsp || objects[i].Tag == HaloTags.ltmp) continue;

                        using (VirtualStream tagStream = new VirtualStream(tagDataStream.Position))
                        using (BinaryWriter tagWriter = tagStream.CreateWriter())
                        {
                            tagGroupFile.TagGroup.Write(tagWriter);
                            objects[i].Size = (uint)tagStream.Length;
                            objects[i].Offset = (uint)tagStream.BaseAddress;

                            writer.Write(tagStream.ToArray());
                        }
                    }

                    tagDataStream.Align(4096);
                    tagDataLength = tagDataStream.Length;

                    using (VirtualStream indexStream = new VirtualStream(Index.IndexVirtualAddress, new byte[indexLength]))
                    using (BinaryWriter indexWriter = indexStream.CreateWriter())
                    {
                        indexWriter.Write(index);

                        foreach (TagHierarchy tag in tags)
                            indexWriter.Write(tag);

                        foreach (ObjectEntry objectEntry in objects)
                            indexWriter.Write(objectEntry);

                        header.IndexOffset = (uint)fs.Align(512, 0);
                        header.MapDataLength = (uint)(indexLength + bspLength + tagDataLength);
                        header.TagDataLength = (uint)tagDataLength;

                        mapWriter.Write(indexStream.ToArray());
                    }

                    Console.WriteLine("Writing tag data...");
                    mapWriter.Write(tagDataStream.ToArray());
                }

                fs.Align(1024);
                fs.Seek(header.IndexOffset + header.IndexLength + 400, SeekOrigin.Begin);

                mapWriter.Write(multilingualUnicodeStringsContainer.En.Count);
                mapWriter.Write(multilingualUnicodeStringsContainer.EnSize);
                mapWriter.Write(enIndex);
                mapWriter.Write(enTable);

                fs.Seek(12, SeekOrigin.Current);
                mapWriter.Write(multilingualUnicodeStringsContainer.Jp.Count);
                mapWriter.Write(multilingualUnicodeStringsContainer.JpSize);
                mapWriter.Write(jpIndex);
                mapWriter.Write(jpTable);

                fs.Seek(12, SeekOrigin.Current);
                mapWriter.Write(multilingualUnicodeStringsContainer.Nl.Count);
                mapWriter.Write(multilingualUnicodeStringsContainer.NlSize);
                mapWriter.Write(nlIndex);
                mapWriter.Write(nlTable);

                fs.Seek(12, SeekOrigin.Current);
                mapWriter.Write(multilingualUnicodeStringsContainer.Fr.Count);
                mapWriter.Write(multilingualUnicodeStringsContainer.FrSize);
                mapWriter.Write(frIndex);
                mapWriter.Write(frTable);

                fs.Seek(12, SeekOrigin.Current);
                mapWriter.Write(multilingualUnicodeStringsContainer.Es.Count);
                mapWriter.Write(multilingualUnicodeStringsContainer.EsSize);
                mapWriter.Write(esIndex);
                mapWriter.Write(esTable);

                fs.Seek(12, SeekOrigin.Current);
                mapWriter.Write(multilingualUnicodeStringsContainer.It.Count);
                mapWriter.Write(multilingualUnicodeStringsContainer.ItSize);
                mapWriter.Write(itIndex);
                mapWriter.Write(itTable);

                fs.Seek(12, SeekOrigin.Current);
                mapWriter.Write(multilingualUnicodeStringsContainer.Kr.Count);
                mapWriter.Write(multilingualUnicodeStringsContainer.KrSize);
                mapWriter.Write(krIndex);
                mapWriter.Write(krTable);

                fs.Seek(12, SeekOrigin.Current);
                mapWriter.Write(multilingualUnicodeStringsContainer.Zh.Count);
                mapWriter.Write(multilingualUnicodeStringsContainer.ZhSize);
                mapWriter.Write(zhIndex);
                mapWriter.Write(zhTable);

                fs.Seek(12, SeekOrigin.Current);
                mapWriter.Write(multilingualUnicodeStringsContainer.Pr.Count);
                mapWriter.Write(multilingualUnicodeStringsContainer.PrSize);
                mapWriter.Write(prIndex);
                mapWriter.Write(prTable);

                Console.WriteLine("Caculationg checksum...");
                header.FileLength = (uint)fs.Length;
                header.Checksum = 0;
                fs.Seek(2048, SeekOrigin.Begin);
                for (int i = 0; i < (header.FileLength - 2048) / 4; i++)
                    header.Checksum ^= mapReader.ReadUInt32();

                Console.WriteLine("Writing header...");
                fs.Seek(0, SeekOrigin.Begin);
                mapWriter.Write(header);
            }
        }
        private void Tag_Process(AbideTagGroupFile file)
        {
            //Process
            Tag_Process(file.Id, file.TagGroup);
        }
        private void Tag_Process(TagId ownerId, Group tagGroup)
        {
            //Loop
            foreach (Block tagBlock in tagGroup)
                Tag_Process(ownerId, tagBlock);
        }
        private void Tag_Process(TagId ownerId, Block tagBlock)
        {
            //Prepare
            Field field = null;
            string valueString = null;

            //Loop
            for (int i = 0; i < tagBlock.Fields.Count; i++)
            {
                //Prepare
                valueString = tagBlock.Fields[i].Value?.ToString() ?? string.Empty;
                field = tagBlock.Fields[i];

                //Handle field type
                switch (field.Type)
                {
                    case FieldType.FieldBlock:
                        foreach (Block nestedTagBlock in field.GetBlockList())
                            Tag_Process(ownerId, nestedTagBlock);
                        break;

                    case FieldType.FieldStruct:
                        Tag_Process(ownerId, ((StructField)field).GetStruct());
                        break;

                    case FieldType.FieldStringId:
                        Tag.Cache.StringIdField stringIdField = new Tag.Cache.StringIdField(field.GetName());
                        if (!string.IsNullOrEmpty(valueString))
                            stringIdField.Value = StringId.FromString(valueString, stringsList.IndexOf(valueString));
                        tagBlock.Fields[i] = stringIdField;
                        break;
                    case FieldType.FieldOldStringId:
                        Tag.Cache.OldStringIdField oldStringIdField = new Tag.Cache.OldStringIdField(field.GetName());
                        if (!string.IsNullOrEmpty(valueString))
                            oldStringIdField.Value = StringId.FromString(valueString, stringsList.IndexOf(valueString));
                        tagBlock.Fields[i] = oldStringIdField;
                        break;

                    case FieldType.FieldTagReference:
                        Tag.Cache.TagReferenceField tagReferenceField = new Tag.Cache.TagReferenceField(field.GetName(), field.GetGroupTag());
                        TagReference tagReference = new TagReference() { Tag = tagReferenceField.GroupTag, Id = TagId.Null };
                        if (!string.IsNullOrEmpty(valueString) && tagResources.ContainsKey(valueString))
                        {
                            var resource = tagResources[valueString];
                            tagReference.Id = resource.Id;
                        }

                        tagReferenceField.Value = tagReference;
                        tagBlock.Fields[i] = tagReferenceField;
                        break;
                    case FieldType.FieldTagIndex:
                        Tag.Cache.TagIndexField tagIndexField = new Tag.Cache.TagIndexField(field.GetName());
                        if (!string.IsNullOrEmpty(valueString) && tagResources.ContainsKey(valueString))
                        {
                            var resource = tagResources[valueString];
                            tagIndexField.Value = resource.Id;
                        }

                        tagBlock.Fields[i] = tagIndexField;
                        break;
                }
            }

            //Check tag block name
            switch (tagBlock.BlockName)
            {
                case "global_geometry_block_info_struct_block":
                    tagBlock.Fields[6].Value = ownerId;     //self reference
                    break;

                case "bitmap_data_block":
                    tagBlock.Fields[16].Value = ownerId;    //self reference
                    break;

                case "scenario_block":
                    Block tableBlock = null;
                    BlockList simulationDefinitionTableBlockList = tagBlock.Fields[143].GetBlockList();
                    simulationDefinitionTableBlockList.Clear();
                    foreach (KeyValuePair<string, AbideTagGroupFile> tagResource in tagResources)
                    {
                        switch (tagResource.Value.TagGroup.GroupTag)
                        {
                            case "bipd":
                            case "bloc":
                            case "ctrl":
                            case "jpt!":
                            case "mach":
                            case "scen":
                            case "ssce":
                            case "vehi":
                                tableBlock = new Tag.Cache.Generated.ScenarioSimulationDefinitionTableBlock();
                                tableBlock.Fields[0].Value = tagResource.Value.Id;
                                simulationDefinitionTableBlockList.Add(tableBlock);
                                break;
                            case "eqip":
                            case "garb":
                            case "proj":
                                tableBlock = new Tag.Cache.Generated.ScenarioSimulationDefinitionTableBlock();
                                tableBlock.Fields[0].Value = tagResource.Value.Id;
                                simulationDefinitionTableBlockList.Add(tableBlock);
                                tableBlock = new Tag.Cache.Generated.ScenarioSimulationDefinitionTableBlock();
                                tableBlock.Fields[0].Value = tagResource.Value.Id;
                                simulationDefinitionTableBlockList.Add(tableBlock);
                                break;
                            case "weap":
                                tableBlock = new Tag.Cache.Generated.ScenarioSimulationDefinitionTableBlock();
                                tableBlock.Fields[0].Value = tagResource.Value.Id;
                                simulationDefinitionTableBlockList.Add(tableBlock);
                                tableBlock = new Tag.Cache.Generated.ScenarioSimulationDefinitionTableBlock();
                                tableBlock.Fields[0].Value = tagResource.Value.Id;
                                simulationDefinitionTableBlockList.Add(tableBlock);
                                tableBlock = new Tag.Cache.Generated.ScenarioSimulationDefinitionTableBlock();
                                tableBlock.Fields[0].Value = tagResource.Value.Id;
                                simulationDefinitionTableBlockList.Add(tableBlock);
                                break;
                        }
                    }
                    break;
            }

            //TODO: I'm sure there are other blocks that possibly need some post-processing (think weapons and other tags with predicted resources)
        }
        private void TagResources_Discover(Group tagGroup)
        {
            //Loop
            foreach (Block tagBlock in tagGroup)
                TagResources_Discover(tagBlock);
        }
        private void TagResources_Discover(Block tagBlock)
        {
            //Prepare
            string valueString = null;
            string tagFileName = null;
            AbideTagGroupFile tagFile = null;

            //Loop through fields
            foreach (Field field in tagBlock.Fields)
            {
                //Get value
                valueString = field.Value?.ToString() ?? string.Empty;

                //Handle field type
                switch (field.Type)
                {
                    case FieldType.FieldBlock:
                        foreach (Block nestedTagBlock in field.GetBlockList())
                            TagResources_Discover(nestedTagBlock);
                        break;

                    case FieldType.FieldStruct:
                        TagResources_Discover(field.GetStruct());
                        break;

                    case FieldType.FieldStringId:
                    case FieldType.FieldOldStringId:
                        if (!stringsList.Contains(valueString))
                            stringsList.Add(valueString);
                        break;

                    case FieldType.FieldTagReference:
                    case FieldType.FieldTagIndex:
                        if (!string.IsNullOrEmpty(valueString))
                        {
                            //Get file name
                            tagFileName = Path.Combine(workspaceDirectory, "tags", valueString);

                            //Check
                            if (!tagResources.ContainsKey(valueString))
                            {
                                //Check
                                if (!File.Exists(tagFileName))
                                {
                                    //TODO: load tag from resource map if it exists
                                    throw new NotImplementedException();
                                }
                                else
                                {
                                    //Add
                                    tagResources.Add(valueString, new AbideTagGroupFile() { Id = currentId++ });
                                    tagFile = tagResources[valueString];

                                    //Load
                                    tagFile.Load(tagFileName);

                                    //Build resources
                                    TagResources_Discover(tagFile.TagGroup);
                                }
                            }
                        }
                        break;
                }
            }
        }
        private void SoundCacheFileGestalt_Prepare()
        {
            //Setup
            SoundCacheFileGestaltFile.TagGroup = new Tag.Guerilla.Generated.SoundCacheFileGestalt();

            //Add default playback
            Block playbackTagBlock = ((BlockField)SoundCacheFileGestaltFile.TagGroup.TagBlocks[0].Fields[0]).Add(out bool success);
            if (success)
            {
                Block playbackParametersStructBlock = (Block)playbackTagBlock.Fields[0].Value;
                playbackParametersStructBlock.Fields[9].Value = (float)Math.PI;
                playbackParametersStructBlock.Fields[10].Value = (float)Math.PI;
            }

            //Add default scale
            Block scaleTagBlock = ((BlockField)SoundCacheFileGestaltFile.TagGroup.TagBlocks[0].Fields[1]).Add(out success);
            if (success)
            {
                Block scaleModifiersStructBlock = (Block)scaleTagBlock.Fields[0].Value;
                scaleModifiersStructBlock.Fields[3].Value = new FloatBounds(1f, 1f);
            }

            //Add flags
            for (int i = 0; i < 686; i++)
            {
                Block runtimePermutationBitVector = ((BlockField)SoundCacheFileGestaltFile.TagGroup.TagBlocks[0].Fields[7]).Add(out success);
                if (success) runtimePermutationBitVector.Fields[0].Value = (byte)0;
            }

            //Add
            tagResources.Add($"i've got a lovely bunch of coconuts.sound_cache_file_gestalt", SoundCacheFileGestaltFile);
        }
        private void Globals_Prepare()
        {
            switch (mapType)
            {
                case 1:
                    resourceMapFile = new HaloMap(RegistrySettings.SharedFileName);
                    using (Stream stream = SharedResources.GetMultiplayerSharedGlobals())
                        GlobalsFile.Load(stream);
                    using (Stream stream = SharedResources.GetMultiplayerGlobals(true))
                        MultiplayerGlobalsFile.Load(stream);
                    break;

                default: throw new Exception("Unable to compile map of specified type.");
            }

            using (Stream stream = SharedResources.GetSoundClasses())
                SoundClassesFile.Load(stream);

            using (Stream stream = SharedResources.GetCombatDialogueConstants())
                CombatDialogueConstantFile.Load(stream);

            tagResources.Add(@"globals\globals.globals", GlobalsFile);
            GlobalsFile.Id = currentId++;
            tagResources.Add(@"sound\sound_classes.sound_classes", SoundClassesFile);
            SoundClassesFile.Id = currentId++;
            tagResources.Add(@"sound\combat_dialogue_constants.sound_dialogue_constants", CombatDialogueConstantFile);
            CombatDialogueConstantFile.Id = currentId++;
            tagResources.Add(@"multiplayer\multiplayer_globals.multiplayer_globals", MultiplayerGlobalsFile);
        }
        private void Scenario_Prepare()
        {
            //Add
            tagResources.Add($"{ScenarioPath}.{ScenarioFile.TagGroup.GroupName}", ScenarioFile);
            ScenarioFile.Id = currentId++;
        }
        private void MultilingualUnicodeStringList_Process(AbideTagGroupFile tagGroupFile)
        {
            //Prepare
            StringContainer strings = new StringContainer();
            byte[] stringData = tagGroupFile.TagGroup.TagBlocks[0].Fields[1].GetData();
            string unicodeString = string.Empty;
            string stringId = string.Empty;
            int offset = 0;

            //Read strings
            using (MemoryStream ms = new MemoryStream(stringData))
            using (BinaryReader reader = new BinaryReader(ms))
            {
                //Loop
                foreach (Block stringReferenceBlock in tagGroupFile.TagGroup.TagBlocks[0].Fields[0].GetBlockList())
                {
                    //Get string ID
                    stringId = (string)stringReferenceBlock.Fields[0].Value;

                    //Add string
                    if (!stringsList.Contains(stringId))
                        stringsList.Add(stringId);

                    //Goto English
                    offset = (int)stringReferenceBlock.Fields[1].Value;
                    if (offset >= 0)
                    {
                        ms.Seek(offset, SeekOrigin.Begin);
                        unicodeString = reader.ReadUTF8NullTerminated();
                        strings.English.Add(new StringEntry(unicodeString, stringId));
                    }

                    //Goto Japanese
                    offset = (int)stringReferenceBlock.Fields[2].Value;
                    if (offset >= 0)
                    {
                        ms.Seek(offset, SeekOrigin.Begin);
                        unicodeString = reader.ReadUTF8NullTerminated();
                        strings.Japanese.Add(new StringEntry(unicodeString, stringId));
                    }

                    //Goto German
                    offset = (int)stringReferenceBlock.Fields[3].Value;
                    if (offset >= 0)
                    {
                        ms.Seek(offset, SeekOrigin.Begin);
                        unicodeString = reader.ReadUTF8NullTerminated();
                        strings.German.Add(new StringEntry(unicodeString, stringId));
                    }

                    //Goto French
                    offset = (int)stringReferenceBlock.Fields[4].Value;
                    if (offset >= 0)
                    {
                        ms.Seek(offset, SeekOrigin.Begin);
                        unicodeString = reader.ReadUTF8NullTerminated();
                        strings.French.Add(new StringEntry(unicodeString, stringId));
                    }

                    //Goto Spanish
                    offset = (int)stringReferenceBlock.Fields[5].Value;
                    if (offset >= 0)
                    {
                        ms.Seek(offset, SeekOrigin.Begin);
                        unicodeString = reader.ReadUTF8NullTerminated();
                        strings.Spanish.Add(new StringEntry(unicodeString, stringId));
                    }

                    //Goto Italian
                    offset = (int)stringReferenceBlock.Fields[6].Value;
                    if (offset >= 0)
                    {
                        ms.Seek(offset, SeekOrigin.Begin);
                        unicodeString = reader.ReadUTF8NullTerminated();
                        strings.Italian.Add(new StringEntry(unicodeString, stringId));
                    }

                    //Goto Korean
                    offset = (int)stringReferenceBlock.Fields[7].Value;
                    if (offset >= 0)
                    {
                        ms.Seek(offset, SeekOrigin.Begin);
                        unicodeString = reader.ReadUTF8NullTerminated();
                        strings.Korean.Add(new StringEntry(unicodeString, stringId));
                    }

                    //Goto Chinese
                    offset = (int)stringReferenceBlock.Fields[8].Value;
                    if (offset >= 0)
                    {
                        ms.Seek(offset, SeekOrigin.Begin);
                        unicodeString = reader.ReadUTF8NullTerminated();
                        strings.Chinese.Add(new StringEntry(unicodeString, stringId));
                    }

                    //Goto Portuguese
                    offset = (int)stringReferenceBlock.Fields[9].Value;
                    if (offset >= 0)
                    {
                        ms.Seek(offset, SeekOrigin.Begin);
                        unicodeString = reader.ReadUTF8NullTerminated();
                        strings.Portuguese.Add(new StringEntry(unicodeString, stringId));
                    }
                }
            }

            //Process tag
            tagGroupFile.TagGroup = new Tag.Cache.Generated.MultilingualUnicodeStringList();
            byte[] padding = (byte[])((PadField)tagGroupFile.TagGroup.TagBlocks[0].Fields[2]).Value;
            using (MemoryStream ms = new MemoryStream(padding))
            using (BinaryWriter writer = new BinaryWriter(ms))
            {
                //Write English
                writer.Write((ushort)multilingualUnicodeStringsContainer.En.Count);
                writer.Write((ushort)strings.English.Count);
                multilingualUnicodeStringsContainer.En.AddRange(strings.English);

                //Write Japanese
                writer.Write((ushort)multilingualUnicodeStringsContainer.Jp.Count);
                writer.Write((ushort)strings.Japanese.Count);
                multilingualUnicodeStringsContainer.Jp.AddRange(strings.Japanese);

                //Write German
                writer.Write((ushort)multilingualUnicodeStringsContainer.Nl.Count);
                writer.Write((ushort)strings.German.Count);
                multilingualUnicodeStringsContainer.Nl.AddRange(strings.German);

                //Write French
                writer.Write((ushort)multilingualUnicodeStringsContainer.Fr.Count);
                writer.Write((ushort)strings.French.Count);
                multilingualUnicodeStringsContainer.Fr.AddRange(strings.French);

                //Write Spanish
                writer.Write((ushort)multilingualUnicodeStringsContainer.Es.Count);
                writer.Write((ushort)strings.Spanish.Count);
                multilingualUnicodeStringsContainer.Es.AddRange(strings.Spanish);

                //Write Italian
                writer.Write((ushort)multilingualUnicodeStringsContainer.It.Count);
                writer.Write((ushort)strings.Italian.Count);
                multilingualUnicodeStringsContainer.It.AddRange(strings.Italian);

                //Write Korean
                writer.Write((ushort)multilingualUnicodeStringsContainer.Kr.Count);
                writer.Write((ushort)strings.Korean.Count);
                multilingualUnicodeStringsContainer.Kr.AddRange(strings.Korean);

                //Write Chinese
                writer.Write((ushort)multilingualUnicodeStringsContainer.Zh.Count);
                writer.Write((ushort)strings.Chinese.Count);
                multilingualUnicodeStringsContainer.Zh.AddRange(strings.Chinese);

                //Write Portuguese
                writer.Write((ushort)multilingualUnicodeStringsContainer.Pr.Count);
                writer.Write((ushort)strings.Portuguese.Count);
                multilingualUnicodeStringsContainer.Pr.AddRange(strings.Portuguese);
            }
        }
        private void Sound_Process(AbideTagGroupFile tagGroupFile)
        {
            //Prepare
            Group soundCacheFileGestalt = SoundCacheFileGestaltFile.TagGroup;
            Group cacheFileSound = new Tag.Cache.Generated.Sound();
            Group sound = tagGroupFile.TagGroup;
            bool success = false;
            int index = 0;

            //Get tag blocks
            Block soundCacheFileGestaltBlock = soundCacheFileGestalt.TagBlocks[0];
            Block cacheFileSoundBlock = cacheFileSound.TagBlocks[0];
            Block soundBlock = sound.TagBlocks[0];

            //Transfer raws
            foreach (int rawOffset in tagGroupFile.GetRawOffsets())
            {
                SoundCacheFileGestaltFile.SetRaw(rawOffset, tagGroupFile.GetRaw(rawOffset));
            }

            //Get block fields from sound cache file gestalt
            BlockField playbacks = (BlockField)soundCacheFileGestaltBlock.Fields[0];
            BlockField scales = (BlockField)soundCacheFileGestaltBlock.Fields[1];
            BlockField importNames = (BlockField)soundCacheFileGestaltBlock.Fields[2];
            BlockField pitchRangeParameters = (BlockField)soundCacheFileGestaltBlock.Fields[3];
            BlockField pitchRanges = (BlockField)soundCacheFileGestaltBlock.Fields[4];
            BlockField permutations = (BlockField)soundCacheFileGestaltBlock.Fields[5];
            BlockField customPlaybacks = (BlockField)soundCacheFileGestaltBlock.Fields[6];
            BlockField runtimePermutationFlags = (BlockField)soundCacheFileGestaltBlock.Fields[7];
            BlockField chunks = (BlockField)soundCacheFileGestaltBlock.Fields[8];
            BlockField promotions = (BlockField)soundCacheFileGestaltBlock.Fields[9];
            BlockField extraInfos = (BlockField)soundCacheFileGestaltBlock.Fields[10];

            //Change
            tagGroupFile.TagGroup = cacheFileSound;

            //Convert fields
            cacheFileSoundBlock.Fields[0].Value = (short)(int)soundBlock.Fields[0].Value;   //flags
            cacheFileSoundBlock.Fields[1].Value = soundBlock.Fields[1].Value;               //class
            cacheFileSoundBlock.Fields[2].Value = soundBlock.Fields[2].Value;               //sample rate
            cacheFileSoundBlock.Fields[3].Value = soundBlock.Fields[9].Value;               //encoding
            cacheFileSoundBlock.Fields[4].Value = soundBlock.Fields[10].Value;              //compression

            //Read 'extra' data that I chose to store in a pad field
            using (MemoryStream ms = new MemoryStream((byte[])soundBlock.Fields[12].Value))
            using (BinaryReader reader = new BinaryReader(ms))
            {
                int maxPlaybackTime = reader.ReadInt32();  //max playback time
                if (reader.ReadByte() != NullByte)
                    cacheFileSoundBlock.Fields[12].Value = maxPlaybackTime;
            }

            //Add or get playback index
            cacheFileSoundBlock.Fields[5].Value = (short)SoundGestalt_FindPlaybackIndex((Block)soundBlock.Fields[5].Value);

            //Add scale
            cacheFileSoundBlock.Fields[8].Value = (byte)(sbyte)SoundGestalt_FindScaleIndex((Block)soundBlock.Fields[6].Value);

            //Add promotion
            Block soundPromotionParametersStruct = (Block)soundBlock.Fields[11].Value;
            if (soundPromotionParametersStruct.Fields[0].GetBlockList().Count > 0)
                cacheFileSoundBlock.Fields[9].Value = (byte)(sbyte)SoundGestalt_FindPromotionIndex((Block)soundBlock.Fields[11].Value);
            else cacheFileSoundBlock.Fields[9].Value = NullByte;

            //Add custom playback
            if (soundBlock.Fields[14].GetBlockList().Count > 0)
            {
                index = customPlaybacks.BlockList.Count;
                Block customPlayback = customPlaybacks.Add(out success);
                if (success)
                {
                    cacheFileSoundBlock.Fields[10].Value = (byte)index;
                    customPlayback.Fields[0].Value = (ITagBlock)soundBlock.Fields[14].GetBlockList()[0].Fields[0].Value;
                }
                else cacheFileSoundBlock.Fields[10].Value = NullByte;
            }
            else cacheFileSoundBlock.Fields[10].Value = NullByte;

            //Add extra info
            if (soundBlock.Fields[15].GetBlockList().Count > 0)
            {
                index = extraInfos.BlockList.Count;
                Block soundExtraInfo = soundBlock.Fields[15].GetBlockList()[0];
                Block extraInfo = extraInfos.Add(out success);
                if (success)
                {
                    cacheFileSoundBlock.Fields[11].Value = (short)index;
                    extraInfo.Fields[1].Value = soundExtraInfo.Fields[2].Value;
                    ((Block)extraInfo.Fields[1].Value).Fields[6].Value = new StringValue($"i've got a lovely bunch of coconuts.sound_cache_file_gestalt");
                    foreach (Block block in soundExtraInfo.Fields[1].GetBlockList())
                        soundExtraInfo.Fields[1].GetBlockList().Add(block);
                }
                else cacheFileSoundBlock.Fields[11].Value = NullShort;
            }
            else cacheFileSoundBlock.Fields[11].Value = NullShort;

            //Add pitch range
            cacheFileSoundBlock.Fields[7].Value = (byte)((BlockField)soundBlock.Fields[13]).BlockList.Count;
            foreach (var soundPitchRange in ((BlockField)soundBlock.Fields[13]).BlockList)
            {
                index = pitchRanges.BlockList.Count;
                Block gestaltPitchRange = pitchRanges.Add(out success);
                if (success)
                {
                    //Set pitch range
                    cacheFileSoundBlock.Fields[6].Value = (short)index;

                    //Add import name
                    gestaltPitchRange.Fields[0].Value = (short)SoundGestalt_FindImportNameIndex((string)soundPitchRange.Fields[0].Value);

                    //Add pitch range parameter
                    gestaltPitchRange.Fields[1].Value = (short)SoundGestalt_FindPitchRangeParameter((short)soundPitchRange.Fields[2].Value,
                        (ShortBounds)soundPitchRange.Fields[4].Value, (ShortBounds)soundPitchRange.Fields[5].Value);

                    //Add permutation
                    gestaltPitchRange.Fields[4].Value = (short)permutations.BlockList.Count;
                    gestaltPitchRange.Fields[5].Value = (short)((BlockField)soundPitchRange.Fields[7]).BlockList.Count;

                    //Loop
                    foreach (Block soundPermutation in ((BlockField)soundPitchRange.Fields[7]).BlockList)
                    {
                        Block gestaltPermutation = permutations.Add(out success);
                        if (success)
                        {
                            //Add import name
                            gestaltPermutation.Fields[0].Value = (short)SoundGestalt_FindImportNameIndex((string)soundPermutation.Fields[0].Value);

                            //Convert fields
                            gestaltPermutation.Fields[1].Value = (short)((float)soundPermutation.Fields[1].Value * 65535f);
                            gestaltPermutation.Fields[2].Value = (byte)((float)soundPermutation.Fields[2].Value * 255f);
                            gestaltPermutation.Fields[3].Value = (byte)(sbyte)(short)soundPermutation.Fields[4].Value;
                            gestaltPermutation.Fields[4].Value = (short)soundPermutation.Fields[5].Value;
                            gestaltPermutation.Fields[5].Value = (int)soundPermutation.Fields[3].Value;

                            //Add chunks
                            gestaltPermutation.Fields[6].Value = (short)chunks.BlockList.Count;
                            gestaltPermutation.Fields[7].Value = (short)((BlockField)soundPermutation.Fields[6]).BlockList.Count;

                            //Loop
                            foreach (Block soundChunk in ((BlockField)soundPermutation.Fields[6]).BlockList)
                            {
                                chunks.BlockList.Add(soundChunk);
                                int offset = (int)soundChunk.Fields[0].Value;
                                if ((offset & 0xc0000000) == 0 && tagGroupFile.GetRaw(offset) == null) System.Diagnostics.Debugger.Break();
                            }
                        }
                        else
                        {
                            gestaltPitchRange.Fields[4].Value = NullShort;
                            gestaltPitchRange.Fields[5].Value = (short)0;
                        }
                    }
                }
                else
                {
                    cacheFileSoundBlock.Fields[6].Value = NullShort;
                    cacheFileSoundBlock.Fields[7].Value = NullByte;
                }
            }
        }
        private int SoundGestalt_FindPitchRangeParameter(short s1, ShortBounds sb1, ShortBounds sb2)
        {
            //Prepare
            Block soundCacheFileGestaltBlock = SoundCacheFileGestaltFile.TagGroup.TagBlocks[0];
            BlockField blockField = (BlockField)soundCacheFileGestaltBlock.Fields[3];
            int index = -1;

            //Check
            foreach (Block gestaltBlock in blockField.BlockList)
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
                Block gestaltBlock = blockField.Add(out bool success);
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
        private int SoundGestalt_FindImportNameIndex(string stringId)
        {
            //Prepare
            Block soundCacheFileGestaltBlock = SoundCacheFileGestaltFile.TagGroup.TagBlocks[0];
            BlockField blockField = (BlockField)soundCacheFileGestaltBlock.Fields[2];
            int index = -1;

            //Check
            foreach (Block gestaltBlock in blockField.BlockList)
            {
                if (((string)gestaltBlock.Fields[0].Value).Equals(stringId))
                {
                    index = blockField.BlockList.IndexOf(gestaltBlock);
                    break;
                }
            }

            //Add (or return -1)
            if (index == -1)
            {
                index = blockField.BlockList.Count;
                Block gestaltBlock = blockField.Add(out bool success);
                if (success) gestaltBlock.Fields[0].Value = stringId;
                else index = -1;
            }

            //return
            return index;
        }
        private int SoundGestalt_FindPromotionIndex(Block structBlock)
        {
            //Prepare
            Block soundCacheFileGestaltBlock = SoundCacheFileGestaltFile.TagGroup.TagBlocks[0];
            BlockField blockField = (BlockField)soundCacheFileGestaltBlock.Fields[9];
            int index = -1;

            //Check
            foreach (Block gestaltBlock in blockField.BlockList)
            {
                if (((Block)gestaltBlock.Fields[0].Value).Equals(structBlock))
                {
                    index = blockField.BlockList.IndexOf(gestaltBlock);
                    break;
                }
            }

            //Add (or return -1)
            if (index == -1)
            {
                index = blockField.BlockList.Count;
                Block gestaltBlock = blockField.Add(out bool success);
                if (success) gestaltBlock.Fields[0].Value = structBlock;
                else index = -1;
            }

            //return
            return index;
        }
        private int SoundGestalt_FindScaleIndex(Block structBlock)
        {
            //Prepare
            Block soundCacheFileGestaltBlock = SoundCacheFileGestaltFile.TagGroup.TagBlocks[0];
            BlockField blockField = (BlockField)soundCacheFileGestaltBlock.Fields[1];
            int index = -1;

            //Check
            foreach (Block gestaltBlock in blockField.BlockList)
            {
                if (((Block)gestaltBlock.Fields[0].Value).Equals(structBlock))
                {
                    index = blockField.BlockList.IndexOf(gestaltBlock);
                    break;
                }
            }

            //Add (or return -1)
            if (index == -1)
            {
                index = blockField.BlockList.Count;
                Block gestaltBlock = blockField.Add(out bool success);
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
        private int SoundGestalt_FindPlaybackIndex(Block structBlock)
        {
            //Prepare
            Block soundCacheFileGestaltBlock = SoundCacheFileGestaltFile.TagGroup.TagBlocks[0];
            BlockField blockField = (BlockField)soundCacheFileGestaltBlock.Fields[0];
            int index = -1;

            //Check
            foreach (Block gestaltBlock in blockField.BlockList)
            {
                if (((Block)gestaltBlock.Fields[0].Value).Equals(structBlock))
                {
                    index = blockField.BlockList.IndexOf(gestaltBlock);
                    break;
                }
            }

            //Add (or return -1)
            if (index == -1)
            {
                index = blockField.BlockList.Count;
                Block gestaltBlock = blockField.Add(out bool success);
                if (success) gestaltBlock.Fields[0].Value = structBlock;
                else index = -1;
            }

            //return
            return index;
        }
        private void TagGroupFile_WriteRaws(AbideTagGroupFile tagGroupFile, Stream mapFileStream, BinaryWriter mapFileWriter)
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
                        Console.WriteLine("Writing {0} data", tagGroupFile.TagGroup.GroupName);
                        tagStream.Seek(64, SeekOrigin.Begin);
                        TagBlock chunks = reader.Read<TagBlock>();
                        for (int i = 0; i < chunks.Count; i++)
                        {
                            tagStream.Seek(chunks.Offset + (i * 12), SeekOrigin.Begin);
                            offsetAddress = tagStream.Position;
                            rawAddress = reader.ReadUInt32();

                            if ((rawAddress & LocationMask) == 0)
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

                            if ((rawAddress & LocationMask) == 0)
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
                        Console.WriteLine("Writing {0} data", tagGroupFile.TagGroup.GroupName);
                        tagStream.Seek(36, SeekOrigin.Begin);
                        TagBlock sections = reader.Read<TagBlock>();
                        for (int i = 0; i < sections.Count; i++)
                        {
                            tagStream.Seek(sections.Offset + (i * 92) + 56, SeekOrigin.Begin);
                            offsetAddress = tagStream.Position;
                            rawAddress = reader.ReadUInt32();

                            if ((rawAddress & LocationMask) == 0)
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

                            if ((rawAddress & LocationMask) == 0)
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
                        Console.WriteLine("Writing {0} data", tagGroupFile.TagGroup.GroupName);
                        tagStream.Seek(0, SeekOrigin.Begin);
                        TagBlock particleSystem = reader.Read<TagBlock>();
                        for (int i = 0; i < particleSystem.Count; i++)
                        {
                            tagStream.Seek(particleSystem.Offset + (i * 140) + 64, SeekOrigin.Begin);
                            offsetAddress = tagStream.Position;
                            rawAddress = reader.ReadUInt32();

                            if ((rawAddress & LocationMask) == 0)
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
                        Console.WriteLine("Writing {0} data", tagGroupFile.TagGroup.GroupName);
                        tagStream.Seek(56, SeekOrigin.Begin);
                        offsetAddress = tagStream.Position;
                        rawAddress = reader.ReadUInt32();

                        if ((rawAddress & LocationMask) == 0)
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
                        Console.WriteLine("Writing {0} data", tagGroupFile.TagGroup.GroupName);
                        tagStream.Seek(160, SeekOrigin.Begin);
                        offsetAddress = tagStream.Position;
                        rawAddress = reader.ReadUInt32();

                        if ((rawAddress & LocationMask) == 0)
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
                        Console.WriteLine("Writing {0} data", tagGroupFile.TagGroup.GroupName);
                        tagStream.Seek(172, SeekOrigin.Begin);
                        TagBlock xboxAnimationData = reader.Read<TagBlock>();
                        for (int i = 0; i < xboxAnimationData.Count; i++)
                        {
                            tagStream.Seek(xboxAnimationData.Offset + (i * 20) + 8, SeekOrigin.Begin);
                            offsetAddress = tagStream.Position;
                            rawAddress = reader.ReadUInt32();

                            if ((rawAddress & LocationMask) == 0)
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
                        Console.WriteLine("Writing {0} data", tagGroupFile.TagGroup.GroupName);
                        tagStream.Seek(156, SeekOrigin.Begin);
                        TagBlock clusters = reader.Read<TagBlock>();
                        for (int i = 0; i < clusters.Count; i++)
                        {
                            tagStream.Seek(clusters.Offset + (i * 176) + 40, SeekOrigin.Begin);
                            offsetAddress = tagStream.Position;
                            rawAddress = reader.ReadUInt32();

                            if ((rawAddress & LocationMask) == 0)
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

                            if ((rawAddress & LocationMask) == 0)
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

                            if ((rawAddress & LocationMask) == 0)
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

                                if ((rawAddress & LocationMask) == 0)
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
                        Console.WriteLine("Writing {0} data", tagGroupFile.TagGroup.GroupName);
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

                                if ((rawAddress & LocationMask) == 0)
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

                                if ((rawAddress & LocationMask) == 0)
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

                                if ((rawAddress & LocationMask) == 0)
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
                        Console.WriteLine("Writing {0} data", tagGroupFile.TagGroup.GroupName);
                        tagStream.Seek(68, SeekOrigin.Begin);
                        TagBlock bitmapData = reader.Read<TagBlock>();
                        for (int i = 0; i < bitmapData.Count; i++)
                        {
                            tagStream.Seek(bitmapData.Offset + (i * 116) + 28, SeekOrigin.Begin);
                            offsetAddress = tagStream.Position;
                            rawAddress = reader.ReadUInt32();

                            if ((rawAddress & LocationMask) == 0)
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

                            if ((rawAddress & LocationMask) == 0)
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

                            if ((rawAddress & LocationMask) == 0)
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

                            if ((rawAddress & LocationMask) == 0)
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

                            if ((rawAddress & LocationMask) == 0)
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

                            if ((rawAddress & LocationMask) == 0)
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
        private void SoundTagGroupFile_WriteExtraInfoRaws(AbideTagGroupFile tagGroupFile, Stream mapFileStream, BinaryWriter mapFileWriter)
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

                            if ((rawAddress & LocationMask) == 0)
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
    }
}
