using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using FOnline;
using FOCommon.Dialog;
using FOCommon.Parsers;

namespace Overseer
{
    public partial class frmMain : Form
    {
        private frmPidFilters formPidFilters = new frmPidFilters();

        private bool closing = false;

        private DefineParser defines = new DefineParser();
        private DefineParser clientDefines = new DefineParser();
        private DefineParser mapperDefines = new DefineParser();
        private DefineParser npcPids = new DefineParser();
        private DefineParser itemPids = new DefineParser();

        private int objectCritter = -1;
        private int objectItem = -1;
        private int objectScenery = -1;

        private object[] pidList_Npcs = new object[0];
        private object[] pidList_Items = new object[0];

        public List<Tuple<int, int>> searchPid = new List<Tuple<int, int>>();

        public frmMain()
        {
            InitializeComponent();

            foreach( string directory in new string[] {
                Overseer.Config.PathScriptsFolder,
                Overseer.Config.PathMapsFolder
            } )
            {
                if( !Directory.Exists( directory ) )
                {
                    MessageBox.Show( this, "Directory <" + Path.GetFullPath( directory ) + "> does not exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                    Application.Exit();
                    Close();
                    return;
                }
            }

            foreach( string file in new string[] {
                Overseer.Config.PathScriptsFolder + Overseer.Config.PathMapperDefines,
                Overseer.Config.PathScriptsFolder + Overseer.Config.PathNpcPid,
                Overseer.Config.PathScriptsFolder + Overseer.Config.PathItemPid
            } )
            {
                if( !File.Exists( file ) )
                {
                    MessageBox.Show( this, "File <" + Path.GetFullPath( file ) + "> does not exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                    Application.Exit();
                    Close();
                    return;
                }
            }

            ReadAllDefines();

            List<string> list = null;

            list = npcPids.KeysToList();
            list.Sort();
            pidList_Npcs = new object[list.Count];
            for( int o = 0, olen = list.Count ; o < olen ; o++ )
            {
                pidList_Npcs[o] = (object)list[o];
            }

            list = itemPids.KeysToList();
            list.Sort();
            pidList_Items = new object[list.Count];
            for( int o = 0, olen = list.Count ; o < olen ; o++ )
            {
                pidList_Items[o] = (object)list[o];
            }

            pidTypeList.SelectedIndex = objectItem + 1;
            RefreshSearchList();
        }

        private void ReadAllDefines()
        {
            //defines.ReadAllDefines( Overseer.Config.PathScriptsFolder+Overseer.Config.Defines );
            //clientDefines.ReadAllDefines( Overseer.Config.PathScriptsFolder+Overseer.Config.PathClientDefines );
            mapperDefines.ReadDefines( Overseer.Config.PathScriptsFolder + Overseer.Config.PathMapperDefines );
            npcPids.ReadDefines( Overseer.Config.PathScriptsFolder + Overseer.Config.PathNpcPid );
            itemPids.ReadDefines( Overseer.Config.PathScriptsFolder + Overseer.Config.PathItemPid );

            if( !mapperDefines.Defines.TryGetValue( "MAP_OBJECT_CRITTER", out objectCritter ) ||
                !mapperDefines.Defines.TryGetValue( "MAP_OBJECT_ITEM", out objectItem ) ||
                !mapperDefines.Defines.TryGetValue( "MAP_OBJECT_SCENERY", out objectScenery ) )
            {
                MessageBox.Show( this, "Missing MAP_OBJECT_* defines in " + Overseer.Config.PathMapperDefines + ", cannot continue", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                Application.Exit();
                Close();
                return;
            }
        }

        void RefreshSearchList()
        {
            textBox1.Clear();
            foreach( Tuple<int, int> element in searchPid )
            {
                string type = "Unknown";
                string pidName = null;
                if( element.Item1 < 0 )
                    type = "Any";
                else if( element.Item1 == objectCritter )
                {
                    type = "Critter";
                    pidName = npcPids.Defines.FindKeyByValue( element.Item2 );
                }
                else if( element.Item1 == objectItem )
                {
                    type = "Item";
                    pidName = itemPids.Defines.FindKeyByValue( element.Item2 );
                }
                else if( element.Item1 == objectScenery )
                    type = "Scenery";

                textBox1.AppendText( type + " " + (pidName == null ? "" : pidName + " (") + element.Item2 + (pidName == null ? "" : ")") + "\r\n" );
            }
            if( searchPid.Count > 0 )
            {
                btnClear.Enabled = true;
                btnSearch.Enabled = true;
                btnSave.Enabled = true;
            }
            else
            {
                btnClear.Enabled = false;
                btnSearch.Enabled = false;
                btnSave.Enabled = false;
            }
        }

        #region Events

        private void frmMain_FormClosing( object sender, FormClosingEventArgs e )
        {
            closing = true;

            if( seekPid.IsBusy )
                seekPid.CancelAsync();
        }

        private void pidTypeList_SelectedIndexChanged( object sender, EventArgs e )
        {
            ComboBox self = (ComboBox)sender;
            self.DroppedDown = false;

            pidList.Items.Clear();
            pidList.SelectedIndex = -1;
            pidList.Text = null;
            if( self.SelectedIndex == objectCritter + 1 )
            {
                pidList.Items.AddRange( pidList_Npcs );
            }
            if( self.SelectedIndex == objectItem + 1 )
            {
                pidList.Items.AddRange( pidList_Items );
            }
        }

        private void resultsText_TextChanged( object sender, EventArgs e )
        {
            TextBox self = (TextBox)sender;

            if( (self.Text != null && self.Text.Length > 0) && !btnClipboard.Enabled )
                btnClipboard.Enabled = true;
            else if( (self.Text == null || self.Text.Length == 0) && btnClipboard.Enabled )
                btnClipboard.Enabled = false;
        }

        private void btnFilters_Click( object sender, EventArgs e )
        {
            if( formPidFilters == null || formPidFilters.IsDisposed )
                formPidFilters = new frmPidFilters();

            formPidFilters.ShowDialog( this );
        }

        private void btnAdd_Click( object sender, EventArgs e )
        {
            bool added = false;
            int pid = -1;
            if( pidList.SelectedIndex >= 0 && pidTypeList.SelectedIndex == objectCritter + 1 )
            {
                if( npcPids.Defines.TryGetValue( (string)pidList.Items[pidList.SelectedIndex], out pid ) )
                {
                    searchPid.Add( new Tuple<int, int>( pidTypeList.SelectedIndex - 1, pid ) );
                    added = true;
                }
                else
                    MessageBox.Show( this, "Unknown define, blame technical designer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning );
            }
            else if( pidList.SelectedIndex >= 0 && pidTypeList.SelectedIndex == objectItem + 1 )
            {
                if( itemPids.Defines.TryGetValue( (string)pidList.Items[pidList.SelectedIndex], out pid ) )
                {
                    searchPid.Add( new Tuple<int, int>( pidTypeList.SelectedIndex - 1, pid ) );
                    added = true;
                }
                else
                    MessageBox.Show( this, "Unknown define, blame technical designer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning );
            }
            else
            {
                if( int.TryParse( pidList.Text, out pid ) )
                {
                    searchPid.Add( new Tuple<int, int>( pidTypeList.SelectedIndex - 1, pid ) );
                    added = true;
                }
                else
                    MessageBox.Show( this, "Object PID must be a number or already existing define", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning );
            }

            if( added )
            {
                pidTypeList.SelectedIndex = 0;
                pidList.SelectedText = "";
                pidList.Text = "";

                RefreshSearchList();
            }
        }

        private void btnSearch_Click( object sender, EventArgs e )
        {
            if( !seekPid.IsBusy )
            {
                if( searchPid.Count == 0 )
                {
                    MessageBox.Show( this, "Nothing to search", "Huh?", MessageBoxButtons.OK, MessageBoxIcon.Stop );
                    return;
                }

                resultsLog.Clear();
                resultsTree.Nodes.Clear();
                resultsLog.TextAlign = HorizontalAlignment.Left;

                btnFilters.Enabled = false;
                btnAdd.Enabled = false;
                btnSearch.Text = "Stop";

                btnClear.Enabled = false;
                btnLoad.Enabled = false;
                btnSave.Enabled = false;

                seekPid.RunWorkerAsync( searchPid );
            }
            else
            {
                seekPid.CancelAsync();
            }
        }

        private void btnClear_Click( object sender, EventArgs e )
        {
            searchPid.Clear();
            RefreshSearchList();
        }

        private void btnLoad_Click( object sender, EventArgs e )
        {
            frmPidList formPidList = new frmPidList();
            formPidList.ShowDialog( this );
            if( formPidList.loadData != null && formPidList.loadData.Count > 0 )
            {
                searchPid = formPidList.loadData;
                RefreshSearchList();
            }
        }

        private void btnSave_Click( object sender, EventArgs e )
        {
            frmPidList formPidList = new frmPidList( searchPid, true );
            formPidList.ShowDialog( this );
        }

        private void btnClipboard_Click( object sender, EventArgs e )
        {
            Clipboard.Clear();
            Clipboard.SetText( resultsLog.Text, TextDataFormat.UnicodeText );
        }

        #endregion

        #region Pid Seeker

        private void FindPidsInBags( BackgroundWorker worker, string filename, List<Tuple<int, int>> pids )
        {
            /*
            BagsParser bags = new BagsParser( filename );
            if( bags == null )
                return;

            if( bags.Parse( itemPids ) )
            {

            }
            */
        }

        private void FindPidsInCrafting( BackgroundWorker worker, string filename, List<Tuple<int, int>> pids )
        {
            /*
            if( !filename.HaveExtension( "msg" ) )
                return;

            FOCRAFTParser focraft = new FOCRAFTParser( filename );
            if( focraft == null )
                return;

            if( focraft.Parse() )
            {
            }
            */
        }

        private void FindPidsInDialog( BackgroundWorker worker, string filename, List<Tuple<int, int>> pids )
        {
            if( !filename.HaveExtension( "fodlg" ) )
                return;

            DialogParser fodlg = new DialogParser( filename );
            if( fodlg == null )
                return;

            if( fodlg.Parse() )
            {
                bool header = false;
                string headerText = "? (" + Path.GetFileName( filename ) + ")\r\n\r\n";
                Dialog dialog = fodlg.GetDialog();
                if( dialog == null )
                    return;

                string lNode = "" + dialog.Nodes.Count.ToString().Length;
                foreach( Node node in dialog.Nodes )
                {
                    int answerId = 0;
                    string lAnswer = "" + node.Answers.Count.ToString().Length;
                    foreach( Answer answer in node.Answers )
                    {
                        answerId++;
                        int demandId = 0;
                        int resultId = 0;
                        string lDemand = "" + answer.Demands.Count.ToString().Length;
                        string lResult = "" + answer.Results.Count.ToString().Length;
                        foreach( Demand demand in answer.Demands )
                        {
                            demandId++;
                            if( demand.Item != null )
                            {
                                int pid = -1;
                                if( itemPids.Defines.TryGetValue( demand.Item.PidDefine, out pid ) &&
                                    pids.IsWantedObject( objectItem, pid ) )
                                {
                                    if( !header )
                                    {
                                        if( resultsLog.Text.Length > 0 )
                                            worker.SendResult( "\r\n" );
                                        worker.SendResult( headerText );
                                        header = true;
                                    }
                                    worker.SendResult( string.Format( "\t[Node {0," + lNode + "} Answer {1," + lAnswer + "} Demand {2," + lDemand + "}] {3} {4} {5}\r\n",
                                        node.Id,
                                        answerId,
                                        demandId,
                                        demand.Item.PidDefine,
                                        demand.Operator == "=" ? "==" : demand.Operator,
                                        demand.Value ) );

                                    worker.SendResult( "Dialogs"
                                        + "|" + Path.GetFileName( filename )
                                        + "|Node " + node.Id
                                            + "^" + node.Text["engl"]
                                        + "|Answer " + answerId
                                            + "^" + answer.Text["engl"]
                                        + "|Demand " + demandId + ": "
                                            + demand.Item.PidDefine + " "
                                            + (demand.Operator == "=" ? "==" : demand.Operator) + " "
                                            + demand.Value );
                                }
                            }
                        }
                        foreach( Result result in answer.Results )
                        {
                            resultId++;
                            if( result.Item != null )
                            {
                                int pid = -1;
                                if( itemPids.Defines.TryGetValue( result.Item.PidDefine, out pid ) &&
                                    pids.IsWantedObject( objectItem, pid ) )
                                {
                                    if( !header )
                                    {
                                        if( resultsLog.Text.Length > 0 )
                                            worker.SendResult( "\r\n" );
                                        worker.SendResult( headerText );
                                        header = true;
                                    }

                                    worker.SendResult( string.Format( "\t[Node {0," + lNode + "} Answer {1," + lAnswer + "} Result {2," + lResult + "}] {3} {4} {5}\r\n",
                                        node.Id,
                                        answerId,
                                        resultId,
                                        result.Item.PidDefine,
                                        result.Operator,
                                        result.Value ) );

                                    worker.SendResult( "Dialogs"
                                        + "|" + Path.GetFileName( filename )
                                        + "|Node " + node.Id
                                            + "^" + node.Text["engl"]
                                        + "|Answer " + answerId
                                            + "^" + answer.Text["engl"]
                                        + "|Result " + resultId + ": "
                                            + result.Item.PidDefine + " "
                                            + result.Operator + " "
                                            + result.Value );
                                }
                            }
                        }
                    }
                }
            }
        }

        private void FindPidsInEncounters( BackgroundWorker worker, string filename, List<Tuple<int, int>> pids )
        {
        }

        private void FindPidsInMap( BackgroundWorker worker, string filename, List<Tuple<int, int>> pids )
        {
            if( !filename.HaveExtension( "fomap" ) )
                return;

            FOnlineMap fomap = FOnlineMap.Load( filename, false, false, true );
            if( fomap == null )
                return;

            bool header = false;
            foreach( Dictionary<string, string> mapObject in fomap.Objects )
            {
                //mapObject["ProtoId"] = "1";

                string tmp;

                int objectType = -1;
                if( !mapObject.TryGetValue( "MapObjType", out tmp ) ||
                    !int.TryParse( tmp, out objectType ) )
                    objectType = -1;

                int objectPid;
                if( mapObject.TryGetValue( "ProtoId", out tmp ) &&
                    int.TryParse( tmp, out objectPid ) &&
                    pids.IsWantedObject( objectType, objectPid ) )
                {
                    int hx, hy;
                    if( mapObject.TryGetValue( "MapX", out tmp ) &&
                        int.TryParse( tmp, out hx ) &&
                        mapObject.TryGetValue( "MapY", out tmp ) &&
                        int.TryParse( tmp, out hy ) )
                    {
                        if( !header )
                        {
                            if( resultsLog.Text.Length > 0 )
                                worker.ReportProgress( 0, "\r\n" );
                            worker.SendResult( string.Format( "{0} ({1})\r\n\r\n",
                                "?", Path.GetFileName( filename ) ) );
                            header = true;
                        }
                        string typeString = "";

                        if( objectType == objectCritter )
                            typeString = "Critter";
                        else if( objectType == objectItem )
                            typeString = "Item";
                        else if( objectType == objectScenery )
                            typeString = "Scenery";

                        string objectPidName = null;
                        if( objectType == objectItem || objectType == objectScenery )
                            objectPidName = itemPids.Defines.FindKeyByValue( objectPid );

                        worker.SendResult( string.Format( "\t[Hex {0,3},{1,3}] {2}{3}\r\n",
                            hx, hy,
                            typeString.Length > 0 ? "(" + typeString + ") " : "",
                            objectPidName != null ? objectPidName + " <" + objectPid + ">" : "" + objectPid
                            ) );

                        worker.SendResult( "Maps"
                           + "|" + Path.GetFileName( filename )
                           + "|" + (typeString == "Scenery" ? "Sceneries" : typeString + "s")
                           + "|" + (objectPidName != null ? objectPidName + " <" + objectPid + ">" : "" + objectPid)
                           + "|Hex " + hx + "," + hy

                           );
                    }
                }
            }
        }

        private void seekPid_DoWork( object sender, DoWorkEventArgs e )
        {
            BackgroundWorker self = (BackgroundWorker)sender;
            List<Tuple<int, int>> data = (List<Tuple<int, int>>)e.Argument;

            foreach( string dir in new string[]{
                Overseer.Config.PathDataFolder,
                Overseer.Config.PathDialogsFolder,
                Overseer.Config.PathMapsFolder
            } )
            {
                if( !Directory.Exists( dir ) )
                {
                    MessageBox.Show( "Directory <" + Path.GetFullPath( dir ) + "> does not exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                    return;
                }
            }

            foreach( string file in new string[]{
                Overseer.Config.PathDataFolder + Overseer.Config.PathBags,
                Overseer.Config.PathTextFolder + "engl\\FOCRAFT.MSG"
            } )
            {
                if( !File.Exists( file ) )
                {
                    MessageBox.Show( "File <" + Path.GetFullPath( file ) + "> does not exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                    return;
                }
            }

            bool searchCritters = false, searchItems = false, searchScenery = false;
            foreach( Tuple<int, int> element in data )
            {
                if( element.Item1 < 0 )
                {
                    searchCritters = true;
                    searchItems = true;
                    searchScenery = true;
                    break;
                }
                else if( element.Item1 == objectCritter )
                    searchCritters = true;
                else if( element.Item1 == objectItem )
                    searchItems = true;
                else if( element.Item1 == objectScenery )
                    searchScenery = true;
            }

            int parsed = 0;
            DirectoryInfo directory;
            FileInfo[] files;
            //
            // 
            //
            if( !self.CancellationPending &&
                formPidFilters.checkBags.Checked &&
                searchItems )
            {
                // TODO
                self.SendProgressText( "Searching in bags..." );
                FindPidsInBags( self, Overseer.Config.PathDataFolder + Overseer.Config.PathBags, data );
            }
            //
            if( !self.CancellationPending &&
                formPidFilters.checkCrafting.Checked &&
                searchItems )
            {
                // TODO
                self.SendProgressText( "Searching in crafing..." );
                FindPidsInCrafting( self, Overseer.Config.PathTextFolder + "engl\\FOCRAFT.MSG", data );
            }
            //
            if( !self.CancellationPending &&
                formPidFilters.checkDialogs.Checked &&
                searchItems )
            {
                self.SendProgressText( "Searching in dialogs..." );
                directory = new DirectoryInfo( Overseer.Config.PathDialogsFolder );
                files = directory.GetFiles( "*.fodlg", SearchOption.TopDirectoryOnly );
                self.SendProgressMax( files.Length );
                parsed = 0;
                foreach( FileInfo file in files )
                {
                    if( self.CancellationPending )
                        break;

                    self.SendProgress();
                    FindPidsInDialog( self, directory.FullName + file.Name, data );
                    parsed++;
                }
            }
            //
            if( !self.CancellationPending &&
                formPidFilters.checkEncounters.Checked &&
                (searchCritters || searchItems) )
            {
                // TODO
            }
            //
            if( !self.CancellationPending &&
                formPidFilters.checkMaps.Checked &&
                (searchCritters || searchItems || searchScenery) )
            {
                self.SendProgressText( "Searching in maps..." );
                directory = new DirectoryInfo( Overseer.Config.PathMapsFolder );
                files = directory.GetFiles( "*.fomap", SearchOption.TopDirectoryOnly );
                self.SendProgressMax( files.Length );
                parsed = 0;
                foreach( FileInfo file in files )
                {
                    if( self.CancellationPending )
                        break;

                    self.SendProgress();
                    FindPidsInMap( self, directory.FullName + "\\" + file.Name, data );
                    parsed++;
                }
            }
            //
            if( self.CancellationPending )
                self.SendResult( "\r\nCancelled." );
        }
        #endregion

        #region Generic Seeker

        private void seeker_ProgressChanged( object sender, ProgressChangedEventArgs e )
        {
            if( closing )
                return;

            BackgroundWorker self = (BackgroundWorker)sender;

            int cmd = e.ProgressPercentage;
            if( cmd == (int)Extensions.BackgroundWorker.SendProgressMax )
            {
                progress.Value = 0;
                progress.Maximum = (int)e.UserState;
                progress.Visible = true;
                status.Update();
            }
            else if( cmd == (int)Extensions.BackgroundWorker.SendProgressText )
            {
                statusText.Text = (string)e.UserState;
                statusText.Visible = true;
                status.Update();
            }
            else if( cmd == (int)Extensions.BackgroundWorker.SendResult )
            {
                string text = (string)e.UserState;

                if( text.Contains( '|' ) )
                {
                    string[] array = text.Split( new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries );
                    bool first = true;
                    TreeNode root = null;
                    foreach( string element in array )
                    {
                        if( first )
                        {
                            if( resultsTree.Nodes.ContainsKey( element ) )
                            {
                                root = resultsTree.Nodes[element];
                                root.Tag = (int)root.Tag + 1;
                                root.Text = root.Name + " (" + (int)root.Tag + ")";
                            }
                            else
                            {
                                root = resultsTree.Nodes.Add( element, element );
                                root.Tag = (int)1;
                            }
                            first = false;
                        }
                        else
                        {
                            TreeNode newRoot = null;
                            string[] txt = element.Split( new char[] { '^' }, StringSplitOptions.RemoveEmptyEntries );
                            if( root.Nodes.ContainsKey( txt[0] ) )
                            {
                                newRoot = root.Nodes[txt[0]];
                                newRoot.Tag = (int)newRoot.Tag + 1;
                                newRoot.Text = newRoot.Name + " (" + (int)newRoot.Tag + ")";
                            }
                            else
                            {
                                newRoot = root.Nodes.Add( txt[0], txt[0] );
                                newRoot.Tag = (int)1;
                                for( int t = 1, tlen = txt.Length ; t < tlen ; t++ )
                                {
                                    if( t > 1 )
                                        newRoot.ToolTipText += "\n";
                                    newRoot.ToolTipText += txt[t];
                                }
                            }
                            root = newRoot;
                        }
                    }
                }
                else
                {
                    resultsLog.AppendText( text );
                    resultsLog.ScrollToCaret();
                    resultsLog.Update();
                    resultsLog.Refresh();
                }
            }
            else if( cmd == (int)Extensions.BackgroundWorker.SendProgress )
            {
                progress.Value += (int)e.UserState;
            }
        }

        private void seeker_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e )
        {
            if( closing )
                return;

            progress.Visible = false;
            statusText.Visible = false;
            status.Update();

            btnFilters.Enabled = true;
            btnAdd.Enabled = true;
            btnSearch.Text = "Search";

            btnClear.Enabled = true;
            btnLoad.Enabled = true;
            btnSave.Enabled = true;
        }

        #endregion
    }
}
