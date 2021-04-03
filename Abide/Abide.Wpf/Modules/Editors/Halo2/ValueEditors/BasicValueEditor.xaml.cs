using Abide.Tag;
using System.Windows;

namespace Abide.Wpf.Modules.Editors.Halo2.ValueEditors
{
    /// <summary>
    /// Interaction logic for BasicValueEditor.xaml
    /// </summary>
    public partial class BasicValueEditor : ValueEditorBase
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
        protected override void OnFieldPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            Value = Field.Value.ToString();
        }
        private static void ValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BasicValueEditor editor && editor.PropogateChanges)
            {
                if (e.NewValue is string value)
                {
                    switch (editor.Field)
                    {
                        case BaseStringField stringField:
                            stringField.String = value;
                            break;

                        case NumericField numericField:
                            if (double.TryParse(value, out double num))
                            {
                                numericField.Number = num;
                            }
                            break;
                    }
                }
            }
        }
    }
}
