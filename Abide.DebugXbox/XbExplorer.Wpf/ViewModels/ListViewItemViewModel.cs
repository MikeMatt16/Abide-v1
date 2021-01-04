using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace XbExplorer.Wpf.ViewModels
{
    public abstract class ListViewItemViewModel : BaseViewModel
    {
        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register(nameof(Name), typeof(string), typeof(ListViewItemViewModel), new PropertyMetadata(NamePropertyChanged));
        public static readonly DependencyProperty CanRenameProperty =
            DependencyProperty.Register(nameof(CanRename), typeof(bool), typeof(ListViewItemViewModel), new PropertyMetadata(false));

        public event EventHandler NameChanged;

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set
            {
                if (CanRename)
                {
                    SetValue(NameProperty, value);
                }
            }
        }
        public bool CanRename
        {
            get { return (bool)GetValue(CanRenameProperty); }
            set { SetValue(CanRenameProperty, value); }
        }

        protected virtual void OnNameChanged(EventArgs e)
        {
            NameChanged?.Invoke(this, e);
        }

        private static void NamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ListViewItemViewModel listViewItem)
            {
                listViewItem.OnNameChanged(new EventArgs());
            }
        }
    }
}
