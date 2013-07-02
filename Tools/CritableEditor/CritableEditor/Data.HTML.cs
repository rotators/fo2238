using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace CritableEditor
{
    public static partial class Data
    {
        private static string makeEffects(int num)
        {
            if(num == 0) return "-";
            string ret = "";
            if((num & Data.HF_KNOCKOUT) != 0) ret += "Knockout, ";
            if((num & Data.HF_KNOCKDOWN) != 0) ret += "Knockdown, ";
            if((num & Data.HF_CRIPPLED_LEFT_LEG) != 0) ret += "Crippled left leg, ";
            if((num & Data.HF_CRIPPLED_RIGHT_LEG) != 0) ret += "Crippled right leg, ";
            if((num & Data.HF_CRIPPLED_LEFT_ARM) != 0) ret += "Crippled left arm, ";
            if((num & Data.HF_CRIPPLED_RIGHT_ARM) != 0) ret += "Crippled right arm, ";
            if((num & Data.HF_BLINDED) != 0) ret += "Blinded, ";
            if((num & Data.HF_DEATH) != 0) ret += "Death, ";
            if((num & Data.HF_ON_FIRE) != 0) ret += "On fire, ";
            if((num & Data.HF_BYPASS_ARMOR) != 0) ret += "Bypass armor, ";
            if((num & Data.HF_DROPPED_WEAPON) != 0) ret += "Dropped weapon, ";
            if((num & Data.HF_LOST_NEXT_TURN) != 0) ret += "Lost next turn, ";
            if((num & Data.HF_RANDOM) != 0) ret += "Random, ";
            return ret.Substring(0, ret.Length - 2);
        }

        public static void SaveHtml()
        {
            StreamWriter file;
            file = File.CreateText(FileHTML);
            file.WriteLine("<html><head><title>Critical hit tables</title></head>");
            file.WriteLine("<body>");
            file.WriteLine("<a name=\"start\">");
            for(int bt = 0; bt < 20; bt++)
                file.WriteLine("<a href=\"#" + Bodytype[bt] + "\">" + Bodytype[bt] + "</a><br>");
            file.WriteLine("<hr>");
            for(int bt = 0; bt < 20; bt++)
            {
                file.WriteLine("<a name=\"" + Bodytype[bt] + "\">");
                file.WriteLine("<big><big>" + Bodytype[bt] + "</big></big>");
                file.WriteLine("<a href=\"#start\">Up</a>");
                file.WriteLine("<hr>");
                file.WriteLine("<table border=\"1\">");
                file.WriteLine("<tr bgcolor=\"khaki\">");
                file.WriteLine("<th>Damage multiplier</th>");
                file.WriteLine("<th>Effects</th>");
                file.WriteLine("<th>Tested stat</th>");
                file.WriteLine("<th>Test modifier</th>");
                file.WriteLine("<th>Extra effects</th>");
                file.WriteLine("<th>Message</th>");
                file.WriteLine("<th>Failure message</th>");
                file.WriteLine("</tr>");
                for(int bp = 0; bp < 9; bp++)
                {
                    file.WriteLine("<tr><th colspan=\"7\">" + Bodypart[bp] + "</th></tr>");
                    for(int num = 0; num < 6; num++)
                    {
                        int idx = num * 7 + bp * 42 + bt * 42 * 9;
                        file.WriteLine("<tr>");

                        file.WriteLine("<td>" + Table[idx] + "</td>");
                        // flags
                        file.WriteLine("<td>" + makeEffects(Table[idx + 1]) + "</td>");
                        file.WriteLine("<td>" + getStatName(Table[idx + 2]) + "</td>");

                        file.WriteLine("<td>" + (Table[idx + 2] == -1 ? "-" : "" + Table[idx + 3]) + "</td>");

                        // flags2
                        file.WriteLine("<td>" + makeEffects(Table[idx + 4]) + "</td>");
                        file.WriteLine("<td>" + getCombatString(Table[idx + 5]) + "</td>");
                        file.WriteLine("<td>" + getCombatString(Table[idx + 6]) + "</td>");
                        file.WriteLine("</tr>");
                    }
                }
                file.WriteLine("</table>");
                file.WriteLine("<hr>");
            }
            file.WriteLine("</body></html>");
            file.Close();
        }

        public static void OpenHtml()
        {
            Process.Start(FileHTML);
        }
    }
}