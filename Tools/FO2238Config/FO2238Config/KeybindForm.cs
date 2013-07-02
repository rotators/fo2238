using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FO2238Config
{
    public partial class KeybindForm : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams result = base.CreateParams;
                if (Environment.OSVersion.Platform == PlatformID.Win32NT
                    && Environment.OSVersion.Version.Major >= 6)
                {
                    result.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                }

                return result;
            }
        }

        static private List<KeyValuePair<String, String>> index;
        private KeyBind Bind;
        private bool ItemsEnabled;
        public KeybindForm(KeyBind bind, List<String> itemnames)
        {
            InitializeComponent();
            Bind = bind;
            ItemsEnabled = false;
            cmbItem.DataSource = itemnames;
            ShowInTaskbar = false;
        }

        private static void Index(String s, String n)
        {
            index.Add(new KeyValuePair<String, String>(s, n));
        }

        public static void LoadIndex()
        {
            index = new List<KeyValuePair<String, String>>();
            Index("AimUncalled", "Disable Aim");
            Index("AimHead", "Aim Head");
            Index("AimEyes", "Aim Eyes");
            Index("AimGroin", "Aim Groin");
            Index("AimTorso", "Aim Torso");
            Index("AimLeftLeg", "Aim Left Leg");
            Index("AimRightLeg", "Aim Right Leg");
            Index("AimLeftArm", "Aim Left Arm");
            Index("AimRightArm", "Aim Right Arm");
            Index("Reload", "Reload Weapon");
            Index("ToggleFog", "Toggle Fog of War");
            Index("UseBind", "Use Items");
        }

        private void EnableItems(bool enable)
        {
            if(ItemsEnabled==enable) return;
            ItemsEnabled = enable;
            groupBox1.Enabled = enable;
        }

        private void KeybindForm_Load(object sender, EventArgs e)
        {
            Bind.SetControls(cbShift, cbAlt, cbCtrl, cmbKey);
            foreach (KeyValuePair<String, String> pair in index)
            {
                cmbType.Items.Add(pair.Value);
                if (pair.Key == Bind.Type) cmbType.SelectedIndex = cmbType.Items.Count - 1;
            }
            if (Bind.Type.Equals("UseBind")) Bind.SetItemsControl(listBox1);
        }

        private void Remove()
        {
            int idx = listBox1.SelectedIndex;
            if (idx < 0) return;
            listBox1.Items.RemoveAt(idx);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Bind.Type = index[cmbType.SelectedIndex].Key;
            if(Bind.Type.Equals("UseBind"))
                Bind.SetItemsFromControl(listBox1);
            Bind.SetFromControls(cbShift, cbAlt, cbCtrl, cmbKey);
            KeyBinds.RefreshLastIndex();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableItems(cmbType.Text.Equals("Use Items"));
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(cmbItem.Text);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            Remove();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            int idx = listBox1.SelectedIndex;
            if (idx <= 0) return;
            object obj = listBox1.Items[idx];
            listBox1.Items[idx] = listBox1.Items[idx - 1];
            listBox1.Items[idx - 1] = obj;
            listBox1.SelectedIndex = idx - 1;
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            int idx = listBox1.SelectedIndex;
            if (idx < 0) return;
            if (idx + 1 == listBox1.Items.Count) return;
            object obj = listBox1.Items[idx];
            listBox1.Items[idx] = listBox1.Items[idx + 1];
            listBox1.Items[idx + 1] = obj;
            listBox1.SelectedIndex = idx + 1;
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            Remove();
        }
    }
}
