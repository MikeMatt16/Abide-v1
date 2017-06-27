using Abide.Classes;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Abide.Dialogs
{
    public partial class UpdateDialog : Form
    {
        private readonly UpdateManifest manifest;
        private volatile string updaterFilename = string.Empty;
        private volatile string packageFilename = string.Empty;
        private volatile string downloadingName = string.Empty;
        private volatile bool working = false;

        private UpdateDialog()
        {
            InitializeComponent();
        }
        public UpdateDialog(UpdateManifest manifest) : this()
        {
            //Setup
            this.manifest = manifest;
            notesRichTextBox.AppendText(manifest.ReleaseNotes);
            downloadButton.Enabled = true;
        }

        private void downloadButton_Click(object sender, EventArgs e)
        {
            //Prepare
            updaterFilename = Path.Combine(Path.GetTempPath(), "Updater.exe");
            packageFilename = Path.GetTempFileName();

            //Disable
            downloadProgressLabel.Text = "...";
            downloadButton.Enabled = false;
            closeButton.Enabled = false;
            working = true;

            //Create Web Client
            WebClient client = new WebClient();
            client.DownloadProgressChanged += Client_DownloadProgressChanged;
            client.DownloadFileCompleted += Client_DownloadPackageCompleted;
            downloadingName = "Update package";

            //Download Package
            client.DownloadFileAsync(new Uri(manifest.PackageUrl), packageFilename);
        }

        private void Client_DownloadPackageCompleted(object sender, AsyncCompletedEventArgs e)
        {
            //Check
            if (e.Error != null)
            {
                //Done
                downloadButton.Enabled = true;
                closeButton.Enabled = true;
                working = false;

                //Throw error...
                downloadProgressBar.Value = 0;
                downloadProgressLabel.Text = e.Error.Message;
            }
            else
            {
                //Get Web Client...
                WebClient client = (WebClient)sender;
                client.DownloadFileCompleted -= Client_DownloadPackageCompleted;
                client.DownloadFileCompleted += Client_DownloadUpdaterCompleted;
                downloadingName = "Updater Application";

                //Download Updater
                client.DownloadFileAsync(new Uri(manifest.UpdaterUrl), updaterFilename);
            }
        }

        private void Client_DownloadUpdaterCompleted(object sender, AsyncCompletedEventArgs e)
        {
            //Done
            downloadButton.Enabled = true;
            closeButton.Enabled = true;
            working = false;

            //Check
            if (e.Error != null)
            {
                //Throw error...
                downloadProgressBar.Value = 0;
                downloadProgressLabel.Text = e.Error.Message;
            }
            else
            {
                //Setup
                downloadProgressBar.Value = 100;
                downloadProgressLabel.Text = "Done";

                //Close
                WebClient client = (WebClient)sender;
                client.Dispose();

                //Update?
                if (Security.ExecuteElevated(updaterFilename, Path.GetTempPath(),
                    new string[] { "-u", Application.ExecutablePath, Application.StartupPath, packageFilename }))
                    DialogResult = DialogResult.OK;
            }
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            //Prepare
            float progress = e.BytesReceived / (float)e.TotalBytesToReceive;
            downloadProgressLabel.Text = $"Downloading {downloadingName} {e.BytesReceived}/{e.TotalBytesToReceive} ({Math.Floor(progress * 100f)}%)";
            downloadProgressBar.Value = (int)(progress * 100f);
        }

        private void UpdateDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Cancel?
            if (e.CloseReason == CloseReason.UserClosing && working && 
                MessageBox.Show("Update package is downloading, are you sure you want to cancel?", 
                "Cancel?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                e.Cancel = true;
        }
    }
}
