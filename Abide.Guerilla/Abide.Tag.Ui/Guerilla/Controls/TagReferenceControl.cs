using System;

namespace Abide.Tag.Ui.Guerilla.Controls
{
    public partial class TagReferenceControl : GuerillaControl
    {
        public TagReference RefernceValue
        {
            get { return tagReference; }
            set { tagReference = value; pathTextBox.Text = tagReference.Id.ToString(); }
        }
        public bool IsReadOnly
        {
            get { return browseTagButton.Enabled; }
            set { browseTagButton.Enabled = value; }
        }
        public EventHandler ValueChanged { get; set; }

        private TagReference tagReference = TagReference.Null;

        public TagReferenceControl()
        {
            InitializeComponent();
        }
    }
}
