
namespace GK_Lab2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ParallelApproximationTrackBar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.ParallelsCountTrackBar = new System.Windows.Forms.TrackBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ParallelApproximationTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ParallelsCountTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(670, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(322, 668);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.ParallelApproximationTrackBar);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ParallelsCountTrackBar);
            this.groupBox1.Location = new System.Drawing.Point(19, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(288, 138);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Triangulation Accuracy";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(231, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Parallel Approximation Vertices Count: 100";
            // 
            // ParallelApproximationTrackBar
            // 
            this.ParallelApproximationTrackBar.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ParallelApproximationTrackBar.Location = new System.Drawing.Point(6, 73);
            this.ParallelApproximationTrackBar.Maximum = 200;
            this.ParallelApproximationTrackBar.Name = "ParallelApproximationTrackBar";
            this.ParallelApproximationTrackBar.Size = new System.Drawing.Size(276, 45);
            this.ParallelApproximationTrackBar.TabIndex = 2;
            this.ParallelApproximationTrackBar.TickFrequency = 10;
            this.ParallelApproximationTrackBar.Value = 100;
            this.ParallelApproximationTrackBar.ValueChanged += new System.EventHandler(this.ParallelApproximationTrackBar_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Parallels Count: 100";
            // 
            // ParallelsCountTrackBar
            // 
            this.ParallelsCountTrackBar.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ParallelsCountTrackBar.Location = new System.Drawing.Point(6, 22);
            this.ParallelsCountTrackBar.Maximum = 200;
            this.ParallelsCountTrackBar.Name = "ParallelsCountTrackBar";
            this.ParallelsCountTrackBar.Size = new System.Drawing.Size(276, 45);
            this.ParallelsCountTrackBar.TabIndex = 0;
            this.ParallelsCountTrackBar.TickFrequency = 10;
            this.ParallelsCountTrackBar.Value = 100;
            this.ParallelsCountTrackBar.ValueChanged += new System.EventHandler(this.ParallelsCountTrackBar_ValueChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, -1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(673, 668);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 660);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GK Projekt 2";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ParallelApproximationTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ParallelsCountTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TrackBar ParallelsCountTrackBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar ParallelApproximationTrackBar;
    }
}

