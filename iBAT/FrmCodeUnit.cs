using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace iBAT
{
    public partial class FrmCodeUnit : Form
    {
        public string GroupName = "";
        public Setting.UserCodeUnit CodeUnit = new Setting.UserCodeUnit();
        public bool TextChange = false;

        public FrmCodeUnit(string GroupName, string GUID = null, string Title = null)
        {
            InitializeComponent();

            this.GroupName = GroupName;

            if (Title != null) this.Text = Title;

            if (GUID != null)
            {
                CodeUnit = Setting.LoadUserCodeGroup(GroupName, GUID);

                Console.WriteLine(GroupName + ":" + GUID);

                textBox_Code.Text = CodeUnit.Text;
                this.Text = CodeUnit.Title;
            }
            else
            {
                textBox_Code.ReadOnly = false;
                toolStrip1.Visible = false;
            }

            toolStripButton_Edit_Enable.Click += delegate(object sender, EventArgs e)
            {
                textBox_Code.ReadOnly = false;
                toolStrip1.Visible = false;
            };

            textBox_Code.TextChanged += delegate(object sender, EventArgs e)
            {
                TextChange = true;
            };

            toolStripButton_Copy.Click += delegate(object sender, EventArgs e)
            {
                try
                {
                    Clipboard.Clear();
                    Clipboard.SetText(textBox_Code.Text);
                    this.Close();
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };

            this.FormClosed += delegate(object sender, FormClosedEventArgs e)
            {
                if (TextChange)
                {
                    if (MessageBox.Show("文档内容已经改变，需要保存吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        if (GUID != null)
                        {
                            Function.UserCodeGroup.CODEItem.Change(GroupName, CodeUnit.Title, textBox_Code.Text, CodeUnit.GUID);
                        }
                        else
                        {
                            Function.UserCodeGroup.CODEItem.Change(GroupName, this.Text, textBox_Code.Text);
                        }
                    }
                }
            };
        }
    }
}
