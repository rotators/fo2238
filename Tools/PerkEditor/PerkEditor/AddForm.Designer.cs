namespace PerkEditor
{
    partial class AddForm
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
            this.cbStat = new System.Windows.Forms.ComboBox();
            this.cbSkill = new System.Windows.Forms.ComboBox();
            this.cbPerk = new System.Windows.Forms.ComboBox();
            this.rbStat = new System.Windows.Forms.RadioButton();
            this.rbSkill = new System.Windows.Forms.RadioButton();
            this.rbPerk = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numValue = new System.Windows.Forms.NumericUpDown();
            this.rbAtmost = new System.Windows.Forms.RadioButton();
            this.rbAtleast = new System.Windows.Forms.RadioButton();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numValue)).BeginInit();
            this.SuspendLayout();
            // 
            // cbStat
            // 
            this.cbStat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStat.FormattingEnabled = true;
            this.cbStat.Location = new System.Drawing.Point(56, 18);
            this.cbStat.Name = "cbStat";
            this.cbStat.Size = new System.Drawing.Size(138, 21);
            this.cbStat.TabIndex = 0;
            this.cbStat.SelectedIndexChanged += new System.EventHandler(this.cbStat_SelectedIndexChanged);
            // 
            // cbSkill
            // 
            this.cbSkill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSkill.FormattingEnabled = true;
            this.cbSkill.Location = new System.Drawing.Point(56, 41);
            this.cbSkill.Name = "cbSkill";
            this.cbSkill.Size = new System.Drawing.Size(138, 21);
            this.cbSkill.TabIndex = 1;
            this.cbSkill.SelectedIndexChanged += new System.EventHandler(this.cbSkill_SelectedIndexChanged);
            // 
            // cbPerk
            // 
            this.cbPerk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPerk.FormattingEnabled = true;
            this.cbPerk.Location = new System.Drawing.Point(56, 64);
            this.cbPerk.Name = "cbPerk";
            this.cbPerk.Size = new System.Drawing.Size(138, 21);
            this.cbPerk.TabIndex = 2;
            this.cbPerk.SelectedIndexChanged += new System.EventHandler(this.cbPerk_SelectedIndexChanged);
            // 
            // rbStat
            // 
            this.rbStat.AutoSize = true;
            this.rbStat.Location = new System.Drawing.Point(6, 19);
            this.rbStat.Name = "rbStat";
            this.rbStat.Size = new System.Drawing.Size(44, 17);
            this.rbStat.TabIndex = 3;
            this.rbStat.TabStop = true;
            this.rbStat.Text = "Stat";
            this.rbStat.UseVisualStyleBackColor = true;
            // 
            // rbSkill
            // 
            this.rbSkill.AutoSize = true;
            this.rbSkill.Location = new System.Drawing.Point(6, 42);
            this.rbSkill.Name = "rbSkill";
            this.rbSkill.Size = new System.Drawing.Size(44, 17);
            this.rbSkill.TabIndex = 4;
            this.rbSkill.TabStop = true;
            this.rbSkill.Text = "Skill";
            this.rbSkill.UseVisualStyleBackColor = true;
            // 
            // rbPerk
            // 
            this.rbPerk.AutoSize = true;
            this.rbPerk.Location = new System.Drawing.Point(6, 65);
            this.rbPerk.Name = "rbPerk";
            this.rbPerk.Size = new System.Drawing.Size(47, 17);
            this.rbPerk.TabIndex = 5;
            this.rbPerk.TabStop = true;
            this.rbPerk.Text = "Perk";
            this.rbPerk.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbStat);
            this.groupBox1.Controls.Add(this.cbPerk);
            this.groupBox1.Controls.Add(this.rbPerk);
            this.groupBox1.Controls.Add(this.cbSkill);
            this.groupBox1.Controls.Add(this.rbSkill);
            this.groupBox1.Controls.Add(this.cbStat);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 94);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parameter";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numValue);
            this.groupBox2.Controls.Add(this.rbAtmost);
            this.groupBox2.Controls.Add(this.rbAtleast);
            this.groupBox2.Location = new System.Drawing.Point(12, 112);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 92);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Relation";
            // 
            // numValue
            // 
            this.numValue.Location = new System.Drawing.Point(39, 52);
            this.numValue.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numValue.Minimum = new decimal(new int[] {
            99999,
            0,
            0,
            -2147483648});
            this.numValue.Name = "numValue";
            this.numValue.Size = new System.Drawing.Size(120, 20);
            this.numValue.TabIndex = 2;
            // 
            // rbAtmost
            // 
            this.rbAtmost.AutoSize = true;
            this.rbAtmost.Location = new System.Drawing.Point(99, 20);
            this.rbAtmost.Name = "rbAtmost";
            this.rbAtmost.Size = new System.Drawing.Size(60, 17);
            this.rbAtmost.TabIndex = 1;
            this.rbAtmost.TabStop = true;
            this.rbAtmost.Text = "At most";
            this.rbAtmost.UseVisualStyleBackColor = true;
            // 
            // rbAtleast
            // 
            this.rbAtleast.AutoSize = true;
            this.rbAtleast.Location = new System.Drawing.Point(7, 20);
            this.rbAtleast.Name = "rbAtleast";
            this.rbAtleast.Size = new System.Drawing.Size(60, 17);
            this.rbAtleast.TabIndex = 0;
            this.rbAtleast.TabStop = true;
            this.rbAtleast.Text = "At least";
            this.rbAtleast.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(19, 212);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(131, 212);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // AddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(222, 247);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "AddForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add/Edit";
            this.Load += new System.EventHandler(this.AddForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbStat;
        private System.Windows.Forms.ComboBox cbSkill;
        private System.Windows.Forms.ComboBox cbPerk;
        private System.Windows.Forms.RadioButton rbStat;
        private System.Windows.Forms.RadioButton rbSkill;
        private System.Windows.Forms.RadioButton rbPerk;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numValue;
        private System.Windows.Forms.RadioButton rbAtmost;
        private System.Windows.Forms.RadioButton rbAtleast;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}