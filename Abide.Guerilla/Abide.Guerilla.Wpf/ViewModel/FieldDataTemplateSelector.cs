using System.Windows;
using System.Windows.Controls;

namespace Abide.Guerilla.Wpf.ViewModel
{
    /// <summary>
    /// Represents a tag field data template selector.
    /// </summary>
    public sealed class FieldDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ExplanationFieldDataTemplate { get; set; }
        public DataTemplate StructFieldDataTemplate { get; set; }
        public DataTemplate BlockFieldDataTemplate { get; set; }
        public DataTemplate ValueFieldDataTemplate { get; set; }
        public DataTemplate EnumFieldDataTemplate { get; set; }
        public DataTemplate FlagsFieldDataTemplate { get; set; }
        public DataTemplate BoundsFieldDataTemplate { get; set; }
        public DataTemplate TagReferenceFieldDataTemplate { get; set; }
        public DataTemplate Tuple2FieldDataTemplate { get; set; }
        public DataTemplate Tuple3FieldDataTemplate { get; set; }
        public DataTemplate Tuple4FieldDataTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            //Check item for null
            if (item == null) return null;

            //Check type
            if (item is ExplanationFieldModel) return ExplanationFieldDataTemplate;
            else if (item is StructFieldModel) return StructFieldDataTemplate;
            else if (item is BlockFieldModel) return BlockFieldDataTemplate;
            else if (item is ValueFieldModel) return ValueFieldDataTemplate;
            else if (item is EnumFieldModel) return EnumFieldDataTemplate;
            else if (item is FlagsFieldModel) return FlagsFieldDataTemplate;
            else if (item is BoundsFieldModel) return BoundsFieldDataTemplate;
            else if (item is Tuple2FieldModel) return Tuple2FieldDataTemplate;
            else if (item is Tuple3FieldModel) return Tuple3FieldDataTemplate;
            else if (item is Tuple4FieldModel) return Tuple4FieldDataTemplate;
            else if (item is TagReferenceFieldModel) return TagReferenceFieldDataTemplate;

            //Return
            return null;
        }
    }
}
