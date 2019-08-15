using Abide.Guerilla.Library;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Input;

namespace Abide.Guerilla.Wpf.ViewModel
{
    /// <summary>
    /// Represents a map compiler view model.
    /// </summary>
    public class MapCompilerModel : NotifyPropertyChangedViewModel
    {
        
        /// <summary>
        /// Gets or sets the scenario file name.
        /// </summary>
        public string ScenarioFileName
        {
            get { return scenarioFileName; }
            set
            {
                if (scenarioFileName != value)
                {
                    scenarioFileName = value;
                    NotifyPropertyChanged();
                }
            }
        }
        /// <summary>
        /// Gets and returns the compile command.
        /// </summary>
        public ICommand CompileCommand { get; }
        /// <summary>
        /// Gets and returns the browse command.
        /// </summary>
        public ICommand BrowseCommand { get; }

        private string scenarioFileName = null;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="MapCompilerModel"/> class.
        /// </summary>
        public MapCompilerModel()
        {
            CompileCommand = new RelayCommand(p => Compile(), p => CanCompile());
            BrowseCommand = new RelayCommand(p => Browse());
        }

        private void Browse()
        {
            //Initialize
            OpenFileDialog openDlg = new OpenFileDialog()
            {
                InitialDirectory = RegistrySettings.WorkspaceDirectory,
                Filter = "Scenario Files (*.scenario)|*.scenario"
            };

            //Add custom place
            openDlg.CustomPlaces.Add(new FileDialogCustomPlace(RegistrySettings.WorkspaceDirectory));

            //Show
            if (openDlg.ShowDialog() ?? false)
                ScenarioFileName = openDlg.FileName;
        }
        private bool CanCompile()
        {
            return File.Exists(scenarioFileName);
        }
        private void Compile()
        {
            //Get compiler
            Assembly compiler = typeof(Compiler.MapCompiler).Assembly;
            Uri codeBase = new Uri(compiler.CodeBase);

            //Check
            if (codeBase.IsFile)
            {
                //Prepare
                ProcessStartInfo compilerProcessStartInfo = new ProcessStartInfo(codeBase.LocalPath)
                {
                    UseShellExecute = false,
                    Arguments = scenarioFileName
                };
                Process compilerProcess = new Process() { StartInfo = compilerProcessStartInfo };

                //Start
                if (compilerProcess.Start()) compilerProcess.WaitForExit();
            }
        }
    }
}
