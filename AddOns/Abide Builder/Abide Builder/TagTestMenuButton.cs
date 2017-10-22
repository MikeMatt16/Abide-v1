using Abide.AddOnApi.Halo2;
using System;
using System.IO;

namespace Abide.Builder
{
    public sealed class TagTestMenuButton : AbideContextMenuButton
    {
        public TagTestMenuButton() : base()
        {
            Name = "Test";
            Click += TagTestMenuButton_Click;
        }

        private void TagTestMenuButton_Click(object sender, EventArgs e)
        {
            //Check
            if (SelectedEntry != null)
                using (AbideTag tag = new AbideTag(Map, SelectedEntry)) //Initialize
                using (BinaryWriter writer = new BinaryWriter(SelectedEntry.TagData))
                {
                    //Post-process tag
                    tag.PostProcess();

                    //Goto and write
                    SelectedEntry.TagData.Seek(SelectedEntry.PostProcessedOffset, SeekOrigin.Begin);
                    writer.Write(tag.Compile((uint)SelectedEntry.PostProcessedOffset));
                }
        }
    }
}
