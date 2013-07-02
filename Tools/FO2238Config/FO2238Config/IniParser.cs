using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
 
namespace FO2238Config
{
    public class IniParser
    {
        private Hashtable keyPairs = new Hashtable();
        private String iniFilePath;
     
        private struct SectionPair
        {
            public String Section;
            public String Key;
        }
     
        /// <summary>
        /// Opens the INI file at the given path and enumerates the values in the IniParser.
        /// </summary>
        /// <param name="iniPath">Full path to INI file.</param>
        public IniParser(String iniPath)
        {
            TextReader iniFile = null;
            String strLine = null;
            String currentRoot = null;
            String[] keyPair = null;
     
            iniFilePath = iniPath;

            if (File.Exists(iniPath))
            {
                try
                {
                    iniFile = new StreamReader(iniPath);

                    strLine = iniFile.ReadLine();

                    while (strLine != null)
                    {
                        strLine = strLine.Trim();

                        if (strLine != "")
                        {
                            if (strLine.StartsWith("[") && strLine.EndsWith("]"))
                            {
                                currentRoot = strLine.Substring(1, strLine.Length - 2);
                            }
                            else if (strLine.StartsWith("#") || strLine.StartsWith(";"))
                            {
                                // comment
                            }
                            else
                            {
                                keyPair = strLine.Split(new char[] { '=' }, 2);

                                SectionPair sectionPair;
                                String value = "";

                                if (currentRoot == null)
                                    currentRoot = "ROOT";

                                sectionPair.Section = currentRoot;
                                sectionPair.Key = keyPair[0];

                                if (keyPair.Length > 1)
                                    value = keyPair[1];

                                keyPairs.Add(sectionPair, value);
                            }
                        }

                        strLine = iniFile.ReadLine();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (iniFile != null)
                        iniFile.Close();
                    MakeDefaultConfig();
                }
            }
            else
            {
                MakeDefaultConfig();
                SaveSettings();
            }
            //throw new FileNotFoundException("Unable to locate " + iniPath);
        }
     
        /// <summary>
        /// Returns the value for the given section, key pair.
        /// </summary>
        /// <param name="sectionName">Section name.</param>
        /// <param name="settingName">Key name.</param>
        public String GetSetting(String sectionName, String settingName)
        {
            SectionPair sectionPair;
            sectionPair.Section = sectionName;
            sectionPair.Key = settingName;
            String ret = (String)keyPairs[sectionPair];
            if((ret==null) || ret.Length==0 || !keyPairs.Contains(sectionPair)) return "0";

            return ret;
        }
     
        /// <summary>
        /// Enumerates all lines for given section.
        /// </summary>
        /// <param name="sectionName">Section to enum.</param>
        public String[] EnumSectionLines(String sectionName)
        {
            ArrayList tmpArray = new ArrayList();
     
            foreach (SectionPair pair in keyPairs.Keys)
            {
                if (pair.Section == sectionName)
                    tmpArray.Add(pair.Key+"="+keyPairs[pair]);
            }
     
            return (String[])tmpArray.ToArray(typeof(String));
        }

        public List<KeyValuePair<String,String>> EnumSection(String sectionName)
        {
            List<KeyValuePair<String, String>> tmpArray = new List<KeyValuePair<String, String>>();

            foreach (SectionPair pair in keyPairs.Keys)
            {
                if (pair.Section == sectionName)
                    tmpArray.Add(new KeyValuePair<String,String>(pair.Key,keyPairs[pair].ToString()));
            }

            return tmpArray;
        }

        /// <summary>
        /// Adds or replaces a setting to the table to be saved.
        /// </summary>
        /// <param name="sectionName">Section to add under.</param>
        /// <param name="settingName">Key name to add.</param>
        /// <param name="settingValue">Value of key.</param>
        public void AddSetting(String sectionName, String settingName, String settingValue)
        {
            SectionPair sectionPair;
            sectionPair.Section = sectionName;
            sectionPair.Key = settingName;
     
            if (keyPairs.ContainsKey(sectionPair))
                keyPairs.Remove(sectionPair);
     
            keyPairs.Add(sectionPair, settingValue);
        }
     
        /// <summary>
        /// Adds or replaces a setting to the table to be saved with a null value.
        /// </summary>
        /// <param name="sectionName">Section to add under.</param>
        /// <param name="settingName">Key name to add.</param>
        public void AddSetting(String sectionName, String settingName)
        {
            AddSetting(sectionName, settingName, null);
        }
     
        /// <summary>
        /// Remove a setting.
        /// </summary>
        /// <param name="sectionName">Section to add under.</param>
        /// <param name="settingName">Key name to add.</param>
        public void DeleteSetting(String sectionName, String settingName)
        {
            SectionPair sectionPair;
            sectionPair.Section = sectionName;
            sectionPair.Key = settingName;
     
            if (keyPairs.ContainsKey(sectionPair))
                keyPairs.Remove(sectionPair);
        }

        public bool IsSetting(String sectionName, String settingName)
        {
            SectionPair sectionPair;
            sectionPair.Section = sectionName;
            sectionPair.Key = settingName;

            return keyPairs.ContainsKey(sectionPair);
        }

        public void AddNewSetting(String sectionName, String settingName, String settingValue)
        {
            if(IsSetting(sectionName,settingName)) return;
            AddSetting(sectionName, settingName, settingValue);
        }

        public void DeleteSection(String sectionName)
        {
            ArrayList pairs = new ArrayList();
            foreach (SectionPair pair in keyPairs.Keys)
                if (pair.Section == sectionName) pairs.Add(pair);
            foreach (SectionPair pair in pairs) keyPairs.Remove(pair);
        }

        /// <summary>
        /// Save settings to new file.
        /// </summary>
        /// <param name="newFilePath">New file path.</param>
        public void SaveSettings(String newFilePath)
        {
            ArrayList sections = new ArrayList();
            String tmpValue = "";
            String strToSave = "";
     
            foreach (SectionPair sectionPair in keyPairs.Keys)
            {
                if (!sections.Contains(sectionPair.Section))
                    sections.Add(sectionPair.Section);
            }

            sections.Sort();

            foreach (String section in sections)
            {
                strToSave += ("[" + section + "]\r\n");
     
                foreach (SectionPair sectionPair in keyPairs.Keys)
                {
                    if (sectionPair.Section == section)
                    {
                        tmpValue = (String)keyPairs[sectionPair];
     
                        if (tmpValue != null)
                            tmpValue = "=" + tmpValue;
     
                        strToSave += (sectionPair.Key + tmpValue + "\r\n");
                    }
                }
     
                strToSave += "\r\n";
            }
     
            try
            {
                TextWriter tw = new StreamWriter(newFilePath);
                tw.Write(strToSave);
                tw.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
     
        /// <summary>
        /// Save settings back to ini file.
        /// </summary>
        public void SaveSettings()
        {
            SaveSettings(iniFilePath);
        }

        private void MakeDefaultConfig()
        {
            AddNewSetting("2238", "DisplayNames", "0"); // bool
            AddNewSetting("2238", "Autosave", "0"); // bool
            AddNewSetting("2238", "MouseScroll", "1"); // bool
            AddNewSetting("2238", "Awareness", "1"); // bool
            AddNewSetting("2238", "DisplayTC", "1"); // bool
            AddNewSetting("2238", "DisplayTCZones", "0"); // 0 to 2

            AddNewSetting("Timeouts", "Enabled", "1"); // bool
            AddNewSetting("Timeouts", "Direction", "1"); // bool
            AddNewSetting("Timeouts", "Font", "5"); // 4 to 8
            AddNewSetting("Timeouts", "NameColor", "0 255 0"); // color
            AddNewSetting("Timeouts", "ValueColor", "0 255 0"); // color
            AddNewSetting("Timeouts", "Transparency", "25"); // 0 to 100

            AddNewSetting("Timeouts", "PositionX", "-145"); // signed
            AddNewSetting("Timeouts", "PositionY", "-110"); // signed
            AddNewSetting("Timeouts", "SpacingX", "72"); // unsigned
            AddNewSetting("Timeouts", "SpacingY", "10"); // unsigned

            AddNewSetting("Timeouts", "Worldmap", "0"); // bool
            AddNewSetting("Timeouts", "WorldmapX", "-145"); // signed
            AddNewSetting("Timeouts", "WorldmapY", "-110"); // signed

            AddNewSetting("Bindings", "AimHead", "Ctrl Alt Del");
            AddNewSetting("Bindings", "AimEyes", "Ctrl Alt Del");
            AddNewSetting("Bindings", "AimUncalled", "Ctrl Alt Del");
            AddNewSetting("Bindings", "AimGroin", "Ctrl Alt Del");
            AddNewSetting("Bindings", "AimLeftArm", "Ctrl Alt Del");
            AddNewSetting("Bindings", "AimRightArm", "Ctrl Alt Del");
            AddNewSetting("Bindings", "AimLeftLeg", "Ctrl Alt Del");
            AddNewSetting("Bindings", "AimRightLeg", "Ctrl Alt Del");
            AddNewSetting("Bindings", "AimTorso", "Ctrl Alt Del");
        }
    }
}