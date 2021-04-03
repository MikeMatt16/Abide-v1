using System.Windows;
using System.Windows.Controls;

namespace Abide.Wpf.Modules.UI
{
    public class DropDownButton : HeaderedItemsControl
    {
        public static readonly DependencyProperty MaxDropDownHeightProperty =
            DependencyProperty.Register(nameof(MaxDropDownHeight), typeof(double), typeof(DropDownButton), new FrameworkPropertyMetadata(500d));
        public static readonly DependencyProperty IsDropDownOpenProperty =
            DependencyProperty.Register(nameof(IsDropDownOpen), typeof(bool), typeof(DropDownButton), new FrameworkPropertyMetadata(false));

        public double MaxDropDownHeight
        {
            get => (double)GetValue(MaxDropDownHeightProperty);
            set => SetValue(MaxDropDownHeightProperty, value);
        }
        public bool IsDropDownOpen
        {
            get => (bool)GetValue(IsDropDownOpenProperty);
            set => SetValue(IsDropDownOpenProperty, value);
        }

        public DropDownButton() { }

        static DropDownButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DropDownButton), new FrameworkPropertyMetadata(typeof(DropDownButton)));
        }
    }
}
