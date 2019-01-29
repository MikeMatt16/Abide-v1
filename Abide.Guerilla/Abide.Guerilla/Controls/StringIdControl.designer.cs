namespace Abide.Tag.Ui.Guerilla.Controls
{
    partial class StringIdControl
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
            this.stringTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // stringTextBox
            // 
            this.stringTextBox.Location = new System.Drawing.Point(208, 3);
            this.stringTextBox.Name = "stringTextBox";
            this.stringTextBox.ReadOnly = true;
            this.stringTextBox.Size = new System.Drawing.Size(200, 20);
            this.stringTextBox.TabIndex = 6;
            // 
            // StringIdControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.stringTextBox);
            this.Name = "StringIdControl";
            this.Controls.SetChildIndex(this.stringTextBox, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox stringTextBox;
    }
}
