using Abide.Guerilla.Library;
using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2;
using Abide.HaloLibrary.Halo2.Retail;
using Abide.HaloLibrary.Halo2Map;
using Abide.HaloLibrary.IO;
using Abide.Tag;
using Abide.Tag.Cache;
using Abide.Tag.Cache.Generated;
using Abide.Wpf.Modules.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abide.Wpf.Modules.Operations
{
    public class HaloMapCompileOperation : BackgroundOperation
    {
        private const int NullIntIndex = -1;
        private const short NullShortIndex = -1;
        private const byte NullCharIndex = 255;

        private readonly StringList strings = new StringList(CommonStrings.GetCommonStrings().ToArray());
        private readonly TagList tagList = new TagList();
        private readonly string workspaceDirectory = RegistrySettings.WorkspaceDirectory;
        private readonly string scenarioFileName = string.Empty;

        private StringLocalizations stringLocalizations = new StringLocalizations();
        private Tag scenarioTag = null;
        private Tag globalsTag = null;
        private Tag soundClassesTag = null;
        private Tag combatDialogueConstantTag = null;
        private Tag soundCacheFileGestaltTag = null;
        private Tag multiplayerGlobalsTag = null;
        private short mapType = 0;

        public AbideTagGroupFile ScenarioFile { get; } = new AbideTagGroupFile();
        public AbideTagGroupFile GlobalsFile { get; } = new AbideTagGroupFile();
        public AbideTagGroupFile SoundClassesFile { get; } = new AbideTagGroupFile();
        public AbideTagGroupFile CombatDialogueConstantFile { get; } = new AbideTagGroupFile();
        public AbideTagGroupFile SoundCacheFileGestaltFile { get; } = new AbideTagGroupFile();
        public AbideTagGroupFile MultiplayerGlobalsFile { get; } = new AbideTagGroupFile();
        public string OutputMapFileName { get; private set; }
        public string ScenarioPath { get; private set; }
        public string MapName { get; private set; }

        public HaloMapCompileOperation(string path)
        {
            scenarioFileName = path;
        }
        protected override void OnBackground(object state)
        {
            ScenarioFile.Load(scenarioFileName);
            mapType = (short)ScenarioFile.TagGroup.TagBlocks[0].Fields[2].Value;

            ReportStatus("Finding tags...");
            SetProgressVisibility(false);

            switch (mapType)
            {
                case 0:
                    Campaign_Prepare();
                    break;

                case 1:
                    Multiplayer_Prepare();
                    break;

                default: throw new InvalidOperationException();
            }

            SoundCacheFileGestalt_Prepare();
            Scenario_Prepare();

            Discover(ScenarioFile.TagGroup);
            Discover(GlobalsFile.TagGroup);
            GlobalsFile.TagGroup.TagBlocks[0].Fields[25].Value = multiplayerGlobalsTag.TagName;
            this.tagList.Add(multiplayerGlobalsTag);
            Discover(MultiplayerGlobalsFile.TagGroup);
            this.tagList.Remove(soundCacheFileGestaltTag);
            this.tagList.Add(soundCacheFileGestaltTag);
            ResetProgress(this.tagList.Count);
            ReportStatus($"Processing {this.tagList.Count} tags...");
            SetProgressVisibility(true);

            var tagList = this.tagList.ToList();
            Parallel.ForEach(tagList, tag =>
            {
                TagGroup_ToCache(tag.File);
            });

            for (int i = 0; i < tagList.Count; i++)
            {
                ReportProgress(i);
                ProcessTag(tagList[i]);
                ReportStatus($"Processing {this.tagList.Count} tags... ({Math.Floor(100f * i / this.tagList.Count)}%)");
            }

            SetProgressVisibility(false);
            ReportStatus($"Writing map file: {OutputMapFileName}");
            string outputDirectory = Path.Combine(workspaceDirectory, "maps");
            if (!Directory.Exists(outputDirectory))
                Directory.CreateDirectory(outputDirectory);

            Header header = Header.CreateDefault();
            Index index = Index.CreateDefault();
            TagHierarchy[] tags = SharedResources.GetTags();
            ObjectEntry[] objects = new ObjectEntry[tagList.Count];
            long indexLength = 0, bspAddress = 0, bspLength = 0, tagDataAddress = 0, tagDataLength = 0;
            using (var fs = File.Create(OutputMapFileName))
            using (BinaryWriter mapWriter = new BinaryWriter(fs))
            using (BinaryReader mapReader = new BinaryReader(fs))
            {
                header.ScenarioPath = ScenarioPath;
                header.Name = MapName;

                using (var soundCache = new VirtualStream(Header.Length))
                {
                    foreach (var reference in tagList.Where(t => t.File.TagGroup.Tag == "ugh!"))
                        SoundCacheFileGestalt_WriteResources(reference.File, soundCache);

                    using (var geometryCache = new VirtualStream(soundCache.Position))
                    {
                        foreach (var reference in tagList.Where(t => t.File.TagGroup.Tag == "mode"))   //render model geometry
                            RenderModel_WriteResources(reference.File, geometryCache);
                        foreach (var reference in tagList.Where(t => t.File.TagGroup.Tag == "scnr"))   //scenario
                            Scenario_WriteZoneResources(reference.File, geometryCache);
                        foreach (var reference in tagList.Where(t => t.File.TagGroup.Tag == "weat"))   //weather system geometry
                            WeatherSystem_WriteResources(reference.File, geometryCache);
                        foreach (var reference in tagList.Where(t => t.File.TagGroup.Tag == "DECR"))   //decorator set geometry
                            DecoratorSet_WriteResources(reference.File, geometryCache);
                        foreach (var reference in tagList.Where(t => t.File.TagGroup.Tag == "PRTM"))   //particle model geometry
                            ParticleModel_WriteResources(reference.File, geometryCache);
                        foreach (var reference in tagList.Where(t => t.File.TagGroup.Tag == "ugh!"))   //sound gestalt extra info geometry
                            SoundCacheFileGestalt_WriteExtraInfoResources(reference.File, geometryCache);

                        using (var animationCache = new VirtualStream(geometryCache.Position))
                        {
                            foreach (var reference in tagList.Where(t => t.File.TagGroup.Tag == "jmad"))   //model animation
                                ModelAnimation_WriteResources(reference.File, animationCache);

                            fs.Seek(Header.Length, SeekOrigin.Begin);
                            mapWriter.Write(soundCache.ToArray());
                            mapWriter.Write(geometryCache.ToArray());
                            mapWriter.Write(animationCache.ToArray());
                        }
                    }
                }

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
                index.ScenarioId = scenarioTag.Id;
                index.GlobalsId = globalsTag.Id;
                index.ObjectCount = (uint)tagList.Count;
                index.Tags = "tags";

                foreach (Block structureBsp in ((BlockField)ScenarioFile.TagGroup.TagBlocks[0].Fields[68]).BlockList)
                {
                    StructureBspBlockHeader bspHeader = new StructureBspBlockHeader();
                    structureBsp.Fields[0] = new StructField<ScenarioStructureBspInfoStructBlock>(string.Empty);
                    Block scenarioStructureBspInfoStructBlock = (Block)structureBsp.Fields[0].Value;

                    bspAddress = Index.IndexVirtualAddress + indexLength;
                    TagReference structureBspTagReference = (TagReference)structureBsp.Fields[1].Value;
                    TagReference structureLightmapReference = (TagReference)structureBsp.Fields[2].Value;
                    using (VirtualStream bspDataStream = new VirtualStream(bspAddress))
                    using (BinaryWriter writer = bspDataStream.CreateWriter())
                    {
                        bspDataStream.Seek(StructureBspBlockHeader.Length, SeekOrigin.Current);
                        AbideTagGroupFile structureBspFile, structureLightmapFile;
                        if (tagList.Any(tag => tag.Id == structureBspTagReference.Id))
                        {
                            bspHeader.StructureBspOffset = (uint)bspDataStream.Position;
                            structureBspFile = tagList.First(tag => tag.Id == structureBspTagReference.Id).File;
                            structureBspFile.TagGroup.Write(writer);
                            bspDataStream.Align(4096);
                        }

                        if (tagList.Any(tag => tag.Id == structureLightmapReference.Id))
                        {
                            bspHeader.StructureLightmapOffset = (uint)bspDataStream.Position;
                            structureLightmapFile = tagList.First(tag => tag.Id == structureLightmapReference.Id).File;
                            structureLightmapFile.TagGroup.Write(writer);
                            bspDataStream.Align(4096);
                        }

                        bspHeader.StructureBsp = "sbsp";
                        bspHeader.BlockLength = (int)bspDataStream.Length;

                        if (bspDataStream.Length > bspLength)
                            bspLength = bspDataStream.Length;

                        bspDataStream.Seek(bspAddress, SeekOrigin.Begin);
                        writer.Write(bspHeader);

                        scenarioStructureBspInfoStructBlock.Fields[0].Value = (int)fs.Align(512, 0);
                        scenarioStructureBspInfoStructBlock.Fields[1].Value = (int)bspDataStream.Length;
                        scenarioStructureBspInfoStructBlock.Fields[2].Value = (int)bspAddress;

                        mapWriter.Write(bspDataStream.ToArray());
                    }
                }

                header.StringCount = (uint)strings.Count;
                header.Strings128Offset = (uint)fs.Align(512, 0);
                foreach (string stringId in strings)
                    mapWriter.WriteUTF8(stringId.PadRight(128, '\0'));

                int offset = 0;
                header.StringsIndexOffset = (uint)fs.Align(512, 0);
                foreach (string stringId in strings)
                {
                    mapWriter.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringId) + 1;
                }

                header.StringsOffset = (uint)fs.Align(512, 0);
                foreach (string stringId in strings)
                    mapWriter.WriteUTF8NullTerminated(stringId);
                header.StringsLength = (uint)strings.Sum(s => Encoding.UTF8.GetByteCount(s) + 1);

                string[] tagNames = tagList.Select(tag => tag.TagName.Substring(0, tag.TagName.LastIndexOf('.'))).ToArray();
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

                offset = 0;
                uint enIndex = (uint)fs.Align(512, 0);
                for (int i = 0; i < stringLocalizations.En.Count; i++)
                {
                    var stringObject = stringLocalizations.En[i];
                    mapWriter.Write(strings[stringObject.ID]);
                    mapWriter.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringObject.Value) + 1;
                }
                uint enTable = (uint)fs.Align(512, 0);
                using (MemoryStream ms = new MemoryStream())
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    foreach (var stringObject in stringLocalizations.En)
                        writer.WriteUTF8NullTerminated(stringObject.Value);
                    stringLocalizations.EnSize = (int)ms.Length;
                    mapWriter.Write(ms.ToArray());
                }

                offset = 0;
                uint jpIndex = (uint)fs.Align(512, 0);
                for (int i = 0; i < stringLocalizations.Jp.Count; i++)
                {
                    var stringObject = stringLocalizations.Jp[i];
                    mapWriter.Write(strings[stringObject.ID]);
                    mapWriter.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringObject.Value) + 1;
                }
                uint jpTable = (uint)fs.Align(512, 0);
                using (MemoryStream ms = new MemoryStream())
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    foreach (var stringObject in stringLocalizations.Jp)
                        writer.WriteUTF8NullTerminated(stringObject.Value);
                    stringLocalizations.JpSize = (int)ms.Length;
                    mapWriter.Write(ms.ToArray());
                }

                offset = 0;
                uint nlIndex = (uint)fs.Align(512, 0);
                for (int i = 0; i < stringLocalizations.Nl.Count; i++)
                {
                    var stringObject = stringLocalizations.Nl[i];
                    mapWriter.Write(strings[stringObject.ID]);
                    mapWriter.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringObject.Value) + 1;
                }
                uint nlTable = (uint)fs.Align(512, 0);
                using (MemoryStream ms = new MemoryStream())
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    foreach (var stringObject in stringLocalizations.Nl)
                        writer.WriteUTF8NullTerminated(stringObject.Value);
                    stringLocalizations.NlSize = (int)ms.Length;
                    mapWriter.Write(ms.ToArray());
                }

                offset = 0;
                uint frIndex = (uint)fs.Align(512, 0);
                for (int i = 0; i < stringLocalizations.Fr.Count; i++)
                {
                    var stringObject = stringLocalizations.Fr[i];
                    mapWriter.Write(strings[stringObject.ID]);
                    mapWriter.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringObject.Value) + 1;
                }
                uint frTable = (uint)fs.Align(512, 0);
                using (MemoryStream ms = new MemoryStream())
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    foreach (var stringObject in stringLocalizations.Fr)
                        writer.WriteUTF8NullTerminated(stringObject.Value);
                    stringLocalizations.FrSize = (int)ms.Length;
                    mapWriter.Write(ms.ToArray());
                }

                offset = 0;
                uint esIndex = (uint)fs.Align(512, 0);
                for (int i = 0; i < stringLocalizations.Es.Count; i++)
                {
                    var stringObject = stringLocalizations.Es[i];
                    mapWriter.Write(strings[stringObject.ID]);
                    mapWriter.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringObject.Value) + 1;
                }
                uint esTable = (uint)fs.Align(512, 0);
                using (MemoryStream ms = new MemoryStream())
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    foreach (var stringObject in stringLocalizations.Es)
                        writer.WriteUTF8NullTerminated(stringObject.Value);
                    stringLocalizations.EsSize = (int)ms.Length;
                    mapWriter.Write(ms.ToArray());
                }

                offset = 0;
                uint itIndex = (uint)fs.Align(512, 0);
                for (int i = 0; i < stringLocalizations.It.Count; i++)
                {
                    var stringObject = stringLocalizations.It[i];
                    mapWriter.Write(strings[stringObject.ID]);
                    mapWriter.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringObject.Value) + 1;
                }
                uint itTable = (uint)fs.Align(512, 0);
                using (MemoryStream ms = new MemoryStream())
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    foreach (var stringObject in stringLocalizations.It)
                        writer.WriteUTF8NullTerminated(stringObject.Value);
                    stringLocalizations.ItSize = (int)ms.Length;
                    mapWriter.Write(ms.ToArray());
                }

                offset = 0;
                uint krIndex = (uint)fs.Align(512, 0);
                for (int i = 0; i < stringLocalizations.Kr.Count; i++)
                {
                    var stringObject = stringLocalizations.Kr[i];
                    mapWriter.Write(strings[stringObject.ID]);
                    mapWriter.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringObject.Value) + 1;
                }
                uint krTable = (uint)fs.Align(512, 0);
                using (MemoryStream ms = new MemoryStream())
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    foreach (var stringObject in stringLocalizations.Kr)
                        writer.WriteUTF8NullTerminated(stringObject.Value);
                    stringLocalizations.KrSize = (int)ms.Length;
                    mapWriter.Write(ms.ToArray());
                }

                offset = 0;
                uint zhIndex = (uint)fs.Align(512, 0);
                for (int i = 0; i < stringLocalizations.Zh.Count; i++)
                {
                    var stringObject = stringLocalizations.Zh[i];
                    mapWriter.Write(strings[stringObject.ID]);
                    mapWriter.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringObject.Value) + 1;
                }
                uint zhTable = (uint)fs.Align(512, 0);
                using (MemoryStream ms = new MemoryStream())
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    foreach (var stringObject in stringLocalizations.Zh)
                        writer.WriteUTF8NullTerminated(stringObject.Value);
                    stringLocalizations.ZhSize = (int)ms.Length;
                    mapWriter.Write(ms.ToArray());
                }

                offset = 0;
                uint prIndex = (uint)fs.Align(512, 0);
                for (int i = 0; i < stringLocalizations.Pr.Count; i++)
                {
                    var stringObject = stringLocalizations.Pr[i];
                    mapWriter.Write(strings[stringObject.ID]);
                    mapWriter.Write(offset);
                    offset += Encoding.UTF8.GetByteCount(stringObject.Value) + 1;
                }
                uint prTable = (uint)fs.Align(512, 0);
                using (MemoryStream ms = new MemoryStream())
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    foreach (var stringObject in stringLocalizations.Pr)
                        writer.WriteUTF8NullTerminated(stringObject.Value);
                    stringLocalizations.PrSize = (int)ms.Length;
                    mapWriter.Write(ms.ToArray());
                }

                header.CrazyOffset = (uint)fs.Align(512, 0);
                using (MemoryStream ms = new MemoryStream())
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    string credits = string.Format(Properties.Resources.Credits, DateTime.Now.ToLongDateString());
                    writer.WriteUTF8(credits);
                    header.CrazyLength = (uint)ms.Length;
                    mapWriter.Write(ms.ToArray());
                };

                using (var textureCache = new VirtualStream(fs.Position))
                {
                    foreach (var reference in tagList.Where(t => t.File.TagGroup.Tag == "bitm"))  //bitmap
                        Bitmap_WriteResources(reference.File, textureCache);

                    mapWriter.Write(textureCache.ToArray());
                }

                tagDataAddress = bspAddress + bspLength;
                using (VirtualStream tagDataStream = new VirtualStream(tagDataAddress))
                using (BinaryWriter writer = new BinaryWriter(tagDataStream))
                {
                    tagDataStream.Align(1024, 0);
                    for (int i = 0; i < tagList.Count; i++)
                    {
                        AbideTagGroupFile tagGroupFile = tagList[i].File;
                        objects[i] = new ObjectEntry() { Id = tagList[i].Id, Tag = tagGroupFile.TagGroup.Tag };
                        if (objects[i].Tag == "sbsp" || objects[i].Tag == "ltmp") continue;

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

                    mapWriter.Write(tagDataStream.ToArray());
                }

                fs.Align(1024);
                fs.Seek(header.IndexOffset + header.IndexLength + 400, SeekOrigin.Begin);

                mapWriter.Write(stringLocalizations.En.Count);
                mapWriter.Write(stringLocalizations.EnSize);
                mapWriter.Write(enIndex);
                mapWriter.Write(enTable);

                fs.Seek(12, SeekOrigin.Current);
                mapWriter.Write(stringLocalizations.Jp.Count);
                mapWriter.Write(stringLocalizations.JpSize);
                mapWriter.Write(jpIndex);
                mapWriter.Write(jpTable);

                fs.Seek(12, SeekOrigin.Current);
                mapWriter.Write(stringLocalizations.Nl.Count);
                mapWriter.Write(stringLocalizations.NlSize);
                mapWriter.Write(nlIndex);
                mapWriter.Write(nlTable);

                fs.Seek(12, SeekOrigin.Current);
                mapWriter.Write(stringLocalizations.Fr.Count);
                mapWriter.Write(stringLocalizations.FrSize);
                mapWriter.Write(frIndex);
                mapWriter.Write(frTable);

                fs.Seek(12, SeekOrigin.Current);
                mapWriter.Write(stringLocalizations.Es.Count);
                mapWriter.Write(stringLocalizations.EsSize);
                mapWriter.Write(esIndex);
                mapWriter.Write(esTable);

                fs.Seek(12, SeekOrigin.Current);
                mapWriter.Write(stringLocalizations.It.Count);
                mapWriter.Write(stringLocalizations.ItSize);
                mapWriter.Write(itIndex);
                mapWriter.Write(itTable);

                fs.Seek(12, SeekOrigin.Current);
                mapWriter.Write(stringLocalizations.Kr.Count);
                mapWriter.Write(stringLocalizations.KrSize);
                mapWriter.Write(krIndex);
                mapWriter.Write(krTable);

                fs.Seek(12, SeekOrigin.Current);
                mapWriter.Write(stringLocalizations.Zh.Count);
                mapWriter.Write(stringLocalizations.ZhSize);
                mapWriter.Write(zhIndex);
                mapWriter.Write(zhTable);

                fs.Seek(12, SeekOrigin.Current);
                mapWriter.Write(stringLocalizations.Pr.Count);
                mapWriter.Write(stringLocalizations.PrSize);
                mapWriter.Write(prIndex);
                mapWriter.Write(prTable);

                header.FileLength = (uint)fs.Length;
                header.Checksum = 0;
                fs.Seek(2048, SeekOrigin.Begin);
                for (int i = 0; i < (header.FileLength - 2048) / 4; i++)
                    header.Checksum ^= mapReader.ReadUInt32();

                fs.Seek(0, SeekOrigin.Begin);
                mapWriter.Write(header);
            }

            tagList.Clear();
            strings.Clear();
            stringLocalizations.Reset();
            mapType = -1;
            GC.Collect();
        }
        protected override object OnStart()
        {
            ScenarioPath = scenarioFileName.Replace(Path.Combine(workspaceDirectory, "tags"), string.Empty).Substring(1).Replace(".scenario", string.Empty);
            MapName = Path.GetFileName(ScenarioPath);
            OutputMapFileName = Path.Combine(workspaceDirectory, "maps", $"{MapName}.map");

            ReportStatus("Starting compile operation...");
            SetProgressVisibility(false);
            return null;
        }
        private void Campaign_Prepare()
        {
            throw new NotImplementedException();
        }
        private void Multiplayer_Prepare()
        {
            using (Stream stream = SharedResources.GetMultiplayerSharedGlobals())
                GlobalsFile.Load(stream);

            using (Stream stream = SharedResources.GetSoundClasses())
                SoundClassesFile.Load(stream);

            using (Stream stream = SharedResources.GetCombatDialogueConstants())
                CombatDialogueConstantFile.Load(stream);

            using (Stream stream = SharedResources.GetMultiplayerGlobals(true))
                MultiplayerGlobalsFile.Load(stream);

            // postprocess globals file because we're too lazy to export the file and re-import into project.
            GlobalsFile.TagGroup.TagBlocks[0].Fields[25].Value = string.Empty;  // null multiplayer globals so we can load in the multiplayer specific one

            globalsTag = new Tag(@"globals\globals.globals") { File = GlobalsFile };
            tagList.Add(globalsTag);

            soundClassesTag = new Tag(@"sound\sound_classes.sound_classes") { File = SoundClassesFile, };
            tagList.Add(soundClassesTag);

            combatDialogueConstantTag = new Tag(@"sound\combat_dialogue_constants.sound_dialogue_constants") { File = CombatDialogueConstantFile };
            tagList.Add(combatDialogueConstantTag);

            multiplayerGlobalsTag = new Tag(@"globals\multiplayer_globals.multiplayer_globals") { File = MultiplayerGlobalsFile };
        }
        private void Scenario_Prepare()
        {
            scenarioTag = new Tag($"{ScenarioPath}.{ScenarioFile.TagGroup.Name}") { File = ScenarioFile };
            tagList.Add(scenarioTag);
        }
        private void SoundCacheFileGestalt_Prepare()
        {
            var tagGroup = SoundCacheFileGestaltFile.TagGroup = new SoundCacheFileGestalt();
            soundCacheFileGestaltTag = new Tag($"i've got a lovely bunch of coconuts.sound_cache_file_gestalt") { File = SoundCacheFileGestaltFile };
            if (((BlockField)tagGroup.TagBlocks[0].Fields[0]).Add(out Block playbackTagBlock))
            {
                Block playbackParametersStructBlock = (Block)playbackTagBlock.Fields[0].Value;
                playbackParametersStructBlock.Fields[9].Value = (float)Math.PI;
                playbackParametersStructBlock.Fields[10].Value = (float)Math.PI;
            }

            if (((BlockField)tagGroup.TagBlocks[0].Fields[1]).Add(out Block scaleTagBlock))
            {
                Block scaleModifiersStructBlock = (Block)scaleTagBlock.Fields[0].Value;
                scaleModifiersStructBlock.Fields[3].Value = new FloatBounds(1f, 1f);
            }

            for (int i = 0; i < 686; i++)
            {
                if (((BlockField)tagGroup.TagBlocks[0].Fields[7]).Add(out Block runtimePermutationBitVector))
                    runtimePermutationBitVector.Fields[0].Value = (byte)0;
            }

            tagList.Add(soundCacheFileGestaltTag);
        }
        private void Discover(Group tagGroup)
        {
            foreach (var block in tagGroup.TagBlocks)
            {
                Discover(block);
            }
        }
        private void Discover(Block block)
        {
            foreach (var field in block.Fields)
            {
                AbideTagGroupFile file;
                switch (field.Type)
                {
                    case FieldType.FieldStringId:
                    case FieldType.FieldOldStringId:
                        if (field.Value is string stringIdValue && !string.IsNullOrEmpty(stringIdValue))
                        {
                            if (!strings.Contains(stringIdValue))
                                strings.Add(stringIdValue);
                        }
                        break;

                    case FieldType.FieldTagReference:
                        if (field.Value is string tagReferenceValue && !string.IsNullOrEmpty(tagReferenceValue))
                        {
                            if (!tagList.Any(t => t.TagName == tagReferenceValue))
                            {
                                string tagFileName = Path.Combine(workspaceDirectory, "tags", tagReferenceValue);
                                if (File.Exists(tagFileName))
                                {
                                    file = new AbideTagGroupFile();
                                    file.Load(tagFileName);

                                    Tag tag = new Tag(tagReferenceValue) { File = file };
                                    tagList.Add(tag);
                                    ReportStatus($"Finding tags... ({tagList.Count} found)");
                                    Discover(tag.File.TagGroup);
                                }
                            }
                        }
                        break;

                    case FieldType.FieldTagIndex:
                        if (field.Value is string tagIndexValue && !string.IsNullOrEmpty(tagIndexValue))
                        {
                            if (!tagList.Any(t => t.TagName == tagIndexValue))
                            {
                                string tagFileName = Path.Combine(workspaceDirectory, "tags", tagIndexValue);
                                if (File.Exists(tagFileName))
                                {
                                    file = new AbideTagGroupFile();
                                    file.Load(tagFileName);

                                    Tag tag = new Tag(tagIndexValue) { File = file };
                                    tagList.Add(tag);
                                    ReportStatus($"Finding tags... ({tagList.Count} found)");
                                    Discover(tag.File.TagGroup);
                                }
                            }
                        }
                        break;
                }

                if (field is BlockField blockField)
                {
                    foreach (var childBlock in blockField.BlockList)
                    {
                        Discover(childBlock);
                    }
                }
                else if (field is StructField structField)
                {
                    if (field.Value is Block structBlock)
                    {
                        Discover(structBlock);
                    }
                }
            }
        }
        private void TagGroup_ToCache(AbideTagGroupFile tagGroupFile)
        {
            Group tagGroup = tagGroupFile.TagGroup;

            for (int i = 0; i < tagGroup.TagBlockCount; i++)
                TagBlock_ToCache(tagGroup.TagBlocks[i]);
        }
        private void TagBlock_ToCache(Block tagBlock)
        {
            for (int i = 0; i < tagBlock.FieldCount; i++)
                tagBlock.Fields[i] = Field_ToCache(tagBlock.Fields[i]);
        }
        private Field Field_ToCache(Field field)
        {
            switch (field)
            {
                case Abide.Tag.Guerilla.StringIdField stringIdField:
                    StringIdField cacheStringIdField = new StringIdField(stringIdField.GetName());
                    if (!string.IsNullOrEmpty(stringIdField.Value))
                    {
                        cacheStringIdField.Value = strings[stringIdField.Value];
                    }
                    return cacheStringIdField;

                case Abide.Tag.Guerilla.OldStringIdField oldStringIdField:
                    OldStringIdField cacheOldStringIdField = new OldStringIdField(oldStringIdField.GetName());
                    if (!string.IsNullOrEmpty(oldStringIdField.Value))
                    {
                        cacheOldStringIdField.Value = strings[oldStringIdField.Value];
                    }
                    return cacheOldStringIdField;

                case Abide.Tag.Guerilla.TagReferenceField tagReferenceField:
                    TagReferenceField cacheTagReferenceField = new TagReferenceField(tagReferenceField.GetName(), tagReferenceField.GroupTag);
                    if (!string.IsNullOrEmpty(tagReferenceField.Value))
                    {
                        var tagReference = cacheTagReferenceField.Reference;
                        tagReference.Id = tagList.First(t => t.TagName == tagReferenceField.Value).Id;
                        cacheTagReferenceField.Value = tagReference;
                    }
                    return cacheTagReferenceField;

                case Abide.Tag.Guerilla.TagIndexField tagIndexField:
                    TagIndexField cacheTagIndexField = new TagIndexField(tagIndexField.GetName());
                    if (!string.IsNullOrEmpty(tagIndexField.Value))
                    {
                        cacheTagIndexField.Value = tagList.First(t => t.TagName == tagIndexField.Value).Id;
                    }
                    return cacheTagIndexField;

                case BlockField blockField:
                    foreach (var block in blockField.BlockList)
                    {
                        TagBlock_ToCache(block);
                    }
                    return blockField;

                case StructField structField:
                    TagBlock_ToCache(structField.Block);
                    return structField;

                default: return field;
            }
        }
        private void ProcessTag(Tag tag)
        {
            switch (tag.File.TagGroup.Name)
            {
                case "multilingual_unicode_string_list":
                    MultilingualUnicodeStringList_Process(tag);
                    break;

                case "sound":
                    Sound_Process(tag);
                    break;
            }

            foreach (var tagBlock in tag.File.TagGroup.TagBlocks)
            {
                IEnumerable<Tag> resources;
                switch (tagBlock.BlockName)
                {
                    case "object_block":
                        var predictedResources = (BlockField)tagBlock.Fields[37];
                        resources = GetPredictedResources(tag);
                        predictedResources.BlockList.Clear();
                        break;

                    case "item_block":
                        var predictedBitmaps = (BlockField)tagBlock.Fields[19];
                        resources = GetPredictedBitmaps(tag);
                        predictedBitmaps.BlockList.Clear();
                        break;

                    case "scenario_block":
                        var scenarioSimulationDefinitions = (BlockField)tagBlock.Fields[143];
                        scenarioSimulationDefinitions.BlockList.Clear();
                        foreach (var reference in GetScenarioSimulationDefinitionTable(tag))
                        {
                            if (scenarioSimulationDefinitions.Add(out Block block))
                            {
                                block.Fields[0] = new TagIndexField(string.Empty);
                                block.Fields[0].Value = reference.Id;
                            }
                        }
                        break;
                }
            }
        }
        private IEnumerable<Tag> GetPredictedResources(Tag tag)
        {
            List<Tag> references = new List<Tag>();
            Action<Block> discoverPredictedResources = null;
            discoverPredictedResources = new Action<Block>(b =>
            {
                foreach (var field in b.Fields)
                {
                    switch (field)
                    {
                        case BlockField blockField:
                            foreach (var block in blockField.BlockList)
                            {
                                discoverPredictedResources(block);
                            }
                            break;

                        case StructField structField:
                            discoverPredictedResources(structField.Block);
                            break;

                        case TagReferenceField tagReferenceField:
                            break;

                        case TagIndexField tagIndexField:
                            break;
                    }
                }
            });

            foreach (var block in tag.File.TagGroup.TagBlocks)
            {
                discoverPredictedResources(block);
            }

            return references;
        }
        private IEnumerable<Tag> GetPredictedBitmaps(Tag tag)
        {
            List<Tag> references = new List<Tag>();
            Action<Block> discoverPredictedBitmaps = null;
            discoverPredictedBitmaps = new Action<Block>(b =>
            {
                foreach (var field in b.Fields)
                {
                    switch (field)
                    {
                        case BlockField blockField:
                            foreach (var block in blockField.BlockList)
                            {
                                discoverPredictedBitmaps(block);
                            }
                            break;

                        case StructField structField:
                            discoverPredictedBitmaps(structField.Block);
                            break;

                        case TagReferenceField tagReferenceField:
                            break;

                        case TagIndexField tagIndexField:
                            break;
                    }
                }
            });

            foreach (var block in tag.File.TagGroup.TagBlocks)
            {
                discoverPredictedBitmaps(block);
            }

            return references;
        }
        private IEnumerable<Tag> GetScenarioSimulationDefinitionTable(Tag tag)
        {
            List<Tag> references = new List<Tag>();
            foreach (var reference in tagList)
            {
                switch (reference.File.TagGroup.Tag)
                {
                    case "bipd":
                    case "bloc":
                    case "ctrl":
                    case "jpt!":
                    case "mach":
                    case "scen":
                    case "ssce":
                    case "vehi":
                        references.Add(reference);
                        break;

                    case "eqip":
                    case "garb":
                    case "proj":
                        references.Add(reference);
                        references.Add(reference);
                        break;

                    case "weap":
                        references.Add(reference);
                        references.Add(reference);
                        references.Add(reference);
                        break;
                }
            }

            return references;
        }
        private void MultilingualUnicodeStringList_Process(Tag tag)
        {
            //Prepare
            StringContainer strings = new StringContainer();
            byte[] stringData = ((DataField)tag.File.TagGroup.TagBlocks[0].Fields[1]).GetBuffer();
            string unicodeString = string.Empty;
            int offset = 0;

            //Read strings
            using (MemoryStream ms = new MemoryStream(stringData))
            using (BinaryReader reader = new BinaryReader(ms))
            {
                //Loop
                foreach (Block stringReferenceBlock in ((BlockField)tag.File.TagGroup.TagBlocks[0].Fields[0]).BlockList)
                {
                    //Get string ID
                    string stringId = this.strings[((StringId)stringReferenceBlock.Fields[0].Value).Index];

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
            tag.File.TagGroup = new MultilingualUnicodeStringList();
            byte[] padding = ((PadField)tag.File.TagGroup.TagBlocks[0].Fields[2]).Value;
            using (MemoryStream ms = new MemoryStream(padding))
            using (BinaryWriter writer = new BinaryWriter(ms))
            {
                //Write English
                writer.Write((ushort)stringLocalizations.En.Count);
                writer.Write((ushort)strings.English.Count);
                stringLocalizations.En.AddRange(strings.English);

                //Write Japanese
                writer.Write((ushort)stringLocalizations.Jp.Count);
                writer.Write((ushort)strings.Japanese.Count);
                stringLocalizations.Jp.AddRange(strings.Japanese);

                //Write German
                writer.Write((ushort)stringLocalizations.Nl.Count);
                writer.Write((ushort)strings.German.Count);
                stringLocalizations.Nl.AddRange(strings.German);

                //Write French
                writer.Write((ushort)stringLocalizations.Fr.Count);
                writer.Write((ushort)strings.French.Count);
                stringLocalizations.Fr.AddRange(strings.French);

                //Write Spanish
                writer.Write((ushort)stringLocalizations.Es.Count);
                writer.Write((ushort)strings.Spanish.Count);
                stringLocalizations.Es.AddRange(strings.Spanish);

                //Write Italian
                writer.Write((ushort)stringLocalizations.It.Count);
                writer.Write((ushort)strings.Italian.Count);
                stringLocalizations.It.AddRange(strings.Italian);

                //Write Korean
                writer.Write((ushort)stringLocalizations.Kr.Count);
                writer.Write((ushort)strings.Korean.Count);
                stringLocalizations.Kr.AddRange(strings.Korean);

                //Write Chinese
                writer.Write((ushort)stringLocalizations.Zh.Count);
                writer.Write((ushort)strings.Chinese.Count);
                stringLocalizations.Zh.AddRange(strings.Chinese);

                //Write Portuguese
                writer.Write((ushort)stringLocalizations.Pr.Count);
                writer.Write((ushort)strings.Portuguese.Count);
                stringLocalizations.Pr.AddRange(strings.Portuguese);
            }
        }
        private void Sound_Process(Tag tag)
        {
            //Prepare
            Group soundCacheFileGestalt = SoundCacheFileGestaltFile.TagGroup;
            Group cacheFileSound = new Sound();
            Group sound = tag.File.TagGroup;
            bool success = false;
            int index = 0;

            //Get tag blocks
            Block soundCacheFileGestaltBlock = soundCacheFileGestalt.TagBlocks[0];
            Block cacheFileSoundBlock = cacheFileSound.TagBlocks[0];
            Block soundBlock = sound.TagBlocks[0];

            //Transfer raws
            foreach (int rawOffset in tag.File.GetResourceAddresses())
            {
                SoundCacheFileGestaltFile.SetResource(rawOffset, tag.File.GetResource(rawOffset));
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
            tag.File.TagGroup = cacheFileSound;

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
                if (reader.ReadByte() != NullCharIndex)
                    cacheFileSoundBlock.Fields[12].Value = maxPlaybackTime;
            }

            //Add or get playback index
            cacheFileSoundBlock.Fields[5].Value = (short)SoundGestalt_FindPlaybackIndex((Block)soundBlock.Fields[5].Value);

            //Add scale
            cacheFileSoundBlock.Fields[8].Value = (byte)(sbyte)SoundGestalt_FindScaleIndex((Block)soundBlock.Fields[6].Value);

            //Add promotion
            Block soundPromotionParametersStruct = (Block)soundBlock.Fields[11].Value;
            if (((BlockField)soundPromotionParametersStruct.Fields[0]).BlockList.Count > 0)
                cacheFileSoundBlock.Fields[9].Value = (byte)(sbyte)SoundGestalt_FindPromotionIndex((Block)soundBlock.Fields[11].Value);
            else cacheFileSoundBlock.Fields[9].Value = NullCharIndex;

            //Add custom playback
            if (((BlockField)soundBlock.Fields[14]).BlockList.Count > 0)
            {
                index = customPlaybacks.BlockList.Count;
                if (customPlaybacks.Add(out Block customPlayback))
                {
                    cacheFileSoundBlock.Fields[10].Value = (byte)index;
                    customPlayback.Fields[0].Value = ((BlockField)soundBlock.Fields[14]).BlockList[0].Fields[0].Value;
                }
                else cacheFileSoundBlock.Fields[10].Value = NullCharIndex;
            }
            else cacheFileSoundBlock.Fields[10].Value = NullCharIndex;

            //Add extra info
            if (((BlockField)soundBlock.Fields[15]).BlockList.Count > 0)
            {
                index = extraInfos.BlockList.Count;
                Block soundExtraInfo = ((BlockField)soundBlock.Fields[15]).BlockList[0];
                if (extraInfos.Add(out Block extraInfo))
                {
                    cacheFileSoundBlock.Fields[11].Value = (short)index;
                    extraInfo.Fields[1].Value = soundExtraInfo.Fields[2].Value;
                    ((Block)extraInfo.Fields[1].Value).Fields[6].Value = tag.Id;
                    foreach (Block block in ((BlockField)soundExtraInfo.Fields[1]).BlockList)
                        ((BlockField)soundExtraInfo.Fields[1]).BlockList.Add(block);
                }
                else cacheFileSoundBlock.Fields[11].Value = NullShortIndex;
            }
            else cacheFileSoundBlock.Fields[11].Value = NullShortIndex;

            //Add pitch range
            cacheFileSoundBlock.Fields[7].Value = (byte)((BlockField)soundBlock.Fields[13]).BlockList.Count;
            foreach (var soundPitchRange in ((BlockField)soundBlock.Fields[13]).BlockList)
            {
                index = pitchRanges.BlockList.Count;
                if (pitchRanges.Add(out Block gestaltPitchRange))
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
                    gestaltPitchRange.Fields[5].Value = (short)((BlockField)soundPitchRange.Fields[7]).BlockList.Count;

                    //Loop
                    foreach (Block soundPermutation in ((BlockField)soundPitchRange.Fields[7]).BlockList)
                    {
                        if (permutations.Add(out Block gestaltPermutation))
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
                            gestaltPermutation.Fields[7].Value = (short)((BlockField)soundPermutation.Fields[6]).BlockList.Count;

                            //Loop
                            foreach (Block soundChunk in ((BlockField)soundPermutation.Fields[6]).BlockList)
                            {
                                chunks.BlockList.Add(soundChunk);
                                int offset = (int)soundChunk.Fields[0].Value;
                                if ((offset & 0xc0000000) == 0 && tag.File.GetResource(offset).Length == 0)
                                    System.Diagnostics.Debugger.Break();
                            }
                        }
                        else
                        {
                            gestaltPitchRange.Fields[4].Value = NullShortIndex;
                            gestaltPitchRange.Fields[5].Value = (short)0;
                        }
                    }
                }
                else
                {
                    cacheFileSoundBlock.Fields[6].Value = NullShortIndex;
                    cacheFileSoundBlock.Fields[7].Value = NullCharIndex;
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
                if (blockField.Add(out Block gestaltBlock))
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
            Block soundCacheFileGestaltBlock = SoundCacheFileGestaltFile.TagGroup.TagBlocks[0];
            BlockField blockField = (BlockField)soundCacheFileGestaltBlock.Fields[2];
            int index = -1;

            //Check
            foreach (Block gestaltBlock in blockField.BlockList)
            {
                if (((StringId)gestaltBlock.Fields[0].Value).Equals(stringId))
                {
                    index = blockField.BlockList.IndexOf(gestaltBlock);
                    break;
                }
            }

            //Add (or return -1)
            if (index == -1)
            {
                index = blockField.BlockList.Count;
                if (blockField.Add(out Block gestaltBlock)) 
                    gestaltBlock.Fields[0].Value = stringId;
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
                if (blockField.Add(out Block gestaltBlock))
                    gestaltBlock.Fields[0].Value = structBlock;
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
                if (blockField.Add(out Block gestaltBlock))
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
                if (blockField.Add(out Block gestaltBlock))
                    gestaltBlock.Fields[0].Value = structBlock;
                else index = -1;
            }

            //return
            return index;
        }
        private bool TagBlock_Equals(Block b1, Block b2)
        {
            if (b1.FieldCount == b2.FieldCount)
            {
                for (int i = 0; i < b1.FieldCount; i++)
                {
                    Field f1 = b1.Fields[i], f2 = b2.Fields[i];
                    if (f1.Type != f2.Type)
                    {
                        return false;
                    }

                    switch (b1.Fields[i].Type)
                    {
                        case FieldType.FieldBlock:
                            BlockField bf1 = (BlockField)f1;
                            BlockField bf2 = (BlockField)f2;
                            if (bf1.BlockList.Count == bf2.BlockList.Count)
                            {
                                for (int j = 0; j < bf1.BlockList.Count; j++)
                                    if (!TagBlock_Equals(bf1.BlockList[j], bf2.BlockList[j]))
                                    {
                                        return false;
                                    }
                            }
                            else
                            {
                                return false;
                            }
                            break;

                        case FieldType.FieldStruct:
                            if (!TagBlock_Equals((Block)f1.Value, (Block)f2.Value))
                            {
                                return false;
                            }
                            break;

                        case FieldType.FieldPad:
                            PadField pf1 = (PadField)f1;
                            PadField pf2 = (PadField)f2;
                            for (int j = 0; j < pf1.Length; j++)
                            {
                                if (pf1.Value[j] != pf2.Value[j])
                                {
                                    return false;
                                }
                            }
                            break;

                        case FieldType.FieldSkip:
                            SkipField sf1 = (SkipField)f1;
                            SkipField sf2 = (SkipField)f2;
                            for (int j = 0; j < sf1.Length; j++)
                            {
                                if (sf1.Value[j] != sf2.Value[j])
                                {
                                    return false;
                                }
                            }
                            break;

                        default:
                            if (f1.Value == null && f2.Value == null)
                            {
                                continue;
                            }
                            else if (!f1.Value.Equals(f2.Value))
                            {
                                return false;
                            }
                            break;
                    }
                }

            }

            return true;
        }
        private void SoundCacheFileGestalt_WriteResources(AbideTagGroupFile tagGroupFile, Stream stream)
        {
            foreach (var chunk in ((BlockField)tagGroupFile.TagGroup.TagBlocks[0].Fields[8]).BlockList)
            {
                long offset = (int)chunk.Fields[0].Value;
                var resource = tagGroupFile.GetResource(offset);
                if (resource.Length > 0)
                {
                    chunk.Fields[0].Value = (int)stream.Align(512, 0);
                    chunk.Fields[1].Value = resource.Length;
                    stream.Write(resource, 0, resource.Length);
                }
            }
        }
        private void RenderModel_WriteResources(AbideTagGroupFile tagGroupFile, Stream stream)
        {
            foreach (var section in ((BlockField)tagGroupFile.TagGroup.TagBlocks[0].Fields[7]).BlockList)
            {
                Block gemoetry = (Block)section.Fields[6].Value;
                long offset = (int)gemoetry.Fields[1].Value;
                var resource = tagGroupFile.GetResource(offset);
                if (resource.Length > 0)
                {
                    gemoetry.Fields[1].Value = (int)stream.Align(512, 0);
                    gemoetry.Fields[2].Value = resource.Length;
                    stream.Write(resource, 0, resource.Length);
                }
            }

            foreach (var prtInfo in ((BlockField)tagGroupFile.TagGroup.TagBlocks[0].Fields[24]).BlockList)
            {
                Block gemoetry = (Block)prtInfo.Fields[13].Value;
                long offset = (int)gemoetry.Fields[1].Value;
                var resource = tagGroupFile.GetResource(offset);
                if (resource.Length > 0)
                {
                    gemoetry.Fields[1].Value = (int)stream.Align(512, 0);
                    gemoetry.Fields[2].Value = resource.Length;
                    stream.Write(resource, 0, resource.Length);
                }
            }
        }
        private void Scenario_WriteZoneResources(AbideTagGroupFile tagGroupFile, Stream stream)
        {
            foreach (var structureBsp in ((BlockField)tagGroupFile.TagGroup.TagBlocks[0].Fields[68]).BlockList)
            {
                var structureBspId = ((TagReferenceField)structureBsp.Fields[1]).Reference.Id;
                var structureLightmapId = ((TagReferenceField)structureBsp.Fields[2]).Reference.Id;
                AbideTagGroupFile structureBspFile = null;
                AbideTagGroupFile structureLightmapFile = null;

                if (tagList.Count > structureBspId.Index)
                {
                    structureBspFile = tagList[structureBspId].File;
                }

                if (tagList.Count > structureLightmapId.Index)
                {
                    structureLightmapFile = tagList[structureLightmapId].File;
                }

                foreach (var cluster in ((BlockField)structureBspFile.TagGroup.TagBlocks[0].Fields[19]).BlockList)
                {
                    Block gemoetry = (Block)cluster.Fields[1].Value;
                    long offset = (int)gemoetry.Fields[1].Value;
                    var resource = structureBspFile.GetResource(offset);
                    if (resource.Length > 0)
                    {
                        gemoetry.Fields[1].Value = (int)stream.Align(512, 0);
                        gemoetry.Fields[2].Value = resource.Length;
                        stream.Write(resource, 0, resource.Length);
                    }
                }

                foreach (var geometryDefinition in ((BlockField)structureBspFile.TagGroup.TagBlocks[0].Fields[39]).BlockList)
                {
                    Block renderInfo = (Block)geometryDefinition.Fields[0].Value;
                    Block gemoetry = (Block)renderInfo.Fields[1].Value;
                    long offset = (int)gemoetry.Fields[1].Value;
                    var resource = structureBspFile.GetResource(offset);
                    if (resource.Length > 0)
                    {
                        gemoetry.Fields[1].Value = (int)stream.Align(512, 0);
                        gemoetry.Fields[2].Value = resource.Length;
                        stream.Write(resource, 0, resource.Length);
                    }
                }

                foreach (var waterDefinition in ((BlockField)structureBspFile.TagGroup.TagBlocks[0].Fields[50]).BlockList)
                {
                    Block gemoetry = (Block)waterDefinition.Fields[2].Value;
                    long offset = (int)gemoetry.Fields[1].Value;
                    var resource = structureBspFile.GetResource(offset);
                    if (resource.Length > 0)
                    {
                        gemoetry.Fields[1].Value = (int)stream.Align(512, 0);
                        gemoetry.Fields[2].Value = resource.Length;
                        stream.Write(resource, 0, resource.Length);
                    }
                }

                foreach (var lightmapGroup in ((BlockField)structureLightmapFile.TagGroup.TagBlocks[0].Fields[16]).BlockList)
                {
                    foreach (var cluster in ((BlockField)lightmapGroup.Fields[6]).BlockList)
                    {
                        Block gemoetry = (Block)cluster.Fields[1].Value;
                        long offset = (int)gemoetry.Fields[1].Value;
                        var resource = structureLightmapFile.GetResource(offset);
                        if (resource.Length > 0)
                        {
                            gemoetry.Fields[1].Value = (int)stream.Align(512, 0);
                            gemoetry.Fields[2].Value = resource.Length;
                            stream.Write(resource, 0, resource.Length);
                        }
                    }

                    foreach (var definition in ((BlockField)lightmapGroup.Fields[8]).BlockList)
                    {
                        Block gemoetry = (Block)definition.Fields[1].Value;
                        long offset = (int)gemoetry.Fields[1].Value;
                        var resource = structureLightmapFile.GetResource(offset);
                        if (resource.Length > 0)
                        {
                            gemoetry.Fields[1].Value = (int)stream.Align(512, 0);
                            gemoetry.Fields[2].Value = resource.Length;
                            stream.Write(resource, 0, resource.Length);
                        }
                    }

                    foreach (var bucket in ((BlockField)lightmapGroup.Fields[10]).BlockList)
                    {
                        Block gemoetry = (Block)bucket.Fields[3].Value;
                        long offset = (int)gemoetry.Fields[1].Value;
                        var resource = structureLightmapFile.GetResource(offset);
                        if (resource.Length > 0)
                        {
                            gemoetry.Fields[1].Value = (int)stream.Align(512, 0);
                            gemoetry.Fields[2].Value = resource.Length;
                            stream.Write(resource, 0, resource.Length);
                        }
                    }
                }

                foreach (var decorator in ((BlockField)structureBspFile.TagGroup.TagBlocks[0].Fields[54]).BlockList)
                {
                    foreach (var cache in ((BlockField)decorator.Fields[2]).BlockList)
                    {
                        Block gemoetry = (Block)cache.Fields[0].Value;
                        long offset = (int)gemoetry.Fields[1].Value;
                        var resource = structureBspFile.GetResource(offset);
                        if (resource.Length > 0)
                        {
                            gemoetry.Fields[1].Value = (int)stream.Align(512, 0);
                            gemoetry.Fields[2].Value = resource.Length;
                            stream.Write(resource, 0, resource.Length);
                        }
                    }
                }
            }
        }
        private void WeatherSystem_WriteResources(AbideTagGroupFile tagGroupFile, Stream stream)
        {
            throw new NotImplementedException();
        }
        private void ParticleModel_WriteResources(AbideTagGroupFile tagGroupFile, Stream stream)
        {
            Block gemoetry = (Block)tagGroupFile.TagGroup.TagBlocks[0].Fields[22].Value;
            long offset = (int)gemoetry.Fields[1].Value;
            var resource = tagGroupFile.GetResource(offset);
            if (resource.Length > 0)
            {
                gemoetry.Fields[1].Value = (int)stream.Align(512, 0);
                gemoetry.Fields[2].Value = resource.Length;
                stream.Write(resource, 0, resource.Length);
            }
        }
        private void DecoratorSet_WriteResources(AbideTagGroupFile tagGroupFile, Stream stream)
        {
            Block gemoetry = (Block)tagGroupFile.TagGroup.TagBlocks[0].Fields[8].Value;
            long offset = (int)gemoetry.Fields[1].Value;
            var resource = tagGroupFile.GetResource(offset);
            if (resource.Length > 0)
            {
                gemoetry.Fields[1].Value = (int)stream.Align(512, 0);
                gemoetry.Fields[2].Value = resource.Length;
                stream.Write(resource, 0, resource.Length);
            }
        }
        private void ModelAnimation_WriteResources(AbideTagGroupFile tagGroupFile, Stream stream)
        {
            foreach (var data in ((BlockField)tagGroupFile.TagGroup.TagBlocks[0].Fields[6]).BlockList)
            {
                long offset = (int)data.Fields[2].Value;
                var resource = tagGroupFile.GetResource(offset);
                if (resource.Length > 0)
                {
                    data.Fields[1].Value = resource.Length;
                    data.Fields[2].Value = (int)stream.Align(512, 0);
                    stream.Write(resource, 0, resource.Length);
                }
            }
        }
        private void SoundCacheFileGestalt_WriteExtraInfoResources(AbideTagGroupFile tagGroupFile, Stream stream)
        {
            foreach (var prtInfo in ((BlockField)tagGroupFile.TagGroup.TagBlocks[0].Fields[10]).BlockList)
            {
                Block gemoetry = (Block)prtInfo.Fields[1].Value;
                long offset = (int)gemoetry.Fields[1].Value;
                var resource = tagGroupFile.GetResource(offset);
                if (resource.Length > 0)
                {
                    gemoetry.Fields[1].Value = (int)stream.Align(512, 0);
                    gemoetry.Fields[2].Value = resource.Length;
                    stream.Write(resource, 0, resource.Length);
                }
            }
        }
        private void Bitmap_WriteResources(AbideTagGroupFile tagGroupFile, Stream stream)
        {
            foreach (var bitmapData in ((BlockField)tagGroupFile.TagGroup.TagBlocks[0].Fields[29]).BlockList)
            {
                SkipField offsetsSkip = (SkipField)bitmapData.Fields[12];
                SkipField sizesSkip = (SkipField)bitmapData.Fields[14];
                byte[] data, length;

                long offset = BitConverter.ToUInt32(offsetsSkip.Value, 0);
                var resource = tagGroupFile.GetResource(offset);
                if (resource.Length > 0)
                {
                    data = BitConverter.GetBytes((int)stream.Align(512, 0));
                    length = BitConverter.GetBytes(resource.Length);
                    Array.Copy(data, 0, offsetsSkip.Value, 0, 4);
                    Array.Copy(length, 0, sizesSkip.Value, 0, 4);
                    stream.Write(resource, 0, resource.Length);
                }
                offset = BitConverter.ToUInt32(offsetsSkip.Value, 4);
                resource = tagGroupFile.GetResource(offset);
                if (resource.Length > 0)
                {
                    data = BitConverter.GetBytes((int)stream.Align(512, 0));
                    length = BitConverter.GetBytes(resource.Length);
                    Array.Copy(data, 0, offsetsSkip.Value, 4, 4);
                    Array.Copy(length, 0, sizesSkip.Value, 4, 4);
                    stream.Write(resource, 0, resource.Length);
                }
                offset = BitConverter.ToUInt32(offsetsSkip.Value, 8);
                resource = tagGroupFile.GetResource(offset);
                if (resource.Length > 0)
                {
                    data = BitConverter.GetBytes((int)stream.Align(512, 0));
                    length = BitConverter.GetBytes(resource.Length);
                    Array.Copy(data, 0, offsetsSkip.Value, 8, 4);
                    Array.Copy(length, 0, sizesSkip.Value, 8, 4);
                    stream.Write(resource, 0, resource.Length);
                }
            }
        }
    }

    internal sealed class StringLocalizations
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
        public void Reset()
        {
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

    internal sealed class Tag
    {
        public string TagName { get; }
        public AbideTagGroupFile File { get; set; }
        public TagId Id { get; set; }

        public Tag(string tagName)
        {
            TagName = tagName;
        }
        public override string ToString()
        {
            return $"{TagName}";
        }
    }

    internal sealed class TagList : IEnumerable<Tag>
    {
        private readonly Dictionary<string, int> lookup = new Dictionary<string, int>();
        private readonly List<Tag> list = new List<Tag>();
        private int addedCount = 0;

        public bool IsReadOnly => false;
        public int Count => list.Count;
        public Tag this[int index] => list[index];
        public Tag this[TagId id] => list[id.Index];
        public Tag this[string name] => list[lookup[name]];

        public TagList() { }
        public void Add(Tag item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            int addedCount = this.addedCount++;
            item.Id = (uint)(((0xe174 + addedCount) << 16) | Count);

            lookup.Add(item.TagName, Count);
            list.Add(item);
        }
        public void Remove(Tag item)
        {
            if (list.Remove(item))
            {
                lookup.Clear();

                for (int i = 0; i < list.Count; i++)
                {
                    var id = list[i].Id;
                    id.Index = (short)i;
                    list[i].Id = id;
                    lookup.Add(list[i].TagName, i);
                }
            }
        }
        public void Clear()
        {
            list.Clear();
            lookup.Clear();
            addedCount = 0;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
        public IEnumerator<Tag> GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
}
