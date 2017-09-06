using System;

namespace Tag_Data_Editor.Controls
{
    public partial class StringControl : MetaControl
    {
        public EventHandler<StringChangedEventArgs> StringChanged
        {
            get { return stringChanged; }
            set { stringChanged = value; }
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
        public string String
        {
            get { return stringBox.Text; }
            set { stringBox.Text = value; }
        }
        public int Length
        {
            get { return stringBox.MaxLength; }
            set { stringBox.MaxLength = value; }
        }

        private EventHandler<StringChangedEventArgs> stringChanged;

        public StringControl()
        {
            InitializeComponent();
        }

        private void stringBox_TextChanged(object sender, EventArgs e)
        {
            //Prepare
            var args = new StringChangedEventArgs(stringBox.Text);
            Label.Text = args.String;

            //Invoke
            stringChanged?.Invoke(this, args);
        }
    }

    public class StringChangedEventArgs : EventArgs
    {
        public string String
        {
            get { return stringValue; }
        }

        private readonly string stringValue;

        public StringChangedEventArgs(string stringValue)
        {
            this.stringValue = stringValue;
        }
    }
}
