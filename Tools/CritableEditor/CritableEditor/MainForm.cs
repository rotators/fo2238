using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CritableEditor
{
    public partial class MainForm : Form
    {
        
        public MainForm()
        {
            InitializeComponent();
        }

        private void UpdateStats()
        {
            int[] table = Data.Table;
            int idx = GetIdx();
            if (idx < 0) return;
            multiplier.Value = table[idx];
            teststat.SelectedIndex = table[idx + 2] <= 0 ? 0 : table[idx + 2];
            testmod.Value = table[idx + 3];
            messagenum.Value=table[idx+5];
            failurenum.Value = table[idx + 6];

            // #define HF_KNOCKOUT                     0x00000001
            // #define HF_KNOCKDOWN                    0x00000002
            // #define HF_CRIPPLED_LEFT_LEG            0x00000004
            // #define HF_CRIPPLED_RIGHT_LEG           0x00000008
            // #define HF_CRIPPLED_LEFT_ARM            0x00000010
            // #define HF_CRIPPLED_RIGHT_ARM           0x00000020
            // #define HF_BLINDED                      0x00000040
            // #define HF_DEATH                        0x00000080
            // #define HF_ON_FIRE                      0x00000400
            // #define HF_BYPASS_ARMOR                 0x00000800
            // #define HF_DROPPED_WEAPON               0x00004000
            // #define HF_LOST_NEXT_TURN               0x00008000
            // #define HF_RANDOM                       0x00200000

            // primary flags
            knockout1.Checked  = (table[idx + 1] & 0x00000001) != 0;
            knockdown1.Checked = (table[idx + 1] & 0x00000002) != 0;
            clleg1.Checked = (table[idx + 1] & 0x00000004) != 0;
            crleg1.Checked = (table[idx + 1] & 0x00000008) != 0;
            clarm1.Checked = (table[idx + 1] & 0x00000010) != 0;
            crarm1.Checked = (table[idx + 1] & 0x00000020) != 0;
            blinded1.Checked = (table[idx + 1] & 0x00000040) != 0;
            death1.Checked = (table[idx + 1] & 0x00000080) != 0;
            fire1.Checked = (table[idx + 1] & 0x00000400) != 0;
            bypass1.Checked = (table[idx + 1] & 0x00000800) != 0;
            drop1.Checked = (table[idx + 1] & 0x00004000) != 0;
            lost1.Checked = (table[idx + 1] & 0x00008000) != 0;
            random1.Checked = (table[idx + 1] & 0x00200000) != 0;

            // extra flags
            knockout2.Checked = (table[idx + 4] & 0x00000001) != 0;
            knockdown2.Checked = (table[idx + 4] & 0x00000002) != 0;
            clleg2.Checked = (table[idx + 4] & 0x00000004) != 0;
            crleg2.Checked = (table[idx + 4] & 0x00000008) != 0;
            clarm2.Checked = (table[idx + 4] & 0x00000010) != 0;
            crarm2.Checked = (table[idx + 4] & 0x00000020) != 0;
            blinded2.Checked = (table[idx + 4] & 0x00000040) != 0;
            death2.Checked = (table[idx + 4] & 0x00000080) != 0;
            fire2.Checked = (table[idx + 4] & 0x00000400) != 0;
            bypass2.Checked = (table[idx + 4] & 0x00000800) != 0;
            drop2.Checked = (table[idx + 4] & 0x00004000) != 0;
            lost2.Checked = (table[idx + 4] & 0x00008000) != 0;
            random2.Checked = (table[idx + 4] & 0x00200000) != 0;
        }

        private int GetIdx()
        {
            return critable.SelectedIndex * 9 * 42 + bodypart.SelectedIndex * 42 + critnum.SelectedIndex * 7;
        }

        private void load_Click(object sender, EventArgs e)
        {
            Data.LoadTable();
            UpdateStats();

        }

        private void save_Click(object sender, EventArgs e)
        {
            Data.SaveTable();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Data.Init();
            Data.LoadTable();
            teststat.DataSource = Data.StatNames;
            bodypart.SelectedIndex = 0;
            critnum.SelectedIndex = 0;
            critable.SelectedIndex = 0;
            UpdateStats();
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Data.Inited) return;
            UpdateStats();
        }

        private void multiplier_ValueChanged(object sender, EventArgs e)
        {
            if(!Data.Inited) return;
            Data.Table[GetIdx()] = (int)multiplier.Value;
        }

        private void teststat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!Data.Inited) return;
            Data.Table[GetIdx() + 2] = teststat.SelectedIndex;
        }

        private void testmod_ValueChanged(object sender, EventArgs e)
        {
            if(!Data.Inited) return;
            Data.Table[GetIdx() + 3] = (int)testmod.Value;
        }

        private void messagenum_ValueChanged(object sender, EventArgs e)
        {
            if(!Data.Inited) return;
            Data.Table[GetIdx() + 5] = (int)messagenum.Value;
            if(Data.HasKey((int)messagenum.Value))
                messagetext.Text = Data.GetKey((int)messagenum.Value);
            else
                messagetext.Text = "Message not found";
        }

        private void failurenum_ValueChanged(object sender, EventArgs e)
        {
            if(!Data.Inited) return;
            Data.Table[GetIdx() + 6] = (int)failurenum.Value;
            if(Data.HasKey((int)failurenum.Value))
                failuretext.Text = Data.GetKey((int)failurenum.Value);
            else
                failuretext.Text = "Message not found";
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            if (!Data.Inited) return;
            string tag = box.Tag as string;
            string[] strs = tag.Split(' ');
            int flag = Convert.ToInt32(strs[0], 16);
            int off = Int32.Parse(strs[1]);
            Data.SetFlag(GetIdx() + off, flag, box.Checked);
        }

        private void html_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Data.SaveHtml();
            Data.OpenHtml();
        }
    }
}