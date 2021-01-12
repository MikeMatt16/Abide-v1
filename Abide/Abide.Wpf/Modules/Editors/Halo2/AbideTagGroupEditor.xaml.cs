using Abide.AddOnApi;
using Abide.AddOnApi.Wpf;
using Abide.Tag.Cache.Generated;
using System.IO;

namespace Abide.Wpf.Modules.Editors.Halo2
{
    /// <summary>
    /// Interaction logic for AbideTagGroupEditor.xaml
    /// </summary>
    [AddOn]
    public partial class AbideTagGroupEditor : FileEditorControl
    {
        public AbideTagGroupEditor()
        {
            InitializeComponent();
        }
        public override bool IsValidEditor(string path)
        {
            if (File.Exists(path))
            {
                using (var stream = File.OpenRead(path))
                using (var reader = new BinaryReader(stream))
                {
                    var abideTag = reader.ReadTag();
                    var groupTag = reader.ReadTag();

                    var tagGroup = TagLookup.CreateTagGroup(groupTag);
                    if (tagGroup != null && abideTag == "atag")
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        public override void Load(string path)
        {
            base.Load(path);

            if (File.Exists(path))
            {
                DataContext = new TagGroupViewModel()
                {
                    FilePath = path
                };
            }
            else
            {
                DataContext = new TagGroupViewModel();
            }
        }
    }
}
