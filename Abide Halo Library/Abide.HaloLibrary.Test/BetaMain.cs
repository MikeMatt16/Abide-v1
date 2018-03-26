using Abide.HaloLibrary.Halo2BetaMap;
using System;
using System.Windows.Forms;

namespace Abide.HaloLibrary.Test
{
    public partial class BetaMain : Form
    {
        private MapFile map = new MapFile();

        public BetaMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Prepare
            string fileName = string.Empty;
            bool open = false;

            //Initialize
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                //Setup
                openDlg.Filter = "Map Files (*.map)|*.map";
                openDlg.Title = "Open File...";
                if (openDlg.ShowDialog() == DialogResult.OK)
                {
                    fileName = openDlg.FileName;
                    open = true;
                }
            }

            //Dispose
            map.Dispose();

            //Check
            if (open)
            {
                map = new MapFile();
                map.Load(fileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Prepare
            string fileName = string.Empty;
            bool save = false;

            //Initialize
            using (SaveFileDialog saveDlg = new SaveFileDialog())
            {
                //Setup
                saveDlg.Filter = "Map Files (*.map)|*.map";
                saveDlg.Title = "Save Map As...";
                saveDlg.FileName = map.Name;
                if (saveDlg.ShowDialog() == DialogResult.OK)
                {
                    fileName = saveDlg.FileName;
                    save = true;
                }
            }

            //Check
            if (save) map.Save(fileName);
        }
    }
}
