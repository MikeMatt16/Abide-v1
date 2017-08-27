using Microsoft.Win32;

namespace Bitmap_Editor.Halo2
{
    internal static class HaloSettings
    {
        private readonly static RegistryKey Halo2 = Registry.CurrentUser.CreateSubKey(@"Software\Xbox\Halo2");
        private readonly static RegistryKey Paths = Halo2.CreateSubKey("Paths");

        public static string CleanMapsDirectory
        {
            get { return Paths.GetValue("CleanMaps") as string; }
            set { Paths.SetValue("CleanMaps", value); }
        }
        public static string MapsDirectory
        {
            get { return Paths.GetValue("Maps") as string; }
            set { Paths.SetValue("Maps", value); }
        }
        public static string MainmenuPath
        {
            get { return Paths.GetValue("MainMenu") as string; }
            set { Paths.SetValue("MainMenu", value); }
        }
        public static string SharedPath
        {
            get { return Paths.GetValue("Shared") as string; }
            set { Paths.SetValue("Shared", value); }
        }
        public static string SingleplayerSharedPath
        {
            get { return Paths.GetValue("SPShared") as string; }
            set { Paths.SetValue("SPShared", value); }
        }
        public static string IFPDirectory
        {
            get { return Paths.GetValue("Plugins") as string; }
            set { Paths.SetValue("Plugins", value); }
        }
    }
}
