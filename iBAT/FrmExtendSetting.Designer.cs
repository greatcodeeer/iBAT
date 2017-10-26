namespace iBAT
{
    partial class FrmExtendSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExtendSetting));
            this.listViewDetails = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.button_OpenFolder = new System.Windows.Forms.Button();
            this.button_Fresh = new System.Windows.Forms.Button();
            this.groupBoxDetail = new System.Windows.Forms.GroupBox();
            this.textBox_Hotkey = new System.Windows.Forms.TextBox();
            this.textBox_Para = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewDetails
            // 
            this.listViewDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listViewDetails.FullRowSelect = true;
            this.listViewDetails.GridLines = true;
            this.listViewDetails.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewDetails.Location = new System.Drawing.Point(10, 61);
            this.listViewDetails.MultiSelect = false;
            this.listViewDetails.Name = "listViewDetails";
            this.listViewDetails.Size = new System.Drawing.Size(502, 248);
            this.listViewDetails.TabIndex = 0;
            this.listViewDetails.UseCompatibleStateImageBehavior = false;
            this.listViewDetails.View = System.Windows.Forms.View.Details;
            this.listViewDetails.SelectedIndexChanged += new System.EventHandler(this.listViewDetails_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "插件名称";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "启动参数（可为空）";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "热键";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 100;
            // 
            // label1
            // 
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(10, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(346, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "将需要快速启动的插件移至【插件】目录，之后刷新列表。";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button_OpenFolder
            // 
            this.button_OpenFolder.Location = new System.Drawing.Point(370, 16);
            this.button_OpenFolder.Name = "button_OpenFolder";
            this.button_OpenFolder.Size = new System.Drawing.Size(68, 29);
            this.button_OpenFolder.TabIndex = 2;
            this.button_OpenFolder.Text = "插件目录";
            this.button_OpenFolder.UseVisualStyleBackColor = true;
            this.button_OpenFolder.Click += new System.EventHandler(this.button_OpenFolder_Click);
            // 
            // button_Fresh
            // 
            this.button_Fresh.Location = new System.Drawing.Point(444, 16);
            this.button_Fresh.Name = "button_Fresh";
            this.button_Fresh.Size = new System.Drawing.Size(68, 29);
            this.button_Fresh.TabIndex = 3;
            this.button_Fresh.Text = "刷新列表";
            this.button_Fresh.UseVisualStyleBackColor = true;
            this.button_Fresh.Click += new System.EventHandler(this.button_Fresh_Click);
            // 
            // groupBoxDetail
            // 
            this.groupBoxDetail.Controls.Add(this.textBox_Hotkey);
            this.groupBoxDetail.Controls.Add(this.textBox_Para);
            this.groupBoxDetail.Controls.Add(this.label3);
            this.groupBoxDetail.Controls.Add(this.label2);
            this.groupBoxDetail.Enabled = false;
            this.groupBoxDetail.Location = new System.Drawing.Point(10, 320);
            this.groupBoxDetail.Name = "groupBoxDetail";
            this.groupBoxDetail.Size = new System.Drawing.Size(502, 97);
            this.groupBoxDetail.TabIndex = 4;
            this.groupBoxDetail.TabStop = false;
            // 
            // textBox_Hotkey
            // 
            this.textBox_Hotkey.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_Hotkey.ImeMode = System.Windows.Forms.ImeMode.Close;
            this.textBox_Hotkey.Location = new System.Drawing.Point(102, 58);
            this.textBox_Hotkey.Name = "textBox_Hotkey";
            this.textBox_Hotkey.ReadOnly = true;
            this.textBox_Hotkey.Size = new System.Drawing.Size(379, 21);
            this.textBox_Hotkey.TabIndex = 3;
            // 
            // textBox_Para
            // 
            this.textBox_Para.Location = new System.Drawing.Point(102, 27);
            this.textBox_Para.Name = "textBox_Para";
            this.textBox_Para.Size = new System.Drawing.Size(379, 21);
            this.textBox_Para.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "热键设定";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "启动参数";
            // 
            // FrmExtendSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(526, 432);
            this.Controls.Add(this.groupBoxDetail);
            this.Controls.Add(this.button_Fresh);
            this.Controls.Add(this.button_OpenFolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listViewDetails);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmExtendSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "快速启动设置";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmExtendSetting_FormClosed);
            this.groupBoxDetail.ResumeLayout(false);
            this.groupBoxDetail.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewDetails;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_OpenFolder;
        private System.Windows.Forms.Button button_Fresh;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.GroupBox groupBoxDetail;
        private System.Windows.Forms.TextBox textBox_Hotkey;
        private System.Windows.Forms.TextBox textBox_Para;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}