namespace Abide.Tag.Ui
{
    partial class BlockViewForm
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
            this.tagBlockView = new Abide.Tag.Ui.Controls.BlockView();
            this.SuspendLayout();
            // 
            // tagBlockView
            // 
            this.tagBlockView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tagBlockView.BackColor = System.Drawing.Color.Gainsboro;
            this.tagBlockView.BlockHeight = 16;
            this.tagBlockView.BlockWidth = 1;
            this.tagBlockView.DataLength = 0;
            this.tagBlockView.DrawBorder = false;
            this.tagBlockView.Location = new System.Drawing.Point(12, 12);
            this.tagBlockView.Name = "tagBlockView";
            this.tagBlockView.Offset = 0;
            this.tagBlockView.ScrollBarVisible = false;
            this.tagBlockView.Size = new System.Drawing.Size(776, 426);
            this.tagBlockView.TabIndex = 0;
            // 
            // BlockViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tagBlockView);
            this.Name = "BlockViewForm";
            this.Text = "BlockViewForm";
            this.Load += new System.EventHandler(this.BlockViewForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.BlockView tagBlockView;
    }
}