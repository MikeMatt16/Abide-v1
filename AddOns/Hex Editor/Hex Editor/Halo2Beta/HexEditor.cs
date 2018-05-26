using Abide.AddOnApi;
using Abide.AddOnApi.Halo2Beta;
using System;

namespace Hex_Editor.Halo2Beta
{
    [AddOn]
    public partial class HexEditor : AbideTool
    {
        public HexEditor()
        {
            InitializeComponent();
        }

        private void HexEditor_SelectedEntryChanged(object sender, EventArgs e)
        {
            //Read Tag Data
            byte[] tagData = new byte[SelectedEntry.PostProcessedSize];
            SelectedEntry.TagData.Seek(SelectedEntry.PostProcessedOffset, System.IO.SeekOrigin.Begin);
            SelectedEntry.TagData.Read(tagData, 0, SelectedEntry.PostProcessedSize);

            //Set
            tagDataHexControl.Data = tagData;
        }
    }
}
