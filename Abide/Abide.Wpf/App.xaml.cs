using Abide.Wpf.Modules;
using Abide.Wpf.Modules.AddOns;
using Abide.Wpf.Modules.Win32;
using Abide.Wpf.Modules.Windows;
using System.IO;
using System.Windows;

namespace Abide.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void ApplicationStartup(object sender, StartupEventArgs e)
        {
            //Handle arguments
            for (int i = 0; i < e.Args.Length; i++)
            {
                //Get argument
                string argument = e.Args[i];

                //Check
                switch (argument)
                {
                    case "-c":  //Clean mode
                        ApplicationSettings.CleanMode = true;
                        break;
                    case "-d":  //Debug mode
                        ApplicationSettings.DebugMode = true;
                        break;

                    case "-s":  //Safe Mode
                        ApplicationSettings.SafeMode = true;
                        break;

                    case "-u":  //Force Update
                        ApplicationSettings.ForceUpdate = true;
                        break;

                    case "-da": //Debug AddOn
                        if (e.Args.Length > i + 1)
                        {
                            string path = e.Args[i + 1]; i++;
                            if (File.Exists(path)) ApplicationSettings.DebugAddOnPaths.Add(path);
                        }
                        break;

                    default:
                        if (File.Exists(argument))
                            ApplicationSettings.FilePaths.Add(argument);
                        break;
                }
            }

            //Apply settings
            ApplicationSettings.Apply();

            //Load AddOns
            if (Directory.Exists(AbideRegistry.AddOnsDirectory) && !ApplicationSettings.CleanMode)
            {
                //Get nested directories
                string[] directories = Directory.GetDirectories(AbideRegistry.AddOnsDirectory);

                //Loop
                foreach (string directory in directories)
                {
                    //Get manifest file name
                    string manifestFileName = Path.Combine(directory, "Manifest.xml");
                    if (File.Exists(manifestFileName))
                        AssemblyManager.AddOnEnvironments.Add(
                            new AddOnEnvironment(directory));
                }
            }

            //Load debug assemblies
            foreach (string path in ApplicationSettings.DebugAddOnPaths)
            {
                //Attempt to load
                if (AssemblyManager.LoadAssembly(path))
                {
                    //Create environment
                    AssemblyManager.AddOnEnvironments.Add(
                        AddOnEnvironment.CreateDebugEnvironment(path));
                }
            }

            //Load AddOn types
            AssemblyManager.GetLoadedAddOnTypes();

            //Set window
            MainWindow = new MainWindow();
            MainWindow.Show();
        }
    }
}
