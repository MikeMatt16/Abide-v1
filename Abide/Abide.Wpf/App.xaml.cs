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
            for (int i = 0; i < e.Args.Length; i++)
            {
                string argument = e.Args[i];
                switch (argument)
                {
                    case "-c":
                        ApplicationSettings.CleanMode = true;
                        break;

                    case "-d":
                        ApplicationSettings.DebugMode = true;
                        break;

                    case "-s":
                        ApplicationSettings.SafeMode = true;
                        break;

                    case "-u":
                        ApplicationSettings.ForceUpdate = true;
                        break;

                    case "-da":
                        if (e.Args.Length > i + 1)
                        {
                            string path = e.Args[i + 1]; i++;
                            if (File.Exists(path))
                            {
                                ApplicationSettings.DebugAddOnPaths.Add(path);
                            }
                        }
                        break;

                    default:
                        if (File.Exists(argument))
                        {
                            ApplicationSettings.FilePaths.Add(argument);
                        }

                        break;
                }
            }

            ApplicationSettings.Apply();
            if (Directory.Exists(AbideRegistry.AddOnsDirectory) && !ApplicationSettings.CleanMode)
            {
                foreach (string directory in Directory.GetDirectories(AbideRegistry.AddOnsDirectory))
                {
                    if (File.Exists(Path.Combine(directory, "Manifest.xml")))
                    {
                        AssemblyManager.AddOnEnvironments.Add(
                            AddOnEnvironment.Create(directory));
                    }
                }
            }

            foreach (string path in ApplicationSettings.DebugAddOnPaths)
            {
                if (AssemblyManager.LoadAssembly(path))
                {
                    AssemblyManager.AddOnEnvironments.Add(
                        AddOnEnvironment.CreateDebugEnvironment(path));
                }
            }

            AssemblyManager.InitializeAddOnTypes();
            // MainWindow = new Abide.Wpf.Modules.Windows.Guerilla();
            MainWindow = new MainWindow();
            MainWindow.Show();
        }
    }
}
