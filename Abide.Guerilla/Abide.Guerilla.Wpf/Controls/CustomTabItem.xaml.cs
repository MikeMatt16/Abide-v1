using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Abide.Guerilla.Wpf.Ui
{
    /// <summary>
    /// Interaction logic for CustomTabItem.xaml
    /// </summary>
    public partial class CustomTabItem : TabItem
    {
        /// <summary>
        /// Gets and returns the tab item close command.
        /// </summary>
        public ICommand CloseCommand
        {
            get { return (ICommand)GetValue(CloseCommandProperty); }
            set { SetValue(CloseCommandProperty, value); }
        }
        /// <summary>
        /// Gets and returns the tab item icon.
        /// </summary>
        public Image Icon
        {
            get { return (Image)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty CloseCommandProperty = DependencyProperty.Register("CloseCommand", typeof(ICommand),
            typeof(CustomTabItem), new PropertyMetadata(null));
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(Image),
            typeof(CustomTabItem), new PropertyMetadata(null));

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomTabItem"/> class.
        /// </summary>
        public CustomTabItem()
        {
            InitializeComponent();
        }
    }
}
