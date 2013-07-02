namespace Overseer
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && (components != null) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.status = new System.Windows.Forms.StatusStrip();
            this.statusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.progress = new System.Windows.Forms.ToolStripProgressBar();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabSearch = new System.Windows.Forms.TabPage();
            this.groupResults = new System.Windows.Forms.GroupBox();
            this.btnClipboard = new System.Windows.Forms.Button();
            this.splitResults = new System.Windows.Forms.SplitContainer();
            this.resultsTree = new System.Windows.Forms.TreeView();
            this.resultsLog = new System.Windows.Forms.TextBox();
            this.panelPid = new System.Windows.Forms.Panel();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnFilters = new System.Windows.Forms.Button();
            this.lblProtoID = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.pidList = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.pidTypeList = new System.Windows.Forms.ComboBox();
            this.seekPid = new System.ComponentModel.BackgroundWorker();
            this.status.SuspendLayout();
            this.tabs.SuspendLayout();
            this.tabSearch.SuspendLayout();
            this.groupResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitResults)).BeginInit();
            this.splitResults.Panel1.SuspendLayout();
            this.splitResults.Panel2.SuspendLayout();
            this.splitResults.SuspendLayout();
            this.panelPid.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // status
            // 
            this.status.GripMargin = new System.Windows.Forms.Padding( 2, 1, 2, 2 );
            this.status.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.statusText,
            this.progress} );
            this.status.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.status.Location = new System.Drawing.Point( 0, 418 );
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size( 725, 22 );
            this.status.TabIndex = 0;
            this.status.Text = "status";
            // 
            // statusText
            // 
            this.statusText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusText.Name = "statusText";
            this.statusText.Size = new System.Drawing.Size( 0, 17 );
            // 
            // progress
            // 
            this.progress.Maximum = 0;
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size( 150, 16 );
            this.progress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progress.Visible = false;
            // 
            // tabs
            // 
            this.tabs.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabs.Controls.Add( this.tabSearch );
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Location = new System.Drawing.Point( 0, 3 );
            this.tabs.Margin = new System.Windows.Forms.Padding( 0, 5, 0, 0 );
            this.tabs.Name = "tabs";
            this.tabs.Padding = new System.Drawing.Point( 0, 0 );
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size( 725, 415 );
            this.tabs.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabs.TabIndex = 2;
            // 
            // tabSearch
            // 
            this.tabSearch.BackColor = System.Drawing.SystemColors.Control;
            this.tabSearch.Controls.Add( this.groupResults );
            this.tabSearch.Controls.Add( this.panelPid );
            this.tabSearch.Location = new System.Drawing.Point( 4, 25 );
            this.tabSearch.Name = "tabSearch";
            this.tabSearch.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabSearch.Size = new System.Drawing.Size( 717, 386 );
            this.tabSearch.TabIndex = 0;
            this.tabSearch.Text = "Search";
            // 
            // groupResults
            // 
            this.groupResults.AutoSize = true;
            this.groupResults.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupResults.Controls.Add( this.btnClipboard );
            this.groupResults.Controls.Add( this.splitResults );
            this.groupResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupResults.Location = new System.Drawing.Point( 240, 3 );
            this.groupResults.Name = "groupResults";
            this.groupResults.Padding = new System.Windows.Forms.Padding( 5, 5, 5, 28 );
            this.groupResults.Size = new System.Drawing.Size( 474, 380 );
            this.groupResults.TabIndex = 7;
            this.groupResults.TabStop = false;
            this.groupResults.Text = "Results";
            // 
            // btnClipboard
            // 
            this.btnClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClipboard.Enabled = false;
            this.btnClipboard.Location = new System.Drawing.Point( 355, 354 );
            this.btnClipboard.Name = "btnClipboard";
            this.btnClipboard.Size = new System.Drawing.Size( 114, 22 );
            this.btnClipboard.TabIndex = 8;
            this.btnClipboard.Text = "Copy to clipboard";
            this.btnClipboard.UseVisualStyleBackColor = true;
            this.btnClipboard.Click += new System.EventHandler( this.btnClipboard_Click );
            // 
            // splitResults
            // 
            this.splitResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitResults.Location = new System.Drawing.Point( 5, 18 );
            this.splitResults.Name = "splitResults";
            this.splitResults.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitResults.Panel1
            // 
            this.splitResults.Panel1.Controls.Add( this.resultsTree );
            // 
            // splitResults.Panel2
            // 
            this.splitResults.Panel2.Controls.Add( this.resultsLog );
            this.splitResults.Size = new System.Drawing.Size( 464, 334 );
            this.splitResults.SplitterDistance = 243;
            this.splitResults.SplitterWidth = 10;
            this.splitResults.TabIndex = 10;
            // 
            // resultsTree
            // 
            this.resultsTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultsTree.Location = new System.Drawing.Point( 0, 0 );
            this.resultsTree.Name = "resultsTree";
            this.resultsTree.PathSeparator = "|";
            this.resultsTree.ShowNodeToolTips = true;
            this.resultsTree.Size = new System.Drawing.Size( 464, 243 );
            this.resultsTree.TabIndex = 8;
            // 
            // resultsLog
            // 
            this.resultsLog.BackColor = System.Drawing.SystemColors.Window;
            this.resultsLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultsLog.Font = new System.Drawing.Font( "Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)) );
            this.resultsLog.HideSelection = false;
            this.resultsLog.Location = new System.Drawing.Point( 0, 0 );
            this.resultsLog.Multiline = true;
            this.resultsLog.Name = "resultsLog";
            this.resultsLog.ReadOnly = true;
            this.resultsLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.resultsLog.Size = new System.Drawing.Size( 464, 81 );
            this.resultsLog.TabIndex = 9;
            this.resultsLog.WordWrap = false;
            this.resultsLog.TextChanged += new System.EventHandler( this.resultsText_TextChanged );
            // 
            // panelPid
            // 
            this.panelPid.Controls.Add( this.btnClear );
            this.panelPid.Controls.Add( this.btnLoad );
            this.panelPid.Controls.Add( this.btnSave );
            this.panelPid.Controls.Add( this.textBox1 );
            this.panelPid.Controls.Add( this.groupBox1 );
            this.panelPid.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelPid.Location = new System.Drawing.Point( 3, 3 );
            this.panelPid.Name = "panelPid";
            this.panelPid.Padding = new System.Windows.Forms.Padding( 0, 0, 0, 4 );
            this.panelPid.Size = new System.Drawing.Size( 237, 380 );
            this.panelPid.TabIndex = 3;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.Location = new System.Drawing.Point( 9, 354 );
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size( 70, 22 );
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "Clear";
            this.btnClear.UseCompatibleTextRendering = true;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler( this.btnClear_Click );
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLoad.Location = new System.Drawing.Point( 85, 354 );
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size( 70, 22 );
            this.btnLoad.TabIndex = 6;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseCompatibleTextRendering = true;
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler( this.btnLoad_Click );
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point( 161, 354 );
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size( 70, 22 );
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseCompatibleTextRendering = true;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler( this.btnSave_Click );
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.textBox1.BackColor = System.Drawing.SystemColors.Window;
            this.textBox1.Location = new System.Drawing.Point( 9, 102 );
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size( 222, 246 );
            this.textBox1.TabIndex = 0;
            this.textBox1.TabStop = false;
            this.textBox1.WordWrap = false;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add( this.btnFilters );
            this.groupBox1.Controls.Add( this.lblProtoID );
            this.groupBox1.Controls.Add( this.btnSearch );
            this.groupBox1.Controls.Add( this.btnAdd );
            this.groupBox1.Controls.Add( this.pidList );
            this.groupBox1.Controls.Add( this.lblType );
            this.groupBox1.Controls.Add( this.pidTypeList );
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point( 0, 0 );
            this.groupBox1.Margin = new System.Windows.Forms.Padding( 3, 3, 3, 0 );
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding( 3, 3, 3, 0 );
            this.groupBox1.Size = new System.Drawing.Size( 237, 99 );
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PID";
            this.groupBox1.UseCompatibleTextRendering = true;
            // 
            // btnFilters
            // 
            this.btnFilters.Location = new System.Drawing.Point( 9, 67 );
            this.btnFilters.Name = "btnFilters";
            this.btnFilters.Size = new System.Drawing.Size( 75, 23 );
            this.btnFilters.TabIndex = 6;
            this.btnFilters.Text = "Filters";
            this.btnFilters.UseVisualStyleBackColor = true;
            this.btnFilters.Click += new System.EventHandler( this.btnFilters_Click );
            // 
            // lblProtoID
            // 
            this.lblProtoID.AutoSize = true;
            this.lblProtoID.Location = new System.Drawing.Point( 6, 43 );
            this.lblProtoID.Name = "lblProtoID";
            this.lblProtoID.Size = new System.Drawing.Size( 46, 13 );
            this.lblProtoID.TabIndex = 5;
            this.lblProtoID.Text = "Proto ID";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point( 161, 67 );
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size( 70, 22 );
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler( this.btnSearch_Click );
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point( 85, 68 );
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size( 70, 22 );
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler( this.btnAdd_Click );
            // 
            // pidList
            // 
            this.pidList.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.pidList.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.pidList.FormattingEnabled = true;
            this.pidList.Location = new System.Drawing.Point( 55, 40 );
            this.pidList.MaxDropDownItems = 50;
            this.pidList.Name = "pidList";
            this.pidList.Size = new System.Drawing.Size( 176, 21 );
            this.pidList.TabIndex = 2;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point( 6, 16 );
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size( 31, 13 );
            this.lblType.TabIndex = 2;
            this.lblType.Text = "Type";
            // 
            // pidTypeList
            // 
            this.pidTypeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pidTypeList.FormattingEnabled = true;
            this.pidTypeList.Items.AddRange( new object[] {
            "Any",
            "Critter",
            "Item",
            "Scenery"} );
            this.pidTypeList.Location = new System.Drawing.Point( 55, 13 );
            this.pidTypeList.Name = "pidTypeList";
            this.pidTypeList.Size = new System.Drawing.Size( 176, 21 );
            this.pidTypeList.TabIndex = 1;
            this.pidTypeList.SelectedIndexChanged += new System.EventHandler( this.pidTypeList_SelectedIndexChanged );
            // 
            // seekPid
            // 
            this.seekPid.WorkerReportsProgress = true;
            this.seekPid.WorkerSupportsCancellation = true;
            this.seekPid.DoWork += new System.ComponentModel.DoWorkEventHandler( this.seekPid_DoWork );
            this.seekPid.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler( this.seeker_ProgressChanged );
            this.seekPid.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler( this.seeker_RunWorkerCompleted );
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 725, 440 );
            this.Controls.Add( this.tabs );
            this.Controls.Add( this.status );
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size( 700, 400 );
            this.Name = "frmMain";
            this.Padding = new System.Windows.Forms.Padding( 0, 3, 0, 0 );
            this.Text = "Overseer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler( this.frmMain_FormClosing );
            this.status.ResumeLayout( false );
            this.status.PerformLayout();
            this.tabs.ResumeLayout( false );
            this.tabSearch.ResumeLayout( false );
            this.tabSearch.PerformLayout();
            this.groupResults.ResumeLayout( false );
            this.splitResults.Panel1.ResumeLayout( false );
            this.splitResults.Panel2.ResumeLayout( false );
            this.splitResults.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitResults)).EndInit();
            this.splitResults.ResumeLayout( false );
            this.panelPid.ResumeLayout( false );
            this.panelPid.PerformLayout();
            this.groupBox1.ResumeLayout( false );
            this.groupBox1.PerformLayout();
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip status;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabSearch;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox pidTypeList;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ToolStripProgressBar progress;
        private System.Windows.Forms.ToolStripStatusLabel statusText;
        private System.Windows.Forms.TextBox resultsLog;
        private System.ComponentModel.BackgroundWorker seekPid;
        private System.Windows.Forms.Panel panelPid;
        private System.Windows.Forms.ComboBox pidList;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblProtoID;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TreeView resultsTree;
        private System.Windows.Forms.GroupBox groupResults;
        private System.Windows.Forms.SplitContainer splitResults;
        private System.Windows.Forms.Button btnFilters;
        private System.Windows.Forms.Button btnClipboard;
    }
}

