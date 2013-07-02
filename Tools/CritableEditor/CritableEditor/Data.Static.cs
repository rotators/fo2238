using System;
using System.Collections.Generic;
using System.Text;

namespace CritableEditor
{
    public static partial class Data
    {
        public const int HF_KNOCKOUT           = 0x00000001;
        public const int HF_KNOCKDOWN          = 0x00000002;
        public const int HF_CRIPPLED_LEFT_LEG  = 0x00000004;
        public const int HF_CRIPPLED_RIGHT_LEG = 0x00000008;
        public const int HF_CRIPPLED_LEFT_ARM  = 0x00000010;
        public const int HF_CRIPPLED_RIGHT_ARM = 0x00000020;
        public const int HF_BLINDED            = 0x00000040;
        public const int HF_DEATH              = 0x00000080;
        public const int HF_ON_FIRE            = 0x00000400;
        public const int HF_BYPASS_ARMOR       = 0x00000800;
        public const int HF_DROPPED_WEAPON     = 0x00004000;
        public const int HF_LOST_NEXT_TURN     = 0x00008000;
        public const int HF_RANDOM             = 0x00200000;

        public static Dictionary<int, string> Dict;

        public static List<String> Header;
        public static String MarkerLine = @"// DO NOT CHANGE THE CONTENTS OF THIS FILE BELOW AND INCLUDING THIS LINE IN ANY OTHER WAY THAN BY USING CritableEditor TOOL!";

        public static string[] Initstring = 
        {
            " /* Head */    ",
            " /* LArm */    ",
            " /* RArm */    ",
            " /* Torso */   ",
            " /* RLeg */    ",
            " /* LLeg */    ",
            " /* Eyes */    ",
            " /* Groin */   ",
            " /* Uncalled */"
        };

        public static string[] Bodypart =
        {
            "Head",
            "Left arm",
            "Right arm",
            "Torso",
            "Right leg",
            "Left leg",
            "Eyes",
            "Groin",
            "Uncalled"
        };

        public static bool Inited = false;
        public static string FileInput = "";
        public static string FileOutput = "";
        public static string FOCombat = "";
        public static string FileHTML = "";

        public static string[] Bodytype = 
        {
            "Men",
            "Women",
            "Children",
            "Super mutant",
            "Ghoul",
            "Brahmin",
            "Radscorpion",
            "Rat",
            "Floater",
            "Centaur",
            "Robot",
            "Dog",
            "Mantis",
            "Deathclaw",
            "Spore plant",
            "Gecko",
            "Alien",
            "Giant ant",
            "Big bad boss",
            "Player"
        };

        public static string[] StatNames = 
        {
            "None",
            "ST_STRENGHT",
            "ST_PERCEPTION",
            "ST_ENDURANCE",
            "ST_CHARISMA",
            "ST_INTELLECT",
            "ST_AGILITY",
            "ST_LUCK",
            "ST_MAX_LIFE",
            "ST_ACTION_POINTS",
            "ST_ARMOR_CLASS",
            "ST_UNARMED_DAMAGE",
            "ST_MELEE_DAMAGE",
            "ST_CARRY_WEIGHT",
            "ST_SEQUENCE",
            "ST_HEALING_RATE",
            "ST_CRITICAL_CHANCE",
            "ST_MAX_CRITICAL",
            "ST_NORMAL_ABSORB",
            "ST_LASER_ABSORB",
            "ST_FIRE_ABSORB",
            "ST_PLASMA_ABSORB",
            "ST_ELECTRO_ABSORB",
            "ST_EMP_ABSORB",
            "ST_EXPLODE_ABSORB",
            "ST_NORMAL_RESIST",
            "ST_LASER_RESIST",
            "ST_FIRE_RESIST",
            "ST_PLASMA_RESIST",
            "ST_ELECTRO_RESIST",
            "ST_EMP_RESIST",
            "ST_EXPLODE_RESIST",
            "ST_RADIATION_RESISTANCE",
            "ST_POISON_RESISTANCE",
            "ST_AGE",
            "ST_GENDER",
            "ST_CURRENT_HP",
            "ST_POISONING_LEVEL",
            "ST_RADIATION_LEVEL",
            "ST_CURRENT_AP",
            "ST_EXPERIENCE",
            "ST_LEVEL",
            "ST_UNSPENT_SKILL_POINTS",
            "ST_UNSPENT_PERKS",
            "ST_KARMA",
            "ST_FOLLOW_CRIT",
            "ST_REPLICATION_MONEY",
            "ST_REPLICATION_COUNT",
            "ST_REPLICATION_TIME",
            "ST_RATE_OBJECT",
            "ST_TURN_BASED_AC",
            "ST_MAX_MOVE_AP",
            "ST_MOVE_AP",
            "ST_NPC_ROLE",
            "ST_SCRIPT_VAR0",
            "ST_SCRIPT_VAR1",
            "ST_SCRIPT_VAR2",
            "ST_SCRIPT_VAR3",
            "ST_SCRIPT_VAR4",
            "ST_SCRIPT_VAR5",
            "ST_SCRIPT_VAR6",
            "ST_SCRIPT_VAR7",
            "ST_SCRIPT_VAR8",
            "ST_SCRIPT_VAR9",
            "ST_PLAYER_KARMA",
            "ST_BONUS_LOOK",
            "ST_REPLICATION_COST",
            "ST_FREE_BARTER_PLAYER",
            "ST_LAST_STEAL_CR_ID",
            "ST_STEAL_COUNT"
        };

        public static int[] Table = new int[7560];
    }
}
