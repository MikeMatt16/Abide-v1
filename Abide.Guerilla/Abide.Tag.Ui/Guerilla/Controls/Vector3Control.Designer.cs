namespace Abide.Tag.Ui.Guerilla.Controls
{
    partial class Vector3Control
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
            this.iLabel = new System.Windows.Forms.Label();
            this.iTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.jTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.kTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // iLabel
            // 
            this.iLabel.AutoSize = true;
            this.iLabel.Location = new System.Drawing.Point(215, 6);
            this.iLabel.Name = "iLabel";
            this.iLabel.Size = new System.Drawing.Size(9, 13);
            this.iLabel.TabIndex = 1;
            this.iLabel.Text = "i";
            // 
            // iTextBox
            // 
            this.iTextBox.Location = new System.Drawing.Point(230, 3);
            this.iTextBox.Name = "iTextBox";
            this.iTextBox.Size = new System.Drawing.Size(50, 20);
            this.iTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(286, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(9, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "j";
            // 
            // jTextBox
            // 
            this.jTextBox.Location = new System.Drawing.Point(301, 3);
            this.jTextBox.Name = "jTextBox";
            this.jTextBox.Size = new System.Drawing.Size(50, 20);
            this.jTextBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(357, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "k";
            // 
            // kTextBox
            // 
            this.kTextBox.Location = new System.Drawing.Point(372, 3);
            this.kTextBox.Name = "kTextBox";
            this.kTextBox.Size = new System.Drawing.Size(50, 20);
            this.kTextBox.TabIndex = 2;
            // 
            // Vector3Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kTextBox);
            this.Controls.Add(this.jTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.iTextBox);
            this.Controls.Add(this.iLabel);
            this.Name = "Vector3Control";
            this.Controls.SetChildIndex(this.iLabel, 0);
            this.Controls.SetChildIndex(this.iTextBox, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.jTextBox, 0);
            this.Controls.SetChildIndex(this.kTextBox, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label iLabel;
        private System.Windows.Forms.TextBox iTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox jTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox kTextBox;
    }
}
