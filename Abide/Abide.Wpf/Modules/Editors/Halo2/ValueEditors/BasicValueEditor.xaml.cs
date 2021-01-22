using Abide.HaloLibrary;
using Abide.Tag;
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
        public static readonly DependencyProperty FieldProperty =
            DependencyProperty.Register(nameof(Field), typeof(Field), typeof(BasicValueEditor), new PropertyMetadata(FieldPropertyChanged));

        public Field Field
        {
            get => (Field)GetValue(FieldProperty);
            set => SetValue(FieldProperty, value);
        }
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
                if (e.NewValue is string value)
                {
                    switch (editor.Field.Type)
                    {
                        case FieldType.FieldTag:
                            editor.Field.Value = new TagFourCc(value.ToString());
                            break;
                        case FieldType.FieldString:
                            editor.Field.Value = new String32() { String = value.ToString() };
                            break;
                        case FieldType.FieldStringId:
                        case FieldType.FieldOldStringId:
                            editor.Field.Value = value.ToString();
                            break;
                        case FieldType.FieldCharInteger:
                            if (byte.TryParse(value.ToString(), out byte b))
                            {
                                editor.Field.Value = b;
                            }
                            break;
                        case FieldType.FieldShortInteger:
                            if (short.TryParse(value.ToString(), out short s))
                            {
                                editor.Field.Value = s;
                            }
                            break;
                        case FieldType.FieldLongInteger:
                            if (int.TryParse(value.ToString(), out int i))
                            {
                                editor.Field.Value = i;
                            }
                            break;
                        case FieldType.FieldReal:
                        case FieldType.FieldAngle:
                        case FieldType.FieldRealFraction:
                            if (float.TryParse(value.ToString(), out float f))
                            {
                                editor.Field.Value = f;
                            }
                            break;
                    }
                }
            }
        }
        private static void FieldPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BasicValueEditor editor)
            {
                if (e.NewValue is Field field)
                {
                    editor.Value = field.GetValue().ToString();
                }
            }
        }
    }
}
