using System;
using System.Windows.Forms;

namespace Abide.TagBuilder
{
    public static class Program
    {
        [STAThread]
        public static void Main(params string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            Application.Run(new Main());
        }
    }
}
