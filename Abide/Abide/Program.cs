using Abide.Halo2;
using Abide.HaloLibrary.Halo2Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Abide
{
    static class Program
    {
        private static Form mainForm = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(params string[] args)
        {
            //Prepare
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Handle Arguments
            if (Main_HandleArguments(args))
                Main_Continue();
        }

        private static void Main_Continue()
        {
            //Check Main Form
            if (mainForm == null) mainForm = new Main();

            //Run Main App
            Application.Run(mainForm);
        }

        private static bool Main_HandleArguments(string[] args)
        {
            //Prepare
            bool cont = true;

            //Loop
            for (int i = 0; i < args.Length; i++)
            {
                //Get Argument
                string arg = args[i];

                //Check
                switch (arg)
                {
                    case "-da":
                        switch (args[i + 1])
                        {
                            case "-h2": mainForm = Editor.DebugAssembly(args[i + 2]); break;
                        }
                        break;
                }
            }

            //Return
            return cont;
        }
    }
}
