using Abide.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Abide
{
    internal static class Program
    {
        /// <summary>
        /// Gets and returns the application's <see cref="AddOnFactoryManager"/>.
        /// </summary>
        public static AddOnFactoryManager Container
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

        private static List<string> addOnAssemblies;
        private static AddOnFactoryManager addOns;
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
            addOnAssemblies = new List<string>();
            addOns = new AddOnFactoryManager();

            //Check
            if (!Directory.Exists(RegistrySettings.AddOnsDirectory)) Directory.CreateDirectory(RegistrySettings.AddOnsDirectory);

            //Load AddOns
            AddOnManifest manifest = new AddOnManifest();
            foreach (string directory in Directory.EnumerateDirectories(RegistrySettings.AddOnsDirectory))
            {
                //Get Manifest Path
                manifest.LoadXml(Path.Combine(directory, "Manifest.xml"));

                //Load
                string assemblyPath = Path.Combine(directory, manifest.PrimaryAssemblyFile);
                if (File.Exists(assemblyPath)) addOnAssemblies.Add(assemblyPath);
            }

            //Handle Arguments
            if (Main_HandleArguments(args))
                Main_Continue();
        }
        
        private static void Main_Continue()
        {
            //Load?
            foreach (string assembly in addOnAssemblies)
                if (safeMode) addOns.AddAssemblySafe(assembly);
                else addOns.AddAssembly(assembly);

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
                    case "-d":  //Debug Mode
                        Debugger.Launch(); break;
                    case "-s":  //Safe Mode
                        safeMode = true;
                        break;

                    case "-da": //Debug AddOn Assembly
                        if (args.Length >= 2 && File.Exists(args[i + 1])) addOnAssemblies.Add(args[i + 1]);
                        break;
                }
            }

            //Return
            return cont;
        }
    }
}
