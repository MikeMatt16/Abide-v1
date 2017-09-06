namespace Tag_Data_Editor
{
    partial class TagEditorSettings
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.h2PluginsPathBox = new System.Windows.Forms.TextBox();
            this.h2PluginsBrowseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Halo 2 Plugins:";
            // 
            // h2PluginsPathBox
            // 
            this.h2PluginsPathBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.h2PluginsPathBox.Location = new System.Drawing.Point(87, 5);
            this.h2PluginsPathBox.Name = "h2PluginsPathBox";
            this.h2PluginsPathBox.ReadOnly = true;
            this.h2PluginsPathBox.Size = new System.Drawing.Size(216, 20);
            this.h2PluginsPathBox.TabIndex = 1;
            // 
            // h2PluginsBrowseButton
            // 
            this.h2PluginsBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.h2PluginsBrowseButton.Location = new System.Drawing.Point(309, 3);
            this.h2PluginsBrowseButton.Name = "h2PluginsBrowseButton";
            this.h2PluginsBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.h2PluginsBrowseButton.TabIndex = 2;
            this.h2PluginsBrowseButton.Text = "...";
            this.h2PluginsBrowseButton.UseVisualStyleBackColor = true;
            this.h2PluginsBrowseButton.Click += new System.EventHandler(this.h2PluginsBrowseButton_Click);
            // 
            // TagEditorSettings
            // 
            this.Author = "Click16";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.h2PluginsBrowseButton);
            this.Controls.Add(this.h2PluginsPathBox);
            this.Controls.Add(this.label1);
            this.Description = "Tag Editor Settings";
            this.Name = "TagEditorSettings";
            this.SettingsName = "Tag Editor";
            this.Size = new System.Drawing.Size(387, 328);
            this.Load += new System.EventHandler(this.TagEditorSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox h2PluginsPathBox;
        private System.Windows.Forms.Button h2PluginsBrowseButton;
    }
}
