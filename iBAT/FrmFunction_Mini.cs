using ScintillaNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace iBAT
{
    public partial class FrmFunction_Mini : Form
    {
        string FunctionName = "";
        string Notice = "";

        string FileName = "";
        string FileDocument = "";
        string Call = "";
        string Example = "";

        public FrmFunction_Mini(string Name, string Notice)
        {
            InitializeComponent();
            this.FunctionName = Name;
            this.Notice = Notice;
        }

        private void FrmFunction_Mini_Load(object sender, EventArgs e)
        {
            LoadData();//加载数据
        }

        private void LoadData()//加载数据
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection())
                {
                    SQLiteConnectionStringBuilder SCS = new SQLiteConnectionStringBuilder();
                    SCS.DataSource = DB.db_Path;
                    conn.ConnectionString = SCS.ToString();

                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Function WHERE Name='" + FunctionName + "' and Notice='" + Notice + "'", conn))
                    {
                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                try
                                {
                                    //配置本地数据
                                    FileName = dr["FileName"].ToString();
                                    FileDocument = dr["FileDocument"].ToString();
                                    Call = dr["Call"].ToString();
                                    Example = dr["Example"].ToString();

                                }
                                catch (Exception E)
                                {
                                    Setting.ConsoleErrorLog("Message:" + E.Message + Environment.NewLine + "StackTrace:" + E.StackTrace + "Source:" + E.Source);
                                }
                            }
                        }
                    }
                }

                //布局
                textBox_Name.Text = FunctionName;
                textBox_Notice.Text = Notice;
                textBox_Call.Text = Call;
                textBox_Example.Text = Example;

                //释放文件
                if (!Directory.Exists(Setting.Folder_Function)) Directory.CreateDirectory(Setting.Folder_Function);
                string ReleasePath = Setting.Folder_Function + FileName;

                using (StreamWriter m_streamWriter = new StreamWriter(new FileStream(ReleasePath, FileMode.Create, FileAccess.Write), System.Text.Encoding.GetEncoding(936)))
                {
                    m_streamWriter.Write(FileDocument);
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button_Copy_Click(object sender, EventArgs e)
        {
            try
            {
                string Temp = "rem :::::函数体[" + textBox_Name.Text + "]开始:::::" + Environment.NewLine + "PATH " + Application.StartupPath + @"\Functions\" + ";%PATH%" + Environment.NewLine + textBox_Example.Text + Environment.NewLine + "rem :::::函数体[" + textBox_Name.Text + "]结束:::::";
                System.Windows.Forms.Clipboard.Clear();
                System.Windows.Forms.Clipboard.SetText(Temp);
                this.Close();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button_Reset_Click(object sender, EventArgs e)
        {
            textBox_Example.Text = Example;
        }
    }
}