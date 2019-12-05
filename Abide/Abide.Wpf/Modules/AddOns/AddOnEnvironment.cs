using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Abide.Wpf.Modules.AddOns
{
    /// <summary>
    /// Represents an AddOn environment.
    /// </summary>
    public sealed class AddOnEnvironment
    {
        private readonly List<Assembly> loadedAssemblies = new List<Assembly>();

        /// <summary>
        /// Gets and returns the location of the environment.
        /// </summary>
        public string Location { get; }
        /// <summary>
        /// Gets and returns the name of the environment.
        /// </summary>
        public string Name { get; } = string.Empty;
        /// <summary>
        /// Gets and returns the primary assembly for this environment.
        /// </summary>
        public Assembly PrimaryAssembly { get; } = null;
        /// <summary>
        /// Initializes a new instance of the <see cref="AddOnEnvironment"/> class using the specified directory.
        /// </summary>
        /// <param name="directory"></param>
        public AddOnEnvironment(string directory)
        {
            //Check
            if (directory == null) throw new ArgumentNullException(nameof(directory));
            if (!Directory.Exists(directory)) throw new ArgumentException("Directory does not exist.", nameof(directory));
            Location = directory;

            //Prepare
            AddOnManifest manifest;

            //Get manifest file name
            string manifestFileName = Path.Combine(directory, "Manifest.xml");

            //Attempt to load AddOn manifest from file.
            try
            {
                manifest = new AddOnManifest();
                manifest.LoadXml(manifestFileName);
            }
            catch { manifest = null; }

            //Check
            if (manifest != null)
            {
                //Write
                Console.WriteLine("Attempting to load AddOn environment for \"{0}\".", manifest.Name);

                //Subscribe to assembly resolve event
                AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

                //Setup
                string primaryAssemblyFile = Path.Combine(directory, manifest.PrimaryAssemblyFile);

                //Load primary assembly
                if (AssemblyManager.LoadAssembly(primaryAssemblyFile))
                    PrimaryAssembly = Assembly.LoadFile(primaryAssemblyFile);

                //Attempt to load remaining files
                foreach (string file in manifest)
                {
                    //Setup
                    string assemblyFile = Path.Combine(directory, file);
                    if (AssemblyManager.LoadAssembly(assemblyFile))
                        loadedAssemblies.Add(Assembly.LoadFile(assemblyFile));
                }
            }
        }
        /// <summary>
        /// Returns the <see cref="Name"/> property.
        /// </summary>
        /// <returns>The <see cref="Name"/> property.</returns>
        public override string ToString()
        {
            return Name;
        }
        /// <summary>
        /// Creates a debug AddOn environment.
        /// </summary>
        /// <param name="assemblyPath">The path of the assembly to debug.</param>
        /// <returns></returns>
        public static AddOnEnvironment CreateDebugEnvironment(string assemblyPath)
        {
            //Prepare
            string directory = Path.GetDirectoryName(assemblyPath);
            List<Assembly> loadedAssemblies = new List<Assembly>();
            Assembly primaryAssembly = null;

            //Load assembly
            if (AssemblyManager.LoadAssembly(assemblyPath))
                primaryAssembly = Assembly.LoadFile(assemblyPath);

            //Load files
            foreach (string file in Directory.GetFiles(directory))
            {
                Assembly asm = null;
                if (AssemblyManager.LoadAssembly(file))
                    asm = Assembly.LoadFile(file);
                if (asm != null) loadedAssemblies.Add(asm);
            }

            //Return
            return new AddOnEnvironment(directory, primaryAssembly, loadedAssemblies,
                $"Debug {Path.GetFileName(assemblyPath)}");
        }

        private AddOnEnvironment(string directory, Assembly primaryAssembly, IEnumerable<Assembly> assemblies, string name = "Debug Environment")
        {
            //Check
            if (directory == null) throw new ArgumentNullException(nameof(directory));
            if (primaryAssembly == null) throw new ArgumentNullException(nameof(primaryAssembly));
            if (assemblies == null) throw new ArgumentNullException(nameof(assemblies));
            if (!Directory.Exists(directory)) throw new ArgumentException("Directory does not exist.", nameof(directory));
            Location = directory;

            //Subscribe to assembly resolve event
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            //Setup
            Name = name;
            PrimaryAssembly = primaryAssembly;
            loadedAssemblies.AddRange(assemblies);
        }
        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            //Get assembly name
            AssemblyName assemblyName = new AssemblyName(args.Name);
            Assembly asm = null;

            //Get possible file names
            string possibleLibrary = $"{Path.Combine(Location, assemblyName.Name)}.dll";
            string possibleExecutable = $"{Path.Combine(Location, assemblyName.Name)}.exe";

            //Check
            if (File.Exists(possibleLibrary)) asm = Assembly.LoadFile(possibleLibrary);
            else if (File.Exists(possibleExecutable)) asm = Assembly.LoadFile(possibleExecutable);

            //Check
            if (asm != null) loadedAssemblies.Add(asm);

            //Return
            return asm;
        }
    }
}
