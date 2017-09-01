namespace Texture_Editor.Halo2.Dialogs
{
    partial class TextureImportOptionsDialog
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
            this.lodLevelUpDown = new System.Windows.Forms.NumericUpDown();
            this.formatComboBox = new System.Windows.Forms.ComboBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.deleteLodsCheckBox = new System.Windows.Forms.CheckBox();
            this.swizzleCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.lodLevelUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // lodLevelUpDown
            // 
            this.lodLevelUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lodLevelUpDown.Location = new System.Drawing.Point(84, 12);
            this.lodLevelUpDown.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.lodLevelUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.lodLevelUpDown.Name = "lodLevelUpDown";
            this.lodLevelUpDown.Size = new System.Drawing.Size(61, 20);
            this.lodLevelUpDown.TabIndex = 0;
            this.lodLevelUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // formatComboBox
            // 
            this.formatComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.formatComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.formatComboBox.FormattingEnabled = true;
            this.formatComboBox.Location = new System.Drawing.Point(199, 11);
            this.formatComboBox.Name = "formatComboBox";
            this.formatComboBox.Size = new System.Drawing.Size(103, 21);
            this.formatComboBox.TabIndex = 1;
            this.formatComboBox.SelectedIndexChanged += new System.EventHandler(this.formatComboBox_SelectedIndexChanged);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(227, 62);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 4;
            this.okButton.Text = "&OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(12, 62);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "LOD Levels:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(151, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Format:";
            // 
            // deleteLodsCheckBox
            // 
            this.deleteLodsCheckBox.AutoSize = true;
            this.deleteLodsCheckBox.Checked = true;
            this.deleteLodsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.deleteLodsCheckBox.Location = new System.Drawing.Point(12, 38);
            this.deleteLodsCheckBox.Name = "deleteLodsCheckBox";
            this.deleteLodsCheckBox.Size = new System.Drawing.Size(126, 17);
            this.deleteLodsCheckBox.TabIndex = 3;
            this.deleteLodsCheckBox.Text = "&Delete Existing LODs";
            this.deleteLodsCheckBox.UseVisualStyleBackColor = true;
            // 
            // swizzleCheckBox
            // 
            this.swizzleCheckBox.AutoSize = true;
            this.swizzleCheckBox.Checked = true;
            this.swizzleCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.swizzleCheckBox.Location = new System.Drawing.Point(241, 39);
            this.swizzleCheckBox.Name = "swizzleCheckBox";
            this.swizzleCheckBox.Size = new System.Drawing.Size(61, 17);
            this.swizzleCheckBox.TabIndex = 2;
            this.swizzleCheckBox.Text = "&Swizzle";
            this.swizzleCheckBox.UseVisualStyleBackColor = true;
            // 
            // TextureImportOptionsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 97);
            this.Controls.Add(this.swizzleCheckBox);
            this.Controls.Add(this.deleteLodsCheckBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.formatComboBox);
            this.Controls.Add(this.lodLevelUpDown);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TextureImportOptionsDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import Options";
            ((System.ComponentModel.ISupportInitialize)(this.lodLevelUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown lodLevelUpDown;
        private System.Windows.Forms.ComboBox formatComboBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox deleteLodsCheckBox;
        private System.Windows.Forms.CheckBox swizzleCheckBox;
    }
}