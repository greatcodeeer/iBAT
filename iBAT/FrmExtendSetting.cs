using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace iBAT
{
    public partial class FrmExtendSetting : Form
    {
        //非人工操作的文本内容改变
        bool AutoWrite = false;

        public FrmExtendSetting()
        {
            InitializeComponent();

            textBox_Hotkey.KeyDown += KeyDownEvent;
            textBox_Hotkey.KeyUp += KeyUpEvent;
            textBox_Para.TextChanged += delegate(object sender, EventArgs e) { SaveData(); };
            textBox_Hotkey.TextChanged += delegate(object sender, EventArgs e) { SaveData(); };

            ReLoadList();
        }

        void SaveData()
        {
            if (AutoWrite) return;
            if (listViewDetails.SelectedItems.Count == 1)
            {
                ListViewItem Lv = listViewDetails.SelectedItems[0];
                Lv.SubItems[1].Text = textBox_Para.Text;
                Lv.SubItems[2].Text = textBox_Hotkey.Text;
            }
        }

        void KeyDownEvent(object sender, KeyEventArgs e)
        {
            StringBuilder keyValue = new StringBuilder();
            keyValue.Length = 0;
            keyValue.Append("");
            if (e.Modifiers != 0)
            {
                if (e.Control)
                    keyValue.Append("Ctrl + ");
                if (e.Alt)
                    keyValue.Append("Alt + ");
                if (e.Shift)
                    keyValue.Append("Shift + ");

                if ((e.KeyValue >= 33 && e.KeyValue <= 40) ||
                    (e.KeyValue >= 65 && e.KeyValue <= 90) ||   //a-z/A-Z
                    (e.KeyValue >= 112 && e.KeyValue <= 123))   //F1-F12
                {
                    keyValue.Append(e.KeyCode);
                }
                else if ((e.KeyValue >= 48 && e.KeyValue <= 57))    //0-9
                {
                    keyValue.Append(e.KeyCode.ToString().Substring(1));
                }

                this.ActiveControl.Text = keyValue.ToString();
            }
            else
            {
                this.ActiveControl.Text = "";
            }
        }

        void KeyUpEvent(object sender, KeyEventArgs e)
        {
            string str = this.ActiveControl.Text.TrimEnd();
            int len = str.Length;
            if (len >= 1 && str.Substring(str.Length - 1) == "+")
            {
                this.ActiveControl.Text = "";
            }
        }

        private void button_Fresh_Click(object sender, EventArgs e)
        {
            ReLoadList();
        }

        void ReLoadList()
        {
            listViewDetails.Items.Clear();

            DirectoryInfo GroupFolder = new DirectoryInfo(Setting.Folder_Extend);
            List<ExtendUnit> TempLst = new List<ExtendUnit>();
            foreach (FileSystemInfo FSI in GroupFolder.GetFileSystemInfos())
            {
                if (Path.GetFileName(FSI.FullName) == Path.GetFileName(Setting.File_ConfigBin)) { continue; }

                string Name = Path.GetFileNameWithoutExtension(FSI.FullName);
                ExtendUnit EU = Setting.FindInExtendLst(Name);

                ExtendUnit Ready2EnterLstUnit = Setting.FindInExtendLst(Name);
                if (Ready2EnterLstUnit == null)
                {
                    Ready2EnterLstUnit = new ExtendUnit();
                    Ready2EnterLstUnit.Name = Name;
                    Ready2EnterLstUnit.Para = "";
                    Ready2EnterLstUnit.Hotkey = "";
                }
                if (!TempLst.Contains(Ready2EnterLstUnit)) TempLst.Add(Ready2EnterLstUnit);
            }

            foreach (ExtendUnit EU in TempLst)
            {
                //去重加载
                bool IsExist = false;
                for (int i = 0; i < listViewDetails.Items.Count; i++)
                {
                    if (listViewDetails.Items[i].Text == EU.Name)
                    {
                        IsExist = true;
                        break;
                    }
                }

                if (IsExist) continue;

                ListViewItem Lv = new ListViewItem();
                Lv.Text = EU.Name;
                Lv.SubItems.Add(EU.Para);
                Lv.SubItems.Add(EU.Hotkey);
                listViewDetails.Items.Add(Lv);
            }
        }

        private void listViewDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoWrite = true;

            if (listViewDetails.SelectedItems.Count == 1)
            {
                groupBoxDetail.Enabled = true;
                groupBoxDetail.Text = listViewDetails.SelectedItems[0].SubItems[0].Text;
                textBox_Para.Text = listViewDetails.SelectedItems[0].SubItems[1].Text;
                textBox_Hotkey.Text = listViewDetails.SelectedItems[0].SubItems[2].Text;
            }
            else
            {
                groupBoxDetail.Enabled = false;
                groupBoxDetail.Text = "";
                textBox_Hotkey.Clear();
                textBox_Para.Clear();
            }

            AutoWrite = false;
        }

        private void button_OpenFolder_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Setting.Folder_Extend);
        }

        private void FrmExtendSetting_FormClosed(object sender, FormClosedEventArgs e)
        {
            Setting.ExtendLst.Clear();
            for (int i = 0; i < listViewDetails.Items.Count; i++)
            {
                ExtendUnit EU = new ExtendUnit();
                EU.Name = listViewDetails.Items[i].SubItems[0].Text;
                EU.Para = listViewDetails.Items[i].SubItems[1].Text;
                EU.Hotkey = listViewDetails.Items[i].SubItems[2].Text;
                if (!Setting.ExtendLst.Contains(EU)) Setting.ExtendLst.Add(EU);
            }
        }
    }
}
