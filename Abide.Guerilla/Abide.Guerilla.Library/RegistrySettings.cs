using Microsoft.Win32;
using System;
using System.IO;

namespace Abide.Guerilla.Library
{
    public static class RegistrySettings
    {
        private readonly static RegistryKey mainKey = Registry.CurrentUser.CreateSubKey(@"Software\Abide\Guerilla");

        public static string WorkspaceDirectory
        {
            get => (string)GetValue(mainKey, "WorkspaceDirectory", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Abide", "Guerilla"));
            set => mainKey.SetValue("WorkspaceDirectory", value ?? string.Empty);
        }
        public static string MainmenuFileName
        {
            get => (string)GetValue(mainKey, "MainmenuFileName");
            set => mainKey.SetValue("MainmenuFileName", value ?? string.Empty);
        }
        public static string SharedFileName
        {
            get => (string)GetValue(mainKey, "SharedFileName");
            set => mainKey.SetValue("SharedFileName", value ?? string.Empty);
        }
        public static string SinglePlayerSharedFileName
        {
            get => (string)GetValue(mainKey, "SinglePlayerSharedFileName");
            set => mainKey.SetValue("SinglePlayerSharedFileName", value ?? string.Empty);
        }

        private static object GetValue(RegistryKey key, string name, object defaultValue = null)
        {
            if (key.GetValue(name) == null && defaultValue != null)
            {
                key.SetValue(name, defaultValue);
            }

            return key.GetValue(name);
        }
    }
}
