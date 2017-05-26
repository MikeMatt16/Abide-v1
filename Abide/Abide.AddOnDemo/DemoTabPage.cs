using Abide.AddOnApi;
using Abide.HaloLibrary.Halo2Map;
using System.Windows.Forms;
using YeloDebug;

namespace Abide.AddOnDemo
{
    public partial class DemoTabPage : TabPage<MapFile, IndexEntry, Xbox>
    {
        public DemoTabPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show(SelectedEntry.ToString());
        }
    }
}
