using Abide.Classes;
using Abide.Compression;
using Abide.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using System.Xml;

namespace Abide
{
    internal static class Program
    {
        /// <summary>
        /// Gets and returns true if the build is an alpha build.
        /// </summary>
        public static bool IsAlpha
        {
            get { return alpha; }
        }
        /// <summary>
        /// Gets and returns the URL of the Update Manifest.
        /// This value is constant.
        /// </summary>
        public const string UpdateManifestUrl = manifestUrl;
        /// <summary>
        /// Gets and returns the application's <see cref="Abide.UpdateManifest"/>.
        /// </summary>
        public static UpdateManifest UpdateManifest
        {
            get { return updateManifest; }
        }
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
        /// Gets and returns true if the application is in debug mode.
        /// </summary>
        public static bool DebugMode
        {
            get { return debugMode; }
        }
        /// <summary>
        /// Gets and returns true if the application should force update, false if not.
        /// </summary>
        public static bool ForceUpdate
        {
            get { return forceUpdate; }
        }
        /// <summary>
        /// Gets and returns an array of files loaded as arguments.
        /// </summary>
        public static string[] Files
        {
            get { return files.ToArray(); }
        }

        private const string manifestUrl = @"http://zaidware.com/michael.mattera/PotentialSoftware/Abide2/Update.xml";
        private readonly static List<string> addOnAssemblies = new List<string>();
        private readonly static List<string> files = new List<string>();
        private static UpdateManifest updateManifest;
        private static AddOnFactoryManager addOns;
        private static bool forceUpdate;
        private static Form mainForm;
        private static bool safeMode;
        private static bool debugMode;
        private static bool alpha = true;

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

            //Load Update Manifset
            XmlDocument updateManifestDocument = new XmlDocument();
            try { updateManifestDocument.Load(manifestUrl); }
            catch { updateManifestDocument = null; }
            if (updateManifestDocument != null) updateManifest = new UpdateManifest(updateManifestDocument);

            //Load AddOns
            AddOnManifest manifest = new AddOnManifest();
            foreach (string directory in Directory.EnumerateDirectories(AbideRegistry.AddOnsDirectory))
            {
                //Get Manifest Path
                if (File.Exists(Path.Combine(directory, "Manifest.xml")))
                {
                    //Load Manifest
                    manifest.LoadXml(Path.Combine(directory, "Manifest.xml"));

                    //Load
                    string assemblyPath = Path.Combine(directory, manifest.PrimaryAssemblyFile);
                    if (File.Exists(assemblyPath)) addOnAssemblies.Add(assemblyPath);
                }
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
            if (addOnInstaller != null) mainForm = addOnInstaller;
            else
            {
                foreach (string assembly in addOnAssemblies)
                    if (safeMode) addOns.AddAssemblySafe(assembly);
                    else addOns.AddAssembly(assembly);
            }

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
            ArgumentStack argStack = new ArgumentStack();
            string currentArgument;
            bool cont = true;

            //Push arguments to stack
            for (int i = 0; i < args.Length; i++)
                argStack.Add(args[i]);

            //Loop
            do
            {
                //Get current argument
                currentArgument = argStack.Next();

                //Check
                switch (currentArgument)
                {
                    case "-d":  //Debug Mode
                        Debugger.Launch(); debugMode = true; break;

                    case "-s":  //Safe Mode
                        safeMode = true; break;

                    case "-u":  //Force Update
                        forceUpdate = true; break;

                    case "-da": //Debug AddOn Assembly
                        string assemblyFileName = argStack.Next();
                        if (File.Exists(assemblyFileName ?? string.Empty))
                            addOnAssemblies.Add(assemblyFileName);
                        break;

                    default:
                        if (File.Exists(currentArgument))
                            files.Add(currentArgument);
                        break;
                }
            }
            while (currentArgument != null);

            //Return
            return cont;
        }

        private class ArgumentStack
        {
            private string[] buffer = new string[32];
            private int lastIndex = -1;

            public int Position { get; set; } = 0;

            public int Add(string argument)
            {
                if (argument == null) throw new ArgumentNullException(nameof(argument));
                if (buffer.Length <= lastIndex)
                {
                    int newLength = buffer.Length * 2;
                    string[] newBuffer = new string[newLength];
                    for (int i = 0; i < buffer.Length; i++)
                        newBuffer[i] = buffer[i];
                    buffer = newBuffer;
                }

                buffer[++lastIndex] = argument;
                return lastIndex;
            }
            public string Next()
            {
                if (Position < 0 || Position >= buffer.Length)
                    return null;

                return buffer[Position++];
            }
            public string Previous()
            {
                if (Position < 0 || Position > buffer.Length)
                    return null;
                return buffer[Position--];
            }
            public void Reset()
            {
                for (int i = 0; i < buffer.Length; i++)
                    buffer[i] = null;
                Position = -1;
                lastIndex = -1;
            }
        }
    }
}
