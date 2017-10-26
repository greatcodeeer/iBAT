using ScintillaNET;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace iBAT
{
    public partial class FrmBAT_set : Form
    {
        public FrmBAT_set()
        {
            InitializeComponent();
        }

        private void FrmBAT_set_Load(object sender, EventArgs e)
        {
            comboBox1.Text = "直接赋值";
            comboBox3.Text = "从第N位之后";
            comboBox4.Text = "正数";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case "直接赋值":
                    comboBox2.Items.Clear();
                    comboBox2.Items.Add("值=");
                    comboBox2.Text = "值=";
                    break;
                case "交互输入":
                    comboBox2.Items.Clear();
                    comboBox2.Items.Add("文件路径");
                    comboBox2.Items.Add("用户提示");
                    comboBox2.Text = "用户提示";
                    break;
                case "变量处理":
                    comboBox2.Items.Clear();
                    comboBox2.Items.Add("计算");
                    comboBox2.Items.Add("替换");
                    comboBox2.Items.Add("截取");
                    comboBox2.Text = "计算";
                    break;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //处理控件的平齐位置
            if (comboBox2.Text != "截取")
            {
                comboBox2.Top = textBox2.Top;
            }
            else
            {
                comboBox2.Top = comboBox3.Top;
            }


            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            comboBox3.Enabled = false;
            comboBox4.Enabled = false;

            if (comboBox2.Text == "值=")
            {
                textBox2.Enabled = true;
            }

            if (comboBox2.Text == "用户提示")
            {
                textBox2.Enabled = true;
            }

            if (comboBox2.Text == "文件路径")
            {
                textBox2.Enabled = true;
            }

            if (comboBox2.Text == "计算")
            {
                textBox2.Enabled = true;
            }

            if (comboBox2.Text == "替换")
            {
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox6.Enabled = true;
            }

            if (comboBox2.Text == "截取")
            {
                textBox4.Enabled = true;
                textBox5.Enabled = true;
                textBox6.Enabled = true;
                comboBox3.Enabled = true;
                comboBox4.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //检测输入
                if (textBox1.Text.Trim() == string.Empty) throw new Exception("新变量名不能为空");

                string str = "";
                if (comboBox1.Text == "直接赋值")
                {
                    str = "set " + textBox1.Text + "=" + textBox2.Text;
                }

                if (comboBox1.Text == "交互输入" && comboBox2.Text == "文件路径")
                {
                    str = "set /P " + textBox1.Text + "=<\"" + textBox2.Text + "\"";
                }

                if (comboBox1.Text == "交互输入" && comboBox2.Text == "用户提示")
                {
                    str = "set /P " + textBox1.Text + "=" + textBox2.Text;
                }

                if (comboBox1.Text == "变量处理" && comboBox2.Text == "计算")
                {
                    str = "set /A " + textBox1.Text + "=" + textBox2.Text;
                }

                if (comboBox1.Text == "变量处理" && comboBox2.Text == "替换")
                {
                    str = "set " + textBox1.Text + "=%" + textBox6.Text + ":" + textBox2.Text + "=" + textBox3.Text + "%";
                }

                if (comboBox1.Text == "变量处理" && comboBox2.Text == "截取")
                {
                    //处理正数倒数问题
                    switch (comboBox3.Text)
                    {
                        case "从第N位之后":
                            string str5 = textBox5.Text;
                            if (comboBox4.Text == "倒数")
                            {
                                str5 = "-" + str5;
                            }
                            str = "set " + textBox1.Text + "=%" + textBox6.Text + ":~" + textBox4.Text + "," + str5 + "%";
                            break;
                        case "从第N位到末":
                            string str4 = textBox4.Text;
                            if (comboBox4.Text == "倒数")
                            {
                                str4 = "-" + str4;
                            }
                            str = "set " + textBox1.Text + "=%" + textBox6.Text + ":~" + str4 + "%";
                            break;
                    }
                }

                //写入数据

                if (checkBox1.Checked == true)//输出结果
                {
                    str += (Environment.NewLine + "echo %" + textBox1.Text + "%");
                }

                System.Windows.Forms.Clipboard.Clear();
                System.Windows.Forms.Clipboard.SetText(str);
                Close();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text == "从第N位之后")
            {
                textBox5.Visible = true;
                label6.Visible = true;
            }
            else
            {
                textBox5.Visible = false;
                label6.Visible = false;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"输入框第一个字符不能为“=”或“/”或“""”：\n在XP以上的系统中，SET /P指令会吞掉为首的空格、制表符、0xff字符\n", "提示",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string str = "";
            if (comboBox1.Text == "直接赋值")
            {
                str = "set " + textBox1.Text + "=" + textBox2.Text;
            }

            if (comboBox1.Text == "交互输入" && comboBox2.Text == "文件路径")
            {
                str = "set /P " + textBox1.Text + "=<\"" + textBox2.Text + "\"";
            }

            if (comboBox1.Text == "交互输入" && comboBox2.Text == "用户提示")
            {
                str = "set /P " + textBox1.Text + "=" + textBox2.Text;
            }

            if (comboBox1.Text == "变量处理" && comboBox2.Text == "计算")
            {
                str = "set /A " + textBox1.Text + "=" + textBox2.Text;
            }

            if (comboBox1.Text == "变量处理" && comboBox2.Text == "替换")
            {
                str = "set " + textBox1.Text + "=%" + textBox6.Text + ":" + textBox2.Text + "=" + textBox3.Text + "%";
            }

            if (comboBox1.Text == "变量处理" && comboBox2.Text == "截取")
            {
                //处理正数倒数问题
                switch (comboBox3.Text)
                {
                    case "从第N位之后":
                        string str5 = textBox5.Text;
                        if (comboBox4.Text == "倒数")
                        {
                            str5 = "-" + str5;
                        }
                        str = "set " + textBox1.Text + "=%" + textBox6.Text + ":~" + textBox4.Text + "," + str5 + "%";
                        break;
                    case "从第N位到末":
                        string str4 = textBox4.Text;
                        if (comboBox4.Text == "倒数")
                        {
                            str4 = "-" + str4;
                        }
                        str = "set " + textBox1.Text + "=%" + textBox6.Text + ":~" + str4 + "%";
                        break;
                }
            }
            //写入数据
            textBox7.Text = str;
            if (checkBox1.Checked == true)//输出结果
            {
                textBox7.AppendText(Environment.NewLine + "echo %" + textBox1.Text + "%");
            }
        }
    }
}
