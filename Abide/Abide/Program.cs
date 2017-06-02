using Abide.Classes;
using Abide.Halo2;
using System;
using System.IO;
using System.Windows.Forms;

namespace Abide
{
    internal static class Program
    {
        /// <summary>
        /// Gets and returns the application's <see cref="AddOnManager"/>.
        /// </summary>
        public static AddOnManager Container
        {
            get { return addOns; }
        }
        /// <summary>
        /// Gets and returns true if the application is to be run in safe mode.
        /// </summary>
        public static bool SafeMode
        {
            get { return safeMode; }
        }

        private static AddOnManager addOns;
        private static Form mainForm;
        private static bool safeMode;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(params string[] args)
        {
            //Prepare
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            addOns = new AddOnManager();

            //Load AddOns
            AddOnManifest manifest = new AddOnManifest();
            foreach (string directory in Directory.EnumerateDirectories(RegistrySettings.AddOnsDirectory))
            {
                //Get Manifest Path
                manifest.LoadXml(Path.Combine(directory, "Manifest.xml"));

                //Load
                string assemblyPath = Path.Combine(directory, manifest.PrimaryAssemblyFile);
                if (File.Exists(assemblyPath)) addOns.AddAssembly(assemblyPath);
            }

            //Handle Arguments
            if (Main_HandleArguments(args))
                Main_Continue();
        }
        
        private static void Main_Continue()
        {
            //Check Main Form
            if (mainForm == null) mainForm = new Main();

            //Run Main App
            Application.Run(mainForm);
        }

        private static bool Main_HandleArguments(string[] args)
        {
            //Prepare
            bool cont = true;

            //Loop
            for (int i = 0; i < args.Length; i++)
            {
                //Get Argument
                string arg = args[i];

                //Check
                switch (arg)
                {
                    case "-s":  //Safe Mode
                        safeMode = true;
                        break;

                    case "-da": //Debug AddOn Assembly
                        if (args.Length >= 2 && File.Exists(args[i + 1])) addOns.AddAssembly(args[i + 1]);
                        break;
                }
            }

            //Return
            return cont;
        }
    }
}
