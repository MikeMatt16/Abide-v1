using Abide.HaloLibrary.Halo2.Retail.Tag;
using Abide.HaloLibrary.Halo2.Retail.Tag.Generated;
using System.IO;

namespace Abide.Wpf.Modules.Tools.Halo2.Retail.TagEditor
{
    public sealed class TagEditorViewModel : BaseAddOnViewModel
    {
        private TagGroupViewModel tagGroup = new TagGroupViewModel();

        public TagGroupViewModel TagGroup
        {
            get { return tagGroup; }
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

        protected override void OnSelectedEntryChanged()
        {
            if (SelectedEntry != null)
            {
                //Create tag group
                Group tagGroup = TagLookup.CreateTagGroup(SelectedEntry.Root);

                //Check
                if (tagGroup != null)
                {
                    //Goto tag data and read tag group
                    var stream = SelectedEntry.Data.GetVirtualStream();
                    stream.Seek(SelectedEntry.Address, SeekOrigin.Begin);
                    using (var reader = stream.CreateReader())
                        tagGroup.Read(reader);
                }

                //Set tag group
                TagGroup.TagGroup = tagGroup;
            }
        }
    }
}
