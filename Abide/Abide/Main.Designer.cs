namespace Abide
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.recentHalo2MapsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearRecentHalo2MapsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recentHalo2MapsToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.recentHalo2BetaMapsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearRecentHalo2BetaMapsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recentHalo2BetaMapsToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.fileToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.createAddOnPackageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addOnManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.registerFileTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileHorizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileVerticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cascadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xboxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quickConnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.versionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.windowToolStripMenuItem,
            this.xboxToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.MdiWindowListItem = this.windowToolStripMenuItem;
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(1384, 24);
            this.mainMenuStrip.TabIndex = 1;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.fileToolStripSeparator1,
            this.recentHalo2MapsToolStripMenuItem,
            this.recentHalo2BetaMapsToolStripMenuItem,
            this.fileToolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // fileToolStripSeparator1
            // 
            this.fileToolStripSeparator1.Name = "fileToolStripSeparator1";
            this.fileToolStripSeparator1.Size = new System.Drawing.Size(202, 6);
            // 
            // recentHalo2MapsToolStripMenuItem
            // 
            this.recentHalo2MapsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearRecentHalo2MapsToolStripMenuItem,
            this.recentHalo2MapsToolStripSeparator1});
            this.recentHalo2MapsToolStripMenuItem.Name = "recentHalo2MapsToolStripMenuItem";
            this.recentHalo2MapsToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.recentHalo2MapsToolStripMenuItem.Text = "Recent Halo &2 Maps";
            this.recentHalo2MapsToolStripMenuItem.Visible = false;
            // 
            // clearRecentHalo2MapsToolStripMenuItem
            // 
            this.clearRecentHalo2MapsToolStripMenuItem.Name = "clearRecentHalo2MapsToolStripMenuItem";
            this.clearRecentHalo2MapsToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.clearRecentHalo2MapsToolStripMenuItem.Text = "&Clear Recent Maps";
            this.clearRecentHalo2MapsToolStripMenuItem.Click += new System.EventHandler(this.clearRecentHalo2MapsToolStripMenuItem_Click);
            // 
            // recentHalo2MapsToolStripSeparator1
            // 
            this.recentHalo2MapsToolStripSeparator1.Name = "recentHalo2MapsToolStripSeparator1";
            this.recentHalo2MapsToolStripSeparator1.Size = new System.Drawing.Size(169, 6);
            // 
            // recentHalo2BetaMapsToolStripMenuItem
            // 
            this.recentHalo2BetaMapsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearRecentHalo2BetaMapsToolStripMenuItem,
            this.recentHalo2BetaMapsToolStripSeparator1});
            this.recentHalo2BetaMapsToolStripMenuItem.Name = "recentHalo2BetaMapsToolStripMenuItem";
            this.recentHalo2BetaMapsToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.recentHalo2BetaMapsToolStripMenuItem.Text = "Recent Halo 2 &Beta Maps";
            this.recentHalo2BetaMapsToolStripMenuItem.Visible = false;
            // 
            // clearRecentHalo2BetaMapsToolStripMenuItem
            // 
            this.clearRecentHalo2BetaMapsToolStripMenuItem.Name = "clearRecentHalo2BetaMapsToolStripMenuItem";
            this.clearRecentHalo2BetaMapsToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.clearRecentHalo2BetaMapsToolStripMenuItem.Text = "&Clear Recent Maps";
            this.clearRecentHalo2BetaMapsToolStripMenuItem.Click += new System.EventHandler(this.clearRecentHalo2BetaMapsToolStripMenuItem_Click);
            // 
            // recentHalo2BetaMapsToolStripSeparator1
            // 
            this.recentHalo2BetaMapsToolStripSeparator1.Name = "recentHalo2BetaMapsToolStripSeparator1";
            this.recentHalo2BetaMapsToolStripSeparator1.Size = new System.Drawing.Size(169, 6);
            // 
            // fileToolStripSeparator2
            // 
            this.fileToolStripSeparator2.Name = "fileToolStripSeparator2";
            this.fileToolStripSeparator2.Size = new System.Drawing.Size(202, 6);
            this.fileToolStripSeparator2.Visible = false;
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.toolStripSeparator2,
            this.createAddOnPackageToolStripMenuItem,
            this.addOnManagerToolStripMenuItem,
            this.toolStripSeparator1,
            this.registerFileTypesToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.optionsToolStripMenuItem.Text = "&Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(193, 6);
            // 
            // createAddOnPackageToolStripMenuItem
            // 
            this.createAddOnPackageToolStripMenuItem.Name = "createAddOnPackageToolStripMenuItem";
            this.createAddOnPackageToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.createAddOnPackageToolStripMenuItem.Text = "&Create AddOn Package";
            this.createAddOnPackageToolStripMenuItem.Click += new System.EventHandler(this.createAddOnPackageToolStripMenuItem_Click);
            // 
            // addOnManagerToolStripMenuItem
            // 
            this.addOnManagerToolStripMenuItem.Name = "addOnManagerToolStripMenuItem";
            this.addOnManagerToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.addOnManagerToolStripMenuItem.Text = "&AddOn Manager";
            this.addOnManagerToolStripMenuItem.Click += new System.EventHandler(this.addOnManagerToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(193, 6);
            // 
            // registerFileTypesToolStripMenuItem
            // 
            this.registerFileTypesToolStripMenuItem.Name = "registerFileTypesToolStripMenuItem";
            this.registerFileTypesToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.registerFileTypesToolStripMenuItem.Text = "&Register File Types";
            this.registerFileTypesToolStripMenuItem.Click += new System.EventHandler(this.registerFileTypesToolStripMenuItem_Click);
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tileHorizontalToolStripMenuItem,
            this.tileVerticalToolStripMenuItem,
            this.cascadeToolStripMenuItem});
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.windowToolStripMenuItem.Text = "&Window";
            // 
            // tileHorizontalToolStripMenuItem
            // 
            this.tileHorizontalToolStripMenuItem.Name = "tileHorizontalToolStripMenuItem";
            this.tileHorizontalToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.tileHorizontalToolStripMenuItem.Text = "Tile &Horizontal";
            // 
            // tileVerticalToolStripMenuItem
            // 
            this.tileVerticalToolStripMenuItem.Name = "tileVerticalToolStripMenuItem";
            this.tileVerticalToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.tileVerticalToolStripMenuItem.Text = "Tile &Vertical";
            // 
            // cascadeToolStripMenuItem
            // 
            this.cascadeToolStripMenuItem.Name = "cascadeToolStripMenuItem";
            this.cascadeToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.cascadeToolStripMenuItem.Text = "&Cascade";
            // 
            // xboxToolStripMenuItem
            // 
            this.xboxToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quickConnectToolStripMenuItem,
            this.connectToToolStripMenuItem,
            this.toolStripSeparator3});
            this.xboxToolStripMenuItem.Name = "xboxToolStripMenuItem";
            this.xboxToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.xboxToolStripMenuItem.Text = "&Xbox";
            // 
            // quickConnectToolStripMenuItem
            // 
            this.quickConnectToolStripMenuItem.Name = "quickConnectToolStripMenuItem";
            this.quickConnectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.quickConnectToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.quickConnectToolStripMenuItem.Text = "&Quick Connect";
            this.quickConnectToolStripMenuItem.Click += new System.EventHandler(this.quickConnectToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(193, 6);
            this.toolStripSeparator3.Visible = false;
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkForUpdatesToolStripMenuItem,
            this.versionToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.checkForUpdatesToolStripMenuItem.Text = "&Check For Updates";
            this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdatesToolStripMenuItem_Click);
            // 
            // versionToolStripMenuItem
            // 
            this.versionToolStripMenuItem.Enabled = false;
            this.versionToolStripMenuItem.Name = "versionToolStripMenuItem";
            this.versionToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.versionToolStripMenuItem.Text = "version";
            // 
            // connectToToolStripMenuItem
            // 
            this.connectToToolStripMenuItem.Name = "connectToToolStripMenuItem";
            this.connectToToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.connectToToolStripMenuItem.Text = "&Connect to...";
            // 
            // Main
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 811);
            this.Controls.Add(this.mainMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "Main";
            this.Text = "Abide Halo Map Editor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Main_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Main_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Main_DragEnter);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator fileToolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileHorizontalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileVerticalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cascadeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createAddOnPackageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addOnManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem recentHalo2MapsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearRecentHalo2MapsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator fileToolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator recentHalo2MapsToolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem registerFileTypesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem versionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xboxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quickConnectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem recentHalo2BetaMapsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearRecentHalo2BetaMapsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator recentHalo2BetaMapsToolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem connectToToolStripMenuItem;
    }
}

