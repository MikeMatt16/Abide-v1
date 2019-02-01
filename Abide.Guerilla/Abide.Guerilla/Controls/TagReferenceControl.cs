using Abide.Guerilla.Library;
using Abide.Tag.Guerilla;
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
        public EventHandler ValueChanged { get; set; }

        private string tagReference = string.Empty;

        public TagReferenceControl(Field field) : this()
        {
            Field = field;
        }
        private TagReferenceControl()
        {
            InitializeComponent();
        }
        protected override void OnFieldChanged(EventArgs e)
        {
            base.OnFieldChanged(e);
            pathTextBox.Text = Field.Value.ToString() ?? string.Empty;
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
                if (Field != null && openDlg.ShowDialog() == DialogResult.OK)
                {
                    pathTextBox.Text = openDlg.FileName.Replace(TagsPath, string.Empty).Substring(1);
                    Field.Value = new StringValue(pathTextBox.Text);
                }
            }
        }
    }
}
