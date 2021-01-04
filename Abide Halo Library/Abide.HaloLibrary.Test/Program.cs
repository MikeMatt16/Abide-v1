using Abide.HaloLibrary.Halo2.Retail;
using Donek.Core.JSON;
using System.IO;

namespace Abide.HaloLibrary.Test
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            //Prepare
            string mapJson = null;

            //Open
            using (HaloMap map = new HaloMap(@"F:\XBox\Original\Games\Halo 2\Clean Maps\lockout.map"))
            {
                //Create serializer
                JsonSerializer serializer = new JsonSerializer();
                mapJson = serializer.Serialize(map).Stringify();

                //Save json
                using (StreamWriter writer = File.CreateText($@"F:\{map.Name}.json"))
                    writer.Write(mapJson);

                //Save
                map.Save($@"F:\{map.Name}.map");
            }
        }
    }
}
