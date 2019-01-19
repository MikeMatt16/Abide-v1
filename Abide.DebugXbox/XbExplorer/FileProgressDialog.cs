using Abide.DebugXbox;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace XbExplorer
{
    public partial class FileProgressDialog : Form
    {
        public string SourceFileName
        {
            get;
            set;
        }
        public string TargetFileName
        {
            get;
            set;
        }
        public bool Completed
        {
            get;
            private set;
        }

        public FileProgressDialog()
        {
            //Initialize
            InitializeComponent();
            Completed = false;
        }
        private void FileProgressDialog_Load(object sender, System.EventArgs e)
        {
            //Setup
            sourceLabel.Text = SourceFileName;
            destinationLabel.Text = TargetFileName;
            if (Completed) DialogResult = DialogResult.OK;
        }
        public void DownloadProgressCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Completed = true;
            if (e.Error != null) throw e.Error; //Throw any errors
            
            if (IsHandleCreated)
                Invoke(new MethodInvoker(delegate { DialogResult = DialogResult.OK; }));
        }
        public void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (IsHandleCreated)
                Invoke(new MethodInvoker(delegate { fileProgressBar.Value = e.ProgressPercentage; }));
        }
        public void UploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
            if (IsHandleCreated)
                Invoke(new MethodInvoker(delegate { fileProgressBar.Value = e.ProgressPercentage; }));
        }
        public void UploadCompleted(object sender, UploadCompletedEventArgs e)
        {
            Completed = true;
            if (e.Error != null) throw e.Error; //Throw any errors

            if (IsHandleCreated)
                Invoke(new MethodInvoker(delegate { DialogResult = DialogResult.OK; }));
        }
    }
}
