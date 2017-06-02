﻿using Microsoft.Win32;
using System;
using System.IO;

namespace Abide
{
    public static class RegistrySettings
    {
        public static string AddOnsDirectory
        {
            get { SetDefault("AddOns", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Abide", "AddOns")); return GetValue<string>("AddOns"); }
            set { SetValue("AddOns", value); }
        }

        private static readonly RegistryKey abide = Registry.CurrentUser.CreateSubKey(@"Software\Xbox\Halo2\Abide");
        private static readonly RegistryKey halo2 = Registry.CurrentUser.CreateSubKey(@"Software\Xbox\Halo2");
        
        private static T GetValue<T>(string name)
        {
            return (T)abide.GetValue(name);
        }
        private static void SetValue(string name, object value)
        {
            abide.SetValue(name, value);
        }
        private static T GetValue<T>(string subkey, string name)
        {
            T value = default(T);
            using (RegistryKey key = abide.CreateSubKey(subkey))
                value = (T)key.GetValue(name);
            return value;
        }
        private static void SetValue(string subkey, string name, object value)
        {
            using (RegistryKey key = abide.CreateSubKey(subkey))
                key.SetValue(name, value);
        }
        private static void SetDefault(string name, object value)
        {
            if (abide.GetValue(name) == null)
                abide.SetValue(name, value); 
        }
        private static void SetDefault(string subkey, string name, object value)
        {
            using (RegistryKey key = abide.CreateSubKey(subkey))
                if (key.GetValue(name) == null)
                    key.SetValue(name, value);
        }
    }
}
