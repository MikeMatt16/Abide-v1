using Abide.HaloLibrary.Halo2Map;
using System.ComponentModel;

namespace HUD_Editor.Halo2
{
    public class MapFileContainer
    {
        [Browsable(false)]
        public MapFile Map
        {
            get { return map; }
            set { map = value; }
        }

        private MapFile map;

        public MapFileContainer() { }
        public MapFileContainer(MapFile map)
        {
            this.map = map;
        }
    }
}
