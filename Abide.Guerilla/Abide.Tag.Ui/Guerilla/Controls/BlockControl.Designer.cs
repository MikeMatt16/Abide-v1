namespace Abide.Tag.Ui.Guerilla.Controls
{
    partial class BlockControl
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
            this.expandCollapseButton = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label();
            this.blockSelectComboBox = new System.Windows.Forms.ComboBox();
            this.controlsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // expandCollapseButton
            // 
            this.expandCollapseButton.BackColor = System.Drawing.SystemColors.Control;
            this.expandCollapseButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.expandCollapseButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.expandCollapseButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.expandCollapseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.expandCollapseButton.Location = new System.Drawing.Point(6, 6);
            this.expandCollapseButton.Name = "expandCollapseButton";
            this.expandCollapseButton.Size = new System.Drawing.Size(23, 23);
            this.expandCollapseButton.TabIndex = 0;
            this.expandCollapseButton.Text = "+";
            this.expandCollapseButton.UseVisualStyleBackColor = false;
            this.expandCollapseButton.Click += new System.EventHandler(this.expandCollapseButton_Click);
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.Location = new System.Drawing.Point(35, 6);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(39, 16);
            this.titleLabel.TabIndex = 1;
            this.titleLabel.Text = "Title";
            // 
            // blockSelectComboBox
            // 
            this.blockSelectComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.blockSelectComboBox.FormattingEnabled = true;
            this.blockSelectComboBox.Items.AddRange(new object[] {
            "None"});
            this.blockSelectComboBox.Location = new System.Drawing.Point(75, 25);
            this.blockSelectComboBox.Name = "blockSelectComboBox";
            this.blockSelectComboBox.Size = new System.Drawing.Size(471, 21);
            this.blockSelectComboBox.TabIndex = 2;
            this.blockSelectComboBox.SelectedIndexChanged += new System.EventHandler(this.blockSelectComboBox_SelectedIndexChanged);
            // 
            // controlsPanel
            // 
            this.controlsPanel.AutoSize = true;
            this.controlsPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.controlsPanel.BackColor = System.Drawing.SystemColors.Control;
            this.controlsPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.controlsPanel.Location = new System.Drawing.Point(6, 52);
            this.controlsPanel.MinimumSize = new System.Drawing.Size(554, 0);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(554, 0);
            this.controlsPanel.TabIndex = 3;
            this.controlsPanel.WrapContents = false;
            // 
            // BlockControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.controlsPanel);
            this.Controls.Add(this.blockSelectComboBox);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.expandCollapseButton);
            this.MinimumSize = new System.Drawing.Size(554, 2);
            this.Name = "BlockControl";
            this.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.Size = new System.Drawing.Size(566, 55);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button expandCollapseButton;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.ComboBox blockSelectComboBox;
        private System.Windows.Forms.FlowLayoutPanel controlsPanel;
    }
}
