using System;
using System.Windows.Forms;

namespace Abide.Tag.Ui.Guerilla.Controls
{
    public partial class BlockControl : UserControl
    {
        public BlockList List
        {
            get { return blockList; }
            set
            {
                blockList = value;
                blockSelectComboBox.Items.Clear();
                Contents.Enabled = value.Count > 0;
                if (value.Count > 0)
                    foreach (ITagBlock block in value)
                        blockSelectComboBox.Items.Add(block.ToString());
                else blockSelectComboBox.Items.Add("None");

                blockSelectComboBox.SelectedIndex = 0;
                controlsPanel.Visible = blockList.Count > 0;
                expandCollapseButton.Text = controlsPanel.Visible ? "-" : "+";
            }
        }
        public FlowLayoutPanel Contents
        {
            get { return controlsPanel; }
        }
        public string Title
        {
            get { return titleLabel.Text; }
            set { titleLabel.Text = value.ToUpper(); }
        }

        private BlockList blockList = new BlockList(0);

        public BlockControl()
        {
            InitializeComponent();
            controlsPanel.Visible = false;
            blockSelectComboBox.SelectedIndex = 0;
        }

        private void blockSelectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Clear
            controlsPanel.Controls.Clear();

            //Check
            if (blockList.Count > 0)
                Tags.GenerateControls(controlsPanel, blockList[blockSelectComboBox.SelectedIndex]);
        }
        private void expandCollapseButton_Click(object sender, EventArgs e)
        {
            //Toggle
            controlsPanel.Visible = !controlsPanel.Visible;

            //Set
            expandCollapseButton.Text = controlsPanel.Visible ? "-" : "+";
        }
    }
}
