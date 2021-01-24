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
    public partial class FlagsValueEditor : ValueEditorBase
    {
        private BaseFlagsField flagsField;
        public ObservableCollection<FlagOption> Options { get; } = new ObservableCollection<FlagOption>();

        public FlagsValueEditor()
        {
            InitializeComponent();
        }
        protected override void OnFieldPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is BaseFlagsField field)
            {
                flagsField = field;
                Options.Clear();
                foreach (var option in flagsField.Options)
                {
                    var flag = 1 << (option.Index + 1);
                    Options.Add(new FlagOption(option, field.HasFlag(option), (t) =>
                    {
                        if (PropogateChanges)
                        {
                            System.Diagnostics.Debugger.Break();
                            field.SetFlag(option, t);
                        }
                    }));
                }
            }
        }
    }

    public class FlagOption : INotifyPropertyChanged
    {
        private bool isChecked = false;

        public event PropertyChangedEventHandler PropertyChanged;
        public Action<bool> FlagToggledCallback { get; }
        public Option BaseOption { get; }
        public bool IsChecked
        {
            get => isChecked;
            set
            {
                if (isChecked != value)
                {
                    isChecked = value;
                    FlagToggledCallback?.Invoke(value);
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(IsChecked)));
                }
            }
        }

        public FlagOption(Option baseOption, bool isToggled, Action<bool> callback)
        {
            isChecked = isToggled;
            FlagToggledCallback = callback;
            BaseOption = baseOption;
        }
    }
}
