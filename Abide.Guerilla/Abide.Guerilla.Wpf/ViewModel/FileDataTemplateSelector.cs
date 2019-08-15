using System.Windows;
using System.Windows.Controls;

namespace Abide.Guerilla.Wpf.ViewModel
{
    /// <summary>
    /// Represents a file data template selector.
    /// </summary>
    public sealed class FileDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Default { get; set; }
        public DataTemplate TagFileTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            //Check if null
            if (item == null) return null;

            //Check if tag file
            if (item is TagFileModel)
                return TagFileTemplate;

            //Return
            return Default;
        }
    }
}
