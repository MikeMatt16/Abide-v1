using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Abide.Wpf.Modules.AddOns
{
    public sealed class AddOnEnvironment
    {
        private readonly List<Assembly> loadedAssemblies = new List<Assembly>();

        public string Location { get; }
        public string Name { get; } = string.Empty;
        public Assembly PrimaryAssembly { get; } = null;

        private AddOnEnvironment(string directory)
        {
            AddOnManifest manifest;
            Location = directory;

            string manifestFileName = Path.Combine(directory, "Manifest.xml");

            try
            {
                manifest = new AddOnManifest();
                manifest.LoadXml(manifestFileName);
            }
            catch { manifest = null; }

            if (manifest != null)
            {
                Console.WriteLine("Attempting to load AddOn environment for \"{0}\".", manifest.Name);
                AppDomain.CurrentDomain.AssemblyResolve += Domain_AssemblyResolve;

                string primaryAssemblyFile = Path.Combine(directory, manifest.PrimaryAssemblyFile);

                if (AssemblyManager.LoadAssembly(primaryAssemblyFile))
                    PrimaryAssembly = Assembly.LoadFile(primaryAssemblyFile);

                foreach (string file in manifest)
                {
                    string assemblyFile = Path.Combine(directory, file);
                    if (AssemblyManager.LoadAssembly(assemblyFile))
                        loadedAssemblies.Add(Assembly.LoadFile(assemblyFile));
                }
            }
        }
        private AddOnEnvironment(string directory, Assembly primaryAssembly, IEnumerable<Assembly> assemblies, string name)
        {
            if (directory == null) throw new ArgumentNullException(nameof(directory));
            if (assemblies == null) throw new ArgumentNullException(nameof(assemblies));
            if (!Directory.Exists(directory)) throw new ArgumentException("Directory does not exist.", nameof(directory));

            PrimaryAssembly = primaryAssembly ?? throw new ArgumentNullException(nameof(primaryAssembly));
            Location = directory;
            Name = name;
            loadedAssemblies.AddRange(assemblies);
        }
        private Assembly Domain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            AssemblyName assemblyName = new AssemblyName(args.Name);
            Assembly asm = null;

            string possibleLibrary = $"{Path.Combine(Location, assemblyName.Name)}.dll";
            string possibleExecutable = $"{Path.Combine(Location, assemblyName.Name)}.exe";

            if (File.Exists(possibleLibrary)) asm = Assembly.LoadFile(possibleLibrary);
            else if (File.Exists(possibleExecutable)) asm = Assembly.LoadFile(possibleExecutable);

            if (asm != null) loadedAssemblies.Add(asm);

            return asm;
        }
        public override string ToString()
        {
            return Name;
        }

        public static AddOnEnvironment CreateDebugEnvironment(string assemblyPath)
        {
            if (assemblyPath == null) throw new ArgumentNullException(nameof(assemblyPath));
            if (!File.Exists(assemblyPath)) throw new FileNotFoundException("File does not exist.", nameof(assemblyPath));

            string directory = Path.GetDirectoryName(assemblyPath);
            List<Assembly> loadedAssemblies = new List<Assembly>();
            Assembly primaryAssembly = null;
            
            if (AssemblyManager.LoadAssembly(assemblyPath))
                primaryAssembly = Assembly.LoadFile(assemblyPath);

            foreach (string file in Directory.GetFiles(directory))
            {
                Assembly asm = null;
                if (AssemblyManager.LoadAssembly(file))
                    asm = Assembly.LoadFile(file);
                if (asm != null) loadedAssemblies.Add(asm);
            }

            return new AddOnEnvironment(directory, primaryAssembly, loadedAssemblies,
                $"Debug {Path.GetFileName(assemblyPath)}");
        }
        public static AddOnEnvironment Create(string directory)
        {
            if (directory == null) throw new ArgumentNullException(nameof(directory));
            if (!Directory.Exists(directory)) throw new ArgumentException("Directory does not exist.", nameof(directory));

            return new AddOnEnvironment(directory);
        }
    }
}
