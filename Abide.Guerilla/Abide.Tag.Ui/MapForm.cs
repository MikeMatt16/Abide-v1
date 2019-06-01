using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using Abide.HaloLibrary.IO;
using Abide.Tag.Definition;
using Abide.Tag.Cache.Generated;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Linq;

namespace Abide.Tag.Ui
{
    public partial class MapForm : Form
    {
        private MapFile map = new MapFile();
        private IndexEntry selectedTag = null; 
        private Group tagGroup = null;

        public MapForm()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Open
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                //Setup
                openDlg.Title = "Open Halo 2 Map";
                openDlg.Filter = "Halo Map Files (*.map)|*.map";

                //Show
                if (openDlg.ShowDialog() == DialogResult.OK)
                {
                    //Close
                    map.Close();
                    map.Dispose();

                    //Load
                    map = new MapFile();
                    map.Load(openDlg.FileName);

                    //Build tree view
                    MapFile_BuildTreeView(map);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Open
            using (SaveFileDialog saveDlg = new SaveFileDialog())
            {
                //Setup
                saveDlg.Title = "Save Halo 2 Map as...";
                saveDlg.Filter = "Halo Map Files (*.map)|*.map";

                //Show
                if (saveDlg.ShowDialog() == DialogResult.OK)
                    using (Stream fs = saveDlg.OpenFile()) map.Save(fs);
            }
        }

        private void tagsTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //Check
            if (e.Node.Tag is IndexEntry entry)
                Tag_Selected(entry);
        }

        private void MapFile_BuildTreeView(MapFile mapFile)
        {
            //Begin
            tagsTreeView.BeginUpdate();
            tagsTreeView.TreeViewNodeSorter = null;

            //Clear
            tagsTreeView.Nodes.Clear();

            //Prepare
            string[] parts = null;
            TreeNodeCollection currentCollection = null;
            TreeNode currentNode = null;

            //Loop
            foreach (IndexEntry tag in mapFile.IndexEntries)
            {
                //Setup
                currentNode = null;
                currentCollection = tagsTreeView.Nodes;

                //Break
                parts = tag.Filename.Split('\\');

                //Loop
                for (int i = 0; i < parts.Length - 1; i++)
                {
                    //Create?
                    if (!currentCollection.ContainsKey(parts[i]))
                        currentNode = currentCollection.Add(parts[i]);
                    else currentNode = currentCollection[parts[i]];

                    //Set Name
                    currentNode.Name = currentNode.Text = parts[i];
                    currentCollection = currentNode.Nodes;
                }

                //Prepare
                currentNode = currentCollection.Add(parts[parts.Length - 1]);
                currentNode.Name = currentNode.Text = $"{parts[parts.Length - 1]}.{tag.Root}";
                currentNode.Tag = tag;
            }

            //Sort
            tagsTreeView.TreeViewNodeSorter = new TagNodeSorter();
            tagsTreeView.Sort();

            //End
            tagsTreeView.EndUpdate();

            //Enable
            dumpTagsButton.Enabled = true;
        }

        private void TagGroup_BuildTreeView(TreeView treeView, Group tagGroup)
        {
            //Prepare
            Func<ITagBlock, TreeNode> createBlockNode = null;
            createBlockNode = new Func<ITagBlock, TreeNode>((tagBlock) =>
            {
                //Prepare
                TreeNode child = null;

                //Create Node
                TreeNode blockNode = new TreeNode($"{tagBlock} Size: {tagBlock.Size}");

                //Loop
                foreach (Field field in tagBlock.Fields)
                    switch (field.Type)
                    {
                        case FieldType.FieldStruct:
                            //Get struct
                            var structValue = ((ITagBlock)field.Value);
                            blockNode.Nodes.Add(createBlockNode(structValue));
                            break;
                        case FieldType.FieldBlock:
                            //Get list
                            var blockList = ((BlockField)field).BlockList;

                            //Create
                            child = new TreeNode($"{field.Name} Count: {blockList.Count}");
                            foreach (ITagBlock childTagBlock in blockList)
                                child.Nodes.Add(createBlockNode(childTagBlock));

                            //Add
                            blockNode.Nodes.Add(child);
                            break;
                        case FieldType.FieldData:
                            child = new TreeNode($"{field.Name} Size: {((DataField)field).BufferLength}");
                            blockNode.Nodes.Add(child);
                            break;
                    }

                //Return
                return blockNode;
            });

            //Begin
            treeView.BeginUpdate();

            //Clear
            treeView.Nodes.Clear();

            //Create Nodes
            foreach (Block tagBlock in tagGroup.TagBlocks)
                treeView.Nodes.Add(createBlockNode(tagBlock));

            //End
            treeView.EndUpdate();
        }

        private void Tag_Selected(IndexEntry entry)
        {
            //Initialize
            selectedTag = entry;

            //
            // offsetLabel
            //
            offsetLabel.Text = $"offset: {((uint)entry.PostProcessedOffset)}";

            //
            // visualizeButton
            //
            visualizeButton.Enabled = entry != null;

            //
            // dumpSelectedTagButton
            //
            dumpSelectedTagButton.Enabled = entry != null;
            dumpSelectedTagButton.Text = $"Dump {entry.Root} tag...";

            //
            // dumpBuiltTagButton
            //
            dumpBuiltTagButton.Enabled = entry != null;
            dumpBuiltTagButton.Text = $"Dump rebuilt {entry.Root} tag...";

            //
            // sizeLabel
            //
            sizeLabel.Text = $"{entry.PostProcessedSize} bytes";

            //
            //rebuildTagButton
            //
            rebuildTagButton.Enabled = entry != null;

            //
            // tagGroup
            //
            tagGroup = TagLookup.CreateTagGroup(entry.Root);
            if (tagGroup != null)
            {
                //Initialize Reader
                using (BinaryReader reader = entry.TagData.CreateReader())
                {
                    //Goto
                    entry.TagData.Seek(entry.PostProcessedOffset, SeekOrigin.Begin);

                    //Read
                    tagGroup.Read(reader);
                }

                //Build tree view
                TagGroup_BuildTreeView(tagStructureTreeView, tagGroup);
            }
        }

