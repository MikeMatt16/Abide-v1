using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Abide
{
    public sealed class AddOnPackageManifest
    {
        private string name;
        private string[] filenames;
        private int primaryAssemblyFileIndex;
        private Type[] exportedTypes;

        public AddOnPackageManifest(string primaryFile)
        {
            //Check
            if (primaryFile == null) throw new ArgumentNullException(nameof(primaryFile));
            if (!File.Exists(primaryFile)) throw new FileNotFoundException("File does not exist.", primaryFile);
        }

        private sealed class AssemblyMap
        {
            public AssemblyMap(Assembly assembly)
            {

            }
        }
    }
}
