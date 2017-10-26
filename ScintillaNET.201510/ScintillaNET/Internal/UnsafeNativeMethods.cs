#region Using Directives

using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

#endregion Using Directives

namespace ScintillaNET.Internal
{
    [SuppressUnmanagedCodeSecurity]
    internal static class UnsafeNativeMethods
    {
        #region Constants

        private const string
            DLL_NAME_KERNEL32 = "kernel32.dll",
            DLL_NAME_USER32 = "user32.dll";

        #endregion Constants

        #region Functions

        //[DllImport(DLL_NAME_KERNEL32, EntryPoint = "GetModuleFileNameW", CharSet = CharSet.Unicode)]
        //public static extern int GetModuleFileName(HandleRef hModule, StringBuilder lpFilename, int nSize);

        [DllImport(DLL_NAME_KERNEL32, CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetProcAddress(HandleRef hModule, string lpProcName);

        [DllImport(DLL_NAME_KERNEL32, EntryPoint = "LoadLibraryW", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport(DLL_NAME_USER32, EntryPoint = "SendMessageW", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, IntPtr wParam, IntPtr lParam);

        #endregion Functions
    }
}
