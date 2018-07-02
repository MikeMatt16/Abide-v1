using System.Windows.Forms;

namespace Abide.Tag.Ui
{
    public partial class TreeViewForm : Form
    {
        public TreeView Tree
        {
            get { return treeView; }
        }
        public string Status
        {
            get { return statusLabel.Text; }
            set { statusLabel.Text = value; }
        }

        public TreeViewForm()
        {
            InitializeComponent();
        }
    }
}
