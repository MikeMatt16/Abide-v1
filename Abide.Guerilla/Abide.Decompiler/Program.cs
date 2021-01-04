using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Abide.Decompiler
{
    static class Program
    {
        public static string FileArgument { get; private set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(params string[] args)
        {
            foreach (var argument in args)
            {
                if (File.Exists(argument))
                {
                    FileArgument = argument;
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CacheDecompiler());
        }
    }
}
