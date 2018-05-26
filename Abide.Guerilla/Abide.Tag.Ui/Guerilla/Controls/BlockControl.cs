using System.Windows.Forms;

namespace Abide.Tag.Ui.Guerilla.Controls
{
    public partial class BlockControl : UserControl
    {
        public FlowLayoutPanel Contents
        {
            get { return controlsPanel; }
        }
        public string Title
        {
            get { return titleLabel.Text; }
            set { titleLabel.Text = value.ToUpper(); }
        }

        public BlockControl()
        {
            InitializeComponent();
            controlsPanel.Visible = false;
        }

        private void expandCollapseButton_Click(object sender, System.EventArgs e)
        {
            //Toggle
            controlsPanel.Visible = !controlsPanel.Visible;

            //Set
            expandCollapseButton.Text = controlsPanel.Visible ? "-" : "+";
        }
    }
}
