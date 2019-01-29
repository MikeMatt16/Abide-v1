using System;
using System.Windows.Forms;

namespace Abide.Guerilla
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Upgrade settings
            if (Properties.Settings.Default.UpgradeRequired)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.Save();
            }

            //Check
            if (Properties.Settings.Default.FirstRun)
                Application.Run(new StartupScreen());
            else Application.Run(new AbideGuerilla());
        }
    }
}
