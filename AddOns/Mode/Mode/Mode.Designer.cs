namespace Mode
{
    partial class Mode
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
            this.alphaBox = new System.Windows.Forms.PictureBox();
            this.betaBox = new System.Windows.Forms.PictureBox();
            this.gammaBox = new System.Windows.Forms.PictureBox();
            this.magnitudeTrackBar = new System.Windows.Forms.TrackBar();
            this.alphaTrackBar = new System.Windows.Forms.TrackBar();
            this.betaTrackBar = new System.Windows.Forms.TrackBar();
            this.gammaTrackBar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.vectorLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.alphaBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.betaBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gammaBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.magnitudeTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.alphaTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.betaTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gammaTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // alphaBox
            // 
            this.alphaBox.BackColor = System.Drawing.Color.CornflowerBlue;
            this.alphaBox.Location = new System.Drawing.Point(3, 3);
            this.alphaBox.Name = "alphaBox";
            this.alphaBox.Size = new System.Drawing.Size(150, 150);
            this.alphaBox.TabIndex = 0;
            this.alphaBox.TabStop = false;
            // 
            // betaBox
            // 
            this.betaBox.BackColor = System.Drawing.Color.CornflowerBlue;
            this.betaBox.Location = new System.Drawing.Point(159, 3);
            this.betaBox.Name = "betaBox";
            this.betaBox.Size = new System.Drawing.Size(150, 150);
            this.betaBox.TabIndex = 0;
            this.betaBox.TabStop = false;
            // 
            // gammaBox
            // 
            this.gammaBox.BackColor = System.Drawing.Color.CornflowerBlue;
            this.gammaBox.Location = new System.Drawing.Point(315, 3);
            this.gammaBox.Name = "gammaBox";
            this.gammaBox.Size = new System.Drawing.Size(150, 150);
            this.gammaBox.TabIndex = 0;
            this.gammaBox.TabStop = false;
            // 
            // magnitudeTrackBar
            // 
            this.magnitudeTrackBar.LargeChange = 10;
            this.magnitudeTrackBar.Location = new System.Drawing.Point(69, 159);
            this.magnitudeTrackBar.Maximum = 100;
            this.magnitudeTrackBar.Name = "magnitudeTrackBar";
            this.magnitudeTrackBar.Size = new System.Drawing.Size(396, 45);
            this.magnitudeTrackBar.TabIndex = 1;
            this.magnitudeTrackBar.TickFrequency = 5;
            this.magnitudeTrackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.magnitudeTrackBar.ValueChanged += new System.EventHandler(this.vector_ValueChanged);
            // 
            // alphaTrackBar
            // 
            this.alphaTrackBar.LargeChange = 10;
            this.alphaTrackBar.Location = new System.Drawing.Point(69, 210);
            this.alphaTrackBar.Maximum = 100;
            this.alphaTrackBar.Name = "alphaTrackBar";
            this.alphaTrackBar.Size = new System.Drawing.Size(396, 45);
            this.alphaTrackBar.TabIndex = 1;
            this.alphaTrackBar.TickFrequency = 5;
            this.alphaTrackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.alphaTrackBar.ValueChanged += new System.EventHandler(this.vector_ValueChanged);
            // 
            // betaTrackBar
            // 
            this.betaTrackBar.LargeChange = 10;
            this.betaTrackBar.Location = new System.Drawing.Point(69, 261);
            this.betaTrackBar.Maximum = 100;
            this.betaTrackBar.Name = "betaTrackBar";
            this.betaTrackBar.Size = new System.Drawing.Size(396, 45);
            this.betaTrackBar.TabIndex = 1;
            this.betaTrackBar.TickFrequency = 5;
            this.betaTrackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.betaTrackBar.ValueChanged += new System.EventHandler(this.vector_ValueChanged);
            // 
            // gammaTrackBar
            // 
            this.gammaTrackBar.LargeChange = 10;
            this.gammaTrackBar.Location = new System.Drawing.Point(69, 312);
            this.gammaTrackBar.Maximum = 100;
            this.gammaTrackBar.Name = "gammaTrackBar";
            this.gammaTrackBar.Size = new System.Drawing.Size(396, 45);
            this.gammaTrackBar.TabIndex = 1;
            this.gammaTrackBar.TickFrequency = 5;
            this.gammaTrackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.gammaTrackBar.ValueChanged += new System.EventHandler(this.vector_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 175);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Magnitude:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 226);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Alpha:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 277);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Beta:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 328);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Gamma:";
            // 
            // vectorLabel
            // 
            this.vectorLabel.AutoSize = true;
            this.vectorLabel.Location = new System.Drawing.Point(471, 3);
            this.vectorLabel.Name = "vectorLabel";
            this.vectorLabel.Size = new System.Drawing.Size(43, 13);
            this.vectorLabel.TabIndex = 3;
            this.vectorLabel.Text = "(0, 0, 0)";
            // 
            // Mode
            // 
            this.Author = "Click16";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.vectorLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gammaTrackBar);
            this.Controls.Add(this.betaTrackBar);
            this.Controls.Add(this.alphaTrackBar);
            this.Controls.Add(this.magnitudeTrackBar);
            this.Controls.Add(this.gammaBox);
            this.Controls.Add(this.betaBox);
            this.Controls.Add(this.alphaBox);
            this.Description = "Mode Model Editor";
            this.Icon = global::Mode.Properties.Resources.Mode;
            this.Name = "Mode";
            this.Size = new System.Drawing.Size(500, 500);
            this.ToolName = "Mode";
            this.SelectedEntryChanged += new System.EventHandler(this.Mode_SelectedEntryChanged);
            ((System.ComponentModel.ISupportInitialize)(this.alphaBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.betaBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gammaBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.magnitudeTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.alphaTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.betaTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gammaTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox alphaBox;
        private System.Windows.Forms.PictureBox betaBox;
        private System.Windows.Forms.PictureBox gammaBox;
        private System.Windows.Forms.TrackBar magnitudeTrackBar;
        private System.Windows.Forms.TrackBar alphaTrackBar;
        private System.Windows.Forms.TrackBar betaTrackBar;
        private System.Windows.Forms.TrackBar gammaTrackBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label vectorLabel;
    }
}
