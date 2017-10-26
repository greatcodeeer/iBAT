using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace iBAT
{
    public partial class About : Form
    {
        public About(string Version)
        {
            InitializeComponent();
            Label_Version.Text = Version;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.Arguments = "/c start http://ibat.okchakela.com";
            process.Start();
        }

        private void button_Copy_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.Clear();
                Clipboard.SetDataObject("codeeer@qq.com");
            }
            catch (Exception E)
            {
                MessageBox.Show("居然会复制失败-。-您手动复制吧", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void webBrowser_VersionHistory_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            label8.Visible = false;
        }
    }
}
