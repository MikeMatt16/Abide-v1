using Abide.AddOnApi;
using Abide.AddOnApi.Halo2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag_Data_Editor.Halo2
{
    [AddOn]
    public class MapReset : AbideMenuButton
    {
        public MapReset()
        {
            Name = "Reset Map";
            Click += MapReset_Click;
        }

        private void MapReset_Click(object sender, EventArgs e)
        {
            //Reset
            if(Xbox!= null && Xbox.Connected)
                Xbox.SetMemory(0x547F6E, (byte)1, (byte)1);
        }
    }
}
