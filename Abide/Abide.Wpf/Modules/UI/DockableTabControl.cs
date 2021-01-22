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

namespace Abide.Wpf.Modules.UI
{
    public class DockableTabControl : TabControl
    {
        public static readonly DependencyProperty IsPanelShownProperty = DependencyProperty.Register(nameof(IsPanelShown), typeof(bool), typeof(DockableTabControl),
            new FrameworkPropertyMetadata());

        public bool IsPanelShown
        {
            get => (bool)GetValue(IsPanelShownProperty);
            set => SetValue(IsPanelShownProperty, value);
        }

        public DockableTabControl()
        {
        }

        static DockableTabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DockableTabControl), new FrameworkPropertyMetadata(typeof(DockableTabControl)));
        }
        protected override void OnLostFocus(RoutedEventArgs e)
        {
        }
        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            if (e.OriginalSource is DockableTabControl control)
            {
                Focus();
                IsPanelShown = true;
                SelectedItem = null;
            }
        }
    }
}
