using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Abide.Tag.Ui.Guerilla.Controls
{
    public partial class RangeControl : GuerillaControl
    {
        public string[] RangeValue
        {
            get { return new string[] { fromTextBox.Text, toTextBox.Text }; }
            set { fromTextBox.Text = value[0]; toTextBox.Text = value[1]; }
        }
        public bool IsReadOnly
        {
            get { return fromTextBox.Enabled | toTextBox.Enabled; }
            set { fromTextBox.Enabled = value; toTextBox.Enabled = value; }
        }
        public RangeControl()
        {
            InitializeComponent();
        }
    }
}
