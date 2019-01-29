using Abide.AddOnApi;
using Abide.AddOnApi.Halo2;
using Abide.Guerilla.Library;
using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using Abide.HaloLibrary.IO;
using Abide.Tag;
using Abide.Tag.Cache;
using Abide.Tag.Cache.Generated;
using Abide.Tag.Definition;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using static Abide.HaloLibrary.Halo2Map.MapFile;
using StringValue = Abide.Tag.Guerilla.StringValue;

namespace Abide.TagBuilder.Halo2
{
    [AddOn]
    public sealed class TagImportButton : AbideMenuButton
    {
        private const short C_NullShort = unchecked((short)0xffff);
        private const byte C_NullByte = unchecked(0xff);
        private StructureBspSelectDialog bspSelectDialog = null;
        private IndexEntry m_SoundCacheFileGestaltEntry = null;
        private ITagGroup m_SoundCacheFileGestalt = null;

        public TagImportButton()
        {
            //
            // this
            //
            Author = "Click16";
            Description = "Import external tag.";
            Name = "Import Tag";
            Icon = Properties.Resources.tag_import_16;
            Click += ImportTagButton_Click;
        }

        private void ImportTagButton_Click(object sender, EventArgs e)
        {
            //Prepare
            XmlDocument document = null;
            string directory = string.Empty;
            byte[] soundCacheFileGestaltTagBuffer = null;
            byte[] tagDataBuffer = null;

            //Check
            if (Map == null) return;
            bspSelectDialog = new StructureBspSelectDialog(Map.Scenario);
            m_SoundCacheFileGestaltEntry = Map.GetSoundCacheFileGestaltEntry();
            m_SoundCacheFileGestalt = null;

            //Initialize OpenFileDialog object...
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                //Setup
                openDlg.Filter = "XML Documents (*.xml)|*.xml";

                //Show
                if (openDlg.ShowDialog() == DialogResult.OK)
                {
                    //Read document
                    document = new XmlDocument();
                    document.Load(openDlg.FileName);

                    //Get directory
                    directory = Path.GetDirectoryName(openDlg.FileName);
                }
            }

