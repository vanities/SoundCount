namespace MeasSpeech
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Record_btn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.DataLog_btn = new System.Windows.Forms.Button();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.PlayBack_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ProcessAudio_btn = new System.Windows.Forms.Button();
            this.Stop_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Record_btn
            // 
            this.Record_btn.Location = new System.Drawing.Point(59, 157);
            this.Record_btn.Name = "Record_btn";
            this.Record_btn.Size = new System.Drawing.Size(68, 47);
            this.Record_btn.TabIndex = 0;
            this.Record_btn.Text = "Record";
            this.Record_btn.UseVisualStyleBackColor = true;
            this.Record_btn.Click += new System.EventHandler(this.Record_btn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(422, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 60);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // DataLog_btn
            // 
            this.DataLog_btn.Location = new System.Drawing.Point(392, 340);
            this.DataLog_btn.Name = "DataLog_btn";
            this.DataLog_btn.Size = new System.Drawing.Size(90, 23);
            this.DataLog_btn.TabIndex = 3;
            this.DataLog_btn.Text = "View Data Log";
            this.DataLog_btn.UseVisualStyleBackColor = true;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "Word Count",
            "Speakers",
            "Speech-To-Text"});
            this.checkedListBox1.Location = new System.Drawing.Point(362, 157);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(120, 49);
            this.checkedListBox1.TabIndex = 5;
            // 
            // PlayBack_btn
            // 
            this.PlayBack_btn.Location = new System.Drawing.Point(59, 264);
            this.PlayBack_btn.Name = "PlayBack_btn";
            this.PlayBack_btn.Size = new System.Drawing.Size(68, 28);
            this.PlayBack_btn.TabIndex = 6;
            this.PlayBack_btn.Text = "Playback";
            this.PlayBack_btn.UseVisualStyleBackColor = true;
            this.PlayBack_btn.Click += new System.EventHandler(this.PlayBack_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(359, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Select Data to view:";
            // 
            // ProcessAudio_btn
            // 
            this.ProcessAudio_btn.Location = new System.Drawing.Point(288, 159);
            this.ProcessAudio_btn.Name = "ProcessAudio_btn";
            this.ProcessAudio_btn.Size = new System.Drawing.Size(68, 47);
            this.ProcessAudio_btn.TabIndex = 8;
            this.ProcessAudio_btn.Text = "Process Audio";
            this.ProcessAudio_btn.UseVisualStyleBackColor = true;
            this.ProcessAudio_btn.Click += new System.EventHandler(this.ProcessAudio_btn_Click);
            // 
            // Stop_btn
            // 
            this.Stop_btn.Location = new System.Drawing.Point(133, 157);
            this.Stop_btn.Name = "Stop_btn";
            this.Stop_btn.Size = new System.Drawing.Size(68, 47);
            this.Stop_btn.TabIndex = 9;
            this.Stop_btn.Text = "Stop";
            this.Stop_btn.UseVisualStyleBackColor = true;
            this.Stop_btn.Click += new System.EventHandler(this.Stop_btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(494, 375);
            this.Controls.Add(this.Stop_btn);
            this.Controls.Add(this.ProcessAudio_btn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PlayBack_btn);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.DataLog_btn);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Record_btn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "SoundCount";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Record_btn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button DataLog_btn;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Button PlayBack_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ProcessAudio_btn;
        private System.Windows.Forms.Button Stop_btn;
    }
}

