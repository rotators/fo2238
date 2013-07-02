// Author: Ghosthack
// 
// A quick a dirty program to generate a _scripts.fos define file from a scripts.lst file

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DefineGenerator
{
    class CScript
    {
        public int id;
        public string name;
        public string description;
    }

    class Program
    {
        static void ExitGracefully(string message)
        {
            Console.Out.WriteLine(message);
            Console.In.ReadLine();
            Environment.Exit(0);
        }

        static void AddLine(List<string> lines, string line)
        {
            lines.Add(line);
            Console.Out.WriteLine(line);
        }

        static void GenerateScriptFile(List<CScript> scripts)
        {
            List<string> lines = new List<string>();

            AddLine(lines, "#ifndef __SCRIPTS__");
            AddLine(lines, "#define __SCRIPTS__");
            AddLine(lines, "");

            foreach (CScript i in scripts)
            {
                if (i.description != null)
                    AddLine(lines, "#define SCRIPT_" + i.name + "\t" + "(" + i.id + ") \t\t // " + i.description);
                else
                    AddLine(lines, "#define SCRIPT_" + i.name + "\t" + "(" + i.id + ")");
            }

            AddLine(lines, "");
            AddLine(lines, "#endif // __SCRIPTS__");
            File.WriteAllLines(".\\_scripts.fos", lines.ToArray());
        }

        static void Main(string[] args)
        {
            List<string> lines = new List<string>();
            List<CScript> scripts = new List<CScript>();

            string scriptlist = ".\\scripts.lst";

            if (!File.Exists(scriptlist))
                ExitGracefully("scripts.lst not found");
            
            lines.AddRange(File.ReadAllLines(scriptlist));

            
            
            foreach (string str in lines)
            {
                char[] seperators = { ' ', '\t' };
                string[] tokens = str.Split(seperators);

                if (tokens.Length < 3)
                    continue;

                if (tokens[0] == "$")
                {
                    CScript script = new CScript();
                    script.id = Convert.ToInt32(tokens[1]);

                    int num = new Int32();

                    for (int i = 2; i < tokens.Length; i++)
                    {
                        if (tokens[i] != "")
                        {
                            script.name = tokens[i];
                            num = i;
                            break;
                        }
                    }

                    for (int i = num; i < tokens.Length; i++)
                    {
                        if (tokens[i] == "#")
                        {
                            script.description = String.Join(" ", tokens, i + 1, tokens.Length - (i + 1));
                            script.description = script.description.Trim();
                            break;
                        }
                    }
                    scripts.Add(script);
                }
                
            }

            Console.Out.WriteLine("Generating _scripts.fos...");
            GenerateScriptFile(scripts);
            ExitGracefully("Generation completed!");
        }
    }
}
