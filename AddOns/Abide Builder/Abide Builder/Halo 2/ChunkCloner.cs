using Abide.HaloLibrary.Halo2Map;
using System.Windows.Forms;

namespace Abide.Builder.Halo_2
{
    public partial class ChunkCloner : Form
    {
        private readonly MapFile map;
        private readonly IndexEntry selectedEntry;

        private ChunkCloner()
        {
            InitializeComponent();
        }
        public ChunkCloner(MapFile map, IndexEntry selectedEntry) : this()
        {
            this.map = map;
            this.selectedEntry = selectedEntry;
        }
    }
}
