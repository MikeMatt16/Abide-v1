namespace Abide.Guerilla
{
    partial class TagGroupFileEditor
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
            this.guerillaFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // guerillaFlowLayoutPanel
            // 
            this.guerillaFlowLayoutPanel.AutoScroll = true;
            this.guerillaFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guerillaFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.guerillaFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.guerillaFlowLayoutPanel.Name = "guerillaFlowLayoutPanel";
            this.guerillaFlowLayoutPanel.Size = new System.Drawing.Size(634, 661);
            this.guerillaFlowLayoutPanel.TabIndex = 1;
            this.guerillaFlowLayoutPanel.WrapContents = false;
            // 
            // TagGroupFileEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 661);
            this.Controls.Add(this.guerillaFlowLayoutPanel);
            this.Name = "TagGroupFileEditor";
            this.Text = "TagGroupFileEditor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TagGroupFileEditor_FormClosing);
            this.Load += new System.EventHandler(this.TagGroupFileEditor_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel guerillaFlowLayoutPanel;
    }
}