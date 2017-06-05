using Abide.AddOnApi.Halo2;
using System;
using System.IO;

namespace Tag_Editor
{
    public partial class TagEditor : AbideTool
    {
        private readonly DataWrapper wrapper;

        public TagEditor()
        {
            InitializeComponent();
            wrapper = new DataWrapper();
        }

        private void TagEditor_SelectedEntryChanged(object sender, EventArgs e)
        {
            //Check selected entry
            if (SelectedEntry == null) return;

            //Clear
            wrapper.Clear();

            //Load
            using(BinaryReader reader = new BinaryReader(SelectedEntry.TagData))
            {
            }
        }
    }
}
