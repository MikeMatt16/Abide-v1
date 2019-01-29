using Abide.Guerilla.Library;
using System;
using System.IO;
using System.Windows.Forms;

namespace Abide.Tag.Ui.Guerilla.Controls
{
    public partial class TagReferenceControl : GuerillaControl
    {
        /// <summary>
        /// Gets and returns the current tags path.
        /// </summary>
        private static string TagsPath { get => Path.Combine(RegistrySettings.WorkspaceDirectory, "tags"); }
        /// <summary>
        /// Gets or sets the tag reference path
        /// </summary>
        public string ReferencePath
        {
            get { return tagReference; }
            set
            {
                tagReference = value;
                pathTextBox.Text = tagReference.ToString();
                ValueChanged?.Invoke(this, new EventArgs());
            }
        }

        public bool IsReadOnly
        {
            get { return !browseTagButton.Enabled; }
            set { browseTagButton.Enabled = !value; }
        }
        public EventHandler ValueChanged { get; set; }

        private string tagReference = string.Empty;

        public TagReferenceControl()
        {
            InitializeComponent();
        }

        private void browseTagButton_Click(object sender, EventArgs e)
        {
            //Initialize
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                //Setup
                openDlg.Filter = "All Files (*.*)|*.*";
                openDlg.CustomPlaces.Add(new FileDialogCustomPlace(Path.Combine(RegistrySettings.WorkspaceDirectory, "tags")));

                //Check
                if (openDlg.ShowDialog() == DialogResult.OK)
                    ReferencePath = openDlg.FileName.Replace(TagsPath, string.Empty).Substring(1);
            }
        }
    }
}
