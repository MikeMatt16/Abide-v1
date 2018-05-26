namespace Tag_Data_Editor.Halo2
{
    partial class TagEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TagEditor));
            this.tagDataSplitter = new System.Windows.Forms.SplitContainer();
            this.structureView = new System.Windows.Forms.TreeView();
            this.tagEditorToolStrip = new System.Windows.Forms.ToolStrip();
            this.pokeToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.tagBlockIndexToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.xboxConnectionToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.tagDataWebBrowser = new Tag_Data_Editor.TagEditorWebBrowser();
            ((System.ComponentModel.ISupportInitialize)(this.tagDataSplitter)).BeginInit();
            this.tagDataSplitter.Panel1.SuspendLayout();
            this.tagDataSplitter.Panel2.SuspendLayout();
            this.tagDataSplitter.SuspendLayout();
            this.tagEditorToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tagDataSplitter
            // 
            this.tagDataSplitter.BackColor = System.Drawing.SystemColors.Control;
            this.tagDataSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tagDataSplitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.tagDataSplitter.Location = new System.Drawing.Point(0, 0);
            this.tagDataSplitter.Name = "tagDataSplitter";
            this.tagDataSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // tagDataSplitter.Panel1
            // 
            this.tagDataSplitter.Panel1.Controls.Add(this.structureView);
            this.tagDataSplitter.Panel1.Controls.Add(this.tagEditorToolStrip);
            // 
            // tagDataSplitter.Panel2
            // 
            this.tagDataSplitter.Panel2.Controls.Add(this.tagDataWebBrowser);
            this.tagDataSplitter.Size = new System.Drawing.Size(1502, 916);
            this.tagDataSplitter.SplitterDistance = 200;
            this.tagDataSplitter.TabIndex = 1;
            this.tagDataSplitter.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.tagDataSplitter_SplitterMoved);
            // 
            // structureView
            // 
            this.structureView.BackColor = System.Drawing.SystemColors.ControlDark;
            this.structureView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.structureView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.structureView.HideSelection = false;
            this.structureView.Location = new System.Drawing.Point(0, 0);
            this.structureView.Name = "structureView";
            this.structureView.Size = new System.Drawing.Size(1502, 175);
            this.structureView.TabIndex = 1;
            this.structureView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.structureView_AfterSelect);
            // 
            // tagEditorToolStrip
            // 
            this.tagEditorToolStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tagEditorToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tagEditorToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pokeToolStripButton,
            this.tagBlockIndexToolStripComboBox,
            this.xboxConnectionToolStripLabel});
            this.tagEditorToolStrip.Location = new System.Drawing.Point(0, 175);
            this.tagEditorToolStrip.Name = "tagEditorToolStrip";
            this.tagEditorToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.tagEditorToolStrip.Size = new System.Drawing.Size(1502, 25);
            this.tagEditorToolStrip.TabIndex = 0;
            this.tagEditorToolStrip.Text = "toolStrip1";
            // 
            // pokeToolStripButton
            // 
            this.pokeToolStripButton.Enabled = false;
            this.pokeToolStripButton.Image = global::Tag_Data_Editor.Properties.Resources.Poke;
            this.pokeToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pokeToolStripButton.Name = "pokeToolStripButton";
            this.pokeToolStripButton.Size = new System.Drawing.Size(102, 22);
            this.pokeToolStripButton.Text = "&Poke Changes";
            this.pokeToolStripButton.Click += new System.EventHandler(this.pokeToolStripButton_Click);
            // 
            // tagBlockIndexToolStripComboBox
            // 
            this.tagBlockIndexToolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tagBlockIndexToolStripComboBox.DropDownWidth = 200;
            this.tagBlockIndexToolStripComboBox.Name = "tagBlockIndexToolStripComboBox";
            this.tagBlockIndexToolStripComboBox.Size = new System.Drawing.Size(200, 25);
            this.tagBlockIndexToolStripComboBox.Visible = false;
            this.tagBlockIndexToolStripComboBox.SelectedIndexChanged += new System.EventHandler(this.tagBlockIndexToolStripComboBox_SelectedIndexChanged);
            // 
            // xboxConnectionToolStripLabel
            // 
            this.xboxConnectionToolStripLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.xboxConnectionToolStripLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.xboxConnectionToolStripLabel.Image = ((System.Drawing.Image)(resources.GetObject("xboxConnectionToolStripLabel.Image")));
            this.xboxConnectionToolStripLabel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xboxConnectionToolStripLabel.Name = "xboxConnectionToolStripLabel";
            this.xboxConnectionToolStripLabel.Size = new System.Drawing.Size(88, 22);
            this.xboxConnectionToolStripLabel.Text = "Not Connected";
            // 
            // tagDataWebBrowser
            // 
            this.tagDataWebBrowser.BitmaskSetCallback = null;
            this.tagDataWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tagDataWebBrowser.EnumSetCallback = null;
            this.tagDataWebBrowser.Location = new System.Drawing.Point(0, 0);
            this.tagDataWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.tagDataWebBrowser.Name = "tagDataWebBrowser";
            this.tagDataWebBrowser.Size = new System.Drawing.Size(1502, 712);
            this.tagDataWebBrowser.StringIdButtonClickCallback = null;
            this.tagDataWebBrowser.StringSetCallback = null;
            this.tagDataWebBrowser.TabIndex = 0;
            this.tagDataWebBrowser.TagButtonClickCallback = null;
            this.tagDataWebBrowser.UnicodeSetCallback = null;
            this.tagDataWebBrowser.Url = new System.Uri("http://zaidware.com/michael.mattera/PotentialSoftware/Abide2/TagEditor/Index.html" +
        "", System.UriKind.Absolute);
            this.tagDataWebBrowser.ValueSetCallback = null;
            // 
            // TagEditor
            // 
            this.Author = "Click16";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tagDataSplitter);
            this.Description = "GUI based tag editor.";
            this.Icon = global::Tag_Data_Editor.Properties.Resources.Meta_Editor;
            this.Name = "TagEditor";
            this.Size = new System.Drawing.Size(1502, 916);
            this.ToolName = "Tag Editor";
            this.Initialize += new System.EventHandler<Abide.AddOnApi.AddOnHostEventArgs>(this.TagEditor_Initialize);
            this.SelectedEntryChanged += new System.EventHandler(this.TagEditor_SelectedEntryChanged);
            this.XboxChanged += new System.EventHandler(this.TagEditor_XboxChanged);
            this.tagDataSplitter.Panel1.ResumeLayout(false);
            this.tagDataSplitter.Panel1.PerformLayout();
            this.tagDataSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tagDataSplitter)).EndInit();
            this.tagDataSplitter.ResumeLayout(false);
            this.tagEditorToolStrip.ResumeLayout(false);
            this.tagEditorToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer tagDataSplitter;
        private System.Windows.Forms.TreeView structureView;
        private System.Windows.Forms.ToolStrip tagEditorToolStrip;
        private Tag_Data_Editor.TagEditorWebBrowser tagDataWebBrowser;
        private System.Windows.Forms.ToolStripComboBox tagBlockIndexToolStripComboBox;
        private System.Windows.Forms.ToolStripButton pokeToolStripButton;
        private System.Windows.Forms.ToolStripLabel xboxConnectionToolStripLabel;
    }
}
