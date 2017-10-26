using Microsoft.CSharp;
using ScintillaNET;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace iBAT
{
    public static class Setting
    {
        /// <summary>
        /// 使用时间
        /// </summary>
        public static Int64 UsingSeconds = 0;

        /// <summary>
        /// 函数库位置
        /// </summary>
        public static string Folder_Function = Application.StartupPath + @"\Functions\";

        /// <summary>
        /// 用户代码组
        /// </summary>
        public static string Folder_UserCodeGroup = Application.StartupPath + @"\CodeGroup\";

        /// <summary>
        /// 插件扩展组
        /// </summary>
        public static string Folder_Extend = Application.StartupPath + @"\Extend\";

        /// <summary>
        /// 插件扩展文件
        /// </summary>
        public static string File_ConfigBin = Setting.Folder_Extend + "Config.bin";

        /// <summary>
        /// 插件扩展列表
        /// </summary>
        public static List<ExtendUnit> ExtendLst = new List<ExtendUnit>();

        /// <summary>
        /// 启动命令行参数
        /// </summary>
        public static string CmdParaS = "";

        /// <summary>
        /// 指定配置文件的版本
        /// TODO：升级、修改XML文件时一定要升级改版本
        /// </summary>
        public static string XMLTargetVersion = "1.1";

        /// <summary>
        /// 主窗体句柄
        /// </summary>
        public static FrmMain Handle;

        /// <summary>
        /// 记录打开的文档数量
        /// </summary>
        public static int DocumentCount = 0;

        /// <summary>
        /// 字体缩放设置
        /// </summary>
        public static int ZoomLevel = 0;

        /// <summary>
        /// 代码区模块宽度
        /// </summary>
        public static float CodeBlock_Width { get; set; }

        /// <summary>
        /// 行号宽度值
        /// </summary>
        public static int Line_Numbers_Margin_Width = 30;

        /// <summary>
        /// 显示行号
        /// </summary>
        public static bool ShowLineCode = true;

        /// <summary>
        /// 保存最近浏览记录
        /// </summary>
        public static class RecentFiles
        {
            private static List<string> List = new List<string>();

            private static string CleanKey = "[清空记录]";

            /// <summary>
            /// 目标关联菜单
            /// </summary>
            public static ToolStripMenuItem TargetToolMenu;

            /// <summary>
            /// 写入关联菜单
            /// </summary>
            private static void Write2TargetMenu(string FilePath, ToolStripMenuItem TargetToolMenu)
            {
                ToolStripMenuItem TSMenu = new ToolStripMenuItem(FilePath);
                TSMenu.Click += delegate (object sender, EventArgs e)
                {
                    if (TSMenu.Text == CleanKey)
                    {
                        List.Clear();
                        SaveToDataBase();
                        TargetToolMenu.DropDownItems.Clear();
                        Write2TargetMenu(CleanKey, TargetToolMenu);
                    }
                    else
                    {
                        Function.CreatBATWindow(Handle, FilePath);
                    }
                };
                TargetToolMenu.DropDownItems.Insert(0, TSMenu);
            }

            /// <summary>
            /// 添加最近浏览记录
            /// </summary>
            /// <param name="PathCollection">以数组的形式添加</param>
            public static void Add(string[] PathCollection)
            {
                foreach (string PathStr in PathCollection)
                {
                    if (PathStr.Trim() != "")
                    {
                        List.Add(PathStr);
                        Write2TargetMenu(PathStr, TargetToolMenu);
                    }
                }
            }

            /// <summary>
            /// 添加最近浏览记录
            /// </summary>
            /// <param name="FilePath">文件路径</param>
            /// <param name="TargetToolMenu">目标关联菜单</param>
            /// <returns></returns>
            public static bool Add(string FilePath)
            {
                bool Exist = false;
                foreach (string Str in List)
                {
                    if (Str.ToUpper() == FilePath.ToUpper())
                    {
                        Exist = true;
                        break;
                    }
                }

                if (Exist)
                {
                    return false;
                }
                else
                {
                    List.Add(FilePath);
                    Write2TargetMenu(FilePath, TargetToolMenu);
                    return true;
                }
            }

            /// <summary>
            /// 从数据库中读取配置信息
            /// </summary>
            public static void ReadFromDataBase()
            {
                try
                {
                    //读取浏览记录
                    RecentFiles.Add(CleanKey);
                    string[] Result = DB.GetValues("SELECT Value FROM Config where Name='RecentFiles'", "Value")[0].Split('|');
                    //截取10个浏览记录
                    int Cut2Ten = Result.Length < 10 ? Result.Length : 10;
                    string[] NewResult = new string[Cut2Ten];
                    for (int i = 0; i < NewResult.Length; i++)
                    {
                        NewResult[i] = Result[i];
                    }
                    RecentFiles.Add(NewResult);

                    //读取软件使用时间
                    if (!DB.GetValues("SELECT Name FROM Config", "Name").Contains("UsingTime"))
                    {
                        DB.SqlCommand("INSERT INTO Config VALUES('UsingTime','0')");
                        UsingSeconds = 0;
                    }
                    else
                    {
                        UsingSeconds = Convert.ToInt64(DB.GetValues("SELECT Value FROM Config where Name='UsingTime'", "Value")[0]);
                    }
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message, "读取数据库失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            /// <summary>
            /// 保存浏览记录到数据库
            /// </summary>
            public static void SaveToDataBase()
            {
                StringBuilder SB = new StringBuilder();
                for (int i = 0; i < RecentFiles.List.Count; i++)
                {
                    string Temp = RecentFiles.List[i];
                    if (Temp != CleanKey)
                    {
                        SB.Append(Temp + "|");
                    }
                }
                DB.SqlCommand("update Config set Value='" + SB.ToString() + "' where Name='RecentFiles'");
            }
        }

        public static string XMLName = @"\UserConfig.xml";

        /// <summary>
        /// 保存配置文件
        /// </summary>
        /// <returns></returns>
        public static bool SaveCodeBaseXML(bool Default = false)
        {
            try
            {
                using (XmlTextWriter lXmlWriter = new XmlTextWriter(Application.StartupPath + XMLName, Encoding.Default))
                {
                    lXmlWriter.Formatting = Formatting.Indented;
                    lXmlWriter.WriteStartDocument();//开始
                    lXmlWriter.WriteStartElement("iBAT");//总根节点

                    //配置清单
                    if (Default)
                    {
                        lXmlWriter.WriteStartElement("Setting");
                        lXmlWriter.WriteElementString("XMLVersion", XMLTargetVersion);//XML版本
                        lXmlWriter.WriteElementString("FileBar", "True");
                        lXmlWriter.WriteElementString("CodeBlock", "True");
                        lXmlWriter.WriteElementString("StatusBar", "True");
                        lXmlWriter.WriteElementString("Menu", "True");
                        lXmlWriter.WriteElementString("WordWrap", "True");
                        lXmlWriter.WriteElementString("Zoom", "0");
                        lXmlWriter.WriteElementString("LineCode", "True");
                        lXmlWriter.WriteElementString("ToolBar", "True");
                    }
                    else
                    {
                        lXmlWriter.WriteStartElement("Setting");
                        lXmlWriter.WriteElementString("XMLVersion", XMLTargetVersion);//XML版本
                        lXmlWriter.WriteElementString("FileBar", Handle.ToolMenu_FileBar.Checked.ToString());
                        lXmlWriter.WriteElementString("CodeBlock", Handle.ToolMenu_CodeBlock.Checked.ToString());
                        lXmlWriter.WriteElementString("StatusBar", Handle.ToolMenu_StatusBar.Checked.ToString());
                        lXmlWriter.WriteElementString("Menu", Handle.ToolMenu_Menu.Checked.ToString());
                        lXmlWriter.WriteElementString("WordWrap", Handle.ToolMenu_WordWrap.Checked.ToString());
                        lXmlWriter.WriteElementString("Zoom", ZoomLevel.ToString());
                        lXmlWriter.WriteElementString("LineCode", Handle.ToolMenu_LineCode.Checked.ToString());
                        lXmlWriter.WriteElementString("ToolBar", Handle.ToolMenu_Toolbar.Checked.ToString());
                    }

                    lXmlWriter.WriteEndElement();//节点关闭


                    lXmlWriter.WriteEndElement();//总根节点关闭
                    return true;
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message + Environment.NewLine + "配置文件保存失败", "出错", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        /// <summary>
        /// 加载配置信息
        /// </summary>
        public static bool LoadSettingXML()
        {
            //创建新的xml文件
            if (!File.Exists(Application.StartupPath + XMLName))
            {
                SaveCodeBaseXML(true);
                return true;
            }

            //读取xml文件
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(Application.StartupPath + XMLName);

                XmlNodeList ItemList;
                XmlNodeList Lst;

                //加载配置文件
                ItemList = doc.SelectNodes("iBAT/Setting");


                if (ItemList.Count == 1)
                {
                    //读取配置
                    Lst = ItemList[0].ChildNodes;

                    //配置文档长度改变时（一般发生在升级配置文档时）重置配置数据
                    if (Lst.Count != 9)
                    {
                        MessageBox.Show("配置文件更新完成，重新打开软件生效。");
                        SaveCodeBaseXML(true);
                        return true;
                    }

                    //获取参数信息
                    Handle.ToolMenu_FileBar.Checked = Convert.ToBoolean(Lst[1].InnerText);
                    Handle.ToolMenu_CodeBlock.Checked = Convert.ToBoolean(Lst[2].InnerText);
                    Handle.ToolMenu_StatusBar.Checked = Convert.ToBoolean(Lst[3].InnerText);
                    Handle.ToolMenu_Menu.Checked = Convert.ToBoolean(Lst[4].InnerText);
                    Handle.ToolMenu_WordWrap.Checked = Convert.ToBoolean(Lst[5].InnerText);
                    ZoomLevel = Convert.ToInt16(Lst[6].InnerText);
                    Handle.ToolMenu_LineCode.Checked = Convert.ToBoolean(Lst[7].InnerText);
                    ShowLineCode = Convert.ToBoolean(Convert.ToBoolean(Lst[7].InnerText));
                    Handle.ToolMenu_Toolbar.Checked = Convert.ToBoolean(Lst[8].InnerText);

                    //根据参数设定软件运行环境
                    Handle.Check_Statu_View();
                }

                return true;
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message + Environment.NewLine + "配置文件读取失败", "出错", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        /// <summary>
        /// Get Files' info of User Group
        /// </summary>
        /// <returns></returns>
        public static List<FileSystemInfo> GetUserGroupNames()
        {
            List<FileSystemInfo> FinalFile = new List<FileSystemInfo>();

            if (!Directory.Exists(Folder_UserCodeGroup)) Directory.CreateDirectory(Folder_UserCodeGroup);

            DirectoryInfo GroupFolder = new DirectoryInfo(Folder_UserCodeGroup);

            foreach (FileSystemInfo FSI in GroupFolder.GetFileSystemInfos())
            {
                if (FSI.Extension.ToLower() == ".db")
                {
                    FinalFile.Add(FSI);
                }
            }

            return FinalFile;
        }

        /// <summary>
        /// 根据名称定位扩展插件
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static ExtendUnit FindInExtendLst(string Name)
        {
            ExtendUnit Temp = null;

            foreach (ExtendUnit EU in Setting.ExtendLst)
            {
                if (EU.Name == Name)
                {
                    Temp = EU;
                    break;
                }
            }

            return Temp;
        }

        /// <summary>
        /// 加载外部扩展插件
        /// </summary>
        public static void LoadExtendTools(ToolStripMenuItem TargetMenu)
        {
            try
            {
                if (!Directory.Exists(Folder_Extend)) Directory.CreateDirectory(Folder_Extend);

                //反序列化加载配置文件 
                if (File.Exists(Setting.File_ConfigBin))
                {
                    BinaryFormatter Bf = new BinaryFormatter();
                    using (FileStream Fs = new FileStream(Setting.File_ConfigBin, FileMode.Open, FileAccess.Read))
                    {
                        Setting.ExtendLst = (List<ExtendUnit>)Bf.Deserialize(Fs);
                    }
                }

                //加载扩展列表菜单
                ExtendMenuReload(TargetMenu);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "加载外部扩展组件失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 加载扩展列表菜单
        /// </summary>
        /// <param name="TM"></param>
        public static void ExtendMenuReload(ToolStripMenuItem TM)
        {
            TM.DropDownItems.Clear();

            string SettingMenuNotice = "设置";
            ToolStripMenuItem SettingMenu = new ToolStripMenuItem(SettingMenuNotice);
            SettingMenu.Click += delegate (object sender, EventArgs e)
            {
                FrmExtendSetting FS = new FrmExtendSetting();
                FS.ShowDialog();
                //刷新列表
                ExtendMenuReload(TM);
            };
            TM.DropDownItems.Insert(0, SettingMenu);

            //添加插件
            DirectoryInfo GroupFolder = new DirectoryInfo(Folder_Extend);
            foreach (FileSystemInfo FSI in GroupFolder.GetFileSystemInfos())
            {
                if (Path.GetFileName(FSI.FullName) == Path.GetFileName(File_ConfigBin)) { continue; }

                string Name = Path.GetFileNameWithoutExtension(FSI.FullName);
                ToolStripMenuItem TSMenu = new ToolStripMenuItem(Name);
                ExtendUnit EU = FindInExtendLst(Name);
                TSMenu.Click += delegate (object sender, EventArgs e)
                {
                    try
                    {
                        ProcessStartInfo Pro = new ProcessStartInfo(FSI.FullName);
                        Pro.WorkingDirectory = Application.StartupPath;
                        if (EU != null)
                        {
                            if (EU.Para.Trim() != "")
                            {
                                Pro.Arguments = EU.Para.Trim();
                            }
                        }
                        Process.Start(Pro);
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show(E.Message, "插件启动时遇到问题", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                };
                if (EU != null && EU.Hotkey.Trim() != "")
                {
                    //计算热键
                    int HotKeyNumber = 0;
                    if (EU.Hotkey.Contains("Ctrl")) HotKeyNumber |= 131072;
                    if (EU.Hotkey.Contains("Shift")) HotKeyNumber |= 65536;
                    if (EU.Hotkey.Contains("Alt")) HotKeyNumber |= 262144;
                    string TheLast = EU.Hotkey.Replace(" ", "").Replace("+", "").Replace("Ctrl", "").Replace("Shift", "").Replace("Alt", "");
                    if (TheLast.Contains("F"))
                    {
                        TheLast = TheLast.Remove(0, 1);
                        HotKeyNumber |= (111 + Convert.ToInt16(TheLast));
                    }
                    else
                    {
                        Char TheLastChar = Convert.ToChar(TheLast);
                        HotKeyNumber |= Convert.ToInt32(TheLastChar);
                    }
                    TSMenu.ShortcutKeys = (System.Windows.Forms.Keys)HotKeyNumber;//绑定热键
                }
                TM.DropDownItems.Insert(0, TSMenu);

                //添加进ExtenLst组
                ExtendUnit Ready2EnterLstUnit = Setting.FindInExtendLst(Name);
                if (Ready2EnterLstUnit == null)
                {
                    Ready2EnterLstUnit = new ExtendUnit();
                    Ready2EnterLstUnit.Name = Name;
                    Ready2EnterLstUnit.Para = "";
                    Ready2EnterLstUnit.Hotkey = "";
                }
                if (!ExtendLst.Contains(Ready2EnterLstUnit)) ExtendLst.Add(Ready2EnterLstUnit);
            }
        }

        /// <summary>
        /// 加载用户组
        /// </summary>
        /// <returns></returns>
        public static bool LoadUserCodeGroup()
        {
            try
            {
                List<FileSystemInfo> GroupFiles = GetUserGroupNames();
                Handle.listView_UserCodeGroup.Items.Clear();

                foreach (FileSystemInfo SingleFile in GroupFiles)
                {
                    try
                    {
                        using (SQLiteConnection conn = new SQLiteConnection())
                        {
                            SQLiteConnectionStringBuilder SCS = new SQLiteConnectionStringBuilder();
                            SCS.DataSource = SingleFile.FullName;
                            conn.ConnectionString = SCS.ToString();

                            conn.Open();

                            //添加组
                            ListViewGroup LvGroup = new ListViewGroup(Path.GetFileNameWithoutExtension(SingleFile.Name));
                            Handle.listView_UserCodeGroup.Groups.Add(LvGroup);

                            using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM CODE", conn))
                            {
                                using (SQLiteDataReader dr = cmd.ExecuteReader())
                                {
                                    while (dr.Read())
                                    {
                                        try
                                        {
                                            Handle.listView_UserCodeGroup.Items.Add(new ListViewItem(new string[] { Function.base64Decode(dr["Title"].ToString()), dr["GUID"].ToString() }, LvGroup));
                                        }
                                        catch (Exception E)
                                        {
                                            Setting.ConsoleErrorLog("Message:" + E.Message + Environment.NewLine + "StackTrace:" + E.StackTrace + "Source:" + E.Source);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show("用户代码组加载出错" + Environment.NewLine + E.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        /// <summary>
        /// 加载用户组
        /// </summary>
        /// <returns></returns>
        public static UserCodeUnit LoadUserCodeGroup(string GroupName, string GUID)
        {
            UserCodeUnit UCU = new UserCodeUnit();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection())
                {
                    SQLiteConnectionStringBuilder SCS = new SQLiteConnectionStringBuilder();
                    SCS.DataSource = Folder_UserCodeGroup + GroupName + ".db";
                    conn.ConnectionString = SCS.ToString();

                    conn.Open();

                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM CODE WHERE GUID='" + GUID + "'", conn))
                    {
                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                try
                                {
                                    UCU.Title = Function.base64Decode(dr["Title"].ToString());
                                    UCU.Text = Function.base64Decode(dr["Code"].ToString());
                                    UCU.GUID = GUID;
                                }
                                catch (Exception E)
                                {
                                    Setting.ConsoleErrorLog("Message:" + E.Message + Environment.NewLine + "StackTrace:" + E.StackTrace + "Source:" + E.Source);
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

            return UCU;
        }

        /// <summary>
        /// 用户代码单元
        /// </summary>
        public struct UserCodeUnit
        {
            public string GUID;
            public string Title;
            public string Text;
        }

        /// <summary>
        /// 输出错误日志
        /// </summary>
        public static void ConsoleErrorLog(string ErrorInfo)
        {
            string OutPutPath = Application.StartupPath + @"\ErrorLog.txt";

            using (StreamWriter SW = new StreamWriter(OutPutPath, true))
            {
                SW.WriteLine(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]"));
                SW.WriteLine(ErrorInfo);
                SW.WriteLine(Environment.NewLine);
            }
        }
    }

    public static class Function
    {
        /// <summary>
        /// 坐标位置
        /// </summary>
        public static int Position;

        /// <summary>
        /// 文本内部改变时TabControl标题加[*]响应
        /// </summary>
        /// <param name="Handle"></param>
        private static void Event_Text_Change(FrmMain Handle)
        {
            try
            {
                if (!Handle.TabS_Scintilla.SelectedTab.Text.EndsWith("*"))
                {
                    Handle.TabS_Scintilla.SelectedTab.Text += "*";

                    //在文件列表处增加标识
                    Handle.listView_FileLst.Items[Handle.FindListFromFilePath(Handle.TabS_Scintilla.SelectedTab.Tag.ToString())].ForeColor = System.Drawing.Color.OrangeRed;
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 输出光标位置
        /// </summary>
        /// <param name="Handle"></param>
        public static void ShowPosition(FrmMain Handle)
        {
            foreach (Control c in Handle.TabS_Scintilla.SelectedTab.Controls)
            {
                if (c.GetType().ToString().Contains("Scintilla"))
                {
                    int Pos_X = 0;
                    int Pos_Y = 0;

                    if (((Scintilla)c).Selection.Text.Length > 0)
                    {
                        Handle.toolStrip_BBS.Text = ((Scintilla)c).Selection.Text.Length + " 个字符被选中";

                        if (!(((Scintilla)c).Selection.Text.Contains("\r") || ((Scintilla)c).Selection.Text.Contains("\n")))
                        {
                            //响应查询
                            Handle.ToolStrip_FindInDictionary.Visible = true;
                            Handle.toolStrip_Line.Visible = true;
                            Handle.ToolStrip_FindInDictionary.Text = "查询：" + ((Scintilla)c).Selection.Text.Trim();
                        }
                        else
                        {
                            Handle.ToolStrip_FindInDictionary.Visible = false;
                            Handle.toolStrip_Line.Visible = false;
                            Handle.ToolStrip_FindInDictionary.Text = "";
                        }
                    }
                    else
                    {
                        Handle.ToolStrip_FindInDictionary.Visible = false;
                        Handle.toolStrip_Line.Visible = false;

                        Pos_X = Convert.ToInt32(((Scintilla)c).Lines.Current.ToString().Replace("Line ", "")) + 1;
                        Pos_Y = ((Scintilla)c).Caret.Position - ((Scintilla)c).Lines.Current.StartPosition + 1;
                        Handle.toolStrip_BBS.ForeColor = System.Drawing.Color.Black;
                        Handle.toolStrip_BBS.Text = "行 " + Pos_X + ", 列 " + Pos_Y;

                        Scintilla Temp = (Scintilla)c;
                        Position = Temp.CurrentPos;
                    }

                    if (Setting.ShowLineCode)
                    {
                        //处理行号宽度
                        if (((Scintilla)c).Lines.Count > 999)
                        {
                            ((Scintilla)c).Margins[0].Width = ((Scintilla)c).Lines.Count.ToString().Length * 8 + 15;
                        }
                        else
                        {
                            ((Scintilla)c).Margins[0].Width = Setting.Line_Numbers_Margin_Width;
                        }
                    }
                    else
                    {
                        ((Scintilla)c).Margins[0].Width = 0;
                    }
                }
            }
        }

        /// <summary>
        /// 执行批处理指令
        /// </summary>
        /// <param name="TargetFilePath">批处理文件路径</param>
        /// <param name="RunAsAdministrator">以管理员身份运行</param>
        public static void RunCMD(string TargetFilePath, bool RunAsAdministrator = false)
        {
            try
            {
                if (!File.Exists(TargetFilePath)) throw new Exception("启动失败，文件尚未保存");
                Process process = new Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.WorkingDirectory = Path.GetDirectoryName(TargetFilePath);
                if (RunAsAdministrator) process.StartInfo.Verb = "runas";
                process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                string WhileEnd = Setting.Handle.ToolStripMenu_ReturnWhileEnd.Checked ? "/K" : "/C";
                if (Setting.CmdParaS.Trim() != "") WhileEnd += @" " + Setting.CmdParaS.Trim();
                process.StartInfo.Arguments = WhileEnd + @" """ + TargetFilePath + @"""";
                process.Start();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        /// <summary>
        /// 执行批处理指令（测试代码）
        /// </summary>
        /// <param name="Document"></param>
        public static void RunForTest(string Document)
        {
            try
            {
                //生成临时文件
                string tempPath = Path.GetTempPath();
                string Bat_Filename = tempPath + Path.GetRandomFileName() + ".BAT";

                using (FileStream fs = new FileStream(Bat_Filename, FileMode.CreateNew, FileAccess.Write))
                {
                    using (StreamWriter m_streamWriter = new StreamWriter(fs, System.Text.Encoding.Default))
                    {
                        m_streamWriter.Write(Document);
                    }
                }

                RunCMD(Bat_Filename);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 关闭所有批处理文件
        /// </summary>
        public static void KillAllCMDs()
        {
            try
            {
                Process[] process = Process.GetProcessesByName("CMD");
                foreach (Process P in process)
                {
                    P.Kill();
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 创建Scintilla窗体
        /// 在这里可以针对Scintilla进行默认初始化的设定，比如行号
        /// </summary>
        private static Scintilla CreatDocumentWindow(FrmMain Handle)
        {
            Scintilla CreatMsg = new Scintilla();
            //字体大小
            CreatMsg.ZoomFactor = Setting.ZoomLevel;
            //行号和标识
            CreatMsg.Margins.Margin0.Width = Setting.Line_Numbers_Margin_Width;
            CreatMsg.Margins.Margin1.AutoToggleMarkerNumber = 0;
            CreatMsg.Margins.Margin1.IsClickable = true;
            CreatMsg.Margins.Margin1.Width = 16;
            CreatMsg.Margins.Margin2.Width = 8;
            //当前行高亮
            CreatMsg.Caret.CurrentLineBackgroundColor = System.Drawing.Color.LightGoldenrodYellow;
            CreatMsg.Caret.HighlightCurrentLine = true;
            CreatMsg.LineWrapping.Mode = LineWrappingMode.Word;
            CreatMsg.Dock = DockStyle.Fill;
            CreatMsg.LineWrapping.Mode = Handle.ToolMenu_WordWrap.Checked ? LineWrappingMode.Word : LineWrappingMode.None;
            CreatMsg.Margins[0].Type = Setting.ShowLineCode ? MarginType.Number : MarginType.Symbol;

            return CreatMsg;
        }

        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="FilePath">目标文件路径</param>
        /// <returns></returns>
        public static string ReadFromFile(string FilePath)
        {
            using (StreamReader Sr = new StreamReader(FilePath, System.Text.Encoding.GetEncoding(936), true))
            {
                return Sr.ReadToEnd();
            }
        }

        /// <summary>
        /// 保存批处理文件
        /// </summary>
        /// <returns></returns>
        public static string SaveBAT(string Document, string SavePath = null)
        {
            try
            {
                bool DoSave = false;
                if (SavePath == null)
                {
                    SaveFileDialog Sfd = new SaveFileDialog();
                    Sfd.Title = "命名要保存的代码文件";
                    Sfd.Filter = "BAT|*.BAT|CMD|*.CMD";
                    if (Sfd.ShowDialog() == DialogResult.OK)
                    {
                        SavePath = Sfd.FileName;
                        DoSave = true;
                    }
                }
                else
                {
                    DoSave = true;
                }

                if (DoSave)
                {
                    using (FileStream fs = new FileStream(SavePath, FileMode.Create, FileAccess.Write))
                    {
                        using (StreamWriter m_streamWriter = new StreamWriter(fs, System.Text.Encoding.GetEncoding(936)))
                        {
                            m_streamWriter.Write(Document);
                        }
                    }
                }

                return SavePath;
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return null;
            }
        }

        /// <summary>
        /// Base64编码
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string base64Encode(string data)
        {
            try
            {
                byte[] encData_byte = new byte[data.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(data);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Encode" + e.Message);
            }
        }

        /// <summary>
        /// Base64解码
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string base64Decode(string data)
        {
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(data);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Decode" + e.Message);
            }
        }

        /// <summary>
        /// 用户代码组
        /// </summary>
        public static class UserCodeGroup
        {
            /// <summary>
            /// 创建用户代码组
            /// </summary>
            /// <param name="GroupName"></param>
            /// <returns></returns>
            public static bool Creat(string GroupName)
            {
                try
                {
                    if (File.Exists(Setting.Folder_UserCodeGroup + GroupName + ".db")) throw new Exception("该组已经存在，不要重复添加");

                    if (!Directory.Exists(Setting.Folder_UserCodeGroup)) Directory.CreateDirectory(Setting.Folder_UserCodeGroup);

                    using (SQLiteConnection conn = new SQLiteConnection())
                    {
                        SQLiteConnectionStringBuilder SCS = new SQLiteConnectionStringBuilder();
                        SCS.DataSource = Setting.Folder_UserCodeGroup + GroupName + ".db";
                        conn.ConnectionString = SCS.ToString();

                        conn.Open();

                        string Bazinga = "create table [CODE] (GUID nvarchar(64),Title nvarchar(1024),Code TEXT COLLATE NOCASE,";
                        Bazinga += @"PRIMARY KEY ([GUID]))";
                        DbCommand cmd = conn.CreateCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = Bazinga;
                        cmd.ExecuteNonQuery();
                    }
                    Setting.LoadUserCodeGroup();//刷新代码组
                    return true;
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }

            /// <summary>
            /// 删除用户代码组
            /// </summary>
            /// <param name="GroupName"></param>
            /// <returns></returns>
            public static bool Delete(string GroupName)
            {
                try
                {
                    File.Delete(Setting.Folder_UserCodeGroup + GroupName + ".db");
                    Setting.LoadUserCodeGroup();//刷新代码组
                    return true;
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }

            /// <summary>
            /// 代码单条
            /// </summary>
            public static class CODEItem
            {
                static bool Operate(string GroupName, string CommandText)
                {
                    try
                    {
                        using (SQLiteConnection conn = new SQLiteConnection())
                        {
                            SQLiteConnectionStringBuilder SCS = new SQLiteConnectionStringBuilder();
                            SCS.DataSource = Setting.Folder_UserCodeGroup + GroupName + ".db";
                            conn.ConnectionString = SCS.ToString();

                            conn.Open();

                            DbCommand cmd = conn.CreateCommand();
                            cmd.Connection = conn;
                            cmd.CommandText = CommandText;
                            cmd.ExecuteNonQuery();
                        }

                        Setting.LoadUserCodeGroup();//刷新代码组

                        return true;
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show(E.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }

                public static bool Add(string GroupName, string Title, string Code)
                {
                    Title = base64Encode(Title);
                    Code = base64Encode(Code);

                    return Operate(GroupName, "INSERT INTO [CODE] (title,Code) VALUES (" + Title + "," + Code + ")");
                }

                public static bool Rename(string GroupName, string GUID, string NewName)
                {
                    NewName = base64Encode(NewName);

                    return Operate(GroupName, "UPDATE [CODE] SET Title='" + NewName + "' WHERE GUID='" + GUID + "'");
                }

                public static bool Remove(string GroupName, string GUID)
                {
                    return Operate(GroupName, "DELETE FROM [CODE] WHERE GUID='" + GUID + "'");
                }

                public static bool Change(string GroupName, string Title, string Code, string GUID = null)
                {
                    Title = base64Encode(Title);
                    Code = base64Encode(Code);

                    if (GUID == null)
                    {
                        return Operate(GroupName, "INSERT INTO [CODE] (GUID,Title,Code) VALUES ('" + Guid.NewGuid().ToString("N") + "','" + Title + "','" + Code + "')");
                    }
                    else
                    {
                        return Operate(GroupName, "UPDATE CODE SET Title='" + Title + "',Code='" + Code + "' WHERE GUID='" + GUID + "'");
                    }
                }
            }
        }

        /// <summary>
        /// 创建新窗口
        /// </summary>
        /// <returns></returns>
        public static bool CreatBATWindow(FrmMain Handle)
        {
            try
            {
                Scintilla rtMessages = CreatDocumentWindow(Handle);

                string FileHash = "@" + Guid.NewGuid().ToString("N");

                TabPage chatRecord = new TabPage("未命名");
                chatRecord.Tag = FileHash;

                //添加到文件清单列表
                Handle.listView_FileLst.Items.Add(new ListViewItem(new string[] { "未命名", FileHash }));

                //添加到选项卡
                Handle.TabS_Scintilla.Controls.Add(chatRecord);
                Handle.TabS_Scintilla.SelectTab(chatRecord);
                chatRecord.Controls.Add(rtMessages);

                rtMessages.Text = "@echo off" + Environment.NewLine + "Setlocal enabledelayedexpansion" + Environment.NewLine + "::CODER BY " + Environment.UserName + " POWERD BY iBAT" + Environment.NewLine + Environment.NewLine + "pause";
                rtMessages.TextChanged += delegate (object sender, EventArgs e) { Event_Text_Change(Handle); };
                rtMessages.Paint += delegate (object sender, PaintEventArgs e) { ShowPosition(Handle); };
                rtMessages.ConfigurationManager.Language = "vbscript";

                //添加右键菜单
                rtMessages.ContextMenuStrip = Handle.contextMenuStrip_ScintillaText;

                Handle.TabPageConnectMenu();

                return true;
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "出错", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
        }

        /// <summary>
        /// 创建新窗口
        /// </summary>
        /// <param name="Path">从已经存在的文件</param>
        /// <returns></returns>
        public static bool CreatBATWindow(FrmMain Handle, string FilePath)
        {
            try
            {
                if (!File.Exists(FilePath)) throw new Exception("无法打开文件，可能已经被移动或删除");

                //判断是否已经存在与文件列表，如果存在，直接打开即可
                int FindExist = Handle.FindListFromFilePath(FilePath);
                if (FindExist != -1)
                {
                    Handle.listView_FileLst.Items[FindExist].Selected = true;
                    Handle.listView_FileLst.Items[FindExist].Focused = true;
                    return true;
                }

                Scintilla rtMessages = CreatDocumentWindow(Handle);

                string FileNameWithoutExtension = Path.GetFileNameWithoutExtension(FilePath);
                TabPage chatRecord = new TabPage(FileNameWithoutExtension);
                chatRecord.Tag = FilePath;

                //添加到文件清单列表
                Handle.listView_FileLst.Items.Add(new ListViewItem(new string[] { FileNameWithoutExtension, FilePath }));

                //添加到选项卡
                Handle.TabS_Scintilla.Controls.Add(chatRecord);
                Handle.TabS_Scintilla.SelectTab(chatRecord);
                chatRecord.Controls.Add(rtMessages);

                //添加进最近浏览记录
                Setting.RecentFiles.Add(FilePath);

                //添加文件内容
                rtMessages.Text = ReadFromFile(FilePath);

                rtMessages.TextChanged += delegate (object sender, EventArgs e) { Event_Text_Change(Handle); };
                rtMessages.Paint += delegate (object sender, PaintEventArgs e) { ShowPosition(Handle); };
                rtMessages.ConfigurationManager.Language = "vbscript";

                //添加右键菜单
                rtMessages.ContextMenuStrip = Handle.contextMenuStrip_ScintillaText;

                Handle.TabPageConnectMenu();

                CheckSameNameButDifferentPath(FileNameWithoutExtension);
                return true;
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        /// <summary>
        /// 检测同名但不同路径的文件
        /// </summary>
        public static void CheckSameNameButDifferentPath(string CurrentName)
        {
            List<int> SameCount = new List<int>();
            for (int i = 0; i < Setting.Handle.listView_FileLst.Items.Count; i++)
            {
                if (Path.GetFileNameWithoutExtension(Setting.Handle.listView_FileLst.Items[i].SubItems[1].Text) == CurrentName)
                {
                    SameCount.Add(i);
                }
            }

            if (SameCount.Count > 1)
            {
                foreach (int Index in SameCount)
                {
                    Setting.Handle.listView_FileLst.Items[Index].SubItems[0].Text = Setting.Handle.listView_FileLst.Items[Index].SubItems[1].Text;
                }
            }
            else if (SameCount.Count == 1)
            {
                Setting.Handle.listView_FileLst.Items[SameCount[0]].SubItems[0].Text = Path.GetFileNameWithoutExtension(Setting.Handle.listView_FileLst.Items[SameCount[0]].SubItems[1].Text);
            }
        }

        /// <summary>
        /// 代码块和函数
        /// </summary>
        public struct CodeAndFunction
        {
            /// <summary>
            /// 代码种类
            /// 枚举对应着数据库的表段，增加或修改时尤其注意！
            /// </summary>
            public enum CodeType { CodeBlock, Function };

            public static void Load(FrmMain Handle, CodeType EnumType)
            {
                using (SQLiteConnection conn = new SQLiteConnection())
                {
                    SQLiteConnectionStringBuilder SCS = new SQLiteConnectionStringBuilder();
                    SCS.DataSource = DB.db_Path;
                    conn.ConnectionString = SCS.ToString();

                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM " + EnumType, conn))
                    {
                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                try
                                {
                                    switch (EnumType)
                                    {
                                        case CodeType.CodeBlock: Handle.listView_CodeBlock.Items.Add(new ListViewItem(new string[] { dr["Name"].ToString(), dr["Code"].ToString() })); break;
                                        case CodeType.Function: Handle.listView_Function.Items.Add(new ListViewItem(new string[] { dr["Name"].ToString(), dr["Notice"].ToString() })); break;
                                    }
                                }
                                catch (Exception E)
                                {
                                    Setting.ConsoleErrorLog("Message:" + E.Message + Environment.NewLine + "StackTrace:" + E.StackTrace + "Source:" + E.Source);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public static class Document
    {
        /// <summary>
        /// 操作处理方法枚举
        /// </summary>
        public enum Modle { Sort, Reverse, Unique };

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="Handle">操作处理方法</param>
        /// <param name="Target">目标控件</param>
        /// <param name="OnlySelection">只处理选中部分</param>
        public static void Sequence(Modle Handle, ref Scintilla Target, bool OnlySelection)
        {
            try
            {
                Scintilla NewDocument;
                if (OnlySelection)
                {
                    //如果处理选中部分，NewDocument充当临时记录用的控件
                    NewDocument = new Scintilla();
                    NewDocument.Text = Target.Selection.Text;
                }
                else
                {
                    //如果处理全文，NewDocument指向目标句柄
                    NewDocument = Target;
                }

                List<string> AllDocument = new List<string>();

                for (int i = 0; i < NewDocument.Lines.Count; i++)
                {
                    string Temp = NewDocument.Lines[i].Text.Replace('\r', ' ').Replace('\n', ' ').Trim();
                    if (Temp != "") AllDocument.Add(Temp);
                }

                switch (Handle)
                {
                    case Modle.Sort: AllDocument.Sort(); break;
                    case Modle.Reverse: AllDocument.Reverse(); break;
                    case Modle.Unique:; AllDocument = Unique(AllDocument); break;
                }

                NewDocument.Text = "";

                for (int i = 0; i < AllDocument.Count; i++)
                {
                    NewDocument.InsertText(AllDocument[i]);

                    if (i != AllDocument.Count - 1) NewDocument.InsertText(Environment.NewLine);
                }

                if (OnlySelection) Target.Selection.Text = NewDocument.Text;
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        /// <summary>
        /// 排序方式（大小写敏感）
        /// </summary>
        /// <param name="Handle">操作处理方法</param>
        /// <param name="Target">目标控件</param>
        /// <param name="OnlySelection">之处理选中部分</param>
        public static void SequenceCaseSentive(Modle Handle, ref Scintilla Target, bool OnlySelection)
        {
            Scintilla NewDocument;
            if (OnlySelection)
            {
                NewDocument = new Scintilla();
                NewDocument.Text = Target.Selection.Text;
            }
            else
            {
                NewDocument = Target;
            }

            List<string> Str_Upper = new List<string>();
            List<string> Str_Lower = new List<string>();
            List<string> Str_Other = new List<string>();

            for (int i = 0; i < NewDocument.Lines.Count; i++)
            {
                string Temp = NewDocument.Lines[i].Text.Replace('\r', ' ').Replace('\n', ' ').Trim();
                if (Temp == "") continue;
                if (char.IsUpper(Temp[0]))
                {
                    Str_Upper.Add(Temp);
                }
                else if (char.IsLower(Temp[0]))
                {
                    Str_Lower.Add(Temp);
                }
                else
                {
                    Str_Other.Add(Temp);
                }
            }

            switch (Handle)
            {
                case Modle.Sort: Str_Upper.Sort(); Str_Lower.Sort(); Str_Other.Sort(); break;
                case Modle.Reverse: Str_Upper.Reverse(); Str_Lower.Reverse(); Str_Other.Reverse(); break;
                case Modle.Unique: Str_Other = Unique(Str_Other); break;
            }

            NewDocument.Text = "";

            for (int i = 0; i < Str_Upper.Count; i++)
            {
                NewDocument.InsertText(Str_Upper[i] + Environment.NewLine);
            }
            for (int i = 0; i < Str_Lower.Count; i++)
            {
                NewDocument.InsertText(Str_Lower[i] + Environment.NewLine);
            }
            for (int i = 0; i < Str_Other.Count; i++)
            {
                NewDocument.InsertText(Str_Other[i]);

                if (i != Str_Other.Count - 1) NewDocument.InsertText(Environment.NewLine);
            }

            if (OnlySelection) Target.Selection.Text = NewDocument.Text;
        }

        /// <summary>
        /// List去重
        /// </summary>
        /// <param name="Source"></param>
        /// <returns></returns>
        public static List<string> Unique(List<string> Source)
        {
            List<string> Temp = new List<string>();
            foreach (string Str in Source)
            {
                if (Temp.IndexOf(Str) == -1) Temp.Add(Str);
            }

            return Temp;
        }
    }

    /// <summary>
    /// 扩展插件单元
    /// </summary>
    [Serializable]
    public class ExtendUnit
    {
        /// <summary>
        /// 扩展名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 扩展参数
        /// </summary>
        public string Para { get; set; }

        /// <summary>
        /// 扩展单元快捷键
        /// </summary>
        public string Hotkey { get; set; }
    }

    public class SuperTabControl : System.Windows.Forms.TabControl
    {
        public SuperTabControl()
        {
            // 开启双缓冲
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            // Enable the OnNotifyMessage event so we get a chance to filter out 
            // Windows messages before they get to the form's WndProc
            this.SetStyle(ControlStyles.EnableNotifyMessage, true);
        }

        protected override void OnNotifyMessage(Message m)
        {
            //Filter out the WM_ERASEBKGND message
            if (m.Msg != 0x14)
            {
                base.OnNotifyMessage(m);
            }
        }
    }
}