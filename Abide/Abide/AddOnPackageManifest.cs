using System;
using System.IO;
using System.Reflection;

namespace Abide
{
    internal sealed class AddOnPackageManifest
    {
        public AddOnPackageManifest(string primaryFile)
        {
            //Check
            if (primaryFile == null) throw new ArgumentNullException(nameof(primaryFile));
            if (!File.Exists(primaryFile)) throw new FileNotFoundException("File does not exist.", primaryFile);

            //Get
            Assembly primary = LoadAssemblyMemory(primaryFile);
            if (primary != null)
                new AssemblyReference(primary, $"package:\\{primaryFile}");
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
