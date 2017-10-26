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
    public partial class FrmBAT_环境变量 : Form
    {
        public FrmBAT_环境变量()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            ScanBox();
        }

        private void ScanBox()//搜索
        {
            int OKNum = 0;

            if (listView2.Items.Count > 0 && textBox2.Text != "")
            {
                for (int i = 0; i < listView2.Items.Count; i++)
                {
                    if (listView2.Items[i].Text.Contains(textBox2.Text.ToUpper()) || listView2.Items[i].SubItems[1].Text.Contains(textBox2.Text.ToUpper()))
                    {
                        OKNum += 1;
                        listView2.Items[i].BackColor = Color.YellowGreen;
                    }
                    else
                    {
                        listView2.Items[i].BackColor = Color.White;
                    }
                }
            }
            else
            {
                for (int i = 0; i < listView2.Items.Count; i++)
                {
                    OKNum = 0;
                    listView2.Items[i].BackColor = Color.White;
                }
            }

            label1.Text = Convert.ToString(OKNum);
        }

        private void listView2_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.Clipboard.Clear();
                System.Windows.Forms.Clipboard.SetText(textBox1.Text + "%" + listView2.SelectedItems[0].SubItems[0].Text + "%" + textBox3.Text);
                MessageBox.Show("代码已经复制到剪贴板", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
