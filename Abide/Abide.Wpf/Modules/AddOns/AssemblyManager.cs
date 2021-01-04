using Abide.AddOnApi;
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
        public static bool SafeMode { get; set; } = false;
        public static bool LoadAssembly(string path)
        {
            if (SafeMode) return false;
            if (string.IsNullOrEmpty(path)) return false;

            Assembly asm;

            try { asm = Assembly.LoadFile(path); }
            catch { asm = null; }

            return asm != null;
        }
        public static bool LoadAssemblyIntoMemory(string path)
        {
            //Check safe mode
            if (SafeMode) return false;

            //Prepare
            Assembly asm;

            try
            {
                //Open file
                using (FileStream fs = File.OpenRead(path))
                {
                    //Read assembly
                    byte[] rawData = new byte[fs.Length];
                    fs.Read(rawData, 0, rawData.Length);

                    //Load assembly
                    asm = AppDomain.CurrentDomain.Load(rawData);
                }
            }
            catch { asm = null; }

            //Check
            if (asm != null) return true;

            //Return
            return false;
        }
        public static void InitializeAddOnTypes()
        {
            //Clear
            AddOnTypes.Clear();

            //Get all types
            List<Type> types = new List<Type>();
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                types.AddRange(assembly.GetExportedTypes());

            //Filter
            AddOnTypes.AddRange(
                types.Where(t => t.GetCustomAttribute<AddOnAttribute>() != null && t.IsClass && !t.IsAbstract)
                .Where(t => t.GetInterface(typeof(IAddOn).FullName) != null));
        }
    }
}
