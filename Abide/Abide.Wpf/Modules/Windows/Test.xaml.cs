using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2.Retail;
using Abide.Tag;
using Abide.Tag.Cache;
using Abide.Tag.Cache.Generated;
using Abide.Wpf.Modules.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using Block = Abide.Tag.Block;

namespace Abide.Wpf.Modules.Windows
{
    /// <summary>
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class Test : Window
    {
        public Test()
        {
            InitializeComponent();
        }
    }

    public sealed class TestViewModel : BaseViewModel
    {
        private TagViewModel selectedTag = null;

        public HaloMapFile Map { get; } = new HaloMapFile();
        public ObservableCollection<TagViewModel> Tags { get; } = new ObservableCollection<TagViewModel>();
        public TagViewModel SelectedTag
        {
            get => selectedTag;
            set
            {
                if (selectedTag != value)
                {
                    selectedTag = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public TestViewModel()
        {
            Map.FileName = @"F:\XBox\Original\Games\Halo 2\Clean Maps\beavercreek.map";
            Map.Load();

            foreach (var tag in Map.GetTagsEnumerator())
            {
                var model = new TagViewModel(tag);
                Tags.Add(model);
            }

            foreach (var tag in Map.GetTagsEnumerator())
            {
                GetReferencedTags(tag);
            }
        }

        private void GetReferencedTags(HaloTag tag)
        {
            var model = Tags.First(t => t.Id == tag.Id);
            var group = TagLookup.CreateTagGroup(tag.Tag);

            if (group != null)
            {
                using (var data = Map.ReadTagData(tag))
                {
                    data.Stream.Position = tag.MemoryAddress;
                    group.Read(data.Stream.CreateReader());
                }
            }

            GetReferencedTags(model, group);
        }

        private void GetReferencedTags(HaloTag tag, TagViewModel owner)
        {
            var group = TagLookup.CreateTagGroup(tag.Tag);

            if (group != null)
            {
                using (var data = Map.ReadTagData(tag))
                {
                    data.Stream.Position = tag.MemoryAddress;
                    group.Read(data.Stream.CreateReader());
                }
            }

            GetReferencedTags(owner, group);
        }

        private void GetReferencedTags(TagViewModel tag, Group tagGroup)
        {
            foreach (var tagBlock in tagGroup.TagBlocks)
            {
                GetReferencedTags(tag, tagBlock);
            }
        }

        private void GetReferencedTags(TagViewModel tag, Block tagBlock)
        {
            foreach (var field in tagBlock.Fields)
            {
                switch (field)
                {
                    case TagReferenceField tagReferenceField:
                        if (tagReferenceField.Value.Id != TagId.Null)
                        {
                            var model = Tags.FirstOrDefault(t => t.Id == tagReferenceField.Value.Id);
                            if (model == null) System.Diagnostics.Debugger.Break();
                            tag.ReferencedTags.Add(model);
                        }
                        break;

                    case TagIndexField tagIndexField:
                        if (tagIndexField.Value != TagId.Null)
                        {
                            var model = Tags.FirstOrDefault(t => t.Id == tagIndexField.Value);
                            if (model != null)
                            {
                                tag.ReferencedTags.Add(model);
                            }
                        }
                        break;

                    case BlockField blockField:
                        foreach (var block in blockField.BlockList)
                        {
                            GetReferencedTags(tag, block);
                        }
                        break;

                    case StructField structField:
                        GetReferencedTags(tag, structField.Value);
                        break;
                }
            }
        }

        protected override void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedTag))
            {
                foreach (var tag in Tags)
                {
                    tag.Background = Brushes.Transparent;
                }

                if (selectedTag != null)
                {
                    selectedTag.SetBackgroundRecursively(Brushes.CornflowerBlue);
                }
            }

            base.OnNotifyPropertyChanged(e);
        }
    }

    public sealed class TagViewModel : BaseViewModel
    {
        private Brush background = Brushes.Transparent;

        public TagId Id { get; }
        public string TagName { get; }
        public TagFourCc GroupTag { get; }
        public ObservableCollection<TagViewModel> ReferencedTags { get; } = new ObservableCollection<TagViewModel>();
        public Brush Background
        {
            get => background;
            set
            {
                if (background != value)
                {
                    background = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public TagViewModel(HaloTag tag)
        {
            Id = tag.Id;
            TagName = tag.TagName;
            GroupTag = tag.Tag;
        }
        public void SetBackgroundRecursively(Brush brush)
        {
            if (Background != brush)
            {
                Background = brush;
                foreach (var tag in ReferencedTags)
                {
                    tag.SetBackgroundRecursively(brush);
                }
            }
        }
        public override string ToString()
        {
            return $"0x{Id} {TagName}.{GroupTag}";
        }
    }
}
