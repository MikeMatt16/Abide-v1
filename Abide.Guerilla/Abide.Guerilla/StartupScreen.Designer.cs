namespace Abide.Guerilla
{
    partial class StartupScreen
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
            this.setupLabel = new System.Windows.Forms.Label();
            this.tagsDirectoryLabel = new System.Windows.Forms.Label();
            this.workspaceDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.selectWorkspacePathButton = new System.Windows.Forms.Button();
            this.sharedMapLabel = new System.Windows.Forms.Label();
            this.sharedFileNameTextBox = new System.Windows.Forms.TextBox();
            this.browseSharedMapButton = new System.Windows.Forms.Button();
            this.mainMenuMapLabel = new System.Windows.Forms.Label();
            this.mainmenuFileNameTextBox = new System.Windows.Forms.TextBox();
            this.browseMainmenuButton = new System.Windows.Forms.Button();
            this.singlePlayerSharedMapLabel = new System.Windows.Forms.Label();
            this.singlePlayerSharedTextBox = new System.Windows.Forms.TextBox();
            this.browseSinglePlayerSharedButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // setupLabel
            // 
            this.setupLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.setupLabel.Location = new System.Drawing.Point(0, 0);
            this.setupLabel.Name = "setupLabel";
            this.setupLabel.Size = new System.Drawing.Size(484, 27);
            this.setupLabel.TabIndex = 0;
            this.setupLabel.Text = "Before we begin, we need to set up your environment.";
            this.setupLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tagsDirectoryLabel
            // 
            this.tagsDirectoryLabel.AutoSize = true;
            this.tagsDirectoryLabel.Location = new System.Drawing.Point(12, 27);
            this.tagsDirectoryLabel.Name = "tagsDirectoryLabel";
            this.tagsDirectoryLabel.Size = new System.Drawing.Size(65, 13);
            this.tagsDirectoryLabel.TabIndex = 1;
            this.tagsDirectoryLabel.Text = "Workspace:";
            // 
            // workspaceDirectoryTextBox
            // 
            this.workspaceDirectoryTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.workspaceDirectoryTextBox.Location = new System.Drawing.Point(12, 43);
            this.workspaceDirectoryTextBox.Name = "workspaceDirectoryTextBox";
            this.workspaceDirectoryTextBox.ReadOnly = true;
            this.workspaceDirectoryTextBox.Size = new System.Drawing.Size(379, 20);
            this.workspaceDirectoryTextBox.TabIndex = 2;
            // 
            // selectWorkspacePathButton
            // 
            this.selectWorkspacePathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectWorkspacePathButton.Location = new System.Drawing.Point(397, 41);
            this.selectWorkspacePathButton.Name = "selectWorkspacePathButton";
            this.selectWorkspacePathButton.Size = new System.Drawing.Size(75, 23);
            this.selectWorkspacePathButton.TabIndex = 3;
            this.selectWorkspacePathButton.Text = "Browse";
            this.selectWorkspacePathButton.UseVisualStyleBackColor = true;
            this.selectWorkspacePathButton.Click += new System.EventHandler(this.selectWorkspacePathButton_Click);
            // 
            // sharedMapLabel
            // 
            this.sharedMapLabel.AutoSize = true;
            this.sharedMapLabel.Location = new System.Drawing.Point(12, 66);
            this.sharedMapLabel.Name = "sharedMapLabel";
            this.sharedMapLabel.Size = new System.Drawing.Size(69, 13);
            this.sharedMapLabel.TabIndex = 4;
            this.sharedMapLabel.Text = "Shared Path:";
            // 
            // sharedFileNameTextBox
            // 
            this.sharedFileNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sharedFileNameTextBox.Location = new System.Drawing.Point(12, 82);
            this.sharedFileNameTextBox.Name = "sharedFileNameTextBox";
            this.sharedFileNameTextBox.ReadOnly = true;
            this.sharedFileNameTextBox.Size = new System.Drawing.Size(379, 20);
            this.sharedFileNameTextBox.TabIndex = 5;
            // 
            // browseSharedMapButton
            // 
            this.browseSharedMapButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseSharedMapButton.Location = new System.Drawing.Point(397, 80);
            this.browseSharedMapButton.Name = "browseSharedMapButton";
            this.browseSharedMapButton.Size = new System.Drawing.Size(75, 23);
            this.browseSharedMapButton.TabIndex = 6;
            this.browseSharedMapButton.Text = "Browse";
            this.browseSharedMapButton.UseVisualStyleBackColor = true;
            this.browseSharedMapButton.Click += new System.EventHandler(this.browseSharedMapButton_Click);
            // 
            // mainMenuMapLabel
            // 
            this.mainMenuMapLabel.AutoSize = true;
            this.mainMenuMapLabel.Location = new System.Drawing.Point(12, 105);
            this.mainMenuMapLabel.Name = "mainMenuMapLabel";
            this.mainMenuMapLabel.Size = new System.Drawing.Size(84, 13);
            this.mainMenuMapLabel.TabIndex = 7;
            this.mainMenuMapLabel.Text = "Mainmenu Path:";
            // 
            // mainmenuFileNameTextBox
            // 
            this.mainmenuFileNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainmenuFileNameTextBox.Location = new System.Drawing.Point(12, 121);
            this.mainmenuFileNameTextBox.Name = "mainmenuFileNameTextBox";
            this.mainmenuFileNameTextBox.ReadOnly = true;
            this.mainmenuFileNameTextBox.Size = new System.Drawing.Size(379, 20);
            this.mainmenuFileNameTextBox.TabIndex = 8;
            // 
            // browseMainmenuButton
            // 
            this.browseMainmenuButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseMainmenuButton.Location = new System.Drawing.Point(397, 119);
            this.browseMainmenuButton.Name = "browseMainmenuButton";
            this.browseMainmenuButton.Size = new System.Drawing.Size(75, 23);
            this.browseMainmenuButton.TabIndex = 9;
            this.browseMainmenuButton.Text = "Browse";
            this.browseMainmenuButton.UseVisualStyleBackColor = true;
            this.browseMainmenuButton.Click += new System.EventHandler(this.browseMainmenuButton_Click);
            // 
            // singlePlayerSharedMapLabel
            // 
            this.singlePlayerSharedMapLabel.AutoSize = true;
            this.singlePlayerSharedMapLabel.Location = new System.Drawing.Point(12, 144);
            this.singlePlayerSharedMapLabel.Name = "singlePlayerSharedMapLabel";
            this.singlePlayerSharedMapLabel.Size = new System.Drawing.Size(133, 13);
            this.singlePlayerSharedMapLabel.TabIndex = 10;
            this.singlePlayerSharedMapLabel.Text = "Single Player Shared Path:";
            // 
            // singlePlayerSharedTextBox
            // 
            this.singlePlayerSharedTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.singlePlayerSharedTextBox.Location = new System.Drawing.Point(12, 160);
            this.singlePlayerSharedTextBox.Name = "singlePlayerSharedTextBox";
            this.singlePlayerSharedTextBox.ReadOnly = true;
            this.singlePlayerSharedTextBox.Size = new System.Drawing.Size(379, 20);
            this.singlePlayerSharedTextBox.TabIndex = 11;
            // 
            // browseSinglePlayerSharedButton
            // 
            this.browseSinglePlayerSharedButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseSinglePlayerSharedButton.Location = new System.Drawing.Point(397, 158);
            this.browseSinglePlayerSharedButton.Name = "browseSinglePlayerSharedButton";
            this.browseSinglePlayerSharedButton.Size = new System.Drawing.Size(75, 23);
            this.browseSinglePlayerSharedButton.TabIndex = 12;
            this.browseSinglePlayerSharedButton.Text = "Browse";
            this.browseSinglePlayerSharedButton.UseVisualStyleBackColor = true;
            this.browseSinglePlayerSharedButton.Click += new System.EventHandler(this.browseSinglePlayerSharedButton_Click);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(397, 226);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 13;
            this.okButton.Text = "&OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.closeButton.Location = new System.Drawing.Point(12, 226);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 14;
            this.closeButton.Text = "&Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // StartupScreen
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 261);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.browseSinglePlayerSharedButton);
            this.Controls.Add(this.singlePlayerSharedTextBox);
            this.Controls.Add(this.singlePlayerSharedMapLabel);
            this.Controls.Add(this.browseMainmenuButton);
            this.Controls.Add(this.mainmenuFileNameTextBox);
            this.Controls.Add(this.mainMenuMapLabel);
            this.Controls.Add(this.browseSharedMapButton);
            this.Controls.Add(this.sharedFileNameTextBox);
            this.Controls.Add(this.sharedMapLabel);
            this.Controls.Add(this.selectWorkspacePathButton);
            this.Controls.Add(this.workspaceDirectoryTextBox);
            this.Controls.Add(this.tagsDirectoryLabel);
            this.Controls.Add(this.setupLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::Abide.Guerilla.Properties.Resources.abide_icon;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StartupScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Abide Guerilla";
            this.Load += new System.EventHandler(this.StartupScreen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label setupLabel;
        private System.Windows.Forms.Label tagsDirectoryLabel;
        private System.Windows.Forms.TextBox workspaceDirectoryTextBox;
        private System.Windows.Forms.Button selectWorkspacePathButton;
        private System.Windows.Forms.Label sharedMapLabel;
        private System.Windows.Forms.TextBox sharedFileNameTextBox;
        private System.Windows.Forms.Button browseSharedMapButton;
        private System.Windows.Forms.Label mainMenuMapLabel;
        private System.Windows.Forms.TextBox mainmenuFileNameTextBox;
        private System.Windows.Forms.Button browseMainmenuButton;
        private System.Windows.Forms.Label singlePlayerSharedMapLabel;
        private System.Windows.Forms.TextBox singlePlayerSharedTextBox;
        private System.Windows.Forms.Button browseSinglePlayerSharedButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button closeButton;
    }
}