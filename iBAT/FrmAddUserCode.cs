using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace iBAT
{
    public partial class FrmAddUserCode : Form
    {
        public FrmAddUserCode()
        {
            InitializeComponent();

            button_Add.Click += delegate(object sender, EventArgs e)
            {
                string Group = comboBox_Group.Text;
                string Title = textBox_Title.Text;

                FrmCodeUnit Unit = new FrmCodeUnit(Group, null, Title);
                Unit.ShowDialog();
                this.Close();
            };

            button_Group_Add.Click += delegate(object sender, EventArgs e)
            {
                string NewUserGroupName = InputBox.ShowInputBox("创建自定义代码组", "请输入新代码组的名称：").Trim();
                if (NewUserGroupName != "")
                {
                    Function.UserCodeGroup.Creat(NewUserGroupName);
                    ReFreshUserGroup();
                }
            };

            button_Group_Delete.Click += delegate(object sender, EventArgs e)
            {
                string GroupName = comboBox_Group.Text;
                if (GroupName.Trim() == "") return;

                if (MessageBox.Show("确认删除组" + GroupName + "？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    Function.UserCodeGroup.Delete(GroupName);
                    ReFreshUserGroup();
                }
            };

            ReFreshUserGroup();
        }

        void ReFreshUserGroup()
        {
            List<FileSystemInfo> FileS = Setting.GetUserGroupNames();
            comboBox_Group.Items.Clear();

            foreach (FileSystemInfo SingleFile in FileS)
            {
                comboBox_Group.Items.Add(Path.GetFileNameWithoutExtension(SingleFile.Name));
            }

            if (comboBox_Group.Items.Count > 0)
            {
                comboBox_Group.Text = comboBox_Group.Items[comboBox_Group.Items.Count - 1].ToString();
                button_Group_Delete.Enabled = true;
            }
            else
            {
                button_Group_Delete.Enabled = false;
            }
        }

        private void timer_Check_Tick(object sender, EventArgs e)
        {
            bool FinalStatu = true;

            if (comboBox_Group.Text.Trim() == "")
            {
                errorProvider_Check.SetError(comboBox_Group, "代码分组不能为空");
                FinalStatu = false;
            }
            else
            {
                errorProvider_Check.SetError(comboBox_Group, "");
            }

            if (textBox_Title.Text.Trim() == "")
            {
                errorProvider_Check.SetError(textBox_Title, "请输入代码标题");
                FinalStatu = false;
            }
            else
            {
                errorProvider_Check.SetError(textBox_Title, "");
            }

            if (FinalStatu && button_Add.Enabled == false) button_Add.Enabled = true;
            if (!FinalStatu && button_Add.Enabled == true) button_Add.Enabled = false;
        }
    }
}
