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
    public partial class FrmBAT_choice : Form
    {
        public FrmBAT_choice()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Trim() == string.Empty) throw new Exception("用户选项列表不能为空");

                string str = "CHOICE /C " + textBox1.Text;

                if (comboBox1.Text == "True")
                {
                    str += " /N";
                }

                if (comboBox2.Text == "True")
                {
                    str += " /CS";
                }

                if (comboBox3.Text != "NONE")
                {
                    str += " /T " + numericUpDown1.Value + " /D " + comboBox3.Text;
                }

                if (textBox2.Text.Trim() != string.Empty)
                {
                    str += " /M \"" + textBox2.Text.Trim() + "\"";
                }

                System.Windows.Forms.Clipboard.Clear();
                System.Windows.Forms.Clipboard.SetText(str);
                Close();
            }
            catch(Exception E)
            {
                MessageBox.Show(E.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("示例：\n选择B，则……\n选择C，则……\n选择D，则……\n于是，你需要在选项列表中输入BCD（不用分隔）", "帮助", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FrmBAT_choice_Load(object sender, EventArgs e)
        {
            comboBox1.Text = "False";
            comboBox2.Text = "False";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            comboBox3.Items.Add("NONE");

            foreach (char a in textBox1.Text)
            {
                if (a != ' ')
                {
                    comboBox3.Items.Add(a);
                }
            }

            comboBox3.Text = "NONE";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string str = "CHOICE /C " + textBox1.Text;

            if (comboBox1.Text == "True")
            {
                str += " /N";
            }

            if (comboBox2.Text == "True")
            {
                str += " /CS";
            }

            if (comboBox3.Text != "NONE")
            {
                str += " /T " + numericUpDown1.Value + " /D " + comboBox3.Text;
            }

            if (textBox2.Text.Trim() != string.Empty)
            {
                str += " /M \"" + textBox2.Text.Trim() + "\"";
            }

            textBox3.Text = str;
        }
    }
}
