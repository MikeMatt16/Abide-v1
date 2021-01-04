using Abide.HaloLibrary.Halo2.Retail;
using Abide.HaloLibrary.Halo2.Retail.Tag.Generated;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abide.Analyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            List<HaloMapFile> cleanMaps = new List<HaloMapFile>();
            HaloMapFile map = null;

            if (File.Exists(args[0]))
            {
                map = HaloMapFile.Load(args[0]);
            }

            HaloMapFile mapFile = new HaloMapFile();
            List<List<string>> tagNameLists = new List<List<string>>();

            if (Directory.Exists(@"F:\XBox\Original\Games\Halo 2\Clean Maps"))
                foreach (var fileName in Directory.GetFiles(@"F:\XBox\Original\Games\Halo 2\Clean Maps"))
                {
                    cleanMaps.Add(HaloMapFile.Load(fileName));
                }


        }
    }
}
