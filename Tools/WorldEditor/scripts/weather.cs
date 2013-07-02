using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using WorldEditor;
using FOCommon.WMLocation;
using WorldEditor.Scripting;

// return extension info

public class WeatherZone
{
    const int RAIN_NONE = 0;
    const int RAIN_OUTSIDE = 1;
    const int RAIN_OUTSIDE_DESERT = 2;
    const int RAIN_BUILDING = 3;
    const int RAIN_BUILDING_TOP = 4;
    const int RAIN_BUILDING_BOTTOM = 5;
    const int RAIN_CAVE = 6;
    const int RAIN_MINE = 7;
    const int RAIN_COAST = 8;
    const int RAIN_SEA = 9;

    public int X { get; set; }
    public int Y { get; set; }
    public int RainMode { get; set; }
    public WeatherZone(int x, int y) { this.X = x; this.Y = y; this.RainMode = -1; }
    public WeatherZone() { this.RainMode = -1; }
}

public class WeatherExt : IScript
{
    Form MainForm;
    Label lblRain;
    ComboBox cmbRain;
    TabPage tabWeather;

    List<WeatherZone> Zones = new List<WeatherZone>();
    WeatherZone CurrentZone;

    public string get_name() { return "Weather"; }
    public string get_author() { return "Ghosthack"; }
    public string get_description() { return "For setting weather data (rain) on zones."; }
    public string get_version() { return "1.0"; }

    Control GetControl(string Name)
    {
        Control[] controls = MainForm.Controls.Find(Name, true);
        if (controls == null || controls.Length == 0)
            MessageBox.Show(Name + " not found!");
        return controls[0];
    }

    private void Save()
    {
        if (CurrentZone != null)
        {
            CurrentZone.RainMode = cmbRain.SelectedIndex;
        }

        List<String> lines = new List<string>();
        foreach (WeatherZone zone in Zones)
        {
            if(zone.RainMode!=-1)
                lines.Add(zone.X + "," + zone.Y + "|" + zone.RainMode);
        }
        File.WriteAllLines(Config.PathMapsDir + "\\weather.fowm", lines.ToArray());
    }

    private void Load()
    {
        if (!File.Exists(Config.PathMapsDir + "\\weather.fowm"))
            return;

        foreach (String line in File.ReadAllLines(Config.PathMapsDir + "\\weather.fowm"))
        {
            string[] param = line.Split('|');
            string[] coords = param[0].Split(',');

            WeatherZone weather = new WeatherZone() { X = Int32.Parse(coords[0]), Y = Int32.Parse(coords[1]), RainMode=Int32.Parse(param[1]) };
            Zones.Add(weather);
        }
    }

    // called after script is loaded, no resources loaded
    public bool init(Form Main)
    {
        MainForm = Main;
        return true;
    }

    public bool save_zones(string fileName)
    {
        Save();
        return false;
    }

    public bool zone_activated(int x, int y) 
    {
        WeatherZone zone = Zones.Find(zo => zo.X == x && zo.Y == y);
        if (zone == null)
        {
            zone = new WeatherZone(x,y);
            Zones.Add(zone);
        }

        if (CurrentZone != null)
        {
            CurrentZone.RainMode = cmbRain.SelectedIndex;
        }
        cmbRain.SelectedIndex = zone.RainMode;
        CurrentZone = zone;
        return false;
    }

    public void main_form_loaded()
    {
        TabControl TabCtrl = (TabControl)GetControl("tabZoneProperties");
        tabWeather = new TabPage("Weather");
        tabWeather.UseVisualStyleBackColor = true;
        TabCtrl.TabPages.Add(tabWeather);

        MainForm.Controls.Remove(TabCtrl);

        lblRain = new Label();
        lblRain.Text = "Rain Type";

        string[] types =  {"RAIN_NONE", "RAIN_OUTSIDE", "RAIN_OUTSIDE_DESERT",
                  "RAIN_BUILDING", "RAIN_BUILDING_TOP", "RAIN_BUILDING_BOTTOM",
                  "RAIN_CAVE", "RAIN_MINE", "RAIN_COAST", "RAIN_SEA" };

        cmbRain = new ComboBox();
        cmbRain.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbRain.DropDownWidth = 200;
        cmbRain.Items.AddRange(types);
        cmbRain.SelectedIndex = -1;

        tabWeather.Controls.Add(cmbRain);
        tabWeather.Controls.Add(lblRain);

        lblRain.Location = new System.Drawing.Point(7, 10);
        cmbRain.Location = new System.Drawing.Point(65, 7);

        Load();
    }
}