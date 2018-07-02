using System;
using System.IO;
using System.Windows.Forms;

namespace Abide.Compiler
{
    public partial class CacheCompiler : Form, ICompileHost
    {
        public CacheCompiler()
        {
            InitializeComponent();
        }

        private void browseScenarioButton_Click(object sender, EventArgs e)
        {
            //Create OpenFileDialog instance.
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                //Prepare
                openDlg.Filter = "Scenario files (*.scenario)|*.scenario";

                //Show
                if (openDlg.ShowDialog() == DialogResult.OK)
                    scenarioPathTextBox.Text = openDlg.FileName;
            }
        }

        private void browseGlobalsButton_Click(object sender, EventArgs e)
        {
            //Create OpenFileDialog instance.
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                //Prepare
                openDlg.Filter = "Globals files (*.globals)|*.globals";

                //Show
                if (openDlg.ShowDialog() == DialogResult.OK)
                    globalsPathTextBox.Text = openDlg.FileName;
            }
        }

        private void globalsPathTextBox_TextChanged(object sender, EventArgs e)
        {
            //Prepare
            bool exists = true;

            //Prepare
            exists &= File.Exists(scenarioPathTextBox.Text);
            exists &= File.Exists(globalsPathTextBox.Text);

            //Set
            compileButton.Enabled = exists;
        }

        private void scenarioPathTextBox_TextChanged(object sender, EventArgs e)
        {
            //Prepare
            bool exists = true;

            //Prepare
            exists &= File.Exists(scenarioPathTextBox.Text);
            exists &= File.Exists(globalsPathTextBox.Text);

            //Set
            compileButton.Enabled = exists;
        }

        private void compileButton_Click(object sender, EventArgs e)
        {
            //Create SaveFileDialog instance.
            using (SaveFileDialog saveDlg = new SaveFileDialog())
            {
                //Prepare
                saveDlg.Filter = "Halo Map Files (*.map)|*.map";

                //Show
                if (saveDlg.ShowDialog() == DialogResult.OK)
                    Map_Compile(saveDlg.FileName, scenarioPathTextBox.Text, globalsPathTextBox.Text);
            }
        }

        private void Map_Compile(string mapFileName, string scenarioFileName, string globalsFileName)
        {
            //Disable
            browseScenarioButton.Enabled = false;
            compileButton.Enabled = false;

            //Decompile
            MapCompiler mapDecompiler = new MapCompiler(this, scenarioFileName, globalsFileName, mapFileName);

            //Start
            mapDecompiler.Start();
        }

        public void Log(string line)
        {
            //Check
            if (InvokeRequired) { Invoke(new LogReportHandler(Log), line); return; }

            //Append
            compileLogRichTextBox.AppendText(Environment.NewLine + line);
            compileLogRichTextBox.Select(compileLogRichTextBox.TextLength - 1, 0);
            compileLogRichTextBox.ScrollToCaret();
        }
        
        public void Complete()
        {
            throw new NotImplementedException();
        }

        public void Fail()
        {
            throw new NotImplementedException();
        }

        public void Marquee()
        {
            //Check
            if (InvokeRequired) { Invoke(new MethodInvoker(Marquee)); return; }

            //Update
            compileProgressBar.Style = ProgressBarStyle.Marquee;
        }

        public void Report(float progress)
        {
            //Check
            if (InvokeRequired) { Invoke(new ProgressReportHandler(Report), progress); return; }

            //Update
            compileProgressBar.Style = ProgressBarStyle.Continuous;
            compileProgressBar.Value = (int)Math.Ceiling(progress * 100f);
        }

        private delegate void LogReportHandler(string line);
        private delegate void ProgressReportHandler(float progress);
    }
}
