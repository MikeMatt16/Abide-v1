using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Abide.Wpf.Modules.ViewModel
{
    public abstract class BaseViewModel : DependencyObject, INotifyPropertyChanged, IDisposable
    {
        private const string empty = "";
        public event PropertyChangedEventHandler PropertyChanged;
        private bool isDisposed = false;

        public bool IsDisposed
        {
            get { return isDisposed; }
            protected set
            {
                if(isDisposed != value)
                {
                    isDisposed = value;
                    NotifyPropertyChanged();
                }
            }
        }
        protected void NotifyPropertyChanged([CallerMemberName]string propertyName = empty)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected virtual void Dispose(bool disposing) { }
        /// <summary>
        /// Releases all resources used by this instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
    }
}
