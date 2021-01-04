using Abide.AddOnApi.Wpf.Halo2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Abide.Wpf.Modules.Tools.Halo2.Retail.ModelEditor
{
    /// <summary>
    /// Interaction logic for ModelEditor.xaml
    /// </summary>
    public partial class ModelEditor : ToolControl
    {
        public ModelEditor()
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
                vm.SelectedEntry = SelectedEntry;
        }
    }
}
