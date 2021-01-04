using Abide.HaloLibrary.Halo2.Retail;
using System;
using System.IO;
using System.Windows.Forms;

namespace Abide.Decompiler
{
    public partial class CacheDecompiler : Form, IDecompileReporter 
    {
        public CacheDecompiler()
        {
            InitializeComponent();

            if (File.Exists(Program.FileArgument))
                mapFilePathTextBox.Text = Program.FileArgument;
        }

        private void decompileTagsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Create OpenFileDialog instance.
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                //Prepare
                openDlg.Filter = "Halo Map Files (*.map)|*.map";

                //Show
                if (openDlg.ShowDialog() == DialogResult.OK)
                    using (TagDecompiler tagDecompiler = new TagDecompiler(openDlg.FileName))
                        tagDecompiler.ShowDialog();
            }
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
            HaloMap map = new HaloMap(mapFilePathTextBox.Text);

            string tagsDirectory = Path.Combine(Guerilla.Library.RegistrySettings.WorkspaceDirectory, "tags");
            if (!Directory.Exists(tagsDirectory))
                Directory.CreateDirectory(tagsDirectory);
            
            Map_Decompile(map, tagsDirectory);
        }

        private void Map_Decompile(HaloMap map, string outputDirectory)
        {
            browseMapButton.Enabled = false;
            decompileButton.Enabled = false;

            MapDecompiler mapDecompiler = new MapDecompiler(map, outputDirectory) { Host = this };
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
