using Abide.HaloLibrary.Halo2Map;
using System;

namespace Tag_Data_Editor.Controls
{
    public partial class TagControl : MetaControl
    {
        public EventHandler<TagButtonEventArgs> GoToButtonClick
        {
            get { return goToButtonClick; }
            set { goToButtonClick = value; }
        }
        public EventHandler<TagButtonEventArgs> TagButtonClick
        {
            get { return tagButtonClick; }
            set { tagButtonClick = value; }
        }
        public override string Type
        {
            get { return typeLabel.Text; }
            set { typeLabel.Text = value; }
        }
        public override string ControlName
        {
            get { return nameLabel.Text; }
            set { nameLabel.Text = value; }
        }
        public IndexEntry SelectedEntry
        {
            get { return selectedEntry; }
            set { selectedEntry = value; }
        }
        public string TagLabel
        {
            get { return tagSelectBox.Text; }
            set { tagSelectBox.Text = value; }
        }

        private EventHandler<TagButtonEventArgs> goToButtonClick;
        private EventHandler<TagButtonEventArgs> tagButtonClick;
        private IndexEntry selectedEntry = null;

        public TagControl()
        {
            InitializeComponent();
        }

        private void tagSelectBox_Click(object sender, EventArgs e)
        {
            tagButtonClick?.Invoke(this, new TagButtonEventArgs(selectedEntry));
            Label.Text = tagSelectBox.Text;
        }

        private void goToButton_Click(object sender, EventArgs e)
        {
            goToButtonClick?.Invoke(this, new TagButtonEventArgs(selectedEntry));
        }
    }

    public class TagButtonEventArgs : EventArgs
    {
        public IndexEntry SelectedEntry
        {
            get { return selectedEntry; }
        }

        private readonly IndexEntry selectedEntry;

        public TagButtonEventArgs(IndexEntry selectedEntry)
        {
            this.selectedEntry = selectedEntry;
        }
    }
}