            //Check
            if (document != null)
            {
                //Get manifest node
                XmlNode manifest = document["Manifest"];

                //Check
                if (manifest != null)
                {
                    //Read sound cache file gestalt
                    using (BinaryReader reader = m_SoundCacheFileGestaltEntry.TagData.CreateReader())
                    {
                        m_SoundCacheFileGestalt = new SoundCacheFileGestalt();
                        reader.BaseStream.Seek(m_SoundCacheFileGestaltEntry.Offset, SeekOrigin.Begin);
                        m_SoundCacheFileGestalt.Read(reader);
                    }

                    //Get tags and strings
                    XmlNode tags = manifest["Tags"];
                    XmlNode strings = manifest["Strings"];

                    //Create lookup
                    TagsContainer tagIdLookup = new TagsContainer();

                    //Check strings
                    if (strings != null)
                    {
                        //Loop through nodes
                        foreach (XmlNode stringNode in strings.ChildNodes)
                        {
                            if (stringNode.Name == "String")
                            {
                                //Get string
                                string sid = stringNode.Attributes["value"]?.Value ?? string.Empty;
                                if (!Map.Strings.Contains(sid)) Map.Strings.Add(sid);
                            }
                        }
                    }

                    //Check tags
                    if (tags != null)
                    {
                        //Loop through nodes
                        TagId lastId = Map.IndexEntries.Last.Id;
                        foreach (XmlNode tagNode in tags.ChildNodes)
                            if (tagNode.Name == "Tag")
                            {
                                //Get local file
                                string localFile = tagNode.Attributes["Path"]?.Value ?? string.Empty;

                                //Check
                                if (File.Exists(Path.Combine(directory, localFile)))
                                    TagGroupFile_Import(tagIdLookup, directory, localFile, ref lastId);
                            }
                    }

                    //Compile new sound cache file gestalt
                    using (VirtualStream ughStream = new VirtualStream(m_SoundCacheFileGestaltEntry.Offset))
                    using (BinaryWriter writer = new BinaryWriter(ughStream))
                    {
                        //Write
                        m_SoundCacheFileGestalt.Write(writer);

                        //Get buffer
                        soundCacheFileGestaltTagBuffer = ughStream.ToArray();
                    }

                    //Get tag data offset
                    long tagDataOffset = Map.TagDataStream.MemoryAddress;
                    using (BinaryReader reader = new BinaryReader(Map.TagDataStream))
                    {
                        //Goto start
                        reader.BaseStream.Seek(tagDataOffset, SeekOrigin.Begin);

                        //Write existing tag data
                        int tagDataLength = (int)(m_SoundCacheFileGestaltEntry.Offset - tagDataOffset);
                        tagDataBuffer = reader.ReadBytes(tagDataLength);
                    }
                    
                    using (VirtualStream tagDataStream = new VirtualStream(tagDataOffset))
                    using (BinaryWriter writer = new BinaryWriter(tagDataStream))
                    using (BinaryReader reader = new BinaryReader(tagDataStream))
                    {
                        //Goto start
                        reader.BaseStream.Seek(tagDataOffset, SeekOrigin.Begin);

                        //Write existing tag data
                        writer.Write(tagDataBuffer);

                        //Write modified sound cache file gestalt
                        writer.Write(soundCacheFileGestaltTagBuffer);

                        //Align
                        tagDataStream.Align(1024);

                        /*
                         * tagData.Seek(64, SeekOrigin.Begin);
                         * int soundsCount = reader.ReadInt32();
                         * int soundsOffset = reader.ReadInt32();
                         * for (int i = 0; i < soundsCount; i++)
                         * {
                         *     //Goto
                         *     tagData.Seek(soundsOffset + (i * 12), SeekOrigin.Begin);
                         *     rawOffset = reader.ReadInt32();
                         * 
                         *     //Check
                         *     if ((raw = tagGroupFile.GetRaw(rawOffset)) != null)
                         *         entry.Raws[RawSection.Sound].Add(new RawStream(raw, rawOffset));
                         * }
                         * 
                         * tagData.Seek(80, SeekOrigin.Begin);
                         * int extraInfosCount = reader.ReadInt32();
                         * int extraInfosOffset = reader.ReadInt32();
                         * for (int i = 0; i < extraInfosCount; i++)
                         * {
                         *     //Goto
                         *     tagData.Seek(extraInfosOffset + (i * 44) + 8, SeekOrigin.Begin);
                         *     rawOffset = reader.ReadInt32();
                         * 
                         *     //Check
                         *     if ((raw = tagGroupFile.GetRaw(rawOffset)) != null)
                         *         entry.Raws[RawSection.LipSync].Add(new RawStream(raw, rawOffset));
                         * }
                         */

                        //Relocate sound raws
                        tagDataStream.Seek(m_SoundCacheFileGestaltEntry.Offset + 64, SeekOrigin.Begin);
                        TagBlock chunks = reader.Read<TagBlock>();
                        for (int i = 0; i < chunks.Count; i++)
                        {
                            //Goto
                            tagDataStream.Seek(chunks.Offset + (i * 12), SeekOrigin.Begin);
                            long offsetAddress = tagDataStream.Position;
                            int rawOffset = reader.ReadInt32();

                            //Check
                            if(m_SoundCacheFileGestaltEntry.Raws[RawSection.Sound].ContainsRawOffset(rawOffset))
                            {
                                m_SoundCacheFileGestaltEntry.Raws[RawSection.Sound][rawOffset].OffsetAddresses.Clear();
                                m_SoundCacheFileGestaltEntry.Raws[RawSection.Sound][rawOffset].OffsetAddresses.Add(offsetAddress);
                            }
                        }

                        //Relocate lip-sync raws
                        tagDataStream.Seek(m_SoundCacheFileGestaltEntry.Offset + 80, SeekOrigin.Begin);
                        TagBlock extraInfos = reader.Read<TagBlock>();
                        for (int i = 0; i < extraInfos.Count; i++)
                        {
                            //Goto
                            tagDataStream.Seek(extraInfos.Offset + (i * 44) + 8, SeekOrigin.Begin);
                            long offsetAddress = tagDataStream.Position;
                            int rawOffset = reader.ReadInt32();

                            //Check
                            if (m_SoundCacheFileGestaltEntry.Raws[RawSection.LipSync].ContainsRawOffset(rawOffset))
                            {
                                m_SoundCacheFileGestaltEntry.Raws[RawSection.LipSync][rawOffset].OffsetAddresses.Clear();
                                m_SoundCacheFileGestaltEntry.Raws[RawSection.LipSync][rawOffset].OffsetAddresses.Add(offsetAddress);
                            }
                        }

                        //Swap arrays
                        Map.SwapTagBuffer(tagDataStream.ToArray(), tagDataOffset);
                    }

                    //Write sound cache file gestalt
                    using (BinaryWriter writer = m_SoundCacheFileGestaltEntry.TagData.CreateWriter())
                    {
                        writer.BaseStream.Seek(m_SoundCacheFileGestaltEntry.Offset, SeekOrigin.Begin);
                        m_SoundCacheFileGestalt.Write(writer);
                    }

                    //Import tags
                    Map.AddTags(tagIdLookup.Tags.ToArray());

                    //Import BSP tags
                    Map.AddScenarioStructureTags(tagIdLookup.BspTags.ToArray());

                    //Null
                    tagIdLookup = null;
                    GC.Collect();

                    //Reload
                    Host.Request(this, "ReloadMap");
                }
            }
        }

