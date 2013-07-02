// dllmain.cpp : Defines the entry point for the DLL application.
#include "stdafx.h"
#include <iostream>
#include <fstream>
#include "log.h"
#include "config.h"
#include "datatables.h"
#include "gametext.h"
#include "faction.h"
#include "../fonline2238.h"

using namespace std;

SQLLog* dblog;
Config* cfg;
MYSQL* conn;
GameText* foobj;

bool isCompiler=true;

int __stdcall DllMain(void* module, unsigned long reason, void* reserved)
{
	switch (reason)
	{
	case DLL_PROCESS_ATTACH:
		break;
	case DLL_THREAD_ATTACH:
		break;
	case DLL_THREAD_DETACH:
		break;
	case DLL_PROCESS_DETACH:
		//CleanFactionType();
		if(!isCompiler)
		{
			if(cfg) delete cfg;
			if(dblog) delete dblog;
			if(conn) mysql_close(conn);
			if(foobj) delete foobj;
		}
		break;
	}
	return TRUE;
}

FONLINE_DLL_ENTRY(compiler)
{
	isCompiler=compiler;
	if(!isCompiler)
	{
		cfg = new Config("FOnlineServer.cfg");
		if(!cfg->IsCompiled()) 
		{
			conn = mysql_init(NULL);
			if(!mysql_real_connect(conn, 
				cfg->GetValue("SqlHost").c_str(), 
				cfg->GetValue("SqlUser").c_str(), 
				cfg->GetValue("SqlPass").c_str(), 
				cfg->GetValue("SqlDB").c_str(),
				0, NULL, 0))
			{
				mysql_close(conn);
				conn = NULL;
				ilog("Couldn't connect to database: " + cfg->GetValue("SqlHost") + ", user: " + cfg->GetValue("SqlUser"));
			}
			if(conn) dblog = new SQLLog(conn);
			foobj = new GameText("text/engl/foobj.msg");
		}
	}
	if(ASEngine) 
	{
		//RegisterFactionType(ASEngine, conn);
	}
}

EXPORT
bool __cdecl dbInitTable(ScriptString* table, ScriptString* columns)
{ 
	return dblog->InitTable(table->c_std_str(), columns->c_std_str()); // or change function args to cstr
}
EXPORT 
bool __cdecl dbLog(ScriptString* table, ScriptString* values)
{
	return dblog->Write(table->c_std_str(), values->c_std_str()); // or change function args to cstr
}
EXPORT 
void __cdecl dbFastLog(ScriptString* table, ScriptString* values)
{
	dblog->FastWrite(table->c_std_str(), values->c_std_str());
}

EXPORT
void __cdecl dbUpdateProtoItem(ProtoItem* proto)
{
	UpdateProtoItem(conn, foobj, proto);
}

EXPORT void __cdecl Dummy()
{
	cout << "ewwwe";
}