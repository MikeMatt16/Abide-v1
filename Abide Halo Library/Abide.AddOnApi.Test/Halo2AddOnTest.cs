using Abide.HaloLibrary.Halo2Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abide.HaloLibrary;

namespace Abide.AddOnApi.Test
{
    class Halo2AddOnTest : IHaloAddOn<MapFile, IndexEntry>
    {
        public MapFile Map
        {
            get { return (MapFile)host.Request("Map"); }
        }

        public IndexEntry SelectedEntry
        {
            get { return (IndexEntry)host.Request("SelectedEntry"); }
        }

        public MapVersion Version
        {
            get { return MapVersion.Halo2; }
        }

        private IHost host;

        public void Initialize(IHost host)
        {
            //Setup
            this.host = host;
        }

        public void OnSelectedEntryChanged()
        {
            System.Windows.Forms.MessageBox.Show(SelectedEntry?.ToString());
        }

        public void OnMapLoad()
        {
            System.Windows.Forms.MessageBox.Show(Map.Name);
        }
    }
}
