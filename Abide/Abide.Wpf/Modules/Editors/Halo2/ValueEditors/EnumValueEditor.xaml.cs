using Abide.Tag;
using System.Collections.ObjectModel;
using System.Windows;

namespace Abide.Wpf.Modules.Editors.Halo2.ValueEditors
{
    /// <summary>
    /// Interaction logic for EnumValueEditor.xaml
    /// </summary>
    public partial class EnumValueEditor : ValueEditorBase
    {
        public static readonly DependencyProperty SelectedOptionProperty =
            DependencyProperty.Register(nameof(SelectedOption), typeof(Option), typeof(EnumValueEditor), new PropertyMetadata(SelectedOptionPropertyChanged));

        private BaseEnumField enumField = null;

        public ObservableCollection<Option> Options => enumField?.Options ?? null;
        public Option SelectedOption
        {
            get => (Option)GetValue(SelectedOptionProperty);
            set => SetValue(SelectedOptionProperty, value);
        }

        public EnumValueEditor()
        {
            InitializeComponent();
        }
        protected override void OnFieldPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is BaseEnumField field)
            {
                enumField = field;
                NotifyPropertyChanged(nameof(Options));
                SelectedOption = enumField.SelectedOption;
            }
        }

        private static void SelectedOptionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is EnumValueEditor editor && editor.PropogateChanges)
            {
                if (e.NewValue is Option option)
                {
                    System.Diagnostics.Debugger.Break();
                    editor.enumField.SelectedOption = option;
                }
            }
        }
    }
}