        private void TagGroupFile_Import(TagsContainer container, string rootDirectory, string localFilePath, ref TagId id)
        {
            //Get file path
            string absoluteFilePath = Path.Combine(rootDirectory, localFilePath);

            //Load tag
            TagGroupFile tagGroupFile = new TagGroupFile();
            tagGroupFile.Load(absoluteFilePath);

            //Check
            var match = Map.IndexEntries.Where(e => $"{e.Filename}.{tagGroupFile.TagGroup.GroupTag}" == localFilePath);
            if (match.Any()) container.IdLookup.Add(localFilePath, match.First().Id);
            else
            {
                //Get base name
                string baseTagName = localFilePath.Substring(0, localFilePath.LastIndexOf('.'));
                
                //Check
                var groupTag = tagGroupFile.TagGroup.GroupTag;

                //Add to lookup
                container.IdLookup.Add(localFilePath, id);

                //Convert
                TagGroup_ToCache(container, tagGroupFile);
                
                //Create placeholder entry
                IndexEntry placeholder = new IndexEntry(new ObjectEntry() { Id = id, Tag = groupTag }, baseTagName, Map.Tags[groupTag]);

                //Prepare memory stream
                using (MemoryStream ms = new MemoryStream())
                using (BinaryWriter writer = new BinaryWriter(ms))
                using (BinaryReader reader = new BinaryReader(ms))
                {
                    //Write tag
                    tagGroupFile.TagGroup.Write(writer);

                    //Populate raws in entry
                    Entry_PopulateRaws(tagGroupFile, placeholder, reader);
                }

                //Build
                if (groupTag == HaloTags.sbsp || groupTag == HaloTags.ltmp)
                {
                    //Show BSP select dialog
                    if (bspSelectDialog.ShowDialog() == DialogResult.OK && bspSelectDialog.SelectedBlockIndex >= 0)
                    {
                        //Create Tag
                        ScenarioStructureTag bspTag = new ScenarioStructureTag(bspSelectDialog.SelectedBlockIndex)
                        {
                            Id = id,
                            TagGroup = tagGroupFile.TagGroup,
                            Name = baseTagName,
                            SourceEntry = placeholder
                        };

                        //Increment
                        id++;

                        //Add to container
                        container.BspTags.Add(bspTag);
                    }
                }
                else
                {
                    //Create Tag
                    Tag tag = new Tag()
                    {
                        Id = id,
                        TagGroup = tagGroupFile.TagGroup,
                        Name = baseTagName,
                        SourceEntry = placeholder
                    };

                    //Increment
                    id++;

                    //Add to container
                    container.Tags.Add(tag);
                }
            }
        }

        private void TagGroup_ToCache(TagsContainer container, TagGroupFile tagGroupFile)
        {
            //Get tag group
            ITagGroup tagGroup = tagGroupFile.TagGroup;

            //Convert any guerilla fields to cache
            for (int i = 0; i < tagGroup.Count; i++)
                TagBlock_ToCache(container, tagGroup[i]);

            //Check
            switch (tagGroup.GroupTag)
            {
                case HaloTags.snd_:
                    SoundGestalt_AddSound(tagGroupFile);
                    break;
                case HaloTags.unic:
                    tagGroup = new MultilingualUnicodeStringList();
                    break;
            }
        }

