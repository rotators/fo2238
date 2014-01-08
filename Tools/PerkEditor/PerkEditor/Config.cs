using System;
using System.Collections.Generic;
using System.Text;
using FOCommon.Parsers;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PerkEditor
{
    static class Config
    {
        public static String ServerPath;
        public static FOCommon.Parsers.MSGParser MsgParser;
        public static int StatBegin;
        public static int StatEnd;
        public static int SkillBegin;
        public static int SkillEnd;
        public static int PerkBegin;
        public static int PerkEnd;
        public static List<String> StatNames;
        public static List<String> SkillNames;
        public static List<String> PerkNames;

        private static void MakeNames()
        {
            StatNames = new List<string>();
            SkillNames = new List<string>();
            PerkNames = new List<string>();

            for (int i = StatBegin; i <= StatEnd; i++)
            {
                String s = MsgParser.GetMSGValue(10*i+100001);
                if (s.Equals("")) s = i.ToString();
                StatNames.Add(s);
            }

            for (int i = SkillBegin; i <= SkillEnd; i++)
            {
                String s = MsgParser.GetMSGValue(10 * i + 100001);
                if (s.Equals("")) s = i.ToString();
                SkillNames.Add(s);
            }

            for (int i = PerkBegin; i <= PerkEnd; i++)
            {
                String s = MsgParser.GetMSGValue(10 * i + 100001);
                if (s.Equals("")) s = i.ToString();
                PerkNames.Add(s);
            }
        }

        public static bool LoadConfig()
        {
            StreamReader file;
            try
            {
                file = File.OpenText("PerkEditor.cfg");
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot open the config file.");
                return false;
            }

            try
            {
                string rec = file.ReadLine();
                Regex regexp = new Regex(@"([^=]+)=([^=]+)");
                Match match = regexp.Match(rec);
                if (!match.Success || !match.Groups[1].Value.Equals("server"))
                {
                    MessageBox.Show("Cannot parse the config file.");
                    return false;
                }
                ServerPath = match.Groups[2].Value;
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot parse the config file.");
                return false;
            }
            MsgParser = new FOCommon.Parsers.MSGParser(ServerPath + "/text/engl/FOGAME.MSG");
            if (!MsgParser.Parse())
            {
                MessageBox.Show("Cannot parse FOGAME.MSG.");
                return false;
            }

            try
            {
                StreamReader reader = new StreamReader(ServerPath + "/scripts/_defines.fos");

                Regex expr = new Regex(@"[\s]*#[\s]*pragma[\s]+crdata[\s]+""[\s]*([^\s]+)[\s]+([^\s]+)[\s]+([^\s]+)[\s]*""");
                while (!reader.EndOfStream)
                {
                    string s = reader.ReadLine();
                    Match match = expr.Match(s);
                    if (!match.Success) continue;
                    //MessageBox.Show(match.Groups[1].Value + "," + match.Groups[2].Value + "," + match.Groups[3].Value);
                    if (match.Groups[1].Value.Equals("Stat"))
                    {
                        StatBegin = Int32.Parse(match.Groups[2].Value);
                        StatEnd = Int32.Parse(match.Groups[3].Value);
                    }
                    else if (match.Groups[1].Value.Equals("Skill"))
                    {
                        SkillBegin = Int32.Parse(match.Groups[2].Value);
                        SkillEnd = Int32.Parse(match.Groups[3].Value);
                    }
                    else if (match.Groups[1].Value.Equals("Perk"))
                    {
                        PerkBegin = Int32.Parse(match.Groups[2].Value);
                        PerkEnd = Int32.Parse(match.Groups[3].Value);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot parse _defines.fos.");
                return false;
            }
            MakeNames();
            return true;
        }
    }
}