        private void dumpTagsButton_Click(object sender, EventArgs e)
        {
            //Prepare
            Group tagGroup = null;

            //Initialize
            using (FolderBrowserDialog folderDlg = new FolderBrowserDialog())
            {
                //Setup
                folderDlg.Description = "Browse to tags folder...";

                //Show
                if (folderDlg.ShowDialog() == DialogResult.OK)
                {
                    //Loop
                    foreach (IndexEntry tag in map.IndexEntries)
                        using (BinaryReader reader = tag.TagData.CreateReader())
                        {
                            //Goto
                            tag.TagData.Seek(tag.PostProcessedOffset, SeekOrigin.Begin);

                            //Check
                            if ((tagGroup = TagLookup.CreateTagGroup(tag.Root)) != null)
                            {
#if DEBUG
                                //Debug
                                Console.WriteLine("Reading tag {0}.{1}...", tag.Filename, tag.Root.FourCc);
#endif
                                //Read
                                tagGroup.Read(reader);

                                //Get filename and create directory if needed
                                string filename = Path.Combine(folderDlg.SelectedPath, $"{tag.Filename}.{ tagGroup.Name}");
                                string directory = Path.GetDirectoryName(filename);
                                if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

                                //Create File
                                using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.Read))
                                using (MemoryStream ms = new MemoryStream())
                                {
                                    //Write tag
                                    using (BinaryWriter writer = new BinaryWriter(ms))
                                    {
                                        tagGroup.Write(writer);
                                        fs.Write(ms.GetBuffer(), 0, (int)ms.Position);
                                    }
                                }
                            }
                        }
                }
            }
        }

        private void dumpSelectedTagButton_Click(object sender, EventArgs e)
        {
            //Initialize
            using (FolderBrowserDialog folderDlg = new FolderBrowserDialog())
            {
                //Setup
                folderDlg.Description = "Browse to tags folder...";

                //Show
                if (folderDlg.ShowDialog() == DialogResult.OK)
                {
                    //Goto
                    selectedTag.TagData.Seek((uint)selectedTag.PostProcessedOffset, SeekOrigin.Begin);
                    byte[] tagData = new byte[(uint)selectedTag.PostProcessedSize];
                    selectedTag.TagData.Read(tagData, 0, tagData.Length);

                    //Get filename and create directory if needed
                    string filename = Path.Combine(folderDlg.SelectedPath, $"{selectedTag.Filename}.{selectedTag.Root}");
                    string directory = Path.GetDirectoryName(filename);
                    if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

                    //Create File
                    using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.Read))
                        fs.Write(tagData, 0, tagData.Length);
                }
            }
        }

        private void dumpBuiltTagButton_Click(object sender, EventArgs e)
        {
            //Prepare
            Group tagGroup = null;
            byte[] tagData = null;
            
            //Initialize
            using (FolderBrowserDialog folderDlg = new FolderBrowserDialog())
            {
                //Setup
                folderDlg.Description = "Browse to tags folder...";

                //Show
                if (folderDlg.ShowDialog() == DialogResult.OK)
                {
                    //Check
                    if ((tagGroup = TagLookup.CreateTagGroup(selectedTag.Root)) != null)
                        using (VirtualStream stream = new VirtualStream((uint)selectedTag.PostProcessedOffset))
                        using (BinaryReader reader = selectedTag.TagData.CreateReader())
                        using (BinaryWriter writer = stream.CreateWriter())
                        {
                            //Read
                            selectedTag.TagData.Seek(selectedTag.PostProcessedOffset, SeekOrigin.Begin);
                            tagGroup.Read(reader);

                            //Write
                            tagGroup.Write(writer);

                            //Align
                            if (selectedTag.Root == HaloTags.sbsp || selectedTag.Root == HaloTags.ltmp)
                                stream.Align(4096);

                            //Setup
                            tagData = stream.ToArray();
                        }

                    //Get length
                    rebuildSizeLabel.Text = $"{tagData.Length} bytes";

                    //Get filename and create directory if needed
                    string filename = Path.Combine(folderDlg.SelectedPath, $"{selectedTag.Filename}.{tagGroup.Name}");
                    string directory = Path.GetDirectoryName(filename);
                    if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

                    //Create File
                    using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.Read))
                    using (BinaryWriter writer = new BinaryWriter(fs))
                        fs.Write(tagData, 0, tagData.Length);
                }
            }
        }

        private void rebuildTagButton_Click(object sender, EventArgs e)
        {
            //Prepare
            Group tagGroup = null;
            VirtualStream buildStream = null;

            //Check
            if ((tagGroup = TagLookup.CreateTagGroup(selectedTag.Root)) != null)
                using (VirtualStream stream = new VirtualStream((uint)selectedTag.PostProcessedOffset))
                using (BinaryReader reader = selectedTag.TagData.CreateReader())
                using (BinaryWriter writer = stream.CreateWriter())
                {
                    //Read
                    selectedTag.TagData.Seek(selectedTag.PostProcessedOffset, SeekOrigin.Begin);
                    tagGroup.Read(reader);

                    //Write
                    tagGroup.Write(writer);

                    //Align
                    if (selectedTag.Root == HaloTags.sbsp || selectedTag.Root == HaloTags.ltmp)
                        stream.Align(4096);

                    //Setup
                    buildStream = new VirtualStream(stream.MemoryAddress, stream.ToArray());
                }

            //Get length
            rebuildSizeLabel.Text = $"{buildStream.Length} bytes";

            //Goto
            using (BinaryReader tagReader = buildStream.CreateReader())
                tagGroup.Read(tagReader);

            //Build tree view
            TreeViewForm tf = new TreeViewForm();
            TagGroup_BuildTreeView(tf.Tree, tagGroup);
            tf.Status = rebuildSizeLabel.Text;
            tf.Show();
            tf.Text = $"{tagGroup.Name}";

            //Dispose
            buildStream.Dispose();
        }

        private void rebuildMapButton_Click(object sender, EventArgs e)
        {
            //Prepare
            Group tagGroup = null;
            long virtualAddress = map.TagDataStream.MemoryAddress;
            long address = virtualAddress, size = 0, offset = 0;
            byte[] tagBuffer = null;
            List<IndexEntry> failedTags = new List<IndexEntry>();

            //Prepare
            using (VirtualStream tagDataStream = new VirtualStream(virtualAddress))
            using (BinaryWriter tagDataWriter = tagDataStream.CreateWriter())
            using (BinaryReader tagDataReader = tagDataStream.CreateReader())
            using (BinaryReader reader = map.TagDataStream.CreateReader())
            {
                //Loop
                foreach (IndexEntry tag in map.IndexEntries)
                {
                    //Check
                    if (tag.TagData == map.TagDataStream)
                    {
                        //Write
                        Console.WriteLine("Compiling {0}.{1}", tag.Filename, tag.Root);

                        //Create tag group...
                        tagGroup = TagLookup.CreateTagGroup(tag.Root);

                        //Goto
                        reader.BaseStream.Seek((uint)tag.PostProcessedOffset, SeekOrigin.Begin);

                        //Read tag group...
                        try { tagGroup.Read(reader); }
                        catch
                        {
                            Console.WriteLine($"{tag.Filename}.{tag.Root} failed to read.");
                            continue;
                        }

                        //Build tag
                        using (VirtualStream tagStream = new VirtualStream(virtualAddress + offset))
                        using (BinaryWriter tagWriter = new BinaryWriter(tagStream))
                        using (BinaryReader tagReader = new BinaryReader(tagStream))
                        {
                            //Write
                            tagGroup.Write(tagWriter);

                            //Re-point raws
                            try { Tag_RepointRaw(tag, tagStream, tagReader, tagWriter); }
                            catch
                            {
                                System.Diagnostics.Debugger.Break();
                                Console.WriteLine($"{tag.Filename}.{tag.Root} failed to have its raws repointed.");
                                failedTags.Add(tag);
                            }

                            //Get Buffer
                            tagBuffer = tagStream.ToArray();
                        }

                        //Prepare
                        address = offset + virtualAddress;
                        size = tagBuffer.LongLength;
                        tagDataStream.Seek(address, SeekOrigin.Begin);
                        tagDataWriter.Write(tagBuffer);

                        //Check
                        if (address != offset + virtualAddress)
                            System.Diagnostics.Debugger.Break();

                        //Setup tag
                        tag.PostProcessedOffset = (int)(uint)(address);
                        tag.PostProcessedSize = (int)(uint)size;
                        tag.SetObjectEntry(new ObjectEntry()
                        {
                            Id = tag.Id,
                            Tag = tag.Root,
                            Offset = (uint)(address),
                            Size = (uint)(size),
                        });

                        //Increment offset
                        offset += tagBuffer.Length;
                    }
                    else Console.WriteLine("{0}.{1} is not part of the tag data stream.", tag.Filename, tag.Root.FourCc);
                }

                //Loop
                Console.WriteLine($"{failedTags.Count} tags failed to have their raws repointed.");
                foreach (IndexEntry tag in failedTags)
                    Console.WriteLine($"{tag.Filename}.{tag.Root}");

                //Replace
                map.SwapTagBuffer(tagDataStream.ToArray(), tagDataStream.MemoryAddress);
            }

            //Attempt to re-build
            Console.WriteLine("Verifying build integrity...");

            //Loop
            using (BinaryReader reader = map.TagDataStream.CreateReader())
                foreach (IndexEntry tag in map.IndexEntries)
                {
                    //Check
                    if (tag.TagData == map.TagDataStream)
                    {
                        //Write
                        Console.WriteLine("Compiling {0}.{1}", tag.Filename, tag.Root);

                        //Create tag group...
                        tagGroup = TagLookup.CreateTagGroup(tag.Root);

                        //Goto
                        reader.BaseStream.Seek((uint)tag.PostProcessedOffset, SeekOrigin.Begin);

                        //Read tag group...
                        try { tagGroup.Read(reader); }
                        catch
                        {
                            Console.WriteLine($"{tag.Filename}.{tag.Root} failed to read.");
                            continue;
                        }
                    }
                }

            //Finished
            Console.WriteLine("Rebuild complete.");
        }

        private void rebuildWholeMap_Click(object sender, EventArgs e)
        {
            //Prepare
            long bspAddress = Index.IndexVirtualAddress + map.IndexLength;
            List<Tuple<IndexEntry, ITagGroup, IndexEntry, ITagGroup>> structureBsps = new List<Tuple<IndexEntry, ITagGroup, IndexEntry, ITagGroup>>();

            //Get BSPs
            using (BinaryReader reader = map.Scenario.TagData.CreateReader())
            {
                //Goto
                long scenarioAddress = (uint)map.Scenario.PostProcessedOffset;
                reader.BaseStream.Seek(scenarioAddress + 528, SeekOrigin.Begin);

                //Read
                TagBlock structureBSPs = reader.Read<TagBlock>();
                for (int i = 0; i < structureBSPs.Count; i++)
                {
                    //Goto
                    reader.BaseStream.Seek(structureBSPs.Offset + (i * 68), SeekOrigin.Begin);

                    //Read
                    reader.ReadUInt32();    //bsp address in file
                    reader.ReadUInt32();    //bsp size
                    reader.ReadUInt32();    //bsp memory address
                    reader.ReadUInt32();    //zero
                    TagReference sbspRef = reader.Read<TagReference>();
                    TagReference ltmpRef = reader.Read<TagReference>();

                    //Get entries
                    IndexEntry structureBspEntry = map.IndexEntries[sbspRef.Id];
                    IndexEntry structureLightmapEntry = map.IndexEntries[ltmpRef.Id];

                    //Read
                    Tuple<IndexEntry, ITagGroup, IndexEntry, ITagGroup> bsp = new Tuple<IndexEntry, ITagGroup, IndexEntry, ITagGroup>(
                        structureBspEntry, TagLookup.CreateTagGroup(HaloTags.sbsp), structureLightmapEntry, TagLookup.CreateTagGroup(HaloTags.ltmp));

                    //Read
                    using (BinaryReader bspReader = map.GetBspTagDataStream(i).CreateReader())
                    {
                        //Goto SBSP
                        bspReader.BaseStream.Seek((uint)structureBspEntry.PostProcessedOffset, SeekOrigin.Begin);
                        bsp.Item2.Read(bspReader);

                        //Goto LTMP
                        if (structureLightmapEntry != null)
                        {
                            bspReader.BaseStream.Seek((uint)structureLightmapEntry.PostProcessedOffset, SeekOrigin.Begin);
                            bsp.Item4.Read(bspReader);
                        }
                    }

                    //Add
                    structureBsps.Add(bsp);
                }
            }

            //Prepare
            List<VirtualStream> bspStreams = new List<VirtualStream>();
            foreach (Tuple<IndexEntry, ITagGroup, IndexEntry, ITagGroup> bsp in structureBsps)
            {
                //Create stream
                VirtualStream bspStream = new VirtualStream(bspAddress);
                StructureBspBlockHeader bspHeader = new StructureBspBlockHeader() { StructureBsp = HaloTags.sbsp };

                //Write
                using (var writer = bspStream.CreateWriter())
                {
                    //Skip header
                    bspStream.Seek(StructureBspBlockHeader.Length, SeekOrigin.Current);

                    //Get structure BSP offset
                    bspHeader.StructureBspOffset = (uint)bspStream.Position;

                    //Write structure BSP to stream
                    using (VirtualStream sbspStream = new VirtualStream(bspStream.Position))
                    using (BinaryReader sbspReader = sbspStream.CreateReader())
                    using (BinaryWriter sbspWriter = sbspStream.CreateWriter())
                    {
                        //Write sbsp
                        bsp.Item2.Write(sbspWriter);

                        //Pad stream
                        sbspStream.Align(4096);

                        //Repoint raw
                        Tag_RepointRaw(bsp.Item1, sbspStream, sbspReader, sbspWriter);

                        //Write to bsp stream
                        writer.Write(sbspStream.ToArray());
                        
                        //Set index entry
                        bsp.Item1.PostProcessedOffset = unchecked((int)sbspStream.MemoryAddress);
                        bsp.Item1.PostProcessedSize = (int)sbspStream.Length;
                    }
                    
                    //Check
                    if (bsp.Item3 != null)
                    {
                        //Get structure lightmap offset
                        bspHeader.StructureLightmapOffset = (uint)bspStream.Position;

                        //Write structure LTMP to stream
                        using (VirtualStream ltmpStream = new VirtualStream(bspStream.Position))
                        using (BinaryReader ltmpReader = ltmpStream.CreateReader())
                        using (BinaryWriter ltmpWriter = ltmpStream.CreateWriter())
                        {
                            //Write ltmp
                            bsp.Item4.Write(ltmpWriter);

                            //Pad stream
                            ltmpStream.Align(4096);

                            //Repoint raw
                            Tag_RepointRaw(bsp.Item3, ltmpStream, ltmpReader, ltmpWriter);

                            //Write to bsp stream
                            writer.Write(ltmpStream.ToArray());

                            //Set index entry
                            bsp.Item3.PostProcessedOffset = unchecked((int)ltmpStream.MemoryAddress);
                            bsp.Item3.PostProcessedSize = (int)ltmpStream.Length;
                        }
                    }

                    //Pad stream
                    bspStream.Align(4096);

                    //Get block length
                    bspHeader.BlockLength = (int)bspStream.Length;

                    //Write header
                    bspStream.Seek(bspAddress, SeekOrigin.Begin);
                    writer.Write(bspHeader);
                }

                //Add
                bspStreams.Add(bspStream);
            }

            //Get BSPs
            using (BinaryReader reader = map.Scenario.TagData.CreateReader())
            using (BinaryWriter writer = map.Scenario.TagData.CreateWriter())
            {
                //Goto
                long scenarioAddress = (uint)map.Scenario.PostProcessedOffset;
                reader.BaseStream.Seek(scenarioAddress + 528, SeekOrigin.Begin);

                //Read
                TagBlock structureBSPs = reader.Read<TagBlock>();
                for (int i = 0; i < structureBSPs.Count; i++)
                {
                    //Goto
                    reader.BaseStream.Seek(structureBSPs.Offset + (i * 68), SeekOrigin.Begin);

                    //Read
                    reader.ReadUInt32();                        //bsp address in file
                    writer.Write((uint)bspStreams[i].Length);   //bsp size
                    writer.Write((uint)bspAddress);             //bsp memory address
                    writer.Write(0);                            //zero

                    //Swap BSP tag buffer
                    map.SwapBspTagBuffer(bspStreams[i].ToArray(), i, bspAddress);
                }
            }

            //Get tag data address
            long tagDataAddress = bspAddress + bspStreams.Max(s => s.Length);
            VirtualStream tagDataStream = new VirtualStream(tagDataAddress);

            //Destroy BSP streams
            foreach (var bspStream in bspStreams)
                bspStream.Dispose();

            //Loop
            for (int i = 0; i < map.IndexEntries.Count; i++)
                if (map.IndexEntries[i].Offset > 0 && map.IndexEntries[i].Size > 0)
                    using (VirtualStream tagStream = new VirtualStream(tagDataStream.Position))
                    using (BinaryWriter writer = tagStream.CreateWriter())
                    using (BinaryReader reader = map.IndexEntries[i].TagData.CreateReader())
                    {
                        //Read
                        ITagGroup tagGroup = TagLookup.CreateTagGroup(map.IndexEntries[i].Root);
                        reader.BaseStream.Seek(map.IndexEntries[i].Offset, SeekOrigin.Begin);
                        tagGroup.Read(reader);

                        //Write into buffer
                        tagGroup.Write(writer);

                        //Repoint raw
                        using (BinaryReader tagReader = tagStream.CreateReader())
                            Tag_RepointRaw(map.IndexEntries[i], tagStream, tagReader, writer);

                        //Write to underlying tag data stream
                        tagDataStream.Write(tagStream.ToArray(), 0, (int)tagStream.Length);

                        //Set object entry
                        map.IndexEntries[i].SetObjectEntry(tagGroup.GroupTag, map.IndexEntries[i].Id, (uint)tagStream.Length, (uint)tagStream.MemoryAddress);

                        //Set index entry
                        map.IndexEntries[i].PostProcessedOffset = unchecked((int)tagStream.MemoryAddress);
                        map.IndexEntries[i].PostProcessedSize = (int)tagStream.Length;
                    }

            //Swap
            map.SwapTagBuffer(tagDataStream.ToArray(), tagDataStream.MemoryAddress);

            //Destroy tag data stream
            tagDataStream.Dispose();

            //Collect
            GC.Collect();
        }
        
        private void Tag_ReadRaws(IndexEntry entry, long address, VirtualStream tagData, BinaryReader reader)
        {
            //Null
            int rawOffset, rawSize;
            long offsetAddress, lengthAddress;
            byte[] rawData = null;

            //Prepare
            using (BinaryReader metaReader = tagData.CreateReader())
            using (BinaryWriter metaWriter = tagData.CreateWriter())
                switch (entry.Root)
                {
                    #region ugh!
                    case HaloTags.ugh_:
                        tagData.Seek(address + 64, SeekOrigin.Begin);
                        int soundsCount = metaReader.ReadInt32();
                        int soundsOffset = metaReader.ReadInt32();
                        for (int i = 0; i < soundsCount; i++)
                        {
                            //Goto
                            tagData.Seek(soundsOffset + (i * 12), SeekOrigin.Begin);
                            offsetAddress = tagData.Position;
                            rawOffset = metaReader.ReadInt32();
                            lengthAddress = tagData.Position;
                            rawSize = metaReader.ReadInt32() & 0x7FFFFFFF;   //Mask off the first bit

                            //Check
                            if ((rawOffset & 0xC0000000) == 0)
                                rawData = entry.Raws[RawSection.Sound][rawOffset].ToArray();
                        }

                        tagData.Seek(address + 80, SeekOrigin.Begin);
                        int extraInfosCount = metaReader.ReadInt32();
                        int extraInfosOffset = metaReader.ReadInt32();
                        for (int i = 0; i < extraInfosCount; i++)
                        {
                            //Goto
                            tagData.Seek(extraInfosOffset + (i * 44) + 8, SeekOrigin.Begin);
                            offsetAddress = tagData.Position;
                            rawOffset = metaReader.ReadInt32();
                            lengthAddress = tagData.Position;
                            rawSize = metaReader.ReadInt32();

                            //Check
                            if ((rawOffset & 0xC0000000) == 0)
                                rawData = entry.Raws[RawSection.LipSync][rawOffset].ToArray();
                        }
                        break;
                    #endregion
                    #region mode
                    case HaloTags.mode:
                        tagData.Seek(address + 36, SeekOrigin.Begin);
                        int sectionCount = metaReader.ReadInt32();
                        int sectionOffset = metaReader.ReadInt32();
                        for (int i = 0; i < sectionCount; i++)
                        {
                            tagData.Seek(sectionOffset + (i * 92) + 56, SeekOrigin.Begin);
                            offsetAddress = tagData.Position;
                            rawOffset = metaReader.ReadInt32();
                            lengthAddress = tagData.Position;
                            rawSize = metaReader.ReadInt32();

                            //Check
                            if ((rawOffset & 0xC0000000) == 0)
                                rawData = entry.Raws[RawSection.Model][rawOffset].ToArray();
                        }

                        tagData.Seek(address + 116, SeekOrigin.Begin);
                        int prtCount = metaReader.ReadInt32();
                        int prtOffset = metaReader.ReadInt32();
                        for (int i = 0; i < prtCount; i++)
                        {
                            tagData.Seek(prtOffset + (i * 88) + 52, SeekOrigin.Begin);
                            offsetAddress = tagData.Position;
                            rawOffset = metaReader.ReadInt32();
                            lengthAddress = tagData.Position;
                            rawSize = metaReader.ReadInt32();

                            //Check
                            if ((rawOffset & 0xC0000000) == 0)
                                rawData = entry.Raws[RawSection.Model][rawOffset].ToArray();
                        }
                        break;
                    #endregion
                    #region weat
                    case HaloTags.weat:
                        tagData.Seek(address, SeekOrigin.Begin);
                        int particleSystemCount = metaReader.ReadInt32();
                        int particleSystemOffset = metaReader.ReadInt32();
                        for (int i = 0; i < particleSystemCount; i++)
                        {
                            tagData.Seek(particleSystemOffset + (i * 140) + 64, SeekOrigin.Begin);
                            offsetAddress = tagData.Position;
                            rawOffset = metaReader.ReadInt32();
                            lengthAddress = tagData.Position;
                            rawSize = metaReader.ReadInt32();

                            //Check
                            if ((rawOffset & 0xC0000000) == 0)
                                rawData = entry.Raws[RawSection.Weather][rawOffset].ToArray();
                        }
                        break;
                    #endregion
                    #region DECR
                    case HaloTags.DECR:
                        tagData.Seek(address + 56, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = metaReader.ReadInt32();
                        lengthAddress = tagData.Position;
                        rawSize = metaReader.ReadInt32();

                        //Check
                        if ((rawOffset & 0xC0000000) == 0)
                            rawData = entry.Raws[RawSection.DecoratorSet][rawOffset].ToArray();
                        break;
                    #endregion
                    #region PRTM
                    case HaloTags.PRTM:
                        tagData.Seek(address + 160, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = metaReader.ReadInt32();
                        lengthAddress = tagData.Position;
                        rawSize = metaReader.ReadInt32();

                        //Check
                        if ((rawOffset & 0xC0000000) == 0)
                            rawData = entry.Raws[RawSection.ParticleModel][rawOffset].ToArray();
                        break;
                    #endregion
                    #region jmad
                    case HaloTags.jmad:
                        tagData.Seek(address + 172, SeekOrigin.Begin);
                        int animationCount = metaReader.ReadInt32();
                        int animationOffset = metaReader.ReadInt32();
                        for (int i = 0; i < animationCount; i++)
                        {
                            tagData.Seek(animationOffset + (i * 20) + 4, SeekOrigin.Begin);
                            lengthAddress = tagData.Position;
                            rawSize = metaReader.ReadInt32();
                            offsetAddress = tagData.Position;
                            rawOffset = metaReader.ReadInt32();

                            //Check
                            if ((rawOffset & 0xC0000000) == 0)
                                rawData = entry.Raws[RawSection.Animation][rawOffset].ToArray();
                        }
                        break;
                    #endregion
                    #region bitm
                    case HaloTags.bitm:
                        tagData.Seek(address + 68, SeekOrigin.Begin);
                        int bitmapCount = metaReader.ReadInt32();
                        int bitmapOffset = metaReader.ReadInt32();
                        for (int i = 0; i < bitmapCount; i++)
                        {
                            //LOD0
                            tagData.Seek(bitmapOffset + (i * 116) + 28, SeekOrigin.Begin);
                            offsetAddress = tagData.Position;
                            rawOffset = metaReader.ReadInt32();
                            tagData.Seek(bitmapOffset + (i * 116) + 52, SeekOrigin.Begin);
                            lengthAddress = tagData.Position;
                            rawSize = metaReader.ReadInt32();

                            //Check
                            if ((rawOffset & 0xC0000000) == 0)
                                rawData = entry.Raws[RawSection.Bitmap][rawOffset].ToArray();

                            //LOD1
                            tagData.Seek(bitmapOffset + (i * 116) + 32, SeekOrigin.Begin);
                            offsetAddress = tagData.Position;
                            rawOffset = metaReader.ReadInt32();
                            tagData.Seek(bitmapOffset + (i * 116) + 56, SeekOrigin.Begin);
                            lengthAddress = tagData.Position;
                            rawSize = metaReader.ReadInt32();

                            //Check
                            if ((rawOffset & 0xC0000000) == 0)
                                rawData = entry.Raws[RawSection.Bitmap][rawOffset].ToArray();

                            //LOD2
                            tagData.Seek(bitmapOffset + (i * 116) + 36, SeekOrigin.Begin);
                            offsetAddress = tagData.Position;
                            rawOffset = metaReader.ReadInt32();
                            tagData.Seek(bitmapOffset + (i * 116) + 60, SeekOrigin.Begin);
                            lengthAddress = tagData.Position;
                            rawSize = metaReader.ReadInt32();

                            //Check
                            if ((rawOffset & 0xC0000000) == 0)
                                rawData = entry.Raws[RawSection.Bitmap][rawOffset].ToArray();

                            //LOD3
                            tagData.Seek(bitmapOffset + (i * 116) + 40, SeekOrigin.Begin);
                            offsetAddress = tagData.Position;
                            rawOffset = metaReader.ReadInt32();
                            tagData.Seek(bitmapOffset + (i * 116) + 64, SeekOrigin.Begin);
                            lengthAddress = tagData.Position;
                            rawSize = metaReader.ReadInt32();

                            //Check
                            if ((rawOffset & 0xC0000000) == 0)
                                rawData = entry.Raws[RawSection.Bitmap][rawOffset].ToArray();

                            //LOD4
                            tagData.Seek(bitmapOffset + (i * 116) + 44, SeekOrigin.Begin);
                            offsetAddress = tagData.Position;
                            rawOffset = metaReader.ReadInt32();
                            tagData.Seek(bitmapOffset + (i * 116) + 68, SeekOrigin.Begin);
                            lengthAddress = tagData.Position;
                            rawSize = metaReader.ReadInt32();

                            //Check
                            if ((rawOffset & 0xC0000000) == 0)
                                rawData = entry.Raws[RawSection.Bitmap][rawOffset].ToArray();

                            //LOD5
                            tagData.Seek(bitmapOffset + (i * 116) + 48, SeekOrigin.Begin);
                            offsetAddress = tagData.Position;
                            rawOffset = metaReader.ReadInt32();
                            tagData.Seek(bitmapOffset + (i * 116) + 72, SeekOrigin.Begin);
                            lengthAddress = tagData.Position;
                            rawSize = metaReader.ReadInt32();

                            //Check
                            if ((rawOffset & 0xC0000000) == 0)
                                rawData = entry.Raws[RawSection.Bitmap][rawOffset].ToArray();
                        }
                        break;
                        #endregion
                }
        }

        private void duplicateTagButton_Click(object sender, EventArgs e)
        {
            //Prepare
            Group tagGroup = null;
            IndexEntry last = map.IndexEntries.Last;
            IndexEntry globals = map.IndexEntries.First;

            //Check
            if (last.Root != HaloTags.ugh_) return;
            if (globals.Root != HaloTags.matg) return;

            //Get last ID
            TagId lastId = last.Id;
            lastId.HiWord++;
            lastId.LoWord++;

            //Get new ID
            TagId newId = last.Id;
            
            //Prepare new tag
            byte[] newTagBuffer = null;
            long newTagAddress = 0, newTagSize = 0; string newTagGroup = string.Empty;
            if ((tagGroup = TagLookup.CreateTagGroup(selectedTag.Root)) != null)
                using (VirtualStream tagStream = new VirtualStream((uint)last.PostProcessedOffset))
                using (BinaryReader tagReader = new BinaryReader(tagStream))
                using (BinaryWriter tagWriter = new BinaryWriter(tagStream))
                using (BinaryReader reader = selectedTag.TagData.CreateReader())
                {
                    //Read
                    selectedTag.TagData.Seek((uint)selectedTag.PostProcessedOffset, SeekOrigin.Begin);
                    tagGroup.Read(reader);

                    //Write
                    tagGroup.Write(tagWriter);
                    
                    //Setup new tag
                    newTagBuffer = tagStream.ToArray();
                    newTagAddress = tagStream.MemoryAddress;
                    newTagSize = newTagBuffer.Length;
                    newTagGroup = tagGroup.GroupTag;
                }

            //Prepare last tag
            byte[] lastTagBuffer = null;
            long lastTagAddress = 0, lastTagSize = 0; string lastTagGroup = string.Empty;
            if ((tagGroup = new SoundCacheFileGestalt()) != null)
                using (VirtualStream tagStream = new VirtualStream(newTagAddress + newTagSize))
                using (BinaryReader tagReader = new BinaryReader(tagStream))
                using (BinaryWriter tagWriter = new BinaryWriter(tagStream))
                using (BinaryReader reader = last.TagData.CreateReader())
                {

                    //Read
                    last.TagData.Seek((uint)last.PostProcessedOffset, SeekOrigin.Begin);
                    tagGroup.Read(reader);

                    //Write
                    tagGroup.Write(tagWriter);

                    //Repoint
                    Tag_RepointRaw(last, tagStream, tagReader, tagWriter);

                    //Setup new tag
                    lastTagBuffer = tagStream.ToArray();
                    lastTagAddress = tagStream.MemoryAddress;
                    lastTagSize = lastTagBuffer.Length;
                    lastTagGroup = tagGroup.GroupTag;
                }

            //Change globals reference
            using (BinaryReader reader = globals.TagData.CreateReader())
            using (BinaryWriter writer = globals.TagData.CreateWriter())
            {
                //Read
                globals.TagData.Seek(globals.Offset + 192);
                TagBlock soundGlobalsBlock = reader.Read<TagBlock>();

                //Check
                if (soundGlobalsBlock.Count > 0)
                {
                    //Read
                    globals.TagData.Seek(soundGlobalsBlock.Offset + 32);
                    writer.Write(lastId);
                }
            }

            //Read tag datas
            byte[] tagDatas = null;
            using (VirtualStream tagDataStream = new VirtualStream(map.TagDataStream.MemoryAddress))
            using (BinaryReader tagDataReader = map.TagDataStream.CreateReader())
            using (BinaryWriter tagDataWriter = tagDataStream.CreateWriter())
            {
                //Read all tag datas
                map.TagDataStream.Seek(map.TagDataStream.MemoryAddress, 0);
                tagDataWriter.Write(tagDataReader.ReadBytes((int)(newTagAddress - map.TagDataStream.MemoryAddress)));

                //Write new tag
                tagDataWriter.Write(newTagBuffer);

                //Write sound gestalt
                tagDataWriter.Write(lastTagBuffer);

                //Get buffer
                tagDatas = tagDataStream.ToArray();
            }

            //Change last to new tag
            last.Filename = $@"development\generated\tags\{Guid.NewGuid().ToString()}";
            last.SetObjectEntry(new ObjectEntry()
            {
                Tag = newTagGroup,
                Id = newId,
                Size = (uint)newTagSize,
                Offset = (uint)newTagAddress
            });

            //Add last tag
            map.IndexEntries.Add(new IndexEntry(new ObjectEntry()
            {
                Tag = lastTagGroup,
                Id = lastId,
                Size = (uint)lastTagSize,
                Offset = (uint)lastTagAddress
            }, "i've got a lovely bunch of coconuts", map.Tags[HaloTags.ugh_]));

            //Swap buffer
            map.SwapTagBuffer(tagDatas, globals.Offset);

            //Reload
            //MapFile_BuildTreeView(map);
        }

        private void overwriteButton_Click(object sender, EventArgs e)
        {
            //Prepare
            Group tagGroup = null;
            byte[] tagBuffer = null;

            //Check
            if ((tagGroup = TagLookup.CreateTagGroup(selectedTag.Root)) != null)
                using (VirtualStream stream = new VirtualStream((uint)selectedTag.PostProcessedOffset))
                using (BinaryReader reader = selectedTag.TagData.CreateReader())
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    //Read
                    selectedTag.TagData.Seek(selectedTag.PostProcessedOffset, SeekOrigin.Begin);
                    tagGroup.Read(reader);

                    //Write
                    tagGroup.Write(writer);

                    //Setup
                    tagBuffer = stream.ToArray();
                    rebuildSizeLabel.Text = $"{tagBuffer.Length} bytes";
                }

            //Check
            using (BinaryWriter writer = selectedTag.TagData.CreateWriter())
            {
                //Get length
                int length = tagBuffer.Length;
                if (length > writer.BaseStream.Length) length = (int)writer.BaseStream.Length;

                //Goto and write
                selectedTag.TagData.Seek((uint)selectedTag.PostProcessedOffset, SeekOrigin.Begin);
                writer.Write(tagBuffer, 0, length);
            }
        }

        private void visualizeButton_Click(object sender, EventArgs e)
        {
            //Create form
            using (BlockViewForm blockForm = new BlockViewForm(map, selectedTag))
                blockForm.ShowDialog();
        }

        private void Tag_RepointRaw(IndexEntry tag, VirtualStream tagData, BinaryReader reader, BinaryWriter writer)
        {
            //Prepare
            long offsetAddress = 0, lengthAddress = 0;
            int rawOffset = 0;

            //Handle
            switch (tag.Root)
            {
                #region ugh!
                case HaloTags.ugh_:
                    tagData.Seek(tagData.MemoryAddress + 64, SeekOrigin.Begin);
                    int soundsCount = reader.ReadInt32();
                    int soundsOffset = reader.ReadInt32();
                    for (int i = 0; i < soundsCount; i++)
                    {
                        //Goto
                        tagData.Seek(soundsOffset + (i * 12), SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        lengthAddress = tagData.Position;

                        //Check
                        if (tag.Raws[RawSection.Sound].ContainsRawOffset(rawOffset))
                        {
                            tag.Raws[RawSection.Sound][rawOffset].OffsetAddresses.Clear();
                            tag.Raws[RawSection.Sound][rawOffset].LengthAddresses.Clear();
                            tag.Raws[RawSection.Sound][rawOffset].OffsetAddresses.Add(offsetAddress);
                            tag.Raws[RawSection.Sound][rawOffset].LengthAddresses.Add(lengthAddress);
                        }
                    }

                    tagData.Seek(tagData.MemoryAddress + 80, SeekOrigin.Begin);
                    int extraInfosCount = reader.ReadInt32();
                    int extraInfosOffset = reader.ReadInt32();
                    for (int i = 0; i < extraInfosCount; i++)
                    {
                        //Goto
                        tagData.Seek(extraInfosOffset + (i * 44) + 8, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        lengthAddress = tagData.Position;

                        //Check
                        if (tag.Raws[RawSection.LipSync].ContainsRawOffset(rawOffset))
                        {
                            tag.Raws[RawSection.LipSync][rawOffset].OffsetAddresses.Clear();
                            tag.Raws[RawSection.LipSync][rawOffset].LengthAddresses.Clear();
                            tag.Raws[RawSection.LipSync][rawOffset].OffsetAddresses.Add(offsetAddress);
                            tag.Raws[RawSection.LipSync][rawOffset].LengthAddresses.Add(lengthAddress);
                        }
                    }
                    break;
                #endregion
                #region mode
                case HaloTags.mode:
                    tagData.Seek(tagData.MemoryAddress + 36, SeekOrigin.Begin);
                    int sectionCount = reader.ReadInt32();
                    int sectionOffset = reader.ReadInt32();
                    for (int i = 0; i < sectionCount; i++)
                    {
                        tagData.Seek(sectionOffset + (i * 92) + 56, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        lengthAddress = tagData.Position;

                        //Check
                        if (tag.Raws[RawSection.Model].ContainsRawOffset(rawOffset))
                        {
                            tag.Raws[RawSection.Model][rawOffset].OffsetAddresses.Clear();
                            tag.Raws[RawSection.Model][rawOffset].LengthAddresses.Clear();
                            tag.Raws[RawSection.Model][rawOffset].OffsetAddresses.Add(offsetAddress);
                            tag.Raws[RawSection.Model][rawOffset].LengthAddresses.Add(lengthAddress);
                        }
                    }

                    tagData.Seek(tagData.MemoryAddress + 116, SeekOrigin.Begin);
                    int prtCount = reader.ReadInt32();
                    int prtOffset = reader.ReadInt32();
                    for (int i = 0; i < prtCount; i++)
                    {
                        tagData.Seek(prtOffset + (i * 88) + 52, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        lengthAddress = tagData.Position;

                        //Check
                        if (tag.Raws[RawSection.Model].ContainsRawOffset(rawOffset))
                        {
                            tag.Raws[RawSection.Model][rawOffset].OffsetAddresses.Clear();
                            tag.Raws[RawSection.Model][rawOffset].LengthAddresses.Clear();
                            tag.Raws[RawSection.Model][rawOffset].OffsetAddresses.Add(offsetAddress);
                            tag.Raws[RawSection.Model][rawOffset].LengthAddresses.Add(lengthAddress);
                        }
                    }
                    break;
                #endregion
                #region weat
                case HaloTags.weat:
                    tagData.Seek(tagData.MemoryAddress, SeekOrigin.Begin);
                    int particleSystemCount = reader.ReadInt32();
                    int particleSystemOffset = reader.ReadInt32();
                    for (int i = 0; i < particleSystemCount; i++)
                    {
                        tagData.Seek(particleSystemOffset + (i * 140) + 64, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        lengthAddress = tagData.Position;

                        //Check
                        if (tag.Raws[RawSection.Weather].ContainsRawOffset(rawOffset))
                        {
                            tag.Raws[RawSection.Weather][rawOffset].OffsetAddresses.Clear();
                            tag.Raws[RawSection.Weather][rawOffset].LengthAddresses.Clear();
                            tag.Raws[RawSection.Weather][rawOffset].OffsetAddresses.Add(offsetAddress);
                            tag.Raws[RawSection.Weather][rawOffset].LengthAddresses.Add(lengthAddress);
                        }
                    }
                    break;
                #endregion
                #region DECR
                case HaloTags.DECR:
                    tagData.Seek(tagData.MemoryAddress + 56, SeekOrigin.Begin);
                    offsetAddress = tagData.Position;
                    rawOffset = reader.ReadInt32();
                    lengthAddress = tagData.Position;

                    //Check
                    if (tag.Raws[RawSection.DecoratorSet].ContainsRawOffset(rawOffset))
                    {
                        tag.Raws[RawSection.DecoratorSet][rawOffset].OffsetAddresses.Clear();
                        tag.Raws[RawSection.DecoratorSet][rawOffset].LengthAddresses.Clear();
                        tag.Raws[RawSection.DecoratorSet][rawOffset].OffsetAddresses.Add(offsetAddress);
                        tag.Raws[RawSection.DecoratorSet][rawOffset].LengthAddresses.Add(lengthAddress);
                    }
                    break;
                #endregion
                #region PRTM
                case HaloTags.PRTM:
                    tagData.Seek(tagData.MemoryAddress + 160, SeekOrigin.Begin);
                    offsetAddress = tagData.Position;
                    rawOffset = reader.ReadInt32();
                    lengthAddress = tagData.Position;

                    //Check
                    if (tag.Raws[RawSection.ParticleModel].ContainsRawOffset(rawOffset))
                    {
                        tag.Raws[RawSection.ParticleModel][rawOffset].OffsetAddresses.Clear();
                        tag.Raws[RawSection.ParticleModel][rawOffset].LengthAddresses.Clear();
                        tag.Raws[RawSection.ParticleModel][rawOffset].OffsetAddresses.Add(offsetAddress);
                        tag.Raws[RawSection.ParticleModel][rawOffset].LengthAddresses.Add(lengthAddress);
                    }
                    break;
                #endregion
                #region jmad
                case HaloTags.jmad:
                    tagData.Seek(tagData.MemoryAddress + 172, SeekOrigin.Begin);
                    int animationCount = reader.ReadInt32();
                    int animationOffset = reader.ReadInt32();
                    for (int i = 0; i < animationCount; i++)
                    {
                        tagData.Seek(animationOffset + (i * 20) + 4, SeekOrigin.Begin);
                        lengthAddress = tagData.Position;
                        reader.ReadInt32();
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();

                        //Check
                        if (tag.Raws[RawSection.Animation].ContainsRawOffset(rawOffset))
                        {
                            tag.Raws[RawSection.Animation][rawOffset].OffsetAddresses.Clear();
                            tag.Raws[RawSection.Animation][rawOffset].LengthAddresses.Clear();
                            tag.Raws[RawSection.Animation][rawOffset].OffsetAddresses.Add(offsetAddress);
                            tag.Raws[RawSection.Animation][rawOffset].LengthAddresses.Add(lengthAddress);
                        }
                    }
                    break;
                #endregion
                #region bitm
                case HaloTags.bitm:
                    tagData.Seek(tagData.MemoryAddress + 68, SeekOrigin.Begin);
                    int bitmapCount = reader.ReadInt32();
                    int bitmapOffset = reader.ReadInt32();
                    for (int i = 0; i < bitmapCount; i++)
                    {
                        //LOD0
                        tagData.Seek(bitmapOffset + (i * 116) + 28, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        tagData.Seek(bitmapOffset + (i * 116) + 52, SeekOrigin.Begin);
                        lengthAddress = tagData.Position;

                        //Check
                        if (tag.Raws[RawSection.Bitmap].ContainsRawOffset(rawOffset))
                        {
                            tag.Raws[RawSection.Bitmap][rawOffset].OffsetAddresses.Clear();
                            tag.Raws[RawSection.Bitmap][rawOffset].LengthAddresses.Clear();
                            tag.Raws[RawSection.Bitmap][rawOffset].OffsetAddresses.Add(offsetAddress);
                            tag.Raws[RawSection.Bitmap][rawOffset].LengthAddresses.Add(lengthAddress);
                        }

                        //LOD1
                        tagData.Seek(bitmapOffset + (i * 116) + 32, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        tagData.Seek(bitmapOffset + (i * 116) + 56, SeekOrigin.Begin);
                        lengthAddress = tagData.Position;

                        //Check
                        if (tag.Raws[RawSection.Bitmap].ContainsRawOffset(rawOffset))
                        {
                            tag.Raws[RawSection.Bitmap][rawOffset].OffsetAddresses.Clear();
                            tag.Raws[RawSection.Bitmap][rawOffset].LengthAddresses.Clear();
                            tag.Raws[RawSection.Bitmap][rawOffset].OffsetAddresses.Add(offsetAddress);
                            tag.Raws[RawSection.Bitmap][rawOffset].LengthAddresses.Add(lengthAddress);
                        }

                        //LOD2
                        tagData.Seek(bitmapOffset + (i * 116) + 36, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        tagData.Seek(bitmapOffset + (i * 116) + 60, SeekOrigin.Begin);
                        lengthAddress = tagData.Position;

                        //Check
                        if (tag.Raws[RawSection.Bitmap].ContainsRawOffset(rawOffset))
                        {
                            tag.Raws[RawSection.Bitmap][rawOffset].OffsetAddresses.Clear();
                            tag.Raws[RawSection.Bitmap][rawOffset].LengthAddresses.Clear();
                            tag.Raws[RawSection.Bitmap][rawOffset].OffsetAddresses.Add(offsetAddress);
                            tag.Raws[RawSection.Bitmap][rawOffset].LengthAddresses.Add(lengthAddress);
                        }

                        //LOD3
                        tagData.Seek(bitmapOffset + (i * 116) + 40, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        tagData.Seek(bitmapOffset + (i * 116) + 64, SeekOrigin.Begin);
                        lengthAddress = tagData.Position;

                        //Check
                        if (tag.Raws[RawSection.Bitmap].ContainsRawOffset(rawOffset))
                        {
                            tag.Raws[RawSection.Bitmap][rawOffset].OffsetAddresses.Clear();
                            tag.Raws[RawSection.Bitmap][rawOffset].LengthAddresses.Clear();
                            tag.Raws[RawSection.Bitmap][rawOffset].OffsetAddresses.Add(offsetAddress);
                            tag.Raws[RawSection.Bitmap][rawOffset].LengthAddresses.Add(lengthAddress);
                        }

                        //LOD4
                        tagData.Seek(bitmapOffset + (i * 116) + 44, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        tagData.Seek(bitmapOffset + (i * 116) + 68, SeekOrigin.Begin);
                        lengthAddress = tagData.Position;

                        //Check
                        if (tag.Raws[RawSection.Bitmap].ContainsRawOffset(rawOffset))
                        {
                            tag.Raws[RawSection.Bitmap][rawOffset].OffsetAddresses.Clear();
                            tag.Raws[RawSection.Bitmap][rawOffset].LengthAddresses.Clear();
                            tag.Raws[RawSection.Bitmap][rawOffset].OffsetAddresses.Add(offsetAddress);
                            tag.Raws[RawSection.Bitmap][rawOffset].LengthAddresses.Add(lengthAddress);
                        }

                        //LOD5
                        tagData.Seek(bitmapOffset + (i * 116) + 48, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        tagData.Seek(bitmapOffset + (i * 116) + 72, SeekOrigin.Begin);
                        lengthAddress = tagData.Position;

                        //Check
                        if (tag.Raws[RawSection.Bitmap].ContainsRawOffset(rawOffset))
                        {
                            tag.Raws[RawSection.Bitmap][rawOffset].OffsetAddresses.Clear();
                            tag.Raws[RawSection.Bitmap][rawOffset].LengthAddresses.Clear();
                            tag.Raws[RawSection.Bitmap][rawOffset].OffsetAddresses.Add(offsetAddress);
                            tag.Raws[RawSection.Bitmap][rawOffset].LengthAddresses.Add(lengthAddress);
                        }
                    }
                    break;
                #endregion
                #region sbsp
                case HaloTags.sbsp:
                    //Goto Clusters
                    tagData.Seek(tagData.MemoryAddress + 156, SeekOrigin.Begin);
                    uint clusterCount = reader.ReadUInt32();
                    uint clusterOffset = reader.ReadUInt32();
                    for (int i = 0; i < clusterCount; i++)
                    {
                        tagData.Seek(clusterOffset + (i * 176) + 40, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        lengthAddress = tagData.Position;

                        //Check
                        if (tag.Raws[RawSection.BSP].ContainsRawOffset(rawOffset))
                        {
                            tag.Raws[RawSection.BSP][rawOffset].OffsetAddresses.Clear();
                            tag.Raws[RawSection.BSP][rawOffset].LengthAddresses.Clear();
                            tag.Raws[RawSection.BSP][rawOffset].OffsetAddresses.Add(offsetAddress);
                            tag.Raws[RawSection.BSP][rawOffset].LengthAddresses.Add(lengthAddress);
                        }
                    }

                    //Goto Geometries definitions
                    tagData.Seek(tagData.MemoryAddress + 312, SeekOrigin.Begin);
                    uint geometriesCount = reader.ReadUInt32();
                    uint geometriesOffset = reader.ReadUInt32();
                    for (int i = 0; i < geometriesCount; i++)
                    {
                        tagData.Seek(geometriesOffset + (i * 200) + 40, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        lengthAddress = tagData.Position;

                        //Check
                        if (tag.Raws[RawSection.BSP].ContainsRawOffset(rawOffset))
                        {
                            tag.Raws[RawSection.BSP][rawOffset].OffsetAddresses.Clear();
                            tag.Raws[RawSection.BSP][rawOffset].LengthAddresses.Clear();
                            tag.Raws[RawSection.BSP][rawOffset].OffsetAddresses.Add(offsetAddress);
                            tag.Raws[RawSection.BSP][rawOffset].LengthAddresses.Add(lengthAddress);
                        }
                    }

                    //Goto Water definitions
                    tagData.Seek(tagData.MemoryAddress + 532, SeekOrigin.Begin);
                    uint watersCount = reader.ReadUInt32();
                    uint watersOffset = reader.ReadUInt32();
                    for (int i = 0; i < watersCount; i++)
                    {
                        tagData.Seek(watersOffset + (i * 172) + 16, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        lengthAddress = tagData.Position;

                        //Check
                        if (tag.Raws[RawSection.BSP].ContainsRawOffset(rawOffset))
                        {
                            tag.Raws[RawSection.BSP][rawOffset].OffsetAddresses.Clear();
                            tag.Raws[RawSection.BSP][rawOffset].LengthAddresses.Clear();
                            tag.Raws[RawSection.BSP][rawOffset].OffsetAddresses.Add(offsetAddress);
                            tag.Raws[RawSection.BSP][rawOffset].LengthAddresses.Add(lengthAddress);
                        }
                    }

                    //Goto Decorators Definitions
                    tagData.Seek(tagData.MemoryAddress + 564, SeekOrigin.Begin);
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
                            offsetAddress = tagData.Position;
                            rawOffset = reader.ReadInt32();
                            lengthAddress = tagData.Position;

                            //Check
                            if (tag.Raws[RawSection.BSP].ContainsRawOffset(rawOffset))
                            {
                                tag.Raws[RawSection.BSP][rawOffset].OffsetAddresses.Clear();
                                tag.Raws[RawSection.BSP][rawOffset].LengthAddresses.Clear();
                                tag.Raws[RawSection.BSP][rawOffset].OffsetAddresses.Add(offsetAddress);
                                tag.Raws[RawSection.BSP][rawOffset].LengthAddresses.Add(lengthAddress);
                            }
                        }
                    }
                    break;
                #endregion
                #region ltmp
                case HaloTags.ltmp:
                    //Goto Lightmap Groups
                    tagData.Seek(tagData.MemoryAddress + 128);
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
                            offsetAddress = tagData.Position;
                            rawOffset = reader.ReadInt32();
                            lengthAddress = tagData.Position;

                            //Check
                            if (tag.Raws[RawSection.BSP].ContainsRawOffset(rawOffset))
                            {
                                tag.Raws[RawSection.BSP][rawOffset].OffsetAddresses.Clear();
                                tag.Raws[RawSection.BSP][rawOffset].LengthAddresses.Clear();
                                tag.Raws[RawSection.BSP][rawOffset].OffsetAddresses.Add(offsetAddress);
                                tag.Raws[RawSection.BSP][rawOffset].LengthAddresses.Add(lengthAddress);
                            }
                        }

                        //Goto Poop Definitions
                        tagData.Seek(groupsPointer + (i * 104) + 48, SeekOrigin.Begin);
                        uint poopsCount = reader.ReadUInt32();
                        uint poopsOffset = reader.ReadUInt32();
                        for (int j = 0; j < poopsCount; j++)
                        {
                            tagData.Seek(poopsOffset + (j * 84) + 40, SeekOrigin.Begin);
                            offsetAddress = tagData.Position;
                            rawOffset = reader.ReadInt32();
                            lengthAddress = tagData.Position;

                            //Check
                            if (tag.Raws[RawSection.BSP].ContainsRawOffset(rawOffset))
                            {
                                tag.Raws[RawSection.BSP][rawOffset].OffsetAddresses.Clear();
                                tag.Raws[RawSection.BSP][rawOffset].LengthAddresses.Clear();
                                tag.Raws[RawSection.BSP][rawOffset].OffsetAddresses.Add(offsetAddress);
                                tag.Raws[RawSection.BSP][rawOffset].LengthAddresses.Add(lengthAddress);
                            }
                        }

                        //Goto Geometry Buckets
                        tagData.Seek(groupsPointer + (i * 104) + 64, SeekOrigin.Begin);
                        uint bucketsCount = reader.ReadUInt32();
                        uint bucketsOffset = reader.ReadUInt32();
                        for (int j = 0; j < bucketsCount; j++)
                        {
                            tagData.Seek(bucketsOffset + (j * 56) + 12, SeekOrigin.Begin);
                            offsetAddress = tagData.Position;
                            rawOffset = reader.ReadInt32();
                            lengthAddress = tagData.Position;

                            //Check
                            if (tag.Raws[RawSection.BSP].ContainsRawOffset(rawOffset))
                            {
                                tag.Raws[RawSection.BSP][rawOffset].OffsetAddresses.Clear();
                                tag.Raws[RawSection.BSP][rawOffset].LengthAddresses.Clear();
                                tag.Raws[RawSection.BSP][rawOffset].OffsetAddresses.Add(offsetAddress);
                                tag.Raws[RawSection.BSP][rawOffset].LengthAddresses.Add(lengthAddress);
                            }
                        }
                    }
                    break;
                    #endregion
            }
        }

        public static IndexEntry GetSoundCacheFileGestaltEntry(MapFile map)
        {
            //Check
            if (map.Globals == null) return null;
            TagId soundGestaltId = TagId.Null;

            //Create IO
            using (BinaryReader reader = map.TagDataStream.CreateReader())
            using (BinaryWriter writer = map.TagDataStream.CreateWriter())
            {
                //Goto sound globals
                map.TagDataStream.Seek(map.Globals.Offset + 192);
                TagBlock soundGlobalsBlock = reader.Read<TagBlock>();

                //Check
                if (soundGlobalsBlock.Count > 0)
                {
                    //Write
                    map.TagDataStream.Seek(soundGlobalsBlock.Offset + 32);
                    soundGestaltId = reader.ReadInt32();
                }
            }

            //Return
            return map.IndexEntries[soundGestaltId];
        }

        private class TagNodeSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x is TreeNode node1 && y is TreeNode node2)
                    return Compare(node1, node2);
                else return 0;
            }
            public int Compare(TreeNode node1, TreeNode node2)
            {
                int result = 0;
                IndexEntry entry1 = node1.Tag as IndexEntry;
                IndexEntry entry2 = node2.Tag as IndexEntry;

                if (entry1 == null && entry2 == null) return node1.Name.CompareTo(node2.Name);
                else if (entry1 != null && entry2 == null) result = 1;
                else if (entry1 == null && entry2 != null) result = -1;
                else if (entry1 != null && entry2 != null)
                {
                    result = entry1.Filename.CompareTo(entry2.Filename);
                    if (result == 0) return entry1.Root.CompareTo(entry2.Root);
                }
                return result;
            }
        }
    }
}
