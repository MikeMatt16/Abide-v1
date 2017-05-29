namespace Abide.Dialogs
{
    partial class OptionsDialog
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
            this.dialogControlsPanel = new System.Windows.Forms.Panel();
            this.settingsTabControl = new System.Windows.Forms.TabControl();
            this.abideSettingsTab = new System.Windows.Forms.TabPage();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.dialogControlsPanel.SuspendLayout();
            this.settingsTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // dialogControlsPanel
            // 
            this.dialogControlsPanel.Controls.Add(this.cancelButton);
            this.dialogControlsPanel.Controls.Add(this.okButton);
            this.dialogControlsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dialogControlsPanel.Location = new System.Drawing.Point(0, 298);
            this.dialogControlsPanel.Name = "dialogControlsPanel";
            this.dialogControlsPanel.Size = new System.Drawing.Size(328, 35);
            this.dialogControlsPanel.TabIndex = 0;
            // 
            // settingsTabControl
            // 
            this.settingsTabControl.Controls.Add(this.abideSettingsTab);
            this.settingsTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsTabControl.Location = new System.Drawing.Point(0, 0);
            this.settingsTabControl.Name = "settingsTabControl";
            this.settingsTabControl.SelectedIndex = 0;
            this.settingsTabControl.Size = new System.Drawing.Size(328, 298);
            this.settingsTabControl.TabIndex = 1;
            // 
            // abideSettingsTab
            // 
            this.abideSettingsTab.Location = new System.Drawing.Point(4, 22);
            this.abideSettingsTab.Name = "abideSettingsTab";
            this.abideSettingsTab.Padding = new System.Windows.Forms.Padding(3);
            this.abideSettingsTab.Size = new System.Drawing.Size(320, 272);
            this.abideSettingsTab.TabIndex = 0;
            this.abideSettingsTab.Text = "Abide Settings";
            this.abideSettingsTab.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(241, 6);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "&OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(12, 6);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // OptionsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 333);
            this.Controls.Add(this.settingsTabControl);
            this.Controls.Add(this.dialogControlsPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Abide Options";
            this.dialogControlsPanel.ResumeLayout(false);
            this.settingsTabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel dialogControlsPanel;
        private System.Windows.Forms.TabControl settingsTabControl;
        private System.Windows.Forms.TabPage abideSettingsTab;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
    }
}