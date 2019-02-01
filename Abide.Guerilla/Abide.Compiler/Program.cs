using Abide.Guerilla.Library;
using System;
using System.IO;

namespace Abide.Compiler
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //Check
            if (args.Length > 0)
            {
                string fileName = Path.GetFullPath(args[0]);
                if (File.Exists(fileName))
                    Map_Compile(fileName);
            }
        }

        private static void Map_Compile(string scenarioFileName)
        {
            //Compile
            MapCompiler compiler = new MapCompiler(scenarioFileName, RegistrySettings.WorkspaceDirectory);
            compiler.Compile();

            //Press any key
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
