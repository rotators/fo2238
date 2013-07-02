using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace PerkEditor
{
    public static partial class Data
    {
        public static List<Perk> Perks;

        public static bool LoadPerks(String filename)
        {
            Perks = new List<Perk>();
            Perk cur = null;
            XmlTextReader reader = new XmlTextReader(filename);
            LevelData level = null;
            int levelid = -1;
            int state = -1;
            while(reader.Read())
            {
                switch(reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if(reader.Name.Equals("perks") && state == -1)
                        {
                            state = 0;
                            continue;
                        }
                        if(reader.Name.Equals("level") && state == 1)
                        {
                            level = new LevelData();
                            levelid = Int32.Parse(reader.GetAttribute(0));
                            level.JustRevert = reader.GetAttribute(1).Equals("1");
                            state = 2;
                            continue;
                        }
                        if(reader.Name.Equals("perk") && state == 0)
                        {
                            state = 1;
                            int id = Int32.Parse(reader.GetAttribute(0));
                            cur = new Perk(id, Config.MsgParser.GetMSGValue(id * 10 + 100001), Config.MsgParser.GetMSGValue(id * 10 + 100002));
                            cur.MaxLevel = Int32.Parse(reader.GetAttribute(1));
                            cur.Type = (Perk.PerkType)Int32.Parse(reader.GetAttribute(2));
                            Perks.Add(cur);
                            continue;
                        }
                        if(reader.Name.Equals("up") && state == 2)
                        {
                            int param = Int32.Parse(reader.GetAttribute(0));
                            bool increase = reader.GetAttribute(1).Equals("1");
                            int value = Int32.Parse(reader.GetAttribute(2));
                            Effect eff = new Effect(param, increase, value);
                            level.UpEffects.Add(eff);
                            continue;
                        }
                        if(reader.Name.Equals("down") && state == 2)
                        {
                            int param = Int32.Parse(reader.GetAttribute(0));
                            bool increase = reader.GetAttribute(1).Equals("1");
                            int value = Int32.Parse(reader.GetAttribute(2));
                            Effect eff = new Effect(param, increase, value);
                            level.DownEffects.Add(eff);
                            continue;
                        }
                        if(reader.Name.Equals("req") && state == 2)
                        {
                            int param = Int32.Parse(reader.GetAttribute(0));
                            bool atleast = reader.GetAttribute(1).Equals("1");
                            int value = Int32.Parse(reader.GetAttribute(2));
                            Requirement eff = new Requirement(param, atleast, value);
                            level.Requirements.Add(eff);
                            continue;
                        }
                        break;
                    case XmlNodeType.Text:
                        break;
                    case XmlNodeType.EndElement:
                        if(reader.Name.Equals("perks") && state == 0)
                        {
                            state = -1;
                            continue;
                        }
                        if(reader.Name.Equals("perk") && state == 1)
                        {
                            state = 0;
                            continue;
                        }
                        if(reader.Name.Equals("level") && state == 2)
                        {
                            cur.EnsureLevel(levelid - 1);
                            cur.Levels[levelid - 1] = level;
                            state = 1;
                            continue;
                        }
                        break;
                }
            }
            reader.Close();
            return true;
        }

        public static void SavePerks(string filename)
        {
            Perks.Sort();

            StreamWriter writer = new StreamWriter(filename);
            writer.WriteLine("<perks>");
            foreach(Perk p in Perks)
            {
                writer.WriteLine("\t<perk id=\"" + p.Id + "\" maxlevel=\"" + p.MaxLevel + "\" type=\"" + p.Type + "\">");
                for(int i = 0; i < p.MaxLevel; i++)
                {
                    writer.WriteLine("\t\t<level id=\"" + (i + 1) + "\" justrevert=\"" + (p.Levels[i].JustRevert ? "1" : "0") + "\">");

                    foreach(Requirement req in p.Levels[i].Requirements)
                    {
                        writer.Write("\t\t\t<req id=\"" + req.Param + "\" atleast=\"");
                        writer.Write(req.AtLeast ? "1" : "0");
                        writer.WriteLine("\" value=\"" + req.Value + "\" />");
                    }

                    foreach(Effect eff in p.Levels[i].UpEffects)
                    {
                        writer.Write("\t\t\t<up id=\"" + eff.Param + "\" increase=\"");
                        writer.Write(eff.Increase ? "1" : "0");
                        writer.WriteLine("\" value=\"" + eff.Value + "\" />");
                    }

                    foreach(Effect eff in p.Levels[i].DownEffects)
                    {
                        writer.Write("\t\t\t<down id=\"" + eff.Param + "\" increase=\"");
                        writer.Write(eff.Increase ? "1" : "0");
                        writer.WriteLine("\" value=\"" + eff.Value + "\" />");
                    }

                    writer.WriteLine("\t\t</level>");
                }
                writer.WriteLine("\t</perk>");
            }
            writer.WriteLine("</perks>");
            writer.Close();
        }

        public static void NewPerks()
        {
            if(Data.Perks == null) return;
            int idx = -1;
            for(int i = Config.PerkBegin; i <= Config.PerkEnd; i++)
            {
                idx = i;
                foreach(Perk p in Perks)
                    if(p.Id == idx)
                    {
                        idx = -1;
                        break;
                    }
                if(idx >= 0) break;
            }
            if(idx < 0)
            {
                MessageBox.Show("No more room for new perks. Remove the existing ones or increase crdata perk space.");
                return;
            }
            Perk perk = new Perk(idx, "", "");
            Perks.Add(perk);
        }

        public static void RemovePerk(int num)
        {
            for(int i = 0; i < Perks.Count; i++)
            {
                if(Perks[i].Id == num)
                {
                    Perks.RemoveAt(i);
                    break;
                }
            }
        }

        public static Perk GetPerk(int num)
        {
            foreach(Perk p in Perks)
            {
                if(p.Id == num)
                    return p;
            }
            return null;
        }
    }
}
