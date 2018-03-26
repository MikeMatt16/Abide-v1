using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Tag_Data_Editor
{
    /// <summary>
    /// Exposes functions for configuring Abide with the Windows Registry.
    /// </summary>
    internal static class AbideRegistry
    {
        private static readonly RegistryKey classes = Registry.CurrentUser.CreateSubKey(@"Software\Classes");
        private static readonly RegistryKey abide = Registry.CurrentUser.CreateSubKey(@"Software\Xbox\Halo2\Abide");
        private static readonly RegistryKey halo2 = Registry.CurrentUser.CreateSubKey(@"Software\Xbox\Halo2");
        private static readonly RegistryKey halo2b = Registry.CurrentUser.CreateSubKey(@"Software\Xbox\Halo2 Beta");

        public static string AddOnsDirectory
        {
            get
            {
                SetDefault(abide, "AddOns", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Abide", "AddOns"));
                return GetValue<string>(abide, "AddOns");
            }
            set { SetValue(abide, "AddOns", value); }
        }
        public static string[] Halo2RecentFiles
        {
            get
            {
                //Default
                for (int i = 0; i < 10; i++) SetDefault(halo2, "Recent Files", i.ToString(), string.Empty);
                List<string> files = new List<string>();
                for (int i = 0; i < 10; i++)
                    if (!string.IsNullOrEmpty(GetValue<string>(halo2, "Recent Files", i.ToString())))
                        files.Add(GetValue<string>(halo2, "Recent Files", i.ToString()));

                //Return
                return files.ToArray();
            }
            set
            {
                string[] files = new string[10];
                for (int i = 0; i < Math.Min(files.Length, value.Length); i++)
                    files[i] = value[i];

                int index = 0;
                foreach (string file in files)
                    if (file != null) { SetValue(halo2, "Recent Files", index.ToString(), file); index++; }
            }
        }
        public static string Halo2PluginsDirectory
        {
            get
            {
                SetDefault(halo2, "Paths", "Plugins", string.Empty);
                return GetValue<string>(halo2, "Paths", "Plugins");
            }
            set
            { SetValue(halo2, "Paths", "Plugins", value); }
        }
        public static string Halo2Shared
        {
            get
            {
                SetDefault(halo2, "Paths", "Shared", string.Empty);
                return GetValue<string>(halo2, "Paths", "Shared");
            }
            set
            { SetValue(halo2, "Paths", "Shared", value); }
        }
        public static string Halo2SpShared
        {
            get
            {
                SetDefault(halo2, "Paths", "SPShared", string.Empty);
                return GetValue<string>(halo2, "Paths", "SPShared");
            }
            set
            { SetValue(halo2, "Paths", "SPShared", value); }
        }
        public static string Halo2bPluginsDirectory
        {
            get
            {
                SetDefault(halo2b, "Paths", "Plugins", string.Empty);
                return GetValue<string>(halo2b, "Paths", "Plugins");
            }
            set
            { SetValue(halo2b, "Paths", "Plugins", value); }
        }
        public static string Halo2Mainmenu
        {
            get
            {
                SetDefault(halo2, "Paths", "MainMenu", string.Empty);
                return GetValue<string>(halo2, "Paths", "MainMenu");
            }
            set
            { SetValue(halo2, "Paths", "MainMenu", value); }
        }
        public static bool IsAaoRegistered
        {
            get
            {
                string progId = GetValue<string>(classes, ".aao", null);
                return progId == "Abide.aao";
            }
        }
        public static bool IsMapRegistered
        {
            get
            {
                string progId = GetValue<string>(classes, ".map", null);
                return progId == "Abide.map";
            }
        }
        public static bool IsATagRegistered
        {
            get
            {
                string progId = GetValue<string>(classes, ".aTag", null);
                return progId == "Abide.aTag";
            }
        }

        public static void UnregisterAao()
        {
            //Initialize
            using (RegistryKey aao = classes.CreateSubKey(".aao"))
            using (RegistryKey abideAao = classes.CreateSubKey("Abide.aao"))
            {
                //Set
                SetValue(aao, null, string.Empty);

                //Delete
                classes.DeleteSubKeyTree("Abide.aao");
            }
        }
        public static void UnregisterMap()
        {
            //Initialize
            using (RegistryKey map = classes.CreateSubKey(".map"))
            using (RegistryKey abideMap = classes.CreateSubKey("Abide.map"))
            {
                //Set
                SetValue(map, null, string.Empty);

                //Delete
                classes.DeleteSubKeyTree("Abide.map");
            }
        }
        public static void UnregisterATag()
        {
            //Initialize
            using (RegistryKey map = classes.CreateSubKey(".aTag"))
            using (RegistryKey abideMap = classes.CreateSubKey("Abide.aTag"))
            {
                //Set
                SetValue(map, null, string.Empty);

                //Delete
                classes.DeleteSubKeyTree("Abide.aTag");
            }
        }
        public static void RegisterAao(string executable)
        {
            //Initialize
            using (RegistryKey aao = classes.CreateSubKey(".aao"))
            using (RegistryKey abideAao = classes.CreateSubKey("Abide.aao"))
            using (RegistryKey command = abideAao.CreateSubKey(@"shell\open\command"))
            using (RegistryKey defaultIcon = abideAao.CreateSubKey("DefaultIcon"))
            {
                //Set ProgID
                SetValue(aao, null, "Abide.aao");

                //Set File Type
                SetValue(abideAao, null, "Abide AddOn Package");

                //Set icon as icon index 1 in executable.
                SetValue(defaultIcon, null, $"{executable},1");

                //Set command
                SetValue(command, null, $"\"{executable}\" \"%1\"");
            }
        }
        public static void RegisterAao()
        {
            //Register
            RegisterAao(Application.ExecutablePath);
        }
        public static void RegisterMap(string executable)
        {
            //Initialize
            using (RegistryKey map = classes.CreateSubKey(".map"))
            using (RegistryKey abideMap = classes.CreateSubKey("Abide.map"))
            using (RegistryKey command = abideMap.CreateSubKey(@"shell\open\command"))
            using (RegistryKey defaultIcon = abideMap.CreateSubKey("DefaultIcon"))
            {
                //Set ProgID
                SetValue(map, null, "Abide.map");

                //Set File Type
                SetValue(abideMap, null, "Halo Map File");

                //Set icon as icon index 1 in executable.
                SetValue(defaultIcon, null, $"{executable},2");

                //Set command
                SetValue(command, null, $"\"{executable}\" \"%1\"");
            }
        }
        public static void RegisterMap()
        {
            //Register
            RegisterMap(Application.ExecutablePath);
        }
        public static void RegisterATag(string executable)
        {
            //Initialize
            using (RegistryKey aTag = classes.CreateSubKey(".aTag"))
            using (RegistryKey abideATag = classes.CreateSubKey("Abide.aTag"))
            using (RegistryKey command = abideATag.CreateSubKey(@"shell\open\command"))
            using (RegistryKey defaultIcon = abideATag.CreateSubKey("DefaultIcon"))
            {
                //Set ProgID
                SetValue(aTag, null, "Abide.aTag");

                //Set File Type
                SetValue(abideATag, null, "Abide Halo Tag File");

                //Set icon as icon index 1 in executable.
                SetValue(defaultIcon, null, $"{executable},3");

                //Set command
                SetValue(command, null, $"\"{executable}\" \"%1\"");
            }
        }
        public static void RegisterATag()
        {
            //Register
            RegisterATag(Application.ExecutablePath);
        }

        private static T GetValue<T>(RegistryKey key, string name)
        {
            return (T)key.GetValue(name);
        }
        private static void SetValue(RegistryKey key, string name, object value)
        {
            key.SetValue(name, value);
        }
        private static T GetValue<T>(RegistryKey key, string subkey, string name)
        {
            T value = default(T);
            using (RegistryKey sub = key.CreateSubKey(subkey))
                value = (T)sub.GetValue(name);
            return value;
        }
        private static void SetValue(RegistryKey key, string subkey, string name, object value)
        {
            using (RegistryKey sub = key.CreateSubKey(subkey))
                sub.SetValue(name, value);
        }
        private static void SetDefault(RegistryKey key, string name, object value)
        {
            if (key.GetValue(name) == null)
                key.SetValue(name, value);
        }
        private static void SetDefault(RegistryKey key, string subkey, string name, object value)
        {
            using (RegistryKey sub = key.CreateSubKey(subkey))
                if (sub.GetValue(name) == null)
                    sub.SetValue(name, value);
        }
    }
}
