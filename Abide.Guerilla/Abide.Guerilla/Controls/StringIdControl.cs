using System;

namespace Abide.Tag.Ui.Guerilla.Controls
{
    public partial class StringIdControl : GuerillaControl
    {
        public new string Value
        {
            get { return stringTextBox.Text; }
            set { base.Value = stringTextBox.Text = value.ToString(); }
        }
        public EventHandler ValueChanged { get; set; }

        public StringIdControl()
        {
            InitializeComponent();
        }

        private void stringTextBox_TextChanged(object sender, EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }
    }
}
