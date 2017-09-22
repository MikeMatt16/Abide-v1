namespace HUD_Editor.Halo2
{
    partial class HudEditor
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
            this.hudBox = new System.Windows.Forms.PictureBox();
            this.propertiesPanel = new System.Windows.Forms.Panel();
            this.resetButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.widgetPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.widgetComboBox = new System.Windows.Forms.ComboBox();
            this.propertiesSplitter = new System.Windows.Forms.Splitter();
            ((System.ComponentModel.ISupportInitialize)(this.hudBox)).BeginInit();
            this.propertiesPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // hudBox
            // 
            this.hudBox.BackColor = System.Drawing.SystemColors.Control;
            this.hudBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hudBox.Location = new System.Drawing.Point(12, 12);
            this.hudBox.Name = "hudBox";
            this.hudBox.Size = new System.Drawing.Size(640, 480);
            this.hudBox.TabIndex = 0;
            this.hudBox.TabStop = false;
            this.hudBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.HudBox_MouseMove);
            // 
            // propertiesPanel
            // 
            this.propertiesPanel.BackColor = System.Drawing.SystemColors.Control;
            this.propertiesPanel.Controls.Add(this.resetButton);
            this.propertiesPanel.Controls.Add(this.saveButton);
            this.propertiesPanel.Controls.Add(this.widgetPropertyGrid);
            this.propertiesPanel.Controls.Add(this.widgetComboBox);
            this.propertiesPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.propertiesPanel.Location = new System.Drawing.Point(664, 0);
            this.propertiesPanel.Name = "propertiesPanel";
            this.propertiesPanel.Size = new System.Drawing.Size(344, 505);
            this.propertiesPanel.TabIndex = 2;
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(55, 12);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(49, 21);
            this.resetButton.TabIndex = 1;
            this.resetButton.Text = "&Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(9, 12);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(40, 21);
            this.saveButton.TabIndex = 0;
            this.saveButton.Text = "&Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // widgetPropertyGrid
            // 
            this.widgetPropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.widgetPropertyGrid.HelpVisible = false;
            this.widgetPropertyGrid.LineColor = System.Drawing.SystemColors.ControlDark;
            this.widgetPropertyGrid.Location = new System.Drawing.Point(9, 39);
            this.widgetPropertyGrid.Name = "widgetPropertyGrid";
            this.widgetPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.widgetPropertyGrid.Size = new System.Drawing.Size(323, 463);
            this.widgetPropertyGrid.TabIndex = 3;
            this.widgetPropertyGrid.ToolbarVisible = false;
            this.widgetPropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.WidgetPropertyGrid_PropertyValueChanged);
            // 
            // widgetComboBox
            // 
            this.widgetComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.widgetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.widgetComboBox.FormattingEnabled = true;
            this.widgetComboBox.Location = new System.Drawing.Point(110, 12);
            this.widgetComboBox.Name = "widgetComboBox";
            this.widgetComboBox.Size = new System.Drawing.Size(222, 21);
            this.widgetComboBox.TabIndex = 2;
            this.widgetComboBox.SelectedIndexChanged += new System.EventHandler(this.WidgetComboBox_SelectedIndexChanged);
            // 
            // propertiesSplitter
            // 
            this.propertiesSplitter.BackColor = System.Drawing.SystemColors.Control;
            this.propertiesSplitter.Dock = System.Windows.Forms.DockStyle.Right;
            this.propertiesSplitter.Location = new System.Drawing.Point(661, 0);
            this.propertiesSplitter.Name = "propertiesSplitter";
            this.propertiesSplitter.Size = new System.Drawing.Size(3, 505);
            this.propertiesSplitter.TabIndex = 3;
            this.propertiesSplitter.TabStop = false;
            // 
            // HudEditor
            // 
            this.Author = "Click16";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this.propertiesSplitter);
            this.Controls.Add(this.propertiesPanel);
            this.Controls.Add(this.hudBox);
            this.Description = "Graphical HUD editor";
            this.Icon = global::HUD_Editor.Properties.Resources.HUD_Editor;
            this.Name = "HudEditor";
            this.Size = new System.Drawing.Size(1008, 505);
            this.ToolName = "HUD Editor";
            this.SelectedEntryChanged += new System.EventHandler(this.HudEditor_TagSelected);
            this.Load += new System.EventHandler(this.HudEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.hudBox)).EndInit();
            this.propertiesPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox hudBox;
        private System.Windows.Forms.Panel propertiesPanel;
        private System.Windows.Forms.ComboBox widgetComboBox;
        private System.Windows.Forms.PropertyGrid widgetPropertyGrid;
        private System.Windows.Forms.Splitter propertiesSplitter;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Button saveButton;
    }
}
