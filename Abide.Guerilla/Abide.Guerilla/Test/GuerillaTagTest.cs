using Abide.Guerilla.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Abide.Guerilla.Test
{
    public partial class GuerillaTagTest : Form
    {
        public GuerillaTagTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GuerillaTagFile file = new GuerillaTagFile();
            file.Load(@"F:\Users\Mike\Documents\Halo 2\Map Editor\tags\objects\weapons\rifle\battle_rifle\battle_rifle.weapon");
        }
    }
}
