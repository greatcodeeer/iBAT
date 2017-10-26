using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ScintillaNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace iBAT
{
    public partial class FrmMain : Form
    {
        CheckUpdateAndLoginS SS;//检测更新

        public FrmMain()
        {
            InitializeComponent();

            ConfigSetting();
            EventSetting();

            //处理启动参数
            String[] CmdArgs = Environment.GetCommandLineArgs();
            bool HasOpen = false;

            if (CmdArgs.Length > 1)
            {
                for (int i = 1; i < CmdArgs.Length; i++)
                {
                    FileInfo fileInfo = new FileInfo(CmdArgs[i]);
                    if (fileInfo.Length > 1024 * 1024 * 30)
                    {
                        MessageBox.Show("只允许打开以下小于30M的文件", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    if (Function.CreatBATWindow(this, CmdArgs[i])) HasOpen = true;
                }
            }

            if (!HasOpen) AddNew();

            //检查更新
            SS = new CheckUpdateAndLoginS(ProductVersion.ToString());
            SS.CheckUpdate(ToolMenu_NewVersion);
        }

        /// <summary>
        /// 选中项
        /// </summary>
        struct ChooseUserGroupItem
        {
            public string GroupName;
            public string Title;
            public string GUID;
        }
        ChooseUserGroupItem ChooseUserGroup = new ChooseUserGroupItem();

        #region Function Group

        /// <summary>
        /// 事件控制设定
        /// </summary>
        void EventSetting()
        {
            //新建
            ToolMenu_NewFile.Click += delegate (object sender, EventArgs e) { AddNew(); };
            ToolStripButton_New.Click += delegate (object sender, EventArgs e) { AddNew(); };

            //打开文件
            ToolMenu_OpenFile.Click += delegate (object sender, EventArgs e)
            {
                Thread oThread = new Thread(new ThreadStart(OpenFile));
                oThread.Start();
            };
            ToolStripButton_Open.Click += delegate (object sender, EventArgs e)
            {
                Thread oThread = new Thread(new ThreadStart(OpenFile));
                oThread.Start();
            };

            //保存
            ToolMenu_Save.Click += delegate (object sender, EventArgs e) { DoSave(TabS_Scintilla.SelectedTab); };
            ToolStripButton_Save.Click += delegate (object sender, EventArgs e) { DoSave(TabS_Scintilla.SelectedTab); };

            //另存为
            ToolMenu_SaveAs.Click += delegate (object sender, EventArgs e)
            {
                try
                {
                    //定位操作目标
                    Scintilla Target_Scintilla = FindScintillaWindow(TabS_Scintilla.SelectedTab);

                    string NewPath = Function.SaveBAT(Target_Scintilla.Text);
                    Function.CreatBATWindow(this, NewPath);
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            };

            //保存全部
            ToolMenu_SaveAll.Click += delegate (object sender, EventArgs e)
            {
                for (int i = 0; i < TabS_Scintilla.TabPages.Count; i++)
                {
                    DoSave(TabS_Scintilla.TabPages[i]);
                }
            };

            //关闭
            ToolMenu_CloseFile.Click += delegate (object sender, EventArgs e)
            {
                int ChooseAfterClose = TabS_Scintilla.SelectedIndex;
                if (DoClose(TabS_Scintilla.SelectedTab))
                {
                    TabS_Scintilla.SelectedIndex = ChooseAfterClose;
                }
            };

            //双击关闭
            TabS_Scintilla.MouseDoubleClick += delegate (object sender, MouseEventArgs e)
            {
                TabControl Temp = (TabControl)sender;
                Point pt = new Point(e.X, e.Y);
                for (int i = 0; i < Temp.TabCount; i++)
                {
                    Rectangle recTab = Temp.GetTabRect(i);
                    if (recTab.Contains(pt))
                    {
                        if (DoClose(TabS_Scintilla.SelectedTab))
                        {
                            TabS_Scintilla.SelectedIndex = TabS_Scintilla.SelectedIndex;
                        }

                        return;
                    }
                }
            };

            //关闭所有
            ToolMenu_CloseAllFiles.Click += delegate (object sender, EventArgs e) { CloseAll(); };

            //退出
            ToolMenu_Exit.Click += delegate (object sender, EventArgs e) { this.Close(); };

            //设置菜单
            //添加至系统菜单
            ToolMenu_ContextMenu.Click += delegate (object sender, EventArgs e)
            {
                string FileName = "SettingContext.exe";
                if (File.Exists(FileName))
                {
                    Process.Start(FileName, "add");
                }
                else
                {
                    MessageBox.Show("必备组件丢失，请下载iBAT完整版覆盖当前目录", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            };
            ToolMenu_RemoveContextMenu.Click += delegate (object sender, EventArgs e)
            {
                string FileName = "SettingContext.exe";
                if (File.Exists(FileName))
                {
                    Process.Start(FileName, "remove");
                }
                else
                {
                    MessageBox.Show("必备组件丢失，请下载iBAT完整版覆盖当前目录", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            };

            //撤销和重做
            ToolMenu_Undo.Click += delegate (object sender, EventArgs e)
            {
                FindScintillaWindow(TabS_Scintilla.TabPages[TabS_Scintilla.SelectedIndex]).UndoRedo.Undo();
            };
            ToolMenu_Redo.Click += delegate (object sender, EventArgs e)
            {
                FindScintillaWindow(TabS_Scintilla.TabPages[TabS_Scintilla.SelectedIndex]).UndoRedo.Redo();
            };
            ToolStripButton_Redo.Click += delegate (object sender, EventArgs e)
            {
                FindScintillaWindow(TabS_Scintilla.TabPages[TabS_Scintilla.SelectedIndex]).UndoRedo.Redo();
            };
            ToolStrip_Undo.Click += delegate (object sender, EventArgs e)
            {
                FindScintillaWindow(TabS_Scintilla.TabPages[TabS_Scintilla.SelectedIndex]).UndoRedo.Undo();
            };
            ToolStripButton_Undo.Click += delegate (object sender, EventArgs e)
            {
                FindScintillaWindow(TabS_Scintilla.TabPages[TabS_Scintilla.SelectedIndex]).UndoRedo.Undo();
            };

            //复制剪切粘贴
            ToolMenu_Copy.Click += delegate (object sender, EventArgs e)
            {
                FindScintillaWindow(TabS_Scintilla.TabPages[TabS_Scintilla.SelectedIndex]).Clipboard.Copy();
            };
            ToolStripButton_Copy.Click += delegate (object sender, EventArgs e)
            {
                FindScintillaWindow(TabS_Scintilla.TabPages[TabS_Scintilla.SelectedIndex]).Clipboard.Copy();
            };
            ToolMenu_Cut.Click += delegate (object sender, EventArgs e)
            {
                FindScintillaWindow(TabS_Scintilla.TabPages[TabS_Scintilla.SelectedIndex]).Clipboard.Cut();
            };
            ToolStripButton_Cut.Click += delegate (object sender, EventArgs e)
            {
                FindScintillaWindow(TabS_Scintilla.TabPages[TabS_Scintilla.SelectedIndex]).Clipboard.Cut();
            };
            ToolMenu_Paste.Click += delegate (object sender, EventArgs e)
            {
                FindScintillaWindow(TabS_Scintilla.TabPages[TabS_Scintilla.SelectedIndex]).Clipboard.Paste();
            };
            ToolStripButton_Paste.Click += delegate (object sender, EventArgs e)
            {
                FindScintillaWindow(TabS_Scintilla.TabPages[TabS_Scintilla.SelectedIndex]).Clipboard.Paste();
            };

            ToolStrip_Cut.Click += delegate (object sender, EventArgs e)
            {
                FindScintillaWindow(TabS_Scintilla.TabPages[TabS_Scintilla.SelectedIndex]).Clipboard.Cut();
            };
            ToolStrip_Copy.Click += delegate (object sender, EventArgs e)
            {
                FindScintillaWindow(TabS_Scintilla.TabPages[TabS_Scintilla.SelectedIndex]).Clipboard.Copy();
            };
            ToolStrip_Paste.Click += delegate (object sender, EventArgs e)
            {
                FindScintillaWindow(TabS_Scintilla.TabPages[TabS_Scintilla.SelectedIndex]).Clipboard.Paste();
            };

            //插入字符
            ToolMenu_Insert_RepeatCharacter.Click += delegate (object sender, EventArgs e)
            {
                FrmNxN NxN = new FrmNxN(FindScintillaWindow(TabS_Scintilla.TabPages[TabS_Scintilla.SelectedIndex]));
                NxN.ShowDialog();
            };
            ToolMenu_Insert_IncreasingSequence.Click += delegate (object sender, EventArgs e)
            {
                Frm123456789 Frm = new Frm123456789(FindScintillaWindow(TabS_Scintilla.TabPages[TabS_Scintilla.SelectedIndex]));
                Frm.ShowDialog();
            };

            //大小写转换
            ToolMenu_ConvertCase_UpperCase.Click += delegate (object sender, EventArgs e)
            {
                FindScintillaWindow(TabS_Scintilla.TabPages[TabS_Scintilla.SelectedIndex]).Commands.Execute(BindableCommand.UpperCase);
            };
            ToolMenu_ConvertCase_LowerCase.Click += delegate (object sender, EventArgs e)
            {
                FindScintillaWindow(TabS_Scintilla.TabPages[TabS_Scintilla.SelectedIndex]).Commands.Execute(BindableCommand.LowerCase);
            };
            ToolMenu_ConvertCase_SwapCase.Click += delegate (object sender, EventArgs e)
            {
                string Temp = FindScintillaWindow(TabS_Scintilla.TabPages[TabS_Scintilla.SelectedIndex]).Selection.Text;
                StringBuilder SB = new StringBuilder();
                for (int i = 0; i < Temp.Length; i++)
                {
                    if (char.IsUpper(Temp[i]))
                    {
                        SB.Append(char.ToLower(Temp[i]));
                    }
                    else
                    {
                        SB.Append(char.ToUpper(Temp[i]));
                    }
                }
                FindScintillaWindow(TabS_Scintilla.TabPages[TabS_Scintilla.SelectedIndex]).Selection.Text = SB.ToString();
            };

            //标记
            ToolMenu_Mark_SetMark.Click += delegate (object sender, EventArgs e)
            {
                Scintilla Target = FindScintillaWindow(TabS_Scintilla.TabPages[TabS_Scintilla.SelectedIndex]);
                Line currentLine = Target.Lines.Current;
                if (Target.Markers.GetMarkerMask(currentLine) == 0)
                {
                    currentLine.AddMarker(0);
                }
                else
                {
                    //currentLine.DeleteMarker(0);
                }
            };
            ToolMenu_Mark_DeleteMark.Click += delegate (object sender, EventArgs e)
            {
                Scintilla Target = FindScintillaWindow(TabS_Scintilla.TabPages[TabS_Scintilla.SelectedIndex]);
                Line currentLine = Target.Lines.Current;
                if (Target.Markers.GetMarkerMask(currentLine) == 0)
                {
                    //currentLine.AddMarker(0);
                }
                else
                {
                    currentLine.DeleteMarker(0);
                }
            };
            ToolMenu_Mark_ClearMark.Click += delegate (object sender, EventArgs e)
            {
                Scintilla Target = FindScintillaWindow(TabS_Scintilla.TabPages[TabS_Scintilla.SelectedIndex]);
                Target.Markers.DeleteAll(0);
            };

            //排序
            ToolMenu_SortLines.Click += delegate (object sender, EventArgs e)
            {
                Scintilla Temp = FindScintillaWindow(TabS_Scintilla.TabPages[TabS_Scintilla.SelectedIndex]);
                Document.Sequence(Document.Modle.Sort, ref Temp, false);
            };
            ToolMenu_SortLinesCaseSentive.Click += delegate (object sender, EventArgs e)
            {
                Scintilla Temp = FindScintillaWindow(TabS_Scintilla.TabPages[TabS_Scintilla.SelectedIndex]);
                Document.SequenceCaseSentive(Document.Modle.Sort, ref Temp, false);
            };
            ToolMenu_L_Reverse.Click += delegate (object sender, EventArgs e)
            {
                Scintilla Temp = FindScintillaWindow(TabS_Scintilla.TabPages[TabS_Scintilla.SelectedIndex]);
                Document.SequenceCaseSentive(Document.Modle.Reverse, ref Temp, false);
            };
            ToolMenu_L_Unique.Click += delegate (object sender, EventArgs e)
            {
                Scintilla Temp = FindScintillaWindow(TabS_Scintilla.TabPages[TabS_Scintilla.SelectedIndex]);
                Document.Sequence(Document.Modle.Unique, ref Temp, false);
            };
            ToolMenu_S_Sort.Click += delegate (object sender, EventArgs e)
            {
                Scintilla Temp = FindScintillaWindow(TabS_Scintilla.TabPages[TabS_Scintilla.SelectedIndex]);
                Document.Sequence(Document.Modle.Sort, ref Temp, true);
            };
            ToolMenu_S_SortCaseSentive.Click += delegate (object sender, EventArgs e)
            {
                Scintilla Temp = FindScintillaWindow(TabS_Scintilla.TabPages[TabS_Scintilla.SelectedIndex]);
                Document.SequenceCaseSentive(Document.Modle.Sort, ref Temp, true);
            };
            ToolMenu_S_Reverse.Click += delegate (object sender, EventArgs e)
            {
                Scintilla Temp = FindScintillaWindow(TabS_Scintilla.TabPages[TabS_Scintilla.SelectedIndex]);
                Document.SequenceCaseSentive(Document.Modle.Reverse, ref Temp, true);
            };
            ToolMenu_S_Unique.Click += delegate (object sender, EventArgs e)
            {
                Scintilla Temp = FindScintillaWindow(TabS_Scintilla.TabPages[TabS_Scintilla.SelectedIndex]);
                Document.SequenceCaseSentive(Document.Modle.Unique, ref Temp, true);
            };

            //查找和替换
            ToolMenu_Find.Click += delegate (object sender, EventArgs e)
            {
                Scintilla Temp = FindScintillaWindow(TabS_Scintilla.TabPages[TabS_Scintilla.SelectedIndex]);
                Temp.FindReplace.ShowFind();
            };
            ToolMenu_Replace.Click += delegate (object sender, EventArgs e)
            {
                Scintilla Temp = FindScintillaWindow(TabS_Scintilla.TabPages[TabS_Scintilla.SelectedIndex]);
                Temp.FindReplace.ShowReplace();
            };

            //视图模块
            ToolMenu_FileBar.Click += delegate (object sender, EventArgs e)
            {
                ToolMenu_FileBar.Checked = !ToolMenu_FileBar.Checked;
                Check_Statu_View();
            };
            ToolMenu_CodeBlock.Click += delegate (object sender, EventArgs e)
            {
                ToolMenu_CodeBlock.Checked = !ToolMenu_CodeBlock.Checked;
                Check_Statu_View();
            };
            ToolMenu_StatusBar.Click += delegate (object sender, EventArgs e)
            {
                ToolMenu_StatusBar.Checked = !ToolMenu_StatusBar.Checked;
                Check_Statu_View();
            };
            ToolMenu_Menu.Click += delegate (object sender, EventArgs e)
            {
                ToolMenu_Menu.Checked = !ToolMenu_Menu.Checked;
                Check_Statu_View();
            };
            ToolMenu_Toolbar.Click += delegate (object sender, EventArgs e)
            {
                ToolMenu_Toolbar.Checked = !ToolMenu_Toolbar.Checked;
                Check_Statu_View();
            };
            ToolMenu_LineCode.Click += delegate (object sender, EventArgs e)
            {
                ToolMenu_LineCode.Checked = !ToolMenu_LineCode.Checked;
                Check_Statu_View();
            };
            menuStrip1.MouseLeave += delegate (object sender, EventArgs e)
            {
                if (!ToolMenu_Menu.Checked) menuStrip1.Visible = false;
            };

            //自动换行
            ToolMenu_WordWrap.Click += delegate (object sender, EventArgs e)
            {
                ToolMenu_WordWrap.Checked = !ToolMenu_WordWrap.Checked;
                Check_Statu_View();
            };

            //字体放大缩小
            ToolMenu_ZoomIn.Click += delegate (object sender, EventArgs e)
            {
                if (Setting.ZoomLevel > -10)
                {
                    Setting.ZoomLevel--;
                    ApplyForAllTabs(EnumForApplyAllTabs.ZoomUpdate);
                }
            };
            ToolMenu_ZoomOut.Click += delegate (object sender, EventArgs e)
            {
                if (Setting.ZoomLevel < 20)
                {
                    Setting.ZoomLevel++;
                    ApplyForAllTabs(EnumForApplyAllTabs.ZoomUpdate);
                }
            };
            ToolMenu_ZoomReset.Click += delegate (object sender, EventArgs e)
            {
                Setting.ZoomLevel = 0;
                ApplyForAllTabs(EnumForApplyAllTabs.ZoomUpdate);
            };

            //Debug模块
            ToolMenu_Run.Click += delegate (object sender, EventArgs e) { ProcessTheBAT(); };
            ToolStripButton_Run.Click += delegate (object sender, EventArgs e) { ProcessTheBAT(); };

            ToolMenu_RunAsAdministrator.Click += delegate (object sender, EventArgs e) { ProcessTheBAT(true); };
            ToolStripButton_System.Click += delegate (object sender, EventArgs e) { ProcessTheBAT(true); };

            ToolMenu_ForceClose.Click += delegate (object sender, EventArgs e) { Function.KillAllCMDs(); };
            forceCloseWhenMoreThan10ProcessToolStripMenuItem.Click += delegate (object sender, EventArgs e)
            {
                forceCloseWhenMoreThan10ProcessToolStripMenuItem.Checked = !forceCloseWhenMoreThan10ProcessToolStripMenuItem.Checked;
            };
            ToolMenu_RunForTest.Click += delegate (object sender, EventArgs e) { Function.RunForTest(FindScintillaWindow(TabS_Scintilla.SelectedTab).Text); };
            ToolStripButton_Test.Click += delegate (object sender, EventArgs e) { Function.RunForTest(FindScintillaWindow(TabS_Scintilla.SelectedTab).Text); };

            ToolStripMenu_ReturnWhileEnd.Click += delegate (object sender, EventArgs e)
            {
                ToolStripMenu_ReturnWhileEnd.Checked = !ToolStripMenu_ReturnWhileEnd.Checked;
            };

            //查询代码
            ToolStrip_FindInDictionary.Click += delegate (object sender, EventArgs e)
            {
                Process process = new Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.Arguments = "/c start http://www.baidu.com/s?wd=" + ToolStrip_FindInDictionary.Text.Replace("查询：", "").Trim() + "+site%3Abathome.net&ie=UTF-8";
                process.Start();
            };

            //安全事件触发
            FrmSafe FrmSafeForm = new FrmSafe();
            System.Windows.Forms.Timer Timer4Safe = new System.Windows.Forms.Timer();
            Timer4Safe.Interval = 1000;
            Timer4Safe.Tick += delegate (object sender, EventArgs e)
            {
                if (forceCloseWhenMoreThan10ProcessToolStripMenuItem.Checked)
                {
                    try
                    {
                        int ProcessNumbers = 0;
                        Process[] process = Process.GetProcessesByName("CMD");
                        foreach (Process P in process)
                        {
                            ProcessNumbers++;
                            if (ProcessNumbers > 10)
                            {
                                FrmSafeForm.WriteInfo("[触发]一旦超过10全部关闭[" + P.Id + "]");
                                FrmSafeForm.Show();
                                Function.KillAllCMDs();
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            };
            Timer4Safe.Start();

            //命令行参数设定
            ToolMenu_ParaS.Click += delegate (object sender, EventArgs e)
            {
                FrmParaS Frm = new FrmParaS();
                Frm.textBox1.Text = Setting.CmdParaS;
                if (Frm.ShowDialog() == DialogResult.OK)
                {
                    Setting.CmdParaS = Frm.textBox1.Text;
                }
            };

            //Command
            ToolMenu_Command_Set.Click += delegate (object sender, EventArgs e)
            {
                FrmBAT_set Frm = new FrmBAT_set();
                Frm.Show();
            };
            ToolMenu_Command_LogicalOperation.Click += delegate (object sender, EventArgs e)
            {
                FrmBAT_logic Frm = new FrmBAT_logic();
                Frm.Show();
            };
            ToolMenu_Command_If.Click += delegate (object sender, EventArgs e)
            {
                FrmBAT_if Frm = new FrmBAT_if();
                Frm.Show();
            };
            ToolMenu_Command_For.Click += delegate (object sender, EventArgs e)
            {
                FrmBAT_for Frm = new FrmBAT_for();
                Frm.Show();
            };
            ToolMenu_Command_Choice.Click += delegate (object sender, EventArgs e)
            {
                FrmBAT_choice Frm = new FrmBAT_choice();
                Frm.Show();
            };
            ToolMenu_Command_Environment.Click += delegate (object sender, EventArgs e)
            {
                FrmBAT_环境变量 Frm = new FrmBAT_环境变量();
                Frm.Show();
            };

            //文件列表
            listView_FileLst.SelectedIndexChanged += delegate (object sender, EventArgs e)
            {
                if (listView_FileLst.SelectedItems.Count == 1)
                {
                    for (int i = 0; i < TabS_Scintilla.TabPages.Count; i++)
                    {
                        if (TabS_Scintilla.TabPages[i].Tag.ToString() == listView_FileLst.SelectedItems[0].SubItems[1].Text)
                        {
                            TabS_Scintilla.SelectedIndex = i;
                        }
                    }

                    //恢复文件列表的焦点
                    listView_FileLst.Focus();
                }
            };
            listView_FileLst.ItemMouseHover += delegate (object sender, ListViewItemMouseHoverEventArgs e)
            {
                if (e.Item.SubItems[1].Text.Substring(0, 1) != "@")
                {
                    toolTip1.SetToolTip(listView_FileLst, e.Item.SubItems[1].Text);
                }
                else
                {
                    toolTip1.SetToolTip(listView_FileLst, "[临时文件]");
                }
            };
            listView_FileLst.MouseLeave += delegate (object sender, EventArgs e)
            {
                toolTip1.SetToolTip(listView_FileLst, "");
            };

            //搜索
            textBox_Search.KeyDown += delegate (object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //Super KEY
                    /*if(textBox_Search.Text.Trim()=="test")
                    {
                        for (int i = 0; i < TabS_Scintilla.TabPages.Count; i++)
                        {
                            foreach (Control c in TabS_Scintilla.TabPages[i].Controls)
                            {
                                if (c.GetType().ToString().Contains("Scintilla"))
                                {
                                    //Test Code
                                }
                            }
                        }
                        return;
                    }*/

                    Process process = new Process();
                    process.StartInfo.FileName = "cmd.exe";
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                    process.StartInfo.Arguments = "/K " + textBox_Search.Text;
                    process.Start();
                }
            };
            textBox_Search.TextChanged += delegate (object sender, EventArgs e)
            {
                label_Search.Visible = textBox_Search.Text == "" ? true : false;
                FindForSearch();
            };
            textBox_Search.DoubleClick += delegate (object sender, EventArgs e)
            {
                textBox_Search.Text = "";
            };

            tabControl_CodeBlock.SelectedIndexChanged += delegate (object sender, EventArgs e)
            {
                FindForSearch();
            };
            label_Search.Click += delegate (object sender, EventArgs e) { label_Search.Visible = false; textBox_Search.Focus(); };

            //双击打开新文档
            tableLayoutPanel1.DoubleClick += delegate (object sender, EventArgs e)
            {
                AddNew();
            };

            //代码块操作
            listView_CodeBlock.DoubleClick += delegate (object sender, EventArgs e)
            {
                try
                {
                    //输出代码到当前编辑窗口
                    foreach (Control c in TabS_Scintilla.SelectedTab.Controls)
                    {
                        if (c.GetType().ToString().Contains("Scintilla"))
                        {
                            Scintilla Temp = (Scintilla)c;
                            Temp.InsertText(Function.Position, listView_CodeBlock.SelectedItems[0].SubItems[1].Text);
                            Temp.Focus();
                            break;
                        }
                    }

                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };
            listView_Function.DoubleClick += delegate (object sender, EventArgs e)
            {
                FrmFunction_Mini Frm = new FrmFunction_Mini(listView_Function.SelectedItems[0].SubItems[0].Text, listView_Function.SelectedItems[0].SubItems[1].Text);
                Frm.Show();
            };
            listView_CodeBlock.ItemMouseHover += delegate (object sender, ListViewItemMouseHoverEventArgs e)
            {
                toolTip1.SetToolTip(listView_CodeBlock, e.Item.SubItems[0].Text);
            };
            listView_CodeBlock.MouseLeave += delegate (object sender, EventArgs e)
            {
                toolTip1.SetToolTip(listView_CodeBlock, "");
            };
            listView_Function.ItemMouseHover += delegate (object sender, ListViewItemMouseHoverEventArgs e)
            {
                toolTip1.SetToolTip(listView_Function, e.Item.SubItems[0].Text + Environment.NewLine + e.Item.SubItems[1].Text);
            };
            listView_Function.MouseLeave += delegate (object sender, EventArgs e)
            {
                toolTip1.SetToolTip(listView_Function, "");
            };

            ToolMenu_NewVersion.Click += delegate (object sender, EventArgs e)
            {
                Process process = new Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.Arguments = "/c start " + SS.MainPage;
                process.Start();
            };

            //拖拽添加
            listView_FileLst.DragEnter += delegate (object sender, DragEventArgs e)
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    e.Effect = DragDropEffects.All;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            };
            listView_FileLst.DragDrop += delegate (object sender, DragEventArgs e)
            {
                string[] FileS = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                for (int i = 0; i < FileS.Length; i++)
                {
                    FileInfo fileInfo = new FileInfo(FileS[i]);
                    if (fileInfo.Length > 1024 * 1024 * 30)
                    {
                        MessageBox.Show("只允许打开以下小于30M的文件", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    Function.CreatBATWindow(this, FileS[i]);
                }
            };

            //用户代码组操作
            ToolStrip_UserGroup_Item_Open.Click += delegate (object sender, EventArgs e)
            {
                FrmCodeUnit Unit = new FrmCodeUnit(ChooseUserGroup.GroupName, ChooseUserGroup.GUID, ChooseUserGroup.Title);
                Unit.Show();
            };
            ToolStrip_UserGroup_Item_AddUnit.Click += delegate (object dender, EventArgs e)
            {
                FrmAddUserCode AddUser = new FrmAddUserCode();
                AddUser.ShowDialog();
                Setting.LoadUserCodeGroup();
            };
            ToolStrip_UserGroup_Item_Rename.Click += delegate (object sender, EventArgs e)
            {
                string NewName = InputBox.ShowInputBox("重命名", "请输入新的名称", ChooseUserGroup.Title).Trim();
                if (NewName != "")
                {
                    Function.UserCodeGroup.CODEItem.Rename(ChooseUserGroup.GroupName, ChooseUserGroup.GUID, NewName);
                    ToolStrip_UserGroup_Item_Rename.Enabled = false;
                    ToolStrip_UserGroup_Item_Delete.Enabled = false;
                }
            };
            ToolStrip_UserGroup_Item_Delete.Click += delegate (object sender, EventArgs e)
            {
                if (MessageBox.Show("确认删除" + ChooseUserGroup.Title + "？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    Function.UserCodeGroup.CODEItem.Remove(ChooseUserGroup.GroupName, ChooseUserGroup.GUID);
                    ToolStrip_UserGroup_Item_Rename.Enabled = false;
                    ToolStrip_UserGroup_Item_Delete.Enabled = false;
                }
            };
            listView_UserCodeGroup.DoubleClick += delegate (object sender, EventArgs e)
            {
                if (ChooseUserGroup.GUID != null)
                {
                    try
                    {
                        //输出代码到当前编辑窗口
                        foreach (Control c in TabS_Scintilla.SelectedTab.Controls)
                        {
                            if (c.GetType().ToString().Contains("Scintilla"))
                            {
                                Scintilla Temp = (Scintilla)c;
                                Temp.InsertText(Function.Position, Setting.LoadUserCodeGroup(ChooseUserGroup.GroupName, ChooseUserGroup.GUID).Text);
                                Temp.Focus();
                                break;
                            }
                        }

                    }
                    catch (Exception E)
                    {
                        MessageBox.Show(E.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            };
            listView_UserCodeGroup.SelectedIndexChanged += delegate (object sender, EventArgs e)
            {
                bool Manual = false;
                if (listView_UserCodeGroup.SelectedItems.Count == 1)
                {
                    ChooseUserGroup.GroupName = Path.GetFileNameWithoutExtension(listView_UserCodeGroup.SelectedItems[0].Group.Header);
                    ChooseUserGroup.Title = listView_UserCodeGroup.SelectedItems[0].Text;
                    ChooseUserGroup.GUID = listView_UserCodeGroup.SelectedItems[0].SubItems[1].Text;
                    Manual = true;
                }
                else
                {
                    ChooseUserGroup.GUID = null;
                }

                ToolStrip_UserGroup_Item_Open.Enabled = Manual;
                ToolStrip_UserGroup_Item_Rename.Enabled = Manual;
                ToolStrip_UserGroup_Item_Delete.Enabled = Manual;
            };

            //关于
            ToolMenu_About.Click += delegate (object sender, EventArgs e)
            {
                About Frm = new About(this.ProductVersion.ToString());
                Frm.ShowDialog();
            };

            //反馈及帮助
            ToolMenu_Help.Click += delegate (object sender, EventArgs e)
            {
                Process process = new Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.Arguments = "/c start http://www.bathome.net/forum-55-1.html";
                process.Start();
            };

            this.FormClosing += delegate (object sender, FormClosingEventArgs e)
            {
                if (!CloseAll())
                {
                    e.Cancel = true;
                }

                Setting.SaveCodeBaseXML();
            };

            //计时器
            this.timer_Counter.Tick += delegate (object sender, EventArgs e)
            {
                Setting.UsingSeconds++;
            };

            this.FormClosed += delegate (object sender, FormClosedEventArgs e)
            {
                //保存最近记录到数据库
                Setting.RecentFiles.SaveToDataBase();

                //保存数据库中的配置信息
                DB.SqlCommand("update Config set Value='" + Setting.UsingSeconds + "' where Name='UsingTime'");

                //序列化保存扩展配置文件
                using (FileStream Fs = new FileStream(Setting.File_ConfigBin, FileMode.Create, FileAccess.Write))
                {
                    new BinaryFormatter().Serialize(Fs, Setting.ExtendLst);
                }
            };
        }

        /// <summary>
        /// 刷新视图配置
        /// </summary>
        public void Check_Statu_View()
        {
            if (ToolMenu_FileBar.Checked)
            {
                tableLayoutPanel1.ColumnStyles[0].Width = 150;
            }
            else
            {
                tableLayoutPanel1.ColumnStyles[0].Width = 0;
            }

            if (ToolMenu_CodeBlock.Checked)
            {
                tableLayoutPanel1.ColumnStyles[2].Width = 250;
            }
            else
            {
                tableLayoutPanel1.ColumnStyles[2].Width = 0;
            }

            statusStrip1.Visible = ToolMenu_StatusBar.Checked;
            menuStrip1.Visible = ToolMenu_Menu.Checked;
            toolStrip_main.Visible = ToolMenu_Toolbar.Checked;
            ApplyForAllTabs(EnumForApplyAllTabs.WordWrap);
            ApplyForAllTabs(EnumForApplyAllTabs.LineCode);
        }

        /// <summary>
        /// 定位搜索
        /// </summary>
        void FindForSearch()
        {
            string SearchStr = textBox_Search.Text.Trim().ToUpper();

            ListView TargetLst = null;
            switch (tabControl_CodeBlock.SelectedIndex)
            {
                case 0: TargetLst = listView_UserCodeGroup; break;
                case 1: TargetLst = listView_CodeBlock; break;
                case 2: TargetLst = listView_Function; break;
                default: return;
            }

            int ResultNum = 0;
            if (TargetLst.Items.Count == 0) return;
            for (int i = 0; i < TargetLst.Items.Count; i++)
            {
                if ((TargetLst.Items[i].SubItems[0].Text.ToUpper().Contains(SearchStr) || TargetLst.Items[i].SubItems[1].Text.ToUpper().Contains(SearchStr)) && SearchStr != "")
                {
                    ResultNum++;
                    TargetLst.Items[i].BackColor = Color.YellowGreen;
                }
                else
                {
                    TargetLst.Items[i].BackColor = Color.White;
                }
            }

            if (ResultNum < 100)
            {
                label_SearchNumber.Text = ResultNum.ToString();
            }
            else
            {
                label_SearchNumber.Text = "99";
            }
        }

        /// <summary>
        /// 运行批处理文件
        /// </summary>
        /// <param name="RunAsAdministrator"></param>
        private void ProcessTheBAT(bool RunAsAdministrator = false, bool RunOnExplorer = false)
        {
            bool IsSaveDone = true;
            if (TabS_Scintilla.TabPages[TabS_Scintilla.SelectedIndex].Text.EndsWith("*"))
            {
                IsSaveDone = DoSave(TabS_Scintilla.SelectedTab);
            }
            if (IsSaveDone)
            {
                Scintilla Temp = FindScintillaWindow(TabS_Scintilla.TabPages[TabS_Scintilla.SelectedIndex]);
                Function.RunCMD(TabS_Scintilla.SelectedTab.Tag.ToString(), RunAsAdministrator);
            }
        }

        /// <summary>
        /// 捕捉按键信息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            if ((Keys.Alt & keyData) == Keys.Alt) menuStrip1.Visible = true;
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private delegate void SetLabelDelegate();

        /// <summary>
        /// 枚举作用于所有TabControl子页面的操作方法
        /// </summary>
        public enum EnumForApplyAllTabs { ZoomUpdate, WordWrap, LineCode };

        /// <summary>
        /// 作用于所有TabControl子页面的操作
        /// </summary>
        public void ApplyForAllTabs(EnumForApplyAllTabs HandleSwitch)
        {
            try
            {
                for (int i = 0; i < TabS_Scintilla.TabPages.Count; i++)
                {
                    foreach (Control c in TabS_Scintilla.TabPages[i].Controls)
                    {
                        if (c.GetType().ToString().Contains("Scintilla"))
                        {
                            switch (HandleSwitch)
                            {
                                case EnumForApplyAllTabs.ZoomUpdate: ((Scintilla)c).ZoomFactor = Setting.ZoomLevel; break;
                                case EnumForApplyAllTabs.WordWrap: ((Scintilla)c).LineWrapping.Mode = ToolMenu_WordWrap.Checked ? LineWrappingMode.Word : LineWrappingMode.None; break;
                                case EnumForApplyAllTabs.LineCode:
                                    ((Scintilla)c).Margins[0].Type = ToolMenu_LineCode.Checked ? MarginType.Number : MarginType.Symbol;
                                    Setting.ShowLineCode = ToolMenu_LineCode.Checked;
                                    Function.ShowPosition(this);
                                    break;
                                default: throw new Exception("没有找到匹配的操作方法");
                            }
                        }
                    }
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        /// <summary>
        /// 定位Scintilla
        /// </summary>
        /// <returns></returns>
        public Scintilla FindScintillaWindow(TabPage Target_TabPage)
        {
            Scintilla Target_Scintilla = new Scintilla();
            foreach (Control c in Target_TabPage.Controls)
            {
                if (c.GetType().ToString().Contains("Scintilla"))
                {
                    Target_Scintilla = (Scintilla)c;
                }
            }

            return Target_Scintilla;
        }

        /// <summary>
        /// 从文件路径信息定位处于文件列表中的位置
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        public int FindListFromFilePath(string FilePath)
        {
            int Result = -1;
            for (int i = 0; i < listView_FileLst.Items.Count; i++)
            {
                if (listView_FileLst.Items[i].SubItems[1].Text == FilePath)
                {
                    Result = i;
                    break;
                }
            }
            return Result;
        }

        /// <summary>
        /// 打开文件
        /// </summary>
        private void OpenFile()
        {
            if (this.InvokeRequired)
            {
                SetLabelDelegate d = new SetLabelDelegate(OpenFile);
                this.Invoke(d);
            }
            else
            {
                OpenFileDialog Ofd = new OpenFileDialog();
                Ofd.Multiselect = true;
                Ofd.Title = "选择要打开的批处理文件";
                Ofd.Filter = "批处理|*.BAT;*.CMD";
                if (DialogResult.OK == Ofd.ShowDialog())
                {
                    foreach (string FileName in Ofd.FileNames)
                    {
                        Function.CreatBATWindow(this, FileName);
                    }
                    TabS_Scintilla.Visible = true;
                }
            }
        }

        /// <summary>
        /// 执行保存指令
        /// </summary>
        /// <param name="Target_Page">保存指定Scintilla窗体的文档</param>
        /// <returns>标识是否成功执行保存</returns>
        public bool DoSave(TabPage Target_Page)
        {
            try
            {
                //定位目标Scintilla文档
                Scintilla Target_Scintilla = FindScintillaWindow(Target_Page);

                //定位目标处于文件清单中的位置
                string FileHash = Target_Page.Tag.ToString();
                int FileList_Pos = FindListFromFilePath(FileHash);

                if (FileList_Pos == -1) throw new Exception("无法定位目标处于文件清单中的位置");

                //保存文件并改变文件列表中路径等参数
                string NewPath = "";
                if (FileHash.Substring(0, 1) == "@")
                {
                    //无路经保存
                    NewPath = Function.SaveBAT(Target_Scintilla.Text);
                }
                else
                {
                    //有路径保存
                    NewPath = Function.SaveBAT(Target_Scintilla.Text, FileHash);
                }

                if (NewPath != null && NewPath.Trim() != "")
                {
                    //更新文件名在文件列表中的显示
                    Target_Page.Tag = NewPath;
                    listView_FileLst.Items[FileList_Pos].SubItems[0].Text = Path.GetFileNameWithoutExtension(NewPath);
                    listView_FileLst.Items[FileList_Pos].SubItems[1].Text = NewPath;

                    //清除文档的"操作了"标识
                    if (Target_Page.Text.EndsWith("*"))
                    {
                        Target_Page.Text = Path.GetFileNameWithoutExtension(NewPath);

                        //清除在文件列表中的标识
                        listView_FileLst.Items[FileList_Pos].ForeColor = Color.Black;
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
        }

        /// <summary>
        /// 执行关闭文档指令
        /// </summary>
        /// <param name="TargetPage">文档所在的TabControl中的位置</param>
        /// <returns>返回是否为非打断关闭</returns>
        public bool DoClose(TabPage TargetPage)
        {
            try
            {
                //判定是否打算关闭
                bool IsBreak = false;

                if (TargetPage.Text.ToString().EndsWith("*"))
                {
                    //确认要保存文件
                    DialogResult R = MessageBox.Show("是否将更改保存到 " + TargetPage.Text.Substring(0, TargetPage.Text.Length - 1) + "？", "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (R == DialogResult.Yes)
                    {
                        if (!DoSave(TargetPage)) IsBreak = true;
                    }
                    else if (R == DialogResult.Cancel)
                    {
                        IsBreak = true;
                    }
                }

                if (!IsBreak)
                {
                    string FilePath = TargetPage.Tag.ToString();
                    TabS_Scintilla.TabPages.Remove(TargetPage);
                    listView_FileLst.Items.RemoveAt(FindListFromFilePath(FilePath));
                    Function.CheckSameNameButDifferentPath(Path.GetFileNameWithoutExtension(FilePath));
                }

                //关联编辑菜单
                TabPageConnectMenu();

                return !IsBreak;
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
        }

        /// <summary>
        /// 关闭所有
        /// </summary>
        /// <returns>返回是否为非打断关闭</returns>
        public bool CloseAll()
        {
            bool IsBreak = false;
            for (int i = 0; i < TabS_Scintilla.TabPages.Count; i++)
            {
                if (DoClose(TabS_Scintilla.TabPages[i]))
                {
                    i--;
                }
                else
                {
                    IsBreak = true;
                    break;
                }
            }
            return !IsBreak;
        }

        /// <summary>
        /// 关联编辑菜单
        /// </summary>
        public void TabPageConnectMenu()
        {
            bool NoTabs = false;
            NoTabs = TabS_Scintilla.TabPages.Count == 0 ? true : false;

            ToolMenu_Edit_.Enabled = !NoTabs;
            ToolMenu_Run.Enabled = !NoTabs;
            ToolMenu_RunAsAdministrator.Enabled = !NoTabs;
            ToolMenu_Save.Enabled = !NoTabs;
            ToolMenu_SaveAll.Enabled = !NoTabs;
            ToolMenu_SaveAs.Enabled = !NoTabs;
            ToolMenu_CloseFile.Enabled = !NoTabs;
            ToolMenu_CloseAllFiles.Enabled = !NoTabs;
        }

        /// <summary>
        /// 配置控制设定
        /// </summary>
        void ConfigSetting()
        {
            //向配置中心传递句柄
            Setting.Handle = this;

            //配置关联菜单指向
            Setting.RecentFiles.TargetToolMenu = ToolMenu_OpenRecent;

            //加载数据库中配置信息
            Setting.RecentFiles.ReadFromDataBase();

            //加载代码块
            Function.CodeAndFunction.Load(this, Function.CodeAndFunction.CodeType.CodeBlock);
            Function.CodeAndFunction.Load(this, Function.CodeAndFunction.CodeType.Function);

            //加载配置信息
            Setting.LoadSettingXML();

            //加载用户代码组
            Setting.LoadUserCodeGroup();

            //加载外部扩展组
            Setting.LoadExtendTools(ToolStripMenuItemExtend);

            //启动计时器
            timer_Counter.Start();
        }

        /// <summary>
        /// 新建批处理文档
        /// </summary>
        void AddNew()
        {
            Function.CreatBATWindow(this);

            foreach (Control c in TabS_Scintilla.SelectedTab.Controls)
            {
                if (c.GetType().ToString().Contains("Scintilla"))
                {
                    ((Scintilla)c).Tag = "Codeeer";//Tag如果为空，检测时就会抛出异常，随便设置一个变量即可
                    ((Scintilla)c).Focus();
                }
            }
        }

        #endregion

        /// <summary>
        /// 软件基础信息
        /// </summary>
        public class SoftwareInfo
        {
            /// <summary>
            /// 软件ID信息
            /// </summary>
            public string ID { get { return "iBAT"; } }

            /// <summary>
            /// 软件主页
            /// </summary>
            public string MainPage { get { return "http://ibat.okchakela.com/"; } }

            /// <summary>
            /// 如果静默模式为真，在非发现新版本时不会提示
            /// </summary>
            public bool SilenceUntilFoundNew { get { return false; } }
        }

        /// <summary>
        /// 检查软件更新和发送使用信息
        /// </summary>
        sealed class CheckUpdateAndLoginS : SoftwareInfo
        {
            const string URL_CheckUpdate = "http://data.okchakela.com/?Method=Login&";

            string SimpleResult = "";

            ToolStripMenuItem Button_CheckUpdate;

            /// <summary>
            /// 记录当前版本号
            /// </summary>
            string NowVersion;

            /// <summary>
            /// 检查软件更新和发送使用信息
            /// </summary>
            /// <param name="Version">当前软件版本信息</param>
            public CheckUpdateAndLoginS(string Version)
            {
                NowVersion = Version;
            }

            /// <summary>
            /// 检查软件更新
            /// </summary>
            public void CheckUpdate(ToolStripMenuItem DoButton = null)
            {
                Button_CheckUpdate = DoButton;
                Control.CheckForIllegalCrossThreadCalls = false;

                Thread oThread = new Thread(new ParameterizedThreadStart(CheckUpdate_Core));
                oThread.IsBackground = true;
                oThread.Start(ID);
            }

            void CheckUpdate_Core(object Name)
            {
                try
                {
                    using (WebClient myWebClient = new WebClient())
                    {
                        StringBuilder SB = new StringBuilder();

                        //预定义信息
                        SB.AppendLine("[" + Application.ProductVersion.ToString() + "]");

                        //使用时间
                        SB.AppendLine("[" + Setting.UsingSeconds + "]");

                        //获取系统信息
                        RegistryKey Key1 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
                        RegistryKey Key2 = Registry.LocalMachine.OpenSubKey(@"HARDWARE\DESCRIPTION\System\CentralProcessor\0");
                        RegistryKey Key3 = Registry.LocalMachine.OpenSubKey(@"HARDWARE\DESCRIPTION\System\BIOS");
                        SB.AppendLine(Key1.GetValue("ProductName").ToString());
                        SB.AppendLine(" " + Key1.GetValue("CurrentVersion") + "." + Key1.GetValue("CurrentBuildNumber"));
                        SB.AppendLine(" " + Key1.GetValue("ProductId"));
                        SB.AppendLine(" " + Key1.GetValue("RegisteredOwner"));
                        SB.AppendLine(" " + Key2.GetValue("ProcessorNameString"));
                        SB.AppendLine(" " + Key2.GetValue("~MHz") + " MHz");
                        SB.AppendLine(" " + Key3.GetValue("BaseBoardManufacturer") + "." + Key3.GetValue("BaseBoardProduct"));

                        string Data = SB.ToString();
                        if (Data.Length > 255) Data = Data.Substring(0, 255);

                        //加密发送
                        byte[] encData_byte = new byte[Data.Length];
                        encData_byte = System.Text.Encoding.UTF8.GetBytes(Data);
                        string encodedData = Convert.ToBase64String(encData_byte);
                        //Console.WriteLine(encodedData);
                        //Console.WriteLine(URL_CheckUpdate + "&SoftwareID=" + (string)Name + "&Info=" + encodedData);
                        using (Stream myStream = myWebClient.OpenRead(URL_CheckUpdate + "&SoftwareID=" + (string)Name + "&Info=" + encodedData))
                        {
                            using (StreamReader sr = new StreamReader(myStream, System.Text.Encoding.GetEncoding("utf-8")))
                            {
                                JObject jo = JObject.Parse(sr.ReadToEnd());

                                if (Convert.ToBoolean(jo["Statu"].ToString()) == true)
                                {
                                    //比较版本号
                                    Version v1 = new Version(NowVersion), v2 = new Version(jo["LastVersion"].ToString());
                                    if (v1 < v2)
                                    {
                                        SimpleResult = v2 + " 更新于 " + jo["UploadDate"].ToString();
                                        if (Button_CheckUpdate != null)
                                        {
                                            Button_CheckUpdate.Text = "发现新版本[" + SimpleResult + "]";
                                            Button_CheckUpdate.Visible = true;
                                        }
                                    }
                                    else
                                    {
                                        if (!SilenceUntilFoundNew) throw new Exception("已经是最新版本");
                                    }
                                }
                                else
                                {
                                    throw new Exception("拉取更新数据失败");
                                }
                            }
                        }
                    }
                }
                catch
                {
                    //MessageBox.Show(E.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
