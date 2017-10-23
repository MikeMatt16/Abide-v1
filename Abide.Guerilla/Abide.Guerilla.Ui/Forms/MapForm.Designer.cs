namespace Abide.Guerilla.Ui.Forms
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
            this.tagTreeSplitter = new System.Windows.Forms.SplitContainer();
            this.tagTreeView = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.tagTreeSplitter)).BeginInit();
            this.tagTreeSplitter.Panel1.SuspendLayout();
            this.tagTreeSplitter.SuspendLayout();
            this.SuspendLayout();
            // 
            // tagTreeSplitter
            // 
            this.tagTreeSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tagTreeSplitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.tagTreeSplitter.Location = new System.Drawing.Point(0, 0);
            this.tagTreeSplitter.Name = "tagTreeSplitter";
            // 
            // tagTreeSplitter.Panel1
            // 
            this.tagTreeSplitter.Panel1.Controls.Add(this.tagTreeView);
            this.tagTreeSplitter.Size = new System.Drawing.Size(784, 661);
            this.tagTreeSplitter.SplitterDistance = 261;
            this.tagTreeSplitter.TabIndex = 0;
            // 
            // tagTreeView
            // 
            this.tagTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tagTreeView.Location = new System.Drawing.Point(0, 0);
            this.tagTreeView.Name = "tagTreeView";
            this.tagTreeView.Size = new System.Drawing.Size(261, 661);
            this.tagTreeView.TabIndex = 0;
            this.tagTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TagTreeView_AfterSelect);
            // 
            // MapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 661);
            this.Controls.Add(this.tagTreeSplitter);
            this.Name = "MapForm";
            this.Text = "MapForm";
            this.tagTreeSplitter.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tagTreeSplitter)).EndInit();
            this.tagTreeSplitter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer tagTreeSplitter;
        private System.Windows.Forms.TreeView tagTreeView;
    }
}