using System;

namespace Abide.Tag.Ui.Guerilla.Controls
{
    public partial class StringControl : GuerillaControl
    {
        public string StringValue
        {
            get { return stringTextBox.Text; }
            set { stringTextBox.Text = value; }
        }
        public bool IsReadOnly
        {
            get { return stringTextBox.Enabled; }
            set { stringTextBox.Enabled = value; }
        }
        public string String
        {
            get { return stringTextBox.Text; }
            set { stringTextBox.Text = value; }
        }
        public EventHandler ValueChanged { get; set; }

        public StringControl()
        {
            InitializeComponent();
        }

        private void stringTextBox_TextChanged(object sender, EventArgs e)
        {
            //Invoke
            ValueChanged?.Invoke(this, e);
        }
    }
}
