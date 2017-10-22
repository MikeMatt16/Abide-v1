using Abide.Guerilla.Managed;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Abide.Guerilla.Ui
{
    static class Program
    {
        /// <summary>
        /// Gets and returns the path of the 'H2Guerilla.exe' file.
        /// </summary>
        public static string H2GuerillaPath
        {
            get { return h2GuerillaPath; }
        }
        /// <summary>
        /// Gets and returns the path of the 'H2alang.dll' file.
        /// </summary>
        public static string H2alangPath
        {
            get { return h2alangPath; }
        }

        private static string h2GuerillaPath;
        private static string h2alangPath;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Prepare
            h2alangPath = Path.Combine(Application.StartupPath, "Guerilla", "H2alang.dll");
            h2GuerillaPath = Path.Combine(Application.StartupPath, "Guerilla", "H2Guerilla.exe");

            //Check H2alang.dll...
            if (!File.Exists(h2alangPath))
                using (OpenFileDialog openDlg = new OpenFileDialog())
                {
                    openDlg.Title = "Open H2alang.dll";
                    openDlg.Filter = "Dynamic Link Libraries (*.dll)|*.dll";
                    if (openDlg.ShowDialog() == DialogResult.OK)
                        h2alangPath = openDlg.FileName;
                    else return;
                }

            //Check H2alang.dll...
            if (!File.Exists(h2GuerillaPath))
                using (OpenFileDialog openDlg = new OpenFileDialog())
                {
                    openDlg.Title = "Open H2Guerilla.exe";
                    openDlg.Filter = "Application Executable (*.exe)|*.exe";
                    if (openDlg.ShowDialog() == DialogResult.OK)
                        h2GuerillaPath = openDlg.FileName;
                    else return;
                }

            //Err?
            GuerillaReader reader = new GuerillaReader(h2GuerillaPath, h2alangPath);
        }
    }
}
