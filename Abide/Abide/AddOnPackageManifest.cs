using System;
using System.IO;
using System.Reflection;

namespace Abide
{
    internal sealed class AddOnPackageManifest
    {
        private string ActiveDirectory { get; set; }

        public AddOnPackageManifest(string primaryFile)
        {
            //Resolve
            AppDomain.CurrentDomain.AssemblyResolve += PackageManifest_AssemblyResolve;
            ActiveDirectory = Path.GetDirectoryName(primaryFile);

            //Check
            if (primaryFile == null) throw new ArgumentNullException(nameof(primaryFile));
            if (!File.Exists(primaryFile)) throw new FileNotFoundException("File does not exist.", primaryFile);

            //Get
            Assembly primary = LoadAssemblyMemory(primaryFile);
            if (primary != null)
                new AssemblyReference(primary, $"package:\\{primaryFile}");

            //Remove
            AppDomain.CurrentDomain.AssemblyResolve -= PackageManifest_AssemblyResolve;
        }

        private Assembly PackageManifest_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            //Get assembly name
            AssemblyName assemblyName = new AssemblyName(args.Name);

            //Check for files
            if (File.Exists(Path.Combine(ActiveDirectory, $"{assemblyName.Name}.dll"))) return LoadAssemblyMemory(Path.Combine(ActiveDirectory, $"{assemblyName.Name}.dll"));
            else if (File.Exists(Path.Combine(ActiveDirectory, $"{assemblyName.Name}.exe"))) return LoadAssemblyMemory(Path.Combine(ActiveDirectory, $"{assemblyName.Name}.dll"));
            else return null;
        }

        private Assembly LoadAssemblyMemory(string filename)
        {
            //Prepare
            Assembly assembly = null;
            byte[] buffer = null;

            //Prepare
            using (MemoryStream ms = new MemoryStream())
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            { fs.CopyTo(ms); ms.Position = 0; buffer = ms.ToArray(); }

            //Load
            try { assembly = Assembly.Load(buffer); }
            catch { }

            return assembly;
        }

        private sealed class AssemblyReference
        {
            private readonly Type[] assemblyTypes;
            private readonly AssemblyName assemblyName;
            private readonly string codebase;

            public AssemblyReference(Assembly assembly, string codebase)
            {
                //Set
                this.codebase = codebase;
                assemblyName = assembly.GetName();
                assemblyTypes = assembly.GetTypes();
            }
        }
    }
}
