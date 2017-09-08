namespace HUD_Editor.Forms
{
    partial class HudBitmapWidgetEffectEditor
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
            this.closeButton = new System.Windows.Forms.Button();
            this.effectComboBox = new System.Windows.Forms.ComboBox();
            this.effectProperties = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.closeButton.Location = new System.Drawing.Point(221, 359);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 0;
            this.closeButton.Text = "&OK";
            this.closeButton.UseVisualStyleBackColor = true;
            // 
            // effectComboBox
            // 
            this.effectComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.effectComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.effectComboBox.FormattingEnabled = true;
            this.effectComboBox.Location = new System.Drawing.Point(12, 12);
            this.effectComboBox.Name = "effectComboBox";
            this.effectComboBox.Size = new System.Drawing.Size(284, 21);
            this.effectComboBox.TabIndex = 5;
            this.effectComboBox.SelectedIndexChanged += new System.EventHandler(this.effectComboBox_SelectedIndexChanged);
            // 
            // effectProperties
            // 
            this.effectProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.effectProperties.HelpVisible = false;
            this.effectProperties.LineColor = System.Drawing.SystemColors.ControlDark;
            this.effectProperties.Location = new System.Drawing.Point(12, 39);
            this.effectProperties.Name = "effectProperties";
            this.effectProperties.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.effectProperties.Size = new System.Drawing.Size(284, 314);
            this.effectProperties.TabIndex = 6;
            this.effectProperties.ToolbarVisible = false;
            // 
            // HudBitmapWidgetEffectEditor
            // 
            this.AcceptButton = this.closeButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 394);
            this.Controls.Add(this.effectProperties);
            this.Controls.Add(this.effectComboBox);
            this.Controls.Add(this.closeButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HudBitmapWidgetEffectEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Effect Editor";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.ComboBox effectComboBox;
        private System.Windows.Forms.PropertyGrid effectProperties;
    }
}