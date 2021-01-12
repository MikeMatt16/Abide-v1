using System.Windows;
using System.Windows.Controls;

namespace Abide.Wpf.Modules.Editors.Halo2.ValueEditors
{
    /// <summary>
    /// Interaction logic for BasicValueEditor.xaml
    /// </summary>
    public partial class BasicValueEditor : UserControl
    {
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(string), typeof(BasicValueEditor), new PropertyMetadata(string.Empty, ValuePropertyChanged));

        public string Value
        {
            get => (string)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public BasicValueEditor()
        {
            InitializeComponent();
        }

        private static void ValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BasicValueEditor editor)
            {
                // todo mark file as dirty
            }
        }
    }
}
