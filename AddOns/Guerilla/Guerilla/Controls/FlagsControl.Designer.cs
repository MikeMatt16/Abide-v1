namespace Guerilla.Controls
{
    partial class FlagsControl
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
            this.flagsCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(3, 3);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(0, 13);
            this.nameLabel.TabIndex = 0;
            // 
            // flagsCheckedListBox
            // 
            this.flagsCheckedListBox.FormattingEnabled = true;
            this.flagsCheckedListBox.Location = new System.Drawing.Point(240, 3);
            this.flagsCheckedListBox.Name = "flagsCheckedListBox";
            this.flagsCheckedListBox.Size = new System.Drawing.Size(180, 19);
            this.flagsCheckedListBox.TabIndex = 1;
            // 
            // FlagsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.flagsCheckedListBox);
            this.Controls.Add(this.nameLabel);
            this.MinimumSize = new System.Drawing.Size(680, 26);
            this.Name = "FlagsControl";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.CheckedListBox flagsCheckedListBox;
    }
}
