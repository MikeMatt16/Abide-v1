using Abide.AddOnApi;
using Abide.AddOnApi.Wpf.Halo2;
using Abide.Wpf.Modules.ViewModel;
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

namespace Abide.Wpf.Modules.Tools.Halo2.Retail.TextureEditor
{
    /// <summary>
    /// Interaction logic for TextureEditor.xaml
    /// </summary>
    [AddOn]
    public partial class TextureEditor : ToolControl
    {
        public static DpiScale CurrentDpiScale { get; private set; } = new DpiScale(1, 1);

        public TextureEditor()
        {
            InitializeComponent();
            CurrentDpiScale = VisualTreeHelper.GetDpi(this);
        }

        private void ToolControl_MapLoad(object sender, RoutedEventArgs e)
        {
            if (DataContext is BaseAddOnViewModel vm)
            {
                vm.Map = Map;
            }
        }

        private void ToolControl_SelectedEntryChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is BaseAddOnViewModel vm)
            {
                vm.SelectedTag = SelectedEntry;
            }
        }

        private void ToolControl_XboxConnectionStateChanged(object sender, RoutedEventArgs e)
        {
        }

        private void ImageCanvas_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Move;
            }
        }

        private void ImageCanvas_Drop(object sender, DragEventArgs e)
        {
            string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (filenames?.Length > 0 && DataContext is TextureEditorViewModel viewModel)
            {
                if (viewModel.SelectedBitmap != null)
                {
                    viewModel.SelectedBitmap.ImportFile(filenames[0]);
                }
            }
        }
    }
}
