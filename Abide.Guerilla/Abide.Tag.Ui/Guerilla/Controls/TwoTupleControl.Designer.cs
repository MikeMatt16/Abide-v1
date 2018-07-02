namespace Abide.Tag.Ui.Guerilla.Controls
{
    partial class TwoTupleControl
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
            this.bTextBox = new System.Windows.Forms.TextBox();
            this.bLabel = new System.Windows.Forms.Label();
            this.aTextBox = new System.Windows.Forms.TextBox();
            this.aLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bTextBox
            // 
            this.bTextBox.Location = new System.Drawing.Point(301, 3);
            this.bTextBox.Name = "bTextBox";
            this.bTextBox.Size = new System.Drawing.Size(50, 20);
            this.bTextBox.TabIndex = 3;
            // 
            // bLabel
            // 
            this.bLabel.AutoSize = true;
            this.bLabel.Location = new System.Drawing.Point(286, 6);
            this.bLabel.Name = "bLabel";
            this.bLabel.Size = new System.Drawing.Size(13, 13);
            this.bLabel.TabIndex = 2;
            this.bLabel.Text = "b";
            // 
            // aTextBox
            // 
            this.aTextBox.Location = new System.Drawing.Point(230, 3);
            this.aTextBox.Name = "aTextBox";
            this.aTextBox.Size = new System.Drawing.Size(50, 20);
            this.aTextBox.TabIndex = 1;
            // 
            // aLabel
            // 
            this.aLabel.AutoSize = true;
            this.aLabel.Location = new System.Drawing.Point(215, 6);
            this.aLabel.Name = "aLabel";
            this.aLabel.Size = new System.Drawing.Size(13, 13);
            this.aLabel.TabIndex = 0;
            this.aLabel.Text = "a";
            // 
            // TwoTupleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bTextBox);
            this.Controls.Add(this.bLabel);
            this.Controls.Add(this.aTextBox);
            this.Controls.Add(this.aLabel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(416, 21);
            this.Name = "TwoTupleControl";
            this.Size = new System.Drawing.Size(416, 26);
            this.Controls.SetChildIndex(this.aLabel, 0);
            this.Controls.SetChildIndex(this.aTextBox, 0);
            this.Controls.SetChildIndex(this.bLabel, 0);
            this.Controls.SetChildIndex(this.bTextBox, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox bTextBox;
        private System.Windows.Forms.Label bLabel;
        private System.Windows.Forms.TextBox aTextBox;
        private System.Windows.Forms.Label aLabel;
    }
}
