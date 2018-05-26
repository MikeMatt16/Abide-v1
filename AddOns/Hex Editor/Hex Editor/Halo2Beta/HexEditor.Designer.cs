namespace Hex_Editor.Halo2Beta
{
    partial class HexEditor
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
            this.tagDataHexControl = new Hex_Editor.HexControl();
            this.SuspendLayout();
            // 
            // tagDataHexControl
            // 
            this.tagDataHexControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tagDataHexControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tagDataHexControl.Data = new byte[0];
            this.tagDataHexControl.GroupLength = 2;
            this.tagDataHexControl.Location = new System.Drawing.Point(4, 4);
            this.tagDataHexControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tagDataHexControl.Name = "tagDataHexControl";
            this.tagDataHexControl.OffsetLength = ((byte)(5));
            this.tagDataHexControl.Size = new System.Drawing.Size(1494, 991);
            this.tagDataHexControl.TabIndex = 0;
            // 
            // HexEditor
            // 
            this.Author = "Click16";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tagDataHexControl);
            this.Description = "Hex editor for tags.";
            this.Name = "HexEditor";
            this.Size = new System.Drawing.Size(1502, 999);
            this.ToolName = "Hex Editor";
            this.SelectedEntryChanged += new System.EventHandler(this.HexEditor_SelectedEntryChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private HexControl tagDataHexControl;
    }
}
