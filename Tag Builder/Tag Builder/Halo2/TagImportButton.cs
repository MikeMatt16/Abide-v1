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
using StringValue = Abide.Tag.Guerilla.StringValue;

namespace Abide.TagBuilder.Halo2
{
    [AddOn]
    public sealed class TagImportButton : AbideMenuButton
    {
        private StructureBspSelectDialog bspSelectDialog = null;

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

            //Check
            if (Map == null) return;
            bspSelectDialog = new StructureBspSelectDialog(Map.Scenario);

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
                    //Get tags and strings
                    XmlNode tags = manifest["Tags"];
                    XmlNode strings = manifest["Strings"];

                    //Create lookup
                    TagsContainer tagIdLookup = new TagsContainer();

                    //Check strings
                    if(strings != null)
                    {
                        //Loop through nodes
                        foreach (XmlNode stringNode in strings.ChildNodes)
                        {
                            if(stringNode.Name == "String")
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
                TagGroup_ToCache(container, tagGroupFile.TagGroup);
                
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

        private void TagGroup_ToCache(TagsContainer container, ITagGroup tagGroup)
        {
            switch (tagGroup.GroupTag)
            {
                case HaloTags.snd_:
                    tagGroup = new Sound();
                    break;
                case HaloTags.unic:
                    tagGroup = new MultilingualUnicodeStringList();
                    break;
                default:
                    for (int i = 0; i < tagGroup.Count; i++)
                        TagBlock_ToCache(container, tagGroup[i]);
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

        private class TagsContainer
        {
            public Dictionary<string, TagId> IdLookup { get; } = new Dictionary<string, TagId>();
            public List<ScenarioStructureTag> BspTags { get; } = new List<ScenarioStructureTag>();
            public List<Tag> Tags { get; } = new List<Tag>();
        }
    }
}
