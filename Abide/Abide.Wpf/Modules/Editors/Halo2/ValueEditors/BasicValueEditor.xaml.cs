using Abide.HaloLibrary;
using Abide.Tag;
using Abide.Wpf.Modules.ViewModel;
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
                    HistoryModel change = new HistoryModel("Value changed", null);
                    switch (editor.Field.Type)
                    {
                        case FieldType.FieldTag:
                            System.Diagnostics.Debugger.Break();
                            editor.Field.Value = new TagFourCc(value.ToString());
                            ApplicationSettings.GlobalState.History.Add(change);
                            break;

                        case FieldType.FieldString:
                            editor.Field.Value = new String32() { String = value.ToString() };
                            break;

                        case FieldType.FieldLongString:
                            System.Diagnostics.Debugger.Break();
                            editor.Field.Value = new String256() { String = value.ToString() };
                            break;

                        case FieldType.FieldStringId:
                        case FieldType.FieldOldStringId:
                            System.Diagnostics.Debugger.Break();
                            editor.Field.Value = value.ToString();
                            break;

                        case FieldType.FieldCharInteger:
                            if (byte.TryParse(value.ToString(), out byte b))
                            {
                                System.Diagnostics.Debugger.Break();
                                editor.Field.Value = b;
                            }
                            break;

                        case FieldType.FieldShortInteger:
                            if (short.TryParse(value.ToString(), out short s))
                            {
                                System.Diagnostics.Debugger.Break();
                                editor.Field.Value = s;
                            }
                            break;

                        case FieldType.FieldLongInteger:
                            if (int.TryParse(value.ToString(), out int i))
                            {
                                System.Diagnostics.Debugger.Break();
                                editor.Field.Value = i;
                            }
                            break;

                        case FieldType.FieldReal:
                        case FieldType.FieldAngle:
                        case FieldType.FieldRealFraction:
                            if (float.TryParse(value.ToString(), out float f))
                            {
                                System.Diagnostics.Debugger.Break();
                                editor.Field.Value = f;
                            }
                            break;
                    }
                }
            }
        }
    }
}
