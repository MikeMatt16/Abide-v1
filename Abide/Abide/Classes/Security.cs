using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;

namespace Abide.Classes
{
    internal static class Security
    {
        /// <summary>
        /// Normal Button
        /// </summary>
        public const int BCM_FIRST = 0x1600;

        /// <summary>
        /// Elevated Button
        /// </summary>
        public const int BCM_SETSHIELD = (BCM_FIRST + 0x000C);

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam);
        public static bool IsAdmin()
        {
            WindowsIdentity id = WindowsIdentity.GetCurrent();
            WindowsPrincipal pr = new WindowsPrincipal(id);
            return pr.IsInRole(WindowsBuiltInRole.Administrator);
        }
        public static void ElevateButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.System;
            try { SendMessage(btn.Handle, BCM_SETSHIELD, IntPtr.Zero, (IntPtr)0xFFFFFFFF); }
            catch (OverflowException) { }
        }
        public static bool Restart(params string[] arguments)
        {
            bool failed = false;
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = true;
            startInfo.WorkingDirectory = Environment.CurrentDirectory;
            startInfo.FileName = Application.ExecutablePath;
            foreach (string arg in arguments)
                startInfo.Arguments += string.Format("\"{0}\" ", arg);

            try { Process p = Process.Start(startInfo); }
            catch (System.ComponentModel.Win32Exception) { failed = true; }

            if (!failed)
                Application.Exit();

            return !failed;
        }
        public static bool RestartElevated()
        {
            //Execute
            return ExecuteElevated(out Process p, Environment.CurrentDirectory, Application.ExecutablePath, null);
        }
        public static bool RestartElevated(params string[] arguments)
        {
            //Execute
            return ExecuteElevated(out Process p, Environment.CurrentDirectory, Application.ExecutablePath, arguments);
        }
        public static bool RestartElevated(out Process process, params string[] arguments)
        {
            //Execute
            return ExecuteElevated(out process, Environment.CurrentDirectory, Application.ExecutablePath, arguments);
        }
        public static bool ExecuteElevated(string fileName, string workingDirectory)
        {
            //Execute
            return ExecuteElevated(out Process p, workingDirectory, fileName, null);
        }
        public static bool ExecuteElevated(string fileName, string workingDirectory, string[] arguments)
        {
            //Execute
            return ExecuteElevated(out Process p, workingDirectory, fileName, arguments);
        }
        public static bool ExecuteElevated(out Process process, string workingDirectory, string fileName, string[] arguments)
        {
            //Setup
            process = null;
            bool success = true;
            ProcessStartInfo startInfo = null;

            //Setup Process Start Info
            startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = true;
            startInfo.WorkingDirectory = workingDirectory;
            startInfo.FileName = fileName;
            startInfo.Verb = "runas";
            if (arguments != null)
                foreach (string arg in arguments)
                    startInfo.Arguments += string.Format("\"{0}\" ", arg);

            //Start Process...
            try { process = Process.Start(startInfo); }
            catch (System.ComponentModel.Win32Exception) { success = false; }

            //Return
            return success;
        }
        public static string GetVersionString()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString(4);
        }
    }
}
