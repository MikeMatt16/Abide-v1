using Microsoft.Win32;

namespace Abide.Guerilla.Library
{
    /// <summary>
    /// Provides a set of <see langword="static"/> (<see langword="Shared"/> in VisualBasic) properties to store and retrieve application settings.
    /// </summary>
    public static class RegistrySettings
    {
        private readonly static RegistryKey mainKey = Registry.CurrentUser.CreateSubKey(@"Software\Abide\Guerilla");

        /// <summary>
        /// Gets or sets the workspace directory.
        /// </summary>
        public static string WorkspaceDirectory
        {
            get { return mainKey.GetValue("WorkspaceDirectory")?.ToString() ?? null; }
            set { mainKey.SetValue("WorkspaceDirectory", value ?? string.Empty); }
        }
        /// <summary>
        /// Gets or sets the tags directory.
        /// </summary>
        public static string TagsDirectory
        {
            get { return mainKey.GetValue("TagsDirectory")?.ToString() ?? null; }
            set { mainKey.SetValue("TagsDirectory", value ?? string.Empty); }
        }
        /// <summary>
        /// Gets or sets the mainmenu file name.
        /// </summary>
        public static string MainmenuFileName
        {
            get { return mainKey.GetValue("MainmenuFileName")?.ToString() ?? null; }
            set { mainKey.SetValue("MainmenuFileName", value ?? string.Empty); }
        }
        /// <summary>
        /// Gets or sets the shared file name.
        /// </summary>
        public static string SharedFileName
        {
            get { return mainKey.GetValue("SharedFileName")?.ToString() ?? null; }
            set { mainKey.SetValue("SharedFileName", value ?? string.Empty); }
        }
        /// <summary>
        /// Gets or sets the single player shared file name.
        /// </summary>
        public static string SinglePlayerSharedFileName
        {
            get { return mainKey.GetValue("SinglePlayerSharedFileName")?.ToString() ?? null; }
            set { mainKey.SetValue("SinglePlayerSharedFileName", value ?? string.Empty); }
        }

    }
}
