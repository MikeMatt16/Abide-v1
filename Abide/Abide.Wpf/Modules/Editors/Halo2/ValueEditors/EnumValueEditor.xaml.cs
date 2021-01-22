using Abide.Tag;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Abide.Wpf.Modules.Editors.Halo2.ValueEditors
{
    /// <summary>
    /// Interaction logic for EnumValueEditor.xaml
    /// </summary>
    public partial class EnumValueEditor : UserControl
    {
        public static readonly DependencyProperty FieldProperty =
            DependencyProperty.Register(nameof(Field), typeof(BaseEnumField), typeof(EnumValueEditor), new PropertyMetadata(FieldPropertyChanged));
        public static readonly DependencyProperty SelectedOptionProperty =
            DependencyProperty.Register(nameof(SelectedOption), typeof(Option), typeof(EnumValueEditor), new PropertyMetadata(SelectedOptionPropertyChanged));

        public ObservableCollection<Option> Options { get; } = new ObservableCollection<Option>();
        public BaseEnumField Field
        {
            get => (BaseEnumField)GetValue(FieldProperty);
            set => SetValue(FieldProperty, value);
        }
        public Option SelectedOption
        {
            get => (Option)GetValue(SelectedOptionProperty);
            set => SetValue(SelectedOptionProperty, value);
        }

        public EnumValueEditor()
        {
            InitializeComponent();
        }

        private static void FieldPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is EnumValueEditor editor)
            {
                if (e.NewValue is OptionField optionField)
                {
                    editor.Options.Clear();
                    foreach (var option in optionField.Options)
                    {
                        editor.Options.Add(option);
                    }

                    if (int.TryParse(optionField.Value?.ToString(), out int i))
                    {
                        if (i >= 0 && i < editor.Options.Count)
                        {
                            editor.SelectedOption = editor.Options[i];
                        }
                    }
                }
            }
        }
        private static void SelectedOptionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is EnumValueEditor editor)
            {
                if (e.NewValue is Option option)
                {
                    if (editor.Options.Contains(option))
                    {
                        int index = editor.Options.IndexOf(option);
                        switch (editor.Field.Type)
                        {
                            case FieldType.FieldCharEnum:
                                editor.Field.Value = (byte)index;
                                break;
                            case FieldType.FieldEnum:
                                editor.Field.Value = (short)index;
                                break;
                            case FieldType.FieldLongEnum:
                                editor.Field.Value = (int)index;
                                break;
                        }
                    }
                }
            }
        }
    }
}
