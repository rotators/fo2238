/*
 *
 * WHINE TEAM
 * We Had Ini, Now Engine
 *
 */

#ifndef __BUFFER_LAZY__
#define __BUFFER_LAZY__

#define SKIP_PRAGMAS
#define FO2238

#ifdef FO2238
	#include "../fonline2238.h"
#else
	#include "../fonline_tla.h"
#endif

#ifdef __MAPPER
	#error "This is NOT mapper dll"
#endif

#include <string>

using namespace std;

// There's a blood on this code.

bool ParseLocalScriptName( const ScriptString& scriptfunc, string& module, string& function )
{
	int pos = scriptfunc.c_std_str().find_first_of( "@" );

	if( pos > 0 )
	{
		string moduleName = scriptfunc.c_std_str().substr( 0, pos );
		string functionDecl = "void ";
		functionDecl += scriptfunc.c_std_str().substr( pos + 1 );
#if defined(__CLIENT)
		functionDecl += "(int,int,int,string@,int[]@)";
#elif defined(__SERVER)
		functionDecl += "(Critter&,int,int,int,string@,int[]@)";
#endif

		module = moduleName;
		function = functionDecl;

		return( true );
	}
	else
		return( false );
}

EXPORT bool Global_IsLocalScript( const ScriptString& scriptfunc )
{
	string module;
	string function;

	if( ParseLocalScriptName( scriptfunc, module, function ))
	{
		asIScriptModule* as_module = ASEngine->GetModule( module.c_str(), asGM_ONLY_IF_EXISTS );
		if( !as_module )
			return( false );

		asIScriptFunction* as_function = as_module->GetFunctionByDecl( function.c_str() );
		if( as_function )
			return( true );
	}

	return( false );
}

#if defined(__CLIENT)
EXPORT void Global_RunLocalScript( const ScriptString& scriptfunc, int p0, int p1, int p2, const ScriptString* p3, ScriptArray* p4 )
#elif defined(__SERVER)
EXPORT void Critter_RunLocalScript( Critter& cr, const ScriptString& scriptfunc, int p0, int p1, int p2, const ScriptString* p3, ScriptArray* p4 )
#endif
{
	if( Global_IsLocalScript( scriptfunc ))
	{
		string module, function;

		if( ParseLocalScriptName( scriptfunc, module, function ))
		{
			int bindId = FOnline->ScriptBind( module.c_str(), function.c_str(), true );
			if( bindId > 0 && FOnline->ScriptPrepare( bindId ))
			{
				#ifdef __SERVER
				FOnline->ScriptSetArgObject( &cr );
				#endif
				FOnline->ScriptSetArgInt( p0 );
				FOnline->ScriptSetArgInt( p1 );
				FOnline->ScriptSetArgInt( p2 );
				FOnline->ScriptSetArgObject( (void*)p3 );
				FOnline->ScriptSetArgObject( p4 );
				FOnline->ScriptRunPrepared();
			}
			else
			{
				Log( "RunLocalScript : cannot bind module<%s> function<%s>\n",
					module.c_str(), function.c_str() );
			}
		}
		else
		{
			Log( "RunLocalScript : invalid function name<%s>\n", scriptfunc.c_str() );
		}
	}
	else
	{
		Log( "RunLocalScript : function<%s> not found\n", scriptfunc.c_str() );
	}
}

#endif // __BUFFER_LAZY__ //
