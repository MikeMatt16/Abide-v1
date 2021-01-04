using Abide.HaloLibrary.Halo2.Retail;
using Abide.Wpf.Modules.ViewModel;
using System;
using System.Windows;

namespace Abide.Wpf.Modules.Tools.Halo2.Retail
{
    public abstract class BaseAddOnViewModel : BaseViewModel
    {
        public static DependencyProperty MapProperty =
            DependencyProperty.Register(nameof(Map), typeof(HaloMap), typeof(BaseAddOnViewModel), new PropertyMetadata(MapPropertyChanged));

        public static DependencyProperty SelectedEntryProperty =
            DependencyProperty.Register(nameof(SelectedEntry), typeof(IndexEntry), typeof(BaseAddOnViewModel), new PropertyMetadata(SelectedEntryPropertyChanged));

        public HaloMap Map
        {
            get { return (HaloMap)GetValue(MapProperty); }
            set { SetValue(MapProperty, value); }
        }
        public IndexEntry SelectedEntry
        {
            get { return (IndexEntry)GetValue(SelectedEntryProperty); }
            set { SetValue(SelectedEntryProperty, value); }
        }
        protected virtual void OnMapChange() { }
        protected virtual void OnSelectedEntryChanged() { }

        private static void MapPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BaseAddOnViewModel vm)
                vm.OnMapChange();
        }
        private static void SelectedEntryPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BaseAddOnViewModel vm)
                vm.OnSelectedEntryChanged();
        }
    }
}
