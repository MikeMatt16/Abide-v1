using Abide.AddOnApi;
using Abide.Wpf.Modules.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Abide.Wpf.Modules.AddOns
{
    public static class AssemblyManager
    {
        public static List<AddOnEnvironment> AddOnEnvironments { get; } = new List<AddOnEnvironment>();
        public static List<Type> AddOnTypes { get; } = new List<Type>();
        public static List<Type> ProjectTypes { get; } = new List<Type>();
        public static bool SafeMode { get; set; } = false;
        public static bool LoadAssembly(string path)
        {
            if (SafeMode)
            {
                return false;
            }

            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            Assembly asm;

            try { asm = Assembly.LoadFile(path); }
            catch { asm = null; }

            return asm != null;
        }
        public static bool LoadAssemblyIntoMemory(string path)
        {
            if (SafeMode)
            {
                return false;
            }

            Assembly asm;

            try
            {
                using (FileStream fs = File.OpenRead(path))
                {
                    byte[] rawData = new byte[fs.Length];
                    _ = fs.Read(rawData, 0, rawData.Length);
                    asm = AppDomain.CurrentDomain.Load(rawData);
                }
            }
            catch { asm = null; }

            if (asm != null)
            {
                return true;
            }

            return false;
        }
        public static void InitializeAddOnTypes()
        {
            List<Type> types = new List<Type>();
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Type[] exportedTypes = new Type[0];
                try { exportedTypes = assembly.GetExportedTypes(); }
                catch { }
                finally { types.AddRange(exportedTypes); }
            }

            AddOnTypes.Clear();
            AddOnTypes.AddRange(types
                .Where(t => t.GetCustomAttribute<AddOnAttribute>() != null && t.IsClass && !t.IsAbstract)
                .Where(t => t.GetInterface(typeof(IAddOn).FullName) != null));

            ProjectTypes.Clear();
            ProjectTypes.AddRange(types
                .Where(t => t.GetCustomAttribute<ProjectTypeAttribute>() != null && t.IsClass && !t.IsAbstract));
        }
    }
}
