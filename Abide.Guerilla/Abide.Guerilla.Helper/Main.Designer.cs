namespace Abide.Guerilla.Helper
{
    partial class Main
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tagCountToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.analyzeMapsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analyzeTagsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.IntegralHeight = false;
            this.listBox1.Location = new System.Drawing.Point(0, 27);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(800, 423);
            this.listBox1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.tagCountToolStripTextBox,
            this.analyzeMapsToolStripMenuItem,
            this.analyzeTagsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 27);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(48, 23);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // tagCountToolStripTextBox
            // 
            this.tagCountToolStripTextBox.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tagCountToolStripTextBox.Name = "tagCountToolStripTextBox";
            this.tagCountToolStripTextBox.ReadOnly = true;
            this.tagCountToolStripTextBox.Size = new System.Drawing.Size(150, 23);
            // 
            // analyzeMapsToolStripMenuItem
            // 
            this.analyzeMapsToolStripMenuItem.Name = "analyzeMapsToolStripMenuItem";
            this.analyzeMapsToolStripMenuItem.Size = new System.Drawing.Size(92, 23);
            this.analyzeMapsToolStripMenuItem.Text = "Analyze &Maps";
            this.analyzeMapsToolStripMenuItem.Click += new System.EventHandler(this.analyzeMapsToolStripMenuItem_Click);
            // 
            // analyzeTagsToolStripMenuItem
            // 
            this.analyzeTagsToolStripMenuItem.Name = "analyzeTagsToolStripMenuItem";
            this.analyzeTagsToolStripMenuItem.Size = new System.Drawing.Size(87, 23);
            this.analyzeTagsToolStripMenuItem.Text = "Analyze &Tags";
            this.analyzeTagsToolStripMenuItem.Click += new System.EventHandler(this.analyzeTagsToolStripMenuItem_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "Abide Guerilla Helper";
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox tagCountToolStripTextBox;
        private System.Windows.Forms.ToolStripMenuItem analyzeMapsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem analyzeTagsToolStripMenuItem;
    }
}

