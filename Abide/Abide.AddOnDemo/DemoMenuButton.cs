using Abide.AddOnApi.Halo2;
using Abide.HaloLibrary.Halo2Map;
using System;
using YeloDebug;

namespace Abide.AddOnDemo
{
    public class DemoMenuButton : MenuButton<MapFile, IndexEntry, Xbox>
    {
        public DemoMenuButton()
        {
            Name = "Demo Menu Button";
            MapVersion = HaloLibrary.MapVersion.Halo2;
            Click += DemoMenuButton_Click;
        }

        private void DemoMenuButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
