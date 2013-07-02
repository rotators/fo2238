namespace Overseer
{
    partial class frmPidFilters
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
            this.checkDialogs = new System.Windows.Forms.CheckBox();
            this.checkMaps = new System.Windows.Forms.CheckBox();
            this.checkBags = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.checkCrafting = new System.Windows.Forms.CheckBox();
            this.checkEncounters = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // checkDialogs
            // 
            this.checkDialogs.AutoSize = true;
            this.checkDialogs.Checked = true;
            this.checkDialogs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkDialogs.Location = new System.Drawing.Point( 13, 49 );
            this.checkDialogs.Name = "checkDialogs";
            this.checkDialogs.Size = new System.Drawing.Size( 61, 17 );
            this.checkDialogs.TabIndex = 3;
            this.checkDialogs.Text = "Dialogs";
            this.checkDialogs.UseVisualStyleBackColor = true;
            // 
            // checkMaps
            // 
            this.checkMaps.AutoSize = true;
            this.checkMaps.Checked = true;
            this.checkMaps.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkMaps.Location = new System.Drawing.Point( 13, 95 );
            this.checkMaps.Name = "checkMaps";
            this.checkMaps.Size = new System.Drawing.Size( 52, 17 );
            this.checkMaps.TabIndex = 4;
            this.checkMaps.Text = "Maps";
            this.checkMaps.UseVisualStyleBackColor = true;
            // 
            // checkBags
            // 
            this.checkBags.AutoSize = true;
            this.checkBags.Enabled = false;
            this.checkBags.Location = new System.Drawing.Point( 13, 3 );
            this.checkBags.Name = "checkBags";
            this.checkBags.Size = new System.Drawing.Size( 50, 17 );
            this.checkBags.TabIndex = 1;
            this.checkBags.Text = "Bags";
            this.checkBags.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point( 13, 118 );
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size( 70, 22 );
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler( this.btnOK_Click );
            // 
            // checkCrafting
            // 
            this.checkCrafting.AutoSize = true;
            this.checkCrafting.Enabled = false;
            this.checkCrafting.Location = new System.Drawing.Point( 13, 26 );
            this.checkCrafting.Name = "checkCrafting";
            this.checkCrafting.Size = new System.Drawing.Size( 62, 17 );
            this.checkCrafting.TabIndex = 2;
            this.checkCrafting.Text = "Crafting";
            this.checkCrafting.UseVisualStyleBackColor = true;
            // 
            // checkEncounters
            // 
            this.checkEncounters.AutoSize = true;
            this.checkEncounters.Enabled = false;
            this.checkEncounters.Location = new System.Drawing.Point( 13, 72 );
            this.checkEncounters.Name = "checkEncounters";
            this.checkEncounters.Size = new System.Drawing.Size( 80, 17 );
            this.checkEncounters.TabIndex = 5;
            this.checkEncounters.Text = "Encounters";
            this.checkEncounters.UseVisualStyleBackColor = true;
            // 
            // frmPidFilters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size( 94, 150 );
            this.ControlBox = false;
            this.Controls.Add( this.checkEncounters );
            this.Controls.Add( this.checkCrafting );
            this.Controls.Add( this.btnOK );
            this.Controls.Add( this.checkBags );
            this.Controls.Add( this.checkMaps );
            this.Controls.Add( this.checkDialogs );
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPidFilters";
            this.Padding = new System.Windows.Forms.Padding( 0, 0, 0, 2 );
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Filters";
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        internal System.Windows.Forms.CheckBox checkDialogs;
        internal System.Windows.Forms.CheckBox checkMaps;
        internal System.Windows.Forms.CheckBox checkBags;
        internal System.Windows.Forms.CheckBox checkCrafting;
        internal System.Windows.Forms.CheckBox checkEncounters;
    }
}