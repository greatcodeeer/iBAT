using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;
using System.Windows.Forms;

namespace iBAT
{
    /// <summary>
    /// Sqlite DataBase Control Center
    /// </summary>
    public static class DB
    {
        /// <summary>
        /// 数据库路径
        /// </summary>
        public static string db_Path = Application.StartupPath + @"\DataS.db";

        /// <summary>
        /// 获取数据库表段
        /// </summary>
        /// <returns></returns>
        public static List<string> GetTables()
        {
            List<string> ResultLst = new List<string>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection())
                {
                    SQLiteConnectionStringBuilder SCS = new SQLiteConnectionStringBuilder();
                    SCS.DataSource = db_Path;
                    conn.ConnectionString = SCS.ToString();

                    conn.Open();
                    using (SQLiteCommand tablesGet = new SQLiteCommand("SELECT name from sqlite_master where type='table'", conn))
                    {
                        using (SQLiteDataReader tables = tablesGet.ExecuteReader())
                        {
                            while (tables.Read())
                            {
                                try
                                {
                                    ResultLst.Add(tables[0].ToString());
                                }
                                catch (Exception E)
                                {
                                    MessageBox.Show(E.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return ResultLst;
        }

        /// <summary>
        /// 获取数据库指定表段中的字段
        /// </summary>
        /// <param name="TargetTable">指定表段</param>
        /// <returns></returns>
        public static List<string> GetWords(string TargetTable)
        {
            List<string> WordsLst = new List<string>();

            using (SQLiteConnection conn = new SQLiteConnection())
            {
                SQLiteConnectionStringBuilder SCS = new SQLiteConnectionStringBuilder();
                SCS.DataSource = db_Path;
                conn.ConnectionString = SCS.ToString();

                conn.Open();
                using (SQLiteCommand tablesGet = new SQLiteCommand(@"SELECT * FROM '" + TargetTable + "'", conn))
                {
                    using (SQLiteDataReader Words = tablesGet.ExecuteReader())
                    {
                        try
                        {
                            for (int i = 0; i < Words.FieldCount; i++)
                            {
                                WordsLst.Add(Words.GetName(i));
                            }
                        }
                        catch (Exception E)
                        {
                            MessageBox.Show(E.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }

            return WordsLst;
        }

        /// <summary>
        /// 获取数据库值信息
        /// </summary>
        /// <param name="Sql">SQL提交语句</param>
        /// <param name="Words">返回的目标字段数据集合</param>
        /// <returns></returns>
        public static List<string> GetValues(string Sql, string Words)
        {
            List<string> ResultLst = new List<string>();

            using (SQLiteConnection conn = new SQLiteConnection())
            {
                SQLiteConnectionStringBuilder SCS = new SQLiteConnectionStringBuilder();
                SCS.DataSource = db_Path;
                conn.ConnectionString = SCS.ToString();

                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(Sql, conn))
                {
                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            try
                            {
                                ResultLst.Add(dr[Words].ToString());
                            }
                            catch (Exception E)
                            {
                                MessageBox.Show(E.Message,"提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }

            return ResultLst;
        }

        /// <summary>
        /// 提交数据库指令
        /// </summary>
        /// <param name="Sql">SQL语句</param>
        /// <returns></returns>
        public static bool SqlCommand(string Sql)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection())
                {
                    SQLiteConnectionStringBuilder SCS = new SQLiteConnectionStringBuilder();
                    SCS.DataSource = db_Path;
                    conn.ConnectionString = SCS.ToString();

                    conn.Open();
                    using (SQLiteCommand cmd_Re = new SQLiteCommand(Sql, conn))
                    {
                        cmd_Re.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }
    }
}
