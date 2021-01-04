using Abide.AddOnApi;
using Abide.AddOnApi.Wpf.Halo2;
using Abide.Wpf.Modules.ViewModel;
using System.ComponentModel;
using System.Windows;

namespace Abide.Wpf.Modules.Tools.Halo2.Retail.TagEditor
{
    /// <summary>
    /// Interaction logic for TagEditor.xaml
    /// </summary>
    [AddOn]
    public partial class TagEditor : ToolControl
    {
        public TagEditor()
        {
            InitializeComponent();
        }

        private void ToolControl_MapLoad(object sender, RoutedEventArgs e)
        {
            if (DataContext is BaseAddOnViewModel vm)
                vm.Map = Map;
        }

        private void ToolControl_SelectedEntryChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is BaseAddOnViewModel vm)
                vm.SelectedTag = SelectedEntry;
        }

        private void ToolControl_XboxConnectionStateChanged(object sender, RoutedEventArgs e)
        {
        }
    }
}
