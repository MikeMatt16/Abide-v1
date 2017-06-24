namespace Abide.Forms
{
    partial class AddOnInstaller
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
            this.controlsPanel = new System.Windows.Forms.Panel();
            this.installButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.contentsPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.installLogRichTextBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.addOnsListBox = new System.Windows.Forms.CheckedListBox();
            this.addPackageButton = new System.Windows.Forms.Button();
            this.controlsPanel.SuspendLayout();
            this.contentsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // controlsPanel
            // 
            this.controlsPanel.Controls.Add(this.installButton);
            this.controlsPanel.Controls.Add(this.closeButton);
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.controlsPanel.Location = new System.Drawing.Point(0, 380);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(584, 38);
            this.controlsPanel.TabIndex = 0;
            // 
            // installButton
            // 
            this.installButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.installButton.Location = new System.Drawing.Point(497, 6);
            this.installButton.Name = "installButton";
            this.installButton.Size = new System.Drawing.Size(75, 23);
            this.installButton.TabIndex = 1;
            this.installButton.Text = "&Install";
            this.installButton.UseVisualStyleBackColor = true;
            this.installButton.Click += new System.EventHandler(this.installButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(12, 6);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 0;
            this.closeButton.Text = "&Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // contentsPanel
            // 
            this.contentsPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.contentsPanel.Controls.Add(this.addPackageButton);
            this.contentsPanel.Controls.Add(this.label2);
            this.contentsPanel.Controls.Add(this.installLogRichTextBox);
            this.contentsPanel.Controls.Add(this.label1);
            this.contentsPanel.Controls.Add(this.addOnsListBox);
            this.contentsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentsPanel.Location = new System.Drawing.Point(0, 0);
            this.contentsPanel.Name = "contentsPanel";
            this.contentsPanel.Size = new System.Drawing.Size(584, 380);
            this.contentsPanel.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 188);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Install Log:";
            // 
            // installLogRichTextBox
            // 
            this.installLogRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.installLogRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.installLogRichTextBox.Location = new System.Drawing.Point(12, 204);
            this.installLogRichTextBox.Name = "installLogRichTextBox";
            this.installLogRichTextBox.ReadOnly = true;
            this.installLogRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.installLogRichTextBox.Size = new System.Drawing.Size(560, 170);
            this.installLogRichTextBox.TabIndex = 2;
            this.installLogRichTextBox.Text = "";
            this.installLogRichTextBox.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "AddOns:";
            // 
            // addOnsListBox
            // 
            this.addOnsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addOnsListBox.FormattingEnabled = true;
            this.addOnsListBox.IntegralHeight = false;
            this.addOnsListBox.Location = new System.Drawing.Point(12, 25);
            this.addOnsListBox.Name = "addOnsListBox";
            this.addOnsListBox.Size = new System.Drawing.Size(560, 131);
            this.addOnsListBox.TabIndex = 0;
            // 
            // addPackageButton
            // 
            this.addPackageButton.Location = new System.Drawing.Point(12, 162);
            this.addPackageButton.Name = "addPackageButton";
            this.addPackageButton.Size = new System.Drawing.Size(92, 23);
            this.addPackageButton.TabIndex = 4;
            this.addPackageButton.Text = "&Add Package...";
            this.addPackageButton.UseVisualStyleBackColor = true;
            this.addPackageButton.Click += new System.EventHandler(this.addPackageButton_Click);
            // 
            // AddOnInstaller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 418);
            this.Controls.Add(this.contentsPanel);
            this.Controls.Add(this.controlsPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = global::Abide.Properties.Resources.Abide_AddOn_Package;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddOnInstaller";
            this.Text = "AddOn Installer";
            this.controlsPanel.ResumeLayout(false);
            this.contentsPanel.ResumeLayout(false);
            this.contentsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel controlsPanel;
        private System.Windows.Forms.Button installButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Panel contentsPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox addOnsListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox installLogRichTextBox;
        private System.Windows.Forms.Button addPackageButton;
    }
}