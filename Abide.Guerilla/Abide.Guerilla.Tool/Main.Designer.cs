namespace Abide.Guerilla.Tool
{
    partial class Main
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bitmapToolButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bitmapToolButton
            // 
            this.bitmapToolButton.Location = new System.Drawing.Point(12, 12);
            this.bitmapToolButton.Name = "bitmapToolButton";
            this.bitmapToolButton.Size = new System.Drawing.Size(75, 23);
            this.bitmapToolButton.TabIndex = 0;
            this.bitmapToolButton.Text = "&Bitmap Tool";
            this.bitmapToolButton.UseVisualStyleBackColor = true;
            this.bitmapToolButton.Click += new System.EventHandler(this.bitmapToolButton_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 261);
            this.Controls.Add(this.bitmapToolButton);
            this.Name = "Main";
            this.Text = "Guerilla Tools";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bitmapToolButton;
    }
}

