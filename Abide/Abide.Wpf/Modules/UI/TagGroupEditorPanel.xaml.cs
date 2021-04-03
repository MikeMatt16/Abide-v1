using Abide.Tag;
using Abide.Tag.Guerilla.Generated;
using Abide.Wpf.Modules.Tag;
using Abide.Wpf.Modules.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Abide.Wpf.Modules.UI
{
    /// <summary>
    /// Interaction logic for TagGroupEditorPanel.xaml
    /// </summary>
    public partial class TagGroupEditorPanel : UserControl
    {
        public static readonly DependencyProperty TagGroupProperty =
            DependencyProperty.Register(nameof(TagGroup), typeof(AbideTagGroup), typeof(TagGroupEditorPanel), new PropertyMetadata(TagGroupPropertyChanged));
        public static readonly DependencyProperty CanModifyTagBlocksProperty =
            DependencyProperty.Register(nameof(CanModifyTagBlocks), typeof(bool), typeof(TagGroupEditorPanel), new PropertyMetadata(CanModifyTagBlocksPropertyChanged));

        public AbideTagGroup TagGroup
        {
            get => (AbideTagGroup)GetValue(TagGroupProperty);
            set => SetValue(TagGroupProperty, value);
        }
        public bool CanModifyTagBlocks
        {
            get => (bool)GetValue(CanModifyTagBlocksProperty);
            set => SetValue(CanModifyTagBlocksProperty, value);
        }

        public TagGroupEditorPanel()
        {
            InitializeComponent();
        }

        private static void TagGroupPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
        private static void CanModifyTagBlocksPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
    }

    internal sealed class StringToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool visible = true;
            bool invert = false;

            if (parameter is string stringParameter)
            {
                switch (stringParameter.ToUpper())
                {
                    case "I":   // invert output
                        invert = true;
                        break;
                }
            }

            if (value is string s)
            {
                if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
                {
                    visible = false;
                }
            }

            if (invert)
            {
                visible = !visible;
            }

            if (visible)
            {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    internal class FieldEditorTemplateSelector : DataTemplateSelector
    {
        public DataTemplate BlockEditor { get; set; } = null;
        public DataTemplate StructEditor { get; set; } = null;
        public DataTemplate StringEditor { get; set; } = null;
        public DataTemplate BoundsEditor { get; set; } = null;
        public DataTemplate EnumEditor { get; set; } = null;
        public DataTemplate BlockIndexEditor { get; set; } = null;
        public DataTemplate FlagsEditor { get; set; } = null;

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            switch (item)
            {
                case TagBlockValueField _:
                    return BlockEditor;
                case StructValueField _:
                    return StructEditor;
                case StringValueField _:
                    return StringEditor;
                case StringBoundsValueField _:
                    return BoundsEditor;
                case EnumValueField _:
                    return EnumEditor;
                case BlockIndexValueField _:
                    return BlockIndexEditor;
                case FlagsValueField _:
                    return FlagsEditor;
            }

            return base.SelectTemplate(item, container);
        }
    }
}
