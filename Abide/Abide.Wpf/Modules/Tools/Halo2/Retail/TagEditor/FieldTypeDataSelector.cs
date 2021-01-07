using Abide.Tag;
using System.Windows;
using System.Windows.Controls;

namespace Abide.Wpf.Modules.Tools.Halo2.Retail.TagEditor
{
    public sealed class FieldTypeDataSelector : DataTemplateSelector
    {
        public DataTemplate DataFieldTemplate { get; set; } = null;
        public DataTemplate BlockFieldTemplate { get; set; } = null;
        public DataTemplate StructFieldTemplate { get; set; } = null;
        public DataTemplate ExplanationTemplate { get; set; } = null;
        public DataTemplate FieldTemplate { get; set; } = null;

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is TagFieldViewModel field)
            {
                switch (field.Type)
                {
                    case FieldType.FieldBlock:
                        return BlockFieldTemplate;
                    case FieldType.FieldData:
                        return DataFieldTemplate;
                    case FieldType.FieldStruct:
                        return StructFieldTemplate;
                    case FieldType.FieldExplanation:
                        return ExplanationTemplate;
                    default:
                        return FieldTemplate;
                }
            }

            return null;
        }
    }
}
