using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Abide
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Prepare
            string filename = string.Empty;
            bool open = false;

            //Initialize
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                //Setup
                openDlg.Filter = "Halo Map Files (*.map)|*.map";
                openDlg.Title = "Open Map File...";
                if (openDlg.ShowDialog() == DialogResult.OK)
                {
                    filename = openDlg.FileName;
                    open = true;
                }
            }

            //Check
            if (open)
            {
                switch (MapHelper.GetMapVersion(filename))
                {
                    case HaloLibrary.MapVersion.HaloCE:
                        break;
                    case HaloLibrary.MapVersion.HaloCEx:
                        break;
                    case HaloLibrary.MapVersion.Halo2:
                        //Setup Editor
                        Form editor = new Halo2.Editor(filename);
                        editor.MdiParent = this;

                        //Show
                        editor.Show();
                        break;
                    case HaloLibrary.MapVersion.Halo2b:
                        break;
                    case HaloLibrary.MapVersion.Halo2v:
                        break;
                    case HaloLibrary.MapVersion.Halo3:
                        break;
                    case HaloLibrary.MapVersion.Halo3b:
                        break;
                    case HaloLibrary.MapVersion.HaloReach:
                        break;
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Exit
            Application.Exit();
        }
    }
}
