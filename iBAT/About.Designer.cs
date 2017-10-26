namespace iBAT
{
    partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Label_Version = new System.Windows.Forms.Label();
            this.Label_Github = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.webBrowser_VersionHistory = new System.Windows.Forms.WebBrowser();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_Email = new System.Windows.Forms.TextBox();
            this.button_Copy = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(72, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "iBAT 批处理集成开发环境";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "版本：";
            // 
            // Label_Version
            // 
            this.Label_Version.AutoSize = true;
            this.Label_Version.Location = new System.Drawing.Point(60, 80);
            this.Label_Version.Name = "Label_Version";
            this.Label_Version.Size = new System.Drawing.Size(77, 12);
            this.Label_Version.TabIndex = 3;
            this.Label_Version.Text = "0.00.00.0000";
            // 
            // Label_Github
            // 
            this.Label_Github.AutoSize = true;
            this.Label_Github.Location = new System.Drawing.Point(83, 80);
            this.Label_Github.Name = "Label_Github";
            this.Label_Github.Size = new System.Drawing.Size(0, 12);
            this.Label_Github.TabIndex = 5;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(21, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label4.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label4.Location = new System.Drawing.Point(114, 343);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(155, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "http://ibat.okchakela.com";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(349, 343);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(155, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "批处理交流QQ群：139354467";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 343);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "iBAT官方网站：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "作者：Codeeer";
            // 
            // webBrowser_VersionHistory
            // 
            this.webBrowser_VersionHistory.AllowNavigation = false;
            this.webBrowser_VersionHistory.AllowWebBrowserDrop = false;
            this.webBrowser_VersionHistory.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser_VersionHistory.Location = new System.Drawing.Point(24, 133);
            this.webBrowser_VersionHistory.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser_VersionHistory.Name = "webBrowser_VersionHistory";
            this.webBrowser_VersionHistory.Size = new System.Drawing.Size(480, 184);
            this.webBrowser_VersionHistory.TabIndex = 13;
            this.webBrowser_VersionHistory.Url = new System.Uri("http://data.okchakela.com/?Method=UpdateLog&SoftwareID=iBAT", System.UriKind.Absolute);
            this.webBrowser_VersionHistory.WebBrowserShortcutsEnabled = false;
            this.webBrowser_VersionHistory.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser_VersionHistory_DocumentCompleted);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(22, 373);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "意见反馈：";
            // 
            // textBox_Email
            // 
            this.textBox_Email.Location = new System.Drawing.Point(114, 368);
            this.textBox_Email.Name = "textBox_Email";
            this.textBox_Email.ReadOnly = true;
            this.textBox_Email.Size = new System.Drawing.Size(91, 21);
            this.textBox_Email.TabIndex = 15;
            this.textBox_Email.Text = "codeeer@qq.com";
            this.textBox_Email.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button_Copy
            // 
            this.button_Copy.Location = new System.Drawing.Point(211, 368);
            this.button_Copy.Name = "button_Copy";
            this.button_Copy.Size = new System.Drawing.Size(44, 21);
            this.button_Copy.TabIndex = 16;
            this.button_Copy.Text = "Copy";
            this.button_Copy.UseVisualStyleBackColor = true;
            this.button_Copy.Click += new System.EventHandler(this.button_Copy_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(184, 216);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(143, 12);
            this.label8.TabIndex = 17;
            this.label8.Text = "正在获取iBAT版本数据...";
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(526, 405);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.button_Copy);
            this.Controls.Add(this.textBox_Email);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.webBrowser_VersionHistory);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Label_Github);
            this.Controls.Add(this.Label_Version);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "关于 iBAT";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Label_Version;
        private System.Windows.Forms.Label Label_Github;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.WebBrowser webBrowser_VersionHistory;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_Email;
        private System.Windows.Forms.Button button_Copy;
        private System.Windows.Forms.Label label8;
    }
}