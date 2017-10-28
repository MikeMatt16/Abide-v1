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
            this.groupPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.tagTreeSplitter)).BeginInit();
            this.tagTreeSplitter.Panel1.SuspendLayout();
            this.tagTreeSplitter.Panel2.SuspendLayout();
            this.tagTreeSplitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            // 
            // tagTreeSplitter.Panel2
            // 
            this.tagTreeSplitter.Panel2.Controls.Add(this.splitContainer1);
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
            // groupPropertyGrid
            // 
            this.groupPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPropertyGrid.LineColor = System.Drawing.SystemColors.ControlDark;
            this.groupPropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.groupPropertyGrid.Name = "groupPropertyGrid";
            this.groupPropertyGrid.Size = new System.Drawing.Size(248, 661);
            this.groupPropertyGrid.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupPropertyGrid);
            this.splitContainer1.Size = new System.Drawing.Size(519, 661);
            this.splitContainer1.SplitterDistance = 248;
            this.splitContainer1.TabIndex = 0;
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
            this.tagTreeSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tagTreeSplitter)).EndInit();
            this.tagTreeSplitter.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer tagTreeSplitter;
        private System.Windows.Forms.TreeView tagTreeView;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PropertyGrid groupPropertyGrid;
    }
}