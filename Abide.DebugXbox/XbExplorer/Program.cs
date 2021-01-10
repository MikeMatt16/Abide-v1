using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace XbExplorer
{
    public static class Program
    {
        internal static Main MainWindow { get; private set; }
        internal static List<Main> Windows { get; } = new List<Main>();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (string.IsNullOrEmpty(Properties.Settings.Default.ScreenshotDirectory))
            {
                Properties.Settings.Default.ScreenshotDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "Xbox Screenshots");
                Properties.Settings.Default.Save();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            MainWindow = new Main();
            Application.Run(MainWindow);
        }

        internal static void Exit()
        {

        }

        internal static void CreateAndShow()
        {
            var window = new Main();
            Windows.Add(window);
            window.Show();
        }
    }
}
