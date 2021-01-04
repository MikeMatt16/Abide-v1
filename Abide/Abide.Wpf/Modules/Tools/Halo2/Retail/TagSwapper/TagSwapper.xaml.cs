using Abide.AddOnApi;
using Abide.AddOnApi.Wpf.Halo2;
using Abide.Wpf.Modules.ViewModel;
using System.Windows;

namespace Abide.Wpf.Modules.Tools.Halo2.Retail.TagSwapper
{
    /// <summary>
    /// Interaction logic for TagSwapper.xaml
    /// </summary>
    [AddOn]
    public partial class TagSwapper : ToolControl
    {
        public TagSwapper()
        {
            InitializeComponent();
        }

        private void ToolControl_SelectedEntryChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is BaseAddOnViewModel vm)
            {
                vm.SelectedTag = SelectedEntry;
            }
        }

        private void ToolControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is BaseAddOnViewModel vm)
            {
                vm.Map = Map;
            }
        }
    }
}
