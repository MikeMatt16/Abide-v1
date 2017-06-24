using Abide.Classes;
using Abide.Compression;
using Abide.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
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
        /// <summary>
        /// Gets and returns an array of files loaded as arguments.
        /// </summary>
        public static string[] Files
        {
            get { return files.ToArray(); }
        }

        private readonly static List<string> addOnAssemblies = new List<string>();
        private readonly static List<string> files = new List<string>();
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
            addOns = new AddOnFactoryManager();

            //Check
            if (!Directory.Exists(AbideRegistry.AddOnsDirectory)) Directory.CreateDirectory(AbideRegistry.AddOnsDirectory);

            //Load AddOns
            AddOnManifest manifest = new AddOnManifest();
            foreach (string directory in Directory.EnumerateDirectories(AbideRegistry.AddOnsDirectory))
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
            //Prepare
            AddOnInstaller addOnInstaller = null;
            FileInfo info = null;

            //Loop through files
            foreach (string filename in files)
            {
                //Initialize
                info = new FileInfo(filename);
                if (info.Extension == ".aao")    //Check Extension
                {
                    //Prepare...
                    AddOnPackageFile package = new AddOnPackageFile();
                    package.DecompressData += Package_DecompressData;

                    //Load
                    try { package.Load(filename); } catch { }

                    //Extract
                    try
                    {
                        if (package.Entries.Count > 0 && package.Entries.ContainsFilename("Manifest.xml"))
                        {
                            //Create?
                            if (addOnInstaller == null) addOnInstaller = new AddOnInstaller();

                            //Load Manifest
                            AddOnManifest manifest = new AddOnManifest();
                            manifest.LoadXml(package.LoadFile("Manifest.xml"));

                            //Add
                            addOnInstaller.AddPackage(package, manifest);
                        }
                    }
                    catch (Exception) { }
                }
            }

            //Check...
            if (addOnInstaller != null)
                mainForm = addOnInstaller;
            else
                foreach (string assembly in addOnAssemblies)
                    if (safeMode) addOns.AddAssemblySafe(assembly);
                    else addOns.AddAssembly(assembly);

            //Check Main Form
            if (mainForm == null) mainForm = new Main();

            //Run Main App
            Application.Run(mainForm);
        }

        internal static byte[] Package_DecompressData(byte[] data, string compressionFourCc)
        {
            //Prepare
            byte[] decompressed = null;

            //Create
            using (MemoryStream dataStream = new MemoryStream(data))
                switch (compressionFourCc)
                {
                    case "GZIP":
                        using (GZipStream zipStream = new GZipStream(dataStream, CompressionMode.Decompress))
                        using (MemoryStream ms = new MemoryStream())
                        {
                            zipStream.CopyTo(ms);
                            decompressed = ms.ToArray();
                        }
                        break;
                }

            //Return
            return decompressed;
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
                        if (args.Length >= 2 && File.Exists(args[i + 1])) addOnAssemblies.Add(args[i + 1]); i += 1;
                        break;

                    default:
                        if (File.Exists(arg)) files.Add(arg);
                        break;
                }
            }

            //Return
            return cont;
        }
    }
}
