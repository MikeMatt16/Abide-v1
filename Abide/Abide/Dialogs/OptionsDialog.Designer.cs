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
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.settingsTabControl = new System.Windows.Forms.TabControl();
            this.abideSettingsTab = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.mainmenuBrowseButton = new System.Windows.Forms.Button();
            this.halo2MainmenuFilePathTextBox = new System.Windows.Forms.TextBox();
            this.halo2SharedFilePathTextBox = new System.Windows.Forms.TextBox();
            this.sharedBrowseButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.halo2SpSharedFilePathTextBox = new System.Windows.Forms.TextBox();
            this.spSharedBrowseButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.dialogControlsPanel.SuspendLayout();
            this.settingsTabControl.SuspendLayout();
            this.abideSettingsTab.SuspendLayout();
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
            this.abideSettingsTab.Controls.Add(this.label3);
            this.abideSettingsTab.Controls.Add(this.label2);
            this.abideSettingsTab.Controls.Add(this.label1);
            this.abideSettingsTab.Controls.Add(this.spSharedBrowseButton);
            this.abideSettingsTab.Controls.Add(this.halo2SpSharedFilePathTextBox);
            this.abideSettingsTab.Controls.Add(this.sharedBrowseButton);
            this.abideSettingsTab.Controls.Add(this.halo2SharedFilePathTextBox);
            this.abideSettingsTab.Controls.Add(this.mainmenuBrowseButton);
            this.abideSettingsTab.Controls.Add(this.halo2MainmenuFilePathTextBox);
            this.abideSettingsTab.Location = new System.Drawing.Point(4, 22);
            this.abideSettingsTab.Name = "abideSettingsTab";
            this.abideSettingsTab.Padding = new System.Windows.Forms.Padding(3);
            this.abideSettingsTab.Size = new System.Drawing.Size(320, 272);
            this.abideSettingsTab.TabIndex = 0;
            this.abideSettingsTab.Text = "Abide Settings";
            this.abideSettingsTab.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Halo 2 Mainmenu:";
            // 
            // mainmenuBrowseButton
            // 
            this.mainmenuBrowseButton.Location = new System.Drawing.Point(283, 64);
            this.mainmenuBrowseButton.Name = "mainmenuBrowseButton";
            this.mainmenuBrowseButton.Size = new System.Drawing.Size(31, 23);
            this.mainmenuBrowseButton.TabIndex = 5;
            this.mainmenuBrowseButton.Text = "...";
            this.mainmenuBrowseButton.UseVisualStyleBackColor = true;
            this.mainmenuBrowseButton.Click += new System.EventHandler(this.mainmenuBrowseButton_Click);
            // 
            // halo2MainmenuFilePathTextBox
            // 
            this.halo2MainmenuFilePathTextBox.Location = new System.Drawing.Point(109, 66);
            this.halo2MainmenuFilePathTextBox.Name = "halo2MainmenuFilePathTextBox";
            this.halo2MainmenuFilePathTextBox.ReadOnly = true;
            this.halo2MainmenuFilePathTextBox.Size = new System.Drawing.Size(168, 20);
            this.halo2MainmenuFilePathTextBox.TabIndex = 4;
            // 
            // halo2SharedFilePathTextBox
            // 
            this.halo2SharedFilePathTextBox.Location = new System.Drawing.Point(109, 37);
            this.halo2SharedFilePathTextBox.Name = "halo2SharedFilePathTextBox";
            this.halo2SharedFilePathTextBox.ReadOnly = true;
            this.halo2SharedFilePathTextBox.Size = new System.Drawing.Size(168, 20);
            this.halo2SharedFilePathTextBox.TabIndex = 2;
            // 
            // sharedBrowseButton
            // 
            this.sharedBrowseButton.Location = new System.Drawing.Point(283, 35);
            this.sharedBrowseButton.Name = "sharedBrowseButton";
            this.sharedBrowseButton.Size = new System.Drawing.Size(31, 23);
            this.sharedBrowseButton.TabIndex = 3;
            this.sharedBrowseButton.Text = "...";
            this.sharedBrowseButton.UseVisualStyleBackColor = true;
            this.sharedBrowseButton.Click += new System.EventHandler(this.sharedBrowseButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Halo 2 Shared:";
            // 
            // halo2SpSharedFilePathTextBox
            // 
            this.halo2SpSharedFilePathTextBox.Location = new System.Drawing.Point(109, 8);
            this.halo2SpSharedFilePathTextBox.Name = "halo2SpSharedFilePathTextBox";
            this.halo2SpSharedFilePathTextBox.ReadOnly = true;
            this.halo2SpSharedFilePathTextBox.Size = new System.Drawing.Size(168, 20);
            this.halo2SpSharedFilePathTextBox.TabIndex = 0;
            // 
            // spSharedBrowseButton
            // 
            this.spSharedBrowseButton.Location = new System.Drawing.Point(283, 6);
            this.spSharedBrowseButton.Name = "spSharedBrowseButton";
            this.spSharedBrowseButton.Size = new System.Drawing.Size(31, 23);
            this.spSharedBrowseButton.TabIndex = 1;
            this.spSharedBrowseButton.Text = "...";
            this.spSharedBrowseButton.UseVisualStyleBackColor = true;
            this.spSharedBrowseButton.Click += new System.EventHandler(this.spSharedBrowseButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Halo 2 SPShared:";
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
            this.abideSettingsTab.ResumeLayout(false);
            this.abideSettingsTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel dialogControlsPanel;
        private System.Windows.Forms.TabControl settingsTabControl;
        private System.Windows.Forms.TabPage abideSettingsTab;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button mainmenuBrowseButton;
        private System.Windows.Forms.TextBox halo2MainmenuFilePathTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button spSharedBrowseButton;
        private System.Windows.Forms.TextBox halo2SpSharedFilePathTextBox;
        private System.Windows.Forms.Button sharedBrowseButton;
        private System.Windows.Forms.TextBox halo2SharedFilePathTextBox;
    }
}