using Abide.AddOnApi;
using System.Windows.Forms;

namespace Abide.AddOnDemo
{
    public class DemoBitmapMenuItem : ContextMenuItem<MapFile, IndexEntry, Xbox>
    {
        public DemoBitmapMenuItem()
        {
            Name = "Internalize Bitmap";
            TagFilter.Add(HaloTags.bitm);
            ApplyTagFilter = true;
            Click += DemoBitmapMenuItem_Click;
        }

        private void DemoBitmapMenuItem_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show(SelectedEntry.ToString());
        }
    }
}
