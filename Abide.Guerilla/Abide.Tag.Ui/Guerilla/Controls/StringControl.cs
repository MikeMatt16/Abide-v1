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
    public partial class StringControl : GuerillaControl
    {
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
        public StringControl()
        {
            InitializeComponent();
        }
    }
}
