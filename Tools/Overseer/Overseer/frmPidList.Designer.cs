namespace Overseer
{
    partial class frmPidList
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
            this.listGlobal = new System.Windows.Forms.ListBox();
            this.btnLoadSave = new System.Windows.Forms.Button();
            this.nameGlobal = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabGlobal = new System.Windows.Forms.TabPage();
            this.tabLocal = new System.Windows.Forms.TabPage();
            this.listLocal = new System.Windows.Forms.ListBox();
            this.nameLocal = new System.Windows.Forms.TextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.tabs.SuspendLayout();
            this.tabGlobal.SuspendLayout();
            this.tabLocal.SuspendLayout();
            this.SuspendLayout();
            // 
            // listGlobal
            // 
            this.listGlobal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listGlobal.Location = new System.Drawing.Point( 3, 3 );
            this.listGlobal.Name = "listGlobal";
            this.listGlobal.Size = new System.Drawing.Size( 328, 147 );
            this.listGlobal.TabIndex = 0;
            this.listGlobal.SelectedIndexChanged += new System.EventHandler( this.listGlobal_SelectedIndexChanged );
            // 
            // btnLoadSave
            // 
            this.btnLoadSave.Location = new System.Drawing.Point( 205, 218 );
            this.btnLoadSave.Name = "btnLoadSave";
            this.btnLoadSave.Size = new System.Drawing.Size( 70, 22 );
            this.btnLoadSave.TabIndex = 1;
            this.btnLoadSave.Text = "LoadSave";
            this.btnLoadSave.UseVisualStyleBackColor = true;
            this.btnLoadSave.Click += new System.EventHandler( this.btnLoadSave_Click );
            // 
            // nameGlobal
            // 
            this.nameGlobal.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.nameGlobal.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.nameGlobal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.nameGlobal.Location = new System.Drawing.Point( 3, 150 );
            this.nameGlobal.Name = "nameGlobal";
            this.nameGlobal.Size = new System.Drawing.Size( 328, 20 );
            this.nameGlobal.TabIndex = 2;
            this.nameGlobal.TextChanged += new System.EventHandler( this.name_TextChanged );
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point( 285, 218 );
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size( 70, 22 );
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler( this.btnCancel_Click );
            // 
            // tabs
            // 
            this.tabs.Controls.Add( this.tabGlobal );
            this.tabs.Controls.Add( this.tabLocal );
            this.tabs.Location = new System.Drawing.Point( 13, 13 );
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size( 342, 199 );
            this.tabs.TabIndex = 5;
            this.tabs.SelectedIndexChanged += new System.EventHandler( this.tabs_SelectedIndexChanged );
            // 
            // tabGlobal
            // 
            this.tabGlobal.Controls.Add( this.listGlobal );
            this.tabGlobal.Controls.Add( this.nameGlobal );
            this.tabGlobal.Location = new System.Drawing.Point( 4, 22 );
            this.tabGlobal.Name = "tabGlobal";
            this.tabGlobal.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabGlobal.Size = new System.Drawing.Size( 334, 173 );
            this.tabGlobal.TabIndex = 0;
            this.tabGlobal.Text = "Global";
            this.tabGlobal.UseVisualStyleBackColor = true;
            // 
            // tabLocal
            // 
            this.tabLocal.Controls.Add( this.listLocal );
            this.tabLocal.Controls.Add( this.nameLocal );
            this.tabLocal.Location = new System.Drawing.Point( 4, 22 );
            this.tabLocal.Name = "tabLocal";
            this.tabLocal.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabLocal.Size = new System.Drawing.Size( 334, 173 );
            this.tabLocal.TabIndex = 1;
            this.tabLocal.Text = "Local";
            this.tabLocal.UseVisualStyleBackColor = true;
            // 
            // listLocal
            // 
            this.listLocal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listLocal.Location = new System.Drawing.Point( 3, 3 );
            this.listLocal.Name = "listLocal";
            this.listLocal.Size = new System.Drawing.Size( 328, 147 );
            this.listLocal.TabIndex = 4;
            this.listLocal.SelectedIndexChanged += new System.EventHandler( this.listLocal_SelectedIndexChanged );
            // 
            // nameLocal
            // 
            this.nameLocal.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.nameLocal.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.nameLocal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.nameLocal.Location = new System.Drawing.Point( 3, 150 );
            this.nameLocal.Name = "nameLocal";
            this.nameLocal.Size = new System.Drawing.Size( 328, 20 );
            this.nameLocal.TabIndex = 3;
            this.nameLocal.TextChanged += new System.EventHandler( this.name_TextChanged );
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point( 129, 218 );
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size( 70, 22 );
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler( this.btnDelete_Click );
            // 
            // frmPidList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size( 367, 252 );
            this.ControlBox = false;
            this.Controls.Add( this.btnDelete );
            this.Controls.Add( this.tabs );
            this.Controls.Add( this.btnCancel );
            this.Controls.Add( this.btnLoadSave );
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPidList";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pid List";
            this.tabs.ResumeLayout( false );
            this.tabGlobal.ResumeLayout( false );
            this.tabGlobal.PerformLayout();
            this.tabLocal.ResumeLayout( false );
            this.tabLocal.PerformLayout();
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.ListBox listGlobal;
        private System.Windows.Forms.Button btnLoadSave;
        private System.Windows.Forms.TextBox nameGlobal;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabGlobal;
        private System.Windows.Forms.TabPage tabLocal;
        private System.Windows.Forms.ListBox listLocal;
        private System.Windows.Forms.TextBox nameLocal;
        private System.Windows.Forms.Button btnDelete;
    }
}