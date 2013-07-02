using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CritableEditor
{
    public static partial class Data
    {
        public static void Init()
        {
            Data.loadIni();
            Data.loadText();
        }

        public static void SaveTable()
        {
            StreamWriter file;
            file = File.CreateText(FileOutput);
            foreach(string line in Header)
                file.WriteLine(line);
            /*file.WriteLine(@"//");
            file.WriteLine(@"// FOnline: 2238");
            file.WriteLine(@"// Rotators");
            file.WriteLine(@"//");
            file.WriteLine(@"// critical_table.fos");
            file.WriteLine(@"//");
            file.WriteLine(@"");
            file.WriteLine(@"// DO NOT CHANGE THIS FILE IN ANY OTHER WAY THAN USING CritableEditor TOOL!");*/
            file.WriteLine(@"");
            file.WriteLine(@"// Fallout2.exe FEF78 - 106597");
            file.WriteLine(@"const int[] CriticalTable = { // 30240");
            file.WriteLine(@"               // Roll 0 - 20                                                21 - 45                                                       46 - 70                                                       71 - 90                                                       91 - 100                                                      100+");
            file.WriteLine(@"		   // DamageMultiplier Effects StatCheck CheckModifier FailureEffect Message FailureMessage");
            file.WriteLine(@"		   // DM  Effects     Stat  Check FailEffects Mess  FailMess");
            for(uint i = 0; i < 20; i++)
            {
                file.WriteLine("// " + Bodytype[i]);
                for(uint j = 0; j < 9; j++)
                    if((i < 19) || (j < 8))
                        file.WriteLine(writeLine(i * 9 + j) + ",");
                    else
                        file.WriteLine(writeLine(i * 9 + j));
            }

            file.WriteLine(@"};");
            file.Close();
        }

        public static void LoadTable()
        {
            StreamReader file;
            string s;
            file = File.OpenText(FileInput);
            Header = new List<string>();
            while(!file.EndOfStream)
            {
                s = file.ReadLine();
                Header.Add(s);
                if(s.Equals(MarkerLine))
                    break;
            }

            for(int i = 0; i < 6; i++)
                file.ReadLine();

            if(file.EndOfStream)
                throw new Exception("Found no marker line in the critical table.");

            int c = 0;
            for(uint i = 0; i < 20; i++)
            {
                s = file.ReadLine();
                for(uint j = 0; j < 9; j++)
                {
                    s = file.ReadLine();
                    s = s.Substring(15, s.Length - 15);
                    string[] line = s.Split(',');
                    string longone = "";
                    foreach(string single in line)
                        longone += single + "|";

                    foreach(string single in line)
                    {
                        if(single == "") continue;
                        if(((c % 7) != 1) && ((c % 7) != 4))
                            Table[c++] = int.Parse(single);
                        else
                        {
                            string single2 = single.Substring(3, single.Length - 3);
                            Table[c++] = Convert.ToInt32(single2, 16);
                        }
                        if((c % 7) == 3) Table[c - 1]++;
                    }
                }
            }

            file.Close();
            Inited = true;
        }

        public static bool HasKey(int key)
        {
            return Dict.ContainsKey(key);
        }

        public static String GetKey(int key)
        {
            return Dict[key];
        }

        public static void SetFlag(int index, int flag, bool set)
        {
            if(set)
                Data.Table[index] |= flag;
            else
                Data.Table[index] &= ~flag;
        }

        private static string getCombatString(int num)
        {
            if(!Dict.ContainsKey(num))
                return "?";
            return Dict[num];
        }

        private static string getStatName(int num)
        {
            if(num == 0) return "-";
            return StatNames[num].ToString();
        }

        private static void loadIni()
        {
            StreamReader file;
            file = File.OpenText(".\\CritableEditor.ini");
            FileInput = file.ReadLine();
            FileOutput = file.ReadLine();
            FOCombat = file.ReadLine();
            FileHTML = file.ReadLine();
            file.Close();
        }

        public static void loadText()
        {
            Dict = new Dictionary<int, string>();
            StreamReader file;
            file = File.OpenText(FOCombat);
            string s;
            s = file.ReadLine();

            while(!file.EndOfStream)
            {
                if((s.Length > 0) && (s[0] == '{'))
                {
                    int i = 0;
                    while(s[i] != '}') i++;
                    int number = int.Parse(s.Substring(1, i - 1));
                    string rest = s.Substring(i + 4, s.Length - 5 - i);
                    if(!Dict.ContainsKey(number))
                        Dict.Add(number, rest);
                }
                s = file.ReadLine();
            }
            file.Close();
        }

        private static string writeLine(uint n)
        {
            string ret;
            if(n < 9)
                ret = Initstring[n];
            else
                ret = "               ";

            ret += Table[42 * n]; // multiplier
            ret += ", ";
            ret += valToHex(Table[42 * n + 1]); // flags
            ret += ",   ";
            if(Table[42 * n + 2] <= 0) // stat
                ret += "-1";
            else
                if(Table[42 * n + 2] <= 10)
                    ret += " " + ((int)Table[42 * n + 2] - 1);
                else
                    ret += +((int)Table[42 * n + 2] - 1);
            ret += ",   ";
            if(Table[42 * n + 3] < 0)
                ret += Table[42 * n + 3]; // bonus
            else
            {
                ret += " ";
                ret += Table[42 * n + 3]; // bonus
            }
            ret += ", ";
            ret += valToHex(Table[42 * n + 4]); // flags
            ret += ", ";
            ret += Table[42 * n + 5]; // message
            ret += ", ";
            ret += Table[42 * n + 6]; // failuremessage
            for(uint i = 1; i <= 5; i++)
            {
                ret += ",            ";
                ret += Table[42 * n + 7 * i]; // multiplier
                ret += ", ";
                ret += valToHex(Table[42 * n + 1 + 7 * i]); // flags
                ret += ",   ";
                if(Table[42 * n + 2 + 7 * i] <= 0) // stat
                    ret += "-1";
                else
                    if(Table[42 * n + 2 + 7 * i] <= 10)
                        ret += " " + ((int)Table[42 * n + 2 + 7 * i] - 1);
                    else
                        ret += +((int)Table[42 * n + 2 + 7 * i] - 1);
                ret += ",   ";
                if(Table[42 * n + 3 + 7 * i] < 0)
                    ret += Table[42 * n + 3 + 7 * i]; // bonus
                else
                {
                    ret += " ";
                    ret += Table[42 * n + 3 + 7 * i]; // bonus
                }
                ret += ", ";
                ret += valToHex(Table[42 * n + 4 + 7 * i]); // flags
                ret += ", ";
                ret += Table[42 * n + 5 + 7 * i]; // message
                ret += ", ";
                ret += Table[42 * n + 6 + 7 * i]; // failuremessage
            }

            return ret;
        }

        private static string valToHex(decimal inval)
        {
            string vv = String.Format("{0:X}", (uint)inval);
            string v2 = "0x";
            for(uint i = 0; i < (8 - vv.Length); i++)
                v2 += "0";
            v2 += vv;
            return v2;
        }
    }
}
