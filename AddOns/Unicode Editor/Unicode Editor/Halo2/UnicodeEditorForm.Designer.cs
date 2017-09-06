namespace Unicode_Editor.Halo2
{
    partial class UnicodeEditorForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.stringList = new System.Windows.Forms.ListView();
            this.nameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.stringColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.stringTextBox = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.japaneseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.germanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frenchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spanishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.italianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.koreanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chineseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.portugueseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addStringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeStringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.stringList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.stringTextBox);
            this.splitContainer1.Panel2.Controls.Add(this.menuStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(716, 580);
            this.splitContainer1.SplitterDistance = 335;
            this.splitContainer1.TabIndex = 0;
            // 
            // stringList
            // 
            this.stringList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumn,
            this.stringColumn});
            this.stringList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stringList.FullRowSelect = true;
            this.stringList.GridLines = true;
            this.stringList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.stringList.Location = new System.Drawing.Point(0, 0);
            this.stringList.MultiSelect = false;
            this.stringList.Name = "stringList";
            this.stringList.Size = new System.Drawing.Size(716, 335);
            this.stringList.TabIndex = 0;
            this.stringList.UseCompatibleStateImageBehavior = false;
            this.stringList.View = System.Windows.Forms.View.Details;
            this.stringList.SelectedIndexChanged += new System.EventHandler(this.stringList_SelectedIndexChanged);
            // 
            // nameColumn
            // 
            this.nameColumn.Text = "Name";
            this.nameColumn.Width = 250;
            // 
            // stringColumn
            // 
            this.stringColumn.Text = "String";
            this.stringColumn.Width = 460;
            // 
            // stringTextBox
            // 
            this.stringTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stringTextBox.Location = new System.Drawing.Point(0, 0);
            this.stringTextBox.Name = "stringTextBox";
            this.stringTextBox.Size = new System.Drawing.Size(716, 217);
            this.stringTextBox.TabIndex = 1;
            this.stringTextBox.Text = "";
            this.stringTextBox.TextChanged += new System.EventHandler(this.stringTextBox_TextChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.languageToolStripMenuItem,
            this.addStringToolStripMenuItem,
            this.removeStringToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 217);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(716, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.englishToolStripMenuItem,
            this.japaneseToolStripMenuItem,
            this.germanToolStripMenuItem,
            this.frenchToolStripMenuItem,
            this.spanishToolStripMenuItem,
            this.italianToolStripMenuItem,
            this.koreanToolStripMenuItem,
            this.chineseToolStripMenuItem,
            this.portugueseToolStripMenuItem});
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            this.languageToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.languageToolStripMenuItem.Text = "&Language";
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            this.englishToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D1)));
            this.englishToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.englishToolStripMenuItem.Text = "&English";
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.languageSelectionToolStripMenuItem_Click);
            // 
            // japaneseToolStripMenuItem
            // 
            this.japaneseToolStripMenuItem.Name = "japaneseToolStripMenuItem";
            this.japaneseToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D2)));
            this.japaneseToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.japaneseToolStripMenuItem.Text = "&Japanese";
            this.japaneseToolStripMenuItem.Click += new System.EventHandler(this.languageSelectionToolStripMenuItem_Click);
            // 
            // germanToolStripMenuItem
            // 
            this.germanToolStripMenuItem.Name = "germanToolStripMenuItem";
            this.germanToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D3)));
            this.germanToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.germanToolStripMenuItem.Text = "&German";
            this.germanToolStripMenuItem.Click += new System.EventHandler(this.languageSelectionToolStripMenuItem_Click);
            // 
            // frenchToolStripMenuItem
            // 
            this.frenchToolStripMenuItem.Name = "frenchToolStripMenuItem";
            this.frenchToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D4)));
            this.frenchToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.frenchToolStripMenuItem.Text = "&French";
            this.frenchToolStripMenuItem.Click += new System.EventHandler(this.languageSelectionToolStripMenuItem_Click);
            // 
            // spanishToolStripMenuItem
            // 
            this.spanishToolStripMenuItem.Name = "spanishToolStripMenuItem";
            this.spanishToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D5)));
            this.spanishToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.spanishToolStripMenuItem.Text = "&Spanish";
            this.spanishToolStripMenuItem.Click += new System.EventHandler(this.languageSelectionToolStripMenuItem_Click);
            // 
            // italianToolStripMenuItem
            // 
            this.italianToolStripMenuItem.Name = "italianToolStripMenuItem";
            this.italianToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D6)));
            this.italianToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.italianToolStripMenuItem.Text = "&Italian";
            this.italianToolStripMenuItem.Click += new System.EventHandler(this.languageSelectionToolStripMenuItem_Click);
            // 
            // koreanToolStripMenuItem
            // 
            this.koreanToolStripMenuItem.Name = "koreanToolStripMenuItem";
            this.koreanToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D7)));
            this.koreanToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.koreanToolStripMenuItem.Text = "&Korean";
            this.koreanToolStripMenuItem.Click += new System.EventHandler(this.languageSelectionToolStripMenuItem_Click);
            // 
            // chineseToolStripMenuItem
            // 
            this.chineseToolStripMenuItem.Name = "chineseToolStripMenuItem";
            this.chineseToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D8)));
            this.chineseToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.chineseToolStripMenuItem.Text = "&Chinese";
            this.chineseToolStripMenuItem.Click += new System.EventHandler(this.languageSelectionToolStripMenuItem_Click);
            // 
            // portugueseToolStripMenuItem
            // 
            this.portugueseToolStripMenuItem.Name = "portugueseToolStripMenuItem";
            this.portugueseToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D9)));
            this.portugueseToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.portugueseToolStripMenuItem.Text = "&Portuguese";
            this.portugueseToolStripMenuItem.Click += new System.EventHandler(this.languageSelectionToolStripMenuItem_Click);
            // 
            // addStringToolStripMenuItem
            // 
            this.addStringToolStripMenuItem.Name = "addStringToolStripMenuItem";
            this.addStringToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.addStringToolStripMenuItem.Text = "&Add String";
            this.addStringToolStripMenuItem.Click += new System.EventHandler(this.addStringToolStripMenuItem_Click);
            // 
            // removeStringToolStripMenuItem
            // 
            this.removeStringToolStripMenuItem.Name = "removeStringToolStripMenuItem";
            this.removeStringToolStripMenuItem.Size = new System.Drawing.Size(96, 20);
            this.removeStringToolStripMenuItem.Text = "&Remove String";
            this.removeStringToolStripMenuItem.Click += new System.EventHandler(this.removeStringToolStripMenuItem_Click);
            // 
            // UnicodeEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 580);
            this.Controls.Add(this.splitContainer1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "UnicodeEditorForm";
            this.Text = "Unicode Editor";
            this.Load += new System.EventHandler(this.UnicodeEditorForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView stringList;
        private System.Windows.Forms.ColumnHeader nameColumn;
        private System.Windows.Forms.ColumnHeader stringColumn;
        private System.Windows.Forms.RichTextBox stringTextBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem japaneseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem germanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frenchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spanishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem italianToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem koreanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chineseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem portugueseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addStringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeStringToolStripMenuItem;
    }
}