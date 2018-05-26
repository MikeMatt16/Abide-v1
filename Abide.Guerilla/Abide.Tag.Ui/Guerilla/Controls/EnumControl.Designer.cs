namespace Abide.Tag.Ui.Guerilla.Controls
{
    partial class EnumControl
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
            this.detailsLabel = new System.Windows.Forms.Label();
            this.informationToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.enumComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // detailsLabel
            // 
            this.detailsLabel.AutoSize = true;
            this.detailsLabel.Location = new System.Drawing.Point(405, 6);
            this.detailsLabel.Name = "detailsLabel";
            this.detailsLabel.Size = new System.Drawing.Size(0, 13);
            this.detailsLabel.TabIndex = 2;
            // 
            // informationToolTip
            // 
            this.informationToolTip.UseAnimation = false;
            this.informationToolTip.UseFading = false;
            // 
            // enumComboBox
            // 
            this.enumComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.enumComboBox.FormattingEnabled = true;
            this.enumComboBox.Location = new System.Drawing.Point(230, 3);
            this.enumComboBox.Name = "enumComboBox";
            this.enumComboBox.Size = new System.Drawing.Size(180, 21);
            this.enumComboBox.TabIndex = 3;
            this.enumComboBox.MouseHover += new System.EventHandler(this.enumComboBox_MouseHover);
            // 
            // EnumControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.enumComboBox);
            this.Controls.Add(this.detailsLabel);
            this.MinimumSize = new System.Drawing.Size(554, 0);
            this.Name = "EnumControl";
            this.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.Size = new System.Drawing.Size(554, 27);
            this.Controls.SetChildIndex(this.detailsLabel, 0);
            this.Controls.SetChildIndex(this.enumComboBox, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label detailsLabel;
        private System.Windows.Forms.ToolTip informationToolTip;
        private System.Windows.Forms.ComboBox enumComboBox;
    }
}