        private void TagBlock_ToCache(TagsContainer container, ITagBlock tagBlock)
        {
            //Loop
            for (int i = 0; i < tagBlock.Fields.Count; i++)
                tagBlock.Fields[i] = Field_ToCache(container, tagBlock.Fields[i]);
        }

        private Field Field_ToCache(TagsContainer container, Field field)
        {
            //Handle type
            switch (field.Type)
            {
                case FieldType.FieldStringId:
                    StringIdField stringIdField = new StringIdField(field.GetName());
                    if (field.Value is StringValue stringIdValue)
                        if (string.IsNullOrEmpty(stringIdValue.Value))
                            stringIdField.Value = StringId.Zero;
                        else stringIdField.Value = Map.Strings[stringIdValue.Value];
                    return stringIdField;

                case FieldType.FieldOldStringId:
                    OldStringIdField oldStringIdField = new OldStringIdField(field.GetName());
                    if (field.Value is StringValue oldStringIdValue)
                        if (string.IsNullOrEmpty(oldStringIdValue.Value))
                            oldStringIdField.Value = StringId.Zero;
                        else oldStringIdField.Value = Map.Strings[oldStringIdValue.Value];
                    return oldStringIdField;

                case FieldType.FieldTagReference:
                    TagReferenceField tagReferenceField = new TagReferenceField(field.GetName(), ((Abide.Tag.Guerilla.TagReferenceField)field).GroupTag);
                    if (field.Value is StringValue tagReferenceValue)
                    {
                        if (container.IdLookup.ContainsKey(tagReferenceValue.Value))
                            tagReferenceField.Value = new TagReference() { Tag = tagReferenceField.GroupTag, Id = container.IdLookup[tagReferenceValue.Value] };
                        else tagReferenceField.Value = tagReferenceField.Null;
                    }
                    return tagReferenceField;

                case FieldType.FieldTagIndex:
                    TagIndexField tagIndexField = new TagIndexField(field.GetName());
                    if(field.Value is StringValue tagIndexValue)
                    {
                        if (container.IdLookup.ContainsKey(tagIndexValue.Value))
                            tagIndexField.Value = container.IdLookup[tagIndexValue.Value];
                        else tagIndexField.Value = TagId.Null;
                    }
                    return tagIndexField;

                case FieldType.FieldBlock:
                    if (field is BaseBlockField blockField)
                        foreach (ITagBlock tagBlock in blockField.BlockList)
                            TagBlock_ToCache(container, tagBlock);
                    return field;
                case FieldType.FieldStruct:
                    if (field.Value is ITagBlock structField)
                        TagBlock_ToCache(container, structField);
                    return field;
                default: return field;
            }
        }

