using System;
using System.Windows.Forms;

namespace Abide.Tag.Ui.Guerilla.Controls
{
    public partial class EnumControl : GuerillaControl
    {
        public string[] Options
        {
            set
            {
                enumComboBox.Items.Clear();
                if (value != null)
                    foreach (string option in value)
                        enumComboBox.Items.Add(option);
            }
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
            get { return enumComboBox.Enabled; }
            set { enumComboBox.Enabled = value; }
        }
        public EventHandler ValueChanged
        {
            get { return valueChanged; }
            set { valueChanged = value; }
        }

        private EventHandler valueChanged;
        private string information = string.Empty;
        
        public EnumControl()
        {
            InitializeComponent();
        }

        private void enumComboBox_MouseHover(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(information))
                informationToolTip.Show(information, (Control)sender);
        }
    }
}
