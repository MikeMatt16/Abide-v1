namespace HUD_Editor.Editors
{
    partial class FunctionEditorDialog
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
            this.elementCountLabel = new System.Windows.Forms.Label();
            this.dataTypeComboBox = new System.Windows.Forms.ComboBox();
            this.dataTypeLabel = new System.Windows.Forms.Label();
            this.containerPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // elementCountLabel
            // 
            this.elementCountLabel.AutoSize = true;
            this.elementCountLabel.Location = new System.Drawing.Point(12, 36);
            this.elementCountLabel.Name = "elementCountLabel";
            this.elementCountLabel.Size = new System.Drawing.Size(98, 13);
            this.elementCountLabel.TabIndex = 0;
            this.elementCountLabel.Text = "elementCountLabel";
            // 
            // dataTypeComboBox
            // 
            this.dataTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dataTypeComboBox.FormattingEnabled = true;
            this.dataTypeComboBox.Location = new System.Drawing.Point(78, 12);
            this.dataTypeComboBox.Name = "dataTypeComboBox";
            this.dataTypeComboBox.Size = new System.Drawing.Size(374, 21);
            this.dataTypeComboBox.TabIndex = 0;
            this.dataTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.dataTypeComboBox_SelectedIndexChanged);
            // 
            // dataTypeLabel
            // 
            this.dataTypeLabel.AutoSize = true;
            this.dataTypeLabel.Location = new System.Drawing.Point(12, 15);
            this.dataTypeLabel.Name = "dataTypeLabel";
            this.dataTypeLabel.Size = new System.Drawing.Size(60, 13);
            this.dataTypeLabel.TabIndex = 2;
            this.dataTypeLabel.Text = "&Data Type:";
            // 
            // containerPanel
            // 
            this.containerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.containerPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.containerPanel.Location = new System.Drawing.Point(12, 52);
            this.containerPanel.AutoScroll = true;
            this.containerPanel.Name = "containerPanel";
            this.containerPanel.Size = new System.Drawing.Size(440, 257);
            this.containerPanel.TabIndex = 1;
            this.containerPanel.WrapContents = false;
            // 
            // FunctionEditorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 321);
            this.Controls.Add(this.containerPanel);
            this.Controls.Add(this.dataTypeLabel);
            this.Controls.Add(this.dataTypeComboBox);
            this.Controls.Add(this.elementCountLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FunctionEditorDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Function Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label elementCountLabel;
        private System.Windows.Forms.ComboBox dataTypeComboBox;
        private System.Windows.Forms.Label dataTypeLabel;
        private System.Windows.Forms.FlowLayoutPanel containerPanel;
    }
}