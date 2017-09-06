using System;
using System.Windows.Forms;

namespace Tag_Data_Editor.Controls
{
    public partial class StringIDControl : MetaControl
    {
        public EventHandler<StringButtonEventArgs> StringButtonClick
        {
            get { return stringButtonClick; }
            set { stringButtonClick = value; }
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
        public string SelectedString
        {
            get { return sid; }
            set { sid = value; }
        }
        public string StringLabel
        {
            get { return stringSelectBox.Text; }
            set { stringSelectBox.Text = value; }
        }

        private EventHandler<StringButtonEventArgs> stringButtonClick;
        private string sid = string.Empty;

        public StringIDControl()
        {
            InitializeComponent();
        }

        private void stringSelectBox_Click(object sender, EventArgs e)
        {
            stringButtonClick?.Invoke(this, new StringButtonEventArgs(SelectedString));
            Label.Text = sid;
        }
    }

    public class StringButtonEventArgs : EventArgs
    {
        public string SelectedString
        {
            get { return selectedString; }
        }

        private readonly string selectedString;

        public StringButtonEventArgs(string selectedString)
        {
            this.selectedString = selectedString;
        }
    }
}
