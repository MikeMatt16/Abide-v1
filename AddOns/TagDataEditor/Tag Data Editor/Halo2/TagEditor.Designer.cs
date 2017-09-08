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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.container = new System.Windows.Forms.Panel();
            this.tagDataEditor = new Tag_Data_Editor.Halo2.TagDataEditor();
            this.toolStrip1.SuspendLayout();
            this.container.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(150, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // container
            // 
            this.container.AutoScroll = true;
            this.container.Controls.Add(this.tagDataEditor);
            this.container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.container.Location = new System.Drawing.Point(0, 25);
            this.container.Name = "container";
            this.container.Size = new System.Drawing.Size(150, 125);
            this.container.TabIndex = 1;
            // 
            // tagDataEditor
            // 
            this.tagDataEditor.AutoSize = true;
            this.tagDataEditor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tagDataEditor.Entry = null;
            this.tagDataEditor.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.tagDataEditor.Location = new System.Drawing.Point(0, 0);
            this.tagDataEditor.Map = null;
            this.tagDataEditor.Name = "tagDataEditor";
            this.tagDataEditor.Owner = null;
            this.tagDataEditor.Size = new System.Drawing.Size(0, 0);
            this.tagDataEditor.TabIndex = 0;
            this.tagDataEditor.WrapContents = false;
            // 
            // TagEditor
            // 
            this.Author = "Click16";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.container);
            this.Controls.Add(this.toolStrip1);
            this.Description = "Controls to edit tag properties.";
            this.Icon = global::Tag_Data_Editor.Properties.Resources.Meta_Editor;
            this.Name = "TagEditor";
            this.ToolName = "Tag Editor";
            this.Initialize += new System.EventHandler<Abide.AddOnApi.AddOnHostEventArgs>(this.TagEditor_Initialize);
            this.MapLoad += new System.EventHandler(this.TagEditor_MapLoad);
            this.SelectedEntryChanged += new System.EventHandler(this.TagEditor_SelectedEntryChanged);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.container.ResumeLayout(false);
            this.container.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Panel container;
        private TagDataEditor tagDataEditor;
    }
}
