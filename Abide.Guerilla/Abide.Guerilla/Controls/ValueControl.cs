using System;
using System.Windows.Forms;

namespace Abide.Tag.Ui.Guerilla.Controls
{
    public partial class ValueControl : GuerillaControl
    {
        public override object Value
        {
            get { return valueTextBox.Text; }
            set { valueTextBox.Text = value.ToString(); }
        }
        public string Details
        {
            get { return detailsLabel.Text; }
            set { detailsLabel.Text = value; }
        }
        public string Information
        {
            get { return information; }
            set { information = value ?? string.Empty; }
        }
        public bool IsReadOnly
        {
            get { return valueTextBox.ReadOnly; }
            set { valueTextBox.ReadOnly = value; }
        }
        public EventHandler ValueChanged { get; set; }

        private string information = string.Empty;

        public ValueControl()
        {
            InitializeComponent();
        }
        private void valueTextBox_TextChanged(object sender, EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }
        private void valueTextBox_MouseHover(object sender, EventArgs e)
        {
            //Check
            if (!string.IsNullOrEmpty(information))
            {
                //Show
                informationToolTip.Show(information, (Control)sender);
            }
        }
    }
}
