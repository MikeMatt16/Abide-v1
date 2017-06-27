using Abide.Updater.Compression;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Abide.Updater
{
    public partial class Main : Form
    {
        private readonly string abideDirectory;
        private readonly string applicationFilename;
        private readonly UpdatePackageFile package;
        private volatile bool launch = false;

        private Main()
        {
            InitializeComponent();
            package = new UpdatePackageFile();
        }

        public Main(string filename, string directory, string packageFilename) : this()
        {
            //Setup
            abideDirectory = directory;
            applicationFilename = filename;
            package.Load(packageFilename);

            //Delete Package File
            try { File.Delete(packageFilename); } catch { }
        }

        private void installButton_Click(object sender, EventArgs e)
        {
            //Disable
            installButton.Enabled = false;

            //Loop through
            string filename = null;
            foreach (var entry in package.Entries)
            {
                //Get and create file...
                filename = Path.Combine(abideDirectory, entry.Filename);
                using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    //Write
                    fs.Write(entry.Data, 0, entry.Length);
                    fs.Flush(); fs.Close();

                    //Setup
                    File.SetCreationTime(filename, entry.Created);
                    File.SetLastAccessTime(filename, entry.Accessed);
                    File.SetLastWriteTime(filename, entry.Modified);

                    //Log
                    Log("Writing {0}", entry.Filename);
                }
            }
            
            //Launch on close...
            launch = true;

            //Done
            Log("Update complete. Press Press Close to open Abide.");
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            //Launch?
            if (launch) Process.Start(applicationFilename);

            //Exit
            Application.Exit();
        }

        private void Log(string message, params object[] args)
        {
            //Append Line
            string line = $"[{DateTime.Now.ToShortTimeString()}] {{0}}{Environment.NewLine}";
            if (args != null) installLogRichTextBox.AppendText(string.Format(line, string.Format(message, args)));
            else installLogRichTextBox.AppendText(string.Format(line, message));

            //Goto new line
            installLogRichTextBox.Select(installLogRichTextBox.TextLength, 0);
            installLogRichTextBox.ScrollToCaret();
        }
    }
}
