using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using ScintillaNET;

namespace iBAT
{
    public partial class Frm123456789 : Form
    {
        public Frm123456789()
        {
            InitializeComponent();
        }

        public Scintilla Target_Scintilla;
        public Frm123456789(Scintilla Target)
        {
            InitializeComponent();
            this.Target_Scintilla = Target;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder str=new StringBuilder();
            if (numericUpDown1.Value <= numericUpDown2.Value)
            {
                for (decimal i = numericUpDown1.Value; i <= numericUpDown2.Value; i++)
                {
                    str.Append(i + textBox1.Text.ToString());
                }
                Target_Scintilla.InsertText(str.ToString() + Environment.NewLine);
            }
            else
            {
                for (decimal i = numericUpDown1.Value; i >= numericUpDown2.Value; i--)
                {
                    str.Append(i + textBox1.Text.ToString());
                }
                Target_Scintilla.InsertText(str.ToString() + Environment.NewLine);
            }
            this.Close();
        }

        private void TextChange()//判断输入是否正确
        {
            bool Right = false;
            Regex r = new Regex("[a-zA-Z]+");
            Regex r_r=new Regex("[a-z]+");
            Regex r_R=new Regex("[A-Z]+");
            if (textBox2.Text.Trim() != string.Empty && textBox3.Text.Trim() != string.Empty)
            {
                if (textBox2.Text != textBox3.Text)
                {
                    if (r.IsMatch(textBox2.Text)
                        && r.IsMatch(textBox3.Text)
                        && textBox2.TextLength == 1
                        && textBox3.TextLength == 1)
                    {
                        if ((r_r.IsMatch(textBox2.Text) && r_r.IsMatch(textBox3.Text))
                            || r_R.IsMatch(textBox2.Text) && r_R.IsMatch(textBox3.Text))
                        {
                            Right = true;
                        }
                        else
                        {
                            label7.Text = "提示:首尾字母大小写必须一致";
                        }
                    }
                    else
                    {
                        if (textBox2.TextLength > 1) { label7.Text = "提示:输入字符必须为1位字母"; }
                        if (textBox3.TextLength > 1) { label7.Text = "提示:输入字符必须为1位字母"; }
                    }
                }
                else
                {
                    label7.Text = "提示:首尾字母不能一致";
                }
            }
            else
            {
                label7.Text = "提示:输入框不能为空";
            }

            button2.Enabled = Right;
            label7.Visible = !Right;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            TextChange();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            TextChange();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int a = Convert.ToChar(textBox2.Text);
            int b = Convert.ToChar(textBox3.Text);
            StringBuilder str = new StringBuilder();

            if (a <= b)
            {
                for (int i = a; i <= b; i++)
                {
                    str.Append(Convert.ToChar(i).ToString() + textBox4.Text.ToString());
                }
            }
            else
            {
                for (int i = a; i >= b; i--)
                {
                    str.Append(Convert.ToChar(i).ToString() + textBox4.Text.ToString());
                }
            }
            Target_Scintilla.InsertText(str.ToString() + Environment.NewLine);
            this.Close();
        }
    }
}
