using System.Windows.Forms;

namespace Abide.Guerilla.Ui.Forms
{
    public partial class TextForm : Form
    {
        /// <summary>
        /// Gets or sets the text content of the form.
        /// </summary>
        public string TextContent
        {
            get { return textRichTextBox.Text; }
            set { textRichTextBox.Text = value ?? string.Empty; }
        }

        public TextForm()
        {
            InitializeComponent();
        }
    }
}
