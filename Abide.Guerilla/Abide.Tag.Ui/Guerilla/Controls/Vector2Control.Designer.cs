namespace Abide.Tag.Ui.Guerilla.Controls
{
    partial class Vector2Control
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
            this.label1 = new System.Windows.Forms.Label();
            this.iTextBox = new System.Windows.Forms.TextBox();
            this.iLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // jTextBox
            // 
            this.jTextBox.Location = new System.Drawing.Point(401, 4);
            this.jTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.jTextBox.Name = "jTextBox";
            this.jTextBox.Size = new System.Drawing.Size(65, 22);
            this.jTextBox.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(381, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "j";
            // 
            // iTextBox
            // 
            this.iTextBox.Location = new System.Drawing.Point(307, 4);
            this.iTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.iTextBox.Name = "iTextBox";
            this.iTextBox.Size = new System.Drawing.Size(65, 22);
            this.iTextBox.TabIndex = 8;
            // 
            // iLabel
            // 
            this.iLabel.AutoSize = true;
            this.iLabel.Location = new System.Drawing.Point(287, 7);
            this.iLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.iLabel.Name = "iLabel";
            this.iLabel.Size = new System.Drawing.Size(11, 17);
            this.iLabel.TabIndex = 5;
            this.iLabel.Text = "i";
            // 
            // Vector2Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.jTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.iTextBox);
            this.Controls.Add(this.iLabel);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "Vector2Control";
            this.Controls.SetChildIndex(this.iLabel, 0);
            this.Controls.SetChildIndex(this.iTextBox, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.jTextBox, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox jTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox iTextBox;
        private System.Windows.Forms.Label iLabel;
    }
}
