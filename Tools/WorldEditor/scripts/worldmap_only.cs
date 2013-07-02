using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

using WorldEditor;
using FOCommon.WMLocation;

using WorldEditor.Scripting;


public class WorldmapOnly : IScript
{
    Form MainForm;

    // return extension info
    public string get_name() { return "Worldmap only"; }
    public string get_author() { return "Ghosthack"; }
    public string get_description() { return "Maximum size for worldmap, no zones are loaded, only locations and all the other data."; }
    public string get_version() { return "1.0"; }

    // called after script is loaded, no resources loaded
    public bool init(Form Main)
    {
        MainForm = Main;
        return true;
    }

    Control GetControl(string Name)
    {
        Control[] controls = MainForm.Controls.Find(Name, true);
        if (controls == null || controls.Length == 0)
            MessageBox.Show(Name + " not found!");
        return controls[0];
    }

    public void main_form_loaded() 
    {
        MainForm.Controls.Remove(GetControl("toolBar"));
        MainForm.Controls.Remove(GetControl("grpSelectedZone"));
        MainForm.Controls.Remove(GetControl("TabControl1"));
        Panel pnl = (Panel)GetControl("pnlWorldMap");
        pnl.Dock = DockStyle.Fill;
        pnl.Focus();
    }
}
