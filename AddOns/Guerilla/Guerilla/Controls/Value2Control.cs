using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Guerilla.Controls
{
    public partial class Value2Control : GuerillaControl
    {
        public string NameLabel => nameLabel.Name;

        public Value2Control()
        {
            InitializeComponent();
        }
    }
}
