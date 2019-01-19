using Abide.HaloLibrary.Halo2Map;
using Abide.HaloLibrary.IO;
using Abide.Tag.Cache.Generated;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Abide.Tag.Ui
{
    public partial class BlockViewForm : Form
    {
        private ITagGroup m_TagGroup;
        private readonly MapFile m_Map;
        private readonly IndexEntry m_Entry;

        public BlockViewForm(MapFile map, IndexEntry entry) : this()
        {
            //Setup fields
            m_Map = map ?? throw new ArgumentNullException(nameof(map));
            m_Entry = entry ?? throw new ArgumentNullException(nameof(entry));
            m_TagGroup = TagLookup.CreateTagGroup(entry.Root);

            //Setup tagBlockView
            tagBlockView.DataLength = m_Entry.PostProcessedSize;
        }
        private BlockViewForm()
        {
            InitializeComponent();
        }

        private void BlockViewForm_Load(object sender, EventArgs e)
        {
            //Begin
            tagBlockView.BeginUpdate();

            //Clear
            tagBlockView.Clear();

            //Load
            if (m_TagGroup != null)
            {
                //Setup
                Text = m_TagGroup.Name;

                //Read
                using (var reader = m_Entry.TagData.CreateReader())
                {
                    //Goto
                    m_Entry.TagData.Seek((uint)m_Entry.PostProcessedOffset, SeekOrigin.Begin);

                    //Read
                    m_TagGroup.Read(reader);
                }

                //Start
                long baseOffset = (uint)m_Entry.PostProcessedOffset;
                long offset = (uint)m_Entry.PostProcessedOffset;

                //Create blocks
                foreach (ITagBlock block in m_TagGroup)
                    TagBlock_CreateDataBlock(block, baseOffset, ref offset);

                //Create child blocks
                foreach (ITagBlock block in m_TagGroup)
                    TagBlock_CreateChildBlocks(block, baseOffset, ref offset);
            }

            //End
            tagBlockView.EndUpdate();
        }

        private void TagBlock_CreateDataBlock(ITagBlock block, long baseOffset, ref long offset)
        {
            //Preapre
            int localOffset = (int)(offset - baseOffset);

            //Create
            using (VirtualStream vs = new VirtualStream(offset))
            using (BinaryWriter writer = new BinaryWriter(vs))
            {
                //Align
                vs.Align(block.Alignment);

                //Writer
                block.Write(writer);

                //Add
                tagBlockView.Blocks.Add(localOffset, (int)vs.Length, block.Name);
                offset += vs.Length;
            }
        }

        private void TagBlock_CreateChildBlocks(ITagBlock block, long baseOffset, ref long offset)
        {
            //Loop through fields
            foreach (Field field in block.Fields)
            {
                switch (field.Type)
                {
                    case Definition.FieldType.FieldStruct:
                        BaseStructField structField = (BaseStructField)field;
                        TagBlock_CreateChildBlocks((ITagBlock)structField.Value, baseOffset, ref offset);
                        break;
                    case Definition.FieldType.FieldBlock:
                        BaseBlockField blockField = (BaseBlockField)field;
                        BlockField_CreateDataBlock(blockField, baseOffset, ref offset);
                        break;
                    case Definition.FieldType.FieldData:
                        DataField dataField = (DataField)field;
                        int localOffset = (int)(dataField.DataAddress - baseOffset);
                        tagBlockView.Blocks.Add(localOffset, dataField.BufferLength, block.Name);
                        offset = dataField.DataAddress + dataField.BufferLength;
                        break;
                }
            }
        }

        private void BlockField_CreateDataBlock(BaseBlockField field, long baseOffset, ref long offset)
        {
            //Create a condensed block
            if (field.BlockList.Count > 0)
            {
                //Prepare
                ITagBlock block = field.BlockList[0];
                int localOffset = (int)(field.BlockAddress - baseOffset);

                //Create
                using (VirtualStream vs = new VirtualStream(offset))
                using (BinaryWriter writer = new BinaryWriter(vs))
                {
                    //Align
                    vs.Align(block.Alignment);

                    //Write
                    foreach (ITagBlock child in field.BlockList)
                        child.Write(writer);

                    //Add
                    tagBlockView.Blocks.Add(localOffset, (int)vs.Length, block.Name);
                    offset = field.BlockAddress + vs.Length;
                }
            }

            //Create child blocks
            foreach (ITagBlock block in field.BlockList)
                TagBlock_CreateChildBlocks(block, baseOffset, ref offset);
        }
    }
}
