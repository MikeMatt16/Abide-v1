namespace Hex_Editor
{
    partial class HexControl
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
            this.headerViewPictureBox = new System.Windows.Forms.PictureBox();
            this.editorPanel = new System.Windows.Forms.Panel();
            this.dataViewPictureBox = new System.Windows.Forms.PictureBox();
            this.offsetPlaneSplitter = new System.Windows.Forms.Splitter();
            this.offsetViewPictureBox = new System.Windows.Forms.PictureBox();
            this.offsetScrollBar = new System.Windows.Forms.VScrollBar();
            ((System.ComponentModel.ISupportInitialize)(this.headerViewPictureBox)).BeginInit();
            this.editorPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataViewPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.offsetViewPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // headerViewPictureBox
            // 
            this.headerViewPictureBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerViewPictureBox.Location = new System.Drawing.Point(0, 0);
            this.headerViewPictureBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.headerViewPictureBox.Name = "headerViewPictureBox";
            this.headerViewPictureBox.Size = new System.Drawing.Size(335, 18);
            this.headerViewPictureBox.TabIndex = 4;
            this.headerViewPictureBox.TabStop = false;
            // 
            // editorPanel
            // 
            this.editorPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.editorPanel.Controls.Add(this.dataViewPictureBox);
            this.editorPanel.Controls.Add(this.offsetPlaneSplitter);
            this.editorPanel.Controls.Add(this.offsetViewPictureBox);
            this.editorPanel.Controls.Add(this.offsetScrollBar);
            this.editorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorPanel.Location = new System.Drawing.Point(0, 18);
            this.editorPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.editorPanel.Name = "editorPanel";
            this.editorPanel.Size = new System.Drawing.Size(335, 325);
            this.editorPanel.TabIndex = 5;
            // 
            // dataViewPictureBox
            // 
            this.dataViewPictureBox.BackColor = System.Drawing.Color.White;
            this.dataViewPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataViewPictureBox.Location = new System.Drawing.Point(14, 0);
            this.dataViewPictureBox.Name = "dataViewPictureBox";
            this.dataViewPictureBox.Size = new System.Drawing.Size(300, 321);
            this.dataViewPictureBox.TabIndex = 7;
            this.dataViewPictureBox.TabStop = false;
            this.dataViewPictureBox.SizeChanged += new System.EventHandler(this.dataViewPictureBox_SizeChanged);
            // 
            // offsetPlaneSplitter
            // 
            this.offsetPlaneSplitter.Location = new System.Drawing.Point(11, 0);
            this.offsetPlaneSplitter.Name = "offsetPlaneSplitter";
            this.offsetPlaneSplitter.Size = new System.Drawing.Size(3, 321);
            this.offsetPlaneSplitter.TabIndex = 6;
            this.offsetPlaneSplitter.TabStop = false;
            this.offsetPlaneSplitter.SplitterMoving += new System.Windows.Forms.SplitterEventHandler(this.offsetPlaneSplitter_SplitterMoving);
            this.offsetPlaneSplitter.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.offsetPlaneSplitter_SplitterMoved);
            // 
            // offsetViewPictureBox
            // 
            this.offsetViewPictureBox.BackColor = System.Drawing.SystemColors.Control;
            this.offsetViewPictureBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.offsetViewPictureBox.Location = new System.Drawing.Point(0, 0);
            this.offsetViewPictureBox.Name = "offsetViewPictureBox";
            this.offsetViewPictureBox.Size = new System.Drawing.Size(11, 321);
            this.offsetViewPictureBox.TabIndex = 5;
            this.offsetViewPictureBox.TabStop = false;
            // 
            // offsetScrollBar
            // 
            this.offsetScrollBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.offsetScrollBar.Location = new System.Drawing.Point(314, 0);
            this.offsetScrollBar.Name = "offsetScrollBar";
            this.offsetScrollBar.Size = new System.Drawing.Size(17, 321);
            this.offsetScrollBar.TabIndex = 4;
            this.offsetScrollBar.Visible = false;
            this.offsetScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.offsetScrollBar_Scroll);
            // 
            // HexControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.editorPanel);
            this.Controls.Add(this.headerViewPictureBox);
            this.Name = "HexControl";
            this.Size = new System.Drawing.Size(335, 343);
            ((System.ComponentModel.ISupportInitialize)(this.headerViewPictureBox)).EndInit();
            this.editorPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataViewPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.offsetViewPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox headerViewPictureBox;
        private System.Windows.Forms.Panel editorPanel;
        private System.Windows.Forms.PictureBox dataViewPictureBox;
        private System.Windows.Forms.Splitter offsetPlaneSplitter;
        private System.Windows.Forms.PictureBox offsetViewPictureBox;
        private System.Windows.Forms.VScrollBar offsetScrollBar;
    }
}
