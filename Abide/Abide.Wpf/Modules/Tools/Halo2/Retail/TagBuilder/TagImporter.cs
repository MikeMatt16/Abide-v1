using Abide.AddOnApi;
using Abide.AddOnApi.Wpf.Halo2;
using Abide.Guerilla.Library;
using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2.Retail;
using Abide.HaloLibrary.IO;
using Abide.Tag;
using Abide.Tag.Cache.Generated;
using Abide.Tag.Definition;
using Abide.Wpf.Modules.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Abide.Wpf.Modules.Tools.Halo2.Retail.TagBuilder
{
    [AddOn]
    public sealed class TagImporter : ToolButton
    {
        public TagImporter()
        {
            AddOnName = "Tag Importer";
            AddOnAuthor = "Click16";
            AddOnDescription = "Imports tags into the map.";
            ClickCommand = new ActionCommand(Click);
        }

        private void Click(object obj)
        {
            List<string> strings = new List<string>();
            List<TagReference> tags = new List<TagReference>();
            List<IndexEntry> entries = new List<IndexEntry>();
            TagId currentId = TagId.Null;

            var openDlg = new OpenFileDialog() { Filter = "XML Files (*.xml)|*.xml" };
            if (openDlg.ShowDialog() ?? false)
            {
                XmlDocument document = new XmlDocument();
                document.Load(openDlg.FileName);

                var tagsRoot = Path.GetDirectoryName(openDlg.FileName);
                var manifest = document["AbideTagManifest"];
                var stringManifest = manifest["Strings"];
                var tagNameManifest = manifest["TagNames"];

                foreach (XmlNode node in stringManifest.ChildNodes)
                {
                    var str = node.Attributes["Value"].InnerText;
                    strings.Add(str);
                }

                foreach (XmlNode node in tagNameManifest)
                {
                    var tagName = node.Attributes["Name"].InnerText;
                    var groupTag = new TagFourCc(node.Attributes["GroupTag"].InnerText);
                    tags.Add(new TagReference()
                    {
                        TagName = tagName,
                        GroupTag = groupTag
                    });
                }

                var globals = Map.GetTagById(Map.GlobalsTagId);
                using (var tagData = Map.ReadTagData(globals))
                {
                    tagData.Stream.Seek(globals.MemoryAddress, SeekOrigin.Begin);
                    var globalsTagGroup = TagLookup.CreateTagGroup(globals.GroupTag);
                    globalsTagGroup.Read(tagData.Stream.CreateReader());

                    var soundGlobals = (BlockField)globalsTagGroup.TagBlocks[0].Fields[4];
                    if (soundGlobals.BlockList.Count == 1)
                    {
                        var soundGlobalsBlock = soundGlobals.BlockList[0];
                        currentId = (TagId)soundGlobalsBlock.Fields[4].Value;
                    }
                }

                using (var map = new HaloMap(Map.FileName))
                {
                    var soundCacheFileGestaltEntry = map.IndexEntries[currentId];

                    foreach (var tag in tags)
                    {
                        if (map.IndexEntries.Any(e => e.Filename == tag.TagName && e.Root == tag.GroupTag))
                        {
                            tag.Id = map.IndexEntries.First(e => e.Filename == tag.TagName && e.Root == tag.GroupTag).Id;
                        }
                        else
                        {
                            var tagGroup = TagLookup.CreateTagGroup(tag.GroupTag);
                            tag.FileName = Path.Combine(tagsRoot, $"{tag.TagName}.{tagGroup.GroupName}");
                            
                            var tagGroupFile = new AbideTagGroupFile();
                            tagGroupFile.Load(tag.FileName);

                            var entry = new IndexEntry()
                            {
                                Filename = tag.TagName,
                                Id = currentId++,
                                Tag = map.Tags.First(t => t.Root == tag.GroupTag),
                            };

                            entries.Add(entry);
                            tag.TagGroup = tagGroupFile.TagGroup;
                            tag.Id = entry.Id;

                            foreach (var offset in tagGroupFile.GetRawOffsets())
                            {
                                var resource = tagGroupFile.GetRaw(offset);
                                entry.Resources.AddResource(offset, resource);
                            }
                        }
                    }

                    soundCacheFileGestaltEntry.Id = currentId;

                    int insertionIndex = map.IndexEntries.IndexOf(e => e == soundCacheFileGestaltEntry);
                    foreach (var tag in tags.Where(t => File.Exists(t.FileName)))
                    {
                        ConvertToCache(tag.TagGroup, map, tags);
                        var entry = entries.First(e => e.Id == tag.Id);

                        using (var stream = new MemoryStream())
                        using (var writer = new BinaryWriter(stream))
                        {
                            tag.TagGroup.Write(writer);
                            entry.Data.SetBuffer(stream.ToArray());
                            _ = map.IndexEntries.Insert(insertionIndex, entry);
                        }
                    }

                    map.Save(Path.Combine(tagsRoot, $"{map.Name}.map"));
                }
            }
        }
        private void ConvertToCache(Group tagGroup, HaloMap map, List<TagReference> tags)
        {
            foreach (var tagBlock in tagGroup.TagBlocks)
            {
                ConvertToCache(tagBlock, map, tags);
            }
        }
        private void ConvertToCache(Block tagBlock, HaloMap map, List<TagReference> tags)
        {
            for (int i = 0; i < tagBlock.FieldCount; i++)
            {
                var field = tagBlock.Fields[i];
                switch (field.Type)
                {
                    case FieldType.FieldBlock:
                        foreach (var childBlock in ((BlockField)field).BlockList)
                        {
                            ConvertToCache(childBlock, map, tags);
                        }
                        break;

                    case FieldType.FieldStruct:
                        if (field.Value is Block structBlock)
                        {
                            ConvertToCache(structBlock, map, tags);
                        }
                        break;

                    case FieldType.FieldStringId:
                    case FieldType.FieldOldStringId:
                        if (field.Value is string strValue)
                        {
                            StringId id = StringId.Zero;
                            if (map.Strings.Contains(strValue))
                            {
                                id = StringId.FromString(strValue, map.Strings.IndexOf(strValue));
                            }
                            else
                            {
                                map.Strings.Add(strValue, out id);
                            }

                            var stringIndex = map.Strings.IndexOf(strValue);
                            tagBlock.Fields[i] = new Tag.Cache.StringIdField(field.Name);
                            tagBlock.Fields[i].Value = id;
                        }
                        break;

                    case FieldType.FieldTagReference:
                        if (field.Value is string tagReferenceName)
                        {
                            var tagRef = tags.FirstOrDefault(t => $"{t.TagName}.{t.TagGroup?.GroupName ?? ""}" == tagReferenceName);
                            tagBlock.Fields[i] = new Tag.Cache.TagReferenceField(field.Name, 0);
                            if (tagRef != null)
                            {
                                tagBlock.Fields[i].Value = new Tag.TagReference() { Tag = tagRef.GroupTag, Id = tagRef.Id };
                            }
                        }
                        break;

                    case FieldType.FieldTagIndex:
                        if (field.Value is string tagName)
                        {
                            var tagRef = tags.FirstOrDefault(t => $"{t.TagName}.{t.TagGroup?.GroupName ?? ""}" == tagName);
                            tagBlock.Fields[i] = new Tag.Cache.TagIndexField(field.Name);
                            if (tagRef != null)
                            {
                                tagBlock.Fields[i].Value = tagRef.Id;
                            }
                        }
                        break;
                }
            }
        }

        private class TagReference
        {
            public string TagName { get; set; }
            public TagFourCc GroupTag { get; set; }
            public string FileName { get; set; }
            public TagId Id { get; set; }
            public Group TagGroup { get; set; }
        }
    }
}
