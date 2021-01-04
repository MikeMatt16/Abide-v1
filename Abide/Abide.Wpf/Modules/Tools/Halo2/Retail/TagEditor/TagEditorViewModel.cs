using Abide.HaloLibrary.Halo2.Retail;
using Abide.HaloLibrary.Halo2.Retail.Tag;
using Abide.HaloLibrary.Halo2.Retail.Tag.Generated;
using Abide.Wpf.Modules.ViewModel;
using System.IO;

namespace Abide.Wpf.Modules.Tools.Halo2.Retail.TagEditor
{
    public sealed class TagEditorViewModel : BaseAddOnViewModel
    {
        private TagGroupViewModel tagGroup = new TagGroupViewModel();
        private TagData tagData = null;

        public TagGroupViewModel TagGroup
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

        public TagEditorViewModel() { }
        protected override void OnMapChange()
        {
            tagGroup.Map = Map;
        }
        protected override void OnSelectedTagChanged()
        {
            if (SelectedTag != null)
            {
                Group tagGroup = TagLookup.CreateTagGroup(SelectedTag.GroupTag);

                if (tagGroup != null)
                {
                    if (tagData != null) tagData.Dispose();
                    tagData = Map.ReadTagData(SelectedTag);

                    using (BinaryReader reader = tagData.Stream.CreateReader())
                    {
                        reader.BaseStream.Seek(SelectedTag.MemoryAddress, SeekOrigin.Begin);
                        tagGroup.Read(reader);
                    }
                }

                TagGroup.TagGroup = tagGroup;
            }
        }
    }
}
