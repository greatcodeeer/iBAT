using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace iBAT
{
    public partial class FrmSafe : Form
    {
        public FrmSafe()
        {
            InitializeComponent();
        }

        public void WriteInfo(string str)
        {
            listBox1.Items.Add("[" + System.DateTime.Now.ToShortTimeString() + "]" + str);
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }

        private void FrmSafe_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Visible = false;
            e.Cancel = true;
        }
    }
}
