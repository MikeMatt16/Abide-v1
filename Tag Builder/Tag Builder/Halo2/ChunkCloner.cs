using Abide.AddOnApi;
using Abide.AddOnApi.Halo2;
using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using Abide.HaloLibrary.IO;
using Abide.Tag;
using Abide.Tag.Cache.Generated;
using Abide.Tag.Definition;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Abide.TagBuilder.Halo2
{
    [AddOn]
    public partial class ChunkCloner : AbideTool
    {
        const string AbideClipboardTag = "AbideClipboardTag";
        private IndexEntry currentEntry = null;
        private Group tagGroup = null;
        
        public ChunkCloner()
        {
            InitializeComponent();
        }

        private void ChunkCloner_SelectedEntryChanged(object sender, EventArgs e)
        {
            //Reset
            resetTagToolStripButton.Enabled = false;
            saveTagToolStripButton.Enabled = false;
            popOutToolStripButton.Enabled = false;
            tagStructureTreeView.BeginUpdate();
            tagStructureTreeView.Nodes.Clear();
            currentEntry = null;
            tagGroup = null;
            
            //Check
            if (SelectedEntry != null)
            {
                //Setup
                currentEntry = SelectedEntry;

                //Enable
                resetTagToolStripButton.Enabled = true;
                saveTagToolStripButton.Enabled = true;
                popOutToolStripButton.Enabled = true;

                //Create empty tag group
                tagGroup = TagLookup.CreateTagGroup(currentEntry.Root);

                //Read tag group
                currentEntry.TagData.Seek((uint)currentEntry.PostProcessedOffset, SeekOrigin.Begin);
                using (BinaryReader reader = currentEntry.TagData.CreateReader())
                    tagGroup.Read(reader);

                //Create tree nodes for each block
                foreach (Block tagBlock in tagGroup.TagBlocks)
                {
                    TreeNode tagBlockNode = TagBlock_CreateTreeNode(tagBlock);
                    tagStructureTreeView.Nodes.Add(tagBlockNode);
                    tagBlockNode.Expand();
                }
            }

            //End
            tagStructureTreeView.EndUpdate();
        }

        private TreeNode TagBlock_CreateTreeNode(ITagBlock block)
        {
            //Prepare
            TreeNode node = new TreeNode(TagBlock_GetDisplayName(block)) { Tag = block };

            //Loop through fields
            foreach (Field field in block.Fields)
            {
                //Check type
                switch (field.Type)
                {
                    case FieldType.FieldBlock: node.Nodes.Add(BlockField_CreateTreeNode((BaseBlockField)field)); break;
                    case FieldType.FieldStruct: node.Nodes.Add(StructField_CreateTreeNode(field)); break;
                    case FieldType.FieldData: node.Nodes.Add(DataField_CreateTreeNode((DataField)field)); break;
                }
            }

            //Return
            return node;
        }

        private TreeNode StructField_CreateTreeNode(Field structField)
        {
            //Create tree node for tag block
            return TagBlock_CreateTreeNode((ITagBlock)structField.Value);
        }

        private TreeNode BlockField_CreateTreeNode(BaseBlockField blockField)
        {
            //Prepare
            string displayName = blockField.Name;
            if (string.IsNullOrEmpty(displayName)) displayName = $"unnamed ({blockField.Create().Name})";

            //Create tag block tree node
            TreeNode fieldNode = new TreeNode(displayName) { Tag = blockField };

            //Add children
            foreach (Block tagBlock in blockField.BlockList)
                fieldNode.Nodes.Add(TagBlock_CreateTreeNode(tagBlock));

            //Return
            return fieldNode;
        }

        private TreeNode DataField_CreateTreeNode(DataField dataField)
        {
            //Create tag block tree node
            TreeNode fieldNode = new TreeNode(dataField.Name) { Tag = dataField };

            //Return
            return fieldNode;
        }

        private string TagBlock_GetDisplayName(ITagBlock tagBlock)
        {
            //Find name fields
            Field[] nameFields = tagBlock.Fields.Where(f => f.IsBlockName).ToArray();
            if (nameFields.Length == 0) return tagBlock.DisplayName;

            //Join
            return string.Join(", ", nameFields.Select(f => Field_GetName(f)));
        }

        private string Field_GetName(Field field)
        {
            //Check Type
            switch (field.Type)
            {
                case FieldType.FieldStringId:
                case FieldType.FieldOldStringId:
                    if (field.Value is StringId stringId)
                        return Map.Strings[stringId.Index];
                    break;
                case FieldType.FieldTagReference:
                    if (field.Value is TagReference tagReference)
                    {
                        if (tagReference.Id.IsNull)
                            return $"{tagReference.Tag} null";
                        else return $"{tagReference.Tag} {Map.IndexEntries[tagReference.Id].Filename}.{Map.IndexEntries[tagReference.Id].Root}";
                    }
                    break;
                case FieldType.FieldStruct:
                    return ((ITagBlock)field.Value).DisplayName;
                default: return field.Value.ToString();
            }

            //Default
            return field.Value.ToString();
        }

        private void tagStructureTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //Hide
            deleteBlockToolStripButton.Visible = false;
            cloneBlockToolStripButton.Visible = false;
            addBlockToolStripButton.Visible = false;
            deleteTagBlocksToolStripButton.Visible = false;
            toolStripSeparator2.Visible = false;
            copyToolStripMenuItem.Enabled = false;
            pasteToolStripMenuButton.Enabled = false;
            toolStripSeparator3.Visible = false;
            moveBlockUpToolStripButton.Visible = false;
            moveBlockDownToolStripButton.Visible = false;

            //Prepare
            BaseBlockField tagBlockField = null;
            DataField dataField = null;
            ITagBlock tagBlock = null;
            
            //Check
            if (e.Node.Tag is ITagBlock && e.Node.Parent != null && e.Node.Parent.Tag is BaseBlockField)
            {
                //Get variables
                tagBlock = (ITagBlock)e.Node.Tag;
                tagBlockField = (BaseBlockField)e.Node.Parent.Tag;

                //Show
                toolStripSeparator2.Visible = true;
                deleteBlockToolStripButton.Visible = true;
                copyToolStripMenuItem.Enabled = true;
                pasteToolStripMenuButton.Enabled = true;
                cloneBlockToolStripButton.Visible = tagBlockField.BlockList.Count < tagBlockField.Create().MaximumElementCount;
                toolStripSeparator3.Visible = true;
                moveBlockUpToolStripButton.Visible = true;
                moveBlockDownToolStripButton.Visible = true;
            }
            else if(e.Node.Tag is BaseBlockField)
            {
                //Get variables
                tagBlockField = (BaseBlockField)e.Node.Tag;

                //Show
                toolStripSeparator2.Visible = true;
                deleteTagBlocksToolStripButton.Visible = true;
                copyToolStripMenuItem.Enabled = true;
                pasteToolStripMenuButton.Enabled = true;
                addBlockToolStripButton.Visible = tagBlockField.BlockList.Count < tagBlockField.Create().MaximumElementCount;
            }
            else if(e.Node.Tag is DataField)
            {
                //Get vairables
                dataField = (DataField)e.Node.Tag;

                //Do nothing for now...
                toolStripSeparator2.Visible = true;
                copyToolStripMenuItem.Enabled = true;
                pasteToolStripMenuButton.Enabled = true;
            }
            else if(e.Node.Tag is BaseStructField structField)
            {
                //Show
                toolStripSeparator2.Visible = true;
                copyToolStripMenuItem.Enabled = true;
                pasteToolStripMenuButton.Enabled = true;
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Prepare
            TagClipboardHeader header = new TagClipboardHeader();

            //Initialize IO
            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(ms))
            {
                //Check
                if (tagStructureTreeView.SelectedNode.Tag is ITagBlock tagBlock)
                {
                    //Setup header
                    header = new TagClipboardHeader
                    {
                        BlockCount = 1,
                        BlockName = tagBlock.Name
                    };

                    //Write header
                    writer.Write(header);

                    //Write block
                    using (VirtualStream tagStream = new VirtualStream(ms.Position))
                    using (BinaryWriter tagWriter = new BinaryWriter(tagStream))
                    {
                        //Write
                        tagBlock.Write(tagWriter);

                        //Post write
                        tagBlock.PostWrite(tagWriter);

                        //Write tag buffer
                        writer.Write(tagStream.ToArray());
                    }

                    //Set
                    Clipboard.SetData(AbideClipboardTag, ms);
                }
                else if (tagStructureTreeView.SelectedNode.Tag is BaseStructField structField && structField.Value is ITagBlock structBlock)
                {
                    //Setup header
                    header = new TagClipboardHeader
                    {
                        BlockCount = 1,
                        BlockName = structBlock.Name
                    };

                    //Write header
                    writer.Write(header);

                    //Write struct
                    using (VirtualStream tagStream = new VirtualStream(ms.Position))
                    using (BinaryWriter tagWriter = new BinaryWriter(tagStream))
                    {
                        //Write
                        structBlock.Write(tagWriter);

                        //Post write
                        structBlock.PostWrite(tagWriter);

                        //Write tag buffer
                        writer.Write(tagStream.ToArray());
                    }

                    //Set
                    Clipboard.SetData(AbideClipboardTag, ms);
                }
                else if (tagStructureTreeView.SelectedNode.Tag is BaseBlockField tagBlockField)
                {
                    //Setup header
                    header = new TagClipboardHeader
                    {
                        BlockCount = tagBlockField.BlockList.Count,
                        BlockName = tagBlockField.Create().Name,
                    };

                    //Write header
                    writer.Write(header);

                    //Write blocks
                    foreach (ITagBlock childBlock in tagBlockField.BlockList)
                        childBlock.Write(writer);
                    foreach (ITagBlock childBlock in tagBlockField.BlockList)
                        childBlock.PostWrite(writer);

                    //Set
                    Clipboard.SetData(AbideClipboardTag, ms);
                }
                else if (tagStructureTreeView.SelectedNode.Tag is DataField dataField && dataField.Value is byte[] buffer)
                {
                    //Setup header
                    header = new TagClipboardHeader
                    {
                        ElementSize = dataField.ElementSize,
                        BlockCount = buffer.Length,
                        IsData = true,
                    };

                    //Write data
                    writer.Write(buffer);

                    //Set
                    Clipboard.SetData(AbideClipboardTag, ms);
                }
            }
        }

        private void pasteToolStripMenuButton_Click(object sender, EventArgs e)
        {
            //Get data
            if (!Clipboard.ContainsData(AbideClipboardTag)) return;
            using (MemoryStream ms = (MemoryStream)Clipboard.GetData(AbideClipboardTag))
            using (BinaryReader reader = new BinaryReader(ms))
            {
                //Get header
                TagClipboardHeader header = reader.Read<TagClipboardHeader>();

                //Check selected value
                if(tagStructureTreeView.SelectedNode.Tag is ITagBlock tagBlock && header.BlockCount == 1 && tagBlock.Name == header.BlockName)
                {
                    //Read block
                    tagBlock.Read(reader);

                    //Prepare
                    TreeNode newNode = TagBlock_CreateTreeNode(tagBlock);
                    TreeNode parent = tagStructureTreeView.SelectedNode.Parent;
                    int index = parent.Nodes.IndexOf(tagStructureTreeView.SelectedNode);

                    //Replace
                    parent.Nodes.RemoveAt(index);
                    parent.Nodes.Insert(index, newNode);
                }
                else if(tagStructureTreeView.SelectedNode.Tag is BaseBlockField blockField && blockField.Create().Name == header.BlockName)
                {
                    //Read and add blocks
                    for (int i = 0; i < header.BlockCount; i++)
                    {
                        //Add and read block
                        ITagBlock newBlock = blockField.Add(out bool success);
                        newBlock.Read(reader);

                        //Check
                        if (success)
                        {
                            TreeNode blockNode = TagBlock_CreateTreeNode(newBlock);
                            tagStructureTreeView.SelectedNode.Nodes.Add(blockNode);
                            tagStructureTreeView.SelectedNode.Expand();
                        }
                    }
                }
                else if(tagStructureTreeView.SelectedNode.Tag is BaseStructField structField && structField.Value is ITagBlock tagStruct && tagStruct.Name == header.BlockName && header.BlockCount == 1)
                {
                    //Read block
                    tagStruct.Read(reader);

                    //Prepare
                    TreeNode newNode = TagBlock_CreateTreeNode(tagStruct);
                    TreeNode parent = tagStructureTreeView.SelectedNode.Parent;
                    int index = parent.Nodes.IndexOf(tagStructureTreeView.SelectedNode);

                    //Replace
                    parent.Nodes.RemoveAt(index);
                    parent.Nodes.Insert(index, newNode);
                }
                else if(tagStructureTreeView.SelectedNode.Tag is DataField dataField && header.IsData && dataField.ElementSize == dataField.ElementSize)
                {
                    //Set buffer
                    dataField.Value = reader.ReadBytes(header.BlockCount);
                }
            }
        }

        private void deleteBlockToolStripButton_Click(object sender, EventArgs e)
        {
            //Prepare
            TreeNode selectedNode = tagStructureTreeView.SelectedNode;
            BaseBlockField tagBlockField = null;
            ITagBlock tagBlock = null;

            //Check
            if (selectedNode.Tag is ITagBlock && selectedNode.Parent != null && selectedNode.Parent.Tag is BaseBlockField)
            {
                //Get variables
                tagBlock = (ITagBlock)selectedNode.Tag;
                tagBlockField = (BaseBlockField)selectedNode.Parent.Tag;

                //Delete
                if (tagBlockField.BlockList.Remove(tagBlock)) selectedNode.Remove();
            }
        }

        private void cloneBlockToolStripButton_Click(object sender, EventArgs e)
        {
            //Prepare
            TreeNode selectedNode = tagStructureTreeView.SelectedNode;
            BaseBlockField tagBlockField = null;
            ITagBlock tagBlock = null;

            //Check
            if (selectedNode.Tag is ITagBlock && selectedNode.Parent != null && selectedNode.Parent.Tag is BaseBlockField)
            {
                //Get variables
                tagBlock = (ITagBlock)selectedNode.Tag;
                tagBlockField = (BaseBlockField)selectedNode.Parent.Tag;

                //Add
                ITagBlock newBlock = tagBlockField.Add(out bool success);

                //Check
                if (success)
                {
                    //Clone
                    using (MemoryStream blockStream = new MemoryStream())
                    using (BinaryWriter writer = new BinaryWriter(blockStream))
                    using (BinaryReader reader = new BinaryReader(blockStream))
                    {
                        //Write
                        tagBlock.Write(writer);

                        //Re-read
                        blockStream.Seek(0, SeekOrigin.Begin);
                        newBlock.Read(reader);
                    }

                    //Make Tree Node
                    TreeNode newNode = TagBlock_CreateTreeNode(newBlock);
                    newNode.ForeColor = Color.CornflowerBlue;
                    selectedNode.Parent.Nodes.Add(newNode);

                    //Select
                    tagStructureTreeView.SelectedNode = newNode;
                    newNode.EnsureVisible();
                }
                else MessageBox.Show("Failed to add a new block to the tag block field.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void addBlockToolStripButton_Click(object sender, EventArgs e)
        {
            //Prepare
            TreeNode selectedNode = tagStructureTreeView.SelectedNode;
            BaseBlockField tagBlockField = null;

            //Check
            if (selectedNode.Tag is BaseBlockField)
            {
                //Get variables
                tagBlockField = (BaseBlockField)selectedNode.Tag;

                //Show
                deleteTagBlocksToolStripButton.Visible = true;
                addBlockToolStripButton.Visible = tagBlockField.BlockList.Count < tagBlockField.Create().MaximumElementCount;

                //Add
                ITagBlock newBlock = tagBlockField.Add(out bool success);
                newBlock.Initialize();

                //Check
                if (success)
                {
                    //Make Tree Node
                    TreeNode newNode = TagBlock_CreateTreeNode(newBlock);
                    newNode.ForeColor = Color.CornflowerBlue;
                    selectedNode.Nodes.Add(newNode);
                    
                    //Select
                    tagStructureTreeView.SelectedNode = newNode;
                    newNode.EnsureVisible();
                }
                else MessageBox.Show("Failed to add a new block to the tag block field.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void deleteTagBlocksToolStripButton_Click(object sender, EventArgs e)
        {
            //Prepare
            TreeNode selectedNode = tagStructureTreeView.SelectedNode;
            BaseBlockField tagBlockField = null;

            //Check
            if (selectedNode.Tag is BaseBlockField)
            {
                //Get variables
                tagBlockField = (BaseBlockField)selectedNode.Tag;

                //Clear
                tagBlockField.BlockList.Clear();
                selectedNode.Nodes.Clear();
            }
        }

        private void resetTagToolStripButton_Click(object sender, EventArgs e)
        {
            //Reset
            resetTagToolStripButton.Enabled = false;
            saveTagToolStripButton.Enabled = false;
            popOutToolStripButton.Enabled = false;
            tagStructureTreeView.BeginUpdate();
            tagStructureTreeView.Nodes.Clear();
            tagGroup = null;

            //Check
            if (SelectedEntry != null && SelectedEntry.TagData == Map.TagDataStream)
            {
                //Enable
                resetTagToolStripButton.Enabled = true;
                saveTagToolStripButton.Enabled = true;
                popOutToolStripButton.Enabled = true;

                //Create empty tag group
                tagGroup = TagLookup.CreateTagGroup(SelectedEntry.Root);

                //Read tag group
                SelectedEntry.TagData.Seek((uint)SelectedEntry.PostProcessedOffset, SeekOrigin.Begin);
                using (BinaryReader reader = SelectedEntry.TagData.CreateReader())
                    tagGroup.Read(reader);

                //Create tree nodes for each block
                foreach (Block tagBlock in tagGroup.TagBlocks)
                {
                    TreeNode tagBlockNode = TagBlock_CreateTreeNode(tagBlock);
                    tagStructureTreeView.Nodes.Add(tagBlockNode);
                    tagBlockNode.Expand();
                }
            }

            //End
            tagStructureTreeView.EndUpdate();
        }

        private void saveTagToolStripButton_Click(object sender, EventArgs e)
        {
            //Prepare
            Group tagGroup = null;
            byte[] tagBuffer = null;

            //Prepare
            long bspLength = 0;
            long virtualBspAddress = Index.IndexVirtualAddress + Map.IndexLength;

            if (SelectedEntry.Root == HaloTags.sbsp || SelectedEntry.Root == HaloTags.ltmp)
            {
                //Write structure bsp tags
                for (int i = 0; i < Map.BspCount; i++)
                    using (VirtualStream bspTagDataStream = new VirtualStream(virtualBspAddress))
                    using (BinaryReader reader = Map.GetBspTagDataStream(i).CreateReader())
                    using (BinaryWriter writer = bspTagDataStream.CreateWriter())
                    {
                        //Get entries that are referenced by the BSP index
                        IndexEntry ltmpEntry = Map.IndexEntries.FirstOrDefault(entry => entry.Root == HaloTags.ltmp && entry.TagData == Map.GetBspTagDataStream(i)) ?? null;
                        IndexEntry sbspEntry = Map.IndexEntries.FirstOrDefault(entry => entry.Root == HaloTags.sbsp && entry.TagData == Map.GetBspTagDataStream(i)) ?? null;
                        TagId sbspId = TagId.Null;
                        TagId ltmpId = TagId.Null;

                        //Skip header
                        bspTagDataStream.Seek(StructureBspBlockHeader.Length, SeekOrigin.Current);

                        //Prepare
                        StructureBspBlockHeader blockHeader = new StructureBspBlockHeader() { StructureBsp = "sbsp" };
                        byte[] sbspTagBuffer = new byte[0];
                        byte[] ltmpTagBuffer = new byte[0];

                        //Check sbsp tag
                        if (sbspEntry != null)
                        {
                            //check
                            if (sbspEntry != currentEntry)
                            {
                                //Read
                                tagGroup = new ScenarioStructureBsp();
                                reader.BaseStream.Seek((uint)sbspEntry.PostProcessedOffset, SeekOrigin.Begin);
                                tagGroup.Read(reader);
                            }
                            else tagGroup = this.tagGroup;

                            //Write
                            using (VirtualStream vs = new VirtualStream(bspTagDataStream.Position))
                            using (BinaryReader tagReader = vs.CreateReader())
                            using (BinaryWriter tagWriter = vs.CreateWriter())
                            {
                                //Write
                                tagGroup.Write(tagWriter);

                                //Re-calculate raw addresses
                                sbspEntry.Raws.RecalculateRawAddresses(sbspEntry.Root, vs, tagReader, tagWriter);

                                //Get buffer
                                sbspTagBuffer = vs.ToArray();
                            }

                            //Setup tag
                            sbspEntry.SetObjectEntry(new ObjectEntry()
                            {
                                Tag = sbspEntry.Root,
                                Id = sbspEntry.Id,
                                Offset = 0,
                                Size = 0,
                            });
                            sbspEntry.PostProcessedOffset = (int)bspTagDataStream.Position;
                            sbspEntry.PostProcessedSize = sbspTagBuffer.Length;
                        }

                        //Write structure BSP to stream
                        if (sbspTagBuffer.Length > 0) blockHeader.StructureBspOffset = (uint)bspTagDataStream.Position;
                        writer.Write(sbspTagBuffer);

                        //Check ltmp tag
                        if (ltmpEntry != null)
                        {
                            //check
                            if (ltmpEntry != currentEntry)
                            {
                                //Read
                                tagGroup = new ScenarioStructureLightmap();
                                reader.BaseStream.Seek((uint)ltmpEntry.PostProcessedOffset, SeekOrigin.Begin);
                                tagGroup.Read(reader);
                            }
                            else tagGroup = this.tagGroup;

                            //Write
                            using (VirtualStream vs = new VirtualStream(bspTagDataStream.Position))
                            using (BinaryReader tagReader = vs.CreateReader())
                            using (BinaryWriter tagWriter = vs.CreateWriter())
                            {
                                //Write
                                tagGroup.Write(tagWriter);

                                //Re-point raws
                                ltmpEntry.Raws.RecalculateRawAddresses(ltmpEntry.Root, vs, tagReader, tagWriter);

                                //Get buffer
                                ltmpTagBuffer = vs.ToArray();
                            }

                            //Setup tag
                            ltmpEntry.SetObjectEntry(new ObjectEntry()
                            {
                                Tag = ltmpEntry.Root,
                                Id = ltmpEntry.Id,
                                Offset = 0,
                                Size = 0,
                            });
                            ltmpEntry.PostProcessedOffset = (int)bspTagDataStream.Position;
                            ltmpEntry.PostProcessedSize = ltmpTagBuffer.Length;
                        }

                        //Write structure lightmap to stream
                        if (ltmpTagBuffer.Length > 0) blockHeader.StructureLightmapOffset = (uint)bspTagDataStream.Position;
                        writer.Write(ltmpTagBuffer);

                        //Align
                        bspTagDataStream.Align(1024);

                        //Get block length
                        blockHeader.BlockLength = (int)bspTagDataStream.Length;

                        //Write header
                        bspTagDataStream.Seek(bspTagDataStream.MemoryAddress, SeekOrigin.Begin);
                        writer.Write(blockHeader);

                        //Check
                        if (bspTagDataStream.Length > bspLength)
                            bspLength = bspTagDataStream.Length;

                        //Swap
                        Map.SwapBspTagBuffer(bspTagDataStream.ToArray(), i, bspTagDataStream.MemoryAddress);
                    }
            }
            else bspLength = Map.TagDataStream.MemoryAddress - virtualBspAddress;

            //Write remaining tags
            long originalTagDataStart = Map.IndexEntries.First.Offset;
            using (VirtualStream tagDataStream = new VirtualStream(virtualBspAddress + bspLength))
            using (BinaryReader reader = Map.TagDataStream.CreateReader())
            using (BinaryWriter writer = tagDataStream.CreateWriter())
            {
                //Loop
                foreach (IndexEntry tag in Map.IndexEntries.Where(t => t.TagData == Map.TagDataStream))
                {
                    //Check
                    if (tag != currentEntry)
                    {
                        //Create tag group
                        tagGroup = TagLookup.CreateTagGroup(tag.Root);

                        //Read
                        reader.BaseStream.Seek((uint)tag.PostProcessedOffset, SeekOrigin.Begin);
                        tagGroup.Read(reader);
                    }
                    else tagGroup = this.tagGroup;

                    //Write
                    using (VirtualStream tagStream = new VirtualStream(tagDataStream.Position))
                    using (BinaryReader tagReader = tagStream.CreateReader())
                    using (BinaryWriter tagWriter = tagStream.CreateWriter())
                    {
                        //Write
                        tagGroup.Write(tagWriter);

                        //Re-calculate raw addresses
                        tag.Raws.RecalculateRawAddresses(tag.Root, tagStream, tagReader, tagWriter);

                        //Get buffer
                        tagBuffer = tagStream.ToArray();
                    }

                    //Setup tag
                    tag.SetObjectEntry(new ObjectEntry()
                    {
                        Tag = tag.Root,
                        Id = tag.Id,
                        Offset = (uint)tagDataStream.Position,
                        Size = (uint)tagBuffer.Length,
                    });

                    //Write
                    writer.Write(tagBuffer);
                }

                //Align to 1024
                tagDataStream.Align(1024);

                //Swap tag buffer
                Map.SwapTagBuffer(tagDataStream.ToArray(), tagDataStream.MemoryAddress);
            }

            //Reload
            Host.Request(this, "ReloadMap");
        }
        
        private void moveBlockUpToolStripButton_Click(object sender, EventArgs e)
        {
            //Prepare
            TreeNode selectedNode = tagStructureTreeView.SelectedNode;

            //Check
            if (selectedNode.Tag is ITagBlock block && selectedNode.Parent != null && selectedNode.Parent.Tag is BaseBlockField blockField)
            {
                int index = -1;
                if ((index = blockField.BlockList.IndexOf(block)) >= 0)
                {
                    if (index - 1 >= 0)
                    {
                        //Reposition block
                        blockField.BlockList.Move(index, index - 1);

                        //Debug
                        if(block.Name == "scenario_structure_bsp_reference_block")
                        {
                            Map.MoveBspTagData(index, index - 1);
                        }

                        //Remove and insert tree node
                        TreeNodeCollection owner = selectedNode.Parent.Nodes;
                        selectedNode.Remove();
                        owner.Insert(index - 1, selectedNode);
                        tagStructureTreeView.SelectedNode = selectedNode;
                        selectedNode.EnsureVisible();
                    }
                }
            }
        }

        private void moveBlockDownToolStripButton_Click(object sender, EventArgs e)
        {
            //Prepare
            TreeNode selectedNode = tagStructureTreeView.SelectedNode;

            //Check
            if (selectedNode.Tag is ITagBlock block && selectedNode.Parent != null && selectedNode.Parent.Tag is BaseBlockField blockField)
            {
                int index = -1;
                if ((index = blockField.BlockList.IndexOf(block)) >= 0)
                {
                    if (index + 1 < blockField.BlockList.Count)
                    {
                        //Reposition block
                        blockField.BlockList.Move(index, index + 1);

                        //Debug
                        if (block.Name == "scenario_structure_bsp_reference_block")
                        {
                            Map.MoveBspTagData(index, index + 1);
                        }

                        //Remove and insert tree node
                        TreeNodeCollection owner = selectedNode.Parent.Nodes;
                        selectedNode.Remove();
                        owner.Insert(index + 1, selectedNode);
                        tagStructureTreeView.SelectedNode = selectedNode;
                        selectedNode.EnsureVisible();
                    }
                }
            }
        }

        private void popOutToolStripButton_Click(object sender, EventArgs e)
        {
            // Not implemented
            MessageBox.Show("Not implemented :(");
        }
    }
}
