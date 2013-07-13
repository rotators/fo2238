using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace FOLauncher
{
    public static class Logging
    {
        static object loglock=new object();

        public static void Init()
        {
            if(File.Exists(".\\Launcher.log"))
                File.Delete(".\\Launcher.log");
        }

        public static void MessageBox(string text, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            System.Windows.Forms.MessageBox.Show(text, "FOnline: 2238 Launcher", buttons, icon);
        }

        public static void Log(string s)
        {
            lock (loglock)
            {
                File.AppendAllText(".\\Launcher.log", "[" + DateTime.Now.ToString() + "] " + s + Environment.NewLine);
            }
        }
    }
}
