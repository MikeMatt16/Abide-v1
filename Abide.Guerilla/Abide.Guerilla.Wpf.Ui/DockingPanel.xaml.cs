using System.ComponentModel;
using System.Windows.Controls;

namespace Abide.Guerilla.Wpf.Ui
{
    /// <summary>
    /// Interaction logic for DockingPanel.xaml
    /// </summary>
    public partial class DockingPanel : UserControl, INotifyPropertyChanged
    {
        /// <summary>
        /// Not implemented
        /// </summary>
        public bool Floating
        {
            get
            {
                return false;
            }
            set { }
        }

        [Category("Appearance")]
        public bool AutoHide { get; set; } = false;
        public string Title
        {
            get { if (titleContent is string) return (string)titleContent; return string.Empty; }
            set { TitleContent = value; }
        }
        [Category("Miscellaneous")]
        public object TitleContent
        {
            get { return titleContent; }
            set
            {
                if(titleContent != value)
                {
                    titleContent = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TitleContent)));
                }
            }
        }

        private object titleContent;
        
        public DockingPanel()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
