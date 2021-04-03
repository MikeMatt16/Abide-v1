using Abide.Tag;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

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
            }
        }

        private static void SelectedOptionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BlockIndexValueEditor editor && editor.PropogateChanges)
            {
                if (e.NewValue is Option option)
                {
                    System.Diagnostics.Debugger.Break();
                }
            }
        }
    }
}
