using Abide.Tag;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Abide.Wpf.Modules.Editors.Halo2.ValueEditors
{
    public class ValueEditorBase : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty FieldProperty =
            DependencyProperty.Register(nameof(Field), typeof(Field), typeof(ValueEditorBase), new PropertyMetadata(FieldPropertyChanged));

        public event PropertyChangedEventHandler PropertyChanged;

        public Field Field
        {
            get => (Field)GetValue(FieldProperty);
            set => SetValue(FieldProperty, value);
        }
        protected bool PropogateChanges { get; private set; }

        public ValueEditorBase() : base()
        {
            Binding binding = new Binding("Information") { Source = Field, Converter = new StringToVisibilityConverter() };
            SetBinding(ToolTipProperty, binding);
        }
        protected virtual void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
        protected virtual void OnFieldPropertyChanged(DependencyPropertyChangedEventArgs e)
        {

        }
        protected void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            OnNotifyPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        static ValueEditorBase()
        {
            BackgroundProperty.OverrideMetadata(typeof(ValueEditorBase), new FrameworkPropertyMetadata(Brushes.Transparent));
        }
        private static void FieldPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ValueEditorBase editor)
            {
                if (e.NewValue is Field field)
                {
                    editor.PropogateChanges = false;
                    editor.OnFieldPropertyChanged(e);
                    editor.PropogateChanges = true;
                }
            }
        }

        private class StringToVisibilityConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is string str)
                {
                    if (string.IsNullOrWhiteSpace(str) || string.IsNullOrEmpty(str))
                    {
                        return null;
                    }
                }

                return value;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
    }
}
