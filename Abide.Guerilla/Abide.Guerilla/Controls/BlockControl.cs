using System;
using System.Windows.Forms;
using Abide.Guerilla;

namespace Abide.Tag.Ui.Guerilla.Controls
{
    public partial class BlockControl : UserControl
    {
        public BlockField Field
        {
            get { return m_Field; }
            set
            {
                bool changed = m_Field != value;
                m_Field = value;
                if (changed) OnFieldChanged(new EventArgs());
            }
        }

        private BlockField m_Field;

        public BlockControl(BlockField blockField) : this()
        {
            Tags.GenerateControls(controlsFlowLayoutPanel, blockField.Create());
            titleLabel.Text = blockField.Name;
            Field = blockField;
        }
        private BlockControl()
        {
            InitializeComponent();
        }
        protected virtual void OnFieldChanged(EventArgs e)
        {
            //Prepare
            blockSelectComboBox.Items.Clear();
            controlsFlowLayoutPanel.Visible = true;
            controlsFlowLayoutPanel.Enabled = true;
            expandCollapseButton.Enabled = true;
            blockSelectComboBox.Enabled = true;
            expandCollapseButton.Text = "-";

            //Loop
            foreach (var tagBlock in m_Field.BlockList) blockSelectComboBox.Items.Add(tagBlock);
            if (blockSelectComboBox.Items.Count > 0) blockSelectComboBox.SelectedIndex = 0;
            else
            {
                expandCollapseButton.Text = "+";
                controlsFlowLayoutPanel.Enabled = false;
                controlsFlowLayoutPanel.Visible = false;
                expandCollapseButton.Enabled = false;
                blockSelectComboBox.Enabled = false;
                blockSelectComboBox.SelectedIndex = -1;
            }
        }
        private void blockSelectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ITagBlock selectedBlock = (ITagBlock)blockSelectComboBox.SelectedItem;
            for (int i = 0; i < selectedBlock.FieldCount; i++)
            {
                switch (selectedBlock[i].Type)
                {
                    case Definition.FieldType.FieldBlock:
                        ((BlockControl)controlsFlowLayoutPanel.Controls[i]).Field = (BlockField)selectedBlock[i];
                        break;
                    case Definition.FieldType.FieldStruct:
                        ((StructControl)controlsFlowLayoutPanel.Controls[i]).Field = (StructField)selectedBlock[i];
                        break;
                    case Definition.FieldType.FieldExplanation:
                        continue;
                    default:
                        ((GuerillaControl)controlsFlowLayoutPanel.Controls[i]).Field = selectedBlock[i];
                        break;
                }
            }
        }
        private void expandCollapseButton_Click(object sender, EventArgs e)
        {
            //Toggle
            controlsFlowLayoutPanel.Visible = !controlsFlowLayoutPanel.Visible;

            //Set
            expandCollapseButton.Text = controlsFlowLayoutPanel.Visible ? "-" : "+";
        }

        private void addBlockButton_Click(object sender, EventArgs e)
        {
            ITagBlock newBlock = m_Field.Add(out bool success);
            if (success)
            {
            }
        }
    }
}
