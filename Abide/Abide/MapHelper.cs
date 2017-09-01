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
            int mapVersion = 0;
            int signature = 0;

            //Check stream
            if (inStream.Length < 2048 + 4096) return version;
            if (!inStream.CanRead) throw new ArgumentException("Stream does not support reading.", nameof(inStream));

            //Create Reader
            using(BinaryReader reader = new BinaryReader(inStream))
            {
                //Read
                Tag Tag = new string(reader.ReadChars(4));
                if(Tag == "head")
                {
                    //Read Version
                    inStream.Seek(4, SeekOrigin.Begin);
                    mapVersion = reader.ReadInt32();

                    //Set Basic Halo 2
                    version = MapVersion.Halo2;

                    //Check Signature
                    inStream.Seek(720, SeekOrigin.Begin);
                    signature = reader.ReadInt32();
                    if (signature == 0)
                        version = MapVersion.Halo2b;
                }
            }

            //Return
            return version;
        }
    }
}
