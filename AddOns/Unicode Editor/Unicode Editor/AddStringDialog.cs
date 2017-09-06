using System;
using System.Windows.Forms;

namespace Unicode_Editor
{
    public partial class AddStringDialog : Form
    {
        public string StringId
        {
            get { return stringIdBox.Text; }
            set { stringIdBox.Text = value; }
        }

        public AddStringDialog()
        {
            InitializeComponent();
        }

        private void stringIdBox_TextChanged(object sender, EventArgs e)
        {
            okButton.Enabled = !string.IsNullOrEmpty(stringIdBox.Text);
        }
    }
}
