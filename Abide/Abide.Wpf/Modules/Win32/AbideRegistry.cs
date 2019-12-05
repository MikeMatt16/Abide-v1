using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;

namespace Abide.Wpf.Modules.Win32
{
    /// <summary>
    /// Exposes functions for configuring Abide with the Windows Registry.
    /// </summary>
    public static class AbideRegistry
    {
        private static readonly RegistryKey classes = Registry.CurrentUser.CreateSubKey(@"Software\Classes");
        private static readonly RegistryKey abide = Registry.CurrentUser.CreateSubKey(@"Software\Abide");
        private static readonly RegistryKey halo2 = Registry.CurrentUser.CreateSubKey(@"Software\Xbox\Halo2");
        private static readonly RegistryKey halo2b = Registry.CurrentUser.CreateSubKey(@"Software\Xbox\Halo2 Beta");

        public static TagView TagViewType
        {
            get { return (TagView)(Enum.Parse(typeof(TagView), GetValue<string>(abide, "TagViewType")) ?? TagView.TagPath); }
            set { SetValue(abide, "TagViewType", Enum.GetName(typeof(TagView), value)); }
        }
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
        public static string[] Halo2bRecentFiles
        {
            get
            {
                //Default
                for (int i = 0; i < 10; i++) SetDefault(halo2b, "Recent Files", i.ToString(), string.Empty);
                List<string> files = new List<string>();
                for (int i = 0; i < 10; i++)
                    if (!string.IsNullOrEmpty(GetValue<string>(halo2b, "Recent Files", i.ToString())))
                        files.Add(GetValue<string>(halo2b, "Recent Files", i.ToString()));

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
                    if (file != null) { SetValue(halo2b, "Recent Files", index.ToString(), file); index++; }
            }
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
        public static string Halo2bShared
        {
            get
            {
                SetDefault(halo2b, "Paths", "Shared", string.Empty);
                return GetValue<string>(halo2b, "Paths", "Shared");
            }
            set
            { SetValue(halo2b, "Paths", "Shared", value); }
        }
        public static string Halo2bSpShared
        {
            get
            {
                SetDefault(halo2b, "Paths", "SPShared", string.Empty);
                return GetValue<string>(halo2b, "Paths", "SPShared");
            }
            set
            { SetValue(halo2b, "Paths", "SPShared", value); }
        }
        public static string Halo2bMainmenu
        {
            get
            {
                SetDefault(halo2b, "Paths", "MainMenu", string.Empty);
                return GetValue<string>(halo2b, "Paths", "MainMenu");
            }
            set
            { SetValue(halo2b, "Paths", "MainMenu", value); }
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

        public enum TagView
        {
            TagType,
            TagPath,
        }
    }
}
