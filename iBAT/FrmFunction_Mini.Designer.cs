namespace iBAT
{
    partial class FrmFunction_Mini
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
            this.textBox_Name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_Notice = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_Call = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_Example = new System.Windows.Forms.TextBox();
            this.button_Copy = new System.Windows.Forms.Button();
            this.button_Reset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "函数";
            // 
            // textBox_Name
            // 
            this.textBox_Name.Location = new System.Drawing.Point(50, 12);
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.ReadOnly = true;
            this.textBox_Name.Size = new System.Drawing.Size(555, 21);
            this.textBox_Name.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "说明";
            // 
            // textBox_Notice
            // 
            this.textBox_Notice.Location = new System.Drawing.Point(50, 43);
            this.textBox_Notice.Multiline = true;
            this.textBox_Notice.Name = "textBox_Notice";
            this.textBox_Notice.ReadOnly = true;
            this.textBox_Notice.Size = new System.Drawing.Size(555, 25);
            this.textBox_Notice.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "引用";
            // 
            // textBox_Call
            // 
            this.textBox_Call.Location = new System.Drawing.Point(50, 80);
            this.textBox_Call.Multiline = true;
            this.textBox_Call.Name = "textBox_Call";
            this.textBox_Call.ReadOnly = true;
            this.textBox_Call.Size = new System.Drawing.Size(555, 131);
            this.textBox_Call.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 226);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "实例";
            // 
            // textBox_Example
            // 
            this.textBox_Example.Location = new System.Drawing.Point(50, 226);
            this.textBox_Example.Multiline = true;
            this.textBox_Example.Name = "textBox_Example";
            this.textBox_Example.Size = new System.Drawing.Size(555, 207);
            this.textBox_Example.TabIndex = 7;
            // 
            // button_Copy
            // 
            this.button_Copy.Location = new System.Drawing.Point(50, 445);
            this.button_Copy.Name = "button_Copy";
            this.button_Copy.Size = new System.Drawing.Size(480, 27);
            this.button_Copy.TabIndex = 10;
            this.button_Copy.Text = "复制并关闭";
            this.button_Copy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button_Copy.UseVisualStyleBackColor = true;
            this.button_Copy.Click += new System.EventHandler(this.button_Copy_Click);
            // 
            // button_Reset
            // 
            this.button_Reset.Location = new System.Drawing.Point(539, 445);
            this.button_Reset.Name = "button_Reset";
            this.button_Reset.Size = new System.Drawing.Size(66, 27);
            this.button_Reset.TabIndex = 11;
            this.button_Reset.Text = "还原";
            this.button_Reset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button_Reset.UseVisualStyleBackColor = true;
            this.button_Reset.Click += new System.EventHandler(this.button_Reset_Click);
            // 
            // FrmFunction_Mini
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(621, 489);
            this.Controls.Add(this.button_Reset);
            this.Controls.Add(this.button_Copy);
            this.Controls.Add(this.textBox_Example);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_Call);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_Notice);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_Name);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.FunctionName = "FrmFunction_Mini";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "函数调用";
            this.Load += new System.EventHandler(this.FrmFunction_Mini_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_Call;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_Example;
        private System.Windows.Forms.Button button_Copy;
        public System.Windows.Forms.TextBox textBox_Name;
        public System.Windows.Forms.TextBox textBox_Notice;
        private System.Windows.Forms.Button button_Reset;
    }
}