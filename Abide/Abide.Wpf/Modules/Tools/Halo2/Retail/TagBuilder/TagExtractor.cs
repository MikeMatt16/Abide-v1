using Abide.AddOnApi;
using Abide.AddOnApi.Wpf.Halo2;
using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2.Retail;
using Abide.Tag;
using Abide.Wpf.Modules.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace Abide.Wpf.Modules.Tools.Halo2.Retail.TagBuilder
{
    [AddOn]
    public sealed class TagExtractor : ToolButton
    {
        public TagExtractor()
        {
            AddOnName = "Tag Extractor";
            AddOnAuthor = "Click16";
            AddOnDescription = "Extracts the selected tag and any references.";
            ClickCommand = new ActionCommand(Click);
        }

        private void Click(object obj)
        {
            Dictionary<HaloTag, Group> tagDictionary = new Dictionary<HaloTag, Group>();
            Group soundCacheFileGestaltTagGroup = null;

            if (SelectedEntry != null)
            {
                var globals = Map.GetTagById(Map.GlobalsTagId);
                using (var tagData = Map.ReadTagData(globals))
                {
                    _ = tagData.Stream.Seek(globals.MemoryAddress, SeekOrigin.Begin);
                    var globalsTagGroup = Abide.Tag.Cache.Generated.TagLookup.CreateTagGroup(globals.Tag);
                    globalsTagGroup.Read(tagData.Stream.CreateReader());

                    var soundGlobals = (BlockField)globalsTagGroup.TagBlocks[0].Fields[4];
                    if (soundGlobals.BlockList.Count == 1)
                    {
                        var soundGlobalsBlock = soundGlobals.BlockList[0];
                        var soundCacheFileGestaltId = (TagId)soundGlobalsBlock.Fields[4].Value;
                        var soundCacheFileGestalt = Map.GetTagById(soundCacheFileGestaltId);
                        _ = tagData.Stream.Seek(soundCacheFileGestalt.MemoryAddress, SeekOrigin.Begin);
                        soundCacheFileGestaltTagGroup = Abide.Tag.Cache.Generated.TagLookup.CreateTagGroup(soundCacheFileGestalt.Tag);
                        soundCacheFileGestaltTagGroup.Read(tagData.Stream.CreateReader());
                    }
                }

                using (var map = new HaloMap(Map.FileName))
                {
                    FindAllReferences(SelectedEntry, tagDictionary);

                    var selectedEntry = map.IndexEntries[SelectedEntry.Id];
                    var tags = tagDictionary.Reverse();

                    var tagName = $"{Path.GetFileName(SelectedEntry.TagName)}.xml";
                    var saveDlg = new SaveFileDialog()
                    {
                        Filter = "XML Files (*.xml)|*.xml",
                        FileName = tagName
                    };

                    if (saveDlg.ShowDialog() ?? false)
                    {
                        using (var streamWriter = File.CreateText(saveDlg.FileName))
                        {
                            var writer = XmlWriter.Create(streamWriter, new XmlWriterSettings()
                            {
                                Indent = true,
                            });

                            writer.WriteStartDocument();
                            writer.WriteStartElement("AbideTagManifest");
                            writer.WriteStartElement("TagNames");
                            foreach (var tag in tags)
                            {
                                writer.WriteStartElement("Tag");
                                writer.WriteStartAttribute("Name");
                                writer.WriteValue(tag.Key.TagName);
                                writer.WriteEndAttribute();
                                writer.WriteStartAttribute("GroupTag");
                                writer.WriteValue(tag.Key.Tag);
                                writer.WriteEndAttribute();
                                writer.WriteEndElement();
                            }
                            writer.WriteEndElement();

                            writer.WriteEndElement();
                            writer.WriteEndDocument();
                            writer.Close();
                        }

                        string directory = Path.GetDirectoryName(saveDlg.FileName);
                        foreach (var tag in tags)
                        {
                            string tagFileName = Path.Combine(directory, $"{tag.Key.TagName}.{tag.Value.Name}");
                            var file = new Abide.Guerilla.Library.AbideTagGroupFile()
                            {
                                TagGroup = Abide.Guerilla.Library.Convert.ToGuerilla(tag.Value, soundCacheFileGestaltTagGroup, selectedEntry, map)
                            };

                            _ = Directory.CreateDirectory(Path.GetDirectoryName(tagFileName));
                            using (var stream = File.Create(tagFileName))
                            {
                                file.Save(stream);
                            }
                        }
                    }
                }
            }
        }
        private void FindAllReferences(HaloTag tag, Dictionary<HaloTag, Group> tags)
        {
            if (!tags.ContainsKey(tag))
            {
                var tagGroup = Abide.Tag.Cache.Generated.TagLookup.CreateTagGroup(tag.Tag);
                using (var data = Map.ReadTagData(tag))
                {
                    _ = data.Stream.Seek(tag.MemoryAddress, SeekOrigin.Begin);
                    tagGroup.Read(data.Stream.CreateReader());
                    tags.Add(tag, tagGroup);

                    FindAllReferences(tagGroup, tags);
                }
            }
        }
        private void FindAllReferences(Group tagGroup, Dictionary<HaloTag, Group> tags)
        {
            foreach (var tagBlock in tagGroup)
            {
                FindAllReferences(tagBlock, tags);
            }
        }
        private void FindAllReferences(Block tagBlock, Dictionary<HaloTag, Group> tags)
        {
            HaloTag tag;
            foreach (var field in tagBlock)
            {
                switch (field)
                {
                    case BlockField blockField:
                        foreach (var block in blockField.BlockList)
                        {
                            FindAllReferences(block, tags);
                        }
                        break;

                    case StructField structField:
                        FindAllReferences(structField.Value, tags);
                        break;

                    case Tag.Cache.TagReferenceField tagReferenceField:
                        TagReference tagReference = tagReferenceField.Value;
                        tag = Map.GetTagById(tagReference.Id);
                        if (tag != null)
                        {
                            FindAllReferences(tag, tags);
                        }
                        break;

                    case Tag.Cache.TagIndexField tagIdField:
                        TagId tagId = tagIdField.Value;
                        tag = Map.GetTagById(tagId);
                        if (tag != null)
                        {
                            FindAllReferences(tag, tags);
                        }
                        break;
                }
            }
        }
    }
}
