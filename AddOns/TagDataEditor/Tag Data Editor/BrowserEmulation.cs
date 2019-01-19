using Microsoft.Win32;

namespace Tag_Data_Editor
{
    internal static class BrowserEmulation
    {
        private static readonly RegistryKey featureBrowserEmulation = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION");
        private static readonly RegistryKey internetExplorer = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Internet Explorer", false);

        /// <summary>
        /// Gets or sets the Browser Emulation for the <see cref="System.Windows.Forms.WebBrowser"/> control in Abide.
        /// </summary>
        public static int Abide
        {
            get { return GetValue<int>(featureBrowserEmulation, "Abide.exe"); }
            set { SetValue(featureBrowserEmulation, "Abide.exe", value); }
        }
        /// <summary>
        /// Gets and returns the version of Internet Explorer.
        /// </summary>
        public static string Version
        {
            get { return GetValue<string>(internetExplorer, "Version"); }
        }
        /// <summary>
        /// Gets and returns the service version of Internet Explorer.
        /// </summary>
        public static string ServiceVersion
        {
            get { return GetValue<string>(internetExplorer, "svcVersion"); }
        }

        private static T GetValue<T>(RegistryKey key, string name)
        {
            if (key.GetValue(name) == null) return default(T);
            else return (T)key.GetValue(name);
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
