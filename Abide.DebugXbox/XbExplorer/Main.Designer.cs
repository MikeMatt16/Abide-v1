﻿namespace XbExplorer
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
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.homeToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.backToolStripMenuButton = new System.Windows.Forms.ToolStripButton();
            this.forwardToolStripMenuButton = new System.Windows.Forms.ToolStripButton();
            this.upToolStripMenuButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressToolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.rootMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.rebootXboxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.warmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.coldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.screenshotXboxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openXboxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteXboxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xboxMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openDriveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formatDriveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.bootToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debuggingDisabledToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debuggingEnabledToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.newFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.renameItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addressPanel = new System.Windows.Forms.Panel();
            this.locationTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.breadcrumbImageList = new System.Windows.Forms.ImageList(this.components);
            this.mainListView = new XbExplorer.ExplorerListView();
            this.nameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dateModifiedHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.typeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sizeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.freeSpaceHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.totalSizeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ipAddressHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.warmToTitleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuStrip.SuspendLayout();
            this.mainToolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.rootMenuStrip.SuspendLayout();
            this.xboxMenuStrip.SuspendLayout();
            this.folderMenuStrip.SuspendLayout();
            this.addressPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(784, 24);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "mainMenuStrip";
            this.mainMenuStrip.Visible = false;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mainToolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.homeToolStripButton,
            this.backToolStripMenuButton,
            this.forwardToolStripMenuButton,
            this.upToolStripMenuButton,
            this.toolStripSeparator1});
            this.mainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Padding = new System.Windows.Forms.Padding(4, 0, 1, 0);
            this.mainToolStrip.Size = new System.Drawing.Size(784, 39);
            this.mainToolStrip.TabIndex = 1;
            this.mainToolStrip.Text = "mainToolStrip";
            // 
            // homeToolStripButton
            // 
            this.homeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.homeToolStripButton.Image = global::XbExplorer.Properties.Resources.home;
            this.homeToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.homeToolStripButton.Name = "homeToolStripButton";
            this.homeToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.homeToolStripButton.Text = "&Home";
            this.homeToolStripButton.Click += new System.EventHandler(this.homeToolStripButton_Click);
            // 
            // backToolStripMenuButton
            // 
            this.backToolStripMenuButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.backToolStripMenuButton.Enabled = false;
            this.backToolStripMenuButton.Image = global::XbExplorer.Properties.Resources.arrow_left;
            this.backToolStripMenuButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.backToolStripMenuButton.Name = "backToolStripMenuButton";
            this.backToolStripMenuButton.Size = new System.Drawing.Size(36, 36);
            this.backToolStripMenuButton.Text = "Go &Back";
            this.backToolStripMenuButton.ToolTipText = "Back";
            this.backToolStripMenuButton.Click += new System.EventHandler(this.backToolStripMenuButton_Click);
            // 
            // forwardToolStripMenuButton
            // 
            this.forwardToolStripMenuButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.forwardToolStripMenuButton.Enabled = false;
            this.forwardToolStripMenuButton.Image = global::XbExplorer.Properties.Resources.arrow_right;
            this.forwardToolStripMenuButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.forwardToolStripMenuButton.Name = "forwardToolStripMenuButton";
            this.forwardToolStripMenuButton.Size = new System.Drawing.Size(36, 36);
            this.forwardToolStripMenuButton.Text = "Go &Forward";
            this.forwardToolStripMenuButton.ToolTipText = "Forward";
            this.forwardToolStripMenuButton.Click += new System.EventHandler(this.forwardToolStripMenuButton_Click);
            // 
            // upToolStripMenuButton
            // 
            this.upToolStripMenuButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.upToolStripMenuButton.Enabled = false;
            this.upToolStripMenuButton.Image = global::XbExplorer.Properties.Resources.arrow_up;
            this.upToolStripMenuButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.upToolStripMenuButton.Name = "upToolStripMenuButton";
            this.upToolStripMenuButton.Size = new System.Drawing.Size(36, 36);
            this.upToolStripMenuButton.Text = "Go &Up";
            this.upToolStripMenuButton.ToolTipText = "Up";
            this.upToolStripMenuButton.Click += new System.EventHandler(this.upToolStripMenuButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusToolStripStatusLabel,
            this.progressToolStripProgressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 439);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(784, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip";
            // 
            // statusToolStripStatusLabel
            // 
            this.statusToolStripStatusLabel.Name = "statusToolStripStatusLabel";
            this.statusToolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.statusToolStripStatusLabel.Text = "Ready";
            // 
            // progressToolStripProgressBar
            // 
            this.progressToolStripProgressBar.Name = "progressToolStripProgressBar";
            this.progressToolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            this.progressToolStripProgressBar.Visible = false;
            // 
            // rootMenuStrip
            // 
            this.rootMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rebootXboxToolStripMenuItem,
            this.screenshotXboxToolStripMenuItem,
            this.openXboxToolStripMenuItem,
            this.toolStripSeparator2,
            this.deleteXboxToolStripMenuItem});
            this.rootMenuStrip.Name = "xboxItemMenuStrip";
            this.rootMenuStrip.Size = new System.Drawing.Size(181, 120);
            // 
            // rebootXboxToolStripMenuItem
            // 
            this.rebootXboxToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.warmToolStripMenuItem,
            this.warmToTitleToolStripMenuItem,
            this.coldToolStripMenuItem});
            this.rebootXboxToolStripMenuItem.Enabled = false;
            this.rebootXboxToolStripMenuItem.Name = "rebootXboxToolStripMenuItem";
            this.rebootXboxToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.rebootXboxToolStripMenuItem.Text = "&Reboot";
            // 
            // warmToolStripMenuItem
            // 
            this.warmToolStripMenuItem.Name = "warmToolStripMenuItem";
            this.warmToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.W)));
            this.warmToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.warmToolStripMenuItem.Text = "&Warm";
            this.warmToolStripMenuItem.Click += new System.EventHandler(this.warmToolStripMenuItem_Click);
            // 
            // coldToolStripMenuItem
            // 
            this.coldToolStripMenuItem.Name = "coldToolStripMenuItem";
            this.coldToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.R)));
            this.coldToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.coldToolStripMenuItem.Text = "&Cold";
            this.coldToolStripMenuItem.Click += new System.EventHandler(this.coldToolStripMenuItem_Click);
            // 
            // screenshotXboxToolStripMenuItem
            // 
            this.screenshotXboxToolStripMenuItem.Enabled = false;
            this.screenshotXboxToolStripMenuItem.Name = "screenshotXboxToolStripMenuItem";
            this.screenshotXboxToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.screenshotXboxToolStripMenuItem.Text = "&Screenshot";
            this.screenshotXboxToolStripMenuItem.Click += new System.EventHandler(this.screenshotXboxToolStripMenuItem_Click);
            // 
            // openXboxToolStripMenuItem
            // 
            this.openXboxToolStripMenuItem.Enabled = false;
            this.openXboxToolStripMenuItem.Name = "openXboxToolStripMenuItem";
            this.openXboxToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openXboxToolStripMenuItem.Text = "&Open";
            this.openXboxToolStripMenuItem.Click += new System.EventHandler(this.openXboxToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // deleteXboxToolStripMenuItem
            // 
            this.deleteXboxToolStripMenuItem.Enabled = false;
            this.deleteXboxToolStripMenuItem.Name = "deleteXboxToolStripMenuItem";
            this.deleteXboxToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteXboxToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.deleteXboxToolStripMenuItem.Text = "D&elete";
            this.deleteXboxToolStripMenuItem.Click += new System.EventHandler(this.deleteXboxToolStripMenuItem_Click);
            // 
            // xboxMenuStrip
            // 
            this.xboxMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openDriveToolStripMenuItem,
            this.formatDriveToolStripMenuItem});
            this.xboxMenuStrip.Name = "xboxMenuStrip";
            this.xboxMenuStrip.Size = new System.Drawing.Size(122, 48);
            // 
            // openDriveToolStripMenuItem
            // 
            this.openDriveToolStripMenuItem.Enabled = false;
            this.openDriveToolStripMenuItem.Name = "openDriveToolStripMenuItem";
            this.openDriveToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.openDriveToolStripMenuItem.Text = "&Open";
            this.openDriveToolStripMenuItem.Click += new System.EventHandler(this.openDriveToolStripMenuItem_Click);
            // 
            // formatDriveToolStripMenuItem
            // 
            this.formatDriveToolStripMenuItem.Enabled = false;
            this.formatDriveToolStripMenuItem.Name = "formatDriveToolStripMenuItem";
            this.formatDriveToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.formatDriveToolStripMenuItem.Text = "Format...";
            this.formatDriveToolStripMenuItem.Click += new System.EventHandler(this.formatDriveToolStripMenuItem_Click);
            // 
            // folderMenuStrip
            // 
            this.folderMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bootToolStripMenuItem,
            this.openItemToolStripMenuItem,
            this.copyItemToolStripMenuItem,
            this.pasteItemToolStripMenuItem,
            this.toolStripSeparator3,
            this.newFolderToolStripMenuItem,
            this.toolStripSeparator4,
            this.renameItemToolStripMenuItem,
            this.deleteItemToolStripMenuItem});
            this.folderMenuStrip.Name = "folderMenuStrip";
            this.folderMenuStrip.Size = new System.Drawing.Size(210, 170);
            // 
            // bootToolStripMenuItem
            // 
            this.bootToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.debuggingDisabledToolStripMenuItem,
            this.debuggingEnabledToolStripMenuItem});
            this.bootToolStripMenuItem.Name = "bootToolStripMenuItem";
            this.bootToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.bootToolStripMenuItem.Text = "&Boot With...";
            this.bootToolStripMenuItem.Visible = false;
            // 
            // debuggingDisabledToolStripMenuItem
            // 
            this.debuggingDisabledToolStripMenuItem.Name = "debuggingDisabledToolStripMenuItem";
            this.debuggingDisabledToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.debuggingDisabledToolStripMenuItem.Text = "Debugging &Disabled";
            this.debuggingDisabledToolStripMenuItem.Click += new System.EventHandler(this.debuggingDisabledToolStripMenuItem_Click);
            // 
            // debuggingEnabledToolStripMenuItem
            // 
            this.debuggingEnabledToolStripMenuItem.Name = "debuggingEnabledToolStripMenuItem";
            this.debuggingEnabledToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.debuggingEnabledToolStripMenuItem.Text = "Debugging &Enabled";
            this.debuggingEnabledToolStripMenuItem.Click += new System.EventHandler(this.debuggingEnabledToolStripMenuItem_Click);
            // 
            // openItemToolStripMenuItem
            // 
            this.openItemToolStripMenuItem.Enabled = false;
            this.openItemToolStripMenuItem.Name = "openItemToolStripMenuItem";
            this.openItemToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.openItemToolStripMenuItem.Text = "&Open";
            this.openItemToolStripMenuItem.Click += new System.EventHandler(this.openItemToolStripMenuItem_Click);
            // 
            // copyItemToolStripMenuItem
            // 
            this.copyItemToolStripMenuItem.Enabled = false;
            this.copyItemToolStripMenuItem.Name = "copyItemToolStripMenuItem";
            this.copyItemToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyItemToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.copyItemToolStripMenuItem.Text = "&Copy";
            this.copyItemToolStripMenuItem.Click += new System.EventHandler(this.copyItemToolStripMenuItem_Click);
            // 
            // pasteItemToolStripMenuItem
            // 
            this.pasteItemToolStripMenuItem.Name = "pasteItemToolStripMenuItem";
            this.pasteItemToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.pasteItemToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.pasteItemToolStripMenuItem.Text = "&Paste";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(206, 6);
            // 
            // newFolderToolStripMenuItem
            // 
            this.newFolderToolStripMenuItem.Name = "newFolderToolStripMenuItem";
            this.newFolderToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.N)));
            this.newFolderToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.newFolderToolStripMenuItem.Text = "New &Folder";
            this.newFolderToolStripMenuItem.Click += new System.EventHandler(this.newFolderToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(206, 6);
            // 
            // renameItemToolStripMenuItem
            // 
            this.renameItemToolStripMenuItem.Enabled = false;
            this.renameItemToolStripMenuItem.Name = "renameItemToolStripMenuItem";
            this.renameItemToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.renameItemToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.renameItemToolStripMenuItem.Text = "Rena&me";
            this.renameItemToolStripMenuItem.Click += new System.EventHandler(this.renameItemToolStripMenuItem_Click);
            // 
            // deleteItemToolStripMenuItem
            // 
            this.deleteItemToolStripMenuItem.Enabled = false;
            this.deleteItemToolStripMenuItem.Name = "deleteItemToolStripMenuItem";
            this.deleteItemToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteItemToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.deleteItemToolStripMenuItem.Text = "&Delete";
            this.deleteItemToolStripMenuItem.Click += new System.EventHandler(this.deleteItemToolStripMenuItem_Click);
            // 
            // addressPanel
            // 
            this.addressPanel.Controls.Add(this.locationTextBox);
            this.addressPanel.Controls.Add(this.label1);
            this.addressPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.addressPanel.Location = new System.Drawing.Point(0, 39);
            this.addressPanel.Name = "addressPanel";
            this.addressPanel.Size = new System.Drawing.Size(784, 26);
            this.addressPanel.TabIndex = 4;
            // 
            // locationTextBox
            // 
            this.locationTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.locationTextBox.Location = new System.Drawing.Point(54, 3);
            this.locationTextBox.Name = "locationTextBox";
            this.locationTextBox.Size = new System.Drawing.Size(727, 20);
            this.locationTextBox.TabIndex = 1;
            this.locationTextBox.Text = "XbExplorer";
            this.locationTextBox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.locationTextBox_PreviewKeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Address";
            // 
            // breadcrumbImageList
            // 
            this.breadcrumbImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.breadcrumbImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.breadcrumbImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // mainListView
            // 
            this.mainListView.AllowDrop = true;
            this.mainListView.AutoArrange = false;
            this.mainListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameHeader,
            this.dateModifiedHeader,
            this.typeHeader,
            this.sizeHeader,
            this.freeSpaceHeader,
            this.totalSizeHeader,
            this.ipAddressHeader});
            this.mainListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainListView.FullRowSelect = true;
            this.mainListView.Location = new System.Drawing.Point(0, 65);
            this.mainListView.Name = "mainListView";
            this.mainListView.Size = new System.Drawing.Size(784, 374);
            this.mainListView.TabIndex = 3;
            this.mainListView.TileSize = new System.Drawing.Size(250, 56);
            this.mainListView.UseCompatibleStateImageBehavior = false;
            this.mainListView.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.mainListView_AfterLabelEdit);
            this.mainListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.mainListView_ColumnClick);
            this.mainListView.ItemActivate += new System.EventHandler(this.mainListView_ItemActivate);
            this.mainListView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.mainListView_ItemDrag);
            this.mainListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.mainListView_ItemSelectionChanged);
            this.mainListView.DragDrop += new System.Windows.Forms.DragEventHandler(this.mainListView_DragDrop);
            this.mainListView.DragEnter += new System.Windows.Forms.DragEventHandler(this.mainListView_DragEnter);
            // 
            // nameHeader
            // 
            this.nameHeader.Text = "Name";
            this.nameHeader.Width = 260;
            // 
            // dateModifiedHeader
            // 
            this.dateModifiedHeader.Text = "Date modified";
            this.dateModifiedHeader.Width = 130;
            // 
            // typeHeader
            // 
            this.typeHeader.Text = "Type";
            this.typeHeader.Width = 130;
            // 
            // sizeHeader
            // 
            this.sizeHeader.Text = "Size";
            this.sizeHeader.Width = 80;
            // 
            // freeSpaceHeader
            // 
            this.freeSpaceHeader.Text = "Free Space";
            this.freeSpaceHeader.Width = 90;
            // 
            // totalSizeHeader
            // 
            this.totalSizeHeader.Text = "Total Capacity";
            this.totalSizeHeader.Width = 90;
            // 
            // ipAddressHeader
            // 
            this.ipAddressHeader.Text = "IP Address";
            this.ipAddressHeader.Width = 100;
            // 
            // warmToTitleToolStripMenuItem
            // 
            this.warmToTitleToolStripMenuItem.Name = "warmToTitleToolStripMenuItem";
            this.warmToTitleToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.W)));
            this.warmToTitleToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.warmToTitleToolStripMenuItem.Text = "Warm to Active &Title";
            this.warmToTitleToolStripMenuItem.Click += new System.EventHandler(this.warmToTitleToolStripMenuItem_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.mainListView);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.addressPanel);
            this.Controls.Add(this.mainToolStrip);
            this.Controls.Add(this.mainMenuStrip);
            this.Icon = global::XbExplorer.Properties.Resources.XbExplorer;
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "Main";
            this.Text = "XbExplorer";
            this.Load += new System.EventHandler(this.Main_Load);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.rootMenuStrip.ResumeLayout(false);
            this.xboxMenuStrip.ResumeLayout(false);
            this.folderMenuStrip.ResumeLayout(false);
            this.addressPanel.ResumeLayout(false);
            this.addressPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStrip mainToolStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private XbExplorer.ExplorerListView mainListView;
        private System.Windows.Forms.ColumnHeader nameHeader;
        private System.Windows.Forms.ColumnHeader dateModifiedHeader;
        private System.Windows.Forms.ColumnHeader sizeHeader;
        private System.Windows.Forms.ToolStripButton homeToolStripButton;
        private System.Windows.Forms.ColumnHeader typeHeader;
        private System.Windows.Forms.ToolStripButton backToolStripMenuButton;
        private System.Windows.Forms.ToolStripButton forwardToolStripMenuButton;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip rootMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem screenshotXboxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rebootXboxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem warmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem coldToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem deleteXboxToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader freeSpaceHeader;
        private System.Windows.Forms.ColumnHeader totalSizeHeader;
        private System.Windows.Forms.ContextMenuStrip xboxMenuStrip;
        private System.Windows.Forms.ContextMenuStrip folderMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem openXboxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDriveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formatDriveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem renameItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ColumnHeader ipAddressHeader;
        private System.Windows.Forms.Panel addressPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox locationTextBox;
        private System.Windows.Forms.ToolStripButton upToolStripMenuButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripStatusLabel statusToolStripStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar progressToolStripProgressBar;
        private System.Windows.Forms.ImageList breadcrumbImageList;
        private System.Windows.Forms.ToolStripMenuItem bootToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debuggingDisabledToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debuggingEnabledToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem warmToTitleToolStripMenuItem;
    }
}
