namespace Abide.Tag.Ui.Guerilla
{
    partial class TagForm
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
            this.gueriallFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // gueriallFlowLayoutPanel
            // 
            this.gueriallFlowLayoutPanel.AutoScroll = true;
            this.gueriallFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gueriallFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.gueriallFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.gueriallFlowLayoutPanel.Name = "gueriallFlowLayoutPanel";
            this.gueriallFlowLayoutPanel.Size = new System.Drawing.Size(584, 661);
            this.gueriallFlowLayoutPanel.TabIndex = 0;
            this.gueriallFlowLayoutPanel.WrapContents = false;
            // 
            // TagForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 661);
            this.Controls.Add(this.gueriallFlowLayoutPanel);
            this.Name = "TagForm";
            this.ShowIcon = false;
            this.Text = "TagForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel gueriallFlowLayoutPanel;
    }
}