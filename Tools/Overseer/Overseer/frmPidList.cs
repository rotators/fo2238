using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Overseer
{
    public partial class frmPidList : Form
    {
        public List<Tuple<int, int>> loadData = null;
        private readonly List<Tuple<int, int>> saveData = null;

        private bool loadMode { get { return (saveData == null); } }
        private bool saveMode { get { return (saveData != null); } }

        private class PidList
        {
            public readonly string Name;
            public List<Tuple<int, int>> Data = new List<Tuple<int, int>>();

            public PidList( string name )
            {
                Name = name;
            }

            public override string ToString()
            {
                if( Data.Count == 0 )
                    return (null);

                string result = Name;
                foreach( Tuple<int, int> element in Data )
                {
                    result += "^" + element.Item1 + "^" + element.Item2;
                }
                return (result);
            }
        }

        private List<PidList> global = new List<PidList>();
        private List<PidList> local = new List<PidList>();

        // load mode
        public frmPidList()
        {
            InitializeComponent();

            Init();
        }

        // save mode
        public frmPidList( List<Tuple<int, int>> saveData, bool save )
        {
            InitializeComponent();

            this.saveData = new List<Tuple<int, int>>();
            this.saveData = saveData;
            Init();
        }

        private void Init()
        {
            if( (!loadMode && !saveMode) ||
                (loadMode && saveMode) )
            {
                MessageBox.Show( "Internal error: init" );
                Close();
                return;
            }

            global = ParseList( Overseer.Config.ListPidsGlobal );
            local = ParseList( Overseer.Config.ListPidsLocal );

            if( loadMode )
            {
                btnLoadSave.Text = "Load";
                nameGlobal.Visible = false;
                nameLocal.Visible = false;
                Update();
                Refresh();
            }
            else
            {
                btnLoadSave.Text = "Save";
            }
            Text = btnLoadSave.Text + " " + Text;
            RefreshLists();
            btnDelete.Enabled = false;
            btnLoadSave.Enabled = false;
        }

        private void RefreshLists()
        {
            listGlobal.Items.Clear();
            listLocal.Items.Clear();

            foreach( PidList glist in global )
            {
                listGlobal.Items.Add( glist.Name );
            }
            foreach( PidList llist in local )
            {
                listLocal.Items.Add( llist.Name );
            }
        }

        private List<PidList> ParseList( string list )
        {
            List<PidList> result = new List<PidList>();

            if( list == null )
                return (result);

            string[] data = list.Split( new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries );
            foreach( string sList in data )
            {
                string[] elements = sList.Split( new char[] { '^' }, StringSplitOptions.RemoveEmptyEntries );
                PidList pidList = null;
                int[] tElements = new int[2] { -1, 0 };
                int idx = 0;
                foreach( string sElement in elements )
                {
                    int tmp;
                    if( idx == 0 )
                    {
                        pidList = new PidList( sElement );
                    }
                    else if( idx > 0 && idx <= tElements.Length )
                    {
                        if( int.TryParse( sElement, out tmp ) )
                            tElements[idx - 1] = tmp;
                        if( idx == tElements.Length )
                        {
                            pidList.Data.Add( new Tuple<int, int>( tElements[0], tElements[1] ) );
                            tElements = new int[2] { -1, 0 };
                            idx = 0;
                        }
                    }
                    idx++;
                }
                if( pidList != null && pidList.Data.Count > 0 )
                    result.Add( pidList );
            }
            return (result);
        }

        private string PidListString( List<PidList> pidList )
        {
            string result = "";
            bool first = true;
            // TODO: pidList.Sort();
            foreach( PidList list in pidList )
            {
                if( !first )
                    result += "|";
                result += list.ToString();
                first = false;
            }
            return (result);
        }

        // events

        private void tabs_SelectedIndexChanged( object sender, EventArgs e )
        {
            listGlobal.SelectedIndex = -1;
            listLocal.SelectedIndex = -1;
            btnDelete.Enabled = false;
            btnLoadSave.Enabled = false;
        }

        private void listGlobal_SelectedIndexChanged( object sender, EventArgs e )
        {
            ListBox self = (ListBox)sender;

            nameGlobal.Text = (string)self.SelectedItem;

            if( self.SelectedIndex >= 0 )
            {
                btnDelete.Enabled = true;
                if( loadMode )
                    btnLoadSave.Enabled = true;
            }
        }

        private void listLocal_SelectedIndexChanged( object sender, EventArgs e )
        {
            ListBox self = (ListBox)sender;

            nameLocal.Text = (string)self.SelectedItem;

            if( self.SelectedIndex >= 0 )
            {
                btnDelete.Enabled = true;
                if( loadMode )
                    btnLoadSave.Enabled = true;
            }
        }

        private void name_TextChanged( object sender, EventArgs e )
        {
            TextBox self = (TextBox)sender;

            if( saveMode )
            {
                if( self.Text == null || self.Text.Length == 0 )
                    btnLoadSave.Enabled = false;
                else if( !btnLoadSave.Enabled )
                    btnLoadSave.Enabled = true;
            }
        }

        private void btnDelete_Click( object sender, EventArgs e )
        {
            List<PidList> pidList = null;
            ListBox list = null;
            string config = null;

            if( tabs.SelectedTab == tabGlobal )
            {
                pidList = global;
                list = listGlobal;
                config = "ListPidsGlobal";
            }
            else
            {
                pidList = local;
                list = listLocal;
                config = "ListPidsLocal";
            }
            if( (string)list.SelectedItem == null || ((string)list.SelectedItem).Length == 0 )
            {
                MessageBox.Show( this, "Select a list to remove", "User error", MessageBoxButtons.OK, MessageBoxIcon.Information );
                return;
            }
            for( int l = 0, len = pidList.Count ; l < len ; l++ )
            {
                if( pidList[l].Name.ToLower() == ((string)list.SelectedItem).ToLower() )
                {
                    pidList.RemoveAt( l );
                    Overseer.Config.SetVariable( config, PidListString( pidList ) );
                    Overseer.Config.Save();
                    RefreshLists();
                    list.SelectedIndex = -1;
                    btnDelete.Enabled = false;
                    btnLoadSave.Enabled = false;
                    return;
                }
            }

            if( (string)list.SelectedItem == null || ((string)list.SelectedItem).Length == 0 )
            {
                MessageBox.Show( this, "List not found", "Internal error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return;
            }
        }

        private void btnLoadSave_Click( object sender, EventArgs e )
        {
            List<PidList> pidList = null;
            ListBox list = null;
            TextBox name = null;
            string config = null;

            if( tabs.SelectedTab == tabGlobal )
            {
                pidList = global;
                list = listGlobal;
                name = nameGlobal;
                config = "ListPidsGlobal";
            }
            else
            {
                pidList = local;
                list = listLocal;
                name = nameLocal;
                config = "ListPidsLocal";
            }

            if( saveMode )
            {
                if( name.Text.Length == 0 )
                {
                    MessageBox.Show( this, "Set a name for this list.", "User error", MessageBoxButtons.OK, MessageBoxIcon.Information );
                    return;
                }
                foreach( char invalid in new char[] { '|', '^' } )
                {
                    if( name.Text.Contains( invalid ) )
                    {
                        MessageBox.Show( this, "Invalid character in list name: " + invalid, "User error", MessageBoxButtons.OK, MessageBoxIcon.Information );
                        return;
                    }
                }
                int tmp;
                if( int.TryParse( name.Text, out tmp ) )
                {
                    MessageBox.Show( this, "List name can't be a number.", "User error", MessageBoxButtons.OK, MessageBoxIcon.Information );
                    return;
                }

                bool overwrite = false;
                foreach( PidList plist in pidList )
                {
                    if( plist.Name.ToLower() == name.Text.ToLower() )
                    {
                        DialogResult = MessageBox.Show( this, "List with name <" + name.Text + "> already exists, overwrite?", null, MessageBoxButtons.YesNo, MessageBoxIcon.Question );
                        if( DialogResult == DialogResult.No )
                            return;
                        else
                        {
                            plist.Data = saveData;
                            overwrite = true;
                        }
                        break;
                    }
                }

                if( !overwrite )
                {
                    PidList newList = new PidList( name.Text );
                    newList.Data = saveData;
                    pidList.Add( newList );
                }

                Overseer.Config.SetVariable( config, PidListString( pidList ) );
                Overseer.Config.Save();
                Close();
            }
            else
            {
                foreach( PidList plist in pidList )
                {
                    if( plist.Name.ToLower() == ((string)list.SelectedItem).ToLower() )
                    {
                        loadData = plist.Data;
                        Close();
                        return;
                    }
                }
            }
        }

        private void btnCancel_Click( object sender, EventArgs e )
        {
            Close();
        }
    }
}
