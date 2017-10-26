using ScintillaNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace iBAT
{
    public partial class FrmBAT_logic : Form
    {
        public FrmBAT_logic()
        {
            InitializeComponent();
        }

        private void FrmBAT_logic_Load(object sender, EventArgs e)
        {
            comboBox1.Text = "无条件执行";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Trim() == string.Empty || textBox2.Text.Trim() == string.Empty) throw new Exception("命令不能为空");

                string str = textBox1.Text;
                switch (comboBox1.Text)
                {
                    case "无条件执行":
                        str += " & ";
                        break;
                    case "执行成功":
                        str += " && ";
                        break;
                    case "执行失败":
                        str += " || ";
                        break;
                }
                str += textBox2.Text;

                System.Windows.Forms.Clipboard.Clear();
                System.Windows.Forms.Clipboard.SetText(str);
                Close();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
