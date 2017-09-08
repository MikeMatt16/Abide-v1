using Abide.HaloLibrary.Halo2Map;
using System.Windows.Forms;

namespace Tag_Data_Editor.Halo2
{
    public partial class PopoutEditor : Form
    {
        public PopoutEditor(TagEditor owner, MapFile map, IndexEntry entry) : this()
        {
            tagDataEditor.Map = map;
            tagDataEditor.Owner = owner;
            tagDataEditor.Entry = entry;
        }

        private PopoutEditor()
        {
            InitializeComponent();
        }
    }
}
