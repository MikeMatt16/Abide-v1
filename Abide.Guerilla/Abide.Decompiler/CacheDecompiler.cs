using Abide.HaloLibrary.Halo2Map;
using System;
using System.IO;
using System.Windows.Forms;

namespace Abide.Decompiler
{
    public partial class CacheDecompiler : Form, IDecompileHost 
    {
        public CacheDecompiler()
        {
            InitializeComponent();
        }

        private void browseMapButton_Click(object sender, EventArgs e)
        {
            //Create OpenFileDialog instance.
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                //Prepare
                openDlg.Filter = "Halo Map Files (*.map)|*.map";

                //Show
                if (openDlg.ShowDialog() == DialogResult.OK)
                    mapFilePathTextBox.Text = openDlg.FileName;
            }
        }

        private void mapFilePathTextBox_TextChanged(object sender, EventArgs e)
        {
            //Prepare
            bool exists = File.Exists(mapFilePathTextBox.Text);

            //Set
            decompileButton.Enabled = exists;
        }

        private void decompileButton_Click(object sender, EventArgs e)
        {
            //Prepare
            MapFile map = new MapFile();

            //Load Map
            try
            {
                using (FileStream fs = new FileStream(mapFilePathTextBox.Text, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    map.Load(fs);
            }
            catch { MessageBox.Show("Faled to open map file."); }

            //Get Directory
            using (FolderBrowserDialog folderDlg = new FolderBrowserDialog())
            {
                //Setup
                folderDlg.Description = $"Choose where to decompile {map.Name} to.";

                //Show
                if (folderDlg.ShowDialog() == DialogResult.OK)
                {
                    //Check
                    if (map != null) Map_Decompile(map, folderDlg.SelectedPath);
                }
            }
        }

        private void Map_Decompile(MapFile map, string outputDirectory)
        {
            //Disable
            browseMapButton.Enabled = false;
            decompileButton.Enabled = false;

            //Decompile
            MapDecompiler mapDecompiler = new MapDecompiler(this, map, outputDirectory);

            //Start
            mapDecompiler.Start();
        }
        
        public void Complete()
        {
            //Check
            if (InvokeRequired) { Invoke(new MethodInvoker(Complete)); return; }

            //Reset
            decompileProgressBar.Value = 0;

            //Enable
            browseMapButton.Enabled = true;
            decompileButton.Enabled = true;
        }

        public void Fail()
        {
            //Do nothing
        }

        public void Report(float progress)
        {
            //Check
            if (InvokeRequired) { Invoke(new ProgressReportHandler(Report), progress); return; }

            //Update
            decompileProgressBar.Value = (int)Math.Ceiling(progress * 100f);
        }

        private delegate void ProgressReportHandler(float progress);
    }
}
