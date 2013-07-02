using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using WorldEditor;
using FOCommon.WMLocation;

using WorldEditor.Scripting;

public class SDKZone : IZone
{
    public SDKZone(int x, int y)
    {
        X = x;
        Y = y;
        Modified = false;
    }

    public IZone Clone() { return ((SDKZone)this.MemberwiseClone()); }

    public string Table { get; set; }
    public string Chance { get; set; }
    public string Terrain { get; set; }
    public string Fill { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public bool Modified { get; set; }
    public bool Brushed { get; set; }
}

public class Script : IScript
{
    List<SDKZone> Zones;
    SDKZone CurrentZone;
    Dictionary<String, int> Defines;

    private List<SDKZone> LoadZones(string FileName)
    {
        List<SDKZone> Zones = new List<SDKZone>();
        try
        {
            List<string> lines = new List<string>(File.ReadAllLines(FileName));

            string version;
            if (!WorldMapUtils.VerifyFormat(lines[0], FileName, "SDK", out version))
                return null;

            foreach (string line in lines)
            {
                if (line.Length == 0 || (line.Contains("Format") && line.Contains("Version")))
                    continue;

                String[] SplittedLine = line.Split('|');
                String[] Coords = SplittedLine[0].Split(',');
                String[] Parameters = SplittedLine[1].Split(',');

                SDKZone zone = new SDKZone(Int32.Parse(Coords[0]), Int32.Parse(Coords[1]));
                zone.Table = Parameters[0];
                zone.Terrain = Parameters[1];
                zone.Fill = Parameters[2];
                zone.Chance = Parameters[3];
                Zones.Add(zone);
            }
        }
        catch (FileNotFoundException)
        {
            WorldEditor.Message.Show("Couldn't open " + FileName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
        catch (Exception e)
        {
            WorldEditor.Message.Show("Exception occured: " + e.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
        return Zones;
    }

    private string Get(bool Compile, string Str)
    {
        if (!Compile) return Str;
        if(!Defines.ContainsKey(Str))
        {
            WorldEditor.Message.Show("Couldn't find define '" + Str + "'.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            throw new KeyNotFoundException(Str);
        }

        return Defines[Str].ToString();
    }

    public bool SaveZones(string FileName, Dictionary<String, int> defines, bool Compile)
    {
        try
        {
            List<String> lines = new List<string>();
            lines.Add("Format=SDK|Version=1");
            foreach (SDKZone Zone in Zones.OrderBy(i => i.X))
            {
                lines.Add(String.Format("{0},{1}|{2},{3},{4},{5}",
                    Zone.X,
                    Zone.Y,
                    Get(Compile, Zone.Table),
                    Get(Compile, Zone.Terrain),
                    Get(Compile, Zone.Fill),
                    Get(Compile, Zone.Chance)));
            }
            File.WriteAllLines(FileName, lines.ToArray());
        }
        catch (FileNotFoundException)
        {
            MessageBox.Show("Couldn't open " + FileName, "WorldEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
        catch (Exception e)
        {
            MessageBox.Show("Exception occured: " + e.Message, "WorldEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
        return true;
    }

    Form MainForm;

    ComboBox cmbChance;
    ComboBox cmbTerrain;
    ComboBox cmbTable;

    ComboBox cmbShowChance;
    ComboBox cmbShowTerrain;
    ComboBox cmbShowTable;

    CheckBox chkShowChance;
    CheckBox chkShowTerrain;
    CheckBox chkShowTable;
    CheckBox chkShowModified;
    CheckBox chkShowImplemented;
    CheckBox chkShowZones;

    // return extension info
    public string get_name() { return "SDK Mode"; }
    public string get_author() { return "Ghosthack"; }
    public string get_description() { return "This module implements SDK mode, which provides a much more scaled down UI, with a lot of features disabled."; }
    public string get_version() { return "0.1"; }
    public int get_api_version() { return 1; }

    bool FormLoaded = false;

    // called after script is loaded, no resources loaded
    public bool init(Form Main)
    {
        MainForm = Main;
        return true;
    }

    public void resources_loaded()
    {
        ScriptGlobal.RefreshWorldMap();
    }

    ToolStripItem GetControlMenu(string Name)
    {
        ToolStripItem[] controls = MainForm.MainMenuStrip.Items.Find(Name, true);
        if (controls == null || controls.Length == 0)
            MessageBox.Show(Name + " not found!");
        return controls[0];
    }

    Control GetControl(string Name)
    {
        Control[] controls = MainForm.Controls.Find(Name, true);
        if(controls==null || controls.Length==0)
            MessageBox.Show(Name + " not found!");

        return controls[0];
    }

    public void main_form_loaded()
    {
        FormLoaded = true;
        // Needed controls
        cmbChance = (ComboBox)GetControl("cmbChance");
        cmbTable = (ComboBox)GetControl("cmbTable");
        cmbTerrain = (ComboBox)GetControl("cmbTerrain");

        cmbShowChance = (ComboBox)GetControl("cmbShowChance");
        cmbShowTable = (ComboBox)GetControl("cmbShowTable");
        cmbShowTerrain = (ComboBox)GetControl("cmbShowTerrain");

        chkShowChance        = (CheckBox)GetControl("chkShowChance");
        chkShowTerrain       = (CheckBox)GetControl("chkShowTerrain");
        chkShowTable         = (CheckBox)GetControl("chkShowTable");
        chkShowModified      = (CheckBox)GetControl("chkShowModified");
        chkShowImplemented   = (CheckBox)GetControl("chkShowImplemented");
        chkShowZones         = (CheckBox)GetControl("chkShowZones");

        // Hide all non-SDK stuff
        ToolStrip Bar = (ToolStrip)GetControl("toolBar");
        foreach (ToolStripItem Item in Bar.Items)
            if (Item.Name == "toolbtnErase" || Item.Name == "toolbtnBrush") Item.Visible = false;


        StatusStrip Status = (StatusStrip)GetControl("StatusStrip1");
        foreach (ToolStripItem Item in Status.Items)
            if (Item.Name == "toolStripStatusLabelMode") Item.Text = "| SDK mode.";
        

        GetControl("tabZoneProperties").Visible = false;
        GetControl("lblEncounterGroup").Visible = false;
        GetControl("lblEncounterLocation").Visible = false;
        GetControl("cmbShowGroup").Visible = false;
        GetControl("chkShowGroup").Visible = false;
        GetControl("cmbShowLocation").Visible = false;
        GetControl("chkShowLocation").Visible = false;
        GetControl("lblFlag").Visible = false;
        GetControl("cmbShowFlag").Visible = false;
        GetControl("chkShowFlag").Visible = false;
        GetControl("lblShowChance").Visible = true;
        GetControl("cmbShowChance").Visible = true;
        GetControl("chkShowChance").Visible = true;

        GetControl("chkHideZoneProperties").Visible = false;
        
        GetControl("chkShowNoEncounter").Visible = false;
        GetControl("chkShowNoLocation").Visible = false;
        GetControl("lblDifficulty").Visible = false;
        GetControl("numericDifficulty").Visible = false;

        GetControlMenu("encounterToolStripMenuItem").Visible = false;

        GetControl("lblTable").Visible = true;
        GetControl("lblChance").Visible = true;
        GetControl("cmbChance").Visible = true;
        GetControl("cmbTable").Visible = true;
        GetControl("cmbShowTable").Visible = true;
        GetControl("lblEncounterTable").Visible = true;
        GetControl("chkShowTable").Visible = true;

        GetControl("lblShowChance").Location = GetControl("lblFlag").Location;
        GetControl("cmbShowChance").Location = GetControl("cmbShowFlag").Location;
        GetControl("chkShowChance").Location = GetControl("chkShowFlag").Location;

        TabPage TabFilters = (TabPage)GetControl("tabFilters");
        foreach (Control Ctrl in TabFilters.Controls)
        {
            if (Ctrl.Location.Y > 100)
                Ctrl.Location = new Point(Ctrl.Location.X, Ctrl.Location.Y- 100);
        }
        TabFilters.Parent.Height -= 100;

        GetControl("tabControl1").Location = new Point(6, 200);
        GetControl("cmbChance").Location = new Point(102, 62);
        GetControl("lblChance").Location = new Point(8, 64);
        GetControl("cmbTable").Location = new Point(102, 88);
        GetControl("lblTable").Location = new Point(8, 90);

        TabControl Parent = (TabControl)TabFilters.Parent;

        foreach (TabPage Ctrl in Parent.TabPages)
        {
            if (Ctrl.TabIndex != TabFilters.TabIndex)
                TabFilters.Parent.Controls.Remove(Ctrl);
        }

        GetControl("grpSelectedZone").Height -= 350;
    }

    void IfSetModified(SDKZone zone, string str1, string str2)
    {
        if (str1 != str2)
        {
            //MessageBox.Show("modified");
            zone.Modified = true;
        }
    }

    void SetZone(SDKZone zone, bool ToUI)
    {
        if (ToUI)
        {
            cmbChance.Enabled = true;
            cmbTerrain.Enabled = true;
            cmbTable.Enabled = true;

            cmbChance.Text = zone.Chance;
            cmbTable.Text = zone.Table;
            cmbTerrain.Text = zone.Terrain;
        }
        else
        {
            IfSetModified(zone, zone.Chance, cmbChance.Text);
            IfSetModified(zone, zone.Table, cmbTable.Text);
            IfSetModified(zone, zone.Terrain, cmbTerrain.Text);
            zone.Chance = cmbChance.Text;
            zone.Table = cmbTable.Text;
            zone.Terrain = cmbTerrain.Text;
        }
    }

    public bool load_zones(string fileName)
    {
        Zones = LoadZones(fileName);
        return true;
    }

    public bool compile_zones(string fileName, Dictionary<String, int> defines)
    {
        SaveZones(fileName, defines, true);
        return true;
    }

    public bool save_zones(string fileName)
    {
        SaveZones(fileName, null, false);
        return true;
    }

    // called after drawing of worldmap
    public void draw_worldmap(Graphics g) 
    {
        if (g == null || Zones == null || !FormLoaded)
            return;

        foreach(SDKZone i in Zones)
        {
            if (chkShowImplemented.Checked) Drawing.DrawZone(g, Drawing.ImplementedColor, i.X, i.Y);
            if (chkShowZones.Checked)
            {
                if (chkShowTable.Checked && i.Table == cmbShowTable.Text) Drawing.DrawZone(g, Drawing.FilteredColor, i.X, i.Y);
                if (chkShowTerrain.Checked && i.Terrain == cmbShowTerrain.Text) Drawing.DrawZone(g, Drawing.FilteredColor, i.X, i.Y);
                if (chkShowChance.Checked && i.Chance == cmbShowChance.Text) Drawing.DrawZone(g, Drawing.FilteredColor, i.X, i.Y);
            }
            if (chkShowModified.Checked && i.Modified) Drawing.DrawZone(g, Drawing.ModifiedColor, i.X, i.Y);
        }

        if (CurrentZone != null && CurrentZone.X != -1 && CurrentZone.Y != -1)
            Drawing.DrawZone(g, Drawing.SelectedColor, CurrentZone.X, CurrentZone.Y);
    }
    public bool zone_activated(int x, int y) 
    {
        SDKZone szone = Zones.Find(zone => zone.X == x && zone.Y == y);
        if (szone == null)
        {
            szone = new SDKZone(x, y);
            Zones.Add(szone);
        }

        if (CurrentZone != null)
            SetZone(CurrentZone, false);
        SetZone(szone, true);
        CurrentZone = szone;

        cmbShowChance.Enabled = true;
        cmbShowTable.Enabled = true;
        cmbShowTerrain.Enabled = true;

        //ScriptGlobal.SetSelectedZone(x, y);
        RefreshWorldMap();

        return true;
    }
}