        private void Entry_PopulateRaws(TagGroupFile tagGroupFile, IndexEntry entry, BinaryReader reader)
        {
            //Prepare
            Stream tagData = reader.BaseStream;
            int rawOffset;
            byte[] raw;

            //Handle
            switch (tagGroupFile.TagGroup.GroupTag)
            {
                #region ugh!
                case HaloTags.ugh_:
                    tagData.Seek(64, SeekOrigin.Begin);
                    int soundsCount = reader.ReadInt32();
                    int soundsOffset = reader.ReadInt32();
                    for (int i = 0; i < soundsCount; i++)
                    {
                        //Goto
                        tagData.Seek(soundsOffset + (i * 12), SeekOrigin.Begin);
                        rawOffset = reader.ReadInt32();

                        //Check
                        if ((raw = tagGroupFile.GetRaw(rawOffset)) != null)
                            entry.Raws[RawSection.Sound].Add(new RawStream(raw, rawOffset));
                    }

                    tagData.Seek(80, SeekOrigin.Begin);
                    int extraInfosCount = reader.ReadInt32();
                    int extraInfosOffset = reader.ReadInt32();
                    for (int i = 0; i < extraInfosCount; i++)
                    {
                        //Goto
                        tagData.Seek(extraInfosOffset + (i * 44) + 8, SeekOrigin.Begin);
                        rawOffset = reader.ReadInt32();

                        //Check
                        if ((raw = tagGroupFile.GetRaw(rawOffset)) != null)
                            entry.Raws[RawSection.LipSync].Add(new RawStream(raw, rawOffset));
                    }
                    break;
                #endregion
                #region mode
                case HaloTags.mode:
                    tagData.Seek(36, SeekOrigin.Begin);
                    int sectionCount = reader.ReadInt32();
                    int sectionOffset = reader.ReadInt32();
                    for (int i = 0; i < sectionCount; i++)
                    {
                        tagData.Seek(sectionOffset + (i * 92) + 56, SeekOrigin.Begin);
                        rawOffset = reader.ReadInt32();

                        //Check
                        if ((raw = tagGroupFile.GetRaw(rawOffset)) != null)
                            entry.Raws[RawSection.Model].Add(new RawStream(raw, rawOffset));
                    }

                    tagData.Seek(116, SeekOrigin.Begin);
                    int prtCount = reader.ReadInt32();
                    int prtOffset = reader.ReadInt32();
                    for (int i = 0; i < prtCount; i++)
                    {
                        tagData.Seek(prtOffset + (i * 88) + 52, SeekOrigin.Begin);
                        rawOffset = reader.ReadInt32();

                        //Check
                        if ((raw = tagGroupFile.GetRaw(rawOffset)) != null)
                            entry.Raws[RawSection.Model].Add(new RawStream(raw, rawOffset));
                    }
                    break;
                #endregion
                #region weat
                case HaloTags.weat:
                    tagData.Seek(0, SeekOrigin.Begin);
                    int particleSystemCount = reader.ReadInt32();
                    int particleSystemOffset = reader.ReadInt32();
                    for (int i = 0; i < particleSystemCount; i++)
                    {
                        tagData.Seek(particleSystemOffset + (i * 140) + 64, SeekOrigin.Begin);
                        rawOffset = reader.ReadInt32();

                        //Check
                        if ((raw = tagGroupFile.GetRaw(rawOffset)) != null)
                            entry.Raws[RawSection.Weather].Add(new RawStream(raw, rawOffset));
                    }
                    break;
                #endregion
                #region DECR
                case HaloTags.DECR:
                    tagData.Seek(56, SeekOrigin.Begin);
                    rawOffset = reader.ReadInt32();

                    //Check
                    if ((raw = tagGroupFile.GetRaw(rawOffset)) != null)
                        entry.Raws[RawSection.DecoratorSet].Add(new RawStream(raw, rawOffset));
                    break;
                #endregion
                #region PRTM
                case HaloTags.PRTM:
                    tagData.Seek(160, SeekOrigin.Begin);
                    rawOffset = reader.ReadInt32();

                    //Check
                    if ((raw = tagGroupFile.GetRaw(rawOffset)) != null)
                        entry.Raws[RawSection.ParticleModel].Add(new RawStream(raw, rawOffset));
                    break;
                #endregion
                #region jmad
                case HaloTags.jmad:
                    tagData.Seek(172, SeekOrigin.Begin);
                    int animationCount = reader.ReadInt32();
                    int animationOffset = reader.ReadInt32();
                    for (int i = 0; i < animationCount; i++)
                    {
                        tagData.Seek(animationOffset + (i * 20) + 4, SeekOrigin.Begin);
                        reader.ReadInt32();
                        rawOffset = reader.ReadInt32();

                        //Check
                        if ((raw = tagGroupFile.GetRaw(rawOffset)) != null)
                            entry.Raws[RawSection.Animation].Add(new RawStream(raw, rawOffset));
                    }
                    break;
                #endregion
                #region bitm
                case HaloTags.bitm:
                    tagData.Seek(68, SeekOrigin.Begin);
                    int bitmapCount = reader.ReadInt32();
                    int bitmapOffset = reader.ReadInt32();
                    for (int i = 0; i < bitmapCount; i++)
                    {
                        //LOD0
                        tagData.Seek(bitmapOffset + (i * 116) + 28, SeekOrigin.Begin);
                        rawOffset = reader.ReadInt32();

                        //Check
                        if ((raw = tagGroupFile.GetRaw(rawOffset)) != null)
                            entry.Raws[RawSection.Bitmap].Add(new RawStream(raw, rawOffset));

                        //LOD1
                        tagData.Seek(bitmapOffset + (i * 116) + 32, SeekOrigin.Begin);
                        rawOffset = reader.ReadInt32();

                        //Check
                        if ((raw = tagGroupFile.GetRaw(rawOffset)) != null)
                            entry.Raws[RawSection.Bitmap].Add(new RawStream(raw, rawOffset));

                        //LOD2
                        tagData.Seek(bitmapOffset + (i * 116) + 36, SeekOrigin.Begin);
                        rawOffset = reader.ReadInt32();

                        //Check
                        if ((raw = tagGroupFile.GetRaw(rawOffset)) != null)
                            entry.Raws[RawSection.Bitmap].Add(new RawStream(raw, rawOffset));

                        //LOD3
                        tagData.Seek(bitmapOffset + (i * 116) + 40, SeekOrigin.Begin);
                        rawOffset = reader.ReadInt32();

                        //Check
                        if ((raw = tagGroupFile.GetRaw(rawOffset)) != null)
                            entry.Raws[RawSection.Bitmap].Add(new RawStream(raw, rawOffset));

                        //LOD4
                        tagData.Seek(bitmapOffset + (i * 116) + 44, SeekOrigin.Begin);
                        rawOffset = reader.ReadInt32();

                        //Check
                        if ((raw = tagGroupFile.GetRaw(rawOffset)) != null)
                            entry.Raws[RawSection.Bitmap].Add(new RawStream(raw, rawOffset));

                        //LOD5
                        tagData.Seek(bitmapOffset + (i * 116) + 48, SeekOrigin.Begin);
                        rawOffset = reader.ReadInt32();

                        //Check
                        if ((raw = tagGroupFile.GetRaw(rawOffset)) != null)
                            entry.Raws[RawSection.Bitmap].Add(new RawStream(raw, rawOffset));
                    }
                    break;
                #endregion
                #region sbsp
                case HaloTags.sbsp:
                    //Goto Clusters
                    tagData.Seek(156, SeekOrigin.Begin);
                    uint clusterCount = reader.ReadUInt32();
                    uint clusterOffset = reader.ReadUInt32();
                    for (int i = 0; i < clusterCount; i++)
                    {
                        tagData.Seek(clusterOffset + (i * 176) + 40, SeekOrigin.Begin);
                        rawOffset = reader.ReadInt32();

                        //Check
                        if ((raw = tagGroupFile.GetRaw(rawOffset)) != null)
                            entry.Raws[RawSection.BSP].Add(new RawStream(raw, rawOffset));
                    }

                    //Goto Geometries definitions
                    tagData.Seek(312, SeekOrigin.Begin);
                    uint geometriesCount = reader.ReadUInt32();
                    uint geometriesOffset = reader.ReadUInt32();
                    for (int i = 0; i < geometriesCount; i++)
                    {
                        tagData.Seek(geometriesOffset + (i * 200) + 40, SeekOrigin.Begin);
                        rawOffset = reader.ReadInt32();

                        //Check
                        if ((raw = tagGroupFile.GetRaw(rawOffset)) != null)
                            entry.Raws[RawSection.BSP].Add(new RawStream(raw, rawOffset));
                    }

                    //Goto Water definitions
                    tagData.Seek(532, SeekOrigin.Begin);
                    uint watersCount = reader.ReadUInt32();
                    uint watersOffset = reader.ReadUInt32();
                    for (int i = 0; i < watersCount; i++)
                    {
                        tagData.Seek(watersOffset + (i * 172) + 16, SeekOrigin.Begin);
                        rawOffset = reader.ReadInt32();

                        //Check
                        if ((raw = tagGroupFile.GetRaw(rawOffset)) != null)
                            entry.Raws[RawSection.BSP].Add(new RawStream(raw, rawOffset));
                    }

                    //Goto Decorators Definitions
                    tagData.Seek(564, SeekOrigin.Begin);
                    uint decoratorsCount = reader.ReadUInt32();
                    uint decoratorsOffset = reader.ReadUInt32();
                    for (int i = 0; i < decoratorsCount; i++)
                    {
                        tagData.Seek(decoratorsOffset + (i * 48) + 16, SeekOrigin.Begin);
                        uint cachesCount = reader.ReadUInt32();
                        uint cachesOffset = reader.ReadUInt32();
                        for (int j = 0; j < cachesCount; j++)
                        {
                            tagData.Seek(cachesOffset + (j * 44), SeekOrigin.Begin);
                            rawOffset = reader.ReadInt32();

                            //Check
                            if ((raw = tagGroupFile.GetRaw(rawOffset)) != null)
                                entry.Raws[RawSection.BSP].Add(new RawStream(raw, rawOffset));
                        }
                    }
                    break;
                #endregion
                #region ltmp
                case HaloTags.ltmp:
                    //Goto Lightmap Groups
                    tagData.Seek(128, SeekOrigin.Begin);
                    uint groupsCount = reader.ReadUInt32();
                    uint groupsPointer = reader.ReadUInt32();
                    for (int i = 0; i < groupsCount; i++)
                    {
                        //Goto Cluster Definitions
                        tagData.Seek(groupsPointer + (i * 104) + 32, SeekOrigin.Begin);
                        uint clustersCount = reader.ReadUInt32();
                        uint clustersOffset = reader.ReadUInt32();
                        for (int j = 0; j < clustersCount; j++)
                        {
                            tagData.Seek(clustersOffset + (j * 84) + 40, SeekOrigin.Begin);
                            rawOffset = reader.ReadInt32();

                            //Check
                            if ((raw = tagGroupFile.GetRaw(rawOffset)) != null)
                                entry.Raws[RawSection.BSP].Add(new RawStream(raw, rawOffset));
                        }

                        //Goto Poop Definitions
                        tagData.Seek(groupsPointer + (i * 104) + 48, SeekOrigin.Begin);
                        uint poopsCount = reader.ReadUInt32();
                        uint poopsOffset = reader.ReadUInt32();
                        for (int j = 0; j < poopsCount; j++)
                        {
                            tagData.Seek(poopsOffset + (j * 84) + 40, SeekOrigin.Begin);
                            rawOffset = reader.ReadInt32();

                            //Check
                            if ((raw = tagGroupFile.GetRaw(rawOffset)) != null)
                                entry.Raws[RawSection.BSP].Add(new RawStream(raw, rawOffset));
                        }

                        //Goto Geometry Buckets
                        tagData.Seek(groupsPointer + (i * 104) + 64, SeekOrigin.Begin);
                        uint bucketsCount = reader.ReadUInt32();
                        uint bucketsOffset = reader.ReadUInt32();
                        for (int j = 0; j < bucketsCount; j++)
                        {
                            tagData.Seek(bucketsOffset + (j * 56) + 12, SeekOrigin.Begin);
                            rawOffset = reader.ReadInt32();

                            //Check
                            if ((raw = tagGroupFile.GetRaw(rawOffset)) != null)
                                entry.Raws[RawSection.BSP].Add(new RawStream(raw, rawOffset));
                        }
                    }
                    break;
                    #endregion
            }
        }
        
