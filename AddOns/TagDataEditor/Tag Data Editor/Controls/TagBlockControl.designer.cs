namespace Tag_Data_Editor.Controls
{
    partial class TagBlockControl
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
            this.nestedPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.nameLabel = new System.Windows.Forms.Label();
            this.indexSelectBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // nestedPanel
            // 
            this.nestedPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nestedPanel.AutoSize = true;
            this.nestedPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.nestedPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.nestedPanel.Enabled = false;
            this.nestedPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.nestedPanel.Location = new System.Drawing.Point(3, 30);
            this.nestedPanel.Name = "nestedPanel";
            this.nestedPanel.Size = new System.Drawing.Size(4, 4);
            this.nestedPanel.TabIndex = 0;
            this.nestedPanel.WrapContents = false;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(178, 6);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(59, 13);
            this.nameLabel.TabIndex = 8;
            this.nameLabel.Text = "nameLabel";
            // 
            // indexSelectBox
            // 
            this.indexSelectBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.indexSelectBox.Enabled = false;
            this.indexSelectBox.FormattingEnabled = true;
            this.indexSelectBox.Location = new System.Drawing.Point(3, 3);
            this.indexSelectBox.Name = "indexSelectBox";
            this.indexSelectBox.Size = new System.Drawing.Size(169, 21);
            this.indexSelectBox.TabIndex = 7;
            this.indexSelectBox.SelectedIndexChanged += new System.EventHandler(this.indexSelectBox_SelectedIndexChanged);
            // 
            // TagBlockControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.indexSelectBox);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.nestedPanel);
            this.Name = "TagBlockControl";
            this.Size = new System.Drawing.Size(240, 37);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel nestedPanel;
        private System.Windows.Forms.ComboBox indexSelectBox;
        private System.Windows.Forms.Label nameLabel;
    }
}
