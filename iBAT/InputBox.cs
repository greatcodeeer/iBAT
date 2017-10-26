using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace iBAT
{
    /// <summary>
    /// 信息输入框
    /// </summary>
    public class InputBox : System.Windows.Forms.Form
    {
        private TextBox txtData;
        private System.Windows.Forms.Label lblInfo;
        private Button button1;
        private Button button2;
        private System.ComponentModel.Container components = null;

        private InputBox(string DefaultInput, bool UsePasswordForm)
        {
            InitializeComponent(DefaultInput, UsePasswordForm);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent(string DefaultInput, bool UsePasswordForm)
        {
            this.txtData = new System.Windows.Forms.TextBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtData
            // 
            this.txtData.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtData.Location = new System.Drawing.Point(19, 76);
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(400, 21);
            this.txtData.TabIndex = 0;
            if (UsePasswordForm)
            {
                this.txtData.PasswordChar = '*';
            }
            if (DefaultInput != null)
            {
                this.txtData.Text = DefaultInput;
            }
            this.txtData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtData_KeyDown);
            // 
            // lblInfo
            // 
            this.lblInfo.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblInfo.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblInfo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInfo.ForeColor = System.Drawing.Color.Gray;
            this.lblInfo.Location = new System.Drawing.Point(19, 9);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(317, 56);
            this.lblInfo.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(352, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 27);
            this.button1.TabIndex = 2;
            this.button1.Text = "确认";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(352, 38);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(67, 27);
            this.button2.TabIndex = 3;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // InputBox
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(431, 111);
            this.ControlBox = false;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.txtData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "InputBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InputBox";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        //对键盘进行响应 
        private void txtData_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Close();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                txtData.Text = string.Empty;
                this.Close();
            }
        }

        //显示InputBox 
        public static string ShowInputBox(string Title, string keyInfo, string DefaultInput = null, bool UsePasswordForm = false)
        {
            InputBox inputbox = new InputBox(DefaultInput, UsePasswordForm);
            inputbox.Text = Title;
            inputbox.TopMost = true;

            if (keyInfo.Trim() != string.Empty)
            {
                inputbox.lblInfo.Text = keyInfo;
            }

            inputbox.ShowDialog();
            return inputbox.txtData.Text;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            txtData.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
    }
}
