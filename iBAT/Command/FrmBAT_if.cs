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
    public partial class FrmBAT_if : Form
    {
        public FrmBAT_if()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Trim() == string.Empty) throw new Exception("条件语句不能为空");
                if (textBox3.Text.Trim() == string.Empty) throw new Exception("目的语句不能为空");

                string str;//定义输出变量

                if (comboBox2.Text.Contains("否定"))//[NOT]处理
                {
                    str = "IF NOT ";
                }
                else
                {
                    str = "IF ";
                }

                switch (comboBox1.Text)
                {
                    case "字符串比较":
                        if (checkBox2.Checked == true)
                        {
                            str += "/I ";
                        }

                        str += @"""%" + textBox1.Text + @"%""";

                        switch (comboBox3.Text)
                        {
                            case "等于":
                                str += " EQU "; break;
                            case "不等于":
                                str += " NEQ "; break;
                            case "小于":
                                str += " LSS "; break;
                            case "小于或等于":
                                str += " LEQ "; break;
                            case "大于":
                                str += " GTR "; break;
                            case "大于或等于":
                                str += " GEQ "; break;
                        }

                        str += @"""" + textBox2.Text + @"""" + " (" + textBox3.Text + ")";
                        break;

                    case "判断文件存在":
                        str += "EXIST \"" + textBox1.Text + "\" (" + textBox3.Text + ")";
                        break;
                }

                //else处理
                if (checkBox2.Checked == true)
                {
                    str += " ELSE (" + textBox4.Text + ")";
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "字符串比较")
            {
                comboBox2.Items.Clear();
                comboBox2.Items.Add("如果字符");
                comboBox2.Items.Add("如果字符（否定）");
                comboBox2.Text = "如果字符";
                comboBox3.Visible = true;
                checkBox1.Visible = true;
                textBox2.Visible = true;
            }

            if (comboBox1.Text == "判断文件存在")
            {
                comboBox2.Items.Clear();
                comboBox2.Items.Add("如果存在");
                comboBox2.Items.Add("如果存在（否定）");
                comboBox2.Text = "如果存在";
                comboBox3.Visible = false;
                checkBox1.Visible = false;
                textBox2.Visible = false;
            }
        }

        private void FrmBAT_if_Load(object sender, EventArgs e)
        {
            comboBox1.Text = "字符串比较";
            comboBox3.Text = "等于";
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                textBox4.Enabled = true;
            }
            else
            {
                textBox4.Enabled = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string str;//定义输出变量

            if (comboBox2.Text.Contains("否定"))//[NOT]处理
            {
                str = "IF NOT ";
            }
            else
            {
                str = "IF ";
            }

            switch (comboBox1.Text)
            {
                case "字符串比较":
                    if (checkBox2.Checked == true)
                    {
                        str += "/I ";
                    }

                    str += @"""%" + textBox1.Text + @"%""";

                    switch (comboBox3.Text)
                    {
                        case "等于":
                            str += " EQU "; break;
                        case "不等于":
                            str += " NEQ "; break;
                        case "小于":
                            str += " LSS "; break;
                        case "小于或等于":
                            str += " LEQ "; break;
                        case "大于":
                            str += " GTR "; break;
                        case "大于或等于":
                            str += " GEQ "; break;
                    }

                    str += @"""" + textBox2.Text + @"""" + " (" + textBox3.Text + ")";
                    break;

                case "判断文件存在":
                    str += "EXIST \"" + textBox1.Text + "\" (" + textBox3.Text + ")";
                    break;
            }

            //else处理
            if (checkBox2.Checked == true)
            {
                str += " ELSE (" + textBox4.Text + ")";
            }

            textBox5.Text = str;
        }
    }
}
