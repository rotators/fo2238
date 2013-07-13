using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace FOLauncher
{
    static class Program
    {
        static bool ReportError(string log, string stacktrace)
        {
            try
            {

            TcpClient client = new TcpClient();
            //IPAddress addr = IPAddress.Parse("fonline2238.net");
            IPAddress addr = IPAddress.Parse("127.0.0.1");
            client.Connect(addr, 2250);
            NetworkStream stream = client.GetStream();

            IPHostEntry IPHost = Dns.GetHostEntry(Dns.GetHostName());
            string localip=IPHost.AddressList[0].ToString();

            Byte[] data = System.Text.Encoding.ASCII.GetBytes("edata|||"+localip+"|||"+log+"|||"+stacktrace);
            stream.Write(data, 0, data.Length);

            // Receive the TcpServer.response.
            // Close everything.
            stream.Close();         
            client.Close();         
          }
            catch (ArgumentNullException) { return false; }
            catch (SocketException) { return false; }
            return true;
        }

        static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            File.WriteAllText(@".\LauncherError.log", ex.ToString());
            MessageBox.Show("The launcher has crashed because of the following exception: "+ Environment.NewLine+ 
                ex.ToString(), "FOnline: 2238 Launcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
            MessageBox.Show("Please submit Launcher.log and LauncherError.log (they are in the game directory) to [change.me@something]. Thank you!", "FOnline: 2238 Launcher", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (!System.Diagnostics.Debugger.IsAttached)
                Environment.Exit(System.Runtime.InteropServices.Marshal.GetHRForException(ex));
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException +=
                new UnhandledExceptionEventHandler(UnhandledException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}
