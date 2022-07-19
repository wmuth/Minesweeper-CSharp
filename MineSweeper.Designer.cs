namespace MineSweeper
{
    partial class MineSweeper
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MineSweeper));
            this.button1 = new System.Windows.Forms.Button();
            this.FlagCount = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.playtimeTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.Location = new System.Drawing.Point(295, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(44, 42);
            this.button1.TabIndex = 0;
            this.button1.Tag = "restart";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FlagCount
            // 
            this.FlagCount.AutoSize = true;
            this.FlagCount.Location = new System.Drawing.Point(255, 27);
            this.FlagCount.Name = "FlagCount";
            this.FlagCount.Size = new System.Drawing.Size(25, 13);
            this.FlagCount.TabIndex = 1;
            this.FlagCount.Text = "000";
            // 
            // timer
            // 
            this.timer.AutoSize = true;
            this.timer.Location = new System.Drawing.Point(345, 27);
            this.timer.Name = "timer";
            this.timer.Size = new System.Drawing.Size(34, 13);
            this.timer.TabIndex = 2;
            this.timer.Text = "00:00";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.richTextBox1.Location = new System.Drawing.Point(1, 1);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(633, 70);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(214, 23);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(35, 20);
            this.button2.TabIndex = 5;
            this.button2.Tag = "hax";
            this.button2.Text = "hax";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // playtimeTimer
            // 
            this.playtimeTimer.Interval = 1000;
            this.playtimeTimer.Tick += new System.EventHandler(this.playtimeTimer_Tick);
            // 
            // MineSweeper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(634, 511);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.timer);
            this.Controls.Add(this.FlagCount);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richTextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MineSweeper";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MineSweeper";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label FlagCount;
        private System.Windows.Forms.Label timer;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer playtimeTimer;
    }
}

