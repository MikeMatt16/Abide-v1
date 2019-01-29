namespace Abide.Tag.Ui.Guerilla.Controls
{
    partial class ValueControl
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
            this.components = new System.ComponentModel.Container();
            this.nameLabel = new System.Windows.Forms.Label();
            this.valueTextBox = new System.Windows.Forms.TextBox();
            this.detailsLabel = new System.Windows.Forms.Label();
            this.informationToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(15, 7);
            this.nameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(0, 17);
            this.nameLabel.TabIndex = 0;
            // 
            // valueTextBox
            // 
            this.valueTextBox.Location = new System.Drawing.Point(307, 4);
            this.valueTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.valueTextBox.Name = "valueTextBox";
            this.valueTextBox.Size = new System.Drawing.Size(80, 22);
            this.valueTextBox.TabIndex = 1;
            this.valueTextBox.TextChanged += new System.EventHandler(this.valueTextBox_TextChanged);
            this.valueTextBox.MouseHover += new System.EventHandler(this.valueTextBox_MouseHover);
            // 
            // detailsLabel
            // 
            this.detailsLabel.AutoSize = true;
            this.detailsLabel.Location = new System.Drawing.Point(395, 7);
            this.detailsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.detailsLabel.Name = "detailsLabel";
            this.detailsLabel.Size = new System.Drawing.Size(49, 17);
            this.detailsLabel.TabIndex = 2;
            this.detailsLabel.Text = "details";
            // 
            // informationToolTip
            // 
            this.informationToolTip.UseAnimation = false;
            this.informationToolTip.UseFading = false;
            // 
            // ValueControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.detailsLabel);
            this.Controls.Add(this.valueTextBox);
            this.Controls.Add(this.nameLabel);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.MinimumSize = new System.Drawing.Size(739, 0);
            this.Name = "ValueControl";
            this.Padding = new System.Windows.Forms.Padding(11, 0, 11, 0);
            this.Size = new System.Drawing.Size(739, 30);
            this.Controls.SetChildIndex(this.nameLabel, 0);
            this.Controls.SetChildIndex(this.valueTextBox, 0);
            this.Controls.SetChildIndex(this.detailsLabel, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox valueTextBox;
        private System.Windows.Forms.Label detailsLabel;
        private System.Windows.Forms.ToolTip informationToolTip;
    }
}
