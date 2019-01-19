using Abide.HaloLibrary;
using System;
using System.Windows.Forms;

namespace Abide.Guerilla.Dialogs
{
    public partial class NewTagGroupDialog : Form
    {
        /// <summary>
        /// Gets and returns the selected tag group.
        /// </summary>
        public Tag.Group SelectedGroup { get; private set; } = null;
        /// <summary>
        /// Gets and returns the name of the tag group.
        /// </summary>
        public string FileName => nameTextBox.Text;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewTagGroupDialog"/> class.
        /// </summary>
        public NewTagGroupDialog()
        {
            InitializeComponent();

            // Populate
            foreach (var tagFourCc in Tags.GetExportedTagGroups())
                tagGroupComboBox.Items.Add(tagFourCc);
        }

        private void tagGroupComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Set
            try { SelectedGroup = Abide.Tag.Guerilla.Generated.TagLookup.CreateTagGroup((TagFourCc)tagGroupComboBox.SelectedItem); }
            catch { SelectedGroup = null; }

            //Enable
            okButton.Enabled = SelectedGroup != null;
        }
    }
}
