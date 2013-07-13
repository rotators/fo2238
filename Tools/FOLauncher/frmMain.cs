using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace FOLauncher
{
    public partial class frmMain : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool
        SetForegroundWindow(IntPtr hWnd);

        frmMain main;
        System.Net.WebClient wclient;

        string updateScriptUrl = "http://fonline2238.net/updatecheck.php";
        string version         = "1.06";

        public frmMain()
        {
            InitializeComponent();
        }

        private void StartProcess(string name)
        {
            Logging.Log("Trying to find process " + name+".exe");
            Process[] procs = Process.GetProcessesByName(name);
            if (procs.Length > 0)
            {
                Logging.Log("Found process, setting focus");
                SetForegroundWindow(procs[0].MainWindowHandle);
                return;
            }
            Logging.Log("Starting process");
            ProcessStartInfo startinfo = new ProcessStartInfo();
            string path = Directory.GetParent(Assembly.GetExecutingAssembly().Location).ToString();
            Logging.Log("Found path of process: " + path);
            string fname = path + "\\" + name + ".exe";
            if (!File.Exists(fname))
            {
                Logging.Log("Unable to find " + fname);
                MessageBox.Show(fname + " not found.","FOnline: 2238 Launcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            startinfo.FileName = fname;
            startinfo.WorkingDirectory = path;

            Process proc = new Process();
            proc.StartInfo = startinfo;
            proc.Start();
            Logging.Log(name+".exe"+" started");
        }

        private string GetPath()
        {
            return Directory.GetParent(Assembly.GetExecutingAssembly().Location).ToString();
        }

        private void pctPlayUp_MouseDown(object sender, MouseEventArgs e)
        {
            pctPlayUp.Visible = false;
            pctPlayDown.Visible = true;
        }

        private void pctPlayUp_MouseUp(object sender, MouseEventArgs e)
        {
            pctPlayUp.Visible = true;
            pctPlayDown.Visible = false;
            StartProcess("FOnline");
        }

        private void pctUpdateUp_MouseDown(object sender, MouseEventArgs e)
        {
            pctUpdateUp.Visible = false;
            pctUpdateDown.Visible = true;
        }

        private void pctUpdateUp_MouseUp(object sender, MouseEventArgs e)
        {
            pctUpdateUp.Visible = true;
            pctUpdateDown.Visible = false;
            StartProcess("Updater");
        }

        private void pctExitUp_MouseDown(object sender, MouseEventArgs e)
        {
            pctExitUp.Visible = false;
            pctExitDown.Visible = true;
        }

        private void pctExitUp_MouseUp(object sender, MouseEventArgs e)
        {
            pctExitUp.Visible = true;
            pctExitDown.Visible = false;
            Logging.Log("Exiting application");
            Application.Exit();
        }

        private void pctConfigUp_MouseDown(object sender, MouseEventArgs e)
        {
            pctConfigUp.Visible = false;
            pctConfigDown.Visible = true;
        }

        private void pctConfigUp_MouseUp(object sender, MouseEventArgs e)
        {
            pctConfigUp.Visible = true;
            pctConfigDown.Visible = false;

            try
            {
                StartProcess("FOConfig");
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message, "FOnline: 2238 Launcher", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser.Document.Body.InnerHtml.Contains("This program cannot display the webpage"))
            {
                Logging.Log("HTML failed to display");
                webBrowser.Hide();
                lblWebBrowerFail.Visible = true;
            }
            else
            {
                Logging.Log("Displaying HTML document");
                webBrowser.Show();
            }
        }

        private void CheckSite()
        {
            string updatemd5 = "";

            Logging.Log("Retrieving MD5");
            try
            {
                // This script should return the MD5 of the latest news so that the launcher can decide if it needs to update
                // or if it can use the cached information.
                updatemd5 = wclient.DownloadString(updateScriptUrl);
            }
            catch (System.Net.WebException webex)
            {
                Logging.Log(webex.Message.ToString());
                return;
            }

            if (File.Exists("changelog.md5"))
            {
                Logging.Log("Reading MD5");
                string localmd5 = File.ReadAllText("changelog.md5");
                Logging.Log("Found MD5 locally: "+localmd5);
                if (!updatemd5.Contains(localmd5))
                {
                    Logging.Log("MD5 doesn't match, retrieve news");
                    FetchSite();
                }
                else
                    Logging.Log("MD5 matches");
            }
            else
                FetchSite();

            if (!File.Exists(Directory.GetCurrentDirectory() + @"\changelog.html"))
                FetchSite();

            lock (main)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    Navigate();
                });
            }

            if (!String.IsNullOrEmpty(updatemd5))
                File.WriteAllText("changelog.md5", updatemd5.Trim());
        }

        private void CheckOnline()
        {
            bool success;
            do
            {
                success = PingOnline();
                Thread.Sleep(5000);
            }
                while(!success);
        }

        private bool PingOnline()
        {
            Logging.Log("Creating ping socket");
            Socket sock = new Socket(AddressFamily.InterNetwork,
                 SocketType.Stream,
                 ProtocolType.Tcp);
            if (sock == null)
            {
                Logging.Log("Failed to initialize socket");
                return false;
            }

            Logging.Log("Setting server status [0]");
            lock (main)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    main.Text = "FOnline: 2238 Launcher - Server Status: [Checking...]";
                });
            }

            int connectTimeout = 10000;

            sock.Blocking = false;
            try
            {
                Logging.Log("Connecting...");
                // Try to connect to the game server.
                sock.Connect("fonline2238.net", 2238);
            }
            catch (SocketException ex)
            {
                // 10035 == WSAEWOULDBLOCK
                if (ex.NativeErrorCode != 10035)
                {
                    // Handle bad exception
                    Logging.Log("Socket error: 10035 WSAEWOULDBLOCK");
                    return false;
                }

                // Wait until connected or timeout.
                // SelectWrite: returns true, if processing a Connect,
                // and the connection has succeeded.
                if (sock == null)
                    return false;
                if (sock.Poll((connectTimeout * 1000), SelectMode.SelectWrite) == false)
                {
                    // Handle connect timeout
                    Logging.Log("Setting server status [1]");
                    lock (main)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            main.Text = "FOnline: 2238 Launcher - Server Status: Offline";
                            pctLampOff.Visible = true;
                        });
                    }
                    Logging.Log("Connection to server failed");
                    return true;
                }
            }
            Logging.Log("Connected to server");
            try
            {
                Logging.Log("Disconnecting...");
                if (sock != null && sock.Connected)
                {
                    sock.Disconnect(false);
                    Logging.Log("Closing socket");
                    sock.Close();
                }
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                Logging.Log("Failed to close socket: " + ex.Message);
                return true;
            }
            Logging.Log("Disconnected");
            Logging.Log("Setting server status [2]");
            lock (main)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    main.Text = "FOnline: 2238 Launcher - Server Status: Online";
                    pctLampOff.Visible = false;
                });
            }
            Logging.Log("Ping thread close");
            return true;
        }

        private void FetchSite()
        {
            Logging.Log("Downloading news");
            string page = wclient.DownloadString("http://fonline2238.net/newsupdate.php");
            File.WriteAllText("changelog.html", page);
            Logging.Log("Downloading CSS");
            string css = wclient.DownloadString("http://fonline2238.net/changelog.css");
            File.WriteAllText("changelog.css", css);
            Logging.Log("Download finished");
        }

        private void Navigate()
        {
            Logging.Log("Navigate HTML document");
            webBrowser.Navigate(Directory.GetCurrentDirectory() + @"\changelog.html");
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            webBrowser.BorderStyle=BorderStyle.None;
            main = this;
        }

        private void Process_Exited(object sender, EventArgs e)
        {
            main.Enabled = true;
            main.BringToFront();
        }

        private void tmrCheckStatus_Tick(object sender, EventArgs e)
        {
            Logging.Log("Check status");
            Process[] procs = Process.GetProcessesByName("FOnline");
            if (procs.Length == 0)
            {
                Logging.Log("FOnline process not started, start ping thread");
                Thread thread = new Thread(new ThreadStart(CheckOnline));
                thread.Start();
            }
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            Directory.SetCurrentDirectory(Directory.GetParent(Assembly.GetExecutingAssembly().Location).ToString());

            Logging.Init();
            Logging.Log("Starting FOLauncher " + version);
            Logging.Log("   IE version: " + webBrowser.Version);
            Logging.Log("   OS version: " + Environment.OSVersion.VersionString + (IntPtr.Size == 4 ? " 32-bit" : " 64-bit"));
            Logging.Log("   .NET CLR version: " + Environment.Version);

            if (Directory.Exists(@Environment.GetEnvironmentVariable("windir") + @"\Microsoft.NET\Framework"))
            {
                Logging.Log("   .NET Supported versions:");
                foreach (string i in Directory.GetDirectories(@Environment.GetEnvironmentVariable("windir") + @"\Microsoft.NET\Framework", "v*.*", SearchOption.AllDirectories))
                {
                    DirectoryInfo di = new DirectoryInfo(i);
                    if (di.Name.Contains("."))
                        Logging.Log("       " + di.Name);
                }
            }

            wclient = new System.Net.WebClient();
            wclient.Proxy = null;
            if (wclient != null)
                Logging.Log("Webclient initialized");
            else
                Logging.Log("Webclient failed to initialize");

            if (File.Exists(Directory.GetCurrentDirectory() + @"\changelog.html"))
                Navigate();
            else
            {
                webBrowser.BorderStyle = BorderStyle.None;
                webBrowser.Hide();
            }

            Logging.Log("Starting ping thread");
            Thread thread = new Thread(new ThreadStart(CheckOnline));
            thread.Start();
            if (wclient != null)
            {
                Logging.Log("Starting web thread");
                Thread sitethread = new Thread(new ThreadStart(CheckSite));
                sitethread.Start();
            }
        }
    }
}
