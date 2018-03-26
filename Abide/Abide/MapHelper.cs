using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using System;
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
            //Prepare
            MapVersion version = MapVersion.None;
            string buildString = null;
            int mapVersion = 0;

            //Check stream
            if (inStream.Length < 2048 + 4096) return version;
            if (!inStream.CanRead) throw new ArgumentException("Stream does not support reading.", nameof(inStream));

            //Create Reader
            using(BinaryReader reader = new BinaryReader(inStream))
            {
                //Read
                Tag Tag = new string(reader.ReadChars(4));
                if(Tag == "daeh")
                {
                    //Read Version
                    inStream.Seek(4, SeekOrigin.Begin);
                    mapVersion = reader.ReadInt32();

                    //Check
                    if(mapVersion == 8) //Some type of Halo 2 map...
                    {
                        //Vista is weird
                        bool mightBeVista = false;

                        //Read build string
                        inStream.Seek(288, SeekOrigin.Begin);
                        buildString = new string(reader.ReadChars(32)).Trim('\0');

                        //Check build
                        switch (buildString)
                        {
                            case "02.09.27.09809": version = MapVersion.Halo2; break;
                            case "02.06.28.07902": version = MapVersion.Halo2b; break;
                            default: mightBeVista = true; break;
                        }

                        //Check
                        if (mightBeVista)
                        {
                            inStream.Seek(304, SeekOrigin.Begin);
                            buildString = new string(reader.ReadChars(32)).Trim('\0');
                            if (buildString == "1.07.04.30.0934.main") version = MapVersion.Halo2v;
                        }
                    }
                }
            }

            //Return
            return version;
        }
    }
}
