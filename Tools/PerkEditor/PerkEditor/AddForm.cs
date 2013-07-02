using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PerkEditor
{
    public partial class AddForm : Form
    {
        private bool isreq;
        public Requirement Req;
        public Effect Eff;
        public bool Ok;

        public AddForm(bool isreq, Requirement req, Effect eff)
        {
            this.isreq = isreq;
            Req = req;
            Eff = eff;
            InitializeComponent();
            if (!isreq)
            {
                rbAtleast.Text = "Increase by";
                rbAtmost.Text = "Decrease by";
                groupBox2.Text = "Operation";
            }
            ShowInTaskbar = false;
        }
        public AddForm()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (isreq)
            {
                Req.AtLeast = rbAtleast.Checked;
                if (rbStat.Checked) Req.Param = cbStat.SelectedIndex + Config.StatBegin;
                else if (rbSkill.Checked) Req.Param = cbSkill.SelectedIndex + Config.SkillBegin;
                else if (rbPerk.Checked) Req.Param = cbPerk.SelectedIndex + Config.PerkBegin;
                Req.Value = Convert.ToInt32(numValue.Value);
            }
            else
            {
                Eff.Increase = rbAtleast.Checked;
                if (rbStat.Checked) Eff.Param = cbStat.SelectedIndex + Config.StatBegin;
                else if (rbSkill.Checked) Eff.Param = cbSkill.SelectedIndex + Config.SkillBegin;
                else if (rbPerk.Checked) Eff.Param = cbPerk.SelectedIndex + Config.PerkBegin;
                Eff.Value = Convert.ToInt32(numValue.Value);
            }
            this.Ok = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbStat_SelectedIndexChanged(object sender, EventArgs e)
        {
            rbStat.Select();
        }

        private void cbSkill_SelectedIndexChanged(object sender, EventArgs e)
        {
            rbSkill.Select();
        }

        private void cbPerk_SelectedIndexChanged(object sender, EventArgs e)
        {
            rbPerk.Select();
        }

        private void AddForm_Load(object sender, EventArgs e)
        {
            cbStat.DataSource = Config.StatNames;
            cbSkill.DataSource = Config.SkillNames;
            cbPerk.DataSource = Config.PerkNames;
            Ok = false;
            if (isreq)
            {
                if (Config.StatBegin <= Req.Param && Req.Param <= Config.StatEnd)
                {
                    cbStat.SelectedIndex = Req.Param - Config.StatBegin;
                    rbStat.Select();
                }
                else if (Config.SkillBegin <= Req.Param && Req.Param <= Config.SkillEnd)
                {
                    cbSkill.SelectedIndex = Req.Param - Config.SkillBegin;
                    rbSkill.Select();
                }
                else if (Config.PerkBegin <= Req.Param && Req.Param <= Config.PerkEnd)
                {
                    cbPerk.SelectedIndex = Req.Param - Config.PerkBegin;
                    rbPerk.Select();
                }
                if (Req.AtLeast) rbAtleast.Select();
                else rbAtmost.Select();
                numValue.Value = Req.Value;
            }
            else
            {
                if (Config.StatBegin <= Eff.Param && Eff.Param <= Config.StatEnd)
                {
                    cbStat.SelectedIndex = Eff.Param - Config.StatBegin;
                    rbStat.Select();
                }
                else if (Config.SkillBegin <= Eff.Param && Eff.Param <= Config.SkillEnd)
                {
                    cbSkill.SelectedIndex = Eff.Param - Config.SkillBegin;
                    rbSkill.Select();
                }
                else if (Config.PerkBegin <= Eff.Param && Eff.Param <= Config.PerkEnd)
                {
                    cbPerk.SelectedIndex = Eff.Param - Config.PerkBegin;
                    rbPerk.Select();
                }
                if (Eff.Increase) rbAtleast.Select();
                else rbAtmost.Select();
                numValue.Value = Eff.Value;
            }
        }
    }
}
