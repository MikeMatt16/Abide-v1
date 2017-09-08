namespace Tag_Data_Editor.Halo2
{
    partial class TagEditor2
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
            this.tagDataSplitter = new System.Windows.Forms.SplitContainer();
            this.structureView = new System.Windows.Forms.TreeView();
            this.tagEditorToolStrip = new System.Windows.Forms.ToolStrip();
            this.reflexiveToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.tagDataWebBrowser = new System.Windows.Forms.WebBrowser();
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
            this.tagDataSplitter.Size = new System.Drawing.Size(600, 500);
            this.tagDataSplitter.SplitterDistance = 200;
            this.tagDataSplitter.TabIndex = 1;
            // 
            // structureView
            // 
            this.structureView.BackColor = System.Drawing.SystemColors.ControlDark;
            this.structureView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.structureView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.structureView.HideSelection = false;
            this.structureView.Location = new System.Drawing.Point(0, 0);
            this.structureView.Name = "structureView";
            this.structureView.Size = new System.Drawing.Size(600, 175);
            this.structureView.TabIndex = 1;
            this.structureView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.structureView_AfterSelect);
            // 
            // tagEditorToolStrip
            // 
            this.tagEditorToolStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tagEditorToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tagEditorToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reflexiveToolStripComboBox});
            this.tagEditorToolStrip.Location = new System.Drawing.Point(0, 175);
            this.tagEditorToolStrip.Name = "tagEditorToolStrip";
            this.tagEditorToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.tagEditorToolStrip.Size = new System.Drawing.Size(600, 25);
            this.tagEditorToolStrip.TabIndex = 0;
            this.tagEditorToolStrip.Text = "toolStrip1";
            // 
            // reflexiveToolStripComboBox
            // 
            this.reflexiveToolStripComboBox.DropDownWidth = 200;
            this.reflexiveToolStripComboBox.Name = "reflexiveToolStripComboBox";
            this.reflexiveToolStripComboBox.Size = new System.Drawing.Size(200, 25);
            this.reflexiveToolStripComboBox.Visible = false;
            // 
            // tagDataWebBrowser
            // 
            this.tagDataWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tagDataWebBrowser.Location = new System.Drawing.Point(0, 0);
            this.tagDataWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.tagDataWebBrowser.Name = "tagDataWebBrowser";
            this.tagDataWebBrowser.Size = new System.Drawing.Size(600, 296);
            this.tagDataWebBrowser.TabIndex = 0;
            // 
            // TagEditor2
            // 
            this.Author = "Click16";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tagDataSplitter);
            this.Description = "Yet another tag editor.";
            this.Icon = global::Tag_Data_Editor.Properties.Resources.Meta_Editor;
            this.Name = "TagEditor2";
            this.Size = new System.Drawing.Size(600, 500);
            this.ToolName = "YATE";
            this.SelectedEntryChanged += new System.EventHandler(this.TagEditor2_SelectedEntryChanged);
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
        private System.Windows.Forms.WebBrowser tagDataWebBrowser;
        private System.Windows.Forms.ToolStripComboBox reflexiveToolStripComboBox;
    }
}
