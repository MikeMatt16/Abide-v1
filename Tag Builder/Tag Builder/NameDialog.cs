using System.Windows.Forms;

namespace Abide.TagBuilder
{
    /// <summary>
    /// Represents a tag name dialog.
    /// </summary>
    public partial class NameDialog : Form
    {
        /// <summary>
        /// Gets or sets the tag name.
        /// </summary>
        public string TagName
        {
            get { return tagPathTextBox.Text; }
            set { tagPathTextBox.Text = value; }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="NameDialog"/> class.
        /// </summary>
        public NameDialog()
        {
            InitializeComponent();
        }
    }
}
