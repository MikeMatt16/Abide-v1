using Abide.AddOnApi.Halo2;
using Abide.HaloLibrary.Halo2Map;
using System;

namespace Bitmap_Editor.Halo2
{
    public partial class BitmapEditor : AbideTool
    {
        private HaloBitmap bitmap;

        public BitmapEditor()
        {
            InitializeComponent();
        }

        private void BitmapEditor_SelectedEntryChanged(object sender, EventArgs e)
        {
            //Filter bitmaps
            if (SelectedEntry.Root == HaloTags.bitm)
            {
                //Dispose?
                bitmap?.Dispose();
                bitmap = new HaloBitmap(SelectedEntry);

                //Reset
                lodUpDown.Value = 1;
                bitmapUpDown.Value = 0;
                bitmapUpDown.Maximum = bitmap.BitmapCount;

                //Load
                if (bitmap.BitmapCount > 0) bitmap_LoadBitmap(0, 1);
            }
        }
        
        private void indexUpDown_ValueChanged(object sender, EventArgs e)
        {
            //Check
            if (bitmap != null) bitmap_LoadBitmap((int)bitmapUpDown.Value, (int)lodUpDown.Value);
        }

        private void bitmap_LoadBitmap(int bitmapIndex, int lodLevel)
        {
            //Get Level of Detail index
            int lodIndex = lodLevel - 1;

            //Check
            if(bitmap[bitmapIndex, lodIndex, 0] != null)
            {

            }
        }
    }
}
