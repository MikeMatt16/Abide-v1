using Abide.Guerilla.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Abide.Guerilla.Dialogs
{
    public partial class CacheCompilerDialog : Form
    {
        public CacheCompilerDialog()
        {
            InitializeComponent();
        }

        private void browseScenarioButton_Click(object sender, EventArgs e)
        {
            //Initialize
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                //Setup
                openDlg.Filter = "Scenario files (*.scenario)|*.scenario";
                openDlg.CustomPlaces.Add(Path.Combine(RegistrySettings.WorkspaceDirectory, "tags"));
                openDlg.InitialDirectory = RegistrySettings.WorkspaceDirectory;

                //Show
                if(openDlg.ShowDialog() == DialogResult.OK)
                {
                    scenarioFileNameTextBox.Text = openDlg.FileName;    //Set
                }
            }
        }

        private void scenarioFileNameTextBox_TextChanged(object sender, EventArgs e)
        {
            //Enable or disable
            compileButton.Enabled = File.Exists(scenarioFileNameTextBox.Text);
        }

        private void compileButton_Click(object sender, EventArgs e)
        {
            //Get compiler
            Assembly compiler = typeof(Compiler.MapCompiler).Assembly;
            Uri codeBase = new Uri(compiler.CodeBase);

            //Check
            if(codeBase.IsFile)
            {
                //Prepare
                ProcessStartInfo compilerProcessStartInfo = new ProcessStartInfo(codeBase.LocalPath)
                {
                    UseShellExecute = false,
                    Arguments = scenarioFileNameTextBox.Text
                };
                Process compilerProcess = new Process() { StartInfo = compilerProcessStartInfo };
                
                //Start
                if (compilerProcess.Start()) compilerProcess.WaitForExit();
            }
        }
    }
}
