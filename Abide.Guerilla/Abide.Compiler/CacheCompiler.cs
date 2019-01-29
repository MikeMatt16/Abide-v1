using Abide.Guerilla.Library;
using System;
using System.IO;
using System.Windows.Forms;

namespace Abide.Compiler
{
    public partial class CacheCompiler : Form, ICompileHost
    {
        private MapCompiler mapCompiler = null;

        public CacheCompiler()
        {
            InitializeComponent();
        }

        private void browseScenarioButton_Click(object sender, EventArgs e)
        {
            //Create OpenFileDialog instance.
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                //Setup
                openDlg.Filter = "Scenario files (*.scenario)|*.scenario";
                openDlg.InitialDirectory = RegistrySettings.WorkspaceDirectory;
                openDlg.CustomPlaces.Add(new FileDialogCustomPlace(RegistrySettings.WorkspaceDirectory));

                //Show
                if (openDlg.ShowDialog() == DialogResult.OK)
                    scenarioPathTextBox.Text = openDlg.FileName;
            }
        }

        private void globalsPathTextBox_TextChanged(object sender, EventArgs e)
        {
            //Prepare
            bool exists = true;

            //Prepare
            exists &= File.Exists(scenarioPathTextBox.Text);

            //Set
            compileButton.Enabled = exists;
        }

        private void scenarioPathTextBox_TextChanged(object sender, EventArgs e)
        {
            //Prepare
            bool exists = true;

            //Prepare
            exists &= File.Exists(scenarioPathTextBox.Text);

            //Set
            compileButton.Enabled = exists;
        }

        private void compileButton_Click(object sender, EventArgs e)
        {
            //Disable
            browseScenarioButton.Enabled = false;
            compileButton.Enabled = false;

            //Decompile
            mapCompiler = new MapCompiler(this, scenarioPathTextBox.Text, RegistrySettings.WorkspaceDirectory);

            //Start
            mapCompiler.Start();
        }

        public void Log(string line)
        {
            //Check
            if (InvokeRequired) { Invoke(new LogReportHandler(Log), line); return; }

            //Append
            compileLogRichTextBox.AppendText(line + Environment.NewLine);
            compileLogRichTextBox.Select(compileLogRichTextBox.TextLength, 0);
            compileLogRichTextBox.ScrollToCaret();
        }
        
        public void Complete()
        {
            //Check
            if (InvokeRequired) { Invoke(new MethodInvoker(Complete)); return; }

            //Enable
            browseScenarioButton.Enabled = true;
            compileButton.Enabled = true;
        }

        public void Fail()
        {
            //Check
            if (InvokeRequired) { Invoke(new MethodInvoker(Complete)); return; }

            MessageBox.Show("feelsbadman");
        }

        public void Report(float progress)
        {
            //Check
            if (InvokeRequired) { Invoke(new ProgressReportHandler(Report), progress); return; }
        }

        private delegate void LogReportHandler(string line);
        private delegate void ProgressReportHandler(float progress);
    }
}
