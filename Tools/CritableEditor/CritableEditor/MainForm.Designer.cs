namespace CritableEditor
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
            this.critable = new System.Windows.Forms.ListBox();
            this.bodypart = new System.Windows.Forms.ListBox();
            this.load = new System.Windows.Forms.Button();
            this.save = new System.Windows.Forms.Button();
            this.knockout1 = new System.Windows.Forms.CheckBox();
            this.knockdown1 = new System.Windows.Forms.CheckBox();
            this.crarm1 = new System.Windows.Forms.CheckBox();
            this.clarm1 = new System.Windows.Forms.CheckBox();
            this.crleg1 = new System.Windows.Forms.CheckBox();
            this.clleg1 = new System.Windows.Forms.CheckBox();
            this.blinded1 = new System.Windows.Forms.CheckBox();
            this.death1 = new System.Windows.Forms.CheckBox();
            this.fire1 = new System.Windows.Forms.CheckBox();
            this.bypass1 = new System.Windows.Forms.CheckBox();
            this.drop1 = new System.Windows.Forms.CheckBox();
            this.lost1 = new System.Windows.Forms.CheckBox();
            this.random1 = new System.Windows.Forms.CheckBox();
            this.random2 = new System.Windows.Forms.CheckBox();
            this.lost2 = new System.Windows.Forms.CheckBox();
            this.drop2 = new System.Windows.Forms.CheckBox();
            this.bypass2 = new System.Windows.Forms.CheckBox();
            this.fire2 = new System.Windows.Forms.CheckBox();
            this.death2 = new System.Windows.Forms.CheckBox();
            this.blinded2 = new System.Windows.Forms.CheckBox();
            this.clleg2 = new System.Windows.Forms.CheckBox();
            this.crleg2 = new System.Windows.Forms.CheckBox();
            this.clarm2 = new System.Windows.Forms.CheckBox();
            this.crarm2 = new System.Windows.Forms.CheckBox();
            this.knockdown2 = new System.Windows.Forms.CheckBox();
            this.knockout2 = new System.Windows.Forms.CheckBox();
            this.teststat = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.multiplier = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.testmod = new System.Windows.Forms.NumericUpDown();
            this.Modifierlabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.failuretext = new System.Windows.Forms.TextBox();
            this.messagetext = new System.Windows.Forms.TextBox();
            this.critnum = new System.Windows.Forms.ListBox();
            this.messagenum = new System.Windows.Forms.NumericUpDown();
            this.failurenum = new System.Windows.Forms.NumericUpDown();
            this.html = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.multiplier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.testmod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.messagenum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.failurenum)).BeginInit();
            this.SuspendLayout();
            // 
            // critable
            // 
            this.critable.FormattingEnabled = true;
            this.critable.Items.AddRange(new object[] {
            "BT_MEN",
            "BT_WOMEN",
            "BT_CHILDREN",
            "BT_SUPER_MUTANT",
            "BT_GHOUL",
            "BT_BRAHMIN",
            "BT_RADSCORPION",
            "BT_RAT",
            "BT_FLOATER",
            "BT_CENTAUR",
            "BT_ROBOT",
            "BT_DOG",
            "BT_MANTI",
            "BT_DEADCLAW",
            "BT_PLANT",
            "BT_GECKO",
            "BT_ALIEN",
            "BT_GIANT_ANT",
            "BT_BIG_BAD_BOSS",
            "BT_PLAYER"});
            this.critable.Location = new System.Drawing.Point(12, 12);
            this.critable.Name = "critable";
            this.critable.Size = new System.Drawing.Size(121, 290);
            this.critable.TabIndex = 0;
            this.critable.SelectedIndexChanged += new System.EventHandler(this.listBox_SelectedIndexChanged);
            // 
            // bodypart
            // 
            this.bodypart.FormattingEnabled = true;
            this.bodypart.Items.AddRange(new object[] {
            "Head",
            "LArm",
            "RArm",
            "Torso",
            "RLeg",
            "LLeg",
            "Eyes",
            "Groin",
            "Uncalled"});
            this.bodypart.Location = new System.Drawing.Point(136, 12);
            this.bodypart.Name = "bodypart";
            this.bodypart.Size = new System.Drawing.Size(68, 121);
            this.bodypart.TabIndex = 1;
            this.bodypart.SelectedIndexChanged += new System.EventHandler(this.listBox_SelectedIndexChanged);
            // 
            // load
            // 
            this.load.Location = new System.Drawing.Point(136, 227);
            this.load.Name = "load";
            this.load.Size = new System.Drawing.Size(65, 23);
            this.load.TabIndex = 2;
            this.load.Text = "Load";
            this.load.UseVisualStyleBackColor = true;
            this.load.Click += new System.EventHandler(this.load_Click);
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(136, 256);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(65, 25);
            this.save.TabIndex = 3;
            this.save.Text = "Save";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // knockout1
            // 
            this.knockout1.AutoSize = true;
            this.knockout1.Location = new System.Drawing.Point(213, 54);
            this.knockout1.Name = "knockout1";
            this.knockout1.Size = new System.Drawing.Size(72, 17);
            this.knockout1.TabIndex = 4;
            this.knockout1.Tag = "0x00000001 1";
            this.knockout1.Text = "Knockout";
            this.knockout1.UseVisualStyleBackColor = true;
            this.knockout1.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // knockdown1
            // 
            this.knockdown1.AutoSize = true;
            this.knockdown1.Location = new System.Drawing.Point(213, 77);
            this.knockdown1.Name = "knockdown1";
            this.knockdown1.Size = new System.Drawing.Size(83, 17);
            this.knockdown1.TabIndex = 5;
            this.knockdown1.Tag = "0x00000002 1";
            this.knockdown1.Text = "Knockdown";
            this.knockdown1.UseVisualStyleBackColor = true;
            this.knockdown1.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // crarm1
            // 
            this.crarm1.AutoSize = true;
            this.crarm1.Location = new System.Drawing.Point(213, 154);
            this.crarm1.Name = "crarm1";
            this.crarm1.Size = new System.Drawing.Size(93, 17);
            this.crarm1.TabIndex = 6;
            this.crarm1.Tag = "0x00000020 1";
            this.crarm1.Text = "Crippled RArm";
            this.crarm1.UseVisualStyleBackColor = true;
            this.crarm1.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // clarm1
            // 
            this.clarm1.AutoSize = true;
            this.clarm1.Location = new System.Drawing.Point(213, 131);
            this.clarm1.Name = "clarm1";
            this.clarm1.Size = new System.Drawing.Size(91, 17);
            this.clarm1.TabIndex = 7;
            this.clarm1.Tag = "0x00000010 1";
            this.clarm1.Text = "Crippled LArm";
            this.clarm1.UseVisualStyleBackColor = true;
            this.clarm1.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // crleg1
            // 
            this.crleg1.AutoSize = true;
            this.crleg1.Location = new System.Drawing.Point(213, 176);
            this.crleg1.Name = "crleg1";
            this.crleg1.Size = new System.Drawing.Size(93, 17);
            this.crleg1.TabIndex = 8;
            this.crleg1.Tag = "0x00000008 1";
            this.crleg1.Text = "Crippled RLeg";
            this.crleg1.UseVisualStyleBackColor = true;
            this.crleg1.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // clleg1
            // 
            this.clleg1.AutoSize = true;
            this.clleg1.Location = new System.Drawing.Point(213, 199);
            this.clleg1.Name = "clleg1";
            this.clleg1.Size = new System.Drawing.Size(91, 17);
            this.clleg1.TabIndex = 9;
            this.clleg1.Tag = "0x00000004 1";
            this.clleg1.Text = "Crippled LLeg";
            this.clleg1.UseVisualStyleBackColor = true;
            this.clleg1.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // blinded1
            // 
            this.blinded1.AutoSize = true;
            this.blinded1.Location = new System.Drawing.Point(322, 54);
            this.blinded1.Name = "blinded1";
            this.blinded1.Size = new System.Drawing.Size(61, 17);
            this.blinded1.TabIndex = 10;
            this.blinded1.Tag = "0x00000040 1";
            this.blinded1.Text = "Blinded";
            this.blinded1.UseVisualStyleBackColor = true;
            this.blinded1.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // death1
            // 
            this.death1.AutoSize = true;
            this.death1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.death1.Location = new System.Drawing.Point(322, 98);
            this.death1.Name = "death1";
            this.death1.Size = new System.Drawing.Size(55, 17);
            this.death1.TabIndex = 11;
            this.death1.Tag = "0x00000080 1";
            this.death1.Text = "Death";
            this.death1.UseVisualStyleBackColor = true;
            this.death1.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // fire1
            // 
            this.fire1.AutoSize = true;
            this.fire1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.fire1.Location = new System.Drawing.Point(322, 131);
            this.fire1.Name = "fire1";
            this.fire1.Size = new System.Drawing.Size(109, 17);
            this.fire1.TabIndex = 12;
            this.fire1.Tag = "0x00000400 1";
            this.fire1.Text = "On fire (don\'t use)";
            this.fire1.UseVisualStyleBackColor = true;
            this.fire1.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // bypass1
            // 
            this.bypass1.AutoSize = true;
            this.bypass1.Location = new System.Drawing.Point(322, 77);
            this.bypass1.Name = "bypass1";
            this.bypass1.Size = new System.Drawing.Size(89, 17);
            this.bypass1.TabIndex = 13;
            this.bypass1.Tag = "0x00000800 1";
            this.bypass1.Text = "Bypass armor";
            this.bypass1.UseVisualStyleBackColor = true;
            this.bypass1.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // drop1
            // 
            this.drop1.AutoSize = true;
            this.drop1.Location = new System.Drawing.Point(322, 154);
            this.drop1.Name = "drop1";
            this.drop1.Size = new System.Drawing.Size(108, 17);
            this.drop1.TabIndex = 14;
            this.drop1.Tag = "0x00004000 1";
            this.drop1.Text = "Dropped weapon";
            this.drop1.UseVisualStyleBackColor = true;
            this.drop1.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // lost1
            // 
            this.lost1.AutoSize = true;
            this.lost1.Location = new System.Drawing.Point(213, 98);
            this.lost1.Name = "lost1";
            this.lost1.Size = new System.Drawing.Size(90, 17);
            this.lost1.TabIndex = 15;
            this.lost1.Tag = "0x00008000 1";
            this.lost1.Text = "Lost next turn";
            this.lost1.UseVisualStyleBackColor = true;
            this.lost1.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // random1
            // 
            this.random1.AutoSize = true;
            this.random1.Location = new System.Drawing.Point(322, 177);
            this.random1.Name = "random1";
            this.random1.Size = new System.Drawing.Size(66, 17);
            this.random1.TabIndex = 16;
            this.random1.Tag = "0x00200000 1";
            this.random1.Text = "Random";
            this.random1.UseVisualStyleBackColor = true;
            this.random1.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // random2
            // 
            this.random2.AutoSize = true;
            this.random2.Location = new System.Drawing.Point(576, 177);
            this.random2.Name = "random2";
            this.random2.Size = new System.Drawing.Size(66, 17);
            this.random2.TabIndex = 29;
            this.random2.Tag = "0x00200000 4";
            this.random2.Text = "Random";
            this.random2.UseVisualStyleBackColor = true;
            this.random2.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // lost2
            // 
            this.lost2.AutoSize = true;
            this.lost2.Location = new System.Drawing.Point(467, 98);
            this.lost2.Name = "lost2";
            this.lost2.Size = new System.Drawing.Size(90, 17);
            this.lost2.TabIndex = 28;
            this.lost2.Tag = "0x00008000 4";
            this.lost2.Text = "Lost next turn";
            this.lost2.UseVisualStyleBackColor = true;
            this.lost2.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // drop2
            // 
            this.drop2.AutoSize = true;
            this.drop2.Location = new System.Drawing.Point(576, 154);
            this.drop2.Name = "drop2";
            this.drop2.Size = new System.Drawing.Size(108, 17);
            this.drop2.TabIndex = 27;
            this.drop2.Tag = "0x00004000 4";
            this.drop2.Text = "Dropped weapon";
            this.drop2.UseVisualStyleBackColor = true;
            this.drop2.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // bypass2
            // 
            this.bypass2.AutoSize = true;
            this.bypass2.Location = new System.Drawing.Point(576, 76);
            this.bypass2.Name = "bypass2";
            this.bypass2.Size = new System.Drawing.Size(89, 17);
            this.bypass2.TabIndex = 26;
            this.bypass2.Tag = "0x00000800 4";
            this.bypass2.Text = "Bypass armor";
            this.bypass2.UseVisualStyleBackColor = true;
            this.bypass2.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // fire2
            // 
            this.fire2.AutoSize = true;
            this.fire2.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.fire2.Location = new System.Drawing.Point(576, 131);
            this.fire2.Name = "fire2";
            this.fire2.Size = new System.Drawing.Size(109, 17);
            this.fire2.TabIndex = 25;
            this.fire2.Tag = "0x00000400 4";
            this.fire2.Text = "On fire (don\'t use)";
            this.fire2.UseVisualStyleBackColor = true;
            this.fire2.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // death2
            // 
            this.death2.AutoSize = true;
            this.death2.Location = new System.Drawing.Point(576, 99);
            this.death2.Name = "death2";
            this.death2.Size = new System.Drawing.Size(55, 17);
            this.death2.TabIndex = 24;
            this.death2.Tag = "0x00000080 4";
            this.death2.Text = "Death";
            this.death2.UseVisualStyleBackColor = true;
            this.death2.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // blinded2
            // 
            this.blinded2.AutoSize = true;
            this.blinded2.Location = new System.Drawing.Point(576, 54);
            this.blinded2.Name = "blinded2";
            this.blinded2.Size = new System.Drawing.Size(61, 17);
            this.blinded2.TabIndex = 23;
            this.blinded2.Tag = "0x00000040 4";
            this.blinded2.Text = "Blinded";
            this.blinded2.UseVisualStyleBackColor = true;
            this.blinded2.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // clleg2
            // 
            this.clleg2.AutoSize = true;
            this.clleg2.Location = new System.Drawing.Point(467, 199);
            this.clleg2.Name = "clleg2";
            this.clleg2.Size = new System.Drawing.Size(91, 17);
            this.clleg2.TabIndex = 22;
            this.clleg2.Tag = "0x00000004 4";
            this.clleg2.Text = "Crippled LLeg";
            this.clleg2.UseVisualStyleBackColor = true;
            this.clleg2.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // crleg2
            // 
            this.crleg2.AutoSize = true;
            this.crleg2.Location = new System.Drawing.Point(467, 176);
            this.crleg2.Name = "crleg2";
            this.crleg2.Size = new System.Drawing.Size(93, 17);
            this.crleg2.TabIndex = 21;
            this.crleg2.Tag = "0x00000008 4";
            this.crleg2.Text = "Crippled RLeg";
            this.crleg2.UseVisualStyleBackColor = true;
            this.crleg2.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // clarm2
            // 
            this.clarm2.AutoSize = true;
            this.clarm2.Location = new System.Drawing.Point(467, 131);
            this.clarm2.Name = "clarm2";
            this.clarm2.Size = new System.Drawing.Size(91, 17);
            this.clarm2.TabIndex = 20;
            this.clarm2.Tag = "0x00000010 4";
            this.clarm2.Text = "Crippled LArm";
            this.clarm2.UseVisualStyleBackColor = true;
            this.clarm2.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // crarm2
            // 
            this.crarm2.AutoSize = true;
            this.crarm2.Location = new System.Drawing.Point(467, 154);
            this.crarm2.Name = "crarm2";
            this.crarm2.Size = new System.Drawing.Size(93, 17);
            this.crarm2.TabIndex = 19;
            this.crarm2.Tag = "0x00000020 4";
            this.crarm2.Text = "Crippled RArm";
            this.crarm2.UseVisualStyleBackColor = true;
            this.crarm2.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // knockdown2
            // 
            this.knockdown2.AutoSize = true;
            this.knockdown2.Location = new System.Drawing.Point(467, 77);
            this.knockdown2.Name = "knockdown2";
            this.knockdown2.Size = new System.Drawing.Size(83, 17);
            this.knockdown2.TabIndex = 18;
            this.knockdown2.Tag = "0x00000002 4";
            this.knockdown2.Text = "Knockdown";
            this.knockdown2.UseVisualStyleBackColor = true;
            this.knockdown2.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // knockout2
            // 
            this.knockout2.AutoSize = true;
            this.knockout2.Location = new System.Drawing.Point(467, 54);
            this.knockout2.Name = "knockout2";
            this.knockout2.Size = new System.Drawing.Size(72, 17);
            this.knockout2.TabIndex = 17;
            this.knockout2.Tag = "0x00000001 4";
            this.knockout2.Text = "Knockout";
            this.knockout2.UseVisualStyleBackColor = true;
            this.knockout2.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // teststat
            // 
            this.teststat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.teststat.FormattingEnabled = true;
            this.teststat.Location = new System.Drawing.Point(467, 28);
            this.teststat.Name = "teststat";
            this.teststat.Size = new System.Drawing.Size(149, 21);
            this.teststat.TabIndex = 30;
            this.teststat.SelectedIndexChanged += new System.EventHandler(this.teststat_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(464, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Test stat:";
            // 
            // multiplier
            // 
            this.multiplier.Location = new System.Drawing.Point(213, 28);
            this.multiplier.Name = "multiplier";
            this.multiplier.Size = new System.Drawing.Size(47, 20);
            this.multiplier.TabIndex = 32;
            this.multiplier.ValueChanged += new System.EventHandler(this.multiplier_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(210, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "Multiplier:";
            // 
            // testmod
            // 
            this.testmod.Location = new System.Drawing.Point(643, 28);
            this.testmod.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.testmod.Minimum = new decimal(new int[] {
            9,
            0,
            0,
            -2147483648});
            this.testmod.Name = "testmod";
            this.testmod.Size = new System.Drawing.Size(47, 20);
            this.testmod.TabIndex = 34;
            this.testmod.ValueChanged += new System.EventHandler(this.testmod_ValueChanged);
            // 
            // Modifierlabel
            // 
            this.Modifierlabel.AutoSize = true;
            this.Modifierlabel.Location = new System.Drawing.Point(640, 12);
            this.Modifierlabel.Name = "Modifierlabel";
            this.Modifierlabel.Size = new System.Drawing.Size(47, 13);
            this.Modifierlabel.TabIndex = 35;
            this.Modifierlabel.Text = "Modifier:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(210, 237);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 37;
            this.label3.Text = "Message number:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(464, 237);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 13);
            this.label4.TabIndex = 38;
            this.label4.Text = "Failure message number:";
            // 
            // failuretext
            // 
            this.failuretext.Location = new System.Drawing.Point(467, 279);
            this.failuretext.Name = "failuretext";
            this.failuretext.ReadOnly = true;
            this.failuretext.Size = new System.Drawing.Size(220, 20);
            this.failuretext.TabIndex = 41;
            // 
            // messagetext
            // 
            this.messagetext.Location = new System.Drawing.Point(213, 279);
            this.messagetext.Name = "messagetext";
            this.messagetext.ReadOnly = true;
            this.messagetext.Size = new System.Drawing.Size(220, 20);
            this.messagetext.TabIndex = 42;
            // 
            // critnum
            // 
            this.critnum.FormattingEnabled = true;
            this.critnum.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.critnum.Location = new System.Drawing.Point(136, 139);
            this.critnum.Name = "critnum";
            this.critnum.Size = new System.Drawing.Size(68, 82);
            this.critnum.TabIndex = 43;
            this.critnum.SelectedIndexChanged += new System.EventHandler(this.listBox_SelectedIndexChanged);
            // 
            // messagenum
            // 
            this.messagenum.Location = new System.Drawing.Point(213, 253);
            this.messagenum.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.messagenum.Name = "messagenum";
            this.messagenum.Size = new System.Drawing.Size(93, 20);
            this.messagenum.TabIndex = 44;
            this.messagenum.ValueChanged += new System.EventHandler(this.messagenum_ValueChanged);
            // 
            // failurenum
            // 
            this.failurenum.Location = new System.Drawing.Point(467, 253);
            this.failurenum.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.failurenum.Name = "failurenum";
            this.failurenum.Size = new System.Drawing.Size(93, 20);
            this.failurenum.TabIndex = 45;
            this.failurenum.ValueChanged += new System.EventHandler(this.failurenum_ValueChanged);
            // 
            // html
            // 
            this.html.AutoSize = true;
            this.html.Location = new System.Drawing.Point(133, 284);
            this.html.Name = "html";
            this.html.Size = new System.Drawing.Size(73, 13);
            this.html.TabIndex = 46;
            this.html.TabStop = true;
            this.html.Text = "Generate html";
            this.html.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.html_LinkClicked);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 311);
            this.Controls.Add(this.html);
            this.Controls.Add(this.failurenum);
            this.Controls.Add(this.messagenum);
            this.Controls.Add(this.critnum);
            this.Controls.Add(this.messagetext);
            this.Controls.Add(this.failuretext);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Modifierlabel);
            this.Controls.Add(this.testmod);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.multiplier);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.teststat);
            this.Controls.Add(this.random2);
            this.Controls.Add(this.lost2);
            this.Controls.Add(this.drop2);
            this.Controls.Add(this.bypass2);
            this.Controls.Add(this.fire2);
            this.Controls.Add(this.death2);
            this.Controls.Add(this.blinded2);
            this.Controls.Add(this.clleg2);
            this.Controls.Add(this.crleg2);
            this.Controls.Add(this.clarm2);
            this.Controls.Add(this.crarm2);
            this.Controls.Add(this.knockdown2);
            this.Controls.Add(this.knockout2);
            this.Controls.Add(this.random1);
            this.Controls.Add(this.lost1);
            this.Controls.Add(this.drop1);
            this.Controls.Add(this.bypass1);
            this.Controls.Add(this.fire1);
            this.Controls.Add(this.death1);
            this.Controls.Add(this.blinded1);
            this.Controls.Add(this.clleg1);
            this.Controls.Add(this.crleg1);
            this.Controls.Add(this.clarm1);
            this.Controls.Add(this.crarm1);
            this.Controls.Add(this.knockdown1);
            this.Controls.Add(this.knockout1);
            this.Controls.Add(this.save);
            this.Controls.Add(this.load);
            this.Controls.Add(this.bodypart);
            this.Controls.Add(this.critable);
            this.Name = "MainForm";
            this.Text = "Critable Editor";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.multiplier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.testmod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.messagenum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.failurenum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox critable;
        private System.Windows.Forms.ListBox bodypart;
        private System.Windows.Forms.Button load;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.CheckBox knockout1;
        private System.Windows.Forms.CheckBox knockdown1;
        private System.Windows.Forms.CheckBox crarm1;
        private System.Windows.Forms.CheckBox clarm1;
        private System.Windows.Forms.CheckBox crleg1;
        private System.Windows.Forms.CheckBox clleg1;
        private System.Windows.Forms.CheckBox blinded1;
        private System.Windows.Forms.CheckBox death1;
        private System.Windows.Forms.CheckBox fire1;
        private System.Windows.Forms.CheckBox bypass1;
        private System.Windows.Forms.CheckBox drop1;
        private System.Windows.Forms.CheckBox lost1;
        private System.Windows.Forms.CheckBox random1;
        private System.Windows.Forms.CheckBox random2;
        private System.Windows.Forms.CheckBox lost2;
        private System.Windows.Forms.CheckBox drop2;
        private System.Windows.Forms.CheckBox bypass2;
        private System.Windows.Forms.CheckBox fire2;
        private System.Windows.Forms.CheckBox death2;
        private System.Windows.Forms.CheckBox blinded2;
        private System.Windows.Forms.CheckBox clleg2;
        private System.Windows.Forms.CheckBox crleg2;
        private System.Windows.Forms.CheckBox clarm2;
        private System.Windows.Forms.CheckBox crarm2;
        private System.Windows.Forms.CheckBox knockdown2;
        private System.Windows.Forms.CheckBox knockout2;
        private System.Windows.Forms.ComboBox teststat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown multiplier;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown testmod;
        private System.Windows.Forms.Label Modifierlabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox failuretext;
        private System.Windows.Forms.TextBox messagetext;
        private System.Windows.Forms.ListBox critnum;
        private System.Windows.Forms.NumericUpDown messagenum;
        private System.Windows.Forms.NumericUpDown failurenum;
        private System.Windows.Forms.LinkLabel html;
    }
}

