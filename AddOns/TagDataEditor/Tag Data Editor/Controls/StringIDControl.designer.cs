namespace Tag_Data_Editor.Controls
{
    partial class StringIDControl
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
            this.nameLabel = new System.Windows.Forms.Label();
            this.typeLabel = new System.Windows.Forms.Label();
            this.stringSelectBox = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(292, 6);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(59, 13);
            this.nameLabel.TabIndex = 6;
            this.nameLabel.Text = "nameLabel";
            // 
            // typeLabel
            // 
            this.typeLabel.AutoSize = true;
            this.typeLabel.Location = new System.Drawing.Point(3, 6);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(53, 13);
            this.typeLabel.TabIndex = 7;
            this.typeLabel.Text = "typeLabel";
            // 
            // stringSelectBox
            // 
            this.stringSelectBox.Location = new System.Drawing.Point(117, 3);
            this.stringSelectBox.Name = "stringSelectBox";
            this.stringSelectBox.Size = new System.Drawing.Size(169, 23);
            this.stringSelectBox.TabIndex = 0;
            this.stringSelectBox.UseVisualStyleBackColor = true;
            this.stringSelectBox.Click += new System.EventHandler(this.stringSelectBox_Click);
            // 
            // StringIDControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.stringSelectBox);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.typeLabel);
            this.Name = "StringIDControl";
            this.Size = new System.Drawing.Size(354, 29);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label typeLabel;
        private System.Windows.Forms.Button stringSelectBox;
    }
}
