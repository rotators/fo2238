namespace PerkEditor
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.perksList = new BrightIdeasSoftware.ObjectListView();
            this.col1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.col2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.col3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.lbReq = new System.Windows.Forms.ListBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numLevel = new System.Windows.Forms.NumericUpDown();
            this.richtextDesc = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCopyDown = new System.Windows.Forms.Button();
            this.btnRemDown = new System.Windows.Forms.Button();
            this.btnAddDown = new System.Windows.Forms.Button();
            this.btnCopyUp = new System.Windows.Forms.Button();
            this.btnRemUp = new System.Windows.Forms.Button();
            this.btnAddUp = new System.Windows.Forms.Button();
            this.btnCopyReq = new System.Windows.Forms.Button();
            this.btnRemReq = new System.Windows.Forms.Button();
            this.btnAddReq = new System.Windows.Forms.Button();
            this.lbDown = new System.Windows.Forms.ListBox();
            this.lbUp = new System.Windows.Forms.ListBox();
            this.cbRevert = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelLevel = new System.Windows.Forms.Label();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.lnkGenerate = new System.Windows.Forms.LinkLabel();
            this.lnkWiki = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.perksList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLevel)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            this.btnLoad.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnLoad.Location = new System.Drawing.Point(12, 375);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 0;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnSave.Location = new System.Drawing.Point(12, 406);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // perksList
            // 
            this.perksList.AllColumns.Add(this.col1);
            this.perksList.AllColumns.Add(this.col2);
            this.perksList.AllColumns.Add(this.col3);
            this.perksList.AllowColumnReorder = true;
            this.perksList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.perksList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col1,
            this.col2,
            this.col3});
            this.perksList.Cursor = System.Windows.Forms.Cursors.Default;
            this.perksList.Font = new System.Drawing.Font("Tahoma", 8F);
            this.perksList.FullRowSelect = true;
            this.perksList.Location = new System.Drawing.Point(12, 12);
            this.perksList.Name = "perksList";
            this.perksList.ShowGroups = false;
            this.perksList.Size = new System.Drawing.Size(274, 357);
            this.perksList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.perksList.TabIndex = 3;
            this.perksList.UseCompatibleStateImageBehavior = false;
            this.perksList.View = System.Windows.Forms.View.Details;
            this.perksList.DoubleClick += new System.EventHandler(this.perksList_DoubleClick_1);
            // 
            // col1
            // 
            this.col1.AspectName = "Id";
            this.col1.HeaderFont = null;
            this.col1.IsEditable = false;
            this.col1.Text = "Id";
            this.col1.Width = 27;
            // 
            // col2
            // 
            this.col2.AspectName = "Name";
            this.col2.HeaderFont = null;
            this.col2.IsEditable = false;
            this.col2.Text = "Name";
            this.col2.Width = 165;
            // 
            // col3
            // 
            this.col3.AspectName = "Type";
            this.col3.HeaderFont = null;
            this.col3.IsEditable = false;
            this.col3.Text = "Type";
            // 
            // lbReq
            // 
            this.lbReq.FormattingEnabled = true;
            this.lbReq.Location = new System.Drawing.Point(3, 16);
            this.lbReq.Name = "lbReq";
            this.lbReq.Size = new System.Drawing.Size(140, 264);
            this.lbReq.TabIndex = 4;
            this.lbReq.DoubleClick += new System.EventHandler(this.lbReq_DoubleClick);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(414, 37);
            this.trackBar1.Maximum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(121, 45);
            this.trackBar1.TabIndex = 5;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // txtName
            // 
            this.txtName.Enabled = false;
            this.txtName.Location = new System.Drawing.Point(292, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(121, 20);
            this.txtName.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(419, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Max level";
            // 
            // numLevel
            // 
            this.numLevel.Location = new System.Drawing.Point(477, 11);
            this.numLevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLevel.Name = "numLevel";
            this.numLevel.Size = new System.Drawing.Size(57, 20);
            this.numLevel.TabIndex = 9;
            this.numLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLevel.ValueChanged += new System.EventHandler(this.numLevel_ValueChanged);
            // 
            // richtextDesc
            // 
            this.richtextDesc.Location = new System.Drawing.Point(541, 11);
            this.richtextDesc.Name = "richtextDesc";
            this.richtextDesc.ReadOnly = true;
            this.richtextDesc.Size = new System.Drawing.Size(188, 53);
            this.richtextDesc.TabIndex = 10;
            this.richtextDesc.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCopyDown);
            this.panel1.Controls.Add(this.btnRemDown);
            this.panel1.Controls.Add(this.btnAddDown);
            this.panel1.Controls.Add(this.btnCopyUp);
            this.panel1.Controls.Add(this.btnRemUp);
            this.panel1.Controls.Add(this.btnAddUp);
            this.panel1.Controls.Add(this.btnCopyReq);
            this.panel1.Controls.Add(this.btnRemReq);
            this.panel1.Controls.Add(this.btnAddReq);
            this.panel1.Controls.Add(this.lbDown);
            this.panel1.Controls.Add(this.lbUp);
            this.panel1.Controls.Add(this.cbRevert);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lbReq);
            this.panel1.Location = new System.Drawing.Point(292, 89);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(441, 340);
            this.panel1.TabIndex = 11;
            // 
            // btnCopyDown
            // 
            this.btnCopyDown.Location = new System.Drawing.Point(297, 315);
            this.btnCopyDown.Name = "btnCopyDown";
            this.btnCopyDown.Size = new System.Drawing.Size(140, 23);
            this.btnCopyDown.TabIndex = 20;
            this.btnCopyDown.Text = "Copy from previous level";
            this.btnCopyDown.UseVisualStyleBackColor = true;
            this.btnCopyDown.Click += new System.EventHandler(this.btnCopyDown_Click);
            // 
            // btnRemDown
            // 
            this.btnRemDown.Location = new System.Drawing.Point(372, 286);
            this.btnRemDown.Name = "btnRemDown";
            this.btnRemDown.Size = new System.Drawing.Size(65, 23);
            this.btnRemDown.TabIndex = 19;
            this.btnRemDown.Text = "Remove";
            this.btnRemDown.UseVisualStyleBackColor = true;
            this.btnRemDown.Click += new System.EventHandler(this.btnRemDown_Click);
            // 
            // btnAddDown
            // 
            this.btnAddDown.Location = new System.Drawing.Point(297, 286);
            this.btnAddDown.Name = "btnAddDown";
            this.btnAddDown.Size = new System.Drawing.Size(65, 23);
            this.btnAddDown.TabIndex = 18;
            this.btnAddDown.Text = "Add";
            this.btnAddDown.UseVisualStyleBackColor = true;
            this.btnAddDown.Click += new System.EventHandler(this.btnAddDown_Click);
            // 
            // btnCopyUp
            // 
            this.btnCopyUp.Location = new System.Drawing.Point(151, 315);
            this.btnCopyUp.Name = "btnCopyUp";
            this.btnCopyUp.Size = new System.Drawing.Size(140, 23);
            this.btnCopyUp.TabIndex = 17;
            this.btnCopyUp.Text = "Copy from previous level";
            this.btnCopyUp.UseVisualStyleBackColor = true;
            this.btnCopyUp.Click += new System.EventHandler(this.btnCopyUp_Click);
            // 
            // btnRemUp
            // 
            this.btnRemUp.Location = new System.Drawing.Point(226, 286);
            this.btnRemUp.Name = "btnRemUp";
            this.btnRemUp.Size = new System.Drawing.Size(65, 23);
            this.btnRemUp.TabIndex = 16;
            this.btnRemUp.Text = "Remove";
            this.btnRemUp.UseVisualStyleBackColor = true;
            this.btnRemUp.Click += new System.EventHandler(this.btnRemUp_Click);
            // 
            // btnAddUp
            // 
            this.btnAddUp.Location = new System.Drawing.Point(151, 286);
            this.btnAddUp.Name = "btnAddUp";
            this.btnAddUp.Size = new System.Drawing.Size(65, 23);
            this.btnAddUp.TabIndex = 15;
            this.btnAddUp.Text = "Add";
            this.btnAddUp.UseVisualStyleBackColor = true;
            this.btnAddUp.Click += new System.EventHandler(this.btnAddUp_Click);
            // 
            // btnCopyReq
            // 
            this.btnCopyReq.Location = new System.Drawing.Point(3, 315);
            this.btnCopyReq.Name = "btnCopyReq";
            this.btnCopyReq.Size = new System.Drawing.Size(140, 23);
            this.btnCopyReq.TabIndex = 14;
            this.btnCopyReq.Text = "Copy from previous level";
            this.btnCopyReq.UseVisualStyleBackColor = true;
            this.btnCopyReq.Click += new System.EventHandler(this.btnCopyReq_Click);
            // 
            // btnRemReq
            // 
            this.btnRemReq.Location = new System.Drawing.Point(78, 286);
            this.btnRemReq.Name = "btnRemReq";
            this.btnRemReq.Size = new System.Drawing.Size(65, 23);
            this.btnRemReq.TabIndex = 13;
            this.btnRemReq.Text = "Remove";
            this.btnRemReq.UseVisualStyleBackColor = true;
            this.btnRemReq.Click += new System.EventHandler(this.btnRemReq_Click);
            // 
            // btnAddReq
            // 
            this.btnAddReq.Location = new System.Drawing.Point(3, 286);
            this.btnAddReq.Name = "btnAddReq";
            this.btnAddReq.Size = new System.Drawing.Size(65, 23);
            this.btnAddReq.TabIndex = 12;
            this.btnAddReq.Text = "Add";
            this.btnAddReq.UseVisualStyleBackColor = true;
            this.btnAddReq.Click += new System.EventHandler(this.btnAddReq_Click);
            // 
            // lbDown
            // 
            this.lbDown.FormattingEnabled = true;
            this.lbDown.Location = new System.Drawing.Point(297, 16);
            this.lbDown.Name = "lbDown";
            this.lbDown.Size = new System.Drawing.Size(140, 264);
            this.lbDown.TabIndex = 11;
            this.lbDown.DoubleClick += new System.EventHandler(this.lbDown_DoubleClick);
            // 
            // lbUp
            // 
            this.lbUp.FormattingEnabled = true;
            this.lbUp.Location = new System.Drawing.Point(151, 16);
            this.lbUp.Name = "lbUp";
            this.lbUp.Size = new System.Drawing.Size(140, 264);
            this.lbUp.TabIndex = 10;
            this.lbUp.DoubleClick += new System.EventHandler(this.lbUp_DoubleClick);
            // 
            // cbRevert
            // 
            this.cbRevert.AutoSize = true;
            this.cbRevert.Location = new System.Drawing.Point(358, -1);
            this.cbRevert.Name = "cbRevert";
            this.cbRevert.Size = new System.Drawing.Size(75, 17);
            this.cbRevert.TabIndex = 9;
            this.cbRevert.Text = "Just revert";
            this.cbRevert.UseVisualStyleBackColor = true;
            this.cbRevert.CheckedChanged += new System.EventHandler(this.cbRevert_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(294, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Perk down";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(148, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Perk up";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Requirements";
            // 
            // labelLevel
            // 
            this.labelLevel.AutoSize = true;
            this.labelLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelLevel.Location = new System.Drawing.Point(541, 71);
            this.labelLevel.Name = "labelLevel";
            this.labelLevel.Size = new System.Drawing.Size(49, 13);
            this.labelLevel.TabIndex = 12;
            this.labelLevel.Text = "Level 1";
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(93, 375);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 13;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(93, 406);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 14;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "Available on level",
            "Support perk",
            "Disabled"});
            this.cmbType.Location = new System.Drawing.Point(295, 43);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(118, 21);
            this.cmbType.TabIndex = 15;
            // 
            // btnApply
            // 
            this.btnApply.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnApply.Location = new System.Drawing.Point(211, 375);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 16;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // lnkGenerate
            // 
            this.lnkGenerate.AutoSize = true;
            this.lnkGenerate.Location = new System.Drawing.Point(208, 411);
            this.lnkGenerate.Name = "lnkGenerate";
            this.lnkGenerate.Size = new System.Drawing.Size(37, 13);
            this.lnkGenerate.TabIndex = 17;
            this.lnkGenerate.TabStop = true;
            this.lnkGenerate.Text = "HTML";
            this.lnkGenerate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkGenerate_LinkClicked);
            // 
            // lnkWiki
            // 
            this.lnkWiki.AutoSize = true;
            this.lnkWiki.Location = new System.Drawing.Point(251, 411);
            this.lnkWiki.Name = "lnkWiki";
            this.lnkWiki.Size = new System.Drawing.Size(28, 13);
            this.lnkWiki.TabIndex = 18;
            this.lnkWiki.TabStop = true;
            this.lnkWiki.Text = "Wiki";
            this.lnkWiki.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkWiki_LinkClicked);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 441);
            this.Controls.Add(this.lnkWiki);
            this.Controls.Add(this.lnkGenerate);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.labelLevel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.richtextDesc);
            this.Controls.Add(this.numLevel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.perksList);
            this.Name = "MainForm";
            this.Text = "Perk Editor";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.perksList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLevel)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnSave;
        private BrightIdeasSoftware.ObjectListView perksList;
        private BrightIdeasSoftware.OLVColumn col1;
        private BrightIdeasSoftware.OLVColumn col2;
        private BrightIdeasSoftware.OLVColumn col3;
        private System.Windows.Forms.ListBox lbReq;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numLevel;
        private System.Windows.Forms.RichTextBox richtextDesc;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox cbRevert;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelLevel;
        private System.Windows.Forms.ListBox lbDown;
        private System.Windows.Forms.ListBox lbUp;
        private System.Windows.Forms.Button btnCopyDown;
        private System.Windows.Forms.Button btnRemDown;
        private System.Windows.Forms.Button btnAddDown;
        private System.Windows.Forms.Button btnCopyUp;
        private System.Windows.Forms.Button btnRemUp;
        private System.Windows.Forms.Button btnAddUp;
        private System.Windows.Forms.Button btnCopyReq;
        private System.Windows.Forms.Button btnRemReq;
        private System.Windows.Forms.Button btnAddReq;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.LinkLabel lnkGenerate;
        private System.Windows.Forms.LinkLabel lnkWiki;
    }
}

