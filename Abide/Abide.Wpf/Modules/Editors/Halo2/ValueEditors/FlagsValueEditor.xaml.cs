using Abide.Tag;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Abide.Wpf.Modules.Editors.Halo2.ValueEditors
{
    /// <summary>
    /// Interaction logic for FlagsValueEditor.xaml
    /// </summary>
    public partial class FlagsValueEditor : UserControl
    {
        public static readonly DependencyProperty FieldProperty =
            DependencyProperty.Register(nameof(Field), typeof(BaseFlagsField), typeof(FlagsValueEditor), new PropertyMetadata(FieldPropertyChanged));

        private bool propogateChanges = true;

        public ObservableCollection<FlagOption> Options { get; } = new ObservableCollection<FlagOption>();
        public BaseFlagsField Field
        {
            get => (BaseFlagsField)GetValue(FieldProperty);
            set => SetValue(FieldProperty, value);
        }

        public FlagsValueEditor()
        {
            InitializeComponent();
        }
        private void FlagToggleCallback(Option flag, bool isToggled)
        {
            if (propogateChanges)
            {
                Field.SetFlag(flag, isToggled);
            }
        }

        private static void FieldPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FlagsValueEditor editor)
            {
                if (e.NewValue is BaseFlagsField field)
                {
                    // todo implement
                    switch (editor.Field.Type)
                    {
                        case FieldType.FieldByteFlags:
                        case FieldType.FieldWordFlags:
                        case FieldType.FieldLongFlags:
                            editor.Options.Clear();
                            editor.propogateChanges = false;
                            foreach (var option in field.Options)
                            {
                                var flag = 1 << (option.Index + 1);
                                editor.Options.Add(new FlagOption(option, field.HasFlag(option), editor.FlagToggleCallback));
                            }
                            editor.propogateChanges = true;
                            break;
                    }
                }
            }
        }
    }

    public class FlagOption : INotifyPropertyChanged
    {
        private bool isChecked = false;

        public event PropertyChangedEventHandler PropertyChanged;
        public Action<Option, bool> FlagToggledCallback { get; }
        public Option BaseOption { get; }
        public bool IsChecked
        {
            get => isChecked;
            set
            {
                if (isChecked != value)
                {
                    isChecked = value;
                    FlagToggledCallback?.Invoke(BaseOption, value);
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(IsChecked)));
                }
            }
        }

        public FlagOption(Option baseOption, bool isToggled, Action<Option, bool> callback)
        {
            isChecked = isToggled;
            FlagToggledCallback = callback;
            BaseOption = baseOption;
        }
    }
}
