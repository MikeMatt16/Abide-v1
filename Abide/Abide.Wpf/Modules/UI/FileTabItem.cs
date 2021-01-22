using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Abide.Wpf.Modules.UI
{
    [TemplatePart(Name = "PART_CloseButton", Type = typeof(Button))]
    public class FileTabItem : TabItem
    {
        public readonly RoutedEvent CloseEvent = EventManager.RegisterRoutedEvent(nameof(Close), RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(FileTabItem));

        public event RoutedEventHandler Close;
        public Button CloseButton { get; }
        public ICommand CloseCommand { get; }

        public RoutedCommand CloseTabCommand { get; }

        static FileTabItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FileTabItem), new FrameworkPropertyMetadata(typeof(FileTabItem)));
        }
    }
}
