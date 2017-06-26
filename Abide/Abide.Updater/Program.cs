using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Abide.Updater
{
    static class Program
    {
        public const string UpdateXML = @"http://zaidware.com/michael.mattera/PotentialSoftware/Abide2/Update.xml";
        private static string workingDirectory = string.Empty;
        private static Form mainForm;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(params string[] args)
        {
            //Prepare
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            //Handle Arguments
            if (Main_HandleArguments(args))
                Main_Continue();
        }

        private static bool Main_HandleArguments(string[] args)
        {
            //Prepare
            bool cont = false;

            //Loop
            for (int i = 0; i < args.Length; i++)
            {
                //Get Argument
                string arg = args[i];

                //Check
                switch (arg)
                {
                    case "-u":      //Update Mode
                        if (args.Length >= i + 4 && File.Exists(args[i + 1]) && Directory.Exists(args[i + 2]) && File.Exists(args[i + 3]))
                        {
                            mainForm = new Main(args[i + 1], args[i + 2], args[i + 3]);
                            cont = true;
                            i += 3;
                        }
                        break;

                    case "-d":      //Debug Mode
                        Debugger.Launch();
                        break;

                    case "-mup":    //Make Update Package
                        if (args.Length >= i + 2 && Directory.Exists(args[i + 1]))
                        {
                            mainForm = new MakeUpdate(args[i + 1]);
                            cont = true;
                            i += 1;
                        }
                        break;
                }
            }

            //Return
            return cont;
        }

        private static void Main_Continue()
        {
            //Check Main Form
            if (mainForm != null) Application.Run(mainForm);
        }
    }
}
