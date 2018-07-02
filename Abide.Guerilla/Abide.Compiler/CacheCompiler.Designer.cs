namespace Abide.Compiler
{
    partial class CacheCompiler
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
            this.scenarioPathTextBox = new System.Windows.Forms.TextBox();
            this.browseScenarioButton = new System.Windows.Forms.Button();
            this.compileButton = new System.Windows.Forms.Button();
            this.compileProgressBar = new System.Windows.Forms.ProgressBar();
            this.compileLogRichTextBox = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.globalsPathTextBox = new System.Windows.Forms.TextBox();
            this.browseGlobalsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "&Scenario:";
            // 
            // scenarioPathTextBox
            // 
            this.scenarioPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scenarioPathTextBox.Location = new System.Drawing.Point(70, 15);
            this.scenarioPathTextBox.Name = "scenarioPathTextBox";
            this.scenarioPathTextBox.ReadOnly = true;
            this.scenarioPathTextBox.Size = new System.Drawing.Size(411, 20);
            this.scenarioPathTextBox.TabIndex = 1;
            this.scenarioPathTextBox.TextChanged += new System.EventHandler(this.scenarioPathTextBox_TextChanged);
            // 
            // browseScenarioButton
            // 
            this.browseScenarioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseScenarioButton.Location = new System.Drawing.Point(487, 13);
            this.browseScenarioButton.Name = "browseScenarioButton";
            this.browseScenarioButton.Size = new System.Drawing.Size(35, 23);
            this.browseScenarioButton.TabIndex = 2;
            this.browseScenarioButton.Text = "...";
            this.browseScenarioButton.UseVisualStyleBackColor = true;
            this.browseScenarioButton.Click += new System.EventHandler(this.browseScenarioButton_Click);
            // 
            // compileButton
            // 
            this.compileButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.compileButton.Enabled = false;
            this.compileButton.Location = new System.Drawing.Point(12, 71);
            this.compileButton.Name = "compileButton";
            this.compileButton.Size = new System.Drawing.Size(510, 23);
            this.compileButton.TabIndex = 6;
            this.compileButton.Text = "&Compile";
            this.compileButton.UseVisualStyleBackColor = true;
            this.compileButton.Click += new System.EventHandler(this.compileButton_Click);
            // 
            // compileProgressBar
            // 
            this.compileProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.compileProgressBar.Location = new System.Drawing.Point(12, 100);
            this.compileProgressBar.Name = "compileProgressBar";
            this.compileProgressBar.Size = new System.Drawing.Size(510, 23);
            this.compileProgressBar.TabIndex = 7;
            // 
            // compileLogRichTextBox
            // 
            this.compileLogRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.compileLogRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.compileLogRichTextBox.Location = new System.Drawing.Point(12, 129);
            this.compileLogRichTextBox.Name = "compileLogRichTextBox";
            this.compileLogRichTextBox.ReadOnly = true;
            this.compileLogRichTextBox.Size = new System.Drawing.Size(510, 220);
            this.compileLogRichTextBox.TabIndex = 8;
            this.compileLogRichTextBox.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "&Globals:";
            // 
            // globalsPathTextBox
            // 
            this.globalsPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.globalsPathTextBox.Location = new System.Drawing.Point(70, 44);
            this.globalsPathTextBox.Name = "globalsPathTextBox";
            this.globalsPathTextBox.ReadOnly = true;
            this.globalsPathTextBox.Size = new System.Drawing.Size(411, 20);
            this.globalsPathTextBox.TabIndex = 4;
            this.globalsPathTextBox.TextChanged += new System.EventHandler(this.globalsPathTextBox_TextChanged);
            // 
            // browseGlobalsButton
            // 
            this.browseGlobalsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseGlobalsButton.Location = new System.Drawing.Point(487, 42);
            this.browseGlobalsButton.Name = "browseGlobalsButton";
            this.browseGlobalsButton.Size = new System.Drawing.Size(35, 23);
            this.browseGlobalsButton.TabIndex = 5;
            this.browseGlobalsButton.Text = "...";
            this.browseGlobalsButton.UseVisualStyleBackColor = true;
            this.browseGlobalsButton.Click += new System.EventHandler(this.browseGlobalsButton_Click);
            // 
            // CacheCompiler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 361);
            this.Controls.Add(this.compileLogRichTextBox);
            this.Controls.Add(this.compileProgressBar);
            this.Controls.Add(this.compileButton);
            this.Controls.Add(this.browseGlobalsButton);
            this.Controls.Add(this.globalsPathTextBox);
            this.Controls.Add(this.browseScenarioButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.scenarioPathTextBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CacheCompiler";
            this.Text = "Compiler";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox scenarioPathTextBox;
        private System.Windows.Forms.Button browseScenarioButton;
        private System.Windows.Forms.Button compileButton;
        private System.Windows.Forms.ProgressBar compileProgressBar;
        private System.Windows.Forms.RichTextBox compileLogRichTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox globalsPathTextBox;
        private System.Windows.Forms.Button browseGlobalsButton;
    }
}

