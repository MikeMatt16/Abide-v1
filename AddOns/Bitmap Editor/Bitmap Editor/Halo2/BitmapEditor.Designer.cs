namespace Bitmap_Editor.Halo2
{
    partial class BitmapEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BitmapEditor));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lodUpDown = new System.Windows.Forms.NumericUpDown();
            this.bitmapUpDown = new System.Windows.Forms.NumericUpDown();
            this.bitmapBox = new System.Windows.Forms.PictureBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.importProperties = new System.Windows.Forms.PropertyGrid();
            this.bitmapProperties = new System.Windows.Forms.PropertyGrid();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.importToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.exportToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.formatLabel = new System.Windows.Forms.ToolStripLabel();
            this.locationLabel = new System.Windows.Forms.ToolStripLabel();
            this.dumpTexturesToolStripButton = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lodUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bitmapUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bitmapBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.lodUpDown);
            this.splitContainer1.Panel1.Controls.Add(this.bitmapUpDown);
            this.splitContainer1.Panel1.Controls.Add(this.bitmapBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(500, 500);
            this.splitContainer1.SplitterDistance = 262;
            this.splitContainer1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(172, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "&LOD Index:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "&Bitmap Index:";
            // 
            // lodUpDown
            // 
            this.lodUpDown.Location = new System.Drawing.Point(239, 12);
            this.lodUpDown.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.lodUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.lodUpDown.Name = "lodUpDown";
            this.lodUpDown.Size = new System.Drawing.Size(51, 20);
            this.lodUpDown.TabIndex = 0;
            this.lodUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.lodUpDown.ValueChanged += new System.EventHandler(this.indexUpDown_ValueChanged);
            // 
            // bitmapUpDown
            // 
            this.bitmapUpDown.Location = new System.Drawing.Point(86, 12);
            this.bitmapUpDown.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.bitmapUpDown.Name = "bitmapUpDown";
            this.bitmapUpDown.Size = new System.Drawing.Size(80, 20);
            this.bitmapUpDown.TabIndex = 0;
            this.bitmapUpDown.ValueChanged += new System.EventHandler(this.indexUpDown_ValueChanged);
            // 
            // bitmapBox
            // 
            this.bitmapBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bitmapBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bitmapBox.Location = new System.Drawing.Point(12, 38);
            this.bitmapBox.Name = "bitmapBox";
            this.bitmapBox.Size = new System.Drawing.Size(476, 221);
            this.bitmapBox.TabIndex = 1;
            this.bitmapBox.TabStop = false;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 25);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.importProperties);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.bitmapProperties);
            this.splitContainer2.Size = new System.Drawing.Size(500, 209);
            this.splitContainer2.SplitterDistance = 250;
            this.splitContainer2.TabIndex = 0;
            // 
            // importProperties
            // 
            this.importProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.importProperties.HelpVisible = false;
            this.importProperties.LineColor = System.Drawing.SystemColors.ControlDark;
            this.importProperties.Location = new System.Drawing.Point(0, 0);
            this.importProperties.Name = "importProperties";
            this.importProperties.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.importProperties.Size = new System.Drawing.Size(250, 209);
            this.importProperties.TabIndex = 0;
            this.importProperties.ToolbarVisible = false;
            // 
            // bitmapProperties
            // 
            this.bitmapProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bitmapProperties.HelpVisible = false;
            this.bitmapProperties.LineColor = System.Drawing.SystemColors.ControlDark;
            this.bitmapProperties.Location = new System.Drawing.Point(0, 0);
            this.bitmapProperties.Name = "bitmapProperties";
            this.bitmapProperties.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.bitmapProperties.Size = new System.Drawing.Size(246, 209);
            this.bitmapProperties.TabIndex = 2;
            this.bitmapProperties.ToolbarVisible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripButton,
            this.exportToolStripButton,
            this.formatLabel,
            this.locationLabel,
            this.dumpTexturesToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(500, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // importToolStripButton
            // 
            this.importToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.importToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("importToolStripButton.Image")));
            this.importToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.importToolStripButton.Name = "importToolStripButton";
            this.importToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.importToolStripButton.Text = "&Import";
            // 
            // exportToolStripButton
            // 
            this.exportToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.exportToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("exportToolStripButton.Image")));
            this.exportToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exportToolStripButton.Name = "exportToolStripButton";
            this.exportToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.exportToolStripButton.Text = "&Export";
            // 
            // formatLabel
            // 
            this.formatLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.formatLabel.Name = "formatLabel";
            this.formatLabel.Size = new System.Drawing.Size(16, 22);
            this.formatLabel.Text = "...";
            // 
            // locationLabel
            // 
            this.locationLabel.Name = "locationLabel";
            this.locationLabel.Size = new System.Drawing.Size(16, 22);
            this.locationLabel.Text = "...";
            // 
            // dumpTexturesToolStripButton
            // 
            this.dumpTexturesToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.dumpTexturesToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.dumpTexturesToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("dumpTexturesToolStripButton.Image")));
            this.dumpTexturesToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.dumpTexturesToolStripButton.Name = "dumpTexturesToolStripButton";
            this.dumpTexturesToolStripButton.Size = new System.Drawing.Size(61, 22);
            this.dumpTexturesToolStripButton.Text = "&Export All";
            this.dumpTexturesToolStripButton.ToolTipText = "Export All Textures";
            // 
            // BitmapEditor
            // 
            this.Author = "Click16";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Description = "Allows bitmap texture tag manipulation.";
            this.Icon = global::Bitmap_Editor.Properties.Resources.texture_edit;
            this.Name = "BitmapEditor";
            this.Size = new System.Drawing.Size(500, 500);
            this.ToolName = "Texture Editor";
            this.SelectedEntryChanged += new System.EventHandler(this.BitmapEditor_SelectedEntryChanged);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lodUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bitmapUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bitmapBox)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown lodUpDown;
        private System.Windows.Forms.NumericUpDown bitmapUpDown;
        private System.Windows.Forms.PictureBox bitmapBox;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.PropertyGrid importProperties;
        private System.Windows.Forms.PropertyGrid bitmapProperties;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton importToolStripButton;
        private System.Windows.Forms.ToolStripButton exportToolStripButton;
        private System.Windows.Forms.ToolStripLabel formatLabel;
        private System.Windows.Forms.ToolStripLabel locationLabel;
        private System.Windows.Forms.ToolStripButton dumpTexturesToolStripButton;
    }
}
