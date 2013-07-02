using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Overseer
{
    public class Config : FOCommon.Config
    {
        // [Paths]
        public string PathDataFolder;
        public string PathDialogsFolder;
        public string PathMapsFolder;
        public string PathScriptsFolder;
        public string PathTextFolder;

        public string PathBags;

        //public string PathDefines;
        //public string PathClientDefines;
        public string PathMapperDefines;
        public string PathNpcPid;
        public string PathItemPid;

        // [Lists]
        public string ListPidsGlobal;
        public string ListPidsLocal;

        public Config( String ConfigName )
            : base( ConfigName )
        {
            AddConfigItem( false, "Paths", "Data", "PathDataFolder", ".\\Data\\" );
            AddConfigItem( false, "Paths", "Dialogs", "PathDialogsFolder", ".\\dialogs\\" );
            AddConfigItem( false, "Paths", "Scripts", "PathScriptsFolder", ".\\scripts\\" );
            AddConfigItem( false, "Paths", "Maps", "PathMapsFolder", ".\\maps\\" );
            AddConfigItem( false, "Paths", "Text", "PathTextFolder", ".\\text\\" );

            AddConfigItem( false, "Paths", "Bags", "PathBags", "Bags.cfg" );

            //AddConfigItem( false, "Paths", "Defines", "PathDefines", "_defines.fos" );
            //AddConfigItem( false, "Paths", "Defines", "PathClientDefines", "_defines.fos" );
            AddConfigItem( false, "Paths", "MapperDefines", "PathMapperDefines", "_mapper_defines.fos" );
            AddConfigItem( false, "Paths", "NpcPid", "PathNpcPid", "_npc_pids.fos" );
            AddConfigItem( false, "Paths", "ItemPid", "PathItemPid", "_itempid.fos" );

            AddConfigItem( false, "Lists", "Pids", "ListPidsGlobal", "" );
            AddConfigItem( true, "Lists", "Pids", "ListPidsLocal", "" );
        }
    }
}
