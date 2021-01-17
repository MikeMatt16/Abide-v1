using Abide.HaloLibrary;
using Abide.Tag;

namespace Abide.Guerilla.Library
{
    public class AbideTag : GuerillaViewModel
    {
        public TagFourCc Tag
        {
            get => GetProperty<TagFourCc>();
            set => SetProperty(value);
        }
        public TagId Id
        {
            get => GetProperty<TagId>();
            set => SetProperty(value);
        }
        public Group Group
        {
            get => GetProperty<Group>();
            set => SetProperty(value);
        }
    }
}
