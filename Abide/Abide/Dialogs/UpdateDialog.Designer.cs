namespace Abide.Dialogs
{
    partial class UpdateDialog
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
            this.notesRichTextBox = new System.Windows.Forms.RichTextBox();
            this.downloadButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.downloadProgressBar = new System.Windows.Forms.ProgressBar();
            this.downloadProgressLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // notesRichTextBox
            // 
            this.notesRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.notesRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.notesRichTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.notesRichTextBox.Location = new System.Drawing.Point(12, 12);
            this.notesRichTextBox.Name = "notesRichTextBox";
            this.notesRichTextBox.ReadOnly = true;
            this.notesRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.notesRichTextBox.Size = new System.Drawing.Size(460, 179);
            this.notesRichTextBox.TabIndex = 0;
            this.notesRichTextBox.Text = "";
            // 
            // downloadButton
            // 
            this.downloadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadButton.Enabled = false;
            this.downloadButton.Location = new System.Drawing.Point(397, 226);
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Size = new System.Drawing.Size(75, 23);
            this.downloadButton.TabIndex = 1;
            this.downloadButton.Text = "&Download";
            this.downloadButton.UseVisualStyleBackColor = true;
            this.downloadButton.Click += new System.EventHandler(this.downloadButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(12, 226);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 2;
            this.closeButton.Text = "&Close";
            this.closeButton.UseVisualStyleBackColor = true;
            // 
            // downloadProgressBar
            // 
            this.downloadProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadProgressBar.Location = new System.Drawing.Point(12, 197);
            this.downloadProgressBar.Name = "downloadProgressBar";
            this.downloadProgressBar.Size = new System.Drawing.Size(460, 23);
            this.downloadProgressBar.TabIndex = 3;
            // 
            // downloadProgressLabel
            // 
            this.downloadProgressLabel.Location = new System.Drawing.Point(93, 226);
            this.downloadProgressLabel.Name = "downloadProgressLabel";
            this.downloadProgressLabel.Size = new System.Drawing.Size(298, 23);
            this.downloadProgressLabel.TabIndex = 4;
            this.downloadProgressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UpdateDialog
            // 
            this.AcceptButton = this.downloadButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(484, 261);
            this.Controls.Add(this.downloadProgressLabel);
            this.Controls.Add(this.downloadProgressBar);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.downloadButton);
            this.Controls.Add(this.notesRichTextBox);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Abide Update";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UpdateDialog_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox notesRichTextBox;
        private System.Windows.Forms.Button downloadButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.ProgressBar downloadProgressBar;
        private System.Windows.Forms.Label downloadProgressLabel;
    }
}