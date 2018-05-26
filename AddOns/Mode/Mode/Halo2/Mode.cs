using Abide.AddOnApi;
using Abide.AddOnApi.Halo2;
using Abide.HaloLibrary.Halo2Map;
using System;
using System.Drawing;

namespace Mode.Halo2
{
    [AddOn]
    public partial class Mode : AbideTool
    {
        HaloModel model = null;

        public Mode()
        {
            InitializeComponent();
        }

        private void Mode_SelectedEntryChanged(object sender, EventArgs e)
        {
            //Check
            if(SelectedEntry.Root == HaloTags.mode)
            {
                //Initialize Model
                model = new HaloModel(Map, SelectedEntry);
            }
        }
    }
}
