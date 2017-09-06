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


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.nestedPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // nestedPanel
            // 
            this.nestedPanel.AutoSize = true;
            this.nestedPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.nestedPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.nestedPanel.Location = new System.Drawing.Point(12, 12);
            this.nestedPanel.Name = "nestedPanel";
            this.nestedPanel.Size = new System.Drawing.Size(0, 0);
            this.nestedPanel.TabIndex = 0;
            this.nestedPanel.WrapContents = false;
            // 
            // TagEditor
            // 
            this.Author = "Click16";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.nestedPanel);
            this.Description = "Editor for tag data.";
            this.Icon = global::Tag_Data_Editor.Properties.Resources.Meta_Editor;
            this.Name = "TagEditor";
            this.Size = new System.Drawing.Size(634, 611);
            this.ToolName = "Tag Editor";
            this.SelectedEntryChanged += new System.EventHandler(this.TagEditor_SelectedEntryChanged);
            this.Load += new System.EventHandler(this.TagEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel nestedPanel;
    }
}
