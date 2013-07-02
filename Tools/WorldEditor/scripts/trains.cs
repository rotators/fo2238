using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;

using WorldEditor;
using FOCommon.WMLocation;

using WorldEditor.Scripting;

// TODO: add actions to menu

public class TrainWaypoint
{
    public string Name { get; set; }
    public Point Position { get; set; }
}

public class TrainRoute
{
    public string Name { get; set; }
    public List<TrainWaypoint> WayPoints { get; set; }
    public Color Color { get; set; }
}

#region InputForm
public class frmTrain : System.Windows.Forms.Form
{
    private System.Windows.Forms.Button btnSay;
    private System.Windows.Forms.TextBox txtText;

    public string TName { get; set; }
    public Color TColor { get; set; }
    private bool Route;
    public bool Finished { get; set; }

    public frmTrain(bool route)
    {
        Route = route;
        InitializeComponent();

    }

    private void InitializeComponent()
    {   
            this.btnOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblColor = new System.Windows.Forms.Label();
            this.cmbColor = new FOCommon.Controls.ColorComboBox();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(130, 60);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(80, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(54, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(225, 20);
            this.txtName.TabIndex = 2;
            this.lblColor.AutoSize = true;
            this.lblColor.Location = new System.Drawing.Point(17, 38);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(31, 13);
            this.lblColor.TabIndex = 3;
            this.lblColor.Text = "Color";
            // 
            // cmbColor
            // 
            this.cmbColor.Color = System.Drawing.Color.Black;
            this.cmbColor.CustomColor = System.Drawing.Color.Empty;
            this.cmbColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbColor.FormattingEnabled = true;
            this.cmbColor.Location = new System.Drawing.Point(54, 35);
            this.cmbColor.Name = "cmbColor";
            this.cmbColor.Size = new System.Drawing.Size(156, 21);
            this.cmbColor.TabIndex = 4;
            // 
            // DeleteMe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 95);
            this.Controls.Add(this.cmbColor);
            this.Controls.Add(this.lblColor);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOk);
            this.Name = "DeleteMe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Trains - New Waypoint";
            this.Load += frmTrain_Load;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void frmTrain_Load(object sender, EventArgs e)
        {
            if (Route)
                this.Text = "Trains - New Route";
            else
            {
                this.Text = "Trains - New Waypoint";
                this.lblColor.Hide();
                this.cmbColor.Hide();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.TName = txtName.Text;
            if (Route)
            {
                string ColorStr="";
                FOCommon.Utils.SetColor(cmbColor, ref ColorStr, false);
                this.TColor = FOCommon.Utils.GetColor(ColorStr);
            }
            Finished = true;
            this.Close();
        }

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblColor;
        private FOCommon.Controls.ColorComboBox cmbColor;
}
#endregion

public class Script : IScript
{
    Form MainForm;

    List<TrainRoute> Routes = new List<TrainRoute>();
    TrainRoute CurrentRoute=null;

    // return extension info
    public string get_name()        { return "Train Routes"; }
    public string get_author()      { return "Ghosthack"; }
    public string get_description() { return "An extension for manipulating train routes. Data loaded/saved in trains.fowm"; }
    public string get_version()     { return "1.0"; }

    public bool init(Form Main)
    {
        MainForm = Main;
        return true;
    }

    bool ActionDone = false;
    bool FormLoaded = false;

    Control GetControl(string Name)
    {
        Control[] controls = MainForm.Controls.Find(Name, true);
        if (controls == null || controls.Length == 0)
            MessageBox.Show(Name + " not found!");
        return controls[0];
    }

    public void main_form_loaded() 
    { 
        FormLoaded = true;
        ToolStripMenuItem SaveMenuItem = new ToolStripMenuItem("Save Train Routes");
        SaveMenuItem.Click += SaveRoutes_Click;
        
        foreach (ToolStripMenuItem Item in MainForm.MainMenuStrip.Items)
        {
            if (Item.Name == "fileToolStripMenuItem")
            {
                Item.DropDownItems.Insert(2, SaveMenuItem);
            }
                
        }
        LoadRoutes();
        
        ScriptGlobal.RefreshWorldMap();
        GetControl("pnlWorldMap").Focus();
    }

    private void SaveRoutes_Click(object sender, EventArgs e)
    {
        SaveRoutes();
        MessageBox.Show("Trains saved!");
    }

    private void SaveRoutes()
    {
        //if(CurrentRoute!=null)
        //    Routes.Add(CurrentRoute);
        //CurrentRoute = null;

        List<String> lines = new List<string>();
        foreach (TrainRoute route in Routes)
        {
            if (route.WayPoints.Count == 0)
                continue;

            string line = "" + route.Name + "|" + route.Color.R + "," + route.Color.G + "," + route.Color.B + "," + route.Color.A;
            foreach (TrainWaypoint wp in route.WayPoints)
            {
                line += "|" + /*+ wp.Name + "," +*/ wp.Position.X + "," + wp.Position.Y;
            }
            lines.Add(line);
        }
        File.WriteAllLines(".\\trains.fowm", lines.ToArray());
    }

    private void LoadRoutes()
    {
        if(!File.Exists(".\\trains.fowm"))
            return;

        foreach(String line in File.ReadAllLines(".\\trains.fowm"))
        {
            string[] param = line.Split('|');
            TrainRoute route = new TrainRoute(){Name=param[0], Color=FOCommon.Utils.GetColor(param[1]), WayPoints=new List<TrainWaypoint>()};
            for(int i=2;i<param.Length;i++)
            {
                string[] wpparam = param[i].Split(',');
                //TrainWaypoint wp = new TrainWaypoint(){Position=new Point(Int32.Parse(wpparam[0]), Int32.Parse(wpparam[1]))};
                TrainWaypoint wp = new TrainWaypoint() { Position = new Point(Int32.Parse(wpparam[0]), (Int32.Parse(wpparam[1]))) };
                route.WayPoints.Add(wp);
            }
            Routes.Add(route);
        }
    }

    public void draw_worldmap(Graphics Surface) 
    {
        if (!FormLoaded)
            return;

        Brush brush = Brushes.Red;
        Font font = new Font(FontFamily.GenericSansSerif, 8.0f);
        if (true /*!ActionDone*/)
        {
            Drawing.DrawOutlinedText(Surface, "Trains extension loaded, use Ctrl+T to add route after adding waypoints.", font, brush, new PointF(5.0f, 5.0f), Brushes.Black);
            Drawing.DrawOutlinedText(Surface, "Ctrl+Left click to add new waypoint. Ctrl+Left-click while holding right button to remove all waypoints.", font, brush, new PointF(5.0f, 35.0f), Brushes.Black);
            Drawing.DrawOutlinedText(Surface, "Routes shown in different colors and with name on first waypoint.", font, brush, new PointF(5.0f, 50.0f), Brushes.Black);
            Drawing.DrawOutlinedText(Surface, "To delete a route or waypoint, use Ctrl+Right-click on it", font, brush, new PointF(5.0f, 65.0f), Brushes.Black);
        }

        List<TrainRoute> DrawRoutes = new List<TrainRoute>();
        DrawRoutes.AddRange(Routes);
        if(CurrentRoute!=null)
            DrawRoutes.Add(CurrentRoute);

        // Draw lines, so that waypoint is always on-top, even when drawing over others.
        foreach (TrainRoute route in DrawRoutes)
        {
            for (int i = 0; i < route.WayPoints.Count; i++)
            {
                TrainWaypoint wp = route.WayPoints[i];
                Brush br = new SolidBrush(route.Color);
                Pen pen = new Pen(Brushes.Black);



                if (i > 0)
                {
                    Point from = new Point(Display.GameCoordsToPixel(route.WayPoints[i - 1].Position.X), Display.GameCoordsToPixel(route.WayPoints[i - 1].Position.Y));
                    Point to   = new Point(Display.GameCoordsToPixel(wp.Position.X), Display.GameCoordsToPixel(wp.Position.Y));

                    Surface.DrawLine(new Pen(br, 3.0f), from, to);
                }
            }
        }

        foreach (TrainRoute route in DrawRoutes)
        {
            for (int i=0;i<route.WayPoints.Count;i++)
            {
                TrainWaypoint wp = route.WayPoints[i];
                Brush br = new SolidBrush(route.Color);
                Pen pen = new Pen(Brushes.Black);

                int PosX = Display.GameCoordsToPixel(wp.Position.X);
                int PosY = Display.GameCoordsToPixel(wp.Position.Y);

                if (i == 0)
                    Drawing.DrawOutlinedText(Surface, route.Name, font, brush, new PointF(PosX - 8, PosY + 8), Brushes.Black);

                Surface.FillRectangle(br, PosX - 8, PosY - 8, 16, 16);
                Surface.DrawRectangle(pen, PosX - 8, PosY - 8, 16, 16);
            }
        }
    }

    private TrainWaypoint CreateWaypoint(int x, int y)
    {
        /*frmTrain frm = new frmTrain(false);
        frm.ShowDialog();
        if (!frm.Finished)
            return null;*/
        return new TrainWaypoint{Name = "", Position= new Point(x, y)};
    }

    private TrainRoute CreateRoute()
    {
        frmTrain frm = new frmTrain(true);
        frm.ShowDialog();
        if (!frm.Finished)
            return null;
        if (String.IsNullOrEmpty(frm.Name))
        {
            MessageBox.Show("No name!");
            return null;
        }
        return new TrainRoute{Name= frm.TName, Color= frm.TColor};
    }

    public bool key_up(Form EventForm, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.T && e.Control)
        {
            if (CurrentRoute == null)
                return false;

            Routes.Add(CurrentRoute);
            MessageBox.Show(CurrentRoute.Name + " added to list.");
            CurrentRoute = null;
        }
        return false;
    }

    private void DeleteWaypoint(TrainWaypoint wp)
    {
        List<TrainRoute> CheckRoutes = new List<TrainRoute>();
        CheckRoutes.AddRange(Routes);
        if (CurrentRoute != null)
            CheckRoutes.Add(CurrentRoute);
        for (int i=0;i<CheckRoutes.Count;i++)
        {
            TrainRoute route = CheckRoutes[i];

            if (route.WayPoints.Contains(wp))
            {
                route.WayPoints.Remove(wp);
                return;
            }
        }
    }

    public TrainWaypoint GetWaypoint(int x, int y)
    {
        List<TrainRoute> CheckRoutes = new List<TrainRoute>();
        CheckRoutes.AddRange(Routes);
        if (CurrentRoute != null)
            CheckRoutes.Add(CurrentRoute);
        foreach (TrainRoute route in CheckRoutes)
        {
            foreach (TrainWaypoint wp in route.WayPoints)
            {
                if ((x >= wp.Position.X - 16 && x <= (wp.Position.X + 16)) && (y >= wp.Position.Y - 16 && y <= (wp.Position.Y + 16)))
                    return wp;
            }
        }
        return null;
    }

    public bool worldmap_coords_clicked(MouseEventArgs e, int x, int y) 
    {
        int GameX = Display.PixelToGameCoords(x);
        int GameY = Display.PixelToGameCoords(y);

        if (Control.ModifierKeys == Keys.Control && e.Button == MouseButtons.Left)
        {
            ActionDone = true;

            if (Control.MouseButtons == MouseButtons.Right)
            {
                if (CurrentRoute == null)
                {
                    MessageBox.Show("No route currently constructed.");
                    return false;
                }
                else
                {
                    CurrentRoute.WayPoints = new List<TrainWaypoint>();
                }
            }
            //MessageBox.Show("Left with ctrl.");
            if (CurrentRoute == null)
            {
                TrainRoute route = CreateRoute();
                if (route == null)
                    return false;
                route.WayPoints = new List<TrainWaypoint>();
                CurrentRoute = route;
                //Routes.Add(route);
            }
            TrainWaypoint wp = CreateWaypoint(GameX, GameY);
            if (wp == null || CurrentRoute == null)
                return false;
            CurrentRoute.WayPoints.Add(wp);
            ScriptGlobal.RefreshWorldMap();
            return true;
        }
        if (Control.ModifierKeys == Keys.Control && e.Button == MouseButtons.Right)
        {
            TrainWaypoint wp = GetWaypoint(GameX, GameY);
            if (wp == null)
                return false;
            DeleteWaypoint(wp);
            ScriptGlobal.RefreshWorldMap();
            return true;
        }

        return false;
    }
}
