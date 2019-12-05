using Abide.Wpf.Modules.UI;
using System.Windows;

namespace Abide.Wpf.Modules.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : GlowWindowHost
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            //Minimize
            WindowState = WindowState.Minimized;
        }
        private void MaximizeRestoreButton_Click(object sender, RoutedEventArgs e)
        {
            //Restore
            if (WindowState == WindowState.Maximized) WindowState = WindowState.Normal;
            else WindowState = WindowState.Maximized;
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            //Close
            Close();
        }
    }
}
