using Abide.AddOnApi.Halo2;
using Abide.HaloLibrary;
using System;

namespace Abide.AddOnDemo
{
    public class DemoMenuButton : AbideMenuButton
    {
        public DemoMenuButton()
        {
            Name = "Demo Menu Button";
            MapVersion = HaloLibrary.MapVersion.Halo2;
            Click += DemoMenuButton_Click;
        }

        private void DemoMenuButton_Click(object sender, EventArgs e)
        {
            //Prepare
            TagId selectedId = SelectedEntry?.ID ?? TagId.Null;

            //Check
            Host.SelectEntry(this, Host.BrowseTag(this, selectedId));
        }
    }
}
