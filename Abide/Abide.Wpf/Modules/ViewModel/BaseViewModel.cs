using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Threading;

namespace Abide.Wpf.Modules.ViewModel
{
    public abstract class BaseViewModel : DependencyObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (CheckAccess())
            {
                OnNotifyPropertyChanged(new PropertyChangedEventArgs(propertyName));
            }
            else
            {
                Dispatcher.Invoke(() =>
                {
                    OnNotifyPropertyChanged(new PropertyChangedEventArgs(propertyName));
                });
            }
        }
    }
}
