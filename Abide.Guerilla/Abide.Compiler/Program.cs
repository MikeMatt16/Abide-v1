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
            Console.WriteLine("Abide Compiler v{0}", typeof(Program).Assembly.GetName().Version);

            if (args.Length > 0)
            {
                string fileName = Path.GetFullPath(args[0]);
                if (File.Exists(fileName))
                    Map_Compile(fileName);
            }
        }

        private static void Map_Compile(string scenarioFileName)
        {
            // var compiler = new OldMapCompiler(scenarioFileName, RegistrySettings.WorkspaceDirectory);
            var compiler = new MapCompiler(scenarioFileName, RegistrySettings.WorkspaceDirectory);
            compiler.Compile();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
