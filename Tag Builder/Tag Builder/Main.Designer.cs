namespace Abide.TagBuilder
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.tagViewPane = new System.Windows.Forms.Panel();
            this.tagTreeView = new System.Windows.Forms.TreeView();
            this.tagTreeImageList = new System.Windows.Forms.ImageList(this.components);
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.generateentPluginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tagViewPane.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tagViewPane
            // 
            this.tagViewPane.Controls.Add(this.tagTreeView);
            this.tagViewPane.Dock = System.Windows.Forms.DockStyle.Left;
            this.tagViewPane.Location = new System.Drawing.Point(0, 24);
            this.tagViewPane.Name = "tagViewPane";
            this.tagViewPane.Size = new System.Drawing.Size(300, 737);
            this.tagViewPane.TabIndex = 0;
            // 
            // tagTreeView
            // 
            this.tagTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tagTreeView.ImageIndex = 0;
            this.tagTreeView.ImageList = this.tagTreeImageList;
            this.tagTreeView.Location = new System.Drawing.Point(0, 0);
            this.tagTreeView.Name = "tagTreeView";
            this.tagTreeView.SelectedImageIndex = 0;
            this.tagTreeView.Size = new System.Drawing.Size(300, 737);
            this.tagTreeView.TabIndex = 0;
            this.tagTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tagTreeView_AfterSelect);
            this.tagTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tagTreeView_NodeMouseDoubleClick);
            // 
            // tagTreeImageList
            // 
            this.tagTreeImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("tagTreeImageList.ImageStream")));
            this.tagTreeImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.tagTreeImageList.Images.SetKeyName(0, "folder-16.png");
            this.tagTreeImageList.Images.SetKeyName(1, "tag-16.png");
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.generateentPluginsToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(1264, 24);
            this.mainMenuStrip.TabIndex = 1;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
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
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(143, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(300, 24);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 737);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // generateentPluginsToolStripMenuItem
            // 
            this.generateentPluginsToolStripMenuItem.Name = "generateentPluginsToolStripMenuItem";
            this.generateentPluginsToolStripMenuItem.Size = new System.Drawing.Size(136, 20);
            this.generateentPluginsToolStripMenuItem.Text = "&Generate *.ent Plugins";
            this.generateentPluginsToolStripMenuItem.Click += new System.EventHandler(this.generateentPluginsToolStripMenuItem_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 761);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.tagViewPane);
            this.Controls.Add(this.mainMenuStrip);
            this.Icon = global::Abide.TagBuilder.Properties.Resources.chunk_cloner_icon;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "Main";
            this.Text = "Tag Builder";
            this.tagViewPane.ResumeLayout(false);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel tagViewPane;
        private System.Windows.Forms.TreeView tagTreeView;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ImageList tagTreeImageList;
        private System.Windows.Forms.ToolStripMenuItem generateentPluginsToolStripMenuItem;
    }
}