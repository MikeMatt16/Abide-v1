namespace XbExplorer
{
    partial class FileUploadDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.totalProgressBar = new System.Windows.Forms.ProgressBar();
            this.fileProgressBar = new System.Windows.Forms.ProgressBar();
            this.localFileNameLabel = new System.Windows.Forms.Label();
            this.targetFileNameLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // totalProgressBar
            // 
            this.totalProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.totalProgressBar.Location = new System.Drawing.Point(12, 12);
            this.totalProgressBar.Name = "totalProgressBar";
            this.totalProgressBar.Size = new System.Drawing.Size(560, 23);
            this.totalProgressBar.TabIndex = 0;
            // 
            // fileProgressBar
            // 
            this.fileProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileProgressBar.Location = new System.Drawing.Point(12, 41);
            this.fileProgressBar.Name = "fileProgressBar";
            this.fileProgressBar.Size = new System.Drawing.Size(560, 23);
            this.fileProgressBar.TabIndex = 1;
            // 
            // localFileNameLabel
            // 
            this.localFileNameLabel.AutoSize = true;
            this.localFileNameLabel.Location = new System.Drawing.Point(12, 72);
            this.localFileNameLabel.Name = "localFileNameLabel";
            this.localFileNameLabel.Size = new System.Drawing.Size(73, 13);
            this.localFileNameLabel.TabIndex = 2;
            this.localFileNameLabel.Text = "localFileName";
            // 
            // targetFileNameLabel
            // 
            this.targetFileNameLabel.AutoSize = true;
            this.targetFileNameLabel.Location = new System.Drawing.Point(12, 91);
            this.targetFileNameLabel.Name = "targetFileNameLabel";
            this.targetFileNameLabel.Size = new System.Drawing.Size(78, 13);
            this.targetFileNameLabel.TabIndex = 3;
            this.targetFileNameLabel.Text = "targetFileName";
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(497, 86);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // FileUploadDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 121);
            this.ControlBox = false;
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.targetFileNameLabel);
            this.Controls.Add(this.localFileNameLabel);
            this.Controls.Add(this.fileProgressBar);
            this.Controls.Add(this.totalProgressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FileUploadDialog";
            this.Text = "Uploading...";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FileUploadDialog_FormClosing);
            this.Load += new System.EventHandler(this.FileUploadDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar totalProgressBar;
        private System.Windows.Forms.ProgressBar fileProgressBar;
        private System.Windows.Forms.Label localFileNameLabel;
        private System.Windows.Forms.Label targetFileNameLabel;
        private System.Windows.Forms.Button cancelButton;
    }
}