using Abide.HaloLibrary;
using System.IO;

namespace Abide
{
    internal static class MapHelper
    {
        public static MapVersion GetMapVersion(string filename)
        {
            MapVersion version = MapVersion.Halo2;
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
                version = GetMapVersion(fs);
            return version;
        }
        public static MapVersion GetMapVersion(Stream inStream)
        {
            //TODO: Implement...
            return MapVersion.Halo2;
        }
    }
}
