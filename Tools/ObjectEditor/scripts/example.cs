using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using ObjectEditor.Scripting;

public class NoFOnline : IScript
{
    Form MainForm;
    // Return extension info
    public string get_name()        { return "Example"; }
    public string get_author()      { return "Ghosthack"; }
    public string get_description() { return "Test extension for ObjectEditor."; }
    public string get_version()     { return "1.0"; }

    // Called after script is loaded, no resources loaded
    public bool init(Form Main)
    {
        MainForm = Main;
        return true; // Init was ok
    }

    // Called on adding control, set Add=false if you want to remove a control.
    public bool add_control(ref Control Ctrl, ref bool Add)
    {
        return false; // We don't intercept the call, letting other scripts handle the event too.
    }
}