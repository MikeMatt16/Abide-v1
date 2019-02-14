namespace Abide.Tag.Ui.Guerilla.Controls
{
    partial class TagReferenceControl
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
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.browseTagButton = new System.Windows.Forms.Button();
            this.gotoButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pathTextBox
            // 
            this.pathTextBox.Location = new System.Drawing.Point(208, 3);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.ReadOnly = true;
            this.pathTextBox.Size = new System.Drawing.Size(200, 20);
            this.pathTextBox.TabIndex = 4;
            // 
            // browseTagButton
            // 
            this.browseTagButton.Location = new System.Drawing.Point(414, 3);
            this.browseTagButton.Name = "browseTagButton";
            this.browseTagButton.Size = new System.Drawing.Size(32, 20);
            this.browseTagButton.TabIndex = 5;
            this.browseTagButton.Text = "...";
            this.browseTagButton.UseVisualStyleBackColor = true;
            this.browseTagButton.Click += new System.EventHandler(this.browseTagButton_Click);
            // 
            // gotoButton
            // 
            this.gotoButton.Location = new System.Drawing.Point(452, 3);
            this.gotoButton.Name = "gotoButton";
            this.gotoButton.Size = new System.Drawing.Size(32, 20);
            this.gotoButton.TabIndex = 6;
            this.gotoButton.Text = "->";
            this.gotoButton.UseVisualStyleBackColor = true;
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(490, 3);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(45, 20);
            this.clearButton.TabIndex = 7;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // TagReferenceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.gotoButton);
            this.Controls.Add(this.browseTagButton);
            this.Controls.Add(this.pathTextBox);
            this.Name = "TagReferenceControl";
            this.Controls.SetChildIndex(this.pathTextBox, 0);
            this.Controls.SetChildIndex(this.browseTagButton, 0);
            this.Controls.SetChildIndex(this.gotoButton, 0);
            this.Controls.SetChildIndex(this.clearButton, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.Button browseTagButton;
        private System.Windows.Forms.Button gotoButton;
        private System.Windows.Forms.Button clearButton;
    }
}
