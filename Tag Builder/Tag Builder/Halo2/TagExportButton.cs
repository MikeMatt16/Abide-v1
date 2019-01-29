using Abide.AddOnApi;
using Abide.AddOnApi.Halo2;
using Abide.Guerilla.Library;
using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using Abide.Tag;
using Abide.Tag.Cache.Generated;
using Abide.Tag.Definition;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace Abide.TagBuilder.Halo2
{
    [AddOn]
    public sealed class TagExportButton : AbideMenuButton
    {
        private IndexEntry m_SoundCacheFileGestaltEntry = null;
        private ITagGroup m_SoundCacheFileGestalt = null;

        public TagExportButton()
        {
            //
            // this
            //
            Author = "Click16";
            Description = "Recursively export the selected tag.";
            Name = "Export Tag";
            Icon = Properties.Resources.tag_extract_16;
            Click += TagExportButton_Click;
        }

        private void TagExportButton_Click(object sender, EventArgs e)
        {
            //Prepare
            TagManifest manifest = null;
            m_SoundCacheFileGestaltEntry = Map.GetSoundCacheFileGestaltEntry();
            m_SoundCacheFileGestalt = null;

            //Check
            if (SelectedEntry != null)
            {
                //Create new FolderBrowserDialog instance
                using (FolderBrowserDialog folderDlg = new FolderBrowserDialog())
                {
                    //Setup
                    folderDlg.Description = "Select the folder you wish to extract the tags to.";
                    manifest = new TagManifest();

                    //Show
                    if (folderDlg.ShowDialog() == DialogResult.OK)
                    {
                        //Read sound cache file gestalt
                        using (BinaryReader reader = m_SoundCacheFileGestaltEntry.TagData.CreateReader())
                        {
                            m_SoundCacheFileGestalt = new SoundCacheFileGestalt();
                            reader.BaseStream.Seek(m_SoundCacheFileGestaltEntry.Offset, SeekOrigin.Begin);
                            m_SoundCacheFileGestalt.Read(reader);
                        }

                        //Export entry
                        IndexEntry_Export(manifest, SelectedEntry, m_SoundCacheFileGestalt, folderDlg.SelectedPath);

                        //Create manifest file
                        XmlWriterSettings settings = new XmlWriterSettings() { Indent = true };
                        using (FileStream fs = new FileStream(Path.Combine(folderDlg.SelectedPath, $"{SelectedEntry.Filename.Split('\\').Last()}.{SelectedEntry.Root}.xml"), FileMode.Create, FileAccess.Write))
                        using (XmlWriter writer = XmlWriter.Create(fs, settings))
                        {
                            //Write document
                            writer.WriteStartDocument();

                            //Write manifest
                            writer.WriteStartElement("Manifest");

                            //Write tags
                            writer.WriteStartElement("Tags");
                            
                            //Loop
                            foreach (string localTag in manifest.TagFileNames)
                            {
                                //Write tag
                                writer.WriteStartElement("Tag");

                                //Write path
                                writer.WriteStartAttribute("Path");
                                writer.WriteValue(localTag);
                                writer.WriteEndAttribute();

                                //End tag
                                writer.WriteEndElement();
                            }

                            //End tags
                            writer.WriteEndElement();

                            //Write strings
                            writer.WriteStartElement("Strings");

                            //Loop
                            foreach (string stringId in manifest.StringIds)
                            {
                                //Write tag
                                writer.WriteStartElement("String");

                                //Write path
                                writer.WriteStartAttribute("value");
                                writer.WriteValue(stringId);
                                writer.WriteEndAttribute();

                                //End tag
                                writer.WriteEndElement();
                            }

                            //End strings
                            writer.WriteEndElement();

                            //End manifest
                            writer.WriteEndElement();

                            //End document
                            writer.WriteEndDocument();
                        }
                    }
                }
            }
        }

        private void IndexEntry_Export(TagManifest manifest, IndexEntry entry, ITagGroup soundCacheFileGestalt, string outputDirectory)
        {
            //Check
            if (entry == null || manifest == null || outputDirectory == null) return;
            if (manifest.TagIdReferences.Contains(entry.Id.Dword)) return;

            //Prepare
            TagGroupFile tagGroupFile = new TagGroupFile() { Id = entry.Id };
            ITagGroup tagGroup = TagLookup.CreateTagGroup(entry.Root);
            string localPath = $"{entry.Filename}.{tagGroup.Name}";
            string absolutePath = Path.Combine(outputDirectory, localPath);

            //Check
            using (BinaryReader reader = entry.TagData.CreateReader())
            {
                //Add reference
                manifest.TagIdReferences.Add(entry.Id.Dword);

                //Read
                entry.TagData.Seek((uint)entry.PostProcessedOffset, SeekOrigin.Begin);
                tagGroup.Read(reader);

                //Get references
                foreach (ITagBlock tagBlock in tagGroup)
                    TagBock_BuildReferences(manifest, tagBlock, outputDirectory);

                //Add file
                manifest.TagFileNames.Add(localPath);

                //Add raws
                foreach (RawSection section in Enum.GetValues(typeof(RawSection)))
                    foreach (RawStream raw in entry.Raws[section])
                        tagGroupFile.SetRaw(raw.RawOffset, raw.ToArray());

                //Convert cache to guerilla
                tagGroup = Guerilla.Library.Convert.ToGuerilla(tagGroup, soundCacheFileGestalt, entry, Map);

                //Create
                Directory.CreateDirectory(Path.GetDirectoryName(absolutePath));

                //Set tag group
                tagGroupFile.TagGroup = tagGroup;

                //Create file
                using (FileStream fs = new FileStream(absolutePath, FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
                    tagGroupFile.Save(fs);
            }
        }

        private void TagBock_BuildReferences(TagManifest manifest, ITagBlock tagBlock, string outputDirectory)
        {
            //Loop through each field
            foreach (Field field in tagBlock.Fields)
                if (field.Type == FieldType.FieldOldStringId && field.Value is StringId oldStringId)
                {
                    if (!manifest.StringIds.Contains(Map.Strings[oldStringId.Index]))
                        manifest.StringIds.Add(Map.Strings[oldStringId.Index]);
                }
                else if (field.Type == FieldType.FieldStringId && field.Value is StringId stringId)
                {
                    if (!manifest.StringIds.Contains(Map.Strings[stringId.Index]))
                        manifest.StringIds.Add(Map.Strings[stringId.Index]);
                }
                else if (field.Type == FieldType.FieldTagReference && field.Value is TagReference tagRef && !tagRef.Id.IsNull)
                {
                    IndexEntry_Export(manifest, Map.IndexEntries[tagRef.Id], m_SoundCacheFileGestalt, outputDirectory);
                }
                else if (field.Type == FieldType.FieldTagIndex && field.Value is TagId id && !id.IsNull)
                {
                    IndexEntry_Export(manifest, Map.IndexEntries[id], m_SoundCacheFileGestalt, outputDirectory);
                }
                else if (field.Type == FieldType.FieldBlock && field is BaseBlockField blockField)
                {
                    foreach (ITagBlock block in blockField.BlockList)
                        TagBock_BuildReferences(manifest, block, outputDirectory);
                }
                else if (field.Type == FieldType.FieldStruct && field.Value is ITagBlock structBlock)
                { TagBock_BuildReferences(manifest, structBlock, outputDirectory); }
        }

        private class TagManifest
        {
            public List<string> TagFileNames { get; } = new List<string>();
            public List<uint> TagIdReferences { get; } = new List<uint>();
            public List<string> StringIds { get; } = new List<string>();
        }
    }
}
