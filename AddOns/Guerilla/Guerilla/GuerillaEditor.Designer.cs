namespace Guerilla
{
    partial class GuerillaEditor
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
            this.fieldsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // fieldsPanel
            // 
            this.fieldsPanel.AutoScroll = true;
            this.fieldsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldsPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fieldsPanel.Location = new System.Drawing.Point(0, 0);
            this.fieldsPanel.Name = "fieldsPanel";
            this.fieldsPanel.Size = new System.Drawing.Size(600, 600);
            this.fieldsPanel.TabIndex = 0;
            this.fieldsPanel.WrapContents = false;
            // 
            // GuerillaEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fieldsPanel);
            this.Name = "GuerillaEditor";
            this.Size = new System.Drawing.Size(600, 600);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel fieldsPanel;
    }
}
