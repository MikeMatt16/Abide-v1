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
            this.deleteButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.openAddOnsFolderButton = new System.Windows.Forms.Button();
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
            this.addOnListView.Size = new System.Drawing.Size(654, 351);
            this.addOnListView.TabIndex = 0;
            this.addOnListView.UseCompatibleStateImageBehavior = false;
            this.addOnListView.View = System.Windows.Forms.View.Details;
            this.addOnListView.SelectedIndexChanged += new System.EventHandler(this.addOnListView_SelectedIndexChanged);
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
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deleteButton.Enabled = false;
            this.deleteButton.Location = new System.Drawing.Point(12, 369);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 1;
            this.deleteButton.Text = "&Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(591, 369);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 2;
            this.closeButton.Text = "&Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // openAddOnsFolderButton
            // 
            this.openAddOnsFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.openAddOnsFolderButton.Location = new System.Drawing.Point(93, 369);
            this.openAddOnsFolderButton.Name = "openAddOnsFolderButton";
            this.openAddOnsFolderButton.Size = new System.Drawing.Size(120, 23);
            this.openAddOnsFolderButton.TabIndex = 3;
            this.openAddOnsFolderButton.Text = "Open &AddOns Folder";
            this.openAddOnsFolderButton.UseVisualStyleBackColor = true;
            this.openAddOnsFolderButton.Click += new System.EventHandler(this.openAddOnsFolderButton_Click);
            // 
            // AddOnManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(678, 404);
            this.Controls.Add(this.openAddOnsFolderButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.deleteButton);
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
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button openAddOnsFolderButton;
    }
}