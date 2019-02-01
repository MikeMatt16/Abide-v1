using Abide.HaloLibrary.Halo2Map;
using Abide.TagEditor.Wpf.Model;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Input;

namespace Abide.TagEditor.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string CurrentFileName { get; private set; } = string.Empty;
        public MapFile Map { get; private set; } = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenItem_Click(object sender, RoutedEventArgs e)
        {
            //Create
            OpenFileDialog openDlg = new OpenFileDialog
            {
                Multiselect = false,
                Filter = "Halo 2 Map Files (*.map)|*.map"
            };

            //Show
            if (openDlg.ShowDialog() ?? false)
            {
                //Load
                if (Map != null) Map.Dispose();
                Map = new MapFile();
                Map.Load(openDlg.FileName);

                //Set
                CurrentFileName = openDlg.FileName;

                //Build file names
                tagTreeView.DataContext = Map.GetItems();
            }
        }

        private void SaveItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CloseItem_Click(object sender, RoutedEventArgs e)
        {
            //Blank out current file name
            CurrentFileName = string.Empty;
            tagTreeView.DataContext = null;

            //Close map
            if (Map != null)
            {
                Map.Close();
                Map.Dispose();
                Map = null;
            }
        }

        private void ExitItem_Click(object sender, RoutedEventArgs e)
        {
            //Exit
            Application.Current.Shutdown();
        }

        private void TreeViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //Get tag
            if(sender is FrameworkElement element && element.DataContext is TagItem tag)
            {
            }
        }
    }
}
