namespace Abide.Tag.Ui
{
    partial class MapForm
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
            this.tagsPanel = new System.Windows.Forms.Panel();
            this.tagsTreeView = new System.Windows.Forms.TreeView();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dumpTagsButton = new System.Windows.Forms.Button();
            this.tagsPanel.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tagsPanel
            // 
            this.tagsPanel.Controls.Add(this.tagsTreeView);
            this.tagsPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.tagsPanel.Location = new System.Drawing.Point(0, 24);
            this.tagsPanel.Name = "tagsPanel";
            this.tagsPanel.Size = new System.Drawing.Size(260, 537);
            this.tagsPanel.TabIndex = 0;
            // 
            // tagsTreeView
            // 
            this.tagsTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tagsTreeView.Location = new System.Drawing.Point(0, 0);
            this.tagsTreeView.Name = "tagsTreeView";
            this.tagsTreeView.Size = new System.Drawing.Size(260, 537);
            this.tagsTreeView.TabIndex = 0;
            this.tagsTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tagsTreeView_AfterSelect);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(260, 24);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 537);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(884, 24);
            this.mainMenuStrip.TabIndex = 2;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // dumpTagsButton
            // 
            this.dumpTagsButton.Enabled = false;
            this.dumpTagsButton.Location = new System.Drawing.Point(269, 27);
            this.dumpTagsButton.Name = "dumpTagsButton";
            this.dumpTagsButton.Size = new System.Drawing.Size(129, 23);
            this.dumpTagsButton.TabIndex = 3;
            this.dumpTagsButton.Text = "&Dump Tags...";
            this.dumpTagsButton.UseVisualStyleBackColor = true;
            this.dumpTagsButton.Click += new System.EventHandler(this.dumpTagsButton_Click);
            // 
            // MapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.dumpTagsButton);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.tagsPanel);
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MapForm";
            this.Text = "MapForm";
            this.tagsPanel.ResumeLayout(false);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel tagsPanel;
        private System.Windows.Forms.TreeView tagsTreeView;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.Button dumpTagsButton;
    }
}