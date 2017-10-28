namespace Abide.Guerilla.Ui.Forms
{
    partial class TextForm
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
            this.textRichTextBox = new System.Windows.Forms.RichTextBox();
            this.textFormMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textFormMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // textRichTextBox
            // 
            this.textRichTextBox.BackColor = System.Drawing.Color.DimGray;
            this.textRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textRichTextBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textRichTextBox.ForeColor = System.Drawing.Color.Gainsboro;
            this.textRichTextBox.Location = new System.Drawing.Point(0, 24);
            this.textRichTextBox.Name = "textRichTextBox";
            this.textRichTextBox.ReadOnly = true;
            this.textRichTextBox.Size = new System.Drawing.Size(768, 560);
            this.textRichTextBox.TabIndex = 0;
            this.textRichTextBox.Text = "";
            this.textRichTextBox.WordWrap = false;
            // 
            // textFormMenuStrip
            // 
            this.textFormMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.textFormMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.textFormMenuStrip.Name = "textFormMenuStrip";
            this.textFormMenuStrip.Size = new System.Drawing.Size(768, 24);
            this.textFormMenuStrip.TabIndex = 1;
            this.textFormMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.saveAsToolStripMenuItem.Text = "&Save As";
            // 
            // TextForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 584);
            this.Controls.Add(this.textRichTextBox);
            this.Controls.Add(this.textFormMenuStrip);
            this.MainMenuStrip = this.textFormMenuStrip;
            this.Name = "TextForm";
            this.Text = "Text Form";
            this.textFormMenuStrip.ResumeLayout(false);
            this.textFormMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox textRichTextBox;
        private System.Windows.Forms.MenuStrip textFormMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
    }
}