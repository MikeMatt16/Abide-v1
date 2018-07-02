namespace Guerilla.Controls
{
    partial class Value3Control
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
            this.value1Label = new System.Windows.Forms.Label();
            this.value1TextBox = new System.Windows.Forms.TextBox();
            this.value2Label = new System.Windows.Forms.Label();
            this.value2TextBox = new System.Windows.Forms.TextBox();
            this.value3TextBox = new System.Windows.Forms.TextBox();
            this.value3Label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(3, 6);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(0, 13);
            this.nameLabel.TabIndex = 0;
            // 
            // value1Label
            // 
            this.value1Label.AutoSize = true;
            this.value1Label.Location = new System.Drawing.Point(222, 6);
            this.value1Label.Name = "value1Label";
            this.value1Label.Size = new System.Drawing.Size(0, 13);
            this.value1Label.TabIndex = 1;
            // 
            // value1TextBox
            // 
            this.value1TextBox.Location = new System.Drawing.Point(240, 3);
            this.value1TextBox.Name = "value1TextBox";
            this.value1TextBox.Size = new System.Drawing.Size(80, 20);
            this.value1TextBox.TabIndex = 2;
            // 
            // value2Label
            // 
            this.value2Label.AutoSize = true;
            this.value2Label.Location = new System.Drawing.Point(326, 6);
            this.value2Label.Name = "value2Label";
            this.value2Label.Size = new System.Drawing.Size(0, 13);
            this.value2Label.TabIndex = 3;
            // 
            // value2TextBox
            // 
            this.value2TextBox.Location = new System.Drawing.Point(332, 3);
            this.value2TextBox.Name = "value2TextBox";
            this.value2TextBox.Size = new System.Drawing.Size(80, 20);
            this.value2TextBox.TabIndex = 4;
            // 
            // value3TextBox
            // 
            this.value3TextBox.Location = new System.Drawing.Point(424, 3);
            this.value3TextBox.Name = "value3TextBox";
            this.value3TextBox.Size = new System.Drawing.Size(80, 20);
            this.value3TextBox.TabIndex = 6;
            // 
            // value3Label
            // 
            this.value3Label.AutoSize = true;
            this.value3Label.Location = new System.Drawing.Point(418, 6);
            this.value3Label.Name = "value3Label";
            this.value3Label.Size = new System.Drawing.Size(0, 13);
            this.value3Label.TabIndex = 5;
            // 
            // Value3Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.value3TextBox);
            this.Controls.Add(this.value3Label);
            this.Controls.Add(this.value2TextBox);
            this.Controls.Add(this.value2Label);
            this.Controls.Add(this.value1TextBox);
            this.Controls.Add(this.value1Label);
            this.Controls.Add(this.nameLabel);
            this.Name = "Value3Control";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label value1Label;
        private System.Windows.Forms.TextBox value1TextBox;
        private System.Windows.Forms.Label value2Label;
        private System.Windows.Forms.TextBox value2TextBox;
        private System.Windows.Forms.TextBox value3TextBox;
        private System.Windows.Forms.Label value3Label;
    }
}
