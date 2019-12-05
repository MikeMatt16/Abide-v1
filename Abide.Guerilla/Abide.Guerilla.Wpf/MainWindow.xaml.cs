using Abide.Decompiler;
using Abide.Guerilla.Wpf.Dialogs;
using Abide.Guerilla.Wpf.Ui;
using System.Windows;

namespace Abide.Guerilla.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : GlowWindowHost
    {
        public MainViewModel Model
        {
            get { if (DataContext is MainViewModel mainView) return mainView; return null; }
        }
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CloseCaptionButton_Click(object sender, RoutedEventArgs e)
        {
            //Close
            Close();
        }

        private void MaximizeCaptionButton_Click(object sender, RoutedEventArgs e)
        {
            //Maximize or restore
            if (WindowState == WindowState.Maximized) WindowState = WindowState.Normal;
            else WindowState = WindowState.Maximized;
        }

        private void MinimizeCaptionButton_Click(object sender, RoutedEventArgs e)
        {
            //Minimize
            WindowState = WindowState.Minimized;
        }

        private void CustomBorderWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //Check and set
            if (e.NewValue is MainViewModel model)
                model.Owner = this;
        }
    }
}
