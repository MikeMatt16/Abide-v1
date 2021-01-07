using Abide.HaloLibrary.Halo2.Retail;
using System.Windows;

namespace Abide.Wpf.Modules.ViewModel
{
    public abstract class BaseAddOnViewModel : BaseViewModel
    {
        public static DependencyProperty MapProperty =
            DependencyProperty.Register(nameof(Map), typeof(HaloMapFile), typeof(BaseAddOnViewModel), new PropertyMetadata(MapPropertyChanged));

        public static DependencyProperty SelectedTagProperty =
            DependencyProperty.Register(nameof(SelectedTag), typeof(HaloTag), typeof(BaseAddOnViewModel), new PropertyMetadata(SelectedTagPropertyChanged));

        public HaloMapFile Map
        {
            get => (HaloMapFile)GetValue(MapProperty);
            set => SetValue(MapProperty, value);
        }
        public HaloTag SelectedTag
        {
            get => (HaloTag)GetValue(SelectedTagProperty);
            set => SetValue(SelectedTagProperty, value);
        }
        protected virtual void OnMapChange() { }
        protected virtual void OnSelectedTagChanged() { }

        private static void MapPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BaseAddOnViewModel vm)
            {
                vm.OnMapChange();
            }
        }
        private static void SelectedTagPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BaseAddOnViewModel vm)
            {
                vm.OnSelectedTagChanged();
            }
        }
    }
}
