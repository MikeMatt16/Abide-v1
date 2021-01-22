using Abide.Guerilla.Library;
using Abide.Tag;
using Abide.Wpf.Modules.UI;
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
        private Group tagGroup = null;

        public string FilePath
        {
            get => (string)GetValue(FilePathProperty);
            set => SetValue(FilePathProperty, value);
        }
        public Group TagGroup
        {
            get => tagGroup;
            private set
            {
                if (tagGroup != value)
                {
                    tagGroup = value;
                    NotifyPropertyChanged();
                }
            }
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
                    ConvertGroup(model.file.TagGroup);
                    model.TagGroup = model.file.TagGroup;
                }
            }
        }
        private static void ConvertGroup(Group tagGroup)
        {
            for (int i = 0; i < tagGroup.TagBlockCount; i++)
            {
                ConvertBlock(tagGroup.TagBlocks[i]);
            }
        }

        private static void ConvertBlock(Block block)
        {
            for (int i = 0; i < block.FieldCount; i++)
            {
                if (block.Fields[i] is BlockField blockField)
                {
                    foreach (var tagBlock in blockField.BlockList)
                    {
                        ConvertBlock(tagBlock);
                    }

                    block.Fields[i] = new SelectableBlockField(blockField);
                }
            }
        }
    }
}
