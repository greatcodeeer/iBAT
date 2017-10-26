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
    public partial class FrmBAT_for : Form
    {
        public FrmBAT_for()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "FOR %%" + textBox1.Text + " IN (" + textBox2.Text + ") DO " + textBox3.Text;
                System.Windows.Forms.Clipboard.Clear();
                System.Windows.Forms.Clipboard.SetText(str);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (textBox7.Text.Trim() == string.Empty)
            {
                label21.Visible = true;
            }
            else
            {
                label21.Visible = false;
            }
        }

        private void FrmBAT_for_Load(object sender, EventArgs e)
        {
            comboBox3.Text = "False";
            comboBox4.Text = "False";
            comboBox5.Text = "False";
            comboBox6.Text = "False";
            comboBox7.Text = "False";
            comboBox8.Text = "False";
            comboBox9.Text = "False";
            comboBox10.Text = "False";
            comboBox11.Text = "False";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == string.Empty)
            {
                label28.Visible = true;
            }
            else
            {
                label28.Visible = false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Trim() == string.Empty)
            {
                label29.Visible = true;
            }
            else
            {
                label29.Visible = false;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text.Trim() == string.Empty)
            {
                label30.Visible = true;
            }
            else
            {
                label30.Visible = false;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text.Trim() == string.Empty)
            {
                label31.Visible = true;
            }
            else
            {
                label31.Visible = false;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text.Trim() == string.Empty)
            {
                label32.Visible = true;
            }
            else
            {
                label32.Visible = false;
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text.Trim() == string.Empty)
            {
                label33.Visible = true;
            }
            else
            {
                label33.Visible = false;
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (textBox8.Text.Trim() == string.Empty)
            {
                label34.Visible = true;
            }
            else
            {
                label34.Visible = false;
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (textBox9.Text.Trim() == string.Empty)
            {
                label35.Visible = true;
            }
            else
            {
                label35.Visible = false;
            }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            if (textBox10.Text.Trim() == string.Empty)
            {
                label36.Visible = true;
            }
            else
            {
                label36.Visible = false;
            }
        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {
            if (textBox16.Text.Trim() == string.Empty)
            {
                label37.Visible = true;
            }
            else
            {
                label37.Visible = false;
            }
        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            if (textBox15.Text.Trim() == string.Empty)
            {
                label38.Visible = true;
            }
            else
            {
                label38.Visible = false;
            }
        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {
            if (textBox17.Text.Trim() == string.Empty)
            {
                label39.Visible = true;
            }
            else
            {
                label39.Visible = false;
            }
        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {
            if (textBox19.Text.Trim() == string.Empty)
            {
                label40.Visible = true;
            }
            else
            {
                label40.Visible = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "FOR /D %%" + textBox4.Text + " IN (" + textBox5.Text + ") DO " + textBox6.Text;
                System.Windows.Forms.Clipboard.Clear();
                System.Windows.Forms.Clipboard.SetText(str);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "FOR /R " + textBox7.Text + " %%" + textBox8.Text + " IN (" + textBox9.Text + ") DO " + textBox10.Text;
                System.Windows.Forms.Clipboard.Clear();
                System.Windows.Forms.Clipboard.SetText(str);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "FOR /L %%" + textBox16.Text + " IN (" + textBox12.Text + "," + textBox13.Text + "," + textBox14.Text + ") DO " + textBox15.Text;
                System.Windows.Forms.Clipboard.Clear();
                System.Windows.Forms.Clipboard.SetText(str);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "FOR /F \"" + comboBox1.Text + "\"" + " %%";

                if (comboBox3.Text == "True" ||
                    comboBox4.Text == "True" ||
                    comboBox5.Text == "True" ||
                    comboBox6.Text == "True" ||
                    comboBox7.Text == "True" ||
                    comboBox8.Text == "True" ||
                    comboBox9.Text == "True" ||
                    comboBox10.Text == "True" ||
                    comboBox11.Text == "True")
                {
                    str += "~";

                    if (comboBox3.Text == "True") { str += "f"; }
                    if (comboBox4.Text == "True") { str += "d"; }
                    if (comboBox5.Text == "True") { str += "p"; }
                    if (comboBox6.Text == "True") { str += "n"; }
                    if (comboBox7.Text == "True") { str += "x"; }
                    if (comboBox8.Text == "True") { str += "s"; }
                    if (comboBox9.Text == "True") { str += "a"; }
                    if (comboBox10.Text == "True") { str += "t"; }
                    if (comboBox11.Text == "True") { str += "z"; }
                }

                str += textBox17.Text + " IN (" + comboBox2.Text + ") DO " + textBox19.Text;

                System.Windows.Forms.Clipboard.Clear();
                System.Windows.Forms.Clipboard.SetText(str);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
