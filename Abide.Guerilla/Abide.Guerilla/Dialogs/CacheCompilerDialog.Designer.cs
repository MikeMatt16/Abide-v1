namespace Abide.Guerilla.Dialogs
{
    partial class CacheCompilerDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.scenarioFileNameTextBox = new System.Windows.Forms.TextBox();
            this.browseScenarioButton = new System.Windows.Forms.Button();
            this.compileButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "&Scenario Path:";
            // 
            // scenarioFileNameTextBox
            // 
            this.scenarioFileNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scenarioFileNameTextBox.Location = new System.Drawing.Point(95, 14);
            this.scenarioFileNameTextBox.Name = "scenarioFileNameTextBox";
            this.scenarioFileNameTextBox.ReadOnly = true;
            this.scenarioFileNameTextBox.Size = new System.Drawing.Size(290, 20);
            this.scenarioFileNameTextBox.TabIndex = 1;
            this.scenarioFileNameTextBox.TextChanged += new System.EventHandler(this.scenarioFileNameTextBox_TextChanged);
            // 
            // browseScenarioButton
            // 
            this.browseScenarioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseScenarioButton.Location = new System.Drawing.Point(391, 12);
            this.browseScenarioButton.Name = "browseScenarioButton";
            this.browseScenarioButton.Size = new System.Drawing.Size(30, 23);
            this.browseScenarioButton.TabIndex = 2;
            this.browseScenarioButton.Text = "...";
            this.browseScenarioButton.UseVisualStyleBackColor = true;
            this.browseScenarioButton.Click += new System.EventHandler(this.browseScenarioButton_Click);
            // 
            // compileButton
            // 
            this.compileButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.compileButton.Enabled = false;
            this.compileButton.Location = new System.Drawing.Point(12, 66);
            this.compileButton.Name = "compileButton";
            this.compileButton.Size = new System.Drawing.Size(410, 23);
            this.compileButton.TabIndex = 3;
            this.compileButton.Text = "&Compile";
            this.compileButton.UseVisualStyleBackColor = true;
            this.compileButton.Click += new System.EventHandler(this.compileButton_Click);
            // 
            // CacheCompilerDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 101);
            this.Controls.Add(this.compileButton);
            this.Controls.Add(this.browseScenarioButton);
            this.Controls.Add(this.scenarioFileNameTextBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = global::Abide.Guerilla.Properties.Resources.abide_icon;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CacheCompilerDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Map Compiler";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox scenarioFileNameTextBox;
        private System.Windows.Forms.Button browseScenarioButton;
        private System.Windows.Forms.Button compileButton;
    }
}