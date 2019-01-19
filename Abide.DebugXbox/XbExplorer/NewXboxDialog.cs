using System;
using System.Net;
using System.Windows.Forms;

namespace XbExplorer
{
    public partial class NewXboxDialog : Form
    {
        /// <summary>
        /// Gets and returns the Xbox name or IP address string.
        /// </summary>
        public string XboxName { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="NewXboxDialog"/> class.
        /// </summary>
        public NewXboxDialog()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            //Prepare
            string xboxName = null;

            //Check
            if (IPAddress.TryParse(debugNameTextBox.Text, out IPAddress address))
                xboxName = address.ToString();
            else xboxName = debugNameTextBox.Text;

            //Set
            if (!string.IsNullOrEmpty(xboxName))
            {
                XboxName = xboxName;
                DialogResult = DialogResult.OK;
            }
        }
    }
}
