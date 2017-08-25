namespace Abide.Halo2
{
    partial class Editor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Editor));
            this.TagPanel = new System.Windows.Forms.Panel();
            this.TagSplitContainer = new System.Windows.Forms.SplitContainer();
            this.TagTree = new System.Windows.Forms.TreeView();
            this.tagContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveTagToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TagImageList = new System.Windows.Forms.ImageList(this.components);
            this.tagSearchBox = new Abide.Controls.CueTextBox();
            this.TagTabControl = new System.Windows.Forms.TabControl();
            this.TagPropertiesTabPage = new System.Windows.Forms.TabPage();
            this.TagPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.toolPanel = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.mapToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.optionsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.TagPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TagSplitContainer)).BeginInit();
            this.TagSplitContainer.Panel1.SuspendLayout();
            this.TagSplitContainer.Panel2.SuspendLayout();
            this.TagSplitContainer.SuspendLayout();
            this.tagContextMenu.SuspendLayout();
            this.TagTabControl.SuspendLayout();
            this.TagPropertiesTabPage.SuspendLayout();
            this.mapToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // TagPanel
            // 
            this.TagPanel.Controls.Add(this.TagSplitContainer);
            this.TagPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.TagPanel.Location = new System.Drawing.Point(0, 0);
            this.TagPanel.Name = "TagPanel";
            this.TagPanel.Size = new System.Drawing.Size(260, 681);
            this.TagPanel.TabIndex = 0;
            // 
            // TagSplitContainer
            // 
            this.TagSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TagSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.TagSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.TagSplitContainer.Name = "TagSplitContainer";
            this.TagSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // TagSplitContainer.Panel1
            // 
            this.TagSplitContainer.Panel1.Controls.Add(this.TagTree);
            this.TagSplitContainer.Panel1.Controls.Add(this.tagSearchBox);
            // 
            // TagSplitContainer.Panel2
            // 
            this.TagSplitContainer.Panel2.Controls.Add(this.TagTabControl);
            this.TagSplitContainer.Size = new System.Drawing.Size(260, 681);
            this.TagSplitContainer.SplitterDistance = 396;
            this.TagSplitContainer.TabIndex = 0;
            // 
            // TagTree
            // 
            this.TagTree.AllowDrop = true;
            this.TagTree.ContextMenuStrip = this.tagContextMenu;
            this.TagTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TagTree.FullRowSelect = true;
            this.TagTree.HideSelection = false;
            this.TagTree.ImageIndex = 0;
            this.TagTree.ImageList = this.TagImageList;
            this.TagTree.Location = new System.Drawing.Point(0, 20);
            this.TagTree.Name = "TagTree";
            this.TagTree.SelectedImageIndex = 0;
            this.TagTree.Size = new System.Drawing.Size(260, 376);
            this.TagTree.TabIndex = 0;
            this.TagTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TagTree_AfterSelect);
            this.TagTree.DragDrop += new System.Windows.Forms.DragEventHandler(this.TagTree_DragDrop);
            this.TagTree.DragEnter += new System.Windows.Forms.DragEventHandler(this.TagTree_DragEnter);
            // 
            // tagContextMenu
            // 
            this.tagContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveTagToolStripMenuItem});
            this.tagContextMenu.Name = "TagContextMenu";
            this.tagContextMenu.Size = new System.Drawing.Size(121, 26);
            // 
            // saveTagToolStripMenuItem
            // 
            this.saveTagToolStripMenuItem.Name = "saveTagToolStripMenuItem";
            this.saveTagToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.saveTagToolStripMenuItem.Text = "&Save Tag";
            this.saveTagToolStripMenuItem.Click += new System.EventHandler(this.saveTagToolStripMenuItem_Click);
            // 
            // TagImageList
            // 
            this.TagImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("TagImageList.ImageStream")));
            this.TagImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.TagImageList.Images.SetKeyName(0, "Folder16.png");
            this.TagImageList.Images.SetKeyName(1, "Reference16.png");
            // 
            // tagSearchBox
            // 
            this.tagSearchBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tagSearchBox.Cue = "Search Tags";
            this.tagSearchBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.tagSearchBox.Location = new System.Drawing.Point(0, 0);
            this.tagSearchBox.Name = "tagSearchBox";
            this.tagSearchBox.Size = new System.Drawing.Size(260, 20);
            this.tagSearchBox.TabIndex = 1;
            this.tagSearchBox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.tagSearchBox_PreviewKeyDown);
            // 
            // TagTabControl
            // 
            this.TagTabControl.Controls.Add(this.TagPropertiesTabPage);
            this.TagTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TagTabControl.Location = new System.Drawing.Point(0, 0);
            this.TagTabControl.Name = "TagTabControl";
            this.TagTabControl.SelectedIndex = 0;
            this.TagTabControl.Size = new System.Drawing.Size(260, 281);
            this.TagTabControl.TabIndex = 0;
            // 
            // TagPropertiesTabPage
            // 
            this.TagPropertiesTabPage.Controls.Add(this.TagPropertyGrid);
            this.TagPropertiesTabPage.Location = new System.Drawing.Point(4, 22);
            this.TagPropertiesTabPage.Name = "TagPropertiesTabPage";
            this.TagPropertiesTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.TagPropertiesTabPage.Size = new System.Drawing.Size(252, 255);
            this.TagPropertiesTabPage.TabIndex = 0;
            this.TagPropertiesTabPage.Text = "Properties";
            this.TagPropertiesTabPage.UseVisualStyleBackColor = true;
            // 
            // TagPropertyGrid
            // 
            this.TagPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TagPropertyGrid.HelpVisible = false;
            this.TagPropertyGrid.LineColor = System.Drawing.SystemColors.ControlDark;
            this.TagPropertyGrid.Location = new System.Drawing.Point(3, 3);
            this.TagPropertyGrid.Name = "TagPropertyGrid";
            this.TagPropertyGrid.Size = new System.Drawing.Size(246, 249);
            this.TagPropertyGrid.TabIndex = 0;
            this.TagPropertyGrid.ToolbarVisible = false;
            // 
            // toolPanel
            // 
            this.toolPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolPanel.Location = new System.Drawing.Point(263, 25);
            this.toolPanel.Name = "toolPanel";
            this.toolPanel.Size = new System.Drawing.Size(571, 656);
            this.toolPanel.TabIndex = 3;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(260, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 681);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            // 
            // mapToolStrip
            // 
            this.mapToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mapToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton,
            this.saveToolStripButton,
            this.openToolStripButton,
            this.optionsToolStripButton});
            this.mapToolStrip.Location = new System.Drawing.Point(263, 0);
            this.mapToolStrip.Name = "mapToolStrip";
            this.mapToolStrip.Size = new System.Drawing.Size(571, 25);
            this.mapToolStrip.TabIndex = 2;
            this.mapToolStrip.Text = "toolStrip1";
            // 
            // toolStripDropDownButton
            // 
            this.toolStripDropDownButton.Image = global::Abide.Properties.Resources.Tools;
            this.toolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton.Name = "toolStripDropDownButton";
            this.toolStripDropDownButton.Size = new System.Drawing.Size(64, 22);
            this.toolStripDropDownButton.Text = "&Tools";
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = global::Abide.Properties.Resources.Save;
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "&Save";
            this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripButton_Click);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = global::Abide.Properties.Resources.Folder16;
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openToolStripButton.Text = "&Open";
            this.openToolStripButton.Visible = false;
            this.openToolStripButton.Click += new System.EventHandler(this.openToolStripButton_Click);
            // 
            // optionsToolStripButton
            // 
            this.optionsToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.optionsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.optionsToolStripButton.Image = global::Abide.Properties.Resources.Cogwheel;
            this.optionsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.optionsToolStripButton.Name = "optionsToolStripButton";
            this.optionsToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.optionsToolStripButton.Text = "O&ptions";
            this.optionsToolStripButton.Visible = false;
            this.optionsToolStripButton.Click += new System.EventHandler(this.optionsToolStripButton_Click);
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 681);
            this.Controls.Add(this.toolPanel);
            this.Controls.Add(this.mapToolStrip);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.TagPanel);
            this.Icon = global::Abide.Properties.Resources.Halo_2_Map;
            this.Name = "Editor";
            this.Text = "Halo 2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Halo2Editor_FormClosing);
            this.TagPanel.ResumeLayout(false);
            this.TagSplitContainer.Panel1.ResumeLayout(false);
            this.TagSplitContainer.Panel1.PerformLayout();
            this.TagSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TagSplitContainer)).EndInit();
            this.TagSplitContainer.ResumeLayout(false);
            this.tagContextMenu.ResumeLayout(false);
            this.TagTabControl.ResumeLayout(false);
            this.TagPropertiesTabPage.ResumeLayout(false);
            this.mapToolStrip.ResumeLayout(false);
            this.mapToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel TagPanel;
        private System.Windows.Forms.SplitContainer TagSplitContainer;
        private System.Windows.Forms.TreeView TagTree;
        private System.Windows.Forms.TabControl TagTabControl;
        private System.Windows.Forms.TabPage TagPropertiesTabPage;
        private System.Windows.Forms.PropertyGrid TagPropertyGrid;
        private System.Windows.Forms.ImageList TagImageList;
        private System.Windows.Forms.ToolStrip mapToolStrip;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton;
        private System.Windows.Forms.Panel toolPanel;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ToolStripButton optionsToolStripButton;
        private System.Windows.Forms.ContextMenuStrip tagContextMenu;
        private System.Windows.Forms.ToolStripMenuItem saveTagToolStripMenuItem;
        private Controls.CueTextBox tagSearchBox;
    }
}