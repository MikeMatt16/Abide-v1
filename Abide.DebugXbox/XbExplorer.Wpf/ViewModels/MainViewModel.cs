using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace XbExplorer.Wpf.ViewModels
{
    public sealed class MainViewModel : BaseViewModel
    {
        public static DependencyProperty SettingsProperty =
            DependencyProperty.Register(nameof(Settings), typeof(SettingsViewModel), typeof(MainViewModel), new PropertyMetadata());
        public static DependencyProperty MainWindowProperty =
            DependencyProperty.Register(nameof(MainWindow), typeof(Window), typeof(MainViewModel));

        private string windowTitle = "XbExplorer";

        public ObservableCollection<XboxItemViewModel> XboxItems = new ObservableCollection<XboxItemViewModel>();

        public SettingsViewModel Settings
        {
            get { return (SettingsViewModel)GetValue(SettingsProperty); }
            set { SetValue(SettingsProperty, value); }
        }
        public Window MainWindow
        {
            get { return (Window)GetValue(MainWindowProperty); }
            set { SetValue(MainWindowProperty, value); }
        }
        public string WindowTitle
        {
            get { return windowTitle; }
            set
            {
                if (windowTitle != value)
                {
                    windowTitle = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public MainViewModel()
        {
            XboxItems.Add(new XboxItemViewModel() { XboxName = "Mikematt16" });
            Settings = SettingsViewModel.Default;

            if (Settings.RequiresUpgrade)
            {
                Settings.Upgrade();
                Settings.Save();
            }
        }
    }
}
