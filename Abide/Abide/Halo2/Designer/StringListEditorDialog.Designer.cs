namespace Abide.Halo2.Designer
{
    partial class StringListEditorDialog
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
            this.stringListBox = new System.Windows.Forms.ListBox();
            this.closeButton = new System.Windows.Forms.Button();
            this.stringListMenuStrip = new System.Windows.Forms.MenuStrip();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stringTextBox = new System.Windows.Forms.TextBox();
            this.stringListMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // stringListBox
            // 
            this.stringListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stringListBox.FormattingEnabled = true;
            this.stringListBox.IntegralHeight = false;
            this.stringListBox.Location = new System.Drawing.Point(12, 27);
            this.stringListBox.Name = "stringListBox";
            this.stringListBox.Size = new System.Drawing.Size(260, 367);
            this.stringListBox.TabIndex = 0;
            this.stringListBox.SelectedIndexChanged += new System.EventHandler(this.stringListBox_SelectedIndexChanged);
            // 
            // closeButton
            // 
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.closeButton.Location = new System.Drawing.Point(12, 426);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 1;
            this.closeButton.Text = "&Close";
            this.closeButton.UseVisualStyleBackColor = true;
            // 
            // stringListMenuStrip
            // 
            this.stringListMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.removeToolStripMenuItem});
            this.stringListMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.stringListMenuStrip.Name = "stringListMenuStrip";
            this.stringListMenuStrip.Size = new System.Drawing.Size(284, 24);
            this.stringListMenuStrip.TabIndex = 3;
            this.stringListMenuStrip.Text = "menuStrip1";
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.addToolStripMenuItem.Text = "&Add";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Enabled = false;
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.removeToolStripMenuItem.Text = "&Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // stringTextBox
            // 
            this.stringTextBox.Enabled = false;
            this.stringTextBox.Location = new System.Drawing.Point(12, 400);
            this.stringTextBox.Name = "stringTextBox";
            this.stringTextBox.Size = new System.Drawing.Size(260, 20);
            this.stringTextBox.TabIndex = 4;
            this.stringTextBox.TextChanged += new System.EventHandler(this.stringTextBox_TextChanged);
            // 
            // StringListEditorDialog
            // 
            this.AcceptButton = this.closeButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(284, 461);
            this.Controls.Add(this.stringTextBox);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.stringListBox);
            this.Controls.Add(this.stringListMenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.stringListMenuStrip;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StringListEditorDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit String List";
            this.stringListMenuStrip.ResumeLayout(false);
            this.stringListMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox stringListBox;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.MenuStrip stringListMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.TextBox stringTextBox;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
    }
}