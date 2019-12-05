using System.Windows;
using System.Windows.Controls;

namespace Abide.Wpf.Modules.ViewModel
{
    internal sealed class TabItemTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Gets or sets the data template that will be selected for <see cref="FileItem"/> objects.
        /// </summary>
        public DataTemplate FileTabItemTemplate { get; set; } = null;

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            //Check
            if (container is FrameworkElement element && item != null)
            {
                //Check
                if (item is FileItem fileTabItem)    //Check for file tab item
                    return FileTabItemTemplate;
                
                //Return
                base.SelectTemplate(item, container);
            }

            //Return
            return null;
        }
    }

    internal sealed class TabContentTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Gets or sets the data template that will be selected for <see cref="FileItem"/> objects.
        /// </summary>
        public DataTemplate FileTabContentTemplate { get; set; } = null;

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            //Check
            if (container is FrameworkElement element && item != null)
            {
                //Check
                if (item is FileItem fileTabItem)    //Check for file tab item
                    return FileTabContentTemplate;
                
                base.SelectTemplate(item, container);
            }

            //Return
            return null;
        }
    }
}
