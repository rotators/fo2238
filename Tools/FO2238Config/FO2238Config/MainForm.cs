using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace FO2238Config
{
    public partial class MainForm : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams result = base.CreateParams;
                if (Environment.OSVersion.Platform == PlatformID.Win32NT
                    && Environment.OSVersion.Version.Major >= 6)
                {
                    result.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                }

                return result;
            }
        }

        private IniParser parser;
        private static String[] KeyNames =
            {"1","2","3","4","5","6","7","8","9","0","-","=",
            "A","B","C","D","E","F","G","H","I","J","K","L","M","N",
            "O","P","Q","R","S","T","U","V","W","X","Y","Z","[","]",";","'",",",".","/",
            "Tab","Ins","Del","Home","End","PgUp","PgDn",
            "Num 0","Num 1","Num 2","Num 3","Num 4","Num 5","Num 6","Num 7","Num 8","Num 9",
            "Num .","Num /","Num *","Num -","Num +","Up","Down","Left","Right"};

        public MainForm()
        {
            Directory.SetCurrentDirectory(Directory.GetParent(Assembly.GetExecutingAssembly().Location).ToString());
            InitializeComponent();
        }

        private void SetObject(Object obj, String section, String key)
        {
            String s=parser.GetSetting(section, key);
            if (obj is CheckBox)
            {
                CheckBox box = (CheckBox)obj;
                box.Checked=(s.Equals("1"));
            }
            else if (obj is NumericUpDown)
            {
                NumericUpDown num = (NumericUpDown)obj;
                num.Value = Clamp(ParseInt(s),(int)num.Minimum,(int)num.Maximum);
            }
            else if (obj is ComboBox)
            {
                ComboBox box = (ComboBox)obj;
                box.SelectedIndex = Clamp(ParseInt(s), 0, box.Items.Count - 1);
            }
            else if (obj is Button)
            {
                Button button = (Button)obj;
                String[] spl = s.Split(' ');
                int r=0;
                int g=0;
                int b=0;
                if (spl.Length > 2) b = Clamp(ParseInt(spl[2]), 0, 255);
                if (spl.Length > 1) g = Clamp(ParseInt(spl[1]), 0, 255);
                if (spl.Length > 0) r = Clamp(ParseInt(spl[0]), 0, 255);
                button.ForeColor = Color.FromArgb(r, g, b);
            }
        }

        private void SetFromObject(Object obj, String section, String key)
        {
            if (obj is CheckBox)
            {
                CheckBox box = (CheckBox)obj;
                parser.AddSetting(section, key, box.Checked ? "1" : "0");
            }
            else if (obj is NumericUpDown)
            {
                NumericUpDown num = (NumericUpDown)obj;
                parser.AddSetting(section, key, num.Value.ToString());
            }
            else if (obj is ComboBox)
            {
                ComboBox box = (ComboBox)obj;
                parser.AddSetting(section, key, box.SelectedIndex.ToString());
            }
            else if (obj is Button)
            {
                Button button = (Button)obj;
                Color color = button.ForeColor;
                parser.AddSetting(section, key, color.R.ToString()+" "+color.G.ToString()+" "+color.B.ToString());
            }
        }

        private void ButtonColor(Button button)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.AnyColor = true;
            colorDlg.SolidColorOnly = false;

            colorDlg.Color = button.ForeColor;
            if (colorDlg.ShowDialog() == DialogResult.OK)
                button.ForeColor = colorDlg.Color;
        }

        private int Clamp(int n, int min, int max)
        {
            if(n>max) return max;
            if(n<min) return min;
            return n;
        }

        private int ParseInt(String s)
        {
            if (s == null) return 0;
            try
            {
                int ret = Int32.Parse(s);
                return ret;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private void LoadConfig()
        {
            // 2238
            SetObject(checkBox1, "2238", "DisplayNames");
            SetObject(checkBox2, "2238", "Awareness");
            SetObject(checkBox3, "2238", "DisplayTC");
            SetObject(comboBox1, "2238", "DisplayTCZones");
            SetObject(checkBox5, "2238", "Autosave");
            checkBox6.Checked = parser.GetSetting("2238", "MouseScroll").Equals("0");

            // timeouts
            SetObject(checkBox4, "Timeouts", "Enabled");
            SetObject(comboBox2, "Timeouts", "Direction");
            comboBox3.SelectedIndex = Clamp(ParseInt(parser.GetSetting("Timeouts", "Font")), 4, 8) - 4;
            SetObject(numericUpDown1, "Timeouts", "Transparency");
            SetObject(checkBox7, "Timeouts", "Worldmap");

            // timeouts colors
            SetObject(button4, "Timeouts", "NameColor");
            SetObject(button5, "Timeouts", "ValueColor");

            // timeouts pos
            SetObject(numericUpDown2, "Timeouts", "PositionX");
            SetObject(numericUpDown3, "Timeouts", "PositionY");
            SetObject(numericUpDown4, "Timeouts", "SpacingX");
            SetObject(numericUpDown5, "Timeouts", "SpacingY");
            SetObject(numericUpDown7, "Timeouts", "WorldmapX");
            SetObject(numericUpDown6, "Timeouts", "WorldmapY");

            /*SetKeybind(checkBox10, checkBox9, checkBox8, comboBox4, "Bindings", "AimUncalled");
            SetKeybind(checkBox11, checkBox12, checkBox13, comboBox5, "Bindings", "AimHead");
            SetKeybind(checkBox14, checkBox15, checkBox16, comboBox6, "Bindings", "AimEyes");
            SetKeybind(checkBox17, checkBox18, checkBox19, comboBox7, "Bindings", "AimGroin");
            SetKeybind(checkBox20, checkBox21, checkBox22, comboBox8, "Bindings", "AimTorso");
            SetKeybind(checkBox23, checkBox24, checkBox25, comboBox9, "Bindings", "AimLeftLeg");
            SetKeybind(checkBox26, checkBox27, checkBox28, comboBox10, "Bindings", "AimRightLeg");
            SetKeybind(checkBox29, checkBox30, checkBox31, comboBox11, "Bindings", "AimLeftArm");
            SetKeybind(checkBox32, checkBox33, checkBox34, comboBox12, "Bindings", "AimRightArm");*/

            // datafiles
            String[] datafiles=parser.EnumSectionLines("DataFiles");
            this.richTextBox1.Lines = datafiles;

            // master.dat and critter.dat
            if (File.Exists(@"DataFiles2238.cfg"))
            {
                StreamReader sr = new StreamReader(@"DataFiles2238.cfg");
                string line;
                line=sr.ReadLine();
                if (line != null)
                {
                    textBox1.Text = line;
                    line = sr.ReadLine();
                    if (line != null) textBox2.Text = line;
                }
                sr.Close();
            }
        }

        private Dictionary<String,String> ParseDatafiles()
        {
            Dictionary<String, String> ret = new Dictionary<String, String>();
            String[] lines = richTextBox1.Lines;
            foreach (String line in lines)
            {
                String trimline = line.Trim();
                if (trimline.Length == 0) continue;
                String[] splitted = trimline.Split('=');
                if (splitted.Length != 2)
                {
                    MessageBox.Show("Error parsing DataFiles entry '" + line + "'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
                ret[splitted[0]] = splitted[1];
            }
            return ret;
        }

        private void SaveConfig()
        {
            // datafiles
            Dictionary<String, String> datafiles = ParseDatafiles();
            if (datafiles == null) return;
            parser.DeleteSection("DataFiles");
            foreach (KeyValuePair<String, String> pair in datafiles)
                parser.AddSetting("DataFiles", pair.Key, pair.Value);

            // 2238
            SetFromObject(checkBox1, "2238", "DisplayNames");
            SetFromObject(checkBox2, "2238", "Awareness");
            SetFromObject(checkBox3, "2238", "DisplayTC");
            SetFromObject(comboBox1, "2238", "DisplayTCZones");
            SetFromObject(checkBox5, "2238", "Autosave");
            parser.AddSetting("2238", "MouseScroll", checkBox6.Checked?"0":"1");

            // timeouts
            SetFromObject(checkBox4, "Timeouts", "Enabled");
            SetFromObject(comboBox2, "Timeouts", "Direction");
            parser.AddSetting("Timeouts", "Font", (comboBox3.SelectedIndex + 4).ToString());
            SetFromObject(numericUpDown1, "Timeouts", "Transparency");
            SetFromObject(checkBox7, "Timeouts", "Worldmap");

            // timeouts colors
            SetFromObject(button4, "Timeouts", "NameColor");
            SetFromObject(button5, "Timeouts", "ValueColor");

            // timeouts pos
            SetFromObject(numericUpDown2, "Timeouts", "PositionX");
            SetFromObject(numericUpDown3, "Timeouts", "PositionY");
            SetFromObject(numericUpDown4, "Timeouts", "SpacingX");
            SetFromObject(numericUpDown5, "Timeouts", "SpacingY");
            SetFromObject(numericUpDown7, "Timeouts", "WorldmapX");
            SetFromObject(numericUpDown6, "Timeouts", "WorldmapY");

            KeyBinds.Save(parser);
            parser.SaveSettings();

            // DataFiles2238.cfg
            StreamWriter sw = new StreamWriter(@"DataFiles2238.cfg");
            sw.WriteLine(textBox1.Text);
            sw.WriteLine(textBox2.Text);
            sw.Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            parser = new IniParser(@".\FOnline2238.cfg");
            LoadConfig();
            KeyBinds.Init(listBoxBindings, parser);
            KeybindForm.LoadIndex();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ButtonColor(button4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ButtonColor(button5);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveConfig();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("FOnline.exe");
            }
            catch (Exception)
            {
                MessageBox.Show("Couldn't run FOnline.exe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Environment.Exit(0);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"http://www.fonline2238.net/wiki/2238");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"http://www.fonline2238.net/wiki/Timeouts");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"http://www.fonline2238.net/wiki/DataFiles");
        }

        private void listBoxBindings_DoubleClick(object sender, EventArgs e)
        {
            KeyBinds.DoubleClick();
        }

        private void btnAddBind_Click(object sender, EventArgs e)
        {
            KeyBinds.AddPressed();
        }

        private void btnRemoveBind_Click(object sender, EventArgs e)
        {
            KeyBinds.RemovePressed();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog FileDialog = new OpenFileDialog();
            FileDialog.RestoreDirectory = true;
            FileDialog.Filter = "Master Datafile|master.dat";
            FileDialog.CheckFileExists = true;
            if (FileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            textBox1.Text=FileDialog.FileName;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog FileDialog = new OpenFileDialog();
            FileDialog.RestoreDirectory = true;
            FileDialog.Filter = "Critter Datafile|critter.dat";
            FileDialog.CheckFileExists = true;
            if (FileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            textBox2.Text = FileDialog.FileName;
        }
    }
}
