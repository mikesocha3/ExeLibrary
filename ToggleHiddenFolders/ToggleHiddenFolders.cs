﻿namespace ToggleHiddenFolders
{
    using Microsoft.Win32;
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Toggles the Windows view options property "View Hidden Items" on or off.
    /// </summary>
    public class ToggleHiddenFolders
    {
        private static bool _testing = false;

        [Flags]
        public enum HChangeNotifyEventID
        {
            SHCNE_ASSOCCHANGED = 0x08000000,
        }

        [Flags]
        public enum HChangeNotifyFlags
        {
            SHCNF_IDLIST = 0x0000
        }

        [Flags]
        public enum SendMessageTimeoutFlags : uint
        {
            SMTO_ABORTIFHUNG = 0x2,
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("shell32.dll")]
        public static extern void SHChangeNotify(HChangeNotifyEventID wEventId, HChangeNotifyFlags uFlags, IntPtr dwItem1, IntPtr dwItem2);

        static void Main(string[] args)
        {
            IntPtr HWND_BROADCAST = new IntPtr(0xffff);
            //UInt32 WM_SETTINGCHANGE = 0x1A; not used
            IntPtr NULL = IntPtr.Zero;

            RegistryKey key = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced");
            if (key != null)
            {
                if (key.GetValue("Hidden").ToString() == "1")
                {
                    key.SetValue("Hidden", 2);
                    Console.WriteLine("The files should now be hidden.");
                }
                else
                {
                    key.SetValue("Hidden", 1);
                    Console.WriteLine("The files should now be visible.");
                }
            }

            IntPtr refresh = new IntPtr(0x7103); //Refresh
            uint WM_COMMAND = 0x0111;
            IntPtr output= SendMessage(HWND_BROADCAST, WM_COMMAND, refresh, IntPtr.Zero);
            SHChangeNotify(HChangeNotifyEventID.SHCNE_ASSOCCHANGED, HChangeNotifyFlags.SHCNF_IDLIST, IntPtr.Zero, IntPtr.Zero);

            if (_testing)
            { //..dont show console etc...
            }
        }

        private static void PromptAnyKey()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }
    
        public static void Test(string[] args)
        {
            _testing = true;
            Main(args);
        }
    }
}