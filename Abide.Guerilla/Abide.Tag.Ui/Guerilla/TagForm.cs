using Abide.HaloLibrary.Halo2Map;
using Abide.Tag.Cache.Generated;
using Abide.Tag.Definition;
using Abide.Tag.Ui.Guerilla.Controls;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Abide.Tag.Ui.Guerilla
{
    public partial class TagForm : Form
    {
        public MapFile Map { get; }
        public new IndexEntry Tag { get; }

        private Group tagGroup;

        public TagForm(MapFile map, IndexEntry tag) : this()
        {
            //
            // this
            //
            Text = $"{tag.Filename}.{tag.Root}";
            Map = map;
            Tag = tag;

            //
            // gueriallFlowLayoutPanel
            //
            gueriallFlowLayoutPanel.Controls.Clear();

            //
            // tagGroup
            //
            if ((tagGroup = TagLookup.CreateTagGroup(tag.Root)) != null)
            {
                tag.TagData.Seek((uint)tag.PostProcessedOffset, SeekOrigin.Begin);
                using (BinaryReader reader = tag.TagData.CreateReader())
                    tagGroup.Read(reader);
                foreach (Block block in tagGroup)
                    Tags.GenerateControls(gueriallFlowLayoutPanel, block);
            }
        }
        private TagForm()
        {
            InitializeComponent();
        }
        
        private void EnumControl_ValueChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void FlagsControl_ValueChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ValueControl_ValueChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void StringControl_ValueChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
