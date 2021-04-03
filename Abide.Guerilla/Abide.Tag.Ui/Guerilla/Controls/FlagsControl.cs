using Abide.Tag.Definition;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Abide.Tag.Ui.Guerilla.Controls
{
    public partial class FlagsControl : GuerillaControl
    {
        public override object Value
        {
            get
            {
                int value = 0;
                foreach (int i in flagsListBox.CheckedIndices)
                    value |= (1 >> i);
                return value;
            }
            set
            {
                ignoreCheck = true;
                if (int.TryParse(value.ToString(), out int flags))
                {
                    for (int i = 0; i < flagsListBox.Items.Count; i++)
                        if ((flags & (1 << i)) == (1 << i))
                            flagsListBox.SetItemChecked(i, true);
                        else flagsListBox.SetItemChecked(i, false);
                }
                ignoreCheck = false;
            }
        }
        public string[] Options
        {
            get
            {
                return options;
            }
            set
            {
                flagsListBox.Items.Clear();
                if (value != null)
                {
                    options = value;
                    foreach (string option in value)
                        flagsListBox.Items.Add(new ObjectName(option).Name);
                }
                else options = new string[0];

                if(flagsListBox.Items.Count > 0 && flagsListBox.Items.Count < 10)
                {
                    int width = ClientSize.Width;
                    int height = flagsListBox.GetItemHeight(0) * flagsListBox.Items.Count;
                    flagsListBox.ClientSize = new Size(width, height);
                }
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
            get { return flagsListBox.Enabled; }
            set { flagsListBox.Enabled = value; }
        }
        public EventHandler ValueChanged { get; set; }

        private bool ignoreCheck = false;
        private string information = string.Empty;
        private string[] options = new string[0];

        public FlagsControl()
        {
            InitializeComponent();
        }

        private void flagsListBox_MouseHover(object sender, EventArgs e)
        {
            Point clientPoint = flagsListBox.PointToClient(MousePosition);
            ObjectName name = new ObjectName();
            for (int i = 0; i < flagsListBox.Items.Count; i++)
            {
                name = new ObjectName(options[i]);
                Rectangle itemRectangle = flagsListBox.GetItemRectangle(i);
                if (!string.IsNullOrEmpty(name.Information) && itemRectangle.Contains(clientPoint))
                    InformationTooltip.Show(name.Information, flagsListBox);
            }
        }
        private void flagsListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (ignoreCheck) return;
            ValueChanged?.Invoke(this, e);
        }
    }
}
