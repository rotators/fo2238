using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace PerkEditor
{
    public partial class MainForm : Form
    {
        private Perk currentPerk;

        public MainForm()
        {
            InitializeComponent();
        }

        void lockControls()
        {
            numLevel.Enabled = false;
            trackBar1.Enabled = false;
            cmbType.Enabled = false;
            lbDown.Enabled = false;
            lbReq.Enabled = false;
            lbUp.Enabled = false;
            btnAddDown.Enabled = false;
            btnCopyDown.Enabled = false;
            btnRemDown.Enabled = false;
            btnAddReq.Enabled = false;
            btnCopyReq.Enabled = false;
            btnRemReq.Enabled = false;
            btnAddUp.Enabled = false;
            btnCopyUp.Enabled = false;
            btnRemUp.Enabled = false;
            cbRevert.Enabled = false;
        }

        void unlockControls()
        {
            numLevel.Enabled = true;
            trackBar1.Enabled = true;
            cmbType.Enabled = true;
            lbDown.Enabled = true;
            lbReq.Enabled = true;
            lbUp.Enabled = true;
            btnAddDown.Enabled = true;
            btnCopyDown.Enabled = true;
            btnRemDown.Enabled = true;
            btnAddReq.Enabled = true;
            btnCopyReq.Enabled = true;
            btnRemReq.Enabled = true;
            btnAddUp.Enabled = true;
            btnCopyUp.Enabled = true;
            btnRemUp.Enabled = true;
            cbRevert.Enabled = true;
        }

        void clearControls()
        {
            numLevel.Value = 1;
            trackBar1.Minimum = 1;
            trackBar1.Maximum = 1;
            txtName.Text = "";
            richtextDesc.Text = "";
            cmbType.SelectedIndex = PerkData.Enabled;
            lbDown.DataSource = null;
            lbReq.DataSource = null;
            lbUp.DataSource = null;
        }

        LevelData getCurrentLevel()
        {
            return currentPerk.Levels[trackBar1.Value - 1];
        }

        void justRevertUpdate()
        {
            lbDown.Enabled = !cbRevert.Checked;
            btnAddDown.Enabled = !cbRevert.Checked;
            btnRemDown.Enabled = !cbRevert.Checked;
            btnCopyDown.Enabled = !cbRevert.Checked;
        }

        void refreshReq()
        {
            LevelData level = getCurrentLevel();
            lbReq.DataSource = null;
            lbReq.DataSource = level.Requirements;
        }

        void refreshUp()
        {
            LevelData level = getCurrentLevel();
            lbUp.DataSource = null;
            lbUp.DataSource = level.UpEffects;
        }

        void refreshDown()
        {
            LevelData level = getCurrentLevel();
            lbDown.DataSource = null;
            lbDown.DataSource = level.DownEffects;
        }

        private void saveCurrentPerk()
        {
            if (currentPerk == null) return;
            currentPerk.MaxLevel = Convert.ToInt32(numLevel.Value);
            currentPerk.Type = (Perk.PerkType)cmbType.SelectedIndex;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            setPerkLevelData();
        }

        private void cbRevert_CheckedChanged(object sender, EventArgs e)
        {
            getCurrentLevel().JustRevert = cbRevert.Checked;
            justRevertUpdate();
        }

        private void numLevel_ValueChanged(object sender, EventArgs e)
        {
            trackBar1.Maximum = Convert.ToInt32(numLevel.Value);
            trackBar1.Minimum = 1;
            if(currentPerk!=null) currentPerk.EnsureLevel(trackBar1.Maximum-1);
        }

        private void btnAddReq_Click(object sender, EventArgs e)
        {
            Requirement req = new Requirement(0, true, 0);
            AddForm window = new AddForm(true, req, null);
            window.ShowDialog();
            if (window.Ok)
            {
                LevelData level = getCurrentLevel();
                level.Requirements.Add(req);
                refreshReq();
            }
        }

        private void btnRemReq_Click(object sender, EventArgs e)
        {
            if (lbReq.SelectedIndex < 0) return;
            LevelData level = getCurrentLevel();
            level.Requirements.RemoveAt(lbReq.SelectedIndex);
            refreshReq();
        }

        private void btnCopyReq_Click(object sender, EventArgs e)
        {
            if (trackBar1.Value == 1) return;
            currentPerk.Levels[trackBar1.Value - 1].Requirements =
                new List<Requirement>(currentPerk.Levels[trackBar1.Value - 2].Requirements.Count);
            currentPerk.Levels[trackBar1.Value - 2].Requirements.ForEach((item) =>
            {
                currentPerk.Levels[trackBar1.Value - 1].Requirements.Add((Requirement)item.Clone());
            });
            refreshReq();
        }

        private void btnAddUp_Click(object sender, EventArgs e)
        {
            Effect eff = new Effect(0, true, 0);
            AddForm window = new AddForm(false, null, eff);
            window.ShowDialog();
            if (window.Ok)
            {
                LevelData level = getCurrentLevel();
                level.UpEffects.Add(eff);
                refreshUp();
            }
        }

        private void btnRemUp_Click(object sender, EventArgs e)
        {
            if (lbUp.SelectedIndex < 0) return;
            LevelData level = getCurrentLevel();
            level.UpEffects.RemoveAt(lbUp.SelectedIndex);
            refreshUp();
        }

        private void btnCopyUp_Click(object sender, EventArgs e)
        {
            if (trackBar1.Value == 1) return;
            currentPerk.Levels[trackBar1.Value - 1].UpEffects =
                new List<Effect>(currentPerk.Levels[trackBar1.Value - 2].UpEffects.Count);
            currentPerk.Levels[trackBar1.Value - 2].UpEffects.ForEach((item) =>
            {
                currentPerk.Levels[trackBar1.Value - 1].UpEffects.Add((Effect)item.Clone());
            });
            refreshUp();
        }

        private void btnAddDown_Click(object sender, EventArgs e)
        {
            Effect eff = new Effect(0, true, 0);
            AddForm window = new AddForm(false, null, eff);
            window.ShowDialog();
            if (window.Ok)
            {
                LevelData level = getCurrentLevel();
                level.DownEffects.Add(eff);
                refreshDown();
            }
        }

        private void btnRemDown_Click(object sender, EventArgs e)
        {
            if (lbDown.SelectedIndex < 0) return;
            LevelData level = getCurrentLevel();
            level.DownEffects.RemoveAt(lbDown.SelectedIndex);
            refreshDown();
        }

        private void btnCopyDown_Click(object sender, EventArgs e)
        {
            if (trackBar1.Value == 1) return;
            currentPerk.Levels[trackBar1.Value - 1].DownEffects =
                new List<Effect>(currentPerk.Levels[trackBar1.Value - 2].DownEffects.Count);
            currentPerk.Levels[trackBar1.Value - 2].DownEffects.ForEach((item) =>
            {
                currentPerk.Levels[trackBar1.Value - 1].DownEffects.Add((Effect)item.Clone());
            });
            refreshDown();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "XML files (*.xml)|*.xml";
            if (openDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!Data.LoadPerks(openDialog.FileName))
                {
                    MessageBox.Show("Couldn't load the perks list.");
                    return;
                }
            }
            perksList.Objects = Data.Perks;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Data.Perks == null) return;
            saveCurrentPerk();
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "XML files (*.xml)|*.xml";
            if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // save perks
                try
                {
                    Data.SavePerks(saveDialog.FileName);
                }
                catch(Exception)
                {
                    MessageBox.Show("Failed to save perks.");
                    return;
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Data.NewPerks();
            perksList.RebuildColumns();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            saveCurrentPerk();
            BrightIdeasSoftware.OLVListItem what = perksList.GetItem(perksList.SelectedIndex);
            int num = int.Parse(what.Text);
            Data.RemovePerk(num);

            perksList.RebuildColumns();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lockControls();
            clearControls();
        }

        private void loadPerk(Perk perk)
        {
            txtName.Text = perk.Name;
            richtextDesc.Text = perk.Desc;
            numLevel.Value = perk.MaxLevel;
            cmbType.SelectedIndex = (int)perk.Type;
            trackBar1.Maximum = perk.MaxLevel;
            trackBar1.Minimum = 1;
            trackBar1.Value = 1;
            currentPerk = perk;
            unlockControls();
            setPerkLevelData();
        }

        private void setPerkLevelData()
        {
            if (currentPerk == null) return;
            LevelData level = currentPerk.Levels[trackBar1.Value - 1];
            labelLevel.Text = "Level " + trackBar1.Value;
            lbDown.DataSource = null;
            cbRevert.Checked = level.JustRevert;
            if (!level.JustRevert)
            {
                lbDown.DataSource = level.DownEffects;
            }
            lbUp.DataSource = level.UpEffects;
            lbReq.DataSource = level.Requirements;
            justRevertUpdate();
        }

        private void perksList_DoubleClick_1(object sender, EventArgs e)
        {
            saveCurrentPerk();
            BrightIdeasSoftware.OLVListItem what = perksList.GetItem(perksList.SelectedIndex);
            int num = int.Parse(what.Text);
            Perk p = Data.GetPerk(num);
            loadPerk(p);
        }

        private void lbReq_DoubleClick(object sender, EventArgs e)
        {
            if (lbReq.SelectedIndex < 0) return;
            LevelData level = getCurrentLevel();
            AddForm window = new AddForm(true, level.Requirements[lbReq.SelectedIndex], null);
            window.ShowDialog();
            if (window.Ok) refreshReq();
        }

        private void lbUp_DoubleClick(object sender, EventArgs e)
        {
            if (lbUp.SelectedIndex < 0) return;
            LevelData level = getCurrentLevel();
            AddForm window = new AddForm(false, null,level.UpEffects[lbUp.SelectedIndex]);
            window.ShowDialog();
            if (window.Ok) refreshUp();
        }

        private void lbDown_DoubleClick(object sender, EventArgs e)
        {
            if (lbDown.SelectedIndex < 0) return;
            LevelData level = getCurrentLevel();
            AddForm window = new AddForm(false, null, level.DownEffects[lbDown.SelectedIndex]);
            window.ShowDialog();
            if (window.Ok) refreshDown();
        }

        private void lnkGenerate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            saveCurrentPerk();
            Data.WriteHTML();
            Process.Start("perks.html");
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            saveCurrentPerk();
            Data.WritePerks();
        }

        private void lnkWiki_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            saveCurrentPerk();
            Data.WriteWikiPage();
            Process.Start("wikipage.txt");
        }
    }
}
