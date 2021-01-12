using Abide.Guerilla.Library;
using Abide.Tag;
using Abide.Wpf.Modules.ViewModel;
using System.IO;
using System.Windows;

namespace Abide.Wpf.Modules.Editors.Halo2
{
    public sealed class TagGroupViewModel : BaseViewModel
    {
        public static readonly DependencyProperty FilePathProperty = 
            DependencyProperty.Register(nameof(FilePath), typeof(string), typeof(TagGroupViewModel), new PropertyMetadata(FilePathPropertyChanged));

        private AbideTagGroupFile file = null;

        public string FilePath
        {
            get => (string)GetValue(FilePathProperty);
            set => SetValue(FilePathProperty, value);
        }
        public Group TagGroup
        {
            get => file?.TagGroup;
        }

        public TagGroupViewModel() { }

        private static void FilePathPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TagGroupViewModel model)
            {
                if (e.NewValue is string filePath && File.Exists(filePath))
                {
                    model.file = new AbideTagGroupFile();
                    model.file.Load(filePath);
                    model.NotifyPropertyChanged(nameof(TagGroup));
                }
            }
        }
    }
}
