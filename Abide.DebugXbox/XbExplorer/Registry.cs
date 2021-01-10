using Microsoft.Win32;
using System.Linq;

namespace XbExplorer
{
    internal static class Registry
    {
        private static RegistryKey Classes { get; } = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"Software\Classes");

        private static bool TryGetExtensionKey(string extension, out RegistryKey extKey)
        {
            //Check current user
            extKey = Classes.OpenSubKey(extension);  //Use Local Machine as a backup

            //Return
            return extKey != null;
        }

        private static bool TryGetProgIdKey(string extension, out RegistryKey progIdKey)
        {
            //Prepare
            progIdKey = null;

            //Get Key
            if (TryGetExtensionKey(extension, out RegistryKey extKey))
                if (extKey.GetValue(string.Empty) is string progIdString)
                {
                    //Return
                    progIdKey = Classes.OpenSubKey(progIdString);
                    return progIdKey != null;
                }
                else
                {
                    RegistryKey openWithProgidsKey = extKey.OpenSubKey("OpenWithProgids");
                    if (openWithProgidsKey != null)
                    {
                        string[] progIds = openWithProgidsKey.GetValueNames();
                        if (progIds.Length > 0)
                        {
                            //Return
                            Classes.OpenSubKey(progIds[0]);
                            return progIdKey != null;
                        }
                    }
                }

            //Return
            return false;
        }

        public static ExtensionInfo GetInfo(string extension)
        {
            //Check
            if (string.IsNullOrEmpty(extension)) return new ExtensionInfo();

            //Prepare
            ExtensionInfo ExtensionInfo = new ExtensionInfo();
            if (TryGetExtensionKey(extension, out RegistryKey extKey) && TryGetProgIdKey(extension, out RegistryKey progIdKey))
            {
                //Get ProgID string
                ExtensionInfo.ProgId = progIdKey.Name.Split('\\').Last();

                //Setup
                ExtensionInfo.Type = (progIdKey.GetValue(string.Empty) ?? string.Empty).ToString();

                //Open DefaultIcon key
                RegistryKey defaultIconKey = progIdKey.OpenSubKey("DefaultIcon");

                //Check
                if (defaultIconKey == null) return ExtensionInfo;

                //Setup
                ExtensionInfo.DefaultIcon = (defaultIconKey.GetValue(string.Empty) ?? string.Empty).ToString();
            }

            //Return
            return ExtensionInfo;
        }

        public sealed class ExtensionInfo
        {
            public string ProgId { get; set; } = string.Empty;
            public string DefaultIcon { get; set; } = string.Empty;
            public string Type { get; set; } = string.Empty;
        }
    }
}
