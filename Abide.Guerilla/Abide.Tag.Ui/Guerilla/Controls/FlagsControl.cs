using Abide.Tag.Definition;
using System;
using System.Drawing;

namespace Abide.Tag.Ui.Guerilla.Controls
{
    public partial class FlagsControl : GuerillaControl
    {
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

        private string information = string.Empty;
        private string[] options = new string[0];

        public FlagsControl()
        {
            InitializeComponent();
        }

        private void flagsListBox_MouseHover(object sender, EventArgs e)
        {
            Point clientPoint = flagsListBox.PointToClient(MousePosition);
            ObjectName name = null;
            for (int i = 0; i < flagsListBox.Items.Count; i++)
            {
                name = new ObjectName(options[i]);
                Rectangle itemRectangle = flagsListBox.GetItemRectangle(i);
                if (!string.IsNullOrEmpty(name.Information) && itemRectangle.Contains(clientPoint))
                    InformationTooltip.Show(name.Information, flagsListBox);
            }
        }
    }
}
