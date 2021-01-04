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
using System.Windows;
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
            List<string> stringList = new List<string>();
            Dictionary<HaloTag, ITagGroup> tagDictionary = new Dictionary<HaloTag, ITagGroup>();
            ITagGroup soundCacheFileGestaltTagGroup = null;

            if (SelectedEntry != null)
            {
                var globals = Map.GetTagById(Map.GlobalsTagId);
                using (var tagData = Map.ReadTagData(globals))
                {
                    tagData.Stream.Seek(globals.MemoryAddress, SeekOrigin.Begin);
                    var globalsTagGroup = Abide.Tag.Cache.Generated.TagLookup.CreateTagGroup(globals.GroupTag);
                    globalsTagGroup.Read(tagData.Stream.CreateReader());

                    var soundGlobals = (BlockField)globalsTagGroup.TagBlocks[0].Fields[4];
                    if (soundGlobals.BlockList.Count == 1)
                    {
                        var soundGlobalsBlock = soundGlobals.BlockList[0];
                        var soundCacheFileGestaltId = (TagId)soundGlobalsBlock.Fields[4].Value;
                        var soundCacheFileGestalt = Map.GetTagById(soundCacheFileGestaltId);
                        tagData.Stream.Seek(soundCacheFileGestalt.MemoryAddress, SeekOrigin.Begin);
                        soundCacheFileGestaltTagGroup = Abide.Tag.Cache.Generated.TagLookup.CreateTagGroup(soundCacheFileGestalt.GroupTag);
                        soundCacheFileGestaltTagGroup.Read(tagData.Stream.CreateReader());
                    }
                }

                using (var map = new HaloMap(Map.FileName))
                {
                    FindAllReferences(SelectedEntry, tagDictionary, stringList);

                    var selectedEntry = map.IndexEntries[SelectedEntry.Id];
                    var strings = stringList.OrderBy(s => s);
                    var tags = tagDictionary.Reverse();

                    var tagName = $"{Path.GetFileName(SelectedEntry.TagName)}.manifest.xml";
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
                            writer.WriteStartElement("Strings");
                            foreach (var str in strings)
                            {
                                writer.WriteStartElement("String");
                                writer.WriteStartAttribute("Value");
                                writer.WriteValue(str);
                                writer.WriteEndAttribute();
                                writer.WriteEndElement();
                            }
                            writer.WriteEndElement();

                            writer.WriteStartElement("TagNames");
                            foreach (var tag in tags)
                            {
                                writer.WriteStartElement("Tag");
                                writer.WriteStartAttribute("Name");
                                writer.WriteValue(tag.Key.TagName);
                                writer.WriteEndAttribute();
                                writer.WriteStartAttribute("GroupTag");
                                writer.WriteValue(tag.Key.GroupTag);
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
                            string tagFileName = Path.Combine(directory, $"{tag.Key.TagName}.{tag.Value.GroupName}");
                            var file = new Abide.Guerilla.Library.AbideTagGroupFile()
                            {
                                Id = tag.Key.Id,
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
        private void FindAllReferences(HaloTag tag, Dictionary<HaloTag, ITagGroup> tags, List<string> strings)
        {
            if (!tags.ContainsKey(tag))
            {
                var tagGroup = Abide.Tag.Cache.Generated.TagLookup.CreateTagGroup(tag.GroupTag);
                using (var data = Map.ReadTagData(tag))
                {
                    data.Stream.Seek(tag.MemoryAddress, SeekOrigin.Begin);
                    tagGroup.Read(data.Stream.CreateReader());
                    tags.Add(tag, tagGroup);

                    FindAllReferences(tagGroup, tags, strings);
                }
            }
        }
        private void FindAllReferences(ITagGroup tagGroup, Dictionary<HaloTag, ITagGroup> tags, List<string> strings)
        {
            foreach (var tagBlock in tagGroup)
            {
                FindAllReferences(tagBlock, tags, strings);
            }
        }
        private void FindAllReferences(ITagBlock tagBlock, Dictionary<HaloTag, ITagGroup> tags, List<string> strings)
        {
            HaloTag tag;
            foreach (var field in tagBlock)
            {
                switch (field)
                {
                    case Abide.Tag.BlockField blockField:
                        foreach (var childBlock in blockField.BlockList)
                        {
                            FindAllReferences(childBlock, tags, strings);
                        }
                        break;

                    case Abide.Tag.StructField structField:
                        if (structField.Value is ITagBlock structBlock)
                        {
                            FindAllReferences(structBlock, tags, strings);
                        }
                        break;

                    case Abide.Tag.Cache.StringIdField stringIdField:
                    case Abide.Tag.Cache.OldStringIdField oldStringIdField:
                        if (field.Value is StringId sid && sid != StringId.Zero)
                        {
                            var str = Map.GetStringById(sid);
                            if (!strings.Contains(str))
                            {
                                strings.Add(str);
                            }
                        }
                        break;

                    case Abide.Tag.Cache.TagReferenceField tagReferenceField:
                        TagReference tagReference = (TagReference)tagReferenceField.Value;
                        tag = Map.GetTagById(tagReference.Id);
                        if (tag != null)
                        {
                            FindAllReferences(tag, tags, strings);
                        }
                        break;

                    case Abide.Tag.Cache.TagIndexField tagIdField:
                        TagId tagId = (TagId)tagIdField.Value;
                        tag = Map.GetTagById(tagId);
                        if (tag != null)
                        {
                            FindAllReferences(tag, tags, strings);
                        }
                        break;
                }
            }
        }
    }
}
