// Author:  Wipe/WHINE Team

#ifndef __CLIENT_ONLINE_STATS_CPP__
#define __CLIENT_ONLINE_STATS_CPP__

#ifndef __CLIENT
#define __CLIENT
#endif
#define NAMES_MAX	(100)

#include "../fonline2238.h"
//#include "online_stats.h"

#include "xfiregameclient.h"

#include <windows.h>
#include <shellapi.h>

#pragma comment(lib, "shell32.lib")

EXPORT void OnlineStats_URL( ScriptString& url )
{
	ShellExecute( NULL, "open", url.c_str(), NULL, NULL, SW_SHOWNORMAL );
};

/*****************
 * XFire support *
 *****************/
 
bool Init=false;
const char *names[NAMES_MAX];
const char *values[NAMES_MAX];

void XFireDLL_InfoInit()
{
	for(uint i=0;i<NAMES_MAX;i++) names[i]=values[i]=NULL;
}

EXPORT void XFireDLL_InfoSet( uint id, ScriptString& name, ScriptString& value )
{
	if(!Init)
	{
		XFireDLL_InfoInit();
		Init=true;
	}
	names[id] = _strdup(name.c_str());
	values[id] = _strdup(value.c_str());
};

EXPORT void XFireDLL_InfoSend( void )
{
	uint total = 0;
	for( uint i=0; i<NAMES_MAX; i++ )
	{
		if( names[i] == NULL || names[i] == "" )
			break;
		total++;
	};

	if( XfireIsLoaded() == 1 )
		XfireSetCustomGameData( total, names, values );
};

EXPORT void XFireDLL_InfoCleanup( void )
{
	for( uint i=0; i<NAMES_MAX; i++ )
	{
		if(!names[i])
		{
			free((char*)(names[i]));
			names[i] = NULL;
		}
	};
};

#endif /* __CLIENT_ONLINE_STATS_CPP__ */
