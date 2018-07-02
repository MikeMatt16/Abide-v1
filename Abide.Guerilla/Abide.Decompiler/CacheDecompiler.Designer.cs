namespace Abide.Decompiler
{
    partial class CacheDecompiler
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
            this.label1 = new System.Windows.Forms.Label();
            this.mapFilePathTextBox = new System.Windows.Forms.TextBox();
            this.browseMapButton = new System.Windows.Forms.Button();
            this.decompileButton = new System.Windows.Forms.Button();
            this.decompileProgressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "&Map:";
            // 
            // mapFilePathTextBox
            // 
            this.mapFilePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapFilePathTextBox.Location = new System.Drawing.Point(49, 14);
            this.mapFilePathTextBox.Name = "mapFilePathTextBox";
            this.mapFilePathTextBox.ReadOnly = true;
            this.mapFilePathTextBox.Size = new System.Drawing.Size(290, 20);
            this.mapFilePathTextBox.TabIndex = 1;
            this.mapFilePathTextBox.TextChanged += new System.EventHandler(this.mapFilePathTextBox_TextChanged);
            // 
            // browseMapButton
            // 
            this.browseMapButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseMapButton.Location = new System.Drawing.Point(345, 11);
            this.browseMapButton.Name = "browseMapButton";
            this.browseMapButton.Size = new System.Drawing.Size(35, 23);
            this.browseMapButton.TabIndex = 2;
            this.browseMapButton.Text = "...";
            this.browseMapButton.UseVisualStyleBackColor = true;
            this.browseMapButton.Click += new System.EventHandler(this.browseMapButton_Click);
            // 
            // decompileButton
            // 
            this.decompileButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.decompileButton.Enabled = false;
            this.decompileButton.Location = new System.Drawing.Point(12, 41);
            this.decompileButton.Name = "decompileButton";
            this.decompileButton.Size = new System.Drawing.Size(368, 23);
            this.decompileButton.TabIndex = 3;
            this.decompileButton.Text = "&Decompile";
            this.decompileButton.UseVisualStyleBackColor = true;
            this.decompileButton.Click += new System.EventHandler(this.decompileButton_Click);
            // 
            // decompileProgressBar
            // 
            this.decompileProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.decompileProgressBar.Location = new System.Drawing.Point(12, 74);
            this.decompileProgressBar.Name = "decompileProgressBar";
            this.decompileProgressBar.Size = new System.Drawing.Size(368, 23);
            this.decompileProgressBar.TabIndex = 4;
            // 
            // CacheDecompiler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 109);
            this.Controls.Add(this.decompileProgressBar);
            this.Controls.Add(this.decompileButton);
            this.Controls.Add(this.browseMapButton);
            this.Controls.Add(this.mapFilePathTextBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CacheDecompiler";
            this.Text = "Decompiler";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox mapFilePathTextBox;
        private System.Windows.Forms.Button browseMapButton;
        private System.Windows.Forms.Button decompileButton;
        private System.Windows.Forms.ProgressBar decompileProgressBar;
    }
}