        private void SoundGestalt_AddSound(TagGroupFile soundTag)
        {
            //Prepare
            ITagGroup soundCacheFileGestalt = m_SoundCacheFileGestalt;
            ITagGroup cacheFileSound = new Sound();
            ITagGroup sound = soundTag.TagGroup;
            string stringId = string.Empty;
            bool success = false;
            int index = 0;

            //Get tag blocks
            ITagBlock soundCacheFileGestaltBlock = soundCacheFileGestalt[0];
            ITagBlock cacheFileSoundBlock = cacheFileSound[0];
            ITagBlock soundBlock = sound[0];

            //Transfer raws
            foreach (int rawOffset in soundTag.GetRawOffsets()) //SoundCacheFileGestaltFile.SetRaw(rawOffset, soundTag.GetRaw(rawOffset));

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
            using (BinaryReader reader = new BinaryReader(ms))
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
                    ((ITagBlock)extraInfo.Fields[1].Value).Fields[6] = new TagIndexField(string.Empty) { Value = m_SoundCacheFileGestaltEntry.Id };
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
            ITagBlock soundCacheFileGestaltBlock = m_SoundCacheFileGestalt[0];
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
            ITagBlock soundCacheFileGestaltBlock = m_SoundCacheFileGestalt[0];
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
            ITagBlock soundCacheFileGestaltBlock = m_SoundCacheFileGestalt[0];
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
            ITagBlock soundCacheFileGestaltBlock = m_SoundCacheFileGestalt[0];
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
            ITagBlock soundCacheFileGestaltBlock = m_SoundCacheFileGestalt[0];
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

        private class TagsContainer
        {
            public Dictionary<string, TagId> IdLookup { get; } = new Dictionary<string, TagId>();
            public List<ScenarioStructureTag> BspTags { get; } = new List<ScenarioStructureTag>();
            public List<Tag> Tags { get; } = new List<Tag>();
        }
    }
}
