using Abide.Tag;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Abide.Wpf.Modules.UI
{
    /// <summary>
    /// Interaction logic for TagGroupEditorPanel.xaml
    /// </summary>
    public partial class TagGroupEditorPanel : UserControl
    {
        public static readonly DependencyProperty TagGroupProperty =
            DependencyProperty.Register(nameof(TagGroup), typeof(Group), typeof(TagGroupEditorPanel), new PropertyMetadata(TagGroupPropertyChanged));
        public static readonly DependencyProperty CanModifyTagBlocksProperty =
            DependencyProperty.Register(nameof(CanModifyTagBlocks), typeof(bool), typeof(TagGroupEditorPanel));

        public Group TagGroup
        {
            get => (Group)GetValue(TagGroupProperty);
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
    }

    internal class FieldTemplateSelector : DataTemplateSelector
    {
        public DataTemplate StandardFieldTemplate { get; set; } = null;
        public DataTemplate BlockFieldTemplate { get; set; } = null;
        public DataTemplate StructFieldTemplate { get; set; } = null;
        public DataTemplate DataFieldTemplate { get; set; } = null;
        public DataTemplate ExplanationFieldTemplate { get; set; } = null;

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is Field tagField)
            {
                switch (tagField.Type)
                {
                    case FieldType.FieldBlock:
                        return BlockFieldTemplate;
                    case FieldType.FieldData:
                        return DataFieldTemplate;
                    case FieldType.FieldExplanation:
                        return ExplanationFieldTemplate;
                    case FieldType.FieldStruct:
                        return StructFieldTemplate;
                    default:
                        return StandardFieldTemplate;
                }
            }

            return base.SelectTemplate(item, container);
        }
    }
}
