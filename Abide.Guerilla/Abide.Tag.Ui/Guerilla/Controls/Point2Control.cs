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
    public partial class Point2Control : GuerillaControl
    {
        public bool IsReadOnly
        {
            get { return iTextBox.Enabled || jTextBox.Enabled; }
            set { iTextBox.Enabled = value; jTextBox.Enabled = value; }
        }

        public Point2Control()
        {
            InitializeComponent();
        }
    }
}
