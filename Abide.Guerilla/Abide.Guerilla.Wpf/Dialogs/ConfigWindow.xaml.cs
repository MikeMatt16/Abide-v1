using Microsoft.Win32;
using System.IO;
using System.Windows;
using WinForms = System.Windows.Forms;

namespace Abide.Guerilla.Wpf
{
    /// <summary>
    /// Interaction logic for ConfigWindow.xaml
    /// </summary>
    public partial class ConfigWindow : Window
    {
        public ConfigWindow()
        {
            InitializeComponent();
        }
        
        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            //Check
            if (sender is FrameworkElement element && element.DataContext is string fileName)
            {
                //Create
                OpenFileDialog openDlg = new OpenFileDialog
                {
                    Filter = "Halo Map Files (*.map)|*.map",
                    FileName = fileName
                };

                if(File.Exists(fileName))
                    openDlg.InitialDirectory = Path.GetDirectoryName(fileName);

                if (openDlg.ShowDialog() ?? false)
                    element.DataContext = openDlg.FileName;
            }
        }

        private void BrowsePathButton_Click(object sender, RoutedEventArgs e)
        {
            //Check
            if(sender is FrameworkElement element && element.DataContext is string directory)
            {
                //Create
                using (WinForms.FolderBrowserDialog folderDlg = new WinForms.FolderBrowserDialog())
                {
                    folderDlg.SelectedPath = directory;
                    if(folderDlg.ShowDialog() == WinForms.DialogResult.OK)
                        element.DataContext = folderDlg.SelectedPath;
                }
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            //Close
            Close();
        }
    }
}
