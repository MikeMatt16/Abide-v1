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
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dumpTagsButton = new System.Windows.Forms.Button();
            this.dumpSelectedTagButton = new System.Windows.Forms.Button();
            this.sizeLabel = new System.Windows.Forms.Label();
            this.rebuildTagButton = new System.Windows.Forms.Button();
            this.tagControlsPanel = new System.Windows.Forms.Panel();
            this.dumpBuiltTagButton = new System.Windows.Forms.Button();
            this.overwriteButton = new System.Windows.Forms.Button();
            this.duplicateTagButton = new System.Windows.Forms.Button();
            this.rebuildMapButton = new System.Windows.Forms.Button();
            this.rebuildSizeLabel = new System.Windows.Forms.Label();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.tagStructureTreeView = new System.Windows.Forms.TreeView();
            this.visualizeButton = new System.Windows.Forms.Button();
            this.offsetLabel = new System.Windows.Forms.Label();
            this.rebuildWholeMap = new System.Windows.Forms.Button();
            this.tagsPanel.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.tagControlsPanel.SuspendLayout();
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
            this.mainMenuStrip.Size = new System.Drawing.Size(1064, 24);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
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
            // dumpTagsButton
            // 
            this.dumpTagsButton.Enabled = false;
            this.dumpTagsButton.Location = new System.Drawing.Point(6, 16);
            this.dumpTagsButton.Name = "dumpTagsButton";
            this.dumpTagsButton.Size = new System.Drawing.Size(129, 23);
            this.dumpTagsButton.TabIndex = 1;
            this.dumpTagsButton.Text = "&Dump Tags...";
            this.dumpTagsButton.UseVisualStyleBackColor = true;
            this.dumpTagsButton.Click += new System.EventHandler(this.dumpTagsButton_Click);
            // 
            // dumpSelectedTagButton
            // 
            this.dumpSelectedTagButton.Enabled = false;
            this.dumpSelectedTagButton.Location = new System.Drawing.Point(6, 74);
            this.dumpSelectedTagButton.Name = "dumpSelectedTagButton";
            this.dumpSelectedTagButton.Size = new System.Drawing.Size(129, 23);
            this.dumpSelectedTagButton.TabIndex = 4;
            this.dumpSelectedTagButton.Text = "...";
            this.dumpSelectedTagButton.UseVisualStyleBackColor = true;
            this.dumpSelectedTagButton.Click += new System.EventHandler(this.dumpSelectedTagButton_Click);
            // 
            // sizeLabel
            // 
            this.sizeLabel.AutoSize = true;
            this.sizeLabel.Location = new System.Drawing.Point(141, 79);
            this.sizeLabel.Name = "sizeLabel";
            this.sizeLabel.Size = new System.Drawing.Size(16, 13);
            this.sizeLabel.TabIndex = 5;
            this.sizeLabel.Text = "...";
            // 
            // rebuildTagButton
            // 
            this.rebuildTagButton.Enabled = false;
            this.rebuildTagButton.Location = new System.Drawing.Point(6, 45);
            this.rebuildTagButton.Name = "rebuildTagButton";
            this.rebuildTagButton.Size = new System.Drawing.Size(129, 23);
            this.rebuildTagButton.TabIndex = 2;
            this.rebuildTagButton.Text = "&Rebuild Tag";
            this.rebuildTagButton.UseVisualStyleBackColor = true;
            this.rebuildTagButton.Click += new System.EventHandler(this.rebuildTagButton_Click);
            // 
            // tagControlsPanel
            // 
            this.tagControlsPanel.Controls.Add(this.rebuildWholeMap);
            this.tagControlsPanel.Controls.Add(this.offsetLabel);
            this.tagControlsPanel.Controls.Add(this.visualizeButton);
            this.tagControlsPanel.Controls.Add(this.dumpBuiltTagButton);
            this.tagControlsPanel.Controls.Add(this.overwriteButton);
            this.tagControlsPanel.Controls.Add(this.duplicateTagButton);
            this.tagControlsPanel.Controls.Add(this.rebuildMapButton);
            this.tagControlsPanel.Controls.Add(this.dumpTagsButton);
            this.tagControlsPanel.Controls.Add(this.rebuildTagButton);
            this.tagControlsPanel.Controls.Add(this.dumpSelectedTagButton);
            this.tagControlsPanel.Controls.Add(this.rebuildSizeLabel);
            this.tagControlsPanel.Controls.Add(this.sizeLabel);
            this.tagControlsPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.tagControlsPanel.Location = new System.Drawing.Point(263, 24);
            this.tagControlsPanel.Name = "tagControlsPanel";
            this.tagControlsPanel.Size = new System.Drawing.Size(270, 537);
            this.tagControlsPanel.TabIndex = 0;
            // 
            // dumpBuiltTagButton
            // 
            this.dumpBuiltTagButton.Enabled = false;
            this.dumpBuiltTagButton.Location = new System.Drawing.Point(6, 103);
            this.dumpBuiltTagButton.Name = "dumpBuiltTagButton";
            this.dumpBuiltTagButton.Size = new System.Drawing.Size(129, 23);
            this.dumpBuiltTagButton.TabIndex = 6;
            this.dumpBuiltTagButton.Text = "...";
            this.dumpBuiltTagButton.UseVisualStyleBackColor = true;
            this.dumpBuiltTagButton.Click += new System.EventHandler(this.dumpBuiltTagButton_Click);
            // 
            // overwriteButton
            // 
            this.overwriteButton.Location = new System.Drawing.Point(6, 219);
            this.overwriteButton.Name = "overwriteButton";
            this.overwriteButton.Size = new System.Drawing.Size(129, 23);
            this.overwriteButton.TabIndex = 9;
            this.overwriteButton.Text = "&Overwrite";
            this.overwriteButton.UseVisualStyleBackColor = true;
            this.overwriteButton.Click += new System.EventHandler(this.overwriteButton_Click);
            // 
            // duplicateTagButton
            // 
            this.duplicateTagButton.Location = new System.Drawing.Point(6, 190);
            this.duplicateTagButton.Name = "duplicateTagButton";
            this.duplicateTagButton.Size = new System.Drawing.Size(129, 23);
            this.duplicateTagButton.TabIndex = 8;
            this.duplicateTagButton.Text = "&Duplicate";
            this.duplicateTagButton.UseVisualStyleBackColor = true;
            this.duplicateTagButton.Click += new System.EventHandler(this.duplicateTagButton_Click);
            // 
            // rebuildMapButton
            // 
            this.rebuildMapButton.Location = new System.Drawing.Point(6, 132);
            this.rebuildMapButton.Name = "rebuildMapButton";
            this.rebuildMapButton.Size = new System.Drawing.Size(129, 23);
            this.rebuildMapButton.TabIndex = 7;
            this.rebuildMapButton.Text = "Rebuild &Map...";
            this.rebuildMapButton.UseVisualStyleBackColor = true;
            this.rebuildMapButton.Click += new System.EventHandler(this.rebuildMapButton_Click);
            // 
            // rebuildSizeLabel
            // 
            this.rebuildSizeLabel.AutoSize = true;
            this.rebuildSizeLabel.Location = new System.Drawing.Point(141, 50);
            this.rebuildSizeLabel.Name = "rebuildSizeLabel";
            this.rebuildSizeLabel.Size = new System.Drawing.Size(16, 13);
            this.rebuildSizeLabel.TabIndex = 3;
            this.rebuildSizeLabel.Text = "...";
            // 
            // splitter2
            // 
            this.splitter2.Location = new System.Drawing.Point(533, 24);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 537);
            this.splitter2.TabIndex = 3;
            this.splitter2.TabStop = false;
            // 
            // tagStructureTreeView
            // 
            this.tagStructureTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tagStructureTreeView.Location = new System.Drawing.Point(536, 24);
            this.tagStructureTreeView.Name = "tagStructureTreeView";
            this.tagStructureTreeView.Size = new System.Drawing.Size(528, 537);
            this.tagStructureTreeView.TabIndex = 2;
            // 
            // visualizeButton
            // 
            this.visualizeButton.Enabled = false;
            this.visualizeButton.Location = new System.Drawing.Point(6, 248);
            this.visualizeButton.Name = "visualizeButton";
            this.visualizeButton.Size = new System.Drawing.Size(129, 23);
            this.visualizeButton.TabIndex = 10;
            this.visualizeButton.Text = "&Visualize...";
            this.visualizeButton.UseVisualStyleBackColor = true;
            this.visualizeButton.Click += new System.EventHandler(this.visualizeButton_Click);
            // 
            // offsetLabel
            // 
            this.offsetLabel.AutoSize = true;
            this.offsetLabel.Location = new System.Drawing.Point(6, 0);
            this.offsetLabel.Name = "offsetLabel";
            this.offsetLabel.Size = new System.Drawing.Size(16, 13);
            this.offsetLabel.TabIndex = 0;
            this.offsetLabel.Text = "...";
            // 
            // rebuildWholeMap
            // 
            this.rebuildWholeMap.Location = new System.Drawing.Point(6, 161);
            this.rebuildWholeMap.Name = "rebuildWholeMap";
            this.rebuildWholeMap.Size = new System.Drawing.Size(129, 23);
            this.rebuildWholeMap.TabIndex = 11;
            this.rebuildWholeMap.Text = "Rebuild &Whole Map...";
            this.rebuildWholeMap.UseVisualStyleBackColor = true;
            this.rebuildWholeMap.Click += new System.EventHandler(this.rebuildWholeMap_Click);
            // 
            // MapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 561);
            this.Controls.Add(this.tagStructureTreeView);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.tagControlsPanel);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.tagsPanel);
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MapForm";
            this.Text = "MapForm";
            this.tagsPanel.ResumeLayout(false);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.tagControlsPanel.ResumeLayout(false);
            this.tagControlsPanel.PerformLayout();
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
        private System.Windows.Forms.Button dumpSelectedTagButton;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.Label sizeLabel;
        private System.Windows.Forms.Button rebuildTagButton;
        private System.Windows.Forms.Panel tagControlsPanel;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.TreeView tagStructureTreeView;
        private System.Windows.Forms.Button rebuildMapButton;
        private System.Windows.Forms.Label rebuildSizeLabel;
        private System.Windows.Forms.Button duplicateTagButton;
        private System.Windows.Forms.Button overwriteButton;
        private System.Windows.Forms.Button dumpBuiltTagButton;
        private System.Windows.Forms.Button visualizeButton;
        private System.Windows.Forms.Label offsetLabel;
        private System.Windows.Forms.Button rebuildWholeMap;
    }
}