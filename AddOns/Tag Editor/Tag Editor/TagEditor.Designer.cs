namespace Tag_Editor
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
            this.editorSplitter = new System.Windows.Forms.SplitContainer();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.treeView1 = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.editorSplitter)).BeginInit();
            this.editorSplitter.Panel1.SuspendLayout();
            this.editorSplitter.Panel2.SuspendLayout();
            this.editorSplitter.SuspendLayout();
            this.SuspendLayout();
            // 
            // editorSplitter
            // 
            this.editorSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorSplitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.editorSplitter.Location = new System.Drawing.Point(0, 0);
            this.editorSplitter.Name = "editorSplitter";
            // 
            // editorSplitter.Panel1
            // 
            this.editorSplitter.Panel1.Controls.Add(this.webBrowser1);
            // 
            // editorSplitter.Panel2
            // 
            this.editorSplitter.Panel2.Controls.Add(this.treeView1);
            this.editorSplitter.Size = new System.Drawing.Size(550, 550);
            this.editorSplitter.SplitterDistance = 314;
            this.editorSplitter.TabIndex = 0;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(314, 550);
            this.webBrowser1.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(232, 550);
            this.treeView1.TabIndex = 0;
            // 
            // TagEditor
            // 
            this.Author = "Click16";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.editorSplitter);
            this.Description = "Provides a user interface to edit tag data.";
            this.Icon = global::Tag_Editor.Properties.Resources.Meta_Editor;
            this.Name = "TagEditor";
            this.Size = new System.Drawing.Size(550, 550);
            this.ToolName = "Tag Editor";
            this.Initialize += new System.EventHandler<Abide.AddOnApi.AddOnHostEventArgs>(this.TagEditor_Initialize);
            this.SelectedEntryChanged += new System.EventHandler(this.TagEditor_SelectedEntryChanged);
            this.editorSplitter.Panel1.ResumeLayout(false);
            this.editorSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.editorSplitter)).EndInit();
            this.editorSplitter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer editorSplitter;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.TreeView treeView1;
    }
}
