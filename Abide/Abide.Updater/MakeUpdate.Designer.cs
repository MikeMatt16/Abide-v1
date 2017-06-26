namespace Abide.Updater
{
    partial class MakeUpdate
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
            this.filesTreeView = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.releaseNotesRichTextBox = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.createPackageButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.updatePackageUrlTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // filesTreeView
            // 
            this.filesTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filesTreeView.CheckBoxes = true;
            this.filesTreeView.Location = new System.Drawing.Point(12, 25);
            this.filesTreeView.Name = "filesTreeView";
            this.filesTreeView.Size = new System.Drawing.Size(560, 205);
            this.filesTreeView.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Choose files to include:";
            // 
            // releaseNotesRichTextBox
            // 
            this.releaseNotesRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.releaseNotesRichTextBox.Location = new System.Drawing.Point(12, 249);
            this.releaseNotesRichTextBox.Name = "releaseNotesRichTextBox";
            this.releaseNotesRichTextBox.Size = new System.Drawing.Size(560, 95);
            this.releaseNotesRichTextBox.TabIndex = 2;
            this.releaseNotesRichTextBox.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 233);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Release Notes:";
            // 
            // createPackageButton
            // 
            this.createPackageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.createPackageButton.Location = new System.Drawing.Point(456, 376);
            this.createPackageButton.Name = "createPackageButton";
            this.createPackageButton.Size = new System.Drawing.Size(116, 23);
            this.createPackageButton.TabIndex = 4;
            this.createPackageButton.Text = "&Create Package";
            this.createPackageButton.UseVisualStyleBackColor = true;
            this.createPackageButton.Click += new System.EventHandler(this.createPackageButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 353);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Update Package Url:";
            // 
            // updatePackageUrlTextBox
            // 
            this.updatePackageUrlTextBox.Location = new System.Drawing.Point(125, 350);
            this.updatePackageUrlTextBox.Name = "updatePackageUrlTextBox";
            this.updatePackageUrlTextBox.Size = new System.Drawing.Size(447, 20);
            this.updatePackageUrlTextBox.TabIndex = 6;
            // 
            // MakeUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 411);
            this.Controls.Add(this.updatePackageUrlTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.createPackageButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.releaseNotesRichTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.filesTreeView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::Abide.Updater.Properties.Resources.Potential_Software;
            this.MaximizeBox = false;
            this.Name = "MakeUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Update Package Creator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView filesTreeView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox releaseNotesRichTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button createPackageButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox updatePackageUrlTextBox;
    }
}