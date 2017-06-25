namespace Abide.Dialogs
{
    partial class FileTypeRegistrationDialog
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.registerAaoCheckBox = new System.Windows.Forms.CheckBox();
            this.registerMapCheckBox = new System.Windows.Forms.CheckBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.fileTypeToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(290, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Choose the file types you would like to associate with Abide.";
            // 
            // registerAaoCheckBox
            // 
            this.registerAaoCheckBox.AutoSize = true;
            this.registerAaoCheckBox.Location = new System.Drawing.Point(12, 38);
            this.registerAaoCheckBox.Name = "registerAaoCheckBox";
            this.registerAaoCheckBox.Size = new System.Drawing.Size(51, 17);
            this.registerAaoCheckBox.TabIndex = 1;
            this.registerAaoCheckBox.Text = "*.aao";
            this.fileTypeToolTip.SetToolTip(this.registerAaoCheckBox, "Register the Abide AddOn Package file type with Abide.");
            this.registerAaoCheckBox.UseVisualStyleBackColor = true;
            // 
            // registerMapCheckBox
            // 
            this.registerMapCheckBox.AutoSize = true;
            this.registerMapCheckBox.Location = new System.Drawing.Point(69, 38);
            this.registerMapCheckBox.Name = "registerMapCheckBox";
            this.registerMapCheckBox.Size = new System.Drawing.Size(53, 17);
            this.registerMapCheckBox.TabIndex = 2;
            this.registerMapCheckBox.Text = "*.map";
            this.fileTypeToolTip.SetToolTip(this.registerMapCheckBox, "Register the Halo Map file type with Abide.");
            this.registerMapCheckBox.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(227, 77);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "&OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(12, 77);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // fileTypeToolTip
            // 
            this.fileTypeToolTip.ToolTipTitle = "Register File Type";
            // 
            // FileTypeRegistrationDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 112);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.registerMapCheckBox);
            this.Controls.Add(this.registerAaoCheckBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FileTypeRegistrationDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Register File Types...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox registerAaoCheckBox;
        private System.Windows.Forms.CheckBox registerMapCheckBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ToolTip fileTypeToolTip;
    }
}