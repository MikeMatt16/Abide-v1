namespace Abide.Dialogs.Halo2
{
    partial class TagBrowserDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TagBrowserDialog));
            this.largeImageList = new System.Windows.Forms.ImageList(this.components);
            this.okButton = new System.Windows.Forms.Button();
            this.nullButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.TagList = new System.Windows.Forms.ListView();
            this.nameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.typeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lengthHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.idHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.viewMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.largeIconToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smallIconToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smallImageList = new System.Windows.Forms.ImageList(this.components);
            this.directoryBox = new System.Windows.Forms.TextBox();
            this.viewMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // largeImageList
            // 
            this.largeImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("largeImageList.ImageStream")));
            this.largeImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.largeImageList.Images.SetKeyName(0, "abide_folder_48.png");
            this.largeImageList.Images.SetKeyName(1, "abide_reference_48.png");
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(577, 466);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "&OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // nullButton
            // 
            this.nullButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nullButton.Location = new System.Drawing.Point(496, 466);
            this.nullButton.Name = "nullButton";
            this.nullButton.Size = new System.Drawing.Size(75, 23);
            this.nullButton.TabIndex = 2;
            this.nullButton.Text = "&Null";
            this.nullButton.UseVisualStyleBackColor = true;
            this.nullButton.Click += new System.EventHandler(this.nullButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(12, 466);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // TagList
            // 
            this.TagList.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.TagList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TagList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameHeader,
            this.typeHeader,
            this.lengthHeader,
            this.idHeader});
            this.TagList.ContextMenuStrip = this.viewMenu;
            this.TagList.FullRowSelect = true;
            this.TagList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.TagList.HideSelection = false;
            this.TagList.LargeImageList = this.largeImageList;
            this.TagList.Location = new System.Drawing.Point(12, 38);
            this.TagList.MultiSelect = false;
            this.TagList.Name = "TagList";
            this.TagList.Size = new System.Drawing.Size(640, 422);
            this.TagList.SmallImageList = this.smallImageList;
            this.TagList.TabIndex = 0;
            this.TagList.UseCompatibleStateImageBehavior = false;
            this.TagList.View = System.Windows.Forms.View.Tile;
            this.TagList.ItemActivate += new System.EventHandler(this.TagList_ItemActivate);
            this.TagList.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.TagList_ItemSelectionChanged);
            // 
            // nameHeader
            // 
            this.nameHeader.Text = "Name";
            this.nameHeader.Width = 360;
            // 
            // typeHeader
            // 
            this.typeHeader.Text = "Type";
            this.typeHeader.Width = 80;
            // 
            // lengthHeader
            // 
            this.lengthHeader.Text = "Length";
            this.lengthHeader.Width = 90;
            // 
            // idHeader
            // 
            this.idHeader.Text = "ID";
            this.idHeader.Width = 90;
            // 
            // viewMenu
            // 
            this.viewMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.largeIconToolStripMenuItem,
            this.smallIconToolStripMenuItem,
            this.tileToolStripMenuItem,
            this.listToolStripMenuItem,
            this.detailsToolStripMenuItem});
            this.viewMenu.Name = "viewMenu";
            this.viewMenu.Size = new System.Drawing.Size(130, 114);
            // 
            // largeIconToolStripMenuItem
            // 
            this.largeIconToolStripMenuItem.Name = "largeIconToolStripMenuItem";
            this.largeIconToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.largeIconToolStripMenuItem.Text = "&Large Icon";
            this.largeIconToolStripMenuItem.Click += new System.EventHandler(this.TagListViewToolStripMenuItem_Click);
            // 
            // smallIconToolStripMenuItem
            // 
            this.smallIconToolStripMenuItem.Name = "smallIconToolStripMenuItem";
            this.smallIconToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.smallIconToolStripMenuItem.Text = "&Small Icon";
            this.smallIconToolStripMenuItem.Click += new System.EventHandler(this.TagListViewToolStripMenuItem_Click);
            // 
            // tileToolStripMenuItem
            // 
            this.tileToolStripMenuItem.Name = "tileToolStripMenuItem";
            this.tileToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.tileToolStripMenuItem.Text = "&Tile";
            this.tileToolStripMenuItem.Click += new System.EventHandler(this.TagListViewToolStripMenuItem_Click);
            // 
            // listToolStripMenuItem
            // 
            this.listToolStripMenuItem.Name = "listToolStripMenuItem";
            this.listToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.listToolStripMenuItem.Text = "L&ist";
            this.listToolStripMenuItem.Click += new System.EventHandler(this.TagListViewToolStripMenuItem_Click);
            // 
            // detailsToolStripMenuItem
            // 
            this.detailsToolStripMenuItem.Name = "detailsToolStripMenuItem";
            this.detailsToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.detailsToolStripMenuItem.Text = "&Details";
            this.detailsToolStripMenuItem.Click += new System.EventHandler(this.TagListViewToolStripMenuItem_Click);
            // 
            // smallImageList
            // 
            this.smallImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("smallImageList.ImageStream")));
            this.smallImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.smallImageList.Images.SetKeyName(0, "abide_folder_16.png");
            this.smallImageList.Images.SetKeyName(1, "abide_reference_16.png");
            // 
            // directoryBox
            // 
            this.directoryBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.directoryBox.BackColor = System.Drawing.SystemColors.Control;
            this.directoryBox.Location = new System.Drawing.Point(12, 12);
            this.directoryBox.Name = "directoryBox";
            this.directoryBox.ReadOnly = true;
            this.directoryBox.Size = new System.Drawing.Size(640, 20);
            this.directoryBox.TabIndex = 4;
            // 
            // TagBrowserDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(664, 501);
            this.Controls.Add(this.directoryBox);
            this.Controls.Add(this.TagList);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.nullButton);
            this.Controls.Add(this.okButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TagBrowserDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Tag...";
            this.Load += new System.EventHandler(this.TagBrowserDialog_Load);
            this.SizeChanged += new System.EventHandler(this.TagBrowserDialog_SizeChanged);
            this.viewMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button nullButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ImageList largeImageList;
        private System.Windows.Forms.ListView TagList;
        private System.Windows.Forms.ImageList smallImageList;
        private System.Windows.Forms.ColumnHeader nameHeader;
        private System.Windows.Forms.ColumnHeader lengthHeader;
        private System.Windows.Forms.ColumnHeader idHeader;
        private System.Windows.Forms.ColumnHeader typeHeader;
        private System.Windows.Forms.TextBox directoryBox;
        private System.Windows.Forms.ContextMenuStrip viewMenu;
        private System.Windows.Forms.ToolStripMenuItem largeIconToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smallIconToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem detailsToolStripMenuItem;
    }
}