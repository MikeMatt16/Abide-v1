using System;
using System.Windows.Forms;

namespace Abide.Tag.Ui.Guerilla.Controls
{
    public partial class EnumControl : GuerillaControl
    {
        public override object Value
        {
            get { return enumComboBox.SelectedIndex.ToString(); }
            set
            {
                int index = int.Parse(value.ToString());
                if (index > enumComboBox.Items.Count || index < 0) enumComboBox.SelectedIndex = -1;
                else enumComboBox.SelectedIndex = int.Parse(value.ToString());
            }
        }
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
        public EventHandler ValueChanged { get; set; }
        
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
        private void enumComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Invoke
            ValueChanged?.Invoke(this, e);
        }
    }
}
