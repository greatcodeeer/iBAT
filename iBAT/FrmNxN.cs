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
    public partial class FrmNxN : Form
    {
        public FrmNxN()
        {
            InitializeComponent();
        }

        public Scintilla Target_Scintilla;
        public FrmNxN(Scintilla Scintilla_Document)
        {
            InitializeComponent();
            Target_Scintilla = Scintilla_Document;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Str = string.Empty;
            for (int i = 0; i < numericUpDown1.Value; i++)
            {
                Str += textBox1.Text;
            }
            Target_Scintilla.InsertText(Str);
            this.Close();
        }
    }
}
