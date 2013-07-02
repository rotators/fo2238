using System;
using System.Collections.Generic;
using System.Text;

namespace PerkEditor
{
    public static class PerkData
    {
        public const int Enabled = 0;
        public const int Support = 1;
        public const int Disabled = 2;
    }

    public class Requirement : ICloneable
    {
        public int Param;
        public bool AtLeast; // if false then AtMost
        public int Value;
        public Requirement(int param, bool atleast, int value)
        {
            Param = param;
            AtLeast = atleast;
            Value = value;
        }
        public override string ToString()
        {
            String s = Config.MsgParser.GetMSGValue(Param * 10 + 100001);
            if (AtLeast) s += " at least "; else s += " at most ";
            s += Value;
            return s;
        }
        public object Clone()
        {
            Requirement r = new Requirement(Param, AtLeast, Value);
            return r;
        }
    }

    public class Effect : ICloneable
    {
        public int Param;
        public bool Increase;
        public int Value;
        public Effect(int param, bool increase, int value)
        {
            Param = param;
            Increase = increase;
            Value = value;
        }
        public override string ToString()
        {
            String s = Config.MsgParser.GetMSGValue(Param * 10 + 100001);
            if (Increase) s += " increased by "; else s += " decreased by ";
            s += Value;
            return s;
        }
        public object Clone()
        {
            Effect r = new Effect(Param, Increase, Value);
            return r;
        }
    }

    public class LevelData
    {
        public List<Requirement> Requirements;
        public List<Effect> UpEffects;
        public List<Effect> DownEffects;
        public bool JustRevert;
        public LevelData()
        {
            Requirements = new List<Requirement>();
            UpEffects = new List<Effect>();
            DownEffects = new List<Effect>();
            JustRevert = false;
        }
    }

    public class Perk : IComparable
    {
        public enum PerkType { Levelling, Support, Disabled };
        public int Id;
        public String Name;
        public String Desc;
        public int MaxLevel;
        public PerkType Type;
        public List<LevelData> Levels;

        public Perk(int id, String name, String desc)
        {
            Id = id;
            Name = name == null ? "" : name;
            Desc = desc == null ? "" : desc;
            MaxLevel = 1;
            Type = PerkData.Enabled;
            Levels = new List<LevelData>();
            EnsureLevel(0);
        }

        public void EnsureLevel(int lvl)
        {
            while (Levels.Count <= lvl) Levels.Add(new LevelData());
        }

        public int CompareTo(object other)
        {
            return this.Id - ((Perk)other).Id;
        }
    }
}
