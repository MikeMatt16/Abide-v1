using Abide.Tag;
using System.Collections.ObjectModel;
using System.Windows;

namespace Abide.Wpf.Modules.Editors.Halo2.ValueEditors
{
    /// <summary>
    /// Interaction logic for BlockIndexValueEditor.xaml
    /// </summary>
    public partial class BlockIndexValueEditor : ValueEditorBase
    {
        public static readonly DependencyProperty SelectedOptionProperty =
            DependencyProperty.Register(nameof(SelectedOption), typeof(Option), typeof(BlockIndexValueEditor), new PropertyMetadata(SelectedOptionPropertyChanged));

        private BaseBlockIndexField indexField = null;

        public ObservableCollection<Option> Options => indexField?.Options ?? null;
        public Option SelectedOption
        {
            get => (Option)GetValue(SelectedOptionProperty);
            set => SetValue(SelectedOptionProperty, value);
        }

        public BlockIndexValueEditor()
        {
            InitializeComponent();
        }
        protected override void OnFieldPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is BaseBlockIndexField field)
            {
                indexField = field;
                NotifyPropertyChanged(nameof(Options));
                SelectedOption = indexField.SelectedOption;
            }
        }

        private static void SelectedOptionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BlockIndexValueEditor editor && editor.PropogateChanges)
            {
                if (e.NewValue is Option option)
                {
                    System.Diagnostics.Debugger.Break();
                    editor.indexField.SelectedOption = option;
                }
            }
        }
    }
}
