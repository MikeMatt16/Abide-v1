namespace Abide.Decompiler
{
    partial class TagDecompiler
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TagDecompiler));
            this.decompileButton = new System.Windows.Forms.Button();
            this.tagTreeView = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // decompileButton
            // 
            this.decompileButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.decompileButton.Enabled = false;
            this.decompileButton.Location = new System.Drawing.Point(12, 526);
            this.decompileButton.Name = "decompileButton";
            this.decompileButton.Size = new System.Drawing.Size(460, 23);
            this.decompileButton.TabIndex = 4;
            this.decompileButton.Text = "&Decompile";
            this.decompileButton.UseVisualStyleBackColor = true;
            // 
            // tagTreeView
            // 
            this.tagTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tagTreeView.CheckBoxes = true;
            this.tagTreeView.HideSelection = false;
            this.tagTreeView.ImageIndex = 0;
            this.tagTreeView.ImageList = this.imageList1;
            this.tagTreeView.Location = new System.Drawing.Point(12, 25);
            this.tagTreeView.Name = "tagTreeView";
            this.tagTreeView.SelectedImageIndex = 0;
            this.tagTreeView.Size = new System.Drawing.Size(460, 495);
            this.tagTreeView.TabIndex = 5;
            this.tagTreeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tagTreeView_AfterCheck);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "folder-16.png");
            this.imageList1.Images.SetKeyName(1, "tag-16.png");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Select the tags you wish to decompile";
            // 
            // TagDecompiler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 561);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tagTreeView);
            this.Controls.Add(this.decompileButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "TagDecompiler";
            this.Text = "Tag Decompiler";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button decompileButton;
        private System.Windows.Forms.TreeView tagTreeView;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label1;
    }
}