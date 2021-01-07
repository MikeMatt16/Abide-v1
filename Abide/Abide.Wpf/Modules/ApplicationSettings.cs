using Abide.Wpf.Modules.AddOns;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Abide.Wpf.Modules
{
    /// <summary>
    /// Represents our current application settings.
    /// </summary>
    public static class ApplicationSettings
    {
        /// <summary>
        /// Gets and returns the list of debug assemblies.
        /// </summary>
        public static List<string> DebugAddOnPaths { get; } = new List<string>();
        /// <summary>
        /// Gets and returns the list of file paths.
        /// </summary>
        public static List<string> FilePaths { get; } = new List<string>();
        /// <summary>
        /// Gets or sets whether the application is in debug mode or not.
        /// </summary>
        public static bool DebugMode { get; set; } = false;
        /// <summary>
        /// Gets or sets whether the application is in clean mode or not.
        /// </summary>
        public static bool CleanMode { get; set; } = false;
        /// <summary>
        /// Gets or sets whether the application is in safe mode or not.
        /// </summary>
        public static bool SafeMode { get; set; } = false;
        /// <summary>
        /// Gets or sets whether the application is to force an update or not.
        /// </summary>
        public static bool ForceUpdate { get; set; } = false;
        /// <summary>
        /// Applies all of the current application settings.
        /// </summary>
        [DebuggerStepThrough]
        public static void Apply()
        {
            //Set safe mode
            AssemblyManager.SafeMode = SafeMode;

            //Check debug mode
            if (DebugMode && !Debugger.IsAttached)
            {
                _ = Debugger.Launch();    //Launch debugger
            }

            //Loop through debug AddOn paths
            foreach (string path in DebugAddOnPaths)
            {
                //Attempt to load the assembly
                if (AssemblyManager.LoadAssembly(path))
                {
                    Console.WriteLine("Loaded \"{0}\".", path);
                }
                else
                {
                    Console.WriteLine("Unable to load assembly \"{0}\".", path);
                }
            }
        }
    }
}
