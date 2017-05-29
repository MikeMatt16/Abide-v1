namespace Abide.Forms
{
    partial class AddOnManager
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
            this.addOnListView = new System.Windows.Forms.ListView();
            this.nameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.typesHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.directoryHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // addOnListView
            // 
            this.addOnListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addOnListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameHeader,
            this.typesHeader,
            this.directoryHeader});
            this.addOnListView.FullRowSelect = true;
            this.addOnListView.GridLines = true;
            this.addOnListView.Location = new System.Drawing.Point(12, 12);
            this.addOnListView.Name = "addOnListView";
            this.addOnListView.Size = new System.Drawing.Size(654, 346);
            this.addOnListView.TabIndex = 0;
            this.addOnListView.UseCompatibleStateImageBehavior = false;
            this.addOnListView.View = System.Windows.Forms.View.Details;
            // 
            // nameHeader
            // 
            this.nameHeader.Text = "Name";
            this.nameHeader.Width = 240;
            // 
            // typesHeader
            // 
            this.typesHeader.Text = "Types";
            this.typesHeader.Width = 150;
            // 
            // directoryHeader
            // 
            this.directoryHeader.Text = "Directory";
            this.directoryHeader.Width = 260;
            // 
            // AddOnManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 404);
            this.Controls.Add(this.addOnListView);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddOnManager";
            this.ShowIcon = false;
            this.Text = "AddOnManager";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView addOnListView;
        private System.Windows.Forms.ColumnHeader nameHeader;
        private System.Windows.Forms.ColumnHeader typesHeader;
        private System.Windows.Forms.ColumnHeader directoryHeader;
    }
}