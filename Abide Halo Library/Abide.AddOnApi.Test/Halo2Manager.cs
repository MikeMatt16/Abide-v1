using Abide.HaloLibrary.Halo2Map;
using System;
using System.Collections.Generic;

namespace Abide.AddOnApi.Test
{
    class Halo2Manager : IHost
    {
        public MapFile Map
        {
            get { return map; }
        }
        public IndexEntry SelectedEntry
        {
            get { return selectedEntry; }
            set { OnIndexEntryChanged(value); }
        }
        
        private readonly MapFile map;
        private readonly List<IHaloAddOn<MapFile, IndexEntry>> addOns;
        private IndexEntry selectedEntry = null;

        public Halo2Manager()
        {
            //Setup
            map = new MapFile();
            addOns = new List<IHaloAddOn<MapFile, IndexEntry>>();
        }

        public void LoadMap(string mapfile)
        {
            //Close
            map.Close();

            //Load
            map.Load(mapfile);

            //Trigger Updates
            addOns.ForEach(a => a.OnMapLoad());
        }

        public void Load(IHaloAddOn<MapFile, IndexEntry> addOn)
        {
            //Initialize
            addOn.Initialize(this);

            //Add
            addOns.Add(addOn);
        }

        protected void OnIndexEntryChanged(IndexEntry entry)
        {
            //Set
            selectedEntry = entry;

            //Trigger Updates
            addOns.ForEach(a => a.OnSelectedEntryChanged());
        }

        public object Request(string request, object[] args = null)
        {
            switch (request)
            {
                case "SelectedEntry": return selectedEntry;
                case "Map": return map;

                default: return null;
            }
        }
    }
}
