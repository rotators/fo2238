using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace FO2238Config
{
    public class KeyBind // contains data in editor format
    {
        public KeyBind()
        {
            Type = "AimUncalled";
            Key = "1";
            Items = new List<String>();
            Ctrl = true;
            Shift = false;
            Alt = false;
        }
        public void Load(String key,String value)
        {
            String activator = value;
            Type = key;
            if (key.Length>7 && key.Substring(0, 7).Equals("UseBind"))
            {
                Type = "UseBind";
                String[] split = value.Split("|".ToCharArray());
                if (split.Length < 2) return;
                activator = split[0];
                String[] items = split[1].Split(" ".ToCharArray());
                foreach(String s in items)
                {
                    if (!KeyBinds.Assoc.ContainsKey(s)) continue;
                    Items.Add(KeyBinds.Assoc[s]);
                }
            }

            Ctrl = activator.Contains("Ctrl");
            Alt = activator.Contains("Alt");
            Shift = activator.Contains("Shift");
            String[] splitted = activator.Split(" \t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (splitted.Length == 0)
            {
                return;
            }
            Key = splitted[splitted.Length - 1].Replace('_', ' ');
            if (Key.Equals("EQUALS")) Key = "=";
        }

        public String GetSave()
        {
            String s = "";
            if (Ctrl) s += "Ctrl ";
            if (Alt) s += "Alt ";
            if (Shift) s += "Shift ";
            if (Key.Equals("=")) s += "EQUALS";
            else s += Key.Replace(' ', '_');
            if (!Type.Equals("UseBind")) return s;
            s += " |";
            foreach (String ss in Items) s += " "+KeyBinds.ItemKeys[ss];
            return s;
        }

        public String GetName()
        {
            String s = "";
            if (Ctrl) s += "Ctrl + ";
            if (Alt) s += "Alt + ";
            if (Shift) s += "Shift + ";
            s += Key;
            s += ": ";
            if(Type.Equals("AimUncalled")) s+="Disable Aim";
            else if(Type.Equals("AimHead")) s+="Aim Head";
            else if(Type.Equals("AimEyes")) s+="Aim Eyes";
            else if(Type.Equals("AimGroin")) s+="Aim Groin";
            else if(Type.Equals("AimTorso")) s+="Aim Torso";
            else if(Type.Equals("AimLeftLeg")) s+="Aim Left leg";
            else if(Type.Equals("AimRightLeg")) s+="Aim Right Leg";
            else if(Type.Equals("AimLeftArm")) s+="Aim Left Arm";
            else if(Type.Equals("AimRightArm")) s += "Aim Right Arm";
            else if (Type.Equals("Reload")) s += "Reload Weapon";
            else if (Type.Equals("ToggleFog")) s += "Toggle Fog of War";
            else
            {
                s += "Use Items - ";
                bool first = true;
                foreach (String ss in Items)
                {
                    if (!first) s += ", ";
                    first = false;
                    s += ss;
                }
            }
            return s;
        }

        public void SetFromControls(CheckBox shift, CheckBox alt, CheckBox ctrl, ComboBox keybox)
        {
            Ctrl = ctrl.Checked;
            Alt = alt.Checked;
            Shift = shift.Checked;
            Key = (String)keybox.SelectedItem;
        }

        public void SetItemsFromControl(ListBox box)
        {
            Items.Clear();
            for (int i = 0, j = box.Items.Count; i < j; i++) Items.Add(box.Items[i].ToString());
        }

        public void SetControls(CheckBox shift, CheckBox alt, CheckBox ctrl, ComboBox keybox)
        {
            ctrl.Checked = Ctrl;
            alt.Checked = Alt;
            shift.Checked = Shift;
            for (int i = 0, j = keybox.Items.Count; i < j; i++)
                if(keybox.Items[i].ToString().Equals(Key))
                {
                    keybox.SelectedIndex = i;
                    return;
                }
            keybox.SelectedIndex = 0;
        }

        public void SetItemsControl(ListBox box)
        {
            foreach (String s in Items) box.Items.Add(s);
        }

        public String Type;
        public bool Ctrl;
        public bool Alt;
        public bool Shift;
        public String Key;
        List<String> Items;
    }

    public static class KeyBinds
    {
        public static List<String> Items;
        public static Dictionary<String,String> ItemKeys;
        public static Dictionary<String,String> Assoc;
        public static List<KeyBind> Binds;
        private static KeyBind tempBind;
        private static ListBox box;
        private static int index;

        private static string[] ItemAssoc = {
            "Antidote","49",
            "Beer", "124",
            "Booze", "125",
            "Buffout", "87",
            "Cigarettes", "541",
            "Cookie", "378",
            "Fruit", "71",
            "Gamma Gulp Beer", "310",
            "Healing Powder", "273",
            "Jet", "259",
            "Mentats", "53",
            "Nuka-Cola", "106",
            "Psycho", "110",
            "RadAway", "48",
            "Rad-X", "109",
            "Roentgen Rum", "311",
            "Rot Gut", "469",
            "Stimpak", "40",
            "Super Stimpak", "144",
            "Weak Healing Powder", "9655"
        };

        private static void AddKey(String name, String id)
        {
            Items.Add(name);
            ItemKeys.Add(name, id); // "Super Stimpak" -> "144"
            Assoc.Add(id, name); // "144" -> "Super Stimpak"
        }

        public static void Init(ListBox thebox, IniParser parser)
        {
            box = thebox;
            Items = new List<String>();
            ItemKeys = new Dictionary<String,String>();
            Assoc = new Dictionary<String, String>();
            Binds = new List<KeyBind>();
            for (int i = 0; i < ItemAssoc.Length; i += 2) AddKey(ItemAssoc[i], ItemAssoc[i+1]);
            Items.Sort();
            List<KeyValuePair<String,String>> keys=parser.EnumSection("Bindings");
            foreach (KeyValuePair<String, String> pair in keys)
            {
                KeyBind bind=new KeyBind();
                bind.Load(pair.Key, pair.Value);
                Binds.Add(bind);
            }
            foreach (KeyBind bind in Binds)
                box.Items.Add(bind.GetName());
        }

        public static void Save(IniParser parser)
        {
            parser.DeleteSection("Bindings");
            int idx = 0;
            foreach (KeyBind bind in Binds)
            {
                if (bind.Type.Equals("UseBind"))
                {
                    parser.AddSetting("Bindings", "UseBind" + idx,bind.GetSave());
                    idx++;
                }
                else parser.AddSetting("Bindings", bind.Type, bind.GetSave());
            }
        }

        public static void RemovePressed()
        {
            int idx = box.SelectedIndex;
            if (idx < 0) return;
            box.Items.RemoveAt(idx);
            Binds.RemoveAt(idx);
        }
        public static void DoubleClick()
        {
            int idx = box.SelectedIndex;
            if (idx < 0) return;
            KeybindForm edit = new KeybindForm(Binds[idx], Items);
            index = idx;
            edit.ShowDialog();
        }
        public static void AddPressed()
        {
            tempBind=new KeyBind();
            KeybindForm edit = new KeybindForm(tempBind, Items);
            index = -1;
            edit.ShowDialog();
        }
        public static void RefreshLastIndex()
        {
            if (index < 0)
            {
                box.Items.Add(tempBind.GetName());
                Binds.Add(tempBind);
                return;
            }
            box.Items[index] = Binds[index].GetName();
        }
    }
}
