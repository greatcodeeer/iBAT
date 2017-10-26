using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SettingContext
{
    class Program
    {
        static void Main(string[] args)
        {
            string ShellType = args[0];//动作执行方式
            switch (ShellType)
            {
                case "add": SetContext(); break;
                case "remove": DeleteRegeditItem(); break;
                default: ProcessCommand("iBAT关键组件，不要更改或删除"); break;
            }


        }

        //设置右键菜单
        static void SetContext()
        {
            Console.WriteLine("请稍后，正在设置右键菜单...");
            string TargetEXE = Application.StartupPath + @"\iBAT.exe";
            if (File.Exists(TargetEXE))
            {
                RegistryKey shellKey = Registry.ClassesRoot.OpenSubKey(@"*\shell", true);
                RegistryKey rightCommondKey = shellKey.CreateSubKey("用iBAT打开");
                RegistryKey associatedProgramKey = rightCommondKey.CreateSubKey("command");
                if (shellKey == null) shellKey = Registry.ClassesRoot.CreateSubKey(@"*\shell");
                associatedProgramKey.SetValue(string.Empty, TargetEXE + @" ""%1""");

                associatedProgramKey.Close();
                rightCommondKey.Close();
                shellKey.Close();

                ProcessCommand("右键菜单设置完成");
            }
            else
            {
                ProcessCommand("无法定位iBAT文件路径");
            }
        }

        //弹窗对话框提示
        static void ProcessCommand(string Msg)
        {
            ProcessStartInfo start = new ProcessStartInfo("mshta");
            start.Arguments = @"vbscript:msgbox(""" + Msg + @""",vbInformation,""提示"")(window.close)";
            start.CreateNoWindow = true;
            Process.Start(start);
        }

        //判定注册表项是否存在
        static bool IsRegeditItemExist()
        {
            string[] subkeyNames;
            RegistryKey shellKey = Registry.ClassesRoot.OpenSubKey(@"*\shell", true);
            subkeyNames = shellKey.GetSubKeyNames();
            foreach (string keyName in subkeyNames)
            {
                if (keyName == "用iBAT打开")
                {
                    shellKey.Close();
                    return true;
                }
            }
            shellKey.Close();
            return false;
        }

        //删除注册表项
        static void DeleteRegeditItem()
        {
            try
            {
                if (!IsRegeditItemExist())
                {
                    ProcessCommand("右键菜单中不存在iBAT项");
                }
                else
                {
                    RegistryKey shellKey = Registry.ClassesRoot.OpenSubKey(@"*\shell", true);
                    shellKey.DeleteSubKey(@"用iBAT打开\command", true);
                    shellKey.DeleteSubKey(@"用iBAT打开", true);
                    shellKey.Close();
                    ProcessCommand("已经清除iBAT右键菜单");
                }
            }
            catch(Exception E)
            {
                ProcessCommand("设置失败：" + E.Message);
            }
        }
    }
}
