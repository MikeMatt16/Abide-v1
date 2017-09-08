namespace Tag_Data_Editor.Halo2
{
    partial class PopoutEditor
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
            this.tagDataEditor = new Tag_Data_Editor.Halo2.TagDataEditor();
            this.SuspendLayout();
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
            // PopoutEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.tagDataEditor);
            this.Name = "PopoutEditor";
            this.Text = "PopoutEditor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TagDataEditor tagDataEditor;
    }
}