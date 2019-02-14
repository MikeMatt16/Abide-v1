using Abide.HaloLibrary;
using Abide.Tag;
using Abide.Tag.Guerilla.Generated;
using System;
using System.Windows.Forms;

namespace Abide.Guerilla.Dialogs
{
    public partial class NewTagGroupDialog : Form
    {
        /// <summary>
        /// Gets and returns the selected tag group.
        /// </summary>
        public ITagGroup SelectedGroup { get; private set; } = null;

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
            try { SelectedGroup = TagLookup.CreateTagGroup((TagFourCc)tagGroupComboBox.SelectedItem); }
            catch { SelectedGroup = null; }

            //Enable
            okButton.Enabled = SelectedGroup != null;
        }

        private void okButton_Click(object sender, EventArgs e)
        {

        }
    }
}
