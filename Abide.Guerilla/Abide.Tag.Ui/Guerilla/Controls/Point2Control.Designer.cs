namespace Abide.Tag.Ui.Guerilla.Controls
{
    partial class Point2Control
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
            this.jTextBox = new System.Windows.Forms.TextBox();
            this.yLabel = new System.Windows.Forms.Label();
            this.iTextBox = new System.Windows.Forms.TextBox();
            this.xLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // jTextBox
            // 
            this.jTextBox.Location = new System.Drawing.Point(394, 5);
            this.jTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.jTextBox.Name = "jTextBox";
            this.jTextBox.Size = new System.Drawing.Size(65, 22);
            this.jTextBox.TabIndex = 11;
            // 
            // yLabel
            // 
            this.yLabel.AutoSize = true;
            this.yLabel.Location = new System.Drawing.Point(374, 8);
            this.yLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.yLabel.Name = "yLabel";
            this.yLabel.Size = new System.Drawing.Size(15, 17);
            this.yLabel.TabIndex = 9;
            this.yLabel.Text = "y";
            // 
            // iTextBox
            // 
            this.iTextBox.Location = new System.Drawing.Point(300, 5);
            this.iTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.iTextBox.Name = "iTextBox";
            this.iTextBox.Size = new System.Drawing.Size(65, 22);
            this.iTextBox.TabIndex = 12;
            // 
            // xLabel
            // 
            this.xLabel.AutoSize = true;
            this.xLabel.Location = new System.Drawing.Point(280, 8);
            this.xLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.xLabel.Name = "xLabel";
            this.xLabel.Size = new System.Drawing.Size(14, 17);
            this.xLabel.TabIndex = 10;
            this.xLabel.Text = "x";
            // 
            // Point2Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.jTextBox);
            this.Controls.Add(this.yLabel);
            this.Controls.Add(this.iTextBox);
            this.Controls.Add(this.xLabel);
            this.Name = "Point2Control";
            this.Controls.SetChildIndex(this.xLabel, 0);
            this.Controls.SetChildIndex(this.iTextBox, 0);
            this.Controls.SetChildIndex(this.yLabel, 0);
            this.Controls.SetChildIndex(this.jTextBox, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox jTextBox;
        private System.Windows.Forms.Label yLabel;
        private System.Windows.Forms.TextBox iTextBox;
        private System.Windows.Forms.Label xLabel;
    }
}
