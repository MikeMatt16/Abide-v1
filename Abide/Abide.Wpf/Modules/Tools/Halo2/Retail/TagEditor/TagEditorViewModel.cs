using Abide.DebugXbox;
using Abide.HaloLibrary.Halo2.Retail;
using Abide.Tag;
using Abide.Tag.Cache.Generated;
using Abide.Wpf.Modules.ViewModel;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Abide.Wpf.Modules.Tools.Halo2.Retail.TagEditor
{
    public sealed class TagEditorViewModel : BaseAddOnViewModel
    {
        public static readonly DependencyPropertyKey TagGroupPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(TagGroup), typeof(TagGroupViewModel), typeof(TagEditorViewModel), new PropertyMetadata());

        public static readonly DependencyProperty XboxProperty =
            DependencyProperty.Register(nameof(Xbox), typeof(Xbox), typeof(TagEditorViewModel));
        public static readonly DependencyProperty TagGroupProperty =
            TagGroupPropertyKey.DependencyProperty;

        private TagData tagData = null;

        public TagGroupViewModel TagGroup
        {
            get => (TagGroupViewModel)GetValue(TagGroupProperty);
            private set => SetValue(TagGroupPropertyKey, value);
        }
        public Xbox Xbox
        {
            get => (Xbox)GetValue(XboxProperty);
            set => SetValue(XboxProperty, value);
        }
        public ICommand SaveCommand { get; }
        public ICommand PokeCommand { get; }

        public TagEditorViewModel()
        {
            TagGroup = new TagGroupViewModel();

            SaveCommand = new ActionCommand(SaveTag);
            PokeCommand = new ActionCommand(PokeTag, o =>
            {
                if (Xbox == null)
                {
                    var xboxes = NameAnsweringProtocol.Discover(10);
                    if (xboxes.Any())
                    {
                        Xbox = xboxes.First();
                        Xbox.Connect();
                    }
                }

                return Xbox?.Connected ?? false;
            });
        }

        private void PokeTag(object obj)
        {
            using (var stream = Xbox.Stream)
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                TagGroup.TagGroup.Overwrite(writer);
            }
        }

        private void SaveTag(object obj)
        {
            if (tagData != null)
            {
                using (var writer = tagData.Stream.CreateWriter())
                {
                    TagGroup.TagGroup.Overwrite(writer);
                }

                Map.OverwriteTagData(tagData);
                Map.RecalculateChecksum();
            }
        }

        protected override void OnMapChange()
        {
            TagGroup.Map = Map;
        }
        protected override void OnSelectedTagChanged()
        {
            if (SelectedTag != null)
            {
                Group tagGroup = TagLookup.CreateTagGroup(SelectedTag.GroupTag);

                if (tagGroup != null)
                {
                    if (tagData != null)
                    {
                        tagData.Dispose();
                    }

                    tagData = Map.ReadTagData(SelectedTag);

                    using (BinaryReader reader = tagData.Stream.CreateReader())
                    {
                        _ = reader.BaseStream.Seek(SelectedTag.MemoryAddress, SeekOrigin.Begin);
                        tagGroup.Read(reader);
                    }
                }

                TagGroup.TagGroup = tagGroup;
            }
        }
    }
}
