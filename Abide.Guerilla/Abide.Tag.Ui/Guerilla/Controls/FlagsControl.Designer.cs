namespace Abide.Tag.Ui.Guerilla.Controls
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
            this.components = new System.ComponentModel.Container();
            this.nameLabel = new System.Windows.Forms.Label();
            this.detailsLabel = new System.Windows.Forms.Label();
            this.informationToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.flagsListBox = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(11, 6);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(0, 13);
            this.nameLabel.TabIndex = 0;
            // 
            // detailsLabel
            // 
            this.detailsLabel.AutoSize = true;
            this.detailsLabel.Location = new System.Drawing.Point(405, 6);
            this.detailsLabel.Name = "detailsLabel";
            this.detailsLabel.Size = new System.Drawing.Size(0, 13);
            this.detailsLabel.TabIndex = 2;
            // 
            // informationToolTip
            // 
            this.informationToolTip.UseAnimation = false;
            this.informationToolTip.UseFading = false;
            // 
            // flagsListBox
            // 
            this.flagsListBox.FormattingEnabled = true;
            this.flagsListBox.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31"});
            this.flagsListBox.Location = new System.Drawing.Point(230, 3);
            this.flagsListBox.MaximumSize = new System.Drawing.Size(200, 139);
            this.flagsListBox.MinimumSize = new System.Drawing.Size(200, 4);
            this.flagsListBox.Name = "flagsListBox";
            this.flagsListBox.Size = new System.Drawing.Size(200, 139);
            this.flagsListBox.TabIndex = 4;
            this.flagsListBox.MouseHover += new System.EventHandler(this.flagsListBox_MouseHover);
            // 
            // FlagsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flagsListBox);
            this.Controls.Add(this.detailsLabel);
            this.Controls.Add(this.nameLabel);
            this.MinimumSize = new System.Drawing.Size(554, 0);
            this.Name = "FlagsControl";
            this.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.Size = new System.Drawing.Size(554, 145);
            this.Controls.SetChildIndex(this.nameLabel, 0);
            this.Controls.SetChildIndex(this.detailsLabel, 0);
            this.Controls.SetChildIndex(this.flagsListBox, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label detailsLabel;
        private System.Windows.Forms.ToolTip informationToolTip;
        private System.Windows.Forms.CheckedListBox flagsListBox;
    }
}
