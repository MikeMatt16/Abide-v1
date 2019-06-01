using Abide.Guerilla.Wpf.Controls;
using Abide.Guerilla.Wpf.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Abide.Guerilla.Wpf
{
    /// <summary>
    /// Interaction logic for TagFileUserControl.xaml
    /// </summary>
    public partial class TagFileUserControl : UserControl
    {
        /// <summary>
        /// Gets and returns the Abide tag group file modal.
        /// </summary>
        private TagFileModel TagFile
        {
            get { if (DataContext is TagFileModel model) return model; return null; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TagFileUserControl"/> class.
        /// </summary>
        public TagFileUserControl()
        {
            InitializeComponent();
        }
        
        private void TagFileUserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //Check old value
            if (e.OldValue != null)
                mainStackPanel.Children.Clear();

            //Check new value
            if (e.NewValue is TagFileModel tagGroupFile)
                LoadTagGroupFile(tagGroupFile);
        }
        private void LoadTagGroupFile(TagFileModel tagGroupFile)
        {
            //Prepare
            TagBlockControl tagBlockControl = null;
            TagBlockModel tagBlockModel = null;

            //Load tag blocks
            foreach (var tagBlock in tagGroupFile.TagGroup)
            {
                //Initialize
                tagBlockModel = new TagBlockModel() { Owner = TagFile, TagBlock = tagBlock };
                tagBlockControl = new TagBlockControl() { DataContext = tagBlockModel };

                //Add
                mainStackPanel.Children.Add(tagBlockControl);
            }
        }
    }
}
